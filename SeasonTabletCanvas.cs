// Decompiled with JetBrains decompiler
// Type: SeasonTabletCanvas
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class SeasonTabletCanvas : MonoBehaviour
{
  [SerializeField]
  private Transform _followParent;
  private bool _isFollowing = true;
  private const int FARAWAY_Y = -50;
  [SerializeField]
  private bool gameobjectActiveOnStart;
  private Vector3 defaultPosition;

  private void Start()
  {
    this.transform.parent = (Transform) null;
    this.defaultPosition = this.transform.position;
    if (this.gameobjectActiveOnStart)
      this.transform.position = this.defaultPosition;
    else
      this.transform.position = new Vector3(this.defaultPosition.x, -50f, this.defaultPosition.z);
  }

  private void LateUpdate()
  {
    if (!this._isFollowing)
      return;
    this.transform.SetPositionAndRotation(this._followParent.position, this._followParent.rotation);
  }

  public void EnableFollow(bool enable)
  {
    if (!enable && this._isFollowing)
      this.transform.position = new Vector3(0.0f, -50f, 0.0f);
    this._isFollowing = enable;
  }
}
