// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowTestData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Throwing/TestData")]
  public class ThrowTestData : ScriptableObject
  {
    public List<CachedThrowData> throwDatas = new List<CachedThrowData>();

    public void StoreData(
      IReadOnlyList<Vector3> velocities,
      IReadOnlyList<Vector3> accelerations,
      IReadOnlyList<Vector3> positions,
      bool hasTarget,
      Vector3 autoAimVector)
    {
      this.throwDatas.Add(new CachedThrowData()
      {
        hasTarget = hasTarget,
        autoAimVector = autoAimVector,
        positions = new List<Vector3>((IEnumerable<Vector3>) positions),
        accelerations = new List<Vector3>((IEnumerable<Vector3>) accelerations),
        velocities = new List<Vector3>((IEnumerable<Vector3>) velocities)
      });
    }

    private void Reset() => this.throwDatas.Clear();
  }
}
