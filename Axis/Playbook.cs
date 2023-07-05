// Decompiled with JetBrains decompiler
// Type: Axis.Playbook
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Axis
{
  [CreateAssetMenu(fileName = "NewPlaybook", menuName = "Data/Playbook", order = 0)]
  public class Playbook : ScriptableObject
  {
    public bool PlayFlipped;
    public int LastFormationIndex;
    public FormationData LastFormationUsed;
    private FormationData _formation;
    private List<FormationData> _offense;
    private List<FormationData> _defense;
    private List<FormationData> _baseFormList;
    private List<int> _baseFormIndex;
    private List<int> _subFormCount;
    private List<int> _subFormIndex;
    private int numOfBaseFormations_Off;
    private int numOfBaseFormations_Def;
    [SerializeField]
    private Player _player;

    public int NumOfBaseFormationsOff => this.numOfBaseFormations_Off;

    public int NumOfBaseFormationsDef => this.numOfBaseFormations_Def;

    public int OffenseCount => this._offense.Count;

    public int DefenseCount => this._defense.Count;

    public List<FormationData> OffPlaybook => this._offense;

    public List<FormationData> DefPlaybook => this._defense;

    public List<int> SubFormationIndex => this._subFormIndex;

    public List<int> SubFormationCount => this._subFormCount;

    public FormationData Formation => this._formation;

    public bool IsFormationValid => this._formation != null;

    public void Init(Player player)
    {
      this._offense = Plays.self.GetOffPlaybookForPlayer(player);
      this._defense = Plays.self.GetDefPlaybookForPlayer(player);
      this.numOfBaseFormations_Off = 0;
      this.numOfBaseFormations_Def = this.DefenseCount;
      this._baseFormList = new List<FormationData>();
      this._baseFormIndex = new List<int>();
      this._subFormCount = new List<int>();
      this._subFormIndex = new List<int>();
      int num;
      for (int index1 = 0; index1 < this._offense.Count; index1 += num)
      {
        BaseFormation baseFormation = this._offense[index1].GetFormationPositions().GetBaseFormation();
        this._baseFormList.Add(this._offense[index1]);
        this._baseFormIndex.Add(index1);
        this._subFormIndex.Add(0);
        ++this.numOfBaseFormations_Off;
        num = 1;
        for (int index2 = index1 + 1; index2 < this._offense.Count && baseFormation == this._offense[index2].GetFormationPositions().GetBaseFormation(); ++index2)
          ++num;
        this._subFormCount.Add(num);
      }
      if (this.numOfBaseFormations_Off < 5)
        Debug.Log((object) "Offensive Playbook does not have the minimum number of base formations");
      this.LastFormationUsed = this._offense[0];
      this.LastFormationIndex = 0;
    }

    public void SelectOffFormation(int i)
    {
      this.PlayFlipped = false;
      if (i < this.OffPlaybook.Count - 1)
      {
        this._formation = this.OffPlaybook[i];
        this.LastFormationUsed = this._formation;
        this.LastFormationIndex = i;
      }
      else if (i == this.OffPlaybook.Count - 1)
      {
        this._formation = Plays.self.specialOffPlays;
      }
      else
      {
        if (i != this.OffenseCount)
          return;
        this._formation = Plays.self.kickoffPlays;
      }
    }

    public void SelectDefFormation(int i)
    {
      if (i < this.DefenseCount - 1)
      {
        this._formation = this.DefPlaybook[i];
        this.LastFormationUsed = this._formation;
        this.LastFormationIndex = i;
      }
      else if (i == this.DefenseCount - 1)
      {
        this._formation = Plays.self.specialDefPlays;
      }
      else
      {
        if (i != this.DefenseCount)
          return;
        this._formation = Plays.self.kickReturnPlays;
      }
    }

    public void SelectFormationByType(FormationType type)
    {
      switch (type)
      {
        case FormationType.OffSpecial:
          this._formation = Plays.self.specialOffPlays;
          break;
        case FormationType.DefSpecial:
          this._formation = Plays.self.specialDefPlays;
          break;
        case FormationType.Kickoff:
          this._formation = Plays.self.kickoffPlays;
          break;
        case FormationType.KickReturn:
          this._formation = Plays.self.kickReturnPlays;
          break;
      }
    }

    public List<FormationData> GetFormationList(bool onOffense) => !onOffense ? this._defense : this._baseFormList;

    public int GetSubFormationCount(int idx) => this._subFormCount[idx];

    public int GetOffFormationActualIndex(int i) => this._baseFormIndex[i] + this._subFormIndex[i];

    public string GetOffSubFormationName(int i) => string.Format("{0} ({1}/{2})", (object) this._offense[this.GetOffFormationActualIndex(i)].GetSubFormationName(), (object) (this._subFormIndex[i] + 1), (object) this._subFormCount[i]);

    public FormationData GetOffFormation(int actualIndex) => this._offense[actualIndex];

    public string GetFormationLargeName() => this._formation.GetFormationPositions().GetSubFormation() == SubFormation.None ? this._formation.GetName() : this._formation.GetFormationPositions().GetBaseFormationString() + ":  " + this._formation.GetSubFormationName();

    public int GetNumberOfPlaysInFormation() => this._formation.GetNumberOfPlaysInFormation();

    public FormationType GetFormationType() => this._formation != null ? this._formation.GetFormationType() : throw new NullReferenceException("Formation is null!");

    public bool IsKickoffOrKickReturnPlay()
    {
      if (this._formation == null)
        return false;
      FormationType formationType = this._formation.GetFormationType();
      return formationType == FormationType.Kickoff || formationType == FormationType.KickReturn;
    }

    public void ClearFormation() => this._formation = (FormationData) null;
  }
}
