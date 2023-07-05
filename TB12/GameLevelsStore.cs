// Decompiled with JetBrains decompiler
// Type: TB12.GameLevelsStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using TB12.GameplayData;
using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/GameLevelsStore", fileName = "GameLevelsStore")]
  [AppStore]
  public class GameLevelsStore : ScriptableObject
  {
    [SerializeField]
    private GameplayDataStore _gameplayDataStore;
    [SerializeField]
    private PlayerProgressStore _progressStore;
    private int _currentLevelIndex;

    public Variable<GameLevel> CurrentLevel { get; } = new Variable<GameLevel>();

    public GameLevel[] GameLevels => this._gameplayDataStore.GameplayLevels.GameLevels;

    private bool _initialized { get; set; }

    public void Initialize(bool force = false)
    {
      if (this._initialized && !force)
        return;
      this._initialized = true;
      if (this.GameLevels.Length < 1)
      {
        Debug.LogError((object) "Game Levels not loaded.");
      }
      else
      {
        this._progressStore.Initialize();
        if (this.TrySelectLevelByIndex(this._progressStore.Progress.MaxLevelCompleted + 1) || this.TrySelectLevelByIndex(this._progressStore.Progress.MaxLevelCompleted))
          return;
        GameLevel gameLevel = this.GameLevels[0];
        this._currentLevelIndex = 0;
        this.CurrentLevel.SetValue(gameLevel);
        if (gameLevel != null)
          return;
        Debug.LogError((object) "GameLevelsStore.Initialize: fallback (first) level was null");
      }
    }

    private bool TrySelectLevelByIndex(int levelId)
    {
      for (int index = 0; index < this.GameLevels.Length; ++index)
      {
        GameLevel gameLevel = this.GameLevels[index];
        if (gameLevel.id == levelId)
        {
          this._currentLevelIndex = index;
          this.CurrentLevel.SetValue(gameLevel);
          return true;
        }
      }
      return false;
    }

    public void PreviousLevel()
    {
      if (this._currentLevelIndex <= 0)
        return;
      this.TrySetLevel(this._currentLevelIndex - 1);
    }

    public void NextLevel()
    {
      if (this._currentLevelIndex >= this.GameLevels.Length - 1)
        return;
      this.TrySetLevel(this._currentLevelIndex + 1);
    }

    private void TrySetLevel(int levelIndex)
    {
      try
      {
        GameLevel gameLevel = this.GameLevels[levelIndex];
        if (gameLevel == null)
        {
          Debug.LogError((object) "Attempting to set null level..");
        }
        else
        {
          this.CurrentLevel.SetValue(gameLevel);
          this._currentLevelIndex = levelIndex;
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) string.Format("Failed to set level with index {0}.\r\nError: {1}", (object) levelIndex, (object) ex.Message));
      }
    }

    public void SetLevel(GameLevel level)
    {
      try
      {
        int num = Array.IndexOf<GameLevel>(this.GameLevels, level);
        if (num < 0)
          return;
        this._currentLevelIndex = num;
        this.CurrentLevel.SetValue(level);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex.Message);
      }
    }
  }
}
