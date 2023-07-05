// Decompiled with JetBrains decompiler
// Type: TB12.UI.LevelsScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.UI;
using System;
using TB12.GameplayData;
using UnityEngine;

namespace TB12.UI
{
  public class LevelsScreen : UIView
  {
    [SerializeField]
    private LevelItem _itemPrefab;
    [SerializeField]
    private Transform _buttonsRoot;
    [SerializeField]
    private GameplayDataStore _gameplayDataStore;
    [SerializeField]
    private PlayerProgressStore _progressStore;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    private MonoBehaviorObjectPool<LevelItem> _itemsPool;

    public override Enum ViewId { get; } = (Enum) EScreens.kGameLevels;

    protected override void OnInitialize() => this._itemsPool = new MonoBehaviorObjectPool<LevelItem>(this._itemPrefab, this._buttonsRoot, 30);

    protected override void WillAppear()
    {
      foreach (GameLevel gameLevel in this._gameplayDataStore.GameplayLevels.GameLevels)
      {
        LevelItem levelItem = this._itemsPool.GetObject();
        bool locked = !this._progressStore.IsLevelAvailable(gameLevel.id) && !(bool) DeveloperMode.Activated;
        ProfileProgress.Entry data = this._progressStore.Progress.GetData(gameLevel.id);
        levelItem.Setup(gameLevel.id, locked, gameLevel.name, data != null ? data.Star : 0);
        levelItem.OnLevelSelected -= new Action<int>(this.HandleLevelSelected);
        levelItem.OnLevelSelected += new Action<int>(this.HandleLevelSelected);
        levelItem.transform.SetSiblingIndex(999);
      }
    }

    private void HandleLevelSelected(int id)
    {
      GameLevel level = this._gameplayDataStore.GameplayLevels.GameLevels.Find<GameLevel>((Predicate<GameLevel>) (x => x.id == id));
      if (level == null)
        Debug.LogError((object) string.Format("Level {0} not found. Load level failed.", (object) id));
      else
        this._levelsStore.SetLevel(level);
    }

    protected override void DidDisappear()
    {
      foreach (LevelItem allocatedObject in this._itemsPool.AllocatedObjects)
        allocatedObject.OnLevelSelected -= new Action<int>(this.HandleLevelSelected);
      this._itemsPool.ReturnAllObjects();
    }
  }
}
