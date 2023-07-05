// Decompiled with JetBrains decompiler
// Type: FormationData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

public class FormationData
{
  private List<PlayData> plays;
  private string formationName;
  private FormationType formationType;
  private FormationPositions formPositions;

  public FormationData(FormationType t, FormationPositions p)
  {
    this.formationType = t;
    this.formPositions = p;
    this.plays = new List<PlayData>();
  }

  public FormationData(string n, FormationType t, FormationPositions p)
  {
    this.formationName = n;
    this.formationType = t;
    this.formPositions = p;
    this.plays = new List<PlayData>();
  }

  public void SetPlays(List<PlayData> p) => this.plays = p;

  public void AddPlay(PlayData p) => this.plays.Add(p);

  public List<PlayData> GetPlays() => this.plays;

  public PlayData GetPlay(int i) => i > this.plays.Count ? (PlayData) null : this.plays[i];

  public FormationPositions GetFormationPositions() => this.formPositions;

  public FormationPositionsDef GetFormationPositionsDef() => (FormationPositionsDef) this.formPositions;

  public FormationType GetFormationType() => this.formationType;

  public string GetName() => this.formationName != null ? this.formationName : this.GetFormationPositions().GetBaseFormationString();

  public string GetBaseFormationName() => this.GetFormationPositions().GetBaseFormationString();

  public string GetSubFormationName() => this.GetFormationPositions().GetSubFormationString();

  public int GetNumberOfPlaysInFormation() => this.plays.Count;

  public string[] GetPlayNames()
  {
    string[] playNames = new string[this.plays.Count];
    for (int index = 0; index < playNames.Length; ++index)
      playNames[index] = this.plays[index].GetPlayName();
    return playNames;
  }
}
