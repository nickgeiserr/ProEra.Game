// Decompiled with JetBrains decompiler
// Type: LockerTvElement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockerTvElement : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI _teamNameText;
  [SerializeField]
  private TextMeshProUGUI _PlayerNameText;
  [SerializeField]
  private TextMeshProUGUI _teamRecordText;
  [SerializeField]
  private Image _logo;
  private Transform _transform;

  public TextMeshProUGUI TeamNameText => this._teamNameText;

  public TextMeshProUGUI PlayerNameText => this._PlayerNameText;

  public TextMeshProUGUI TeamRecordText => this._teamRecordText;

  public Image Logo => this._logo;

  public Transform ElementTransform => this._transform;

  private void Start() => this._transform = this.transform;
}
