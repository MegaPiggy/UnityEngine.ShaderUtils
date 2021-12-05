using System.Collections.Generic;

namespace UnityEngine
{
    using Rendering;

    public static class ShaderUtils
    {
        private static Texture2D bump;
        public static Texture2D bumpTexture
        {
            get
            {
                if (bump == null)
                {
                    bump = Object.Instantiate(Texture2D.normalTexture);
                    bump.name = "UnityBump";
                    Color fillColor = new Color(0.5f, 0.5f, 1f, 0.5f);
                    Color[] fillColorArray = bump.GetPixels();
                    for (int i = 0; i < fillColorArray.Length; ++i)
                        fillColorArray[i] = fillColor;
                    bump.SetPixels(fillColorArray);
                    bump.Apply();
                    bump.hideFlags = Texture2D.normalTexture.hideFlags;
                }
                return bump;
            }
        }

        public static MaterialProperty[] GetShaderProperties(this Shader sha) => new Material(sha).GetMaterialProperties();

        public static MaterialProperty[] GetMaterialProperties(this Material mat)
        {
            Shader sha = mat.shader;
            int n = sha.GetPropertyCount();
            List<MaterialProperty> properties = new List<MaterialProperty>();
            for (int i = 0; i < n; i++)
            {
                var type = sha.GetPropertyType(i);
                switch (type)
                {
                    case ShaderPropertyType.Color:
                        properties.Add(new ColorProperty(i, mat));
                        break;
                    case ShaderPropertyType.Vector:
                        properties.Add(new VectorProperty(i, mat));
                        break;
                    case ShaderPropertyType.Float:
                        properties.Add(new FloatProperty(i, mat));
                        break;
                    case ShaderPropertyType.Range:
                        properties.Add(new RangeProperty(i, mat));
                        break;
                    case ShaderPropertyType.Texture:
                        properties.Add(new TextureProperty(i, mat));
                        break;
                    default:
                        properties.Add(new MaterialProperty(i, mat));
                        break;
                }
            }
            return properties.ToArray();
        }

