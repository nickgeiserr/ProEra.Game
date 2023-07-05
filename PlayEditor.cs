// Decompiled with JetBrains decompiler
// Type: PlayEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;
using UnityEngine.UI;

public class PlayEditor : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image gradientCover_Img;
  private WaitForSecondsRealtime _disableMove = new WaitForSecondsRealtime(0.2f);

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void Init()
  {
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.ShowBackButton();
    BottomBarManager.instance.SetControllerButtonGuide(10);
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowBottomSection();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowCreateSection();
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenKeyboard.instance.IsVisible())
      return;
    int num = ControllerManagerTitle.self.usingController ? 1 : 0;
  }
}
