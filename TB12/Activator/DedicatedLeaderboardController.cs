// Decompiled with JetBrains decompiler
// Type: TB12.Activator.DedicatedLeaderboardController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TB12.Activator.Data;
using UnityEngine;

namespace TB12.Activator
{
  public class DedicatedLeaderboardController : MonoBehaviour
  {
    [SerializeField]
    private float _delay = 5f;
    [SerializeField]
    private ALeaderboardData _data;

    private IEnumerator Start()
    {
      while (true)
      {
        this._data.GetTop10();
        yield return (object) new WaitForSeconds(this._delay);
      }
    }
  }
}
