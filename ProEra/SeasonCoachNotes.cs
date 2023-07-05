// Decompiled with JetBrains decompiler
// Type: ProEra.SeasonCoachNotes
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace ProEra
{
  public class SeasonCoachNotes : MonoBehaviour
  {
    [SerializeField]
    private CoachNotesObject _coachNotesBelow500;
    [SerializeField]
    private CoachNotesObject _coachNotesAbove500;
    [SerializeField]
    private CoachNotesObject _coachNotesLosePlayoff;
    [SerializeField]
    private CoachNotesObject _coachNotesLoseDivision;
    [SerializeField]
    private CoachNotesObject _coachNotesLoseSuper;
    [SerializeField]
    private CoachNotesObject _coachNotesWinSuper;
    private CoachNotesObject _currentNotes;
    [SerializeField]
    private LocalizeStringEvent _notesText;

    private void Start()
    {
      if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
        return;
      SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);
    }

    private void OnEnable()
    {
    }

    private void OnDisable() => Debug.Log((object) "disabled coaches notes");

    private void OnDestroy()
    {
      if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
        return;
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
    }

    [ContextMenu("Init")]
    private void Init()
    {
      CoachNotesObject notesBySeasonStat = this.GetNotesBySeasonStat();
      if (!((UnityEngine.Object) notesBySeasonStat != (UnityEngine.Object) null))
        return;
      this._notesText.StringReference.TableEntryReference = (TableEntryReference) notesBySeasonStat.coachNotesLocalizationKey;
    }

    private CoachNotesObject GetNotesBySeasonStat()
    {
      CoachNotesObject notesBySeasonStat = (CoachNotesObject) null;
      SeasonModeManager self = SeasonModeManager.self;
      switch (self.seasonModeData.seasonState)
      {
        case ProEraSeasonState.RegularSeason:
          notesBySeasonStat = ScriptableObject.CreateInstance<CoachNotesObject>();
          break;
        case ProEraSeasonState.DidNotMakePlayoffs:
          notesBySeasonStat = (double) self.seasonModeData.allTimeWins / (double) self.seasonModeData.allTimeLosses > 1.0 ? this._coachNotesAbove500 : this._coachNotesBelow500;
          break;
        case ProEraSeasonState.LostInPlayoffs:
          SeasonModeGameRound gameRound = self.GetGameRound(self.seasonModeData.currentWeek);
          notesBySeasonStat = (gameRound == SeasonModeGameRound.DivisionalRound ? 1 : (gameRound == SeasonModeGameRound.ConferenceChampionship ? 1 : 0)) != 0 ? this._coachNotesLoseDivision : this._coachNotesLosePlayoff;
          break;
        case ProEraSeasonState.LostInChampionship:
          notesBySeasonStat = this._coachNotesLoseSuper;
          break;
        case ProEraSeasonState.WonInChampionShip:
          notesBySeasonStat = this._coachNotesWinSuper;
          break;
      }
      return notesBySeasonStat;
    }

    public CoachNotesObject GetNotes(int a_noteIndex)
    {
      switch (a_noteIndex)
      {
        case 0:
          return this._coachNotesBelow500;
        case 1:
          return this._coachNotesAbove500;
        case 2:
          return this._coachNotesLosePlayoff;
        case 3:
          return this._coachNotesLoseDivision;
        case 4:
          return this._coachNotesLoseSuper;
        case 5:
          return this._coachNotesWinSuper;
        default:
          return (CoachNotesObject) null;
      }
    }
  }
}
