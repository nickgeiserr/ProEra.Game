// Decompiled with JetBrains decompiler
// Type: MultiplayerLoadingPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Networked;
using Photon.Pun;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.Collections;
using TMPro;
using UnityEngine;

public class MultiplayerLoadingPage : TabletPage
{
  [SerializeField]
  private GameObject _multiplayerManagerPrefab;
  [SerializeField]
  private TMP_Text _loadingLabel;
  [SerializeField]
  private LoadingImage _loadingImage;
  private readonly RoutineHandle _routineNetCheck = new RoutineHandle();
  private const float NET_TIMEOUT = 10f;

  private void Awake() => this._pageType = TabletPage.Pages.MultiplayerLoading;

  private void OnEnable()
  {
    MultiplayerManager[] objectsOfType = Object.FindObjectsOfType<MultiplayerManager>();
    if (objectsOfType.Length != 0)
    {
      PhotonNetwork.Disconnect();
      foreach (Component component in objectsOfType)
        Object.Destroy((Object) component.gameObject);
      NetworkState.InstantiatedMultiplayerManager = (MultiplayerManager) null;
      NetworkState.Connected.SetValue(false);
    }
    if (Application.internetReachability == NetworkReachability.NotReachable)
    {
      NetworkState.CurrentNetworkStatus.SetValue(NetworkStatusCode.InternetNotAvailable);
      TransitionScreenController.CheckForNetworkMessages();
      (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);
    }
    else
      this._routineNetCheck.Run(this.HandleConnectionState(Time.time + 10f));
  }

  private void OnDisable()
  {
    if ((Object) this._loadingImage != (Object) null)
      this._loadingImage.StopSpinning();
    this._routineNetCheck.Stop();
  }

  private IEnumerator HandleConnectionState(float timeout)
  {
    MultiplayerLoadingPage multiplayerLoadingPage = this;
    if ((Object) multiplayerLoadingPage._loadingImage != (Object) null)
      multiplayerLoadingPage._loadingImage.StartSpinning();
    if ((Object) NetworkState.InstantiatedMultiplayerManager == (Object) null)
    {
      GameObject target = Object.Instantiate<GameObject>(multiplayerLoadingPage._multiplayerManagerPrefab);
      Object.DontDestroyOnLoad((Object) target);
      Debug.Log((object) "Created a new Multiplayer Manager");
      NetworkState.InstantiatedMultiplayerManager = target.GetComponent<MultiplayerManager>();
    }
    NetworkState.InstantiatedMultiplayerManager.ConnectToNetwork();
    bool isConnected = false;
    while (!isConnected && (double) Time.time < (double) timeout)
    {
      if ((bool) NetworkState.Connected)
      {
        Debug.Log((object) "Network connected");
        if ((Object) multiplayerLoadingPage._loadingImage != (Object) null)
          multiplayerLoadingPage._loadingImage.StopSpinning();
        (multiplayerLoadingPage.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MultiplayerMain);
        isConnected = true;
      }
      yield return (object) null;
    }
    if (!(bool) NetworkState.Connected)
    {
      Debug.Log((object) "Network connection FAIL");
      (multiplayerLoadingPage.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);
    }
    yield return (object) null;
  }

  private void OnDestroy()
  {
    if ((Object) this._loadingImage != (Object) null)
      this._loadingImage.StopSpinning();
    this._routineNetCheck.Stop();
  }
}
