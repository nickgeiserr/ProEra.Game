// Decompiled with JetBrains decompiler
// Type: UDB.IconModalPanelDetails
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;

namespace UDB
{
  public class IconModalPanelDetails : ModalPanelDetailsBase
  {
    public Sprite icon;

    public IconModalPanelDetails(string title, string message, Sprite icon, Sprite background = null)
    {
      this.title = title;
      this.message = message;
      this.icon = icon;
      this.background = background;
      this.buttons = new ModalButton[3];
      this.buttons[0] = (ModalButton) null;
      this.buttons[1] = (ModalButton) null;
      this.buttons[2] = (ModalButton) null;
    }

    public IconModalPanelDetails(IconModalDetails iconModalDetails, UnityAction[] unityActions)
    {
      this.title = iconModalDetails.title;
      this.message = iconModalDetails.message;
      this.icon = iconModalDetails.icon;
      this.background = iconModalDetails.background;
      this.buttons = new ModalButton[3];
      this.SetUpButtons((ModalDetailsBase) iconModalDetails, unityActions);
    }
  }
}
