// Decompiled with JetBrains decompiler
// Type: MultiplayerPlayerList
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerPlayerList : MonoBehaviour
{
  public static MultiplayerPlayerList Instance;
  public GameObject playerListObject;
  public MultiplayerPlayerList.PlayerListItem[] playerItems;
  private bool bShouldBeOn;

  public void Awake()
  {
    if ((UnityEngine.Object) MultiplayerPlayerList.Instance == (UnityEngine.Object) null)
      MultiplayerPlayerList.Instance = this;
    else
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ToggleList(bool bOn) => this.bShouldBeOn = bOn;

  public void UpdateList()
  {
  }

  private void Update()
  {
  }

  [Serializable]
  public struct PlayerListItem
  {
    public GameObject gameObject;
    public TextMeshProUGUI PlayerName;
    public Image SpeakerIcon;
    public PlayerAvatarNetworked playerAvatar;
  }
}
