// Decompiled with JetBrains decompiler
// Type: LoadingScreenManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MovementEffects;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
  public static LoadingScreenManager self;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private TextMeshProUGUI loadingInfo_Txt;
  [SerializeField]
  private Image loadingBar_Img;

  private void Awake()
  {
    if ((Object) LoadingScreenManager.self == (Object) null)
    {
      LoadingScreenManager.self = this;
      this.mainWindow_CG.alpha = 0.0f;
      this.mainWindow_CG.blocksRaycasts = false;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }
    else
      Object.Destroy((Object) this.gameObject);
  }

  public void ShowWindow()
  {
    this.mainWindow_CG.alpha = 1f;
    this.mainWindow_CG.blocksRaycasts = true;
  }

  public void HideWindowImmediately()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void HideWindowAfterDelay(int framesToWait = 120) => this.StartCoroutine(this.DelayHideWindow(framesToWait));

  private IEnumerator DelayHideWindow(int framesToWait)
  {
    for (int frame = 0; frame < framesToWait; ++frame)
      yield return (object) null;
    this.HideWindow();
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void LoadScene(string sceneToLoad, string loadingText)
  {
    this.SetLoadingText("Initializing Unload...");
    PersistentData.previousScene = SceneManager.GetActiveScene().name;
    Cursor.visible = true;
    this.StopAllCoroutines();
    Timing.KillCoroutines();
    this.ResetLoadingBar();
    this.ShowWindow();
    this.StartCoroutine(this.InitSceneLoading(loadingText, sceneToLoad));
  }

  private IEnumerator InitSceneLoading(string loadingText, string sceneToLoad)
  {
    yield return (object) null;
    this.SetLoadingText("Unloading Assets...");
    UnityEngine.Resources.UnloadUnusedAssets();
    string transitionLoadingText = "Unloading Scene Data...";
    this.SetLoadingText(transitionLoadingText);
    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Empty Transition Scene");
    while ((double) asyncOperation.progress < 0.89999997615814209)
    {
      float fillAmount = asyncOperation.progress + 0.1f;
      this.SetLoadingText(transitionLoadingText + " " + Mathf.RoundToInt(fillAmount * 100f).ToString() + "%");
      this.SetLoadingBarFill(fillAmount);
      yield return (object) null;
    }
    this.ResetLoadingBar();
    this.SetLoadingText(loadingText);
    asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    asyncOperation.allowSceneActivation = false;
    while ((double) asyncOperation.progress < 0.89999997615814209)
    {
      float fillAmount = asyncOperation.progress + 0.1f;
      this.SetLoadingText(loadingText + " " + Mathf.RoundToInt(fillAmount * 100f).ToString() + "%");
      this.SetLoadingBarFill(fillAmount);
      yield return (object) null;
    }
    this.SetLoadingText("Initializing...");
    asyncOperation.allowSceneActivation = true;
  }

  public void SetLoadingBarFill(float fillAmount) => this.loadingBar_Img.fillAmount = fillAmount;

  public void SetLoadingText(string loadingText) => this.loadingInfo_Txt.text = loadingText;

  public void ResetLoadingBar() => this.loadingBar_Img.fillAmount = 0.0f;
}