        public static MaterialProperty GetMaterialPropertyFromNameId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                    return prop;
            }
            return null;
        }

        public static MaterialProperty GetMaterialProperty(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                    return prop;
            }
            return null;
        }

        public static MaterialProperty GetMaterialProperty(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop;
            }
            return null;
        }

        public static ColorProperty[] GetColorProperties(this Shader sha) => new Material(sha).GetColorProperties();

        public static ColorProperty[] GetColorProperties(this Material mat)
        {
            List<ColorProperty> colors = new List<ColorProperty>();
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop is ColorProperty colorProperty)
                    colors.Add(colorProperty);
            }
            return colors.ToArray();
        }

        public static ColorProperty GetColorPropertyFromNameId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                {
                    if (prop is ColorProperty colorProperty)
                        return colorProperty;
                    else
                        throw new System.Exception($"id {nameID} is not a color property!");
                }
            }
            return null;
        }

        public static ColorProperty GetColorProperty(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                {
                    if (prop is ColorProperty colorProperty)
                        return colorProperty;
                    else
                        throw new System.Exception($"property at index #{index} is not a color!");
                }
            }
            return null;
        }

        public static ColorProperty GetColorProperty(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is ColorProperty colorProperty)
                        return colorProperty;
                    else
                        throw new System.Exception($"\"{name}\" is not a color property!");
                }
            }
            return null;
        }

        public static TextureProperty[] GetTextureProperties(this Shader sha) => new Material(sha).GetTextureProperties();

        public static TextureProperty[] GetTextureProperties(this Material mat)
        {
            List<TextureProperty> textures = new List<TextureProperty>();
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop is TextureProperty textureProperty)
                    textures.Add(textureProperty);
            }
            return textures.ToArray();
        }

        public static TextureProperty GetTexturePropertyFromNameId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                {
                    if (prop is TextureProperty textureProperty)
                        return textureProperty;
                    else
                        throw new System.Exception($"id {nameID} is not a texture property!");
                }
            }
            return null;
        }

        public static TextureProperty GetTextureProperty(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                {
                    if (prop is TextureProperty textureProperty)
                        return textureProperty;
                    else
                        throw new System.Exception($"property at index #{index} is not a texture!");
                }
            }
            return null;
        }

        public static TextureProperty GetTextureProperty(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is TextureProperty textureProperty)
                        return textureProperty;
                    else
                        throw new System.Exception($"\"{name}\" is not a texture property!");
                }
            }
            return null;
        }

        public static FloatProperty[] GetFloatProperties(this Shader sha) => new Material(sha).GetFloatProperties();

        public static FloatProperty[] GetFloatProperties(this Material mat)
        {
            List<FloatProperty> floats = new List<FloatProperty>();
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop is FloatProperty floatProperty)
                    floats.Add(floatProperty);
            }
            return floats.ToArray();
        }

        public static FloatProperty GetFloatPropertyFromNameId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                {
                    if (prop is FloatProperty floatProperty)
                        return floatProperty;
                    else
                        throw new System.Exception($"id {nameID} is not a float property!");
                }
            }
            return null;
        }

        public static FloatProperty GetFloatProperty(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                {
                    if (prop is FloatProperty floatProperty)
                        return floatProperty;
                    else
                        throw new System.Exception($"property at index #{index} is not a float!");
                }
            }
            return null;
        }

        public static FloatProperty GetFloatProperty(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is FloatProperty floatProperty)
                        return floatProperty;
                    else
                        throw new System.Exception($"\"{name}\" is not a float property!");
                }
            }
            return null;
        }

        public static RangeProperty[] GetRangeProperties(this Shader sha) => new Material(sha).GetRangeProperties();

        public static RangeProperty[] GetRangeProperties(this Material mat)
        {
            List<RangeProperty> ranges = new List<RangeProperty>();
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop is RangeProperty rangeProperty)
                    ranges.Add(rangeProperty);
            }
            return ranges.ToArray();
        }

        public static RangeProperty GetRangePropertyFromNameId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                {
                    if (prop is RangeProperty rangeProperty)
                        return rangeProperty;
                    else
                        throw new System.Exception($"id {nameID} is not a range property!");
                }
            }
            return null;
        }

        public static RangeProperty GetRangeProperty(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                {
                    if (prop is RangeProperty rangeProperty)
                        return rangeProperty;
                    else
                        throw new System.Exception($"property at index #{index} is not a range!");
                }
            }
            return null;
        }

        public static RangeProperty GetRangeProperty(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is RangeProperty rangeProperty)
                        return rangeProperty;
                    else
                        throw new System.Exception($"\"{name}\" is not a range property!");
                }
            }
            return null;
        }

        public static VectorProperty[] GetVectorProperties(this Shader sha) => new Material(sha).GetVectorProperties();

        public static VectorProperty[] GetVectorProperties(this Material mat)
        {
            List<VectorProperty> vectors = new List<VectorProperty>();
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop is VectorProperty vectorProperty)
                    vectors.Add(vectorProperty);
            }
            return vectors.ToArray();
        }

        public static VectorProperty GetVectorPropertyFromNameId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                {
                    if (prop is VectorProperty vectorProperty)
                        return vectorProperty;
                    else
                        throw new System.Exception($"id {nameID} is not a vector property!");
                }
            }
            return null;
        }

        public static VectorProperty GetVectorProperty(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                {
                    if (prop is VectorProperty vectorProperty)
                        return vectorProperty;
                    else
                        throw new System.Exception($"property at index #{index} is not a vector!");
                }
            }
            return null;
        }

        public static VectorProperty GetVectorProperty(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is VectorProperty vectorProperty)
                        return vectorProperty;
                    else
                        throw new System.Exception($"\"{name}\" is not a vector property!");
                }
            }
            return null;
        }

        public static MaterialProperty[] GetUnknownProperties(this Shader sha) => new Material(sha).GetUnknownProperties();

        public static MaterialProperty[] GetUnknownProperties(this Material mat)
        {
            List<MaterialProperty> unknown = new List<MaterialProperty>();
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (!((prop is ColorProperty) || (prop is TextureProperty) || (prop is FloatProperty) || (prop is RangeProperty) || (prop is VectorProperty)))
                    unknown.Add(prop);
            }
            return unknown.ToArray();
        }

        public static string GetPropertyName(this Material mat, int index)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Index == index)
                    return prop.Name;
            }
            return string.Empty;
        }

        public static string GetPropertyNameFromId(this Material mat, int nameID)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.NameID == nameID)
                    return prop.Name;
            }
            return string.Empty;
        }

        public static int GetPropertyNameId(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop.NameID;
            }
            return -1;
        }

        public static string GetPropertyDescription(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop.Description;
            }
            return string.Empty;
        }

        public static string[] GetPropertyAttributes(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop.Attributes;
            }
            return null;
        }

        public static ShaderPropertyFlags GetPropertyFlags(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop.Flags;
            }
            return ShaderPropertyFlags.None;
        }

        public static object GetPropertyValue(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop.Value;
            }
            return null;
        }

        public static object GetPropertyDefaultValue(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is ColorProperty colorProperty)
                        return colorProperty.DefaultValue;
                    else if (prop is TextureProperty textureProperty)
                        return textureProperty.DefaultValue;
                    else if (prop is FloatProperty floatProperty)
                        return floatProperty.DefaultValue;
                    else if (prop is RangeProperty rangeProperty)
                        return rangeProperty.DefaultValue;
                    else if (prop is VectorProperty vectorProperty)
                        return vectorProperty.DefaultValue;
                }
            }
            return null;
        }

        public static Vector2 GetPropertyRangeLimits(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is RangeProperty rangeProperty)
                        return rangeProperty.RangeLimits;
                    else
                        throw new System.Exception($"\"{name}\" is not a range property!");
                }
            }
            return Vector2.zero;
        }

        public static TextureDimension GetPropertyTextureDimension(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                {
                    if (prop is TextureProperty textureProperty)
                        return textureProperty.Dimension;
                    else
                        throw new System.Exception($"\"{name}\" is not a texture property!");
                }
            }
            return TextureDimension.Unknown;
        }

        public static ShaderPropertyType GetPropertyType(this Material mat, string name)
        {
            foreach (MaterialProperty prop in mat.GetMaterialProperties())
            {
                if (prop.Name == name)
                    return prop.Type;
            }
            return (ShaderPropertyType)(-1);
        }
    }
}
