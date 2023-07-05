// Decompiled with JetBrains decompiler
// Type: TB12.UI.ThrowSuccessPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class ThrowSuccessPanel : SuccessPanel
  {
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private TextMeshProUGUI _accuracyText;
    [SerializeField]
    private TextMeshProUGUI _distanceText;
    [SerializeField]
    private TextMeshProUGUI _speedText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public override bool canContinue => (int) this._store.BallsHitTarget > 0;

    protected override void WillAppear()
    {
      this.gameObject.SetActive(true);
      if ((int) this._store.BallsThrown != 0)
        this._accuracyText.text = string.Format("{0}%", (object) ((int) this._store.BallsHitTarget * 100 / (int) this._store.BallsThrown));
      this._distanceText.text = this._store.BestDistance.ToString("0.00");
      this._speedText.text = (this._store.BestSpeed * 2.23694f).ToString("0.00");
      this._scoreText.text = this._store.Score.ToString();
    }
  }
}
