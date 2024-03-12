using System;
using Unity;
using UnityEngine;

namespace Assets.Scrip.Utilities
{
    [Serializable]
    public class MaterialSerializer : MonoBehaviour
    {
        public string shaderName;
        public Color mainColor;
    }

    [Serializable]
    public class ConvertMaterial : MonoBehaviour
    {
        public static MaterialSerializer ConvertMaterialToText(Material material)
        {
            MaterialSerializer serializer = new MaterialSerializer();
            serializer.shaderName = material.shader.name;
            serializer.mainColor = material.color;
            return serializer;
        }

        public static Material ConvertTextToMaterial(MaterialSerializer materialSerializer)
        {
            Material material = new Material(Shader.Find(materialSerializer.shaderName));
            material.color = materialSerializer.mainColor;
            return material;
        }
    }
}
