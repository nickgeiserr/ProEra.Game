// Decompiled with JetBrains decompiler
// Type: UDB.TextModalController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine.Events;

namespace UDB
{
  public class TextModalController : ModalBaseController
  {
    public void ShowModal(
      string title,
      string question,
      UnityAction[] unityActions,
      string[] buttonText)
    {
      this.SetUpBaseModal(title, question, unityActions, buttonText);
      this.OpenPanel();
    }

    public void ShowModal(TextModalPanelDetails textModalPanelDetails)
    {
      this.SetUpBaseModal((ModalPanelDetailsBase) textModalPanelDetails);
      this.OpenPanel();
    }
  }
}
