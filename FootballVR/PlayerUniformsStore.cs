// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerUniformsStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "MultiplayerUniformsStore", menuName = "FootballVR/Data/MultiplayerUniformsStore", order = 1)]
  public class PlayerUniformsStore : ScriptableObject
  {
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private UniformStore _uniformStore;
    private readonly Dictionary<int, int> _indexToUserId = new Dictionary<int, int>();
    private readonly UniformCapture.Info info = new UniformCapture.Info();

    public void RegenerateUniformsForPlayer(int playerId)
    {
      int userIndex;
      if (!this.TryGetUserIndex(playerId, out userIndex))
        return;
      this.RegenerateTextures(userIndex / 11);
    }

    private void RegenerateTextures(int textureIndex)
    {
      int capacity = this.GetMaxUserIndex(textureIndex) + 1;
      List<string> stringList = new List<string>(capacity);
      List<int> intList = new List<int>(capacity);
      List<FootballWorld.UniformConfig> uniformConfigList = new List<FootballWorld.UniformConfig>(capacity);
      Dictionary<int, PlayerCustomization> profiles = this._playerProfile.Profiles;
      if (textureIndex < 0)
      {
        Debug.LogError((object) "Negative texture index value");
      }
      else
      {
        for (int index = 0; index < capacity; ++index)
        {
          int key;
          bool flag1 = this._indexToUserId.TryGetValue(index + textureIndex * 11, out key);
          PlayerCustomization playerCustomization = (PlayerCustomization) null;
          if (key == -1)
            playerCustomization = this._playerProfile.Customization;
          else if (flag1)
            profiles.TryGetValue(key, out playerCustomization);
          if (playerCustomization != null)
          {
            stringList.Add((string) playerCustomization.LastName);
            intList.Add((int) playerCustomization.UniformNumber);
            ETeamUniformFlags flag2 = playerCustomization.HomeUniform.Value ? ETeamUniformFlags.Home : ETeamUniformFlags.Away;
            FootballWorld.UniformConfig uniformConfig = this._uniformStore.GetUniformConfig((ETeamUniformId) (Variable<ETeamUniformId>) playerCustomization.Uniform, flag2);
            if (uniformConfig == null)
            {
              Debug.LogError((object) string.Format("Was unable to find {0}, using default (Ravens Home)", (object) playerCustomization.Uniform));
              uniformConfig = this._uniformStore.GetUniformConfig(ETeamUniformId.Ravens, ETeamUniformFlags.Home);
            }
            uniformConfigList.Add(uniformConfig);
          }
          else
          {
            stringList.Add(string.Empty);
            intList.Add(0);
            uniformConfigList.Add((FootballWorld.UniformConfig) null);
          }
        }
      }
    }

    private int GetMaxUserIndex(int textureIndex)
    {
      int maxUserIndex = 0;
      for (int index = 0; index < 11; ++index)
      {
        if (this._indexToUserId.TryGetValue(index + textureIndex * 11, out int _))
          maxUserIndex = index;
      }
      return maxUserIndex;
    }

    public UniformCapture.Info RegisterAvatar(int userId)
    {
      if (userId == -1)
        this.info.PlayerIndex = 0;
      else if (!this.TryGetUserIndex(userId, out this.info.PlayerIndex))
        this.info.PlayerIndex = this.GetFreeIndex();
      int textureIndex = this.info.PlayerIndex / 11;
      this._indexToUserId[this.info.PlayerIndex] = userId;
      this.RegenerateTextures(textureIndex);
      this.info.TextsAtlas = (Texture[]) UniformCapture.GetTextsTexture(textureIndex + 4, ETeamUniformFlags.Home);
      return this.info;
    }

    private int GetFreeIndex()
    {
      for (int key = 1; key < 100; ++key)
      {
        if (!this._indexToUserId.TryGetValue(key, out int _))
          return key;
      }
      Debug.LogError((object) "Failed to find a free uniform index.. something is very wrong");
      return 0;
    }

    public void UnregisterPlayer(int userId)
    {
      int userIndex;
      if (!this.TryGetUserIndex(userId, out userIndex))
        return;
      this._indexToUserId.Remove(userIndex);
    }

    private bool TryGetUserIndex(int userId, out int userIndex)
    {
      userIndex = -1;
      foreach (KeyValuePair<int, int> keyValuePair in this._indexToUserId)
      {
        if (keyValuePair.Value == userId)
        {
          userIndex = keyValuePair.Key;
          return true;
        }
      }
      return false;
    }
  }
}
