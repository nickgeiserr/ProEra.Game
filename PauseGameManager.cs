// Decompiled with JetBrains decompiler
// Type: PauseGameManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using ProEra.Game;
using System;
using UDB;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
  private void Awake() => Globals.PauseGame.OnValueChanged += new Action<bool>(this.OnPauseGameChanged);

  private void OnDestroy() => Time.timeScale = (float) GameSettings.TimeScale;

  private void OnPauseGameChanged(bool pause)
  {
    if (pause)
    {
      if (SingletonBehaviour<MobileControllerManager, MonoBehaviour>.Exists())
        SingletonBehaviour<MobileControllerManager, MonoBehaviour>.instance.EnableTouches(false);
      Time.timeScale = 0.0f;
    }
    else
    {
      Time.timeScale = (float) GameSettings.TimeScale;
      if (!SingletonBehaviour<MobileControllerManager, MonoBehaviour>.Exists())
        return;
      SingletonBehaviour<MobileControllerManager, MonoBehaviour>.instance.EnableTouches(true);
    }
  }
}
