// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowDebug
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;

namespace FootballVR
{
  public class ThrowDebug : MonoBehaviour
  {
    [SerializeField]
    private BallThrowMechanic _throwMechanic;
    [SerializeField]
    private LineRenderer _linePrefab;
    private LineRenderer _predictionLine;
    private LineRenderer _positionLine;
    private LineRenderer _throwDirLine;
    private readonly Vector3[] _predictPositions = new Vector3[2];
    private BallObject _previousBall;
    private ThrowDebugSettings _settings;

    private ThrowSettings _throwSettings => ScriptableSingleton<ThrowSettings>.Instance;

    private void Awake()
    {
      this._settings = this._throwSettings.DebugSettings;
      this._throwMechanic.OnBallReleased += new Action<BallObject>(this.HandleBallReleased);
      this._settings.ShowThrowDebug.OnValueChanged += new Action<bool>(this.HandleShowThrowDebug);
      this.HandleShowThrowDebug((bool) this._settings.ShowThrowDebug);
    }

    private void HandleShowThrowDebug(bool show)
    {
      this.enabled = show;
      if ((UnityEngine.Object) this._predictionLine == (UnityEngine.Object) null)
      {
        if (!show)
          return;
        this._predictionLine = UnityEngine.Object.Instantiate<LineRenderer>(this._linePrefab, this.transform);
        this._positionLine = UnityEngine.Object.Instantiate<LineRenderer>(this._linePrefab, this.transform);
        this._positionLine.startColor = Color.blue;
        this._positionLine.endColor = Color.blue;
        this._throwDirLine = UnityEngine.Object.Instantiate<LineRenderer>(this._linePrefab, (Transform) null);
        this._throwDirLine.startColor = Color.green;
        this._throwDirLine.endColor = Color.green;
      }
      if (!show && (UnityEngine.Object) this._previousBall != (UnityEngine.Object) null)
        this._previousBall.Graphics.HideTrail(0.5f);
      this._predictionLine.gameObject.SetActive(show);
      this._positionLine.gameObject.SetActive(show);
      this._throwDirLine.gameObject.SetActive(show);
    }

    private void HandleBallReleased(BallObject ball)
    {
      if (!(bool) this._settings.ShowThrowDebug)
        return;
      this._throwMechanic.Update();
      Vector3Cache positionCacheV2 = this._throwMechanic.positionCacheV2;
      int length = Mathf.Min(positionCacheV2.Count, 8) + 1;
      Vector3[] positions = new Vector3[length];
      for (int index = 0; index < length - 1; ++index)
        positions[index] = positionCacheV2[positionCacheV2.Count - length + index];
      Vector3 position = ball.transform.position;
      positions[length - 1] = position;
      this._positionLine.positionCount = length;
      this._positionLine.SetPositions(positions);
      this._positionLine.useWorldSpace = true;
      Vector3 vector3 = this._throwMechanic.AdjustThrowVector(this._throwMechanic.GetThrowVector(this._throwSettings.ThrowConfig.ThrowVersion));
      Transform transform = this.transform;
      this._predictPositions[0] = transform.InverseTransformPoint(position);
      this._predictPositions[1] = transform.InverseTransformDirection(vector3);
      this._throwDirLine.transform.SetPositionAndRotation(transform.position, transform.rotation);
      this._throwDirLine.useWorldSpace = false;
      this._throwDirLine.SetPositions(this._predictPositions);
      if ((UnityEngine.Object) this._previousBall != (UnityEngine.Object) null)
        this._previousBall.Graphics.HideTrail(0.5f);
      this._previousBall = this._throwMechanic.ThrowComparisonBall(ball.transform.position, vector3, Color.magenta, false);
    }

    private void Update()
    {
      Vector3 direction = this._throwMechanic.AdjustThrowVector(this._throwMechanic.GetThrowVector(this._throwSettings.ThrowConfig.ThrowVersion));
      this._predictPositions[0] = Vector3.zero;
      this._predictPositions[1] = this.transform.InverseTransformDirection(direction);
      this._predictionLine.SetPositions(this._predictPositions);
    }
  }
}
