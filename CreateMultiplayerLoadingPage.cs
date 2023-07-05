// Decompiled with JetBrains decompiler
// Type: CreateMultiplayerLoadingPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CreateMultiplayerLoadingPage : TabletPage
{
  [SerializeField]
  private GameObject _multiplayerManagerPrefab;
  [SerializeField]
  private TMP_Text _loadingLabel;
  [SerializeField]
  private LoadingImage _loadingImage;
  [SerializeField]
  private string _defaultLoadingLabelText = "Creating room...";
  private readonly RoutineHandle _routineNetCheck = new RoutineHandle();
  private const float NET_TIMEOUT = 10f;

  private void Awake()
  {
    this._pageType = TabletPage.Pages.CreateMultiplayerLoading;
    if (!((UnityEngine.Object) this._loadingImage == (UnityEngine.Object) null))
      return;
    Console.Error.WriteLine("_loading Image is null in CreateMultiplayerLoadingPage. Please check the prefab");
  }

  private void OnEnable()
  {
    Console.WriteLine("CreateMultiplayerLoadingPage enabled!");
    this._loadingLabel.text = this._defaultLoadingLabelText;
    this._routineNetCheck.Run(this.HandleConnectionState(Time.time + 10f));
  }

  private void OnDisable()
  {
    if ((UnityEngine.Object) this._loadingImage != (UnityEngine.Object) null)
      this._loadingImage.StopSpinning();
    this._routineNetCheck.Stop();
  }

  private IEnumerator HandleConnectionState(float timeout)
  {
    yield return (object) null;
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._loadingImage != (UnityEngine.Object) null)
      this._loadingImage.StopSpinning();
    this._routineNetCheck.Stop();
  }
}
