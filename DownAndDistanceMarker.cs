// Decompiled with JetBrains decompiler
// Type: DownAndDistanceMarker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownAndDistanceMarker : MonoBehaviour
{
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private GameObject playerView_GO;
  [SerializeField]
  private GameObject sidelineView_GO;
  [SerializeField]
  private Image playerViewBackground_Img;
  [SerializeField]
  private Image sidelineViewBackground_Img;
  [SerializeField]
  private Image playerViewLogo_Img;
  [SerializeField]
  private Image sidelineViewLogo_Img;
  [SerializeField]
  private Transform sideLineViewLogo_Trans;
  [SerializeField]
  private Transform sidelineViewText_Trans;
  [SerializeField]
  private TextMeshProUGUI playerView_Txt;
  [SerializeField]
  private TextMeshProUGUI sidelineView_Txt;
  [SerializeField]
  private Image[] borders_Img;
  [SerializeField]
  private Transform trans;

  private void Start() => this.mainWindow_CG.alpha = 0.0f;

  public void Hide() => LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);

  public void Show() => LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);

  public void SetDownMarker()
  {
    if (PlayState.IsPuntOrKickoff || global::Game.IsFG || (bool) ProEra.Game.MatchState.RunningPat)
      return;
    this.trans.eulerAngles = new Vector3(90f, 0.0f, 90f * (float) global::Game.OffensiveFieldDirection);
    this.trans.position = new Vector3(MatchManager.instance.ballHashPosition, 0.1f, ProEra.Game.MatchState.BallOn.Value) - Vector3.right * Field.SEVEN_YARDS;
    this.trans.position = new Vector3(this.trans.position.x, this.trans.position.y, this.trans.position.z);
    this.playerView_Txt.text = ScoreClockState.GetDownAndDistance(ProEra.Game.MatchState.IsHomeTeamOnOffense);
    this.sidelineView_Txt.text = ScoreClockState.GetDownAndDistance(ProEra.Game.MatchState.IsHomeTeamOnOffense);
    this.sideLineViewLogo_Trans.localScale = new Vector3((float) global::Game.OffensiveFieldDirection, (float) global::Game.OffensiveFieldDirection, 1f);
    this.sidelineViewText_Trans.localScale = new Vector3((float) global::Game.OffensiveFieldDirection, (float) global::Game.OffensiveFieldDirection, 1f);
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (PersistentData.userIsHome)
      {
        this.playerViewLogo_Img.sprite = PersistentData.GetHomeMediumLogo();
        this.sidelineViewLogo_Img.sprite = PersistentData.GetHomeMediumLogo();
        this.playerViewBackground_Img.color = PersistentData.GetHomeBackgroundColor();
        this.sidelineViewBackground_Img.color = PersistentData.GetHomeBackgroundColor();
        for (int index = 0; index < this.borders_Img.Length; ++index)
          this.borders_Img[index].color = PersistentData.GetHomeBackgroundColor();
      }
      else
      {
        this.playerViewLogo_Img.sprite = PersistentData.GetAwayMediumLogo();
        this.sidelineViewLogo_Img.sprite = PersistentData.GetAwayMediumLogo();
        this.playerViewBackground_Img.color = PersistentData.GetAwayBackgroundColor();
        this.sidelineViewBackground_Img.color = PersistentData.GetAwayBackgroundColor();
        for (int index = 0; index < this.borders_Img.Length; ++index)
          this.borders_Img[index].color = PersistentData.GetAwayBackgroundColor();
      }
    }
    else if (PersistentData.userIsHome)
    {
      this.playerViewLogo_Img.sprite = PersistentData.GetAwayMediumLogo();
      this.sidelineViewLogo_Img.sprite = PersistentData.GetAwayMediumLogo();
      this.playerViewBackground_Img.color = PersistentData.GetAwayBackgroundColor();
      this.sidelineViewBackground_Img.color = PersistentData.GetAwayBackgroundColor();
      for (int index = 0; index < this.borders_Img.Length; ++index)
        this.borders_Img[index].color = PersistentData.GetAwayBackgroundColor();
    }
    else
    {
      this.playerViewLogo_Img.sprite = PersistentData.GetHomeMediumLogo();
      this.sidelineViewLogo_Img.sprite = PersistentData.GetHomeMediumLogo();
      this.playerViewBackground_Img.color = PersistentData.GetHomeBackgroundColor();
      this.sidelineViewBackground_Img.color = PersistentData.GetHomeBackgroundColor();
      for (int index = 0; index < this.borders_Img.Length; ++index)
        this.borders_Img[index].color = PersistentData.GetHomeBackgroundColor();
    }
    this.Show();
  }
}
