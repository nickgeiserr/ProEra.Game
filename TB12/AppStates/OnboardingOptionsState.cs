// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.OnboardingOptionsState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using Framework.StateManagement;
using System;
using System.Collections.Generic;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/OnboardingOptionsState")]
  public class OnboardingOptionsState : GameState
  {
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override EAppState Id => EAppState.kOnboardingOptions;

    public override bool allowPause => false;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      this._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(this.HandleStateTransition))
      });
    }

    protected override void OnExitState() => this._linksHandler.Clear();

    private void HandleStateTransition(bool inTransition)
    {
      if (inTransition)
        return;
      UIDispatch.FrontScreen.DisplayView(EScreens.kOnboardingOptions);
    }
  }
}
