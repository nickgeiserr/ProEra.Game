// Decompiled with JetBrains decompiler
// Type: FootballVR.DifficultySetting
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class DifficultySetting
  {
    public float GameSpeed = 0.8f;
    public int AutoAimPasses = 10;
    public bool UnderCenterAutoDropBackPass = true;
    public bool UnderCenterAutoDropBackRun = true;
    public bool UnderCenterBulletTimePass = true;
    public bool UnderCenterBulletTimeRun = true;
    public bool ShotgunBulletTimePass = true;
    public bool ShotgunBulletTimeRun = true;
    public bool OpponentCanIntercept;
    public bool OpponentCanBlitz;
    public bool DelayOfGame;
  }
}
