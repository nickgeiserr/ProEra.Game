// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.CatchChallenge
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace TB12.GameplayData
{
  [Serializable]
  public class CatchChallenge
  {
    public string id;
    public float distance;
    public int ballCount = 13;
    public int ballsToWin = 7;
    public string emitterType = "Machine";
    public int emittersCount = 3;
    public float emittersAngle = 40f;
    public float throwInterval = 2.5f;
    public bool fromBehind;
    public float ballTravelTime = 2f;
    public float ballTravelTime2 = 1.2f;
    public float radiusIncrease = 1.2f;
    public float throwDelay = 0.75f;
  }
}
