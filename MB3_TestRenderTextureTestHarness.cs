// Decompiled with JetBrains decompiler
// Type: MB3_TestRenderTextureTestHarness
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MB3_TestRenderTextureTestHarness : MonoBehaviour
{
  public Texture2D input;
  public bool doColor;
  public Color32 color;

  public Texture2D Create3x3Tex()
  {
    Texture2D texture2D = new Texture2D(3, 3, TextureFormat.ARGB32, false);
    Color32[] colors = new Color32[texture2D.width * texture2D.height];
    for (int index = 0; index < colors.Length; ++index)
      colors[index] = this.color;
    texture2D.SetPixels32(colors);
    texture2D.Apply();
    return texture2D;
  }

  public Texture2D Create3x3Clone()
  {
    Texture2D texture2D = new Texture2D(3, 3, TextureFormat.ARGB32, false);
    Color32[] colors = new Color32[9]
    {
      new Color32((byte) 54, (byte) 54, (byte) 201, byte.MaxValue),
      new Color32((byte) 128, (byte) 37, (byte) 218, byte.MaxValue),
      new Color32((byte) 201, (byte) 54, (byte) 201, byte.MaxValue),
      new Color32((byte) 37, (byte) 128, (byte) 218, byte.MaxValue),
      new Color32((byte) 128, (byte) 128, byte.MaxValue, byte.MaxValue),
      new Color32((byte) 218, (byte) 128, (byte) 218, byte.MaxValue),
      new Color32((byte) 54, (byte) 201, (byte) 201, byte.MaxValue),
      new Color32((byte) 128, (byte) 218, (byte) 218, byte.MaxValue),
      new Color32((byte) 201, (byte) 201, (byte) 201, byte.MaxValue)
    };
    texture2D.SetPixels32(colors);
    texture2D.Apply();
    return texture2D;
  }

  public static void TestRender(Texture2D input, Texture2D output)
  {
    int num1 = 1;
    ShaderTextureProperty[] shaderTexturePropertyArray = new ShaderTextureProperty[1]
    {
      new ShaderTextureProperty("_BumpMap", false)
    };
    int width = input.width;
    int height = input.height;
    int num2 = 0;
    Rect[] rectArray = new Rect[1]
    {
      new Rect(0.0f, 0.0f, 1f, 1f)
    };
    List<MB3_TextureCombiner.MB_TexSet> mbTexSetList = new List<MB3_TextureCombiner.MB_TexSet>();
    mbTexSetList.Add(new MB3_TextureCombiner.MB_TexSet(new MB3_TextureCombiner.MeshBakerMaterialTexture[1]
    {
      new MB3_TextureCombiner.MeshBakerMaterialTexture(input)
    }, Vector2.zero, Vector2.one));
    GameObject gameObject = new GameObject("MBrenderAtlasesGO");
    MB3_AtlasPackerRenderTexture packerRenderTexture = gameObject.AddComponent<MB3_AtlasPackerRenderTexture>();
    gameObject.AddComponent<Camera>();
    for (int index = 0; index < num1; ++index)
    {
      Debug.Log((object) ("About to render " + shaderTexturePropertyArray[index].name + " isNormal=" + shaderTexturePropertyArray[index].isNormalMap.ToString()));
      packerRenderTexture.LOG_LEVEL = MB2_LogLevel.trace;
      packerRenderTexture.width = width;
      packerRenderTexture.height = height;
      packerRenderTexture.padding = num2;
      packerRenderTexture.rects = rectArray;
      packerRenderTexture.textureSets = mbTexSetList;
      packerRenderTexture.indexOfTexSetToRender = index;
      packerRenderTexture.isNormalMap = shaderTexturePropertyArray[index].isNormalMap;
      Texture2D tex = packerRenderTexture.OnRenderAtlas((MB3_TextureCombiner) null);
      Debug.Log((object) ("Created atlas " + shaderTexturePropertyArray[index].name + " w=" + tex.width.ToString() + " h=" + tex.height.ToString() + " id=" + tex.GetInstanceID().ToString()));
      Color color = tex.GetPixel(5, 5);
      string str1 = color.ToString();
      color = Color.red;
      string str2 = color.ToString();
      Debug.Log((object) ("Color " + str1 + " " + str2));
      File.WriteAllBytes(Application.dataPath + "/_Experiment/red.png", tex.EncodeToPNG());
    }
  }
}
