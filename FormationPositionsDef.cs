// Decompiled with JetBrains decompiler
// Type: FormationPositionsDef
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public class FormationPositionsDef : FormationPositions
{
  private float[] xLocationShift1;
  private float[] xLocationShift2;
  private float[] xLocationShift3;
  private float[] xLocationShift4;
  private float[] xLocationShift5;
  private float[] zLocationShift1;
  private float[] zLocationShift2;
  private float[] zLocationShift3;
  private float[] zLocationShift4;
  private float[] zLocationShift5;

  public FormationPositionsDef(
    string _personnel,
    BaseFormation _baseFormation,
    SubFormation _subFormation)
    : base(_personnel, _baseFormation, _subFormation, 0)
  {
  }

  public void SetXLocationsShift1(float[] x) => this.xLocationShift1 = x;

  public void SetXLocationsShift2(float[] x) => this.xLocationShift2 = x;

  public void SetXLocationsShift3(float[] x) => this.xLocationShift3 = x;

  public void SetXLocationsShift4(float[] x) => this.xLocationShift4 = x;

  public void SetXLocationsShift5(float[] x) => this.xLocationShift5 = x;

  public void SetZLocationsShift1(float[] z) => this.zLocationShift1 = z;

  public void SetZLocationsShift2(float[] z) => this.zLocationShift2 = z;

  public void SetZLocationsShift3(float[] z) => this.zLocationShift3 = z;

  public void SetZLocationsShift4(float[] z) => this.zLocationShift4 = z;

  public void SetZLocationsShift5(float[] z) => this.zLocationShift5 = z;

  public float[] GetXLocations(int shiftedIndex)
  {
    switch (shiftedIndex)
    {
      case 1:
        return this.xLocationShift1;
      case 2:
        return this.xLocationShift2;
      case 3:
        return this.xLocationShift3;
      case 4:
        return this.xLocationShift4;
      case 5:
        return this.xLocationShift5;
      default:
        return this.GetXLocations();
    }
  }

  public float[] GetZLocations(int shiftedIndex)
  {
    switch (shiftedIndex)
    {
      case 1:
        return this.zLocationShift1;
      case 2:
        return this.zLocationShift2;
      case 3:
        return this.zLocationShift3;
      case 4:
        return this.zLocationShift4;
      case 5:
        return this.zLocationShift5;
      default:
        return this.GetZLocations();
    }
  }
}
