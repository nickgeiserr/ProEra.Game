// Decompiled with JetBrains decompiler
// Type: TextureEditorUtility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextureEditorUtility
{
  public static readonly Color[] CustomLogoColors = new Color[5]
  {
    Color.red,
    Color.blue,
    Color.green,
    Color.white,
    new Color(0.6f, 0.6f, 0.6f)
  };

  public static void DrawOverTexture(
    Color32[] target,
    Color32[] maskMap,
    RectInt maskRect,
    int targetWidth,
    params ColorChangeCondition[] conditions)
  {
    for (int i = 0; i < maskMap.Length; i++)
    {
      int index = maskRect.y + i / maskRect.width * targetWidth + (i % maskRect.width + maskRect.x);
      ColorChangeCondition colorChangeCondition = ((IEnumerable<ColorChangeCondition>) conditions).FirstOrDefault<ColorChangeCondition>((Func<ColorChangeCondition, bool>) (c => c.Match(maskMap[i])));
      if (colorChangeCondition != null)
        target[index] = colorChangeCondition.Color;
    }
  }

  public static Texture2D DrawTexture(
    Texture2D sourceTexture,
    params ColorChangeCondition[] conditions)
  {
    Texture2D targetTexture = new Texture2D(sourceTexture.width, sourceTexture.height, TextureFormat.ARGB32, true);
    TextureEditorUtility.RedrawTexture(targetTexture, sourceTexture, conditions);
    return targetTexture;
  }

  public static void RedrawTexture(
    Texture2D targetTexture,
    Texture2D sourceTexture,
    params ColorChangeCondition[] conditions)
  {
    Color32[] array = Enumerable.Repeat<Color32>(new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 0), targetTexture.width * targetTexture.height).ToArray<Color32>();
    TextureEditorUtility.DrawOverTexture(array, sourceTexture.GetPixels32(), new RectInt(0, 0, sourceTexture.width, sourceTexture.height), sourceTexture.width, conditions);
    targetTexture.SetPixels32(array);
    targetTexture.Apply();
  }

  public static Sprite SpriteFromTexture2D(Texture2D tx, float scale = 1f) => Sprite.Create(tx, new Rect(0.0f, 0.0f, (float) tx.width, (float) tx.height), new Vector2(0.5f, 0.5f), 100f / scale);

  public static ColorChangeCondition[] GetLogoColorChangeConditions(params Color[] colors) => Enumerable.Range(0, colors.Length).Select<int, ColorChangeCondition>((Func<int, ColorChangeCondition>) (i => TextureEditorUtility.GetLogoColorChangeCondition(i, colors[i]))).Reverse<ColorChangeCondition>().ToArray<ColorChangeCondition>();

  private static ColorChangeCondition GetLogoColorChangeCondition(int index, Color c)
  {
    switch (index)
    {
      case 0:
        return new ColorChangeCondition((Predicate<Color32>) (color => (double) color.r > 0.0), (Color32) c);
      case 1:
        return new ColorChangeCondition((Predicate<Color32>) (color => (double) color.b > 0.0), (Color32) c);
      case 2:
        return new ColorChangeCondition((Predicate<Color32>) (color => (double) color.g > 0.0), (Color32) c);
      case 3:
        return new ColorChangeCondition((Predicate<Color32>) (color => (double) color.g == 1.0 && (double) color.b == 1.0 && (double) color.r == 1.0), (Color32) c);
      case 4:
        return new ColorChangeCondition((Predicate<Color32>) (color => (double) color.r > 0.0 && (double) color.r < 1.0 && (double) color.g > 0.0 && (double) color.g < 1.0 && (double) color.b > 0.0 && (double) color.b < 1.0), (Color32) c);
      default:
        Debug.LogError((object) string.Format("Unexpected index {0} color change condition.", (object) index));
        return (ColorChangeCondition) null;
    }
  }
}
