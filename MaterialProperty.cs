using System;

namespace UnityEngine
{
    using Rendering;

    public class MaterialProperty
    {
        protected Shader sha;
        protected Material mat;
        protected int index;

        public Shader Shader => sha;
        public Material Material => mat;
        public int Index => index;
        public string Name => sha.GetPropertyName(index);
        public int NameID => sha.GetPropertyNameId(index);
        public string Description => sha.GetPropertyDescription(index);
        public string[] Attributes => sha.GetPropertyAttributes(index);
        public object Value
        {
            get
            {
                switch (Type)
                {
                    case ShaderPropertyType.Color:
                        return mat.GetColor(Name);
                    case ShaderPropertyType.Vector:
                        return mat.GetVector(Name);
                    case ShaderPropertyType.Float:
                        return mat.GetFloat(Name);
                    case ShaderPropertyType.Range:
                        return mat.GetFloat(Name);
                    case ShaderPropertyType.Texture:
                        return mat.GetTexture(Name);
                    default:
                        return null;
                }
            }
        }
        public ShaderPropertyType Type => sha.GetPropertyType(index);
        public ShaderPropertyFlags Flags => sha.GetPropertyFlags(index);

        public MaterialProperty(int index, Material mat)
        {
            this.sha = mat.shader;
            this.mat = mat;
            this.index = index;
        }
    }

    public class ColorProperty : MaterialProperty
    {
        public Color DefaultValue => sha.GetPropertyDefaultVectorValue(index);

        public Color GetValue() => mat.GetColor(Name);
        public void SetValue(Color val) => mat.SetColor(Name, val);

        public ColorProperty(int index, Material mat) : base(index, mat)
        {
            if (mat.shader.GetPropertyType(index) != ShaderPropertyType.Color)
                throw new NotSupportedException("property must be a color");
        }
    }

    public class TextureProperty : MaterialProperty
    {
        public string DefaultName => sha.GetPropertyTextureDefaultName(index);
        public Texture2D DefaultValue
        {
            get
            {
                switch (DefaultName)
                {
                    case "black":
                        return Texture2D.blackTexture;
                    case "white":
                        return Texture2D.whiteTexture;
                    case "red":
                        return Texture2D.redTexture;
                    case "bump":
                        return ShaderUtils.bumpTexture;
                    default:
                        return Texture2D.grayTexture;
                }
            }
        }
        public TextureDimension Dimension => sha.GetPropertyTextureDimension(index);
        public Vector2 Offset => mat.GetTextureOffset(Name);
        public Vector2 Scale => mat.GetTextureScale(index);

        public Texture GetValue() => mat.GetTexture(Name);
        public void SetValue(Texture val) => mat.SetTexture(Name, val);

        public TextureProperty(int index, Material mat) : base(index, mat)
        {
            if (Type != ShaderPropertyType.Texture)
                throw new NotSupportedException("property must be a texture");
        }
    }

    public class VectorProperty : MaterialProperty
    {
        public Vector4 DefaultValue => sha.GetPropertyDefaultVectorValue(index);

        public Vector4 GetValue() => mat.GetVector(Name);
        public void SetValue(Vector4 val) => mat.SetVector(Name, val);

        public VectorProperty(int index, Material mat) : base(index, mat)
        {
            if (Type != ShaderPropertyType.Vector)
                throw new NotSupportedException("property must be a vector");
        }
    }

    public class FloatProperty : MaterialProperty
    {
        public float DefaultValue => sha.GetPropertyDefaultFloatValue(index);

        public float GetValue() => mat.GetFloat(Name);
        public void SetValue(float val) => mat.SetFloat(Name, val);

        public FloatProperty(int index, Material mat) : base(index, mat)
        {
            if (Type != ShaderPropertyType.Float)
                throw new NotSupportedException("property must be a float");
        }
    }

    public class RangeProperty : MaterialProperty
    {
        public float DefaultValue => sha.GetPropertyDefaultFloatValue(index);
        public Vector2 RangeLimits => sha.GetPropertyRangeLimits(index);

        public float GetValue() => mat.GetFloat(Name);
        public void SetValue(float val) => mat.SetFloat(Name, val);

        public RangeProperty(int index, Material mat) : base(index, mat)
        {
            if (Type != ShaderPropertyType.Range)
                throw new NotSupportedException("property must be a range");
        }
    }
}
