// Decompiled with JetBrains decompiler
// Type: CustomLogoData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class CustomLogoData
{
  [Key(0)]
  public string SourceLogoName { get; private set; }

  [Key(1)]
  public string SourceAssetBundle { get; private set; }

  [Key(2)]
  public float[] RedReplacement { get; private set; }

  [Key(3)]
  public float[] GreenReplacement { get; private set; }

  [Key(4)]
  public float[] BlueReplacement { get; private set; }

  [Key(5)]
  public float[] WhiteReplacement { get; private set; }

  public CustomLogoData()
  {
  }

  public CustomLogoData(
    string sourceLogoName,
    string sourceAssetBundle,
    Color red,
    Color green,
    Color blue,
    Color white)
  {
    this.SourceLogoName = sourceLogoName;
    this.SourceAssetBundle = sourceAssetBundle;
    this.RedReplacement = new float[3];
    this.RedReplacement[0] = red.r;
    this.RedReplacement[1] = red.g;
    this.RedReplacement[2] = red.b;
    this.GreenReplacement = new float[3];
    this.GreenReplacement[0] = green.r;
    this.GreenReplacement[1] = green.g;
    this.GreenReplacement[2] = green.b;
    this.BlueReplacement = new float[3];
    this.BlueReplacement[0] = blue.r;
    this.BlueReplacement[1] = blue.g;
    this.BlueReplacement[2] = blue.b;
    this.WhiteReplacement = new float[3];
    this.WhiteReplacement[0] = white.r;
    this.WhiteReplacement[1] = white.g;
    this.WhiteReplacement[2] = white.b;
  }

  private Texture2D LoadSourceLogoTexture() => AddressablesData.instance.LoadAssetSync<Sprite>(AddressablesData.CorrectingAssetKey(this.SourceAssetBundle), this.SourceLogoName).texture;

  public Sprite GetSpriteFromTexture2D()
  {
    Texture2D texture2D = this.LoadSourceLogoTexture();
    Color32[] pixels32 = texture2D.GetPixels32();
    Texture2D targetTexture = new Texture2D(texture2D.width, texture2D.height, TextureFormat.ARGB32, false);
    Color32[] color32Array = new Color32[pixels32.Length];
    Color color1 = new Color(this.RedReplacement[0], this.RedReplacement[1], this.RedReplacement[2]);
    Color color2 = new Color(this.GreenReplacement[0], this.GreenReplacement[1], this.GreenReplacement[2]);
    Color color3 = new Color(this.BlueReplacement[0], this.BlueReplacement[1], this.BlueReplacement[2]);
    Color color4 = new Color(this.WhiteReplacement[0], this.WhiteReplacement[1], this.WhiteReplacement[2]);
    Color32[] sourceLogoMap = pixels32;
    Color32[] targetLogoMap = color32Array;
    Color redReplacement = color1;
    Color greenReplacement = color2;
    Color blueReplacement = color3;
    Color whiteReplacement = color4;
    return TextureUtility.ColorTextureIntoSprite(targetTexture, sourceLogoMap, targetLogoMap, redReplacement, greenReplacement, blueReplacement, whiteReplacement);
  }
}
