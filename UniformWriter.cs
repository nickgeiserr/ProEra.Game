// Decompiled with JetBrains decompiler
// Type: UniformWriter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UniformWriter : MonoBehaviour
{
  private static Dictionary<FontType, Texture2D[]> frontBackNumbersCache;
  private static Dictionary<FontType, Texture2D[]> lettersCache;
  private static int jerseyTextureSize;
  public static Texture2D jerseyTexture;
  public static Color32[] jerseyTextureMap;
  private static Color32[] textureFillMap;
  private static Texture2D _numberTexture;
  private static float textureScale;
  private static Texture2D _resourceTexture;
  private static Texture2D _letterTexture;

  private void Awake()
  {
    UniformWriter.jerseyTextureSize = 1024;
    UniformWriter.textureScale = 1f;
    UniformWriter.frontBackNumbersCache = new Dictionary<FontType, Texture2D[]>();
    UniformWriter.lettersCache = new Dictionary<FontType, Texture2D[]>();
  }

  public static Texture2D CreateJersey(
    UniformSet u,
    UniformConfig config,
    int jerseyIndex,
    string name,
    int number)
  {
    UniformWriter._resourceTexture = u.GetJerseyTexture(jerseyIndex);
    UniformWriter.jerseyTexture = new Texture2D(UniformWriter._resourceTexture.width, UniformWriter._resourceTexture.height, TextureFormat.ARGB32, true);
    try
    {
      UniformWriter.jerseyTextureMap = UniformWriter._resourceTexture.GetPixels32();
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Caught exception at CreateJersey () of Uniform Writer. Error: " + ex?.ToString()));
      return UniformWriter._resourceTexture;
    }
    if (config.JerseyHasNumberOutline2())
      UniformWriter.WriteNumbers(number, config.GetNumberFontType(), config.GetNumberFillColor(), config.GetNumberOutlineColor1(), config.GetNumberOutlineColor2(), config.JerseyHasShoulderNumbers(), config.JerseyHasSleeveNumbers());
    else if (config.JerseyHasNumberOutline1())
      UniformWriter.WriteNumbers(number, config.GetNumberFontType(), config.GetNumberFillColor(), config.GetNumberOutlineColor1(), config.JerseyHasShoulderNumbers(), config.JerseyHasSleeveNumbers());
    else
      UniformWriter.WriteNumbers(number, config.GetNumberFontType(), config.GetNumberFillColor(), config.JerseyHasShoulderNumbers(), config.JerseyHasSleeveNumbers());
    if (config.JerseyHasLetterOutline())
      UniformWriter.WriteLetters(name, config.GetLetterFontType(), config.GetLetterFillColor(), config.GetLetterOutlineColor());
    else
      UniformWriter.WriteLetters(name, config.GetLetterFontType(), config.GetLetterFillColor());
    try
    {
      UniformWriter.jerseyTexture.Compress(true);
      UniformWriter.jerseyTexture.anisoLevel = 1;
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Caught excpetion in SetJersey() of UniformManager. Trying to Compress texture. Error: " + ex.ToString()));
    }
    return UniformWriter.jerseyTexture;
  }

  public static void WriteNumbers(
    int number,
    FontType font,
    Color fillColor,
    bool writeOnShoulder,
    bool writeOnSleeve)
  {
    UniformWriter.WriteBackNumbers(number, font, new Color[1]
    {
      fillColor
    });
    UniformWriter.WriteFrontNumbers(number, font, new Color[1]
    {
      fillColor
    });
  }

  public static void WriteNumbers(
    int number,
    FontType font,
    Color fillColor,
    Color outerOutline,
    bool writeOnShoulder,
    bool writeOnSleeve)
  {
    UniformWriter.WriteBackNumbers(number, font, new Color[2]
    {
      fillColor,
      outerOutline
    });
    UniformWriter.WriteFrontNumbers(number, font, new Color[2]
    {
      fillColor,
      outerOutline
    });
  }

  public static void WriteNumbers(
    int number,
    FontType font,
    Color fillColor,
    Color outerOutline,
    Color innerOutline,
    bool writeOnShoulder,
    bool writeOnSleeve)
  {
    UniformWriter.WriteBackNumbers(number, font, new Color[3]
    {
      fillColor,
      outerOutline,
      innerOutline
    });
    UniformWriter.WriteFrontNumbers(number, font, new Color[3]
    {
      fillColor,
      outerOutline,
      innerOutline
    });
  }

  private static void WriteBackNumbers(int number, FontType font, Color[] colors)
  {
    if (!UniformWriter.frontBackNumbersCache.ContainsKey(font))
      UniformWriter.frontBackNumbersCache[font] = UniformWriter.LoadTextures(font, "front_back_number_textures");
    Texture2D[] fillTexture = UniformWriter.frontBackNumbersCache[font];
    int textureWidth = Mathf.RoundToInt(100f * UniformWriter.textureScale);
    int startX = Mathf.RoundToInt(312f * UniformWriter.textureScale);
    int startY = Mathf.RoundToInt(165f * UniformWriter.textureScale);
    UniformWriter.SetupNumbers(number, false, false, false, colors, startX, startY, textureWidth, fillTexture);
  }

  private static void WriteFrontNumbers(int number, FontType font, Color[] colors)
  {
    if (!UniformWriter.frontBackNumbersCache.ContainsKey(font))
      UniformWriter.frontBackNumbersCache[font] = UniformWriter.LoadTextures(font, "front_back_number_textures");
    Texture2D[] fillTexture = UniformWriter.frontBackNumbersCache[font];
    int textureWidth = Mathf.RoundToInt(100f * UniformWriter.textureScale);
    int startX = Mathf.RoundToInt(142f * UniformWriter.textureScale);
    int startY = Mathf.RoundToInt(600f * UniformWriter.textureScale);
    UniformWriter.SetupNumbers(number, true, true, true, colors, startX, startY, textureWidth, fillTexture);
  }

  private static void SetupNumbers(
    int number,
    bool putSecondNumberFirst,
    bool flipTextureVertically,
    bool flipTextureHorizontally,
    Color[] colors,
    int startX,
    int startY,
    int textureWidth,
    Texture2D[] fillTexture)
  {
    int startY1 = Mathf.RoundToInt((float) (startY * UniformWriter.jerseyTextureSize) * UniformWriter.textureScale);
    int num1 = 1;
    int index1 = number / 10;
    int index2 = number % 10;
    if (putSecondNumberFirst)
    {
      int num2 = index1;
      index1 = index2;
      index2 = num2;
    }
    if (number > 9)
      num1 = 2;
    else if (putSecondNumberFirst)
      index2 = index1;
    else
      index1 = index2;
    int num3 = textureWidth;
    if (num1 > 1)
    {
      num3 += textureWidth;
      UniformWriter._numberTexture = fillTexture[index1];
    }
    else
      UniformWriter._numberTexture = fillTexture[index2];
    if (flipTextureVertically)
      UniformWriter._numberTexture = UniformWriter.FlipTextureVertically(UniformWriter._numberTexture);
    if (flipTextureHorizontally)
      UniformWriter._numberTexture = UniformWriter.FlipTextureHorizontally(UniformWriter._numberTexture);
    UniformWriter.textureFillMap = UniformWriter._numberTexture.GetPixels32();
    int startX1 = (startX + startX) / 2 - num3 / 2;
    UniformWriter.WriteNumber(UniformWriter.textureFillMap, colors, startX1, startY1, UniformWriter._numberTexture.width);
    if (num1 > 1)
    {
      int startX2 = startX1 + textureWidth;
      UniformWriter._numberTexture = fillTexture[index2];
      if (flipTextureVertically)
        UniformWriter._numberTexture = UniformWriter.FlipTextureVertically(UniformWriter._numberTexture);
      if (flipTextureHorizontally)
        UniformWriter._numberTexture = UniformWriter.FlipTextureHorizontally(UniformWriter._numberTexture);
      UniformWriter.textureFillMap = UniformWriter._numberTexture.GetPixels32();
      UniformWriter.WriteNumber(UniformWriter.textureFillMap, colors, startX2, startY1, UniformWriter._numberTexture.width);
    }
    UniformWriter.jerseyTexture.SetPixels32(UniformWriter.jerseyTextureMap);
    UniformWriter.jerseyTexture.Apply();
  }

  private static void WriteNumber(
    Color32[] fillMap,
    Color[] colors,
    int startX,
    int startY,
    int numTextureSize)
  {
    bool flag1 = colors.Length > 1;
    bool flag2 = colors.Length > 2;
    for (int index1 = 0; index1 < fillMap.Length; ++index1)
    {
      int index2 = startY + index1 / numTextureSize * Mathf.RoundToInt((float) UniformWriter.jerseyTextureSize * UniformWriter.textureScale) + (index1 % numTextureSize + startX);
      if (fillMap[index1].g > (byte) 0)
        UniformWriter.jerseyTextureMap[index2] = !flag1 ? (Color32) colors[0] : (Color32) colors[1];
      else if (fillMap[index1].b > (byte) 0)
        UniformWriter.jerseyTextureMap[index2] = !flag2 ? (Color32) colors[0] : (Color32) colors[2];
      else if (fillMap[index1].r > (byte) 0)
        UniformWriter.jerseyTextureMap[index2] = (Color32) colors[0];
    }
  }

  public static void WriteLetters(string name, FontType font, Color fillColor) => UniformWriter.WriteName(name, font, new Color[1]
  {
    fillColor
  });

  public static void WriteLetters(string name, FontType font, Color fillColor, Color outlineColor) => UniformWriter.WriteName(name, font, new Color[2]
  {
    fillColor,
    outlineColor
  });

  private static void WriteName(string name, FontType font, Color[] colors)
  {
    if (!UniformWriter.lettersCache.ContainsKey(font))
      UniformWriter.lettersCache[font] = UniformWriter.LoadTextures(font, "letter_textures");
    Texture2D[] fillTexture = UniformWriter.lettersCache[font];
    int[] textureWidths;
    int spacing;
    if (font == FontType.Block)
    {
      textureWidths = new int[29]
      {
        26,
        22,
        22,
        23,
        20,
        19,
        23,
        23,
        9,
        22,
        24,
        19,
        27,
        24,
        24,
        22,
        22,
        22,
        22,
        22,
        23,
        26,
        35,
        26,
        26,
        22,
        15,
        8,
        8
      };
      spacing = 3;
    }
    else
    {
      textureWidths = new int[29]
      {
        27,
        24,
        23,
        24,
        23,
        23,
        23,
        27,
        13,
        24,
        28,
        22,
        32,
        27,
        23,
        24,
        22,
        26,
        22,
        26,
        27,
        26,
        37,
        27,
        25,
        22,
        15,
        6,
        8
      };
      spacing = 3;
    }
    int minX = Mathf.RoundToInt(70f * UniformWriter.textureScale);
    int maxX = Mathf.RoundToInt(535f * UniformWriter.textureScale);
    int minY = Mathf.RoundToInt(365f * UniformWriter.textureScale);
    UniformWriter.SetupLetters(name, colors, minX, maxX, minY, spacing, textureWidths, fillTexture);
  }

  private static void SetupLetters(
    string name,
    Color[] colors,
    int minX,
    int maxX,
    int minY,
    int spacing,
    int[] textureWidths,
    Texture2D[] fillTexture)
  {
    int textureOffset = Mathf.RoundToInt((float) (minY * UniformWriter.jerseyTextureSize) * UniformWriter.textureScale);
    name = name.Trim();
    int length = name.Length;
    int num1 = 0;
    for (int index = 0; index < length; ++index)
      num1 += textureWidths[index];
    int num2 = num1 + (length - 1) * spacing;
    int startX = (minX + maxX) / 2 - num2 / 2;
    char[] charArray = name.ToUpper().ToCharArray();
    for (int index1 = 0; index1 < length; ++index1)
    {
      int index2 = (int) charArray[index1] - 65;
      if (index2 < 0)
      {
        switch (index2)
        {
          case -33:
            startX += 20;
            continue;
          case -26:
            index2 = 27;
            break;
          case -20:
            index2 = 26;
            break;
          case -19:
            index2 = 28;
            break;
          default:
            Debug.Log((object) ("Invalid Character: " + charArray[index1].ToString()));
            continue;
        }
      }
      UniformWriter._letterTexture = fillTexture[index2];
      UniformWriter.textureFillMap = UniformWriter._letterTexture.GetPixels32();
      UniformWriter.WriteLetter(UniformWriter.textureFillMap, colors, startX, textureOffset, UniformWriter._letterTexture.width);
      startX += textureWidths[index2] + spacing;
    }
    UniformWriter.jerseyTexture.SetPixels32(UniformWriter.jerseyTextureMap);
    UniformWriter.jerseyTexture.Apply();
  }

  private static void WriteLetter(
    Color32[] fillMap,
    Color[] colors,
    int startX,
    int textureOffset,
    int textureSize)
  {
    bool flag = colors.Length > 1;
    for (int index1 = 0; index1 < fillMap.Length; ++index1)
    {
      int index2 = textureOffset + index1 / textureSize * Mathf.RoundToInt((float) UniformWriter.jerseyTextureSize * UniformWriter.textureScale) + (index1 % textureSize + startX);
      if (fillMap[index1].g > (byte) 0)
        UniformWriter.jerseyTextureMap[index2] = !flag ? (Color32) colors[0] : (Color32) colors[1];
      else if (fillMap[index1].r > (byte) 0)
        UniformWriter.jerseyTextureMap[index2] = (Color32) colors[0];
    }
  }

  private static Texture2D FlipTextureVertically(Texture2D texture)
  {
    Texture2D texture2D = new Texture2D(texture.width, texture.height);
    int width = texture.width;
    int height = texture.height;
    for (int y = 0; y < width; ++y)
    {
      for (int x = 0; x < height; ++x)
        texture2D.SetPixel(x, width - y - 1, texture.GetPixel(x, y));
    }
    texture2D.Apply();
    return texture2D;
  }

  private static Texture2D FlipTextureHorizontally(Texture2D texture)
  {
    Texture2D texture2D = new Texture2D(texture.width, texture.height);
    int width = texture.width;
    int height = texture.height;
    for (int x = 0; x < width; ++x)
    {
      for (int y = 0; y < height; ++y)
        texture2D.SetPixel(width - x - 1, y, texture.GetPixel(x, y));
    }
    texture2D.Apply();
    return texture2D;
  }

  private static Texture2D[] LoadTextures(FontType font, string assetBundlePath) => AddressablesData.instance.LoadAssetsSync<Texture2D>(AddressablesData.CorrectingAssetKey(assetBundlePath + "/" + ((int) font).ToString()));

  public static void ClearTextureCache()
  {
    UniformWriter.frontBackNumbersCache.Clear();
    UniformWriter.lettersCache.Clear();
  }
}
