// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerThrowingScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12
{
  public class MultiplayerThrowingScene : MonoBehaviour, IMultiplayerScene
  {
    [SerializeField]
    private Transform[] _positions;
    [SerializeField]
    private MultiplayerRandomizedTargets _targetsControllerPrefab;
    [SerializeField]
    private Transform[] _bucketPositions;
    [SerializeField]
    private Transform[] _sidelinePositions;
    private MultiplayerRandomizedTargets _targetsController;

    public Transform[] sidelinePostions
    {
      get => this._sidelinePositions;
      set => this._sidelinePositions = value;
    }

    public Transform[] bucketPositions
    {
      get => this._bucketPositions;
      set => this._bucketPositions = value;
    }

    public Transform localSidelineTransform { get; set; }

    public Transform[] positions
    {
      get => this._positions;
      set => this._positions = value;
    }

    private void Awake()
    {
      this._targetsController = Object.Instantiate<MultiplayerRandomizedTargets>(this._targetsControllerPrefab, this.transform);
      this._targetsController.transform.ResetTransform();
    }

    public void LoadTargets(int groupId)
    {
      this._targetsController.LoadTargetGroup(groupId);
      this._targetsController.SetState(true);
    }

    public void HideTargets()
    {
      if (!((Object) this._targetsController != (Object) null))
        return;
      this._targetsController.SetState(false);
    }

    public int GetRandomGroupId() => this._targetsController.GetRandomTargetIndex();
  }
}
