// Decompiled with JetBrains decompiler
// Type: BossModeMapPositions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class BossModeMapPositions : MonoBehaviour
{
  [SerializeField]
  private Transform[] _playerPositions;
  [SerializeField]
  private Transform[] _bucketPositions;

  public bool TryGetPlayerPositions(int playerCount, out Transform[] positions)
  {
    if (this._playerPositions.Length >= playerCount)
    {
      positions = this._playerPositions;
      return true;
    }
    positions = (Transform[]) null;
    return false;
  }

  public bool TryGetBucketPositions(int bucketCount, out Transform[] positions)
  {
    if (this._bucketPositions.Length >= bucketCount)
    {
      positions = this._bucketPositions;
      return true;
    }
    positions = (Transform[]) null;
    return false;
  }
}
