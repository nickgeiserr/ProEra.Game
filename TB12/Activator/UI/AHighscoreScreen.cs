// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.AHighscoreScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12.Activator.UI
{
  public class AHighscoreScreen : UIView
  {
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public override Enum ViewId { get; } = (Enum) EScreens.kHighscore;

    protected override void WillAppear()
    {
      VariableInt score = this._gameplayStore.Score;
      ActivationState.Score = (int) score;
      this._scoreText.text = string.Format("{0}pts", (object) score);
      AppSounds.PlaySfx(ESfxTypes.kRoar);
    }
  }
}
