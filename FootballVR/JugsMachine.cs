// Decompiled with JetBrains decompiler
// Type: FootballVR.JugsMachine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FootballVR
{
  public class JugsMachine : MonoBehaviour, IBallThrower
  {
    [SerializeField]
    private Transform _shootTransform;
    [SerializeField]
    private RectTransform _triangleTx;
    [SerializeField]
    private GameObject _highlight;
    private readonly RoutineHandle _throwRoutine = new RoutineHandle();

    public event Action<Transform, Vector3, float> OnBallThrown;

    public Vector3 position
    {
      get => this.transform.position;
      set => this.transform.position = value;
    }

    public bool isReady => true;

    public void ThrowToSpot(Vector3 targetPos, float flightTime, float throwDelay) => this._throwRoutine.Run(this.ThrowRoutine(targetPos, flightTime, throwDelay));

    private IEnumerator ThrowRoutine(Vector3 targetPos, float flightTime, float throwDelay)
    {
      yield return (object) new WaitForSeconds(throwDelay);
      Action<Transform, Vector3, float> onBallThrown = this.OnBallThrown;
      if (onBallThrown != null)
        onBallThrown(this._shootTransform, targetPos, flightTime);
    }

    public void Initialize(Vector3 targetPos, bool autoSpawnBall)
    {
      this.transform.LookAt(PersistentSingleton<GamePlayerController>.Instance.transform.position, Vector3.up);
      this._triangleTx.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float) ((double) (targetPos - this.transform.position).magnitude * 1000.0 / 3.0));
      this._highlight.SetActive(false);
    }

    public void SetHighlight(bool state) => this._highlight.SetActive(state);

    private void OnDisable() => this._throwRoutine.Stop();

    [SpecialName]
    Transform IBallThrower.get_transform() => this.transform;
  }
}
