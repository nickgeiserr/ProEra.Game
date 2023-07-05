// Decompiled with JetBrains decompiler
// Type: FootballVR.DampedTargetMover
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  public class DampedTargetMover : MonoBehaviour
  {
    [SerializeField]
    private float _smoothTime = 1f;
    [SerializeField]
    private float _offsetDelay = 0.2f;
    [SerializeField]
    private Vector3 _targetOffset = Vector3.zero;
    [SerializeField]
    private float _animTime = 2f;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform[] _transforms;
    [SerializeField]
    private bool _useCamTarget = true;
    private Vector3[] _velocities;
    private readonly RoutineHandle _adjustRoutine = new RoutineHandle();
    private bool _init;

    private VRSettings _settings => ScriptableSingleton<VRSettings>.Instance;

    private IEnumerator Start()
    {
      DampedTargetMover dampedTargetMover = this;
      yield return (object) new WaitForEndOfFrame();
      if (dampedTargetMover._useCamTarget && (UnityEngine.Object) dampedTargetMover._target == (UnityEngine.Object) null)
        dampedTargetMover._target = PersistentSingleton<PlayerCamera>.Instance.transform;
      dampedTargetMover._init = true;
      dampedTargetMover._velocities = new Vector3[dampedTargetMover._transforms.Length];
      dampedTargetMover._settings.UpdateUIHeight.OnValueChanged += new Action<bool>(dampedTargetMover.HandleUpdateUIHeight);
      dampedTargetMover.HandleUpdateUIHeight((bool) dampedTargetMover._settings.UpdateUIHeight);
      dampedTargetMover.UpdatePosition();
    }

    private void UpdatePosition()
    {
      Vector3 vector3 = this._target.position + this._targetOffset;
      this._adjustRoutine.Stop();
      int length = this._transforms.Length;
      for (int index = 0; index < length; ++index)
        this._transforms[index].localPosition = this._transforms[index].localPosition.SetY(vector3.y);
    }

    private void OnDestroy() => this._settings.UpdateUIHeight.OnValueChanged -= new Action<bool>(this.HandleUpdateUIHeight);

    private void HandleUpdateUIHeight(bool state)
    {
      this.enabled = state;
      if (this.enabled)
        return;
      this._adjustRoutine.Stop();
    }

    private void Update()
    {
      if (!this._init || (double) Mathf.Abs((this._target.position + this._targetOffset).y - this._transforms[0].localPosition.y) <= (double) this._offsetDelay)
        return;
      this._adjustRoutine.Run(this.PositionRoutine());
    }

    private IEnumerator PositionRoutine()
    {
      float destTime = Time.time + this._animTime;
      while ((double) Time.time < (double) destTime)
      {
        float y = (this._target.position + this._targetOffset).y;
        int length = this._transforms.Length;
        for (int index = 0; index < length; ++index)
        {
          Vector3 position;
          Vector3 target = (position = this._transforms[index].position) with
          {
            y = y
          };
          ref Vector3 local = ref this._velocities[index];
          double smoothTime = (double) this._smoothTime;
          Vector3 vector3 = Vector3.SmoothDamp(position, target, ref local, (float) smoothTime);
          this._transforms[index].position = vector3;
        }
        yield return (object) null;
      }
    }
  }
}
