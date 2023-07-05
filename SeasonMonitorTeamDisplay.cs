// Decompiled with JetBrains decompiler
// Type: SeasonMonitorTeamDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeasonMonitorTeamDisplay : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI _rank;
  [SerializeField]
  private TextMeshProUGUI _name;
  [SerializeField]
  private Image _logo;
  [SerializeField]
  private TextMeshProUGUI _rec;
  [SerializeField]
  private TextMeshProUGUI _div;
  [SerializeField]
  private TextMeshProUGUI _stk;

  public TextMeshProUGUI Rank => this._rank;

  public TextMeshProUGUI Name => this._name;

  public Image Logo => this._logo;

  public TextMeshProUGUI Rec => this._rec;

  public TextMeshProUGUI Div => this._div;

  public TextMeshProUGUI Stk => this._stk;

  private void Awake()
  {
  }
}
