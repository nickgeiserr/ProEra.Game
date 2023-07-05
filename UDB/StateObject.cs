// Decompiled with JetBrains decompiler
// Type: UDB.StateObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  [Serializable]
  public struct StateObject
  {
    public Enum previousState;
    public Enum currentState;
    public Enum nextState;

    public StateObject(Enum previousState, Enum currentState, Enum nextState)
    {
      this.previousState = previousState;
      this.currentState = currentState;
      this.nextState = nextState;
    }

    public StateObject(Enum previousState, Enum currentState)
    {
      this.previousState = previousState;
      this.currentState = currentState;
      this.nextState = (Enum) GenericState.Empty;
    }

    public StateObject(Enum currentState)
    {
      this.previousState = (Enum) GenericState.Empty;
      this.currentState = currentState;
      this.nextState = (Enum) GenericState.Empty;
    }
  }
}
