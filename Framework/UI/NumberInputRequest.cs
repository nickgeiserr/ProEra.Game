// Decompiled with JetBrains decompiler
// Type: Framework.UI.NumberInputRequest
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using TB12;
using UnityEngine;

namespace Framework.UI
{
  public class NumberInputRequest : UIRequest
  {
    public string inputString;
    private Func<string, bool> validation;

    public override Enum ViewId => (Enum) EScreens.kNumberInput;

    public string title { get; set; }

    public string body { get; set; }

    public bool canCancel { get; set; }

    public NumberInputRequest()
    {
    }

    public NumberInputRequest(string defaultString, bool canCancel = false)
    {
      this.inputString = defaultString;
      this.canCancel = canCancel;
    }

    public override bool Complete()
    {
      if (this.validation == null || this.validation(this.inputString))
        return base.Complete();
      Debug.LogError((object) "Invalid input string");
      return false;
    }
  }
}
