// Decompiled with JetBrains decompiler
// Type: StateQBThrow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;

public class StateQBThrow : StateMachineBehaviour
{
  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (!MatchManager.instance.playersManager.throwStarted || SingletonBehaviour<BallManager, MonoBehaviour>.instance.ballState != EBallState.PlayersHands)
      return;
    Debug.LogError((object) "PLayer is exiting Throw Animation but ball has not be released");
    MatchManager.instance.playersManager.throwStarted = false;
    MatchManager.instance.playersManager.forceQBScramble = true;
  }
}
