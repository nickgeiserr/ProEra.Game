// Decompiled with JetBrains decompiler
// Type: TB12.AxisGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using UnityEngine;

namespace TB12
{
  public class AxisGameScene : MonoBehaviour
  {
    [SerializeField]
    private Transform _underCenterSweetSpot;

    private void Start()
    {
    }

    public void SetSweetSpotPosition(Vector3 pos)
    {
      this._underCenterSweetSpot.position = pos;
      this._underCenterSweetSpot.rotation = Quaternion.identity;
      this._underCenterSweetSpot.gameObject.SetActive(true);
    }

    public void SetSweetSpotPosition(Transform trans, bool parent, Vector3 offset)
    {
      this._underCenterSweetSpot.position = trans.position;
      if (parent)
        this._underCenterSweetSpot.SetParent(trans);
      this._underCenterSweetSpot.position += offset;
      this._underCenterSweetSpot.localRotation = Quaternion.identity;
      this._underCenterSweetSpot.gameObject.SetActive(true);
    }

    public void DeactivateSweetSpot()
    {
      this._underCenterSweetSpot.gameObject.SetActive(false);
      this._underCenterSweetSpot.parent = (Transform) null;
    }

    public bool IsInSweetSpot(HandData hand) => this._underCenterSweetSpot.GetComponent<UnderCenterSweetSpot>().HasHand(hand);

    public bool BallInSweetSpot() => this._underCenterSweetSpot.GetComponent<UnderCenterSweetSpot>().HasBall();

    public bool SweetSpotIsActive() => this._underCenterSweetSpot.gameObject.activeInHierarchy;
  }
}
