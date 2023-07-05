// Decompiled with JetBrains decompiler
// Type: FootballVR.ForwardHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Locomotion/ForwardHandler")]
  public class ForwardHandler : ScriptableObject
  {
    [SerializeField]
    private HeadTiltHandler _tiltHandler;
    [SerializeField]
    private GameObject _debugPrefab;
    private ForwardSettings _settings;
    private GamePlayerController _player;
    private Transform _cameraTx;
    private Transform _bodyTx;
    private bool _initialized;
    private bool _state;
    private GameObject _debugForwardGo;
    private ForwardHandler.ControllerData[] _controllerDatas;
    private Vector3 _swingDirection;
    private readonly RoutineHandle _updateRoutine = new RoutineHandle();

    public Vector3 ForwardDirection { get; private set; }

    private void OnEnable()
    {
      this._state = false;
      this._updateRoutine.Stop();
      this.Deinitialize();
    }

    private void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      this._player = PersistentSingleton<GamePlayerController>.Instance;
      this._cameraTx = PlayerCamera.Camera.transform;
      Vector3 forward = this._cameraTx.forward;
      this._swingDirection = forward.SetY(0.0f).normalized;
      Transform transform = this._player.Rig.transform;
      this._controllerDatas = new ForwardHandler.ControllerData[2]
      {
        new ForwardHandler.ControllerData(this._player.Rig.rightHandAnchor, transform, forward),
        new ForwardHandler.ControllerData(this._player.Rig.leftHandAnchor, transform, forward)
      };
      this._settings = ScriptableSingleton<LocomotionSettings>.Instance.ForwardSettings;
      this._settings.FramesTracked.OnValueChanged += new Action<int>(this.HandleFramesTrackedChanged);
      this.HandleFramesTrackedChanged((int) this._settings.FramesTracked);
      this._settings.ForwardDirection.OnValueChanged += new Action<EForwardDirection>(this.HandleForwardType);
      this.HandleForwardType((EForwardDirection) (Variable<EForwardDirection>) this._settings.ForwardDirection);
      this._settings.DebugForward.OnValueChanged += new Action<bool>(this.HandleDebugForward);
      this.HandleDebugForward((bool) this._settings.DebugForward);
      this._updateRoutine.Run(this.UpdateRoutine());
    }

    private void HandleFramesTrackedChanged(int count)
    {
      for (int index = 0; index < 2; ++index)
        this._controllerDatas[index].SetFramesTracked(count, this._cameraTx.forward.SetY(0.0f));
    }

    public void Deinitialize()
    {
      if (!this._initialized)
        return;
      this._initialized = false;
      this._settings.FramesTracked.OnValueChanged -= new Action<int>(this.HandleFramesTrackedChanged);
      this._settings.ForwardDirection.OnValueChanged -= new Action<EForwardDirection>(this.HandleForwardType);
      this._settings.DebugForward.OnValueChanged -= new Action<bool>(this.HandleDebugForward);
      this.SetState(false);
    }

    public void SetState(bool state)
    {
      if (this._state == state)
        return;
      this._state = state;
      if (this._state)
        this.Initialize();
      if (this._state)
        return;
      this._updateRoutine.Stop();
    }

    private void HandleForwardType(EForwardDirection dirType)
    {
      this._bodyTx = this._cameraTx;
      this.ForwardDirection = this.GetForwardDirection((EForwardDirection) (Variable<EForwardDirection>) this._settings.ForwardDirection);
    }

    private IEnumerator UpdateRoutine()
    {
      while (true)
      {
        Vector3 camForw = this._cameraTx.forward.SetY(0.0f);
        for (int index = 0; index < 2; ++index)
          this._controllerDatas[index].Update(camForw, this._settings);
        this.ForwardDirection = Vector3.Lerp(this.ForwardDirection, this.GetForwardDirection((EForwardDirection) (Variable<EForwardDirection>) this._settings.ForwardDirection), (float) this._settings.ForwardLerpCoef).normalized;
        if ((bool) this._settings.DebugForward)
          this._debugForwardGo.transform.SetPositionAndRotation(this._cameraTx.position.SetY(0.0f) + this.ForwardDirection * 0.5f, Quaternion.LookRotation(this.ForwardDirection, Vector3.up));
        yield return (object) null;
      }
    }

    private Vector3 GetForwardDirection(EForwardDirection type)
    {
      switch (type)
      {
        case EForwardDirection.Composite:
          return Vector3.Lerp(this.GetForwardDirection(EForwardDirection.Head), this.GetForwardDirection(EForwardDirection.SwingDirection), this._settings.lerpWithSwingDirFactor);
        case EForwardDirection.SwingDirection:
          Vector3 vec3 = Vector3.zero;
          Vector3 vector3_1;
          for (int index = 0; index < 2; ++index)
          {
            Vector3 vector3_2 = vec3;
            vector3_1 = this._controllerDatas[index].GetSwingDirection();
            Vector3 normalized = vector3_1.normalized;
            vec3 = vector3_2 + normalized;
          }
          Vector3 swingDirection = this._swingDirection;
          vector3_1 = vec3.SetY(0.0f);
          Vector3 normalized1 = vector3_1.normalized;
          double swingSumLerpFactor = (double) this._settings.swingSumLerpFactor;
          vector3_1 = Vector3.Lerp(swingDirection, normalized1, (float) swingSumLerpFactor);
          this._swingDirection = vector3_1.normalized;
          return this._swingDirection;
        case EForwardDirection.ControllerPosition:
          Vector3 normalized2 = ((this._controllerDatas[0].position + this._controllerDatas[1].position) / 2f - this._cameraTx.position).SetY(0.0f).normalized;
          Vector3 normalized3 = this._cameraTx.forward.SetY(0.0f).normalized;
          if ((double) Vector3.Dot(normalized2, normalized3) < 0.0)
            normalized2 *= -1f;
          return normalized2;
        case EForwardDirection.LeanDirection:
          return this._tiltHandler.GetTiltDirection();
        default:
          return this._bodyTx.forward.SetY(0.0f).normalized;
      }
    }

    private void HandleDebugForward(bool state)
    {
      if (state && (UnityEngine.Object) this._debugForwardGo == (UnityEngine.Object) null)
      {
        this._debugForwardGo = UnityEngine.Object.Instantiate<GameObject>(this._debugPrefab);
        this._debugForwardGo.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
      }
      if (!((UnityEngine.Object) this._debugForwardGo != (UnityEngine.Object) null))
        return;
      this._debugForwardGo.SetActive(state);
    }

    private class ControllerData
    {
      private readonly Transform transform;
      private readonly Transform _rigTx;
      private Vector3 prevPos;
      private readonly Vector3Cache swingCache = new Vector3Cache(24);

      private Vector3 swingDirection { get; set; }

      public Vector3 position => this.transform.position;

      public ControllerData(Transform tx, Transform rigTx, Vector3 startValue)
      {
        startValue.y = 0.0f;
        this.transform = tx;
        this._rigTx = rigTx;
        this.swingCache.FillValues(startValue);
        this.swingDirection = startValue;
      }

      public void Update(Vector3 camForw, ForwardSettings settings)
      {
        Vector3 vector3_1 = this._rigTx.InverseTransformPoint(this.transform.position);
        Vector3 vector3_2 = this._rigTx.TransformVector(vector3_1 - this.prevPos).SetY(0.0f);
        if ((double) Vector3.Dot(settings.useCamForw ? camForw : this.swingDirection, vector3_2) < 0.0)
          vector3_2 *= -1f;
        else if (settings.ignoreRecoverySwipes)
        {
          this.prevPos = vector3_1;
          return;
        }
        if ((double) vector3_2.magnitude < (double) settings.minStep)
          return;
        vector3_2 = Vector3.Lerp(this.swingDirection, vector3_2, settings.swingLerpFactor);
        this.prevPos = vector3_1;
        this.swingCache.PushValue(vector3_2);
      }

      public Vector3 GetSwingDirection()
      {
        this.swingDirection = this.swingCache.AverageValue();
        return this.swingDirection;
      }

      public void SetFramesTracked(int count, Vector3 defaultVector)
      {
        this.swingCache.SetSize(count);
        this.swingCache.PushValue(defaultVector);
      }
    }
  }
}
