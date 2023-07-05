// Decompiled with JetBrains decompiler
// Type: TB12.PlayModeStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(fileName = "PlayModeStore", menuName = "TB12/Stores/PlayModeStore", order = 1)]
  [AppStore]
  public class PlayModeStore : ScriptableObject
  {
    public float distance;
    public float speed;
    public List<Transform> Receivers = new List<Transform>();
    public Transform Center;
    public bool Touchdown;
    public EPlayResult Result;
    private const float yardLineStart = 50f;
    private float _yardLine;
    public float ballFlightTime;

    public float yardLine
    {
      get => this._yardLine;
      set
      {
        this._yardLine = value;
        this.Touchdown = (double) this._yardLine > 85.0;
      }
    }

    private void OnEnable() => this.Clear();

    public void Clear()
    {
      this.Center = (Transform) null;
      this.Receivers.Clear();
      this.distance = 0.0f;
      this.speed = 0.0f;
      this.Touchdown = false;
    }

    public void ResetYardLine() => this.yardLine = 50f;
  }
}
