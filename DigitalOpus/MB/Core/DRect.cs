// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.DRect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public struct DRect
  {
    public double x;
    public double y;
    public double width;
    public double height;

    public DRect(Rect r)
    {
      this.x = (double) r.x;
      this.y = (double) r.y;
      this.width = (double) r.width;
      this.height = (double) r.height;
    }

    public DRect(Vector2 o, Vector2 s)
    {
      this.x = (double) o.x;
      this.y = (double) o.y;
      this.width = (double) s.x;
      this.height = (double) s.y;
    }

    public DRect(float xx, float yy, float w, float h)
    {
      this.x = (double) xx;
      this.y = (double) yy;
      this.width = (double) w;
      this.height = (double) h;
    }

    public DRect(double xx, double yy, double w, double h)
    {
      this.x = xx;
      this.y = yy;
      this.width = w;
      this.height = h;
    }

    public Rect GetRect() => new Rect((float) this.x, (float) this.y, (float) this.width, (float) this.height);

    public Vector2 min => new Vector2((float) this.x, (float) this.y);

    public Vector2 max => new Vector2((float) (this.x + this.width), (float) (this.y + this.width));

    public Vector2 size => new Vector2((float) this.width, (float) this.width);

    public override bool Equals(object obj)
    {
      DRect drect = (DRect) obj;
      return drect.x == this.x && drect.y == this.y && drect.width == this.width && drect.height == this.height;
    }

    public static bool operator ==(DRect a, DRect b) => a.Equals((object) b);

    public static bool operator !=(DRect a, DRect b) => !a.Equals((object) b);

    public override string ToString() => string.Format("(x={0},y={1},w={2},h={3})", (object) this.x.ToString("F5"), (object) this.y.ToString("F5"), (object) this.width.ToString("F5"), (object) this.height.ToString("F5"));

    public bool Encloses(DRect smallToTestIfFits)
    {
      double x1 = smallToTestIfFits.x;
      double y1 = smallToTestIfFits.y;
      double num1 = smallToTestIfFits.x + smallToTestIfFits.width;
      double num2 = smallToTestIfFits.y + smallToTestIfFits.height;
      double x2 = this.x;
      double y2 = this.y;
      double num3 = this.x + this.width;
      double num4 = this.y + this.height;
      return x2 <= x1 && x1 <= num3 && x2 <= num1 && num1 <= num3 && y2 <= y1 && y1 <= num4 && y2 <= num2 && num2 <= num4;
    }

    public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.width.GetHashCode() ^ this.height.GetHashCode();
  }
}
