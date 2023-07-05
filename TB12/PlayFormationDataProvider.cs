// Decompiled with JetBrains decompiler
// Type: TB12.PlayFormationDataProvider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game;
using TB12.UI;
using UnityEngine;
using Vars;

namespace TB12
{
  public class PlayFormationDataProvider : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private AxisPlaysStore _store;
    [SerializeField]
    private PlayFormationButton _buttonPrefab;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._buttonPrefab;

    public int itemCount => this._store.GetFormationsCount((BaseFormation) (Variable<BaseFormation>) PlaybookState.FormationBase);

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      PlayFormationButton playFormationButton = (PlayFormationButton) item;
      FormationData formationData = this._store.GetFormationData((BaseFormation) (Variable<BaseFormation>) PlaybookState.FormationBase, itemIndex);
      playFormationButton.Text = formationData.GetFormationType() == FormationType.OffSpecial ? "SPECIAL TEAMS" : formationData.GetSubFormationName();
      playFormationButton.SetPlayersInForm(formationData);
    }
  }
}
