// Decompiled with JetBrains decompiler
// Type: FootballVR.AccelerationSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR
{
  [Serializable]
  public class AccelerationSettings
  {
    public float acceleration = 0.06f;
    [SerializeField]
    private float _decelerationRate = 1.7f;
    [SerializeField]
    private float _decelerationConstant = 660f;
    [SerializeField]
    private float _decelerationOnTurning = 0.15f;

    public float decelerationOnTurning => this._decelerationOnTurning / 10f;

    public float decelerationConstant => this._decelerationConstant / 20f;

    public float decelerationRate => this._decelerationRate / 10f;
  }
}
