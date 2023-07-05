// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerLobbyScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12
{
  public class MultiplayerLobbyScene : MonoBehaviour, IMultiplayerScene
  {
    [SerializeField]
    private Transform[] _positions;
    [SerializeField]
    private Transform[] _sidelinePositions;
    [SerializeField]
    private Transform[] _bucketPositions;

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
  }
}
