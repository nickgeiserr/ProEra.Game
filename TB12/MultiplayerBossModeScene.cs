// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerBossModeScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB12
{
  public class MultiplayerBossModeScene : MonoBehaviour, IMultiplayerScene
  {
    [SerializeField]
    private Transform _bossPosition;
    [SerializeField]
    private Transform _bossBucketPosition;
    [SerializeField]
    private Transform[] _positions;
    [SerializeField]
    private Transform[] _sidelinePositions;
    [SerializeField]
    private Transform[] _bucketPositions;

    public Transform bossPosition => this._bossPosition;

    public Transform bossBucketPosition => this._bossBucketPosition;

    public Transform[] positions
    {
      get => this._positions;
      set => this._positions = value;
    }

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

    public void SetBucketPositions(Transform[] bucketPositions)
    {
      List<Transform> list = ((IEnumerable<Transform>) bucketPositions).ToList<Transform>();
      list.Add(this.bossBucketPosition);
      this._bucketPositions = list.ToArray();
    }
  }
}
