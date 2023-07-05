// Decompiled with JetBrains decompiler
// Type: JoinMultiplayerLoadingPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.Collections;
using TMPro;
using UnityEngine;

public class JoinMultiplayerLoadingPage : TabletPage
{
  [SerializeField]
  private GameObject _multiplayerManagerPrefab;
  [SerializeField]
  private TMP_Text _loadingLabel;
  [SerializeField]
  private LoadingImage _loadingImage;
  [SerializeField]
  private string _defaultJoiningRoomText = "Joining Room...";
  private readonly RoutineHandle _routineFindRoom = new RoutineHandle();
  private readonly RoutineHandle _routineJoinRoom = new RoutineHandle();
  private const float NET_TIMEOUT = 10f;

  private void Awake() => this._pageType = TabletPage.Pages.JoinMultiplayerLoading;

  private void OnEnable()
  {
    this._loadingLabel.text = this._defaultJoiningRoomText;
    this._routineFindRoom.Run(this.HandleConnectionState(Time.time + 10f));
  }

  private void OnDisable()
  {
    if ((Object) this._loadingImage != (Object) null)
      this._loadingImage.StopSpinning();
    this._routineFindRoom.Stop();
    this._routineJoinRoom.Stop();
  }

  private IEnumerator HandleConnectionState(float timeout)
  {
    yield return (object) null;
    yield return (object) null;
  }

  private void OnDestroy()
  {
    if ((Object) this._loadingImage != (Object) null)
      this._loadingImage.StopSpinning();
    this._routineFindRoom.Stop();
    this._routineJoinRoom.Stop();
  }
}
