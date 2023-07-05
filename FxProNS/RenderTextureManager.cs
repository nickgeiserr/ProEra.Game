// Decompiled with JetBrains decompiler
// Type: FxProNS.RenderTextureManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FxProNS
{
  public class RenderTextureManager : IDisposable
  {
    private static RenderTextureManager instance;
    private List<RenderTexture> allRenderTextures;
    private List<RenderTexture> availableRenderTextures;

    public static RenderTextureManager Instance => RenderTextureManager.instance ?? (RenderTextureManager.instance = new RenderTextureManager());

    public RenderTexture RequestRenderTexture(
      int _width,
      int _height,
      int _depth,
      RenderTextureFormat _format)
    {
      if (this.allRenderTextures == null)
        this.allRenderTextures = new List<RenderTexture>();
      if (this.availableRenderTextures == null)
        this.availableRenderTextures = new List<RenderTexture>();
      RenderTexture _tex = (RenderTexture) null;
      foreach (RenderTexture availableRenderTexture in this.availableRenderTextures)
      {
        if (!((UnityEngine.Object) null == (UnityEngine.Object) availableRenderTexture) && availableRenderTexture.width == _width && availableRenderTexture.height == _height && availableRenderTexture.depth == _depth && availableRenderTexture.format == _format)
          _tex = availableRenderTexture;
      }
      if ((UnityEngine.Object) null != (UnityEngine.Object) _tex)
      {
        this.MakeRenderTextureNonAvailable(_tex);
        _tex.DiscardContents();
        return _tex;
      }
      RenderTexture newTexture = this.CreateNewTexture(_width, _height, _depth, _format);
      this.MakeRenderTextureNonAvailable(newTexture);
      return newTexture;
    }

    public RenderTexture ReleaseRenderTexture(RenderTexture _tex)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) _tex || this.availableRenderTextures == null)
        return (RenderTexture) null;
      if (this.availableRenderTextures.Contains(_tex))
        return (RenderTexture) null;
      this.availableRenderTextures.Add(_tex);
      return (RenderTexture) null;
    }

    public void SafeAssign(ref RenderTexture a, RenderTexture b)
    {
      if ((UnityEngine.Object) a == (UnityEngine.Object) b)
        return;
      this.ReleaseRenderTexture(a);
      a = b;
    }

    public void MakeRenderTextureNonAvailable(RenderTexture _tex)
    {
      if (!this.availableRenderTextures.Contains(_tex))
        return;
      this.availableRenderTextures.Remove(_tex);
    }

    private RenderTexture CreateNewTexture(
      int _width,
      int _height,
      int _depth,
      RenderTextureFormat _format)
    {
      RenderTexture newTexture = new RenderTexture(_width, _height, _depth, _format);
      newTexture.Create();
      this.allRenderTextures.Add(newTexture);
      this.availableRenderTextures.Add(newTexture);
      return newTexture;
    }

    public void PrintRenderTextureStats()
    {
      string message1 = "<color=blue>availableRenderTextures: </color>" + this.availableRenderTextures.Count.ToString() + "\n";
      foreach (RenderTexture availableRenderTexture in this.availableRenderTextures)
        message1 = message1 + "\t" + this.RenderTexToString(availableRenderTexture) + "\n";
      Debug.Log((object) message1);
      string message2 = "<color=green>allRenderTextures:</color>" + this.allRenderTextures.Count.ToString() + "\n";
      foreach (RenderTexture allRenderTexture in this.allRenderTextures)
        message2 = message2 + "\t" + this.RenderTexToString(allRenderTexture) + "\n";
      Debug.Log((object) message2);
    }

    private string RenderTexToString(RenderTexture _rt)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) _rt)
        return "null";
      string[] strArray = new string[7];
      int num = _rt.width;
      strArray[0] = num.ToString();
      strArray[1] = " x ";
      num = _rt.height;
      strArray[2] = num.ToString();
      strArray[3] = "\t";
      num = _rt.depth;
      strArray[4] = num.ToString();
      strArray[5] = "\t";
      strArray[6] = _rt.format.ToString();
      return string.Concat(strArray);
    }

    private void PrintRenderTexturesCount(string _prefix = "") => Debug.Log((object) (_prefix + ": " + (this.allRenderTextures.Count - this.availableRenderTextures.Count).ToString() + "/" + this.allRenderTextures.Count.ToString()));

    public void ReleaseAllRenderTextures()
    {
      if (this.allRenderTextures == null)
        return;
      foreach (RenderTexture allRenderTexture in this.allRenderTextures)
      {
        if (!this.availableRenderTextures.Contains(allRenderTexture))
          this.ReleaseRenderTexture(allRenderTexture);
      }
    }

    public void PrintBalance()
    {
      int num = this.allRenderTextures.Count - this.availableRenderTextures.Count;
      string str1 = num.ToString();
      num = this.allRenderTextures.Count;
      string str2 = num.ToString();
      Debug.Log((object) ("RenderTextures balance: " + str1 + "/" + str2));
    }

    public void Dispose()
    {
      if (this.allRenderTextures != null)
      {
        foreach (RenderTexture allRenderTexture in this.allRenderTextures)
          allRenderTexture.Release();
        this.allRenderTextures.Clear();
      }
      if (this.availableRenderTextures == null)
        return;
      this.availableRenderTextures.Clear();
    }
  }
}
