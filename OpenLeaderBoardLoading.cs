// Decompiled with JetBrains decompiler
// Type: OpenLeaderBoardLoading
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.Collections;
using TMPro;
using UnityEngine;

public class OpenLeaderBoardLoading : TabletPage
{
  [SerializeField]
  private GameObject _multiplayerManagerPrefab;
  [SerializeField]
  private TMP_Text _loadingLabel;
  [SerializeField]
  private LoadingImage _loadingImage;
  [SerializeField]
  private string _defaultText = "Checking Online Availability...";
  private readonly RoutineHandle _routineCheckingAvailability = new RoutineHandle();
  private const float NET_TIMEOUT = 10f;

  private void Awake() => this._pageType = TabletPage.Pages.OpenLeaderBoardLoading;

  private void OnEnable()
  {
    this._loadingLabel.text = this._defaultText;
    this._routineCheckingAvailability.Run(this.HandleCheckingAvailability(Time.time + 10f));
  }

  private void OnDisable()
  {
    if ((Object) this._loadingImage != (Object) null)
      this._loadingImage.StopSpinning();
    this._routineCheckingAvailability.Stop();
  }

  private IEnumerator HandleCheckingAvailability(float timeout)
  {
    yield return (object) new WaitForSeconds(0.5f);
    yield return (object) null;
  }

  private void OnDestroy()
  {
    if ((Object) this._loadingImage != (Object) null)
      this._loadingImage.StopSpinning();
    this._routineCheckingAvailability.Stop();
  }
}
