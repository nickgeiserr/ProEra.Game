// Decompiled with JetBrains decompiler
// Type: FormationPositions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class FormationPositions
{
  private float[] xLocations;
  private float[] zLocations;
  private int[] stances;
  private float[] motionPositions;
  private Position[] positions;
  private string personnel;
  private BaseFormation baseFormation;
  private SubFormation subFormation;
  private int defensiveFormationVariation;

  public FormationPositions(
    string _personnel,
    BaseFormation _baseFormation,
    SubFormation _subFormation,
    int _defensiveFormationVariation)
  {
    this.personnel = _personnel;
    this.baseFormation = _baseFormation;
    this.subFormation = _subFormation;
    this.defensiveFormationVariation = _defensiveFormationVariation;
  }

  public void SetXLocations(float[] x) => this.xLocations = x;

  public void SetZLocations(float[] z) => this.zLocations = z;

  public void SetStances(int[] s) => this.stances = s;

  public void SetMotionPositions(float[] s) => this.motionPositions = s;

  public void SetPositions(Position[] p) => this.positions = p;

  public int GetDefensiveFormationVariation() => this.defensiveFormationVariation;

  public float[] GetXLocations() => this.xLocations;

  public float[] GetZLocations() => this.zLocations;

  public int[] GetStances() => this.stances;

  public float[] GetMotionPositions() => this.motionPositions;

  public Position[] GetPositions() => this.positions;

  public Position GetPositions(int index) => this.positions[index];

  public BaseFormation GetBaseFormation() => this.baseFormation;

  public string GetBaseFormationString() => Common.EnumToString(this.baseFormation.ToString());

  public SubFormation GetSubFormation() => this.subFormation;

  public string GetSubFormationString() => this.subFormation == SubFormation.None ? "" : Common.EnumToString(this.subFormation.ToString());

  public int GetReceiversInFormation()
  {
    int receiversInFormation = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.WR || this.positions[index] == Position.SLT)
        ++receiversInFormation;
    }
    return receiversInFormation;
  }

  public int GetTEsInFormation()
  {
    int tesInFormation = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.TE)
        ++tesInFormation;
    }
    return tesInFormation;
  }

  public int GetBacksInFormation()
  {
    int backsInFormation = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.RB || this.positions[index] == Position.FB)
        ++backsInFormation;
    }
    return backsInFormation;
  }

  public bool IsQBUnderCenter() => (double) this.zLocations[5] > -3.0;

  public bool IsTELeft()
  {
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.TE && (double) this.xLocations[index] < 0.0)
        return true;
    }
    return false;
  }

  public bool IsTERight()
  {
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.TE && (double) this.xLocations[index] > 0.0)
        return true;
    }
    return false;
  }

  public int GetDefensiveLinemenInFormation()
  {
    int linemenInFormation = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.DL || this.positions[index] == Position.NT || this.positions[index] == Position.DE || this.positions[index] == Position.DT)
        ++linemenInFormation;
    }
    return linemenInFormation;
  }

  public int GetLinebackersInFormation()
  {
    int linebackersInFormation = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.OLB || this.positions[index] == Position.ILB || this.positions[index] == Position.MLB)
        ++linebackersInFormation;
    }
    return linebackersInFormation;
  }

  public int GetDefensiveBacksInFormation()
  {
    int backsInFormation = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.DB || this.positions[index] == Position.CB || this.positions[index] == Position.FS || this.positions[index] == Position.SS)
        ++backsInFormation;
    }
    return backsInFormation;
  }

  public string GetPersonnel() => this.personnel;

  public override string ToString()
  {
    string baseFormationString = this.GetBaseFormationString();
    string subFormationString = this.GetSubFormationString();
    string str1 = subFormationString.Trim().Length > 0 ? " - " : "";
    string str2 = subFormationString;
    return baseFormationString + str1 + str2;
  }

  public int NumHBLeft()
  {
    int num = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if ((this.positions[index] == Position.FB || this.positions[index] == Position.RB) && (double) this.xLocations[index] < 0.0)
        ++num;
    }
    return num;
  }

  public int NumHBRight()
  {
    int num = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if ((this.positions[index] == Position.FB || this.positions[index] == Position.RB) && (double) this.xLocations[index] > 0.0)
        ++num;
    }
    return num;
  }

  public int NumTELeft()
  {
    int num = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.TE && (double) this.xLocations[index] < 0.0)
        ++num;
    }
    return num;
  }

  public int NumTERight()
  {
    int num = 0;
    for (int index = 0; index < this.positions.Length; ++index)
    {
      if (this.positions[index] == Position.TE && (double) this.xLocations[index] > 0.0)
        ++num;
    }
    return num;
  }
}
