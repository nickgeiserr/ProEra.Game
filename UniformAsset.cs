// Decompiled with JetBrains decompiler
// Type: UniformAsset
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UniformAsset
{
  public int numberOfHelmets;
  public int numberOfJerseys;
  public int numberOfPants;
  public string[] namesOfUniforms;
  public string[] helmetNames;
  public string[] jerseyNames;
  public string[] pantNames;
  public Texture2D[] pants;
  public Texture2D[] jerseys;
  public Texture2D[] helmets;
  [SerializeField]
  public Dictionary<string, string> textAssets;
  public Dictionary<string, string> textAssetsToString;
  public List<string> testAssetList;
  public List<UniformMap> textAssetsMap;
}
