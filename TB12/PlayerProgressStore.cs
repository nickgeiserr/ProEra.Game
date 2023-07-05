// Decompiled with JetBrains decompiler
// Type: TB12.PlayerProgressStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/PlayerProgressStore")]
  [AppStore]
  public class PlayerProgressStore : ScriptableObject
  {
    [SerializeField]
    private PlayerProfile _playerProfile;
    public ProfileProgress Progress = new ProfileProgress();
    private bool _initialized;

    public string PlayerName => this._playerProfile.PlayerLastName;

    private void OnEnable() => this.Initialize();

    public async void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      if (PersistentSingleton<SaveManager>.Exist())
        this.Progress = PersistentSingleton<SaveManager>.Instance.profileProgress;
      this.Progress.OnLoad();
    }

    public bool IsLevelAvailable(int levelId)
    {
      int maxLevelCompleted = this.Progress.MaxLevelCompleted;
      int num = maxLevelCompleted + (maxLevelCompleted % 6 == 5 ? 2 : 1);
      return levelId <= num;
    }

    public int StarsForChallenge(int levelId)
    {
      ProfileProgress.Entry entry;
      return !this.Progress.DataEntries.TryGetValue(levelId, out entry) ? 0 : entry.Star;
    }

    public void Apply(int levelId, int stars, int score)
    {
      this.Progress.Apply(levelId, stars, score);
      AppEvents.SaveProfileProgress.Trigger();
    }

    [ContextMenu("Reset progress")]
    public void ResetProgress()
    {
      if (!PersistentSingleton<SaveManager>.Exist())
        return;
      PersistentSingleton<SaveManager>.Instance.ResetPlayerProgress();
    }

    public void Clear()
    {
      this._initialized = false;
      this.Progress = new ProfileProgress();
    }
  }
}
