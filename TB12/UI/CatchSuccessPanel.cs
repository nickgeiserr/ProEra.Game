// Decompiled with JetBrains decompiler
// Type: TB12.UI.CatchSuccessPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class CatchSuccessPanel : SuccessPanel
  {
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private TextMeshProUGUI _ballsCaughtText;

    public override bool canContinue => this._gameplayStore.BallsCaught > 0;

    protected override void WillAppear()
    {
      int ballsEmitted = this._gameplayStore.BallsEmitted;
      int ballsCaught = this._gameplayStore.BallsCaught;
      if (ballsEmitted == 0)
        return;
      this._ballsCaughtText.text = string.Format("{0}/{1}", (object) ballsCaught, (object) ballsEmitted);
    }
  }
}
