// Decompiled with JetBrains decompiler
// Type: FootballVR.DebugUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class DebugUI : Singleton<DebugUI>
  {
    public TextMeshProUGUI rightTextDown;
    public TextMeshProUGUI rightTextUp;
    public TextMeshProUGUI leftTextDown;
    public TextMeshProUGUI leftTextUp;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private ThrowSettings _throwSettings => ScriptableSingleton<ThrowSettings>.Instance;

    private void Awake()
    {
      this.RegisterSingleton(this);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<LocomotionSettings>.Instance.ShowDebug, new Action<bool>(this.HandleDebugStateChanged)),
        EventHandle.Link<bool>((Variable<bool>) this._throwSettings.InteractionSettings.ShowDebug, new Action<bool>(this.HandleDebugStateChanged))
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void HandleDebugStateChanged(bool unused)
    {
      bool flag = (bool) ScriptableSingleton<LocomotionSettings>.Instance.ShowDebug || (bool) this._throwSettings.InteractionSettings.ShowDebug;
      TextMeshProUGUI[] textMeshProUguiArray = new TextMeshProUGUI[4]
      {
        this.rightTextDown,
        this.rightTextUp,
        this.leftTextDown,
        this.leftTextUp
      };
      foreach (Component component in textMeshProUguiArray)
        component.gameObject.SetActive(flag);
    }
  }
}
