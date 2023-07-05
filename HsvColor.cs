// Decompiled with JetBrains decompiler
// Type: HsvColor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public struct HsvColor
{
  public double H;
  public double S;
  public double V;

  public float normalizedH
  {
    get => (float) this.H / 360f;
    set => this.H = (double) value * 360.0;
  }

  public float normalizedS
  {
    get => (float) this.S;
    set => this.S = (double) value;
  }

  public float normalizedV
  {
    get => (float) this.V;
    set => this.V = (double) value;
  }

  public HsvColor(double h, double s, double v)
  {
    this.H = h;
    this.S = s;
    this.V = v;
  }

  public override string ToString() => "{" + this.H.ToString("f2") + "," + this.S.ToString("f2") + "," + this.V.ToString("f2") + "}";
}
