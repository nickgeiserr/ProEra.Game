// Decompiled with JetBrains decompiler
// Type: FootballVR.TransitionScreenController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using Framework.Networked;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class TransitionScreenController : Singleton<TransitionScreenController>
  {
    [SerializeField]
    private TransitionScreenController.EditorTransitionStruct[] _editorTransitionEnvironments;
    [SerializeField]
    private LayerMask _transitionLayer;
    private Dictionary<TransitionScreenController.ETransitionType, TransitionEnvironment> _transitionEnvironments;
    private Camera _camera;
    private int _defaultLayer;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    public static readonly VariableBool Transitioning = new VariableBool();
    public static readonly VariableBool ShowingNetworkMessage = new VariableBool();
    private static TransitionScreenController.ETransitionType TransitionType;
    private const int NETWORK_MESSAGE_WAIT_TIME = 9;

    private void Awake()
    {
      this._transitionEnvironments = new Dictionary<TransitionScreenController.ETransitionType, TransitionEnvironment>();
      for (int index = 0; index < this._editorTransitionEnvironments.Length; ++index)
      {
        TransitionScreenController.EditorTransitionStruct transitionEnvironment = this._editorTransitionEnvironments[index];
        transitionEnvironment.env.gameObject.SetActive(false);
        this._transitionEnvironments.Add(transitionEnvironment.type, transitionEnvironment.env);
      }
      this._camera = PlayerCamera.Camera;
      if ((UnityEngine.Object) this._camera != (UnityEngine.Object) null)
        this._defaultLayer = this._camera.cullingMask;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        TransitionScreenController.Transitioning.Link<bool>(new Action<bool>(this.HandleTransition)),
        TransitionScreenController.ShowingNetworkMessage.Link<bool>(new Action<bool>(this.HandleNetworkMessage))
      });
    }

    public static void SetTransitionActive(
      TransitionScreenController.ETransitionType type,
      bool active)
    {
      Debug.Log((object) ("SetTransitionActive: " + active.ToString()));
      TransitionScreenController.TransitionType = type;
      TransitionScreenController.Transitioning.SetValue(active);
    }

    public static void CheckForNetworkMessages()
    {
      Debug.Log((object) nameof (CheckForNetworkMessages));
      if (!NetworkState.IsCurrentNetworkStatusError())
        return;
      TransitionScreenController.TransitionType = TransitionScreenController.ETransitionType.NetworkMessage;
      TransitionScreenController.Transitioning.SetValue(true);
      TransitionScreenController.ShowingNetworkMessage.SetValue(true);
    }

    private void HandleNetworkMessage(bool showing)
    {
      Debug.Log((object) nameof (HandleNetworkMessage));
      if (!showing)
        return;
      this.StartCoroutine(this.WaitToHideNetworkMessage());
    }

    private IEnumerator WaitToHideNetworkMessage()
    {
      Debug.Log((object) nameof (WaitToHideNetworkMessage));
      float startTime = Time.realtimeSinceStartup;
      while ((double) Time.realtimeSinceStartup < (double) startTime + 9.0)
        yield return (object) null;
      NetworkState.CurrentNetworkStatus.SetValue(NetworkStatusCode.None);
      TransitionScreenController.Transitioning.SetValue(false);
      TransitionScreenController.ShowingNetworkMessage.SetValue(false);
    }

    private void HandleTransition(bool active)
    {
      TransitionEnvironment transitionEnvironment = this._transitionEnvironments[TransitionScreenController.TransitionType];
      TransitionScreenController.Transitioning.SetValue(active);
      transitionEnvironment.gameObject.SetActive(active);
      switch (TransitionScreenController.TransitionType)
      {
        case TransitionScreenController.ETransitionType.Loading:
          ((LoadingEnvironment) transitionEnvironment).SetVisibility(active);
          break;
        case TransitionScreenController.ETransitionType.EndOfQuarter:
          transitionEnvironment.ShowQuarterEnd();
          break;
        case TransitionScreenController.ETransitionType.TwoMinuteWarning:
          transitionEnvironment.Show2MinWarning();
          break;
        case TransitionScreenController.ETransitionType.EndOfGame:
          transitionEnvironment.ShowGameOver();
          break;
        case TransitionScreenController.ETransitionType.ByeWeek:
          ((DebugTextEnvironment) transitionEnvironment).ShowText(TransitionScreenController.TransitionType);
          break;
        case TransitionScreenController.ETransitionType.DebugThrowing:
          ((DebugTextEnvironment) transitionEnvironment).ShowText(TransitionScreenController.TransitionType);
          break;
        case TransitionScreenController.ETransitionType.SeasonModeUpdate:
          transitionEnvironment.ShowSeasonModeUpdate();
          break;
        case TransitionScreenController.ETransitionType.NetworkMessage:
          ((DebugTextEnvironment) transitionEnvironment).ShowText(TransitionScreenController.TransitionType);
          break;
      }
      this._camera.cullingMask = active ? this._transitionLayer.value : this._defaultLayer;
    }

    private void OnDestroy() => this._linksHandler.Clear();

    public enum ETransitionType
    {
      Loading,
      EndOfQuarter,
      TwoMinuteWarning,
      EndOfGame,
      ByeWeek,
      DebugThrowing,
      SeasonModeUpdate,
      NetworkMessage,
    }

    [Serializable]
    private struct EditorTransitionStruct
    {
      public TransitionScreenController.ETransitionType type;
      public TransitionEnvironment env;
    }
  }
}
