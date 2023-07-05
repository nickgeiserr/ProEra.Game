// Decompiled with JetBrains decompiler
// Type: TeamPromotionSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamPromotionSlot : MonoBehaviour
{
  public TextMeshProUGUI results_Txt;
  public Image teamLogo_Img;
  [SerializeField]
  private Image upArrow_Img;
  [SerializeField]
  private Image downArrow_Img;
  [SerializeField]
  private Image line_Img;

  public void SetPromotionArrow()
  {
    this.upArrow_Img.enabled = true;
    this.downArrow_Img.enabled = false;
    this.line_Img.enabled = false;
  }

  public void SetDemotionArrow()
  {
    this.upArrow_Img.enabled = false;
    this.downArrow_Img.enabled = true;
    this.line_Img.enabled = false;
  }

  public void SetRemainsArrow()
  {
    this.upArrow_Img.enabled = false;
    this.downArrow_Img.enabled = false;
    this.line_Img.enabled = true;
  }
}
