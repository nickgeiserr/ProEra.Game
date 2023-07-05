// Decompiled with JetBrains decompiler
// Type: PopupLoadingScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class PopupLoadingScreen : MonoBehaviour
{
  public static PopupLoadingScreen self;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private TextMeshProUGUI loadingText;
  public bool screenIsLoading;

  private void Awake()
  {
    PopupLoadingScreen.self = this;
    this.HidePopupLoadingScreen();
  }

  public void ShowPopupLoadingScreen(string loadText)
  {
    this.mainWindow_CG.alpha = 1f;
    this.screenIsLoading = true;
    this.loadingText.text = loadText;
  }

  public void HidePopupLoadingScreen()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
    this.screenIsLoading = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha == 1.0;
}
