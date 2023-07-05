// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchUI2DPassthroughToggle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  [RequireComponent(typeof (Toggle), typeof (IButton))]
  public class TouchUI2DPassthroughToggle : MonoBehaviour
  {
    private IButton thisButton;
    private Toggle thisToggle;

    private void Start()
    {
      this.thisButton = this.GetComponent<IButton>();
      this.thisToggle = this.GetComponent<Toggle>();
      this.thisButton.onClick += (Action) (() => this.thisToggle.isOn = true);
    }
  }
}
