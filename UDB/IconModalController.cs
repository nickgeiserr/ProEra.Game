// Decompiled with JetBrains decompiler
// Type: UDB.IconModalController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UDB
{
  public class IconModalController : ModalBaseController
  {
    public Image icon;

    public void ShowModal(
      string title,
      string question,
      UnityAction[] unityActions,
      string[] buttonText,
      Sprite icon = null)
    {
      this.SetUpBaseModal(title, question, unityActions, buttonText);
      if ((Object) icon != (Object) null)
      {
        this.icon.sprite = icon;
        this.icon.gameObject.SetActive(true);
      }
      else
        this.icon.gameObject.SetActive(false);
      this.OpenPanel();
    }

    public void ShowModal(IconModalPanelDetails iconModalPanelDetails)
    {
      this.SetUpBaseModal((ModalPanelDetailsBase) iconModalPanelDetails);
      if ((Object) iconModalPanelDetails.icon != (Object) null)
      {
        this.icon.sprite = iconModalPanelDetails.icon;
        this.icon.gameObject.SetActive(true);
      }
      else
        this.icon.gameObject.SetActive(false);
      this.OpenPanel();
    }
  }
}
