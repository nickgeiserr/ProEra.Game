// Decompiled with JetBrains decompiler
// Type: TB12.PracticeModeFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12
{
  public class PracticeModeFlow : AxisGameFlow
  {
    private string[] _testCases = new string[1]
    {
      "{\"version\":\"0.2.14\",\"throwData\":{\"timestamp\":132908910807367771,\"successfulThrow\":true,\"throwVector\":{\"x\":7.253625869750977,\"y\":-0.16947391629219056,\"z\":10.481449127197266},\"startPosition\":{\"x\":0.42305195331573489,\"y\":1.3142496347427369,\"z\":-32.79066467285156},\"flightTime\":0.30485522747039797,\"targetIndex\":7,\"targetPosition\":{\"x\":7.598560333251953,\"y\":-0.0001938298810273409,\"z\":-24.11655044555664}},\"gameData\":{\"offFormation\":\"SHOTGUN\",\"offPlayType\":\"ACE POST\",\"defFormation\":\"FOUR THREE\",\"defPlayType\":\"COVER 2 INSIDE STUFF\",\"snapTime\":0.5061721801757813,\"startFieldPosition\":-27.431997299194337,\"startHash\":0.0,\"offenseNorth\":true},\"rawThrowVector\":{\"x\":2.9014503955841066,\"y\":-0.07061412930488587,\"z\":4.192579746246338},\"cameraForward\":{\"x\":0.72324538230896,\"y\":-0.17157617211341859,\"z\":0.6689377427101135}}"
    };
    private int _testCaseIndex;
    private string replayJSON;
    private ReplayData _replayData;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    protected override void Awake()
    {
      base.Awake();
      this._ballHikeSequence.OnHikeComplete += new System.Action(this.TurnOnLocomotion);
      PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        Ball.State.BallState.Link<EBallState>(new Action<EBallState>(this.HandleBallState)),
        PlaybookState.HidePlaybook.Link(new System.Action(((AxisGameFlow) this).HandlePlayCallEnded)),
        PlaybookState.ShowPlaybook.Link(new System.Action(((AxisGameFlow) this).HandlePlayCallStarted))
      });
      this._testCaseIndex = 0;
      ProEra.Game.MatchState.Down.ClearValueEvents();
    }

    private new void HandleGameEvent(PEGameplayEvent e)
    {
      if (!(e is PEPlayOverEvent pePlayOverEvent))
        return;
      this.OnPlayComplete(pePlayOverEvent.Type.ToString());
    }

    private void TurnOnLocomotion() => VRState.LocomotionEnabled.SetValue(true);

    private IEnumerator WaitToThrowBall()
    {
      PracticeModeFlow practiceModeFlow = this;
      yield return (object) new WaitForSeconds(practiceModeFlow._replayData.gameData.snapTime + 0.5f);
      BallObject component = SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetComponent<BallObject>();
      component.transform.position = practiceModeFlow._replayData.throwData.startPosition;
      List<PlayerAI> curUserScriptRef = MatchManager.instance.playersManager.curUserScriptRef;
      for (int index = 0; index < curUserScriptRef.Count; ++index)
      {
        if (curUserScriptRef[index].indexInFormation == practiceModeFlow._replayData.throwData.targetIndex)
        {
          curUserScriptRef[index].trans.position = practiceModeFlow._replayData.throwData.targetPosition;
          EligibilityManager.Instance.SetCurrentTarget(curUserScriptRef[index]);
          break;
        }
      }
      practiceModeFlow._handsDataModel.ActiveHand.hand.transform.parent.GetComponentInChildren<BallThrowMechanic>().ThrowBall(component, practiceModeFlow._replayData.rawThrowVector);
      Debug.Break();
    }

    private void OnPlayComplete(string type)
    {
      Debug.Log((object) nameof (OnPlayComplete));
      Globals.ReplayMode.SetValue(false);
      ++this._testCaseIndex;
    }

    private void HandleReplayStartCalled()
    {
      Debug.Log((object) nameof (HandleReplayStartCalled));
      this.replayJSON = this._testCases[this._testCaseIndex];
      Globals.ReplayMode.SetValue(true);
      this._replayData = ReplayData.LoadData(this.replayJSON);
      ProEra.Game.MatchState.Turnover.Value = false;
      PlayDataOff formationPlayByName1 = (PlayDataOff) PlayManager.GetFormationPlayByName(this._replayData.gameData.offPlayType, PlayManager.GetFormationByBaseName(this._replayData.gameData.offFormation, Plays.self.GetOffPlaybookForPlayer(Player.One)));
      PlayDataDef formationPlayByName2 = (PlayDataDef) PlayManager.GetFormationPlayByName(this._replayData.gameData.defPlayType, PlayManager.GetFormationByBaseName(this._replayData.gameData.defFormation, Plays.self.GetDefPlaybookForPlayer(Player.One)));
      MatchManager.instance.playManager.LoadPlay(formationPlayByName1, formationPlayByName2, this._replayData.gameData.startFieldPosition, this._replayData.gameData.startHash, this._replayData.gameData.offenseNorth);
      PlaybookState.HidePlaybook.Trigger();
      UIDispatch.FrontScreen.HideView(EScreens.kPracticeMode);
      MatchManager.instance.timeManager.SetRunPlayClock(false);
    }

    private void HandleBallState(EBallState state)
    {
      if (!Globals.ReplayMode.Value || state != EBallState.InCentersHandsBeforeSnap)
        return;
      this.StartCoroutine(this.WaitToSnapBall());
    }

    private IEnumerator WaitToSnapBall()
    {
      yield return (object) new WaitForSeconds(0.5f);
      System.Action onSnapCalled = EditorUserControls.OnSnapCalled;
      if (onSnapCalled != null)
        onSnapCalled();
    }

    protected override void OnDestroy()
    {
      PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);
      this._linksHandler.Clear();
      this._ballHikeSequence.OnHikeComplete -= new System.Action(this.TurnOnLocomotion);
      base.OnDestroy();
    }
  }
}
