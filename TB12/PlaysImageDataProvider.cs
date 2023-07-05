// Decompiled with JetBrains decompiler
// Type: TB12.PlaysImageDataProvider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game;
using UnityEngine;

namespace TB12
{
  public class PlaysImageDataProvider : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private PlayImageItem _prefab;
    [SerializeField]
    private BaseFormation _overrideFormation;
    [SerializeField]
    private AxisPlaysStore _store;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._prefab;

    public BaseFormation OverrideFormation => this._overrideFormation;

    public AxisPlaysStore store => this._store;

    public int itemCount
    {
      get
      {
        FormationData formationData = this.OverrideFormation == BaseFormation.None ? PlaybookState.CurrentFormation.Value : this._store.GetFormationData(this.OverrideFormation, 0);
        return formationData == null ? 0 : formationData.GetNumberOfPlaysInFormation();
      }
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      PlayData play = formationData?.GetPlay(itemIndex);
      if (play == null)
      {
        Debug.LogError((object) string.Format("Null formation or play not found {0}, {1}", (object) (formationData != null), (object) itemIndex));
      }
      else
      {
        PlayImageItem playImageItem = (PlayImageItem) item;
        playImageItem.playData.SetValue(play);
        if (!((Object) playImageItem.spriteRenderer != (Object) null))
          return;
        this._store.SetupPlayImage(playImageItem.spriteRenderer, play);
        if (!MatchManager.Exists())
          return;
        playImageItem.SetAudibleText(MatchManager.instance.playManager.canUserCallAudible);
      }
    }
  }
}
