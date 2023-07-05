// Decompiled with JetBrains decompiler
// Type: UDB.MusicTrack
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  [Serializable]
  public class MusicTrack
  {
    public string name;
    public AudioClip audioClip;
    public float loopDelay;
    public bool loop = true;
    [NonSerialized]
    public float defaultVolume;
  }
}
