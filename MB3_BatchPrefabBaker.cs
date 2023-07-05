// Decompiled with JetBrains decompiler
// Type: MB3_BatchPrefabBaker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class MB3_BatchPrefabBaker : MonoBehaviour
{
  public MB3_BatchPrefabBaker.MB3_PrefabBakerRow[] prefabRows;
  public string outputPrefabFolder;

  [Serializable]
  public class MB3_PrefabBakerRow
  {
    public GameObject sourcePrefab;
    public GameObject resultPrefab;
  }
}
