// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_CoachRating
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_CoachRating : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow;
  [SerializeField]
  private Image coachPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI coachName_Txt;
  [SerializeField]
  private TextMeshProUGUI coachPosition_Txt;
  [SerializeField]
  private TextMeshProUGUI overall_Txt;

  public void ShowWindow() => this.mainWindow.SetActive(true);

  public void HideWindow() => this.mainWindow.SetActive(false);

  public void SetCoachData(CoachData coach)
  {
    this.coachPortrait_Img.sprite = PortraitManager.self.LoadCoachPortrait(coach.Skin, coach.Portrait, coach.Age);
    this.coachName_Txt.text = coach.FirstName + "\n" + coach.LastName;
    this.coachPosition_Txt.text = Common.EnumToString(coach.CoachPosition.ToString());
    this.overall_Txt.text = TeamData.GetAttributeGradeFromNumber(coach.GetOverall());
    this.ShowWindow();
  }
}
