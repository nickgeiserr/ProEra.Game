// Decompiled with JetBrains decompiler
// Type: MB_MaterialAndUVRect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[Serializable]
public class MB_MaterialAndUVRect
{
  public Material material;
  public Rect atlasRect;
  public string srcObjName;
  public Rect samplingRectMatAndUVTiling;
  public Rect sourceMaterialTiling;
  public Rect samplingEncapsulatinRect;

  public MB_MaterialAndUVRect(
    Material m,
    Rect destRect,
    Rect samplingRectMatAndUVTiling,
    Rect sourceMaterialTiling,
    Rect samplingEncapsulatinRect,
    string objName)
  {
    this.material = m;
    this.atlasRect = destRect;
    this.samplingRectMatAndUVTiling = samplingRectMatAndUVTiling;
    this.sourceMaterialTiling = sourceMaterialTiling;
    this.samplingEncapsulatinRect = samplingEncapsulatinRect;
    this.srcObjName = objName;
  }

  public override int GetHashCode() => this.material.GetInstanceID() ^ this.samplingEncapsulatinRect.GetHashCode();

  public override bool Equals(object obj) => obj is MB_MaterialAndUVRect && (UnityEngine.Object) this.material == (UnityEngine.Object) ((MB_MaterialAndUVRect) obj).material && this.samplingEncapsulatinRect == ((MB_MaterialAndUVRect) obj).samplingEncapsulatinRect;
}
