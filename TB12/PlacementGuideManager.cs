// Decompiled with JetBrains decompiler
// Type: TB12.PlacementGuideManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace TB12
{
  public class PlacementGuideManager : MonoBehaviour
  {
    [SerializeField]
    public Transform _yardLineGuideTransform;
    [SerializeField]
    private Transform _scrimmageLineGuideTransform;

    public float YardLineGuidePosition => this._yardLineGuideTransform.GetPositionYards().z;

    public float YardLineGuideLocalPosition => this._yardLineGuideTransform.GetLocalPositionYards().z;

    public void SetYardLineGuideToYard(int yardPos) => this._yardLineGuideTransform.SetPositionZ(Field.GetFieldLocationByYardline(yardPos, true));

    public void SetHashGuide(float linePos) => this._scrimmageLineGuideTransform.SetPositionX(linePos);

    public int GetYardLine() => (int) this._yardLineGuideTransform.position.z;
  }
}
