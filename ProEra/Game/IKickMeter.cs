﻿// Decompiled with JetBrains decompiler
// Type: ProEra.Game.IKickMeter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace ProEra.Game
{
  public interface IKickMeter
  {
    void ShowWindow();

    void HideWindow();

    void SetAimSpeed(int s);

    bool IsVisible();

    void SetWind();

    float GetKickHeightAngle();

    float GetKickDirection();

    float GetKickPower();
  }
}
