// Decompiled with JetBrains decompiler
// Type: FootballVR.BallsContainer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR
{
  public class BallsContainer : MonoBehaviour
  {
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private GameObject _ballsDisplay;
    [SerializeField]
    private bool _isStatusProBallContainer;
    [SerializeField]
    private Material _statusProBallMaterial;
    private Vector3 _defaultPos;
    private int _ballCount = -1;
    private MeshRenderer _ballDisplayRenderer;

    protected virtual void Awake()
    {
      this._ballDisplayRenderer = this._ballsDisplay.GetComponent<MeshRenderer>();
      this._defaultPos = this.transform.position;
    }

    public void SetIsStatusProBallContainer(bool val)
    {
      this._isStatusProBallContainer = val;
      if (!this._isStatusProBallContainer || !(bool) (Object) this._ballDisplayRenderer)
        return;
      this._ballDisplayRenderer.material = this._statusProBallMaterial;
    }

    public void SetColliderActive(bool enabled) => this._collider.enabled = enabled;

    public void ResetPosition() => this.transform.position = this._defaultPos;

    public virtual BallObject SpawnBall(Vector3 pos)
    {
      if (this._ballCount == 0)
        return (BallObject) null;
      --this._ballCount;
      this._ballsDisplay.SetActive(this._ballCount != 0);
      return PersistentSingleton<BallsContainerManager>.Instance.SpawnBall(pos, this._isStatusProBallContainer);
    }

    public void SetBallCount(int count)
    {
      this._ballCount = count;
      this._ballsDisplay.SetActive(count != 0);
    }
  }
}
