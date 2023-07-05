// Decompiled with JetBrains decompiler
// Type: KickMeterManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using ProEra.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KickMeterManager : MonoBehaviour, IKickMeter
{
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Transform kickArrow_Trans;
  [SerializeField]
  private Image logoBackground_Img;
  [SerializeField]
  private Image logo_Img;
  [SerializeField]
  private Image kickMeterFill_Img;
  [SerializeField]
  private RectTransform windDirectionArrow_Trans;
  [SerializeField]
  private TextMeshProUGUI windDirection_Txt;
  [SerializeField]
  private UnityEngine.UI.Button mobileKickButton_btn;
  private float kickAngleMin = 20f;
  private float kickAngleMax = 60f;
  private float kickAimMin = -20f;
  private float kickAimMax = 20f;
  private float kickAngle;
  private float kickAim;
  private float aimSpeed;
  private KickControlState kickState;
  private float normalizedPower;

  private void Awake() => ProEra.Game.Sources.UI.KickMeter = (IKickMeter) this;

  public void Init()
  {
    this.kickAngle = 50f;
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void ShowWindow()
  {
    this.logoBackground_Img.color = PersistentData.GetOffensiveTeamData().GetPrimaryColor();
    this.logo_Img.sprite = PersistentData.GetOffensiveTeamData().GetMediumLogo();
    this.SetWind();
    this.kickAngle = 50f;
    this.kickAim = 0.0f;
    if (!global::Game.IsOnsidesKick)
      this.kickArrow_Trans.gameObject.SetActive(true);
    else
      this.kickArrow_Trans.gameObject.SetActive(false);
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
  }

  public void HideWindow()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.kickArrow_Trans.gameObject.SetActive(false);
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  private void FixedUpdate()
  {
    if (!this.IsVisible())
      return;
    this.UpdateKickAngle();
    if ((double) this.kickMeterFill_Img.fillAmount >= 1.0)
      this.kickMeterFill_Img.fillAmount = 0.0f;
    this.kickMeterFill_Img.fillAmount += 0.02f;
  }

  public void SetWind()
  {
    this.windDirection_Txt.text = Mathf.RoundToInt(Mathf.Sqrt((float) ((double) MatchManager.instance.windSpeed.x * (double) MatchManager.instance.windSpeed.x + (double) MatchManager.instance.windSpeed.z * (double) MatchManager.instance.windSpeed.z)) * 2f).ToString() + " MPH";
    Vector2 vec2 = new Vector2(MatchManager.instance.windSpeed.x * (float) global::Game.OffensiveFieldDirection, MatchManager.instance.windSpeed.z * (float) global::Game.OffensiveFieldDirection);
    float zAngle = 0.0f;
    if (PersistentData.windType != 0)
      zAngle = this.AngleBetweenVector2(Vector2.up, vec2);
    this.windDirectionArrow_Trans.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    this.windDirectionArrow_Trans.Rotate(0.0f, 0.0f, zAngle);
  }

  private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
  {
    float num = (double) Vector2.Dot(new Vector2(-vec1.y, vec1.x), vec2) < 0.0 ? -1f : 1f;
    return Vector2.Angle(vec1, vec2) * num;
  }

  public void SetAimSpeed(int s) => this.aimSpeed = 1f + (100f - (float) s) * 0.1f;

  public float GetKickHeightAngle() => this.kickAngle;

  public float GetKickPower() => this.kickMeterFill_Img.fillAmount * 100f;

  public float GetKickDirection() => (double) this.kickArrow_Trans.eulerAngles.z < 180.0 ? (float) ((double) this.kickArrow_Trans.eulerAngles.z / (double) this.kickAimMax * -1.0) : (360f - this.kickArrow_Trans.eulerAngles.z) / this.kickAimMax;

  private void UpdateKickAngle()
  {
    Player userIndex = !global::Game.IsPlayerOneOnOffense ? Player.Two : Player.One;
    this.kickAngle = Mathf.Clamp(this.kickAngle + UserManager.instance.LeftStickY(userIndex), this.kickAngleMin, this.kickAngleMax);
    this.kickAim = Mathf.Clamp(this.kickAim - UserManager.instance.LeftStickX(userIndex), this.kickAimMin, this.kickAimMax);
    this.kickArrow_Trans.position = KickMeterManager.GetBallPosition() + KickMeterManager.GetKickArrowOffset();
    this.kickArrow_Trans.eulerAngles = this.GetKickArrowAngles();
  }

  private Vector3 GetKickArrowAngles() => new Vector3(90f - this.kickAngle, 0.0f, this.kickAim);

  private static Vector3 GetKickArrowOffset()
  {
    float nineInches = Field.NINE_INCHES;
    if (global::Game.IsFG)
      return Field.FlipVectorByFieldDirection(new Vector3(0.11f, nineInches, -3f));
    if (PlayState.IsPunt)
      return Field.FlipVectorByFieldDirection(new Vector3(0.0f, nineInches, -5f));
    return PlayState.IsKickoff ? Field.FlipVectorByFieldDirection(new Vector3(0.0f, nineInches, -5f)) : Vector3.zero;
  }

  private static Vector3 GetBallPosition() => new Vector3(MatchManager.instance.ballHashPosition, 0.0f, ProEra.Game.MatchState.BallOn.Value);
}
