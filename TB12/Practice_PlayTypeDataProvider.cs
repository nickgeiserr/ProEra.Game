// Decompiled with JetBrains decompiler
// Type: TB12.Practice_PlayTypeDataProvider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using UnityEngine;

namespace TB12
{
  public class Practice_PlayTypeDataProvider : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private PracticePlaysStore _store;
    [SerializeField]
    private CircularTextItem _prefab;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._prefab;

    public int itemCount => this._store.BaseFormationsCount;

    public void SetupItem(int itemIndex, CircularLayoutItem item) => ((CircularTextItem) item).localizationText = this._store.GetBaseFormationName(itemIndex);
  }
}
