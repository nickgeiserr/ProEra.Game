// Decompiled with JetBrains decompiler
// Type: SidelineTeamAvatars
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System.Collections.Generic;
using UnityEngine;

public class SidelineTeamAvatars : MonoBehaviour
{
  [SerializeField]
  private bool IsPlayer;
  [SerializeField]
  private AvatarGraphics[] avatarsGraphics;

  private void Awake()
  {
    if (!((Object) MatchManager.instance != (Object) null))
      return;
    MatchManager.instance.playersManager.RegisterSidelineTeam(this, this.IsPlayer);
  }

  public void Refresh(
    RosterData mainRoster,
    int[] formationPlayerIds,
    Texture2D uniformTex,
    Texture2D[] uniformLabels)
  {
    List<int> intList = new List<int>();
    if (mainRoster != null)
    {
      for (int index1 = 0; index1 < mainRoster.numberOfPlayersOnRoster; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < formationPlayerIds.Length; ++index2)
        {
          if (index1 == formationPlayerIds[index2])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          intList.Add(index1);
      }
      int index3 = 0;
      if (this.avatarsGraphics == null)
        return;
      for (int index4 = 0; index4 < this.avatarsGraphics.Length; ++index4)
      {
        AvatarGraphics avatarsGraphic = this.avatarsGraphics[index4];
        if (!((Object) avatarsGraphic == (Object) null))
        {
          if (index3 >= intList.Count)
          {
            Debug.LogError((object) "Amount of sideline avatars is higher vs sideline players that should be");
            break;
          }
          PlayerData player = mainRoster.GetPlayer(intList[index3]);
          if (player != null)
          {
            avatarsGraphic.ConfigAvatar(player.AvatarID, player.PlayerPosition);
            avatarsGraphic.SetBasemap(uniformTex);
            avatarsGraphic.SetTextsMap(uniformLabels, intList[index3]);
            ++index3;
          }
          else
            Debug.LogError((object) "SidelineTeamAvatars : Refresh : playerData is null");
        }
      }
    }
    else
      Debug.LogError((object) "SidelineTeamAvatars : Refresh : mainRoster is null");
  }
}
