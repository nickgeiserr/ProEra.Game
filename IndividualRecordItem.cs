// Decompiled with JetBrains decompiler
// Type: IndividualRecordItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class IndividualRecordItem : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI recordTitle_Txt;
  [SerializeField]
  private TextMeshProUGUI year_Txt;
  [SerializeField]
  private TextMeshProUGUI team_Txt;
  [SerializeField]
  private TextMeshProUGUI player_Txt;
  [SerializeField]
  private TextMeshProUGUI record_Txt;
  private bool doesRecordHaveAHolder;
  private float value;

  public void SetData(RecordHolder recordHolder)
  {
    this.doesRecordHaveAHolder = recordHolder.Year != 0;
    this.recordTitle_Txt.text = recordHolder.RecordName;
    if (this.doesRecordHaveAHolder)
    {
      this.year_Txt.text = recordHolder.Year.ToString();
      this.team_Txt.text = recordHolder.RecordHolderTeam;
      this.player_Txt.text = recordHolder.RecordHolderName;
      this.record_Txt.text = recordHolder.RecordValue.ToString();
    }
    else
    {
      this.year_Txt.text = "-";
      this.team_Txt.text = "-";
      this.player_Txt.text = "-";
      this.record_Txt.text = "-";
    }
    this.value = recordHolder.RecordValue;
  }

  public void SetToDecimalDisplay()
  {
    if (this.doesRecordHaveAHolder)
      this.record_Txt.text = string.Format("{0:0.#}", (object) this.value);
    else
      this.record_Txt.text = "-";
  }

  public void SetToPercentageDisplay()
  {
    if (this.doesRecordHaveAHolder)
      this.record_Txt.text = (double) this.value == -1.0 ? "-" : Mathf.RoundToInt(this.value * 100f).ToString() + "%";
    else
      this.record_Txt.text = "-";
  }

  public void SetToABSDisplay()
  {
    if (this.doesRecordHaveAHolder)
      this.record_Txt.text = Mathf.Abs(this.value).ToString();
    else
      this.record_Txt.text = "-";
  }
}
