// Decompiled with JetBrains decompiler
// Type: FootballVR.BallsContainerNetworked
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR
{
  [RequireComponent(typeof (PhotonView))]
  public class BallsContainerNetworked : BallsContainer
  {
    [SerializeField]
    private float _delayBeforeSpawn = 5f;
    [SerializeField]
    private Image _timerImage;
    [SerializeField]
    private Renderer _ballRenderer;
    [SerializeField]
    private bool _ballAvailable = true;
    [SerializeField]
    private SpawnEffect _spawnEffect;
    private PhotonView _view;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    protected override void Awake()
    {
      base.Awake();
      this._view = PhotonView.Get((Component) this);
      this._spawnEffect.Initialize();
    }

    private void Start() => this._spawnEffect.Visible = true;

    public override BallObject SpawnBall(Vector3 pos)
    {
      if (!this._ballAvailable)
        return (BallObject) null;
      BallObject ballObject = base.SpawnBall(pos);
      if (!((Object) ballObject != (Object) null))
        return ballObject;
      this._view.RPC("StartDelayTimer", RpcTarget.AllViaServer);
      return ballObject;
    }

    [PunRPC]
    public void StartDelayTimer() => this._routineHandle.Run(this.StartDelayTimeRoutine());

    private IEnumerator StartDelayTimeRoutine()
    {
      float normalizedTime = 0.0f;
      this._ballAvailable = false;
      this._timerImage.enabled = true;
      this._ballRenderer.enabled = false;
      while ((double) normalizedTime <= 1.0)
      {
        if ((Object) this._timerImage == (Object) null)
        {
          yield break;
        }
        else
        {
          this._timerImage.fillAmount = normalizedTime;
          normalizedTime += Time.deltaTime / this._delayBeforeSpawn;
          yield return (object) null;
        }
      }
      this._ballAvailable = true;
      this._timerImage.enabled = false;
      this._ballRenderer.enabled = true;
    }

    public void SetDelayTime(float newDelayTime) => this._delayBeforeSpawn = newDelayTime;
  }
}
