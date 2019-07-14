using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lasm.UnityUtilities
{
    public static class TextureUtilities
    {
        /// <summary>
        /// Create a texture of one pixel of a single color.
        /// </summary>
        /// <param name="color">The color of the texture.</param>
        /// <returns>A single pixel single color texture</returns>
        public static Texture2D SinglePixelTexture(Color color)
        {
            var colorTexture = new Texture2D(1, 1);
            colorTexture.SetPixel(0, 0, color);
            colorTexture.Apply();

            return colorTexture;
        }

        /// <summary>
        /// Takes a texture and returns a copy tinted.
        /// </summary>
        /// <param name="texture">The the texture to tint.</param>
        /// <param name="tint">The color to tint the texture.</param>
        /// <returns></returns>
        public static Texture2D TintedTexture(Texture2D texture, Color tint)
        {
            Texture2D tintedTexture = Texture2D.Instantiate(texture);
            Color[] textureColors = texture.GetPixels();

            for (int i = 0; i < textureColors.Length; i++)
            {
                textureColors[i] = Color.Lerp(textureColors[i], tint, 0.5f);
            }

            tintedTexture.SetPixels(textureColors);
            tintedTexture.Apply();

            return tintedTexture;
        }
    }
}
