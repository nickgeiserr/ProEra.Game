// Decompiled with JetBrains decompiler
// Type: BadgeDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeDisplay : MonoBehaviour
{
  [SerializeField]
  private GameObject badge_GO;
  [SerializeField]
  private Image badgeIcon;
  [SerializeField]
  private TextMeshProUGUI title_Txt;
  [SerializeField]
  private TextMeshProUGUI requiredRole_Txt;
  [SerializeField]
  private TextMeshProUGUI description_Txt;

  public void HideDisplay() => this.badge_GO.SetActive(false);

  public void ShowDisplay() => this.badge_GO.SetActive(true);

  public void ShowCoachBadgeData(CoachBadge badge)
  {
    if ((Object) badge == (Object) null)
    {
      this.HideDisplay();
    }
    else
    {
      this.badgeIcon.sprite = badge.icon;
      this.title_Txt.text = badge.title.ToUpper();
      this.requiredRole_Txt.text = "REQUIRES: " + Common.EnumToString(badge.requiredRole.ToString());
      if (badge.allowHigherRoleToActivate)
        this.requiredRole_Txt.text += "+";
      this.description_Txt.text = badge.description;
      this.ShowDisplay();
    }
  }

  public void ShowCoachBadgeData(int badgeID) => this.ShowCoachBadgeData(BadgeDatabase.GetCoachBadge(badgeID));

  public void ShowBadgeData(PlayerBadge badge)
  {
  }
}
