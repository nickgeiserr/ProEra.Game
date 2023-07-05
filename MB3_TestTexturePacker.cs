// Decompiled with JetBrains decompiler
// Type: MB3_TestTexturePacker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using UnityEngine;

public class MB3_TestTexturePacker : MonoBehaviour
{
  private MB2_TexturePacker texturePacker;
  public int numTex = 32;
  public int min = 126;
  public int max = 2046;
  public float xMult = 1f;
  public float yMult = 1f;
  public bool imgsMustBePowerOfTwo;
  public List<Vector2> imgsToAdd = new List<Vector2>();
  public int padding = 1;
  public int maxDim = 4096;
  public bool doPowerOfTwoTextures = true;
  public bool doMultiAtlas;
  public MB2_LogLevel logLevel;
  public string res;
  public AtlasPackingResult[] rs;

  [ContextMenu("Generate List Of Images To Add")]
  public void GenerateListOfImagesToAdd()
  {
    this.imgsToAdd = new List<Vector2>();
    for (int index = 0; index < this.numTex; ++index)
    {
      Vector2 vector2 = new Vector2((float) Mathf.RoundToInt((float) Random.Range(this.min, this.max) * this.xMult), (float) Mathf.RoundToInt((float) Random.Range(this.min, this.max) * this.yMult));
      if (this.imgsMustBePowerOfTwo)
      {
        vector2.x = (float) MB2_TexturePacker.RoundToNearestPositivePowerOfTwo((int) vector2.x);
        vector2.y = (float) MB2_TexturePacker.RoundToNearestPositivePowerOfTwo((int) vector2.y);
      }
      this.imgsToAdd.Add(vector2);
    }
  }

  [ContextMenu("Run")]
  public void RunTestHarness()
  {
    this.texturePacker = new MB2_TexturePacker();
    this.texturePacker.doPowerOfTwoTextures = this.doPowerOfTwoTextures;
    this.texturePacker.LOG_LEVEL = this.logLevel;
    this.rs = this.texturePacker.GetRects(this.imgsToAdd, this.maxDim, this.padding, this.doMultiAtlas);
    if (this.rs != null)
    {
      Debug.Log((object) ("NumAtlas= " + this.rs.Length.ToString()));
      for (int index1 = 0; index1 < this.rs.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.rs[index1].rects.Length; ++index2)
        {
          Rect rect = this.rs[index1].rects[index2];
          rect.x *= (float) this.rs[index1].atlasX;
          rect.y *= (float) this.rs[index1].atlasY;
          rect.width *= (float) this.rs[index1].atlasX;
          rect.height *= (float) this.rs[index1].atlasY;
          Debug.Log((object) rect.ToString("f5"));
        }
        Debug.Log((object) "===============");
      }
      this.res = "mxX= " + this.rs[0].atlasX.ToString() + " mxY= " + this.rs[0].atlasY.ToString();
    }
    else
      this.res = "ERROR: PACKING FAILED";
  }

  private void OnDrawGizmos()
  {
    if (this.rs == null)
      return;
    for (int index1 = 0; index1 < this.rs.Length; ++index1)
    {
      Vector2 vector2 = new Vector2((float) index1 * 1.5f * (float) this.maxDim, 0.0f);
      AtlasPackingResult r = this.rs[index1];
      Gizmos.DrawWireCube((Vector3) new Vector2(vector2.x + (float) (r.atlasX / 2), vector2.y + (float) (r.atlasY / 2)), (Vector3) new Vector2((float) r.atlasX, (float) r.atlasY));
      for (int index2 = 0; index2 < this.rs[index1].rects.Length; ++index2)
      {
        Rect rect = this.rs[index1].rects[index2];
        Gizmos.color = new Color(Random.value, Random.value, Random.value);
        Gizmos.DrawCube((Vector3) new Vector2(vector2.x + (rect.x + rect.width / 2f) * (float) this.rs[index1].atlasX, vector2.y + (rect.y + rect.height / 2f) * (float) this.rs[index1].atlasY), (Vector3) new Vector2(rect.width * (float) this.rs[index1].atlasX, rect.height * (float) this.rs[index1].atlasY));
      }
    }
  }

  [ContextMenu("Test1")]
  private void Test1()
  {
    this.texturePacker = new MB2_TexturePacker();
    this.texturePacker.doPowerOfTwoTextures = true;
    List<Vector2> imgWidthHeights = new List<Vector2>();
    imgWidthHeights.Add(new Vector2(450f, 200f));
    imgWidthHeights.Add(new Vector2(450f, 200f));
    imgWidthHeights.Add(new Vector2(450f, 80f));
    this.texturePacker.LOG_LEVEL = this.logLevel;
    this.rs = this.texturePacker.GetRects(imgWidthHeights, 512, 8, true);
    Debug.Log((object) "Success! ");
  }

  [ContextMenu("Test2")]
  private void Test2()
  {
    this.texturePacker = new MB2_TexturePacker();
    this.texturePacker.doPowerOfTwoTextures = true;
    List<Vector2> imgWidthHeights = new List<Vector2>();
    imgWidthHeights.Add(new Vector2(200f, 450f));
    imgWidthHeights.Add(new Vector2(200f, 450f));
    imgWidthHeights.Add(new Vector2(80f, 450f));
    this.texturePacker.LOG_LEVEL = this.logLevel;
    this.rs = this.texturePacker.GetRects(imgWidthHeights, 512, 8, true);
    Debug.Log((object) "Success! ");
  }

  [ContextMenu("Test3")]
  private void Test3()
  {
    this.texturePacker = new MB2_TexturePacker();
    this.texturePacker.doPowerOfTwoTextures = false;
    List<Vector2> imgWidthHeights = new List<Vector2>();
    imgWidthHeights.Add(new Vector2(450f, 200f));
    imgWidthHeights.Add(new Vector2(450f, 200f));
    imgWidthHeights.Add(new Vector2(450f, 80f));
    this.texturePacker.LOG_LEVEL = this.logLevel;
    this.rs = this.texturePacker.GetRects(imgWidthHeights, 512, 8, true);
    Debug.Log((object) "Success! ");
  }

  [ContextMenu("Test4")]
  private void Test4()
  {
    this.texturePacker = new MB2_TexturePacker();
    this.texturePacker.doPowerOfTwoTextures = false;
    List<Vector2> imgWidthHeights = new List<Vector2>();
    imgWidthHeights.Add(new Vector2(200f, 450f));
    imgWidthHeights.Add(new Vector2(200f, 450f));
    imgWidthHeights.Add(new Vector2(80f, 450f));
    this.texturePacker.LOG_LEVEL = this.logLevel;
    this.rs = this.texturePacker.GetRects(imgWidthHeights, 512, 8, true);
    Debug.Log((object) "Success! ");
  }
}
