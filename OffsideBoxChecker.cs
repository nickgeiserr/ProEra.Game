// Decompiled with JetBrains decompiler
// Type: OffsideBoxChecker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using TB12;
using UnityEngine;

public class OffsideBoxChecker : MonoBehaviour
{
  [SerializeField]
  private EOffsideCheck _restrictedTeam;
  [SerializeField]
  private MultiplayerStore _multiplayerStore;
  private bool _markAsOut;
  private int _hitPlayerID = -1;

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log((object) other.gameObject);
    PlayerSphereOfInfluence component;
    if (!other.gameObject.TryGetComponent<PlayerSphereOfInfluence>(out component))
      return;
    this._hitPlayerID = component.GetPlayerID();
    switch (this._restrictedTeam)
    {
      case EOffsideCheck.Any:
        this._markAsOut = true;
        break;
      case EOffsideCheck.TeamOne:
        this._markAsOut = this._multiplayerStore.GetOrCreatePlayerData(this._hitPlayerID).onTeamOne;
        break;
      case EOffsideCheck.TeamTwo:
        this._markAsOut = !this._multiplayerStore.GetOrCreatePlayerData(this._hitPlayerID).onTeamOne;
        break;
      default:
        this._markAsOut = false;
        break;
    }
    if (!this._markAsOut)
      return;
    MultiplayerEvents.BallHitPlayerMaster.Trigger(-1, this._hitPlayerID, true);
    this._markAsOut = false;
  }
}
