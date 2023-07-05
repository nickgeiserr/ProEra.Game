// Decompiled with JetBrains decompiler
// Type: TB12.UI.GameInfoUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using ProEra;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class GameInfoUI : MonoBehaviour
  {
    [SerializeField]
    private GameInfoDefaultUI _defaultUi;
    [SerializeField]
    private ScoreboardUI _scoreboardUi;
    [SerializeField]
    private GameScoreboardUI _gameScoreboardUi;
    [SerializeField]
    private bool _forceDefaultUi;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      if (AppState.Mode.Value == EMode.kMultiplayer)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      this.gameObject.SetActive(false);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<bool>((Variable<bool>) AppState.GameInfoUI, new Action<bool>(this.HandleState))
      });
      if ((UnityEngine.Object) this._defaultUi != (UnityEngine.Object) null)
        this._defaultUi.Initialize();
      else
        Debug.Log((object) ("ERROR! _defaultUi is null in GameInfoUI->Awake on _" + this.gameObject.name));
      if ((UnityEngine.Object) this._scoreboardUi != (UnityEngine.Object) null)
        this._scoreboardUi.Initialize();
      else
        Debug.Log((object) ("ERROR! _scoreboardUi is null in GameInfoUI->Awake on _" + this.gameObject.name));
    }

    private void OnEnable()
    {
      EGameMode gameMode = AppState.GameMode;
      if ((UnityEngine.Object) this._scoreboardUi != (UnityEngine.Object) null && !this._forceDefaultUi && (EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game && (gameMode == EGameMode.kThrow || gameMode == EGameMode.kAgility))
        this._scoreboardUi.Show();
      else if ((UnityEngine.Object) this._gameScoreboardUi != (UnityEngine.Object) null && !this._forceDefaultUi && (gameMode == EGameMode.kAxisGame || gameMode == EGameMode.kPracticeMode || gameMode == EGameMode.kOnboarding || gameMode == EGameMode.kAISimGameMode || gameMode == EGameMode.k2MD || gameMode == EGameMode.kHeroMoment || gameMode == EGameMode.kTunnel))
      {
        this._gameScoreboardUi.gameObject.SetActive(true);
      }
      else
      {
        if (!((UnityEngine.Object) this._defaultUi != (UnityEngine.Object) null))
          return;
        this._defaultUi.Show();
      }
    }

    private void OnDisable()
    {
      if ((UnityEngine.Object) this._defaultUi != (UnityEngine.Object) null)
        this._defaultUi.Hide();
      else
        Debug.Log((object) ("ERROR! _defaultUi is null in GameInfoUI->OnDisable on _" + this.gameObject.name));
      if ((UnityEngine.Object) this._scoreboardUi != (UnityEngine.Object) null)
      {
        this._scoreboardUi.Hide();
        this._gameScoreboardUi.gameObject.SetActive(false);
      }
      else
        Debug.Log((object) ("ERROR! _scoreboardUi is null in GameInfoUI->OnDisable on _" + this.gameObject.name));
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void HandleState(bool state) => this.gameObject.SetActive(state);
  }
}
