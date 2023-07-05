// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MiniCamp.RunAndShoot.RunAndShootLayout
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System.Collections;
using UnityEngine;

namespace TB12.AppStates.MiniCamp.RunAndShoot
{
  public class RunAndShootLayout : MonoBehaviour
  {
    [SerializeField]
    private Transform[] _bucketTransforms;
    [SerializeField]
    private GameObject _miniWall;
    [SerializeField]
    private GameObject _layoutContainer;
    private bool isActive;

    private void Awake()
    {
      foreach (SpawnEffect componentsInChild in this.GetComponentsInChildren<SpawnEffect>())
        componentsInChild.Initialize();
    }

    public void SetLayoutBucketActive(int ballCount = 5)
    {
      this._miniWall.SetActive(true);
      BallsContainerManager.CanSpawnBall.SetValue(true);
      PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(this._bucketTransforms, ballCount);
    }

    public void SetLayoutActive(bool active)
    {
      this.isActive = active;
      if (this.isActive)
      {
        this._layoutContainer.SetActive(true);
      }
      else
      {
        this.StartCoroutine(this.HideLayout());
        this._miniWall.SetActive(false);
      }
      foreach (SpawnEffect componentsInChild in this.GetComponentsInChildren<SpawnEffect>())
        componentsInChild.Visible = active;
    }

    private IEnumerator HideLayout()
    {
      yield return (object) new WaitForSeconds(3f);
      if (!this.isActive)
        this._layoutContainer.SetActive(false);
    }

    public bool TargetsAreActive() => (bool) (Object) this._layoutContainer && this._layoutContainer.activeSelf;

    public Vector3 GetMiniWallPosition() => !(bool) (Object) this._miniWall ? Vector3.zero : this._miniWall.transform.position;
  }
}
