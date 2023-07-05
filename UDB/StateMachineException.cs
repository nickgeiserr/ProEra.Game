// Decompiled with JetBrains decompiler
// Type: UDB.StateMachineException
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  [Serializable]
  public class StateMachineException : Exception
  {
    protected string message;

    public StateMachineException(string message) => this.message = message;

    public override string Message => this.message;

    public override string ToString() => "State Machine Exception: " + this.message;
  }
}
