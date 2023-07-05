// Decompiled with JetBrains decompiler
// Type: StartScreenManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private RectTransform axisLogo_Trans;
  [SerializeField]
  private RectTransform copyright_Trans;
  [SerializeField]
  private Image gradientCover_Img;
  [SerializeField]
  private CanvasGroup titleSection_CG;
  [SerializeField]
  private CanvasGroup coverPlayer_CG;
  [SerializeField]
  private CanvasGroup startText_CG;
  [SerializeField]
  private Image player_Img;
  [SerializeField]
  private Sprite[] playerSprites;
  [SerializeField]
  private Color[] playerTeamColors;
  private int currentPlayerIndex;
  private int playersShown;
  private bool allowUserToProceed;

  private void Update()
  {
    if (!this.allowUserToProceed || !Input.anyKeyDown)
      return;
    this.StartCoroutine(this.BeginExitTransition());
  }

  public void ShowWindow()
  {
    this.mainWindow_GO.SetActive(true);
    this.titleSection_CG.alpha = 0.0f;
    this.coverPlayer_CG.alpha = 0.0f;
    this.startText_CG.alpha = 0.0f;
    this.gradientCover_Img.color = Color.white;
    this.allowUserToProceed = false;
    this.currentPlayerIndex = -1;
    this.playersShown = 0;
    this.StartCoroutine(this.BeginIntroTransition());
  }

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public bool IsVisible() => this.mainWindow_GO.activeInHierarchy;

  private IEnumerator BeginIntroTransition()
  {
    StartScreenManager startScreenManager = this;
    yield return (object) new WaitForSeconds(2f);
    if (startScreenManager.IsVisible())
    {
      LeanTween.move(startScreenManager.axisLogo_Trans, (Vector3) new Vector2(600f, -295f), 0.5f).setEaseOutQuart();
      LeanTween.scale(startScreenManager.axisLogo_Trans, (Vector3) new Vector2(0.35f, 0.35f), 0.5f).setEaseOutQuart();
      LeanTween.move(startScreenManager.copyright_Trans, (Vector3) new Vector2(560f, 180f), 0.5f).setEaseOutQuart();
      LeanTween.alphaCanvas(startScreenManager.titleSection_CG, 1f, 0.5f);
      startScreenManager.StartCoroutine(startScreenManager.CyclePlayers());
    }
  }

  private IEnumerator BeginExitTransition()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
    yield return (object) new WaitForSeconds(0.3f);
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowWindow();
  }

  private IEnumerator CyclePlayers()
  {
    while (this.IsVisible())
    {
      this.SetNewPlayer();
      this.gradientCover_Img.CrossFadeColor(this.playerTeamColors[this.currentPlayerIndex], 0.2f, true, true);
      if (this.playersShown > 0)
      {
        LeanTween.alphaCanvas(this.coverPlayer_CG, 0.0f, 0.1f);
        yield return (object) new WaitForSeconds(0.1f);
      }
      this.player_Img.sprite = this.playerSprites[this.currentPlayerIndex];
      LeanTween.alphaCanvas(this.coverPlayer_CG, 1f, 0.1f);
      if (this.playersShown == 1)
      {
        LeanTween.alphaCanvas(this.startText_CG, 1f, 0.1f);
        this.allowUserToProceed = true;
      }
      ++this.playersShown;
      yield return (object) new WaitForSeconds(3f);
    }
  }

  private void SetNewPlayer()
  {
    int num = this.currentPlayerIndex;
    while (num == this.currentPlayerIndex)
      num = Random.Range(0, this.playerSprites.Length);
    this.currentPlayerIndex = num;
  }
}
