// Decompiled with JetBrains decompiler
// Type: TB12.UI.GameInfoUI_Lightweight
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using ProEra;
using UnityEngine;

namespace TB12.UI
{
  public class GameInfoUI_Lightweight : MonoBehaviour
  {
    [SerializeField]
    private GameScoreboardUI_Lightweight _scoreboardUI;
    [SerializeField]
    private bool _forceDefaultUi;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake() => Object.Destroy((Object) this.gameObject);

    private void OnEnable()
    {
      EGameMode gameMode = AppState.GameMode;
      if (!((Object) this._scoreboardUI != (Object) null) || this._forceDefaultUi || gameMode != EGameMode.kAxisGame && gameMode != EGameMode.kPracticeMode && gameMode != EGameMode.kOnboarding && gameMode != EGameMode.kAISimGameMode && gameMode != EGameMode.k2MD && gameMode != EGameMode.kHeroMoment)
        return;
      this._scoreboardUI.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
      if (!((Object) this._scoreboardUI != (Object) null))
        return;
      this._scoreboardUI.gameObject.SetActive(false);
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void HandleState(bool state) => this.gameObject.SetActive(state);
  }
}
