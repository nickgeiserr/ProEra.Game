// Decompiled with JetBrains decompiler
// Type: ProEra.WhiteboardKeysToTheGame
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace ProEra
{
  public class WhiteboardKeysToTheGame : MonoBehaviour
  {
    [SerializeField]
    private StatTracker m_keyToTheGameTracker;

    private void Start() => SeasonModeManager.self.OnInitComplete += new System.Action(this.Initialize);

    [ContextMenu("1234")]
    private void Initialize()
    {
      TeamDataStore[] teamData = SeasonTeamDataHolder.GetTeamData();
      SeasonModeManager self = SeasonModeManager.self;
      int teamOpponentForWeek = self.GetTeamOpponentForWeek(self.userTeamData.TeamIndex, PersistentData.seasonWeek, out int _, out int _);
      if (teamOpponentForWeek == -1)
        return;
      TeamDataStore teamDataStore = teamData[teamOpponentForWeek];
      for (int index = 0; index < teamDataStore.KeysToGame.Length; ++index)
        this.m_keyToTheGameTracker.SetStatByName<string>(string.Format("key_to_the_game_{0}", (object) index), teamDataStore.KeysToGame[index]);
    }

    private void OnDestroy() => SeasonModeManager.self.OnInitComplete -= new System.Action(this.Initialize);
  }
}
