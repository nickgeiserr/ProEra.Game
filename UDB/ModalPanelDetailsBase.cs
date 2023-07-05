// Decompiled with JetBrains decompiler
// Type: UDB.ModalPanelDetailsBase
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.Events;

namespace UDB
{
  [Serializable]
  public class ModalPanelDetailsBase
  {
    public ModalType modalType;
    public string title;
    public string message;
    public Sprite background;
    public ModalButton[] buttons;

    protected void SetUpButtons(ModalDetailsBase modalDetails, UnityAction[] unityActions)
    {
      if (modalDetails.buttons.Length != 0)
      {
        this.buttons[0] = new ModalButton(modalDetails.buttons[0]);
        this.buttons[0].action = unityActions[0];
      }
      else
        this.buttons[0] = (ModalButton) null;
      if (modalDetails.buttons.Length > 1)
      {
        this.buttons[1] = new ModalButton(modalDetails.buttons[1]);
        this.buttons[1].action = unityActions[1];
      }
      else
        this.buttons[1] = (ModalButton) null;
      if (modalDetails.buttons.Length > 2)
      {
        this.buttons[2] = new ModalButton(modalDetails.buttons[2]);
        this.buttons[2].action = unityActions[2];
      }
      else
        this.buttons[2] = (ModalButton) null;
    }
  }
}
