// Decompiled with JetBrains decompiler
// Type: FootballVR.SplashManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  public class SplashManager : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] _splashScreens;
    public static bool Finished;
    private int _currentSplash;

    private IEnumerator Start()
    {
      for (; this._currentSplash < this._splashScreens.Length; ++this._currentSplash)
      {
        this._splashScreens[this._currentSplash].SetActive(true);
        yield return (object) new WaitUntil((Func<bool>) (() => SplashSequence.Finished));
        this._splashScreens[this._currentSplash].SetActive(false);
        SplashSequence.Finished = false;
      }
      SplashManager.Finished = true;
    }

    private void Update()
    {
    }
  }
}
