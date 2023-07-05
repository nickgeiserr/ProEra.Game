// Decompiled with JetBrains decompiler
// Type: UDB.StateMachine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;

namespace UDB
{
  public class StateMachine
  {
    private Dictionary<Enum, List<StateMachine.StateChangeDelegate>> exitStateListeners;
    private Dictionary<Enum, List<StateMachine.StateChangeDelegate>> enterStateListeners;
    private List<StateMachine.StateChangeDelegate> stateChangeListeners;
    private Enum _currentState;
    private Enum _previousState;
    private StateObject _stateObject;

    public event StateMachine.StateChangeDelegate OnStateChange;

    public Enum currentState
    {
      get => this._currentState;
      private set => this._currentState = value;
    }

    public Enum previousState
    {
      get
      {
        if (this._previousState == null)
          this._previousState = (Enum) GenericState.Empty;
        return this._previousState;
      }
      private set => this._previousState = value;
    }

    public StateObject stateObject
    {
      get
      {
        this._stateObject = new StateObject(this.previousState, this.currentState);
        return this._stateObject;
      }
    }

    public StateMachine()
    {
      this.previousState = (Enum) GenericState.Empty;
      this.enterStateListeners = new Dictionary<Enum, List<StateMachine.StateChangeDelegate>>();
      this.exitStateListeners = new Dictionary<Enum, List<StateMachine.StateChangeDelegate>>();
      this.stateChangeListeners = new List<StateMachine.StateChangeDelegate>();
    }

    private void NotifyStateChangeListeners()
    {
      for (int index = 0; index < this.stateChangeListeners.Count; ++index)
        this.stateChangeListeners[index](this.stateObject);
    }

    private void NotifyEnterStateListeners(Enum state)
    {
      if (!this.enterStateListeners.ContainsKey(state))
        return;
      for (int index = 0; index < this.enterStateListeners[state].Count; ++index)
        this.enterStateListeners[state][index](this.stateObject);
    }

    private void NotifyExitStateListeners(Enum state)
    {
      if (!this.exitStateListeners.ContainsKey(state))
        return;
      for (int index = 0; index < this.exitStateListeners[state].Count; ++index)
        this.exitStateListeners[state][index](this.stateObject);
    }

    public bool IsInState(Enum state) => this.currentState.Equals((object) state);

    public void AddChangeStateListener(
      StateMachine.StateChangeDelegate stateChangeDelegate)
    {
      if (stateChangeDelegate == null)
        throw new StateMachineException("StateChangeDelegate 'stateChangeDelegate' cannot be null.");
      if (this.stateChangeListeners.Contains(stateChangeDelegate))
        return;
      this.stateChangeListeners.Add(stateChangeDelegate);
    }

    public void RemoveChangeStateListener(
      StateMachine.StateChangeDelegate stateChangeDelegate)
    {
      if (stateChangeDelegate == null)
        throw new StateMachineException("StateChangeDelegate 'stateChangeDelegate' cannot be null.");
      if (!this.stateChangeListeners.Contains(stateChangeDelegate))
        return;
      this.stateChangeListeners.Remove(stateChangeDelegate);
    }

    public void AddEnterStateListener(
      Enum state,
      StateMachine.StateChangeDelegate stateChangeDelegate)
    {
      if (stateChangeDelegate == null)
        throw new StateMachineException("StateChangeDelegate 'stateChangeDelegate' cannot be null.");
      if (!this.enterStateListeners.ContainsKey(state))
      {
        this.enterStateListeners.Add(state, new List<StateMachine.StateChangeDelegate>());
        this.enterStateListeners[state].Add(stateChangeDelegate);
      }
      else
      {
        if (this.enterStateListeners[state].Contains(stateChangeDelegate))
          return;
        this.enterStateListeners[state].Add(stateChangeDelegate);
      }
    }

    public void RemoveEnterStateListener(
      Enum state,
      StateMachine.StateChangeDelegate stateChangeDelegate)
    {
      if (stateChangeDelegate == null)
        throw new StateMachineException("StateChangeDelegate 'method' cannot be null.");
      if (!this.enterStateListeners.ContainsKey(state) || this.enterStateListeners[state].Contains(stateChangeDelegate))
        return;
      this.enterStateListeners[state].Remove(stateChangeDelegate);
    }

    public void AddExitStateListener(
      Enum state,
      StateMachine.StateChangeDelegate stateChangeDelegate)
    {
      if (stateChangeDelegate == null)
        throw new StateMachineException("StateChangeDelegate 'method' cannot be null.");
      if (!this.exitStateListeners.ContainsKey(state))
      {
        this.exitStateListeners.Add(state, new List<StateMachine.StateChangeDelegate>());
        this.exitStateListeners[state].Add(stateChangeDelegate);
      }
      else
      {
        if (this.exitStateListeners[state].Contains(stateChangeDelegate))
          return;
        this.exitStateListeners[state].Add(stateChangeDelegate);
      }
    }

    public void RemoveExitStateListener(
      Enum state,
      StateMachine.StateChangeDelegate stateChangeDelegate)
    {
      if (stateChangeDelegate == null)
        throw new StateMachineException("StateChangeDelegate 'stateChangeDelegate' cannot be null.");
      if (!this.exitStateListeners.ContainsKey(state) || this.exitStateListeners[state].Contains(stateChangeDelegate))
        return;
      this.exitStateListeners[state].Remove(stateChangeDelegate);
    }

    public void SetState(Enum state)
    {
      this.previousState = this.currentState;
      this.NotifyExitStateListeners(this.previousState);
      this.currentState = state;
      this.NotifyStateChangeListeners();
      if (this.OnStateChange != null)
        this.OnStateChange(this.stateObject);
      this.NotifyEnterStateListeners(this.currentState);
    }

    public delegate void StateChangeDelegate(StateObject stateObject);
  }
}
