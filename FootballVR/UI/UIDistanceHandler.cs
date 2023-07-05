// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.UIDistanceHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR.UI
{
  public class UIDistanceHandler : MonoBehaviour
  {
    [SerializeField]
    private Transform _parentTx;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake() => this._linksHandler.SetLinks(new List<EventHandle>()
    {
      ScriptableSingleton<VRSettings>.Instance.UIDistance.Link<float>((Action<float>) (x => this.UpdatePosition())),
      VRState.CenterUI.Link<bool>((Action<bool>) (x => this.UpdatePosition())),
      VREvents.UpdateUI.Link(new System.Action(this.UpdatePosition))
    });

    private void OnDestroy() => this._linksHandler.Clear();

    private void UpdatePosition()
    {
      this.transform.position = this._parentTx.position + ((bool) VRState.CenterUI ? this.GetForw() : this._parentTx.forward) * ((float) ScriptableSingleton<VRSettings>.Instance.UIDistance * ((bool) (VariableBool) VRState.BigSizeMode ? VRState.BigSizeScale : 1f));
      if ((bool) VRState.CenterUI)
        this.transform.LookAt(this.transform.position * 2f - PersistentSingleton<PlayerCamera>.Instance.Position.SetY(this.transform.position.y));
      else
        this.transform.localRotation = Quaternion.identity;
    }

    private Vector3 GetForw() => PersistentSingleton<PlayerCamera>.Instance.transform.forward.SetY(0.0f).normalized;
  }
}
