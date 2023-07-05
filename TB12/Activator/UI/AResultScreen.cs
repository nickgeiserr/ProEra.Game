// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.AResultScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using TB12.Activator.Data;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class AResultScreen : UIView
  {
    [SerializeField]
    private ALeaderboardData _leaderboardData;
    [SerializeField]
    private ALeaderboard _leaderboard;
    [SerializeField]
    private AResultEnterNameView _enterNameView;
    [SerializeField]
    private AResultEnterTeamView _enterTeamView;

    public override Enum ViewId { get; } = (Enum) EScreens.kLeaderboardEntry;

    protected override void OnInitialize()
    {
      this._enterNameView.Initialize();
      this._enterTeamView.Initialize();
    }

    protected override void WillAppear()
    {
      this._leaderboardData.InsertAndHighlight(ActivationState.Score, AppState.GameMode);
      this._enterNameView.onConfirmed += new Action(this.NameConfirmedHandler);
      this._enterTeamView.onConfirmed += new Action(this.TeamConfirmedHandler);
      this._enterNameView.Show();
    }

    protected override void DidAppear() => this._leaderboard.ScrollToCurrent();

    protected override void DidDisappear()
    {
      this._enterNameView.onConfirmed -= new Action(this.NameConfirmedHandler);
      this._enterTeamView.onConfirmed -= new Action(this.TeamConfirmedHandler);
      this._enterNameView.Hide(false);
      this._enterTeamView.Hide(false);
    }

    private void NameConfirmedHandler()
    {
      ActivationState.PlayerName = this._enterNameView.UserName;
      this._leaderboard.UpdateNameAtIndex(this._enterNameView.UserName);
      this._enterTeamView.Show();
    }

    private void TeamConfirmedHandler()
    {
      this._leaderboard.UpdateTeamAtIndex(this._enterTeamView.TeamIndex);
      this.WaitAndHide(2f);
    }
  }
}
