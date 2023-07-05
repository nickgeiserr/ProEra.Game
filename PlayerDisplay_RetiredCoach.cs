// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_RetiredCoach
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_RetiredCoach : MonoBehaviour
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
  private TextMeshProUGUI record_Txt;
  [SerializeField]
  private TextMeshProUGUI playoffAppearances_Txt;
  [SerializeField]
  private TextMeshProUGUI championships_Txt;

  public void ShowWindow() => this.mainWindow.SetActive(true);

  public void HideWindow() => this.mainWindow.SetActive(false);

  public void SetData(CoachData coach)
  {
    this.coachPortrait_Img.sprite = PortraitManager.self.LoadCoachPortrait(coach.Skin, coach.Portrait, coach.Age);
    this.coachName_Txt.text = coach.FirstName + "\n" + coach.LastName;
    this.coachPosition_Txt.text = Common.EnumToString(coach.CoachPosition.ToString());
    TextMeshProUGUI recordTxt = this.record_Txt;
    int num = coach.CareerWins;
    string str1 = num.ToString();
    num = coach.CareerLosses;
    string str2 = num.ToString();
    string str3 = str1 + "-" + str2;
    recordTxt.text = str3;
    this.playoffAppearances_Txt.text = coach.PlayoffAppearances.ToString();
    this.championships_Txt.text = coach.Championships.ToString();
    this.ShowWindow();
  }
}
