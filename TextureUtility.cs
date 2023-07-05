// Decompiled with JetBrains decompiler
// Type: TextureUtility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class TextureUtility : MonoBehaviour
{
  public static Sprite ColorTextureIntoSprite(
    Texture2D targetTexture,
    Color32[] sourceLogoMap,
    Color32[] targetLogoMap,
    Color redReplacement,
    Color greenReplacement,
    Color blueReplacement,
    Color whiteReplacement)
  {
    Texture2D texture = TextureUtility.ColorTexture(targetTexture, sourceLogoMap, targetLogoMap, redReplacement, greenReplacement, blueReplacement, whiteReplacement);
    return Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) texture.width, (float) texture.height), new Vector2(0.5f, 0.5f));
  }

  public static Texture2D ColorTexture(
    Texture2D targetTexture,
    Color32[] sourceLogoMap,
    Color32[] targetLogoMap,
    Color redReplacement,
    Color greenReplacement,
    Color blueReplacement,
    Color whiteReplacement)
  {
    int num = 150;
    for (int index = 0; index < sourceLogoMap.Length; ++index)
      targetLogoMap[index] = (double) sourceLogoMap[index].a >= 1.0 ? ((int) sourceLogoMap[index].r <= num || (int) sourceLogoMap[index].g <= num || (int) sourceLogoMap[index].b <= num ? ((int) sourceLogoMap[index].r <= num ? ((int) sourceLogoMap[index].g <= num ? (Color32) blueReplacement : (Color32) greenReplacement) : (Color32) redReplacement) : (Color32) whiteReplacement) : (Color32) Color.clear;
    targetTexture.SetPixels32(targetLogoMap);
    targetTexture.Apply();
    return targetTexture;
  }

  public static void WriteTextureOnTopOfTexture(
    Texture2D baseTexture,
    Texture2D textureToWrite,
    int xLocation,
    int yLocation)
  {
    Color[] pixels1 = textureToWrite.GetPixels();
    Color[] pixels2 = baseTexture.GetPixels(xLocation - textureToWrite.width / 2, yLocation - textureToWrite.height / 2, textureToWrite.width, textureToWrite.height);
    Color[] colorArray = new Color[pixels1.Length];
    for (int index = 0; index < pixels1.Length; ++index)
      colorArray[index] = (double) pixels1[index].a != 0.0 ? ((double) pixels1[index].a != 1.0 ? new Color((float) ((double) pixels1[index].r * (double) pixels1[index].a + (double) pixels2[index].r * (1.0 - (double) pixels1[index].a)), (float) ((double) pixels1[index].g * (double) pixels1[index].a + (double) pixels2[index].g * (1.0 - (double) pixels1[index].a)), (float) ((double) pixels1[index].b * (double) pixels1[index].a + (double) pixels2[index].b * (1.0 - (double) pixels1[index].a))) : pixels1[index]) : pixels2[index];
    Color[] colors = colorArray;
    baseTexture.SetPixels(xLocation - textureToWrite.width / 2, yLocation - textureToWrite.height / 2, textureToWrite.width, textureToWrite.height, colors);
    baseTexture.Apply();
  }

  public static void RotateTexture(Texture2D texture, int rotation)
  {
    if (rotation != 0 && rotation != 90 && rotation != 180 && rotation != 270)
      return;
    Color32[] pixels32 = texture.GetPixels32();
    Color32[] colors = (Color32[]) null;
    int width = texture.width;
    int height = texture.height;
    switch (rotation)
    {
      case 0:
        return;
      case 90:
        colors = new Color32[width * height];
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < width; ++index2)
            colors[height - 1 - index1 + index2 * height] = pixels32[index2 + index1 * width];
        }
        break;
      case 180:
        colors = pixels32;
        Array.Reverse<Color32>(colors);
        break;
      case 270:
        colors = new Color32[texture.width * texture.height];
        for (int index3 = 0; index3 < height; ++index3)
        {
          for (int index4 = 0; index4 < width; ++index4)
            colors[height - 1 - index3 + index4 * height] = pixels32[index4 + index3 * width];
        }
        Array.Reverse<Color32>(colors);
        break;
    }
    texture.Reinitialize(height, width);
    texture.SetPixels32(colors);
    texture.Apply();
  }

  public static void ResizeTexture(Texture2D texture, int width, int height)
  {
    if (width <= 0 || height <= 0)
      return;
    TextureUtility.ScaleTexture(texture, width, height);
  }

  public static void ScaleTexture(Texture2D tex, int width, int height) => TextureScale.Bilinear(tex, width, height);

  public static void ScaleTexture(Texture2D tex, float percentage) => TextureScale.Bilinear(tex, (int) ((double) tex.width * (double) percentage), (int) ((double) tex.height * (double) percentage));
}
