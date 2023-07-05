// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.AISimGameInitState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using System.Collections;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/AISimGameInitState")]
  public class AISimGameInitState : GameState
  {
    public int[] MatchupHomeTeam;
    public int[] MatchupAwayTeam;
    private AISimGameTestManager testManager;
    private readonly RoutineHandle _startGameRoutine = new RoutineHandle();

    public override EAppState Id => EAppState.kAISimGameInit;

    public override bool showLoadingScreen => false;

    public override bool showTransition => false;

    public override bool allowRain => false;

    public override bool allowPause => false;

    public override bool clearFadeOnEntry => false;

    public override bool UnloadGameplayScene => false;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      this.testManager = UnityEngine.Object.FindObjectOfType<AISimGameTestManager>();
      if ((UnityEngine.Object) this.testManager == (UnityEngine.Object) null)
        Debug.LogError((object) " No AISimGameTestManager found. Please add one to the AISimGameHub scene");
      this._startGameRoutine.Run(this.StartSimGame());
    }

    protected override void OnExitState() => WorldState.WorldRevealed.Unlink(new System.Action(this.WorldRevealedHandler));

    private void WorldRevealedHandler()
    {
      if (AppState.Mode.Value != EMode.kMultiplayer)
        return;
      AppState.Mode.SetValue(EMode.kUnknown);
    }

    public override void ClearState()
    {
    }

    private IEnumerator StartSimGame()
    {
      yield return (object) new WaitForSeconds(10f);
      if ((bool) (UnityEngine.Object) this.testManager)
        this.testManager.SetNextMatchup();
      AppEvents.LoadState(EAppState.kAISimGame, ETimeOfDay.Clear);
      yield return (object) null;
    }
  }
}
