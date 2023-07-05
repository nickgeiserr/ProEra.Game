// Decompiled with JetBrains decompiler
// Type: GameStatLineDisplaySlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStatLineDisplaySlot : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI title_Txt;
  [SerializeField]
  private TextMeshProUGUI homeValue_Txt;
  [SerializeField]
  private TextMeshProUGUI awayValue_Txt;
  [SerializeField]
  private Image homeLine_Img;
  [SerializeField]
  private Image awayLine_Img;
  private static float lineFillPadding = 0.005f;
  private static float maxLineFill = (float) (1.0 - (double) GameStatLineDisplaySlot.lineFillPadding * 2.0);
  private static float minLineFill = GameStatLineDisplaySlot.lineFillPadding;
  private static Color largerColor = Color.white;
  private static Color smallerColor = new Color(1f, 1f, 1f, 0.5f);

  public void SetData(string title, int homeValue, int awayValue, bool reverseDisplay = false)
  {
    this.title_Txt.text = title;
    this.homeValue_Txt.text = homeValue.ToString();
    this.awayValue_Txt.text = awayValue.ToString();
    float num = (float) (homeValue + awayValue);
    if ((double) num == 0.0)
    {
      this.homeLine_Img.fillAmount = 0.5f - GameStatLineDisplaySlot.lineFillPadding;
      this.awayLine_Img.fillAmount = 0.5f - GameStatLineDisplaySlot.lineFillPadding;
    }
    else if (homeValue < 0 && awayValue < 0)
    {
      this.homeLine_Img.fillAmount = GameStatLineDisplaySlot.minLineFill;
      this.awayLine_Img.fillAmount = GameStatLineDisplaySlot.minLineFill;
    }
    else if (homeValue < 0)
    {
      this.homeLine_Img.fillAmount = GameStatLineDisplaySlot.minLineFill;
      this.awayLine_Img.fillAmount = GameStatLineDisplaySlot.maxLineFill;
    }
    else if (awayValue < 0)
    {
      this.homeLine_Img.fillAmount = GameStatLineDisplaySlot.maxLineFill;
      this.awayLine_Img.fillAmount = GameStatLineDisplaySlot.minLineFill;
    }
    else
    {
      this.homeLine_Img.fillAmount = Mathf.Clamp((float) homeValue / num - GameStatLineDisplaySlot.lineFillPadding, GameStatLineDisplaySlot.minLineFill, GameStatLineDisplaySlot.maxLineFill);
      this.awayLine_Img.fillAmount = Mathf.Clamp((float) awayValue / num - GameStatLineDisplaySlot.lineFillPadding, GameStatLineDisplaySlot.minLineFill, GameStatLineDisplaySlot.maxLineFill);
    }
    if (homeValue > awayValue)
    {
      this.homeLine_Img.color = GameStatLineDisplaySlot.largerColor;
      this.awayLine_Img.color = GameStatLineDisplaySlot.smallerColor;
    }
    else if (awayValue > homeValue)
    {
      this.homeLine_Img.color = GameStatLineDisplaySlot.smallerColor;
      this.awayLine_Img.color = GameStatLineDisplaySlot.largerColor;
    }
    else
    {
      this.homeLine_Img.color = GameStatLineDisplaySlot.largerColor;
      this.awayLine_Img.color = GameStatLineDisplaySlot.largerColor;
    }
    if (!reverseDisplay)
      return;
    Color color = this.homeLine_Img.color;
    float fillAmount = this.homeLine_Img.fillAmount;
    this.homeLine_Img.color = this.awayLine_Img.color;
    this.homeLine_Img.fillAmount = this.awayLine_Img.fillAmount;
    this.awayLine_Img.color = color;
    this.awayLine_Img.fillAmount = fillAmount;
  }
}
