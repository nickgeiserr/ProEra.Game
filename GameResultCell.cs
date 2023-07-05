// Decompiled with JetBrains decompiler
// Type: GameResultCell
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultCell : MonoBehaviour
{
  [SerializeField]
  private TMP_Text downAndDistance;
  [SerializeField]
  private TMP_Text gamecast;
  [SerializeField]
  private TMP_Text offInfo1;
  [SerializeField]
  private TMP_Text offInfo2;
  [SerializeField]
  private TMP_Text offInfo3;
  [SerializeField]
  private TMP_Text defInfo1;
  [SerializeField]
  private TMP_Text defInfo2;
  [SerializeField]
  private TMP_Text defInfo3;
  [SerializeField]
  private Image offTeamLogo;
  [SerializeField]
  private Image defTeamLogo;

  public void SetOffTeam(Sprite value) => this.offTeamLogo.sprite = value;

  public void SetDefTeam(Sprite value) => this.defTeamLogo.sprite = value;

  public void SetDownAndDistanceInfo(string message) => this.downAndDistance.text = message;

  public void SetGamecastInfo(string message) => this.gamecast.text = message;

  public void SetOffInfo1(string message) => this.offInfo1.text = message;

  public void SetOffInfo2(string message) => this.offInfo2.text = message;

  public void SetOffInfo3(string message) => this.offInfo3.text = message;

  public void SetDefInfo1(string message) => this.defInfo1.text = message;

  public void SetDefInfo2(string message) => this.defInfo2.text = message;

  public void SetDefInfo3(string message) => this.defInfo3.text = message;

  public void ClearContent()
  {
    this.SetOffTeam((Sprite) null);
    this.SetDefTeam((Sprite) null);
    this.SetDownAndDistanceInfo(string.Empty);
    this.SetGamecastInfo(string.Empty);
    this.SetOffInfo1(string.Empty);
    this.SetOffInfo2(string.Empty);
    this.SetOffInfo3(string.Empty);
    this.SetDefInfo1(string.Empty);
    this.SetDefInfo2(string.Empty);
    this.SetDefInfo3(string.Empty);
  }
}
