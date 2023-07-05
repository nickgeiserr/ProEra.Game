// Decompiled with JetBrains decompiler
// Type: PlayerDownChecker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PlayerDownChecker : MonoBehaviour
{
  private MatchManager _matchManager;
  private PlayersManager _playersManager;
  private PlayerAI _playerAI;

  public void Start()
  {
    this._matchManager = MatchManager.instance;
    if ((Object) this._matchManager != (Object) null)
      this._playersManager = this._matchManager.playersManager;
    this._playerAI = this.GetComponent<PlayerAI>();
  }

  private void OnTriggerEnter(Collider other) => this.CheckForPlayEnd(other);

  private void OnTriggerStay(Collider other) => this.CheckForPlayEnd(other);

  private void CheckForPlayEnd(Collider other)
  {
    if ((Object) this._playerAI == (Object) null || (Object) this._playersManager == (Object) null || !Game.IsPlayActive || !Game.IsBallHolder(other.gameObject) || this._playerAI.onOffense != Game.IsTurnover || !this._playersManager.ballHolderScript.IsAttemptingFumbleRecovery)
      return;
    this._matchManager.EndPlay(PlayEndType.Tackle);
  }
}
