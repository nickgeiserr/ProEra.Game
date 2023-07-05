// Decompiled with JetBrains decompiler
// Type: UDB.TextModalPanelDetails
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;

namespace UDB
{
  public class TextModalPanelDetails : ModalPanelDetailsBase
  {
    public TextModalPanelDetails(string title, string message, Sprite background = null)
    {
      this.title = title;
      this.message = message;
      this.background = background;
      this.buttons = new ModalButton[3];
      this.buttons[0] = (ModalButton) null;
      this.buttons[1] = (ModalButton) null;
      this.buttons[2] = (ModalButton) null;
    }

    public TextModalPanelDetails(TextModalDetails textModalDetails, UnityAction[] unityActions)
    {
      this.title = textModalDetails.title;
      this.message = textModalDetails.message;
      this.background = textModalDetails.background;
      this.buttons = new ModalButton[3];
      this.SetUpButtons((ModalDetailsBase) textModalDetails, unityActions);
    }
  }
}
