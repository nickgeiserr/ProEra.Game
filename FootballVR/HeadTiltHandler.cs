// Decompiled with JetBrains decompiler
// Type: FootballVR.HeadTiltHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Locomotion/HeadTiltHandler")]
  public class HeadTiltHandler : ScriptableObject
  {
    private HeadTiltSettings _settings;
    private Transform _cameraTx;
    private bool _initialized;
    private bool _state;
    private float _playerHeight = 1.5f;
    private float _playerCurrentHeight = 1.5f;
    private Vector3Cache _playerHeightCache;
    private readonly RoutineHandle _updateRoutine = new RoutineHandle();

    public Vector3 playerPos { get; private set; }

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
      this._cameraTx = PlayerCamera.Camera.transform;
      this.playerPos = this._cameraTx.localPosition;
      this._playerCurrentHeight = this.playerPos.y;
      this._playerHeight = this._playerCurrentHeight - 0.1f;
      this._playerHeightCache = new Vector3Cache(30);
      this._settings = ScriptableSingleton<LocomotionSettings>.Instance.LeanDetectionSettings;
      this._settings.AverageFrameCount.OnValueChanged += new Action<int>(this.HandleFrameCountChanged);
      this.HandleFrameCountChanged((int) this._settings.AverageFrameCount);
    }

    private void HandleFrameCountChanged(int size) => this._playerHeightCache.SetSize(size);

    public void Deinitialize()
    {
      if (!this._initialized)
        return;
      this._settings.AverageFrameCount.OnValueChanged -= new Action<int>(this.HandleFrameCountChanged);
      this._initialized = false;
      this.SetState(false);
    }

    public void SetState(bool state)
    {
      if (this._state == state)
        return;
      this._state = state;
      if (this._state)
      {
        this.Initialize();
        this._updateRoutine.Run(this.UpdateRoutine());
      }
      if (this._state)
        return;
      this._updateRoutine.Stop();
    }

    public float GetTiltAngle()
    {
      switch (this._settings.TiltMethod)
      {
        case ETiltMethod.HeadToDefaultPos:
          return Vector3.Angle(this._cameraTx.position - this._cameraTx.TransformPoint(this.playerPos).SetY(this._settings.DefaultPositionHeight), Vector3.up);
        default:
          return this._cameraTx.localRotation.eulerAngles.x;
      }
    }

    private IEnumerator UpdateRoutine()
    {
      float time = 0.0f;
      while (true)
      {
        Vector3 localPosition = this._cameraTx.localPosition;
        this._playerCurrentHeight = Mathf.Lerp(this._playerCurrentHeight, localPosition.y, this._settings.HeightLerpFactor);
        time += Time.deltaTime;
        if ((double) time > (double) this._settings.SampleTime)
        {
          time -= this._settings.SampleTime;
          this._playerHeightCache.PushValue(localPosition.SetY(this._playerCurrentHeight));
          float y = this._playerHeightCache.AverageValue().y;
          bool flag = (double) y > (double) this._playerHeight;
          this._playerHeight = Mathf.Lerp(this._playerHeight, y, flag ? this._settings.LerpUpFactor : this._settings.LerpDownFactor / 1000f);
        }
        if ((double) this._playerCurrentHeight > (double) this._playerHeight - (double) this._settings.HeightFlexibility)
          this.playerPos = localPosition;
        if ((bool) ScriptableSingleton<LocomotionSettings>.Instance.ShowDebug)
        {
          Vector3 lhs = this._cameraTx.position - this.playerPos;
          Vector3 rhs = this._cameraTx.forward.SetY(0.0f);
          Singleton<DebugUI>.Instance.leftTextUp.text = string.Format("{0:F}, h:{1:F}/{2}", (object) (lhs.magnitude * Mathf.Sign(Vector3.Dot(lhs, rhs))), (object) this._playerCurrentHeight, (object) this._playerHeight);
        }
        yield return (object) null;
      }
    }

    public Vector3 GetTiltDirection()
    {
      Vector3 vec3 = this._cameraTx.position - this.playerPos;
      if (vec3 == Vector3.zero)
        vec3 = this._cameraTx.forward;
      return vec3.SetY(0.0f).normalized;
    }
  }
}
