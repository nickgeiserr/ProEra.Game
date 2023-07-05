// Decompiled with JetBrains decompiler
// Type: Axis.CustomPlaybook
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Axis
{
  public class CustomPlaybook
  {
    public List<FormationData> Playbook { get; set; }

    public string Name { get; set; }

    public bool IsOffense { get; set; }

    public CustomPlaybook(Dictionary<string, string> playbookLines)
    {
      this.Playbook = new List<FormationData>();
      this.Name = playbookLines[nameof (Name)].Length > 25 ? playbookLines[nameof (Name)].Substring(0, 25).Trim() : playbookLines[nameof (Name)].Trim();
      this.IsOffense = playbookLines["Type"].Equals("offense", StringComparison.OrdinalIgnoreCase);
      IEnumerable<KeyValuePair<string, string>> source = playbookLines.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (l => l.Key.StartsWith("Formation")));
      if (this.IsOffense && source.Count<KeyValuePair<string, string>>() < 1)
        throw new NotSupportedException("There were not enough formations specified for offense");
      if (!this.IsOffense && source.Count<KeyValuePair<string, string>>() < 2)
        throw new NotSupportedException("There were not enough formations specified for defense");
      this.CheckForDuplicates(source.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (l => l.Value)).ToArray<string>());
      if (this.IsOffense)
        this.Playbook.Add(Plays.self.goallinePlays_Heavy);
      else
        this.Playbook.Add(Plays.self.sixTwoPlays);
      foreach (KeyValuePair<string, string> keyValuePair in source)
      {
        foreach (string str in keyValuePair.Value.Split(',', StringSplitOptions.None))
        {
          if (this.IsOffense)
            this.Playbook.Add(this.ParseOffensiveSubFormation(str.Trim()));
          else
            this.Playbook.Add(this.ParseDefensiveSubFormation(str.Trim()));
        }
      }
      if (this.IsOffense)
      {
        this.Playbook.Add(Plays.self.hailMaryPlays_Normal);
        this.Playbook.Add(Plays.self.clockManagementPlays);
        this.Playbook.Add(Plays.self.specialOffPlays);
      }
      else
      {
        this.Playbook.Add(Plays.self.dimePlays);
        this.Playbook.Add(Plays.self.specialDefPlays);
      }
    }

    private FormationData ParseOffensiveSubFormation(string formation)
    {
      switch (formation)
      {
        case "EM01":
          return Plays.self.emptyPlays_TreyOpen;
        case "EM02":
          return Plays.self.emptyPlays_FlexTrips;
        case "IF00":
          return Plays.self.iFormPlays_Normal;
        case "IF01":
          return Plays.self.iFormPlays_Tight;
        case "IF02":
          return Plays.self.iFormPlays_SlotFlex;
        case "IF03":
          return Plays.self.iFormPlays_TwinTE;
        case "IF04":
          return Plays.self.iFormPlays_Twins;
        case "IF05":
          return Plays.self.iFormPlays_YTrips;
        case "IW00":
          return Plays.self.weakIPlays_CloseTwins;
        case "IW01":
          return Plays.self.weakIPlays_Normal;
        case "IW02":
          return Plays.self.weakIPlays_Twins;
        case "IW03":
          return Plays.self.weakIPlays_TwinsFlex;
        case "IW04":
          return Plays.self.weakIPlays_TwinTE;
        case "PI00":
          return Plays.self.pistolPlays_Ace;
        case "PI01":
          return Plays.self.pistolPlays_Bunch;
        case "PI02":
          return Plays.self.pistolPlays_Slot;
        case "PI03":
          return Plays.self.pistolPlays_SpreadFlex;
        case "PI04":
          return Plays.self.pistolPlays_TreyOpen;
        case "PI05":
          return Plays.self.pistolPlays_Trio;
        case "PI06":
          return Plays.self.pistolPlays_YTrips;
        case "SH00":
          return Plays.self.shotgunPlays_Normal;
        case "SH01":
          return Plays.self.shotgunPlays_NormalYFlex;
        case "SH02":
          return Plays.self.shotgunPlays_QuadsTrio;
        case "SH03":
          return Plays.self.shotgunPlays_SlotOffset;
        case "SH04":
          return Plays.self.shotgunPlays_SplitOffset;
        case "SH05":
          return Plays.self.shotgunPlays_Spread;
        case "SH06":
          return Plays.self.shotgunPlays_Tight;
        case "SH07":
          return Plays.self.shotgunPlays_Trey;
        case "SH08":
          return Plays.self.shotgunPlays_Trips;
        case "SH09":
          return Plays.self.shotgunPlays_Spread5WR;
        case "SH10":
          return Plays.self.shotgunPlays_Bunch5WR;
        case "SI00":
          return Plays.self.singleBackPlays_Big;
        case "SI01":
          return Plays.self.singleBackPlays_BigTwins;
        case "SI02":
          return Plays.self.singleBackPlays_Bunch;
        case "SI03":
          return Plays.self.singleBackPlays_Slot;
        case "SI04":
          return Plays.self.singleBackPlays_Spread;
        case "SI05":
          return Plays.self.singleBackPlays_TreyOpen;
        case "SI06":
          return Plays.self.singleBackPlays_Trio;
        case "SI07":
          return Plays.self.singleBackPlays_Trio4WR;
        case "ST00":
          return Plays.self.strongIPlays_Close;
        case "ST01":
          return Plays.self.strongIPlays_Normal;
        case "ST02":
          return Plays.self.strongIPlays_Tight;
        case "ST03":
          return Plays.self.strongIPlays_TwinTE;
        case "ST04":
          return Plays.self.strongIPlays_Twins;
        case "ST05":
          return Plays.self.strongIPlays_TwinsFlex;
        default:
          throw new NotSupportedException("Did not find a matching offensive sub-formation");
      }
    }

    private FormationData ParseDefensiveSubFormation(string formation)
    {
      switch (formation)
      {
        case "VT00":
          return Plays.self.fiveThreePlays;
        case "FF00":
          return Plays.self.fourFourPlays;
        case "FT00":
          return Plays.self.fourThreePlays;
        case "TF00":
          return Plays.self.threeFourPlays;
        case "NC00":
          return Plays.self.nickelPlays;
        default:
          throw new NotSupportedException("Did not find a matching defensive sub-formation");
      }
    }

    private void CheckForDuplicates(string[] lines)
    {
      HashSet<string> stringSet1 = new HashSet<string>();
      HashSet<string> stringSet2 = new HashSet<string>();
      foreach (string line in lines)
      {
        string[] strArray = line.Split(',', StringSplitOptions.None);
        string str1 = strArray[0].Substring(0, 2);
        if (!stringSet1.Add(str1))
          throw new NotSupportedException("Found two of the same formation across lines");
        foreach (string str2 in strArray)
        {
          if (!str2.Substring(0, 2).Equals(str1, StringComparison.OrdinalIgnoreCase))
            throw new NotSupportedException("Found two different prefixes within the same line");
          if (!stringSet2.Add(str2))
            throw new NotSupportedException("Found two of the same sub-formation within the same line");
        }
      }
    }
  }
}
