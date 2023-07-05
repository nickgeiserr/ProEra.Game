// Decompiled with JetBrains decompiler
// Type: BadgeDisplaySmall
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeDisplaySmall : MonoBehaviour
{
  [SerializeField]
  private Image badgeLogo_Img;
  [SerializeField]
  private TextMeshProUGUI badgeTitle_Txt;

  public void SetBadgeData(int badgeID)
  {
    if (badgeID == -1)
    {
      this.badgeLogo_Img.enabled = false;
      this.badgeTitle_Txt.text = "";
    }
    else
    {
      CoachBadge coachBadge = BadgeDatabase.GetCoachBadge(badgeID);
      this.badgeLogo_Img.enabled = true;
      this.badgeLogo_Img.sprite = coachBadge.icon;
      this.badgeTitle_Txt.text = coachBadge.title.ToUpper();
    }
  }
}
