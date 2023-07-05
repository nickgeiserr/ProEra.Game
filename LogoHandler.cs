// Decompiled with JetBrains decompiler
// Type: LogoHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class LogoHandler
{
  private int logoWidth;
  private int logoHeight;
  private float logoScale;
  private int rotateLogoDegrees;
  public int logoCenterX;
  public int logoCenterY;
  private Texture2D modifiedLogo;

  public LogoHandler(
    Texture2D originalLogo,
    int width,
    int height,
    float scale,
    int rotation,
    int centerX,
    int centerY)
  {
    this.modifiedLogo = originalLogo;
    this.logoWidth = width;
    this.logoHeight = height;
    this.logoScale = scale;
    this.rotateLogoDegrees = rotation;
    this.logoCenterX = centerX;
    this.logoCenterY = centerY;
    this.modifiedLogo = this.resizeLogo(this.modifiedLogo, this.logoWidth, this.logoHeight, this.logoScale);
    this.modifiedLogo = this.rotateLogo(this.modifiedLogo, this.rotateLogoDegrees);
  }

  public Texture2D GetTexture() => this.modifiedLogo;

  private Texture2D rotateLogo(Texture2D logo, int rotation)
  {
    if (rotation != 0 && rotation != 90 && rotation != 180 && rotation != 270)
      return (Texture2D) null;
    Color[] pixels = logo.GetPixels();
    Color[] colors = new Color[1];
    int width = logo.width;
    int height = logo.height;
    switch (rotation)
    {
      case 0:
        return logo;
      case 90:
        colors = new Color[width * height];
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < width; ++index2)
            colors[height - 1 - index1 + index2 * height] = pixels[index2 + index1 * width];
        }
        break;
      case 180:
        colors = pixels;
        Array.Reverse<Color>(colors);
        break;
      case 270:
        colors = new Color[logo.width * logo.height];
        for (int index3 = 0; index3 < height; ++index3)
        {
          for (int index4 = 0; index4 < width; ++index4)
            colors[height - 1 - index3 + index4 * height] = pixels[index4 + index3 * width];
        }
        Array.Reverse<Color>(colors);
        break;
    }
    Texture2D texture2D = new Texture2D(height, width);
    texture2D.SetPixels(colors);
    texture2D.Apply();
    return texture2D;
  }

  private Texture2D resizeLogo(
    Texture2D logoToResize,
    int logoWidth,
    int logoHeight,
    float logoScale)
  {
    if (logoWidth > 0 && logoHeight > 0)
    {
      logoToResize = this.scaleTexture(logoToResize, logoWidth, logoHeight);
    }
    else
    {
      if ((double) logoScale == 1.0)
        return logoToResize;
      logoToResize = this.scaleTexture(logoToResize, logoScale);
    }
    return logoToResize;
  }

  private Texture2D scaleTexture(Texture2D tex, int width, int height)
  {
    TextureScale.Bilinear(tex, width, height);
    return tex;
  }

  private Texture2D scaleTexture(Texture2D tex, float percentage)
  {
    TextureScale.Bilinear(tex, (int) ((double) tex.width * (double) percentage), (int) ((double) tex.height * (double) percentage));
    return tex;
  }
}
