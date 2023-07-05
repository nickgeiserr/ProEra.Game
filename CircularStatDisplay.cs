// Decompiled with JetBrains decompiler
// Type: CircularStatDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CircularStatDisplay : MonoBehaviour
{
  public Image fill_Img;
  public TextMeshProUGUI title_Txt;
  public TextMeshProUGUI insideValue_Txt;
  public TextMeshProUGUI topValue_Txt;
  public static int NumberOfTeamsInFranchise;

  public void SetValues(string title, int rank, int statValue, string statValueType = "YDS")
  {
    this.title_Txt.text = title;
    this.insideValue_Txt.text = "#" + (rank + 1).ToString();
    this.topValue_Txt.text = statValue.ToString() + " " + statValueType;
    this.fill_Img.fillAmount = (float) (1.0 - (double) rank / (double) CircularStatDisplay.NumberOfTeamsInFranchise);
  }
}
