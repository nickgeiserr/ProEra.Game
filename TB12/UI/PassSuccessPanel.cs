// Decompiled with JetBrains decompiler
// Type: TB12.UI.PassSuccessPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class PassSuccessPanel : SuccessPanel
  {
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private TextMeshProUGUI _statsText;

    public override bool canContinue => this._gameplayStore.PassSuccesses > 0;

    protected override void WillAppear() => this._statsText.text = string.Format("{0} / {1}", (object) this._gameplayStore.PassSuccesses, (object) this._gameplayStore.PassAttempts);
  }
}
