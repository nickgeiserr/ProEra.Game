// Decompiled with JetBrains decompiler
// Type: StandingSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class StandingSlot : MonoBehaviour
{
  [SerializeField]
  public Text teamName;
  [SerializeField]
  public Text wins;
  [SerializeField]
  public Text loses;
  [SerializeField]
  public Text margin;
  [SerializeField]
  private Transform trans;
  private int slot;
  private string team;
  private static int spacing = 35;
  private static int fontSize = 20;
  private static Color userColor = new Color(0.05490196f, 0.6666667f, 1f);

  public void SetSlot(int rank)
  {
    this.teamName.fontSize = StandingSlot.fontSize;
    this.wins.fontSize = StandingSlot.fontSize;
    this.loses.fontSize = StandingSlot.fontSize;
    this.margin.fontSize = StandingSlot.fontSize;
    this.slot = rank;
    float y;
    float x;
    if (this.slot < 16)
    {
      y = -50f - (float) (StandingSlot.spacing * this.slot);
      x = 10f;
    }
    else
    {
      y = -50f - (float) (StandingSlot.spacing * (this.slot - 16));
      x = 490f;
    }
    this.trans.localPosition = new Vector3(x, y, 0.0f);
    this.teamName.text = (this.slot + 1).ToString() + ". " + this.team;
  }

  public void SetTeamText(int teamIndex)
  {
    if (teamIndex == PersistentData.GetUserTeamIndex())
    {
      this.teamName.color = StandingSlot.userColor;
      this.wins.color = StandingSlot.userColor;
      this.loses.color = StandingSlot.userColor;
      this.margin.color = StandingSlot.userColor;
    }
    else
    {
      this.teamName.color = Color.white;
      this.wins.color = Color.white;
      this.loses.color = Color.white;
      this.margin.color = Color.white;
    }
  }

  public void SetValues(int w, int l, int plusMinus)
  {
    this.wins.text = w.ToString();
    this.loses.text = l.ToString();
    this.margin.text = plusMinus >= 0 ? "+" : "-";
    this.margin.text += Mathf.Abs(plusMinus).ToString();
  }
}
