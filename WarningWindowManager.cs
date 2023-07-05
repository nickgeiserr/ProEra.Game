// Decompiled with JetBrains decompiler
// Type: WarningWindowManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class WarningWindowManager : MonoBehaviour
{
  public static WarningWindowManager instance;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private TextMeshProUGUI message_Txt;
  [SerializeField]
  private UnityEngine.UI.Button ok_Btn;

  private void Awake()
  {
    if ((Object) WarningWindowManager.instance == (Object) null)
      WarningWindowManager.instance = this;
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void ShowWindow(string message)
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    this.message_Txt.text = message;
    BottomBarManager.instance.ShowBackButton();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.ok_Btn);
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowWindow();
  }
}
