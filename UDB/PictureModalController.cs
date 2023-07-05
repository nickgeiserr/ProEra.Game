// Decompiled with JetBrains decompiler
// Type: UDB.PictureModalController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UDB
{
  public class PictureModalController : ModalBaseController
  {
    public Image picture;

    public void ShowModal(
      string title,
      string question,
      UnityAction[] unityActions,
      string[] buttonText,
      Sprite picture = null)
    {
      this.SetUpBaseModal(title, question, unityActions, buttonText);
      if ((Object) picture != (Object) null)
      {
        this.picture.sprite = picture;
        this.picture.gameObject.SetActive(true);
      }
      else
        this.picture.gameObject.SetActive(false);
      this.OpenPanel();
    }

    public void ShowModal(PictureModalPanelDetails pictureModalPanelDetails)
    {
      this.SetUpBaseModal((ModalPanelDetailsBase) pictureModalPanelDetails);
      if ((Object) pictureModalPanelDetails.picture != (Object) null)
      {
        this.picture.sprite = pictureModalPanelDetails.picture;
        this.picture.gameObject.SetActive(true);
      }
      else
        this.picture.gameObject.SetActive(false);
      this.OpenPanel();
    }
  }
}
