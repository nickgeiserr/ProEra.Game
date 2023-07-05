// Decompiled with JetBrains decompiler
// Type: UDB.StateMachineTest
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public class StateMachineTest : MonoBehaviour
  {
    public StateMachine testStateMachine;
    public TestEnum state;
    public StateObject enterStateObject;
    public StateObject exitStateObject;
    public StateObject changeStateObject;

    private void Start()
    {
      this.testStateMachine = new StateMachine();
      this.testStateMachine.AddChangeStateListener(new StateMachine.StateChangeDelegate(this.ChangeStateCallback));
      this.testStateMachine.AddEnterStateListener((Enum) TestEnum.Four, new StateMachine.StateChangeDelegate(this.EnterStateCallback));
      this.testStateMachine.AddExitStateListener((Enum) TestEnum.Four, new StateMachine.StateChangeDelegate(this.ExitStateCallback));
    }

    private void EnterStateCallback(StateObject stateObject)
    {
      Debug.Log((object) ("EnterStateCallback: currentState" + stateObject.currentState.ToString() + " previousState: " + stateObject.previousState.ToString()));
      this.enterStateObject = stateObject;
    }

    private void ExitStateCallback(StateObject stateObject)
    {
      Debug.Log((object) ("ExitStateCallback: currentState" + stateObject.currentState.ToString() + " previousState: " + stateObject.previousState.ToString()));
      this.exitStateObject = stateObject;
    }

    private void ChangeStateCallback(StateObject stateObject)
    {
      Debug.Log((object) ("ChangeStateCallback: currentState" + stateObject.currentState.ToString() + " previousState: " + stateObject.previousState.ToString()));
      this.changeStateObject = stateObject;
    }

    public void ChangeState() => this.testStateMachine.SetState((Enum) this.state);
  }
}
