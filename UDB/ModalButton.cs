// Decompiled with JetBrains decompiler
// Type: UDB.ModalButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.Events;

namespace UDB
{
  [Serializable]
  public class ModalButton
  {
    public string title;
    public Sprite background;
    public UnityAction action;

    public ModalButton(string title, Sprite background, UnityAction action)
    {
      this.title = title;
      this.background = background;
      this.action = action;
    }

    public ModalButton(ModalButtonDetails buttonDetails)
    {
      this.title = buttonDetails.title;
      this.background = buttonDetails.background;
      this.action = (UnityAction) null;
    }
  }
}
