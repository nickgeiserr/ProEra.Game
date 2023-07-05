// Decompiled with JetBrains decompiler
// Type: TB12.Multiplayer.MultiplayerNotificationHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using Framework.Networked;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.Multiplayer
{
  public class MultiplayerNotificationHandler : MonoBehaviour
  {
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private NetworkReachability _loggedInternetStatus;
    private NetworkReachability _currentInternetStatus;

    private void Awake()
    {
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        NetworkState.CurrentNetworkStatus.Link<NetworkStatusCode>(new Action<NetworkStatusCode>(this.NotifyNetworkStatus)),
        AppEvents.NotifyNetworkStatus.Link(new Action(this.NotifyCurrentNetworkStatus))
      });
      this._currentInternetStatus = Application.internetReachability;
      this.CheckInternetReachability();
    }

    private void NotifyCurrentNetworkStatus() => this.NotifyNetworkStatus(NetworkState.CurrentNetworkStatus.Value);

    private void NotifyNetworkStatus(NetworkStatusCode code) => Debug.Log((object) ("NotifyNetworkStatus: code[" + code.ToString() + "] GetNetworkStatusMessage[" + NetworkState.GetNetworkStatusMessage((byte) code) + "]"));

    private void LateUpdate() => this.CheckInternetReachability();

    private void CheckInternetReachability()
    {
      this._currentInternetStatus = Application.internetReachability;
      if (this._loggedInternetStatus == this._currentInternetStatus)
        return;
      switch (this._currentInternetStatus)
      {
        case NetworkReachability.NotReachable:
          NetworkState.CurrentNetworkStatus.SetValue(NetworkStatusCode.InternetNotAvailable);
          break;
        case NetworkReachability.ReachableViaCarrierDataNetwork:
          NetworkState.CurrentNetworkStatus.SetValue(NetworkStatusCode.ConnectedToInternet);
          break;
        case NetworkReachability.ReachableViaLocalAreaNetwork:
          NetworkState.CurrentNetworkStatus.SetValue(NetworkStatusCode.ConnectedToInternet);
          break;
        default:
          NetworkState.CurrentNetworkStatus.SetValue(NetworkStatusCode.InternetNotAvailable);
          break;
      }
      this._loggedInternetStatus = this._currentInternetStatus;
    }
  }
}
