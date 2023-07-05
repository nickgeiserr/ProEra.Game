// Decompiled with JetBrains decompiler
// Type: UDB.ModalBaseController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UDB
{
  public class ModalBaseController : MonoBehaviour
  {
    public GameObject modalPanel;
    public Text title;
    public Text message;
    public List<UIButton> buttons = new List<UIButton>();

    private void SetButtonDetails(int index, ref ModalButton button)
    {
      if (button != null)
      {
        this.buttons[index].RemoveAllListeners();
        this.buttons[index].AddListener(button.action);
        this.buttons[index].SetActive(true);
        this.buttons[index].SetTitle(button.title);
      }
      else
        this.buttons[index].SetActive(false);
    }

    protected void SetUpBaseModal(
      string title,
      string message,
      UnityAction[] unityActions,
      string[] buttonText)
    {
      for (int index = 0; index < unityActions.Length; ++index)
      {
        if (index < this.buttons.Count)
        {
          UIButton button = this.buttons[index];
          button.RemoveAllListeners();
          button = this.buttons[index];
          button.AddListener(unityActions[index]);
          button = this.buttons[index];
          button.SetTitle(buttonText[index]);
          button = this.buttons[index];
          button.SetActive(true);
        }
      }
      for (int length = unityActions.Length; length < this.buttons.Count; ++length)
        this.buttons[length].gameObject.SetActive(false);
      this.title.text = title;
      this.message.text = message;
    }

    protected void SetUpBaseModal(ModalPanelDetailsBase modalPanelDetailsBase)
    {
      if (this.buttons.Count >= 3)
      {
        this.SetButtonDetails(0, ref modalPanelDetailsBase.buttons[0]);
        this.SetButtonDetails(1, ref modalPanelDetailsBase.buttons[1]);
        this.SetButtonDetails(2, ref modalPanelDetailsBase.buttons[2]);
      }
      this.title.text = modalPanelDetailsBase.title;
      this.message.text = modalPanelDetailsBase.message;
    }

    public void ClosePanel()
    {
      this.transform.SetAsFirstSibling();
      this.modalPanel.SetActive(false);
    }

    public void OpenPanel()
    {
      this.transform.SetAsLastSibling();
      this.modalPanel.SetActive(true);
    }
  }
}
