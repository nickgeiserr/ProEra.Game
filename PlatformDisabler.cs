// Decompiled with JetBrains decompiler
// Type: PlatformDisabler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PlatformDisabler : MonoBehaviour
{
  [Header("Standalone Platforms")]
  [SerializeField]
  private bool disableOnWindows;
  [SerializeField]
  private bool disableOnLinux;
  [SerializeField]
  private bool disableOnMac;
  [Header("Mobile Platforms")]
  [SerializeField]
  private bool disableOnIOS;
  [SerializeField]
  private bool disableOnAndroid;
  [Header("Console Platforms")]
  [SerializeField]
  private bool disableOnXboxOne;
  [SerializeField]
  private bool disableOnPS4;
  [SerializeField]
  private bool disableOnTVOS;

  private void Awake()
  {
    if (!this.disableOnWindows)
      return;
    this.gameObject.SetActive(false);
  }
}
