// Decompiled with JetBrains decompiler
// Type: FootballVR.LoadingScreenController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class LoadingScreenController : Singleton<LoadingScreenController>
  {
    [SerializeField]
    private LoadingEnvironment _loadingEnvironment;
    [SerializeField]
    private LayerMask _loadingLayer;
    private Camera _camera;
    private int _defaultLayer;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    public static readonly VariableBool Loading = new VariableBool();

    private void Awake()
    {
      this._camera = PlayerCamera.Camera;
      if ((UnityEngine.Object) this._camera != (UnityEngine.Object) null)
        this._defaultLayer = this._camera.cullingMask;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        LoadingScreenController.Loading.Link<bool>(new Action<bool>(this.HandleLoadingState))
      });
    }

    private void HandleLoadingState(bool loading)
    {
      this._loadingEnvironment.SetVisibility(loading);
      this._camera.cullingMask = loading ? this._loadingLayer.value : this._defaultLayer;
    }

    private void OnDestroy() => this._linksHandler.Clear();
  }
}
