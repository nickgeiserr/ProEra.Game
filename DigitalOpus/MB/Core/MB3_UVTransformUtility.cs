// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_UVTransformUtility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class MB3_UVTransformUtility
  {
    public static void Test()
    {
      DRect drect = new DRect(0.5, 0.5, 2.0, 2.0);
      DRect t = new DRect(0.25, 0.25, 3.0, 3.0);
      DRect r1 = MB3_UVTransformUtility.InverseTransform(ref drect);
      DRect r2 = MB3_UVTransformUtility.InverseTransform(ref t);
      DRect r3 = MB3_UVTransformUtility.CombineTransforms(ref drect, ref r2);
      Debug.Log((object) r1);
      Debug.Log((object) r3);
      Debug.Log((object) ("one mat trans " + MB3_UVTransformUtility.TransformPoint(ref drect, new Vector2(1f, 1f)).ToString()));
      Debug.Log((object) ("one inv mat trans " + MB3_UVTransformUtility.TransformPoint(ref r1, new Vector2(1f, 1f)).ToString("f4")));
      Debug.Log((object) ("zero " + MB3_UVTransformUtility.TransformPoint(ref r3, new Vector2(0.0f, 0.0f)).ToString("f4")));
      Debug.Log((object) ("one " + MB3_UVTransformUtility.TransformPoint(ref r3, new Vector2(1f, 1f)).ToString("f4")));
    }

    public static float TransformX(DRect r, double x) => (float) (r.width * x + r.x);

    public static DRect CombineTransforms(ref DRect r1, ref DRect r2) => new DRect(r1.x * r2.width + r2.x, r1.y * r2.height + r2.y, r1.width * r2.width, r1.height * r2.height);

    public static Rect CombineTransforms(ref Rect r1, ref Rect r2) => new Rect(r1.x * r2.width + r2.x, r1.y * r2.height + r2.y, r1.width * r2.width, r1.height * r2.height);

    public static void Canonicalize(ref DRect r, double minX, double minY)
    {
      r.x -= (double) Mathf.FloorToInt((float) r.x);
      if (r.x < minX)
        r.x += (double) Mathf.CeilToInt((float) minX);
      r.y -= (double) Mathf.FloorToInt((float) r.y);
      if (r.y >= minY)
        return;
      r.y += (double) Mathf.CeilToInt((float) minY);
    }

    public static void Canonicalize(ref Rect r, float minX, float minY)
    {
      r.x -= (float) Mathf.FloorToInt(r.x);
      if ((double) r.x < (double) minX)
        r.x += (float) Mathf.CeilToInt(minX);
      r.y -= (float) Mathf.FloorToInt(r.y);
      if ((double) r.y >= (double) minY)
        return;
      r.y += (float) Mathf.CeilToInt(minY);
    }

    public static DRect InverseTransform(ref DRect t) => new DRect()
    {
      x = -t.x / t.width,
      y = -t.y / t.height,
      width = 1.0 / t.width,
      height = 1.0 / t.height
    };

    public static DRect GetEncapsulatingRect(ref DRect uvRect1, ref DRect uvRect2)
    {
      double x1 = uvRect1.x;
      double y1 = uvRect1.y;
      double num1 = uvRect1.x + uvRect1.width;
      double num2 = uvRect1.y + uvRect1.height;
      double x2 = uvRect2.x;
      double y2 = uvRect2.y;
      double num3 = uvRect2.x + uvRect2.width;
      double num4 = uvRect2.y + uvRect2.height;
      double num5;
      double xx = num5 = x1;
      double num6;
      double yy = num6 = y1;
      if (x2 < xx)
        xx = x2;
      if (x1 < xx)
        xx = x1;
      if (y2 < yy)
        yy = y2;
      if (y1 < yy)
        yy = y1;
      if (num3 > num5)
        num5 = num3;
      if (num1 > num5)
        num5 = num1;
      if (num4 > num6)
        num6 = num4;
      if (num2 > num6)
        num6 = num2;
      return new DRect(xx, yy, num5 - xx, num6 - yy);
    }

    public static bool RectContains(ref DRect bigRect, ref DRect smallToTestIfFits)
    {
      double x = smallToTestIfFits.x;
      double y = smallToTestIfFits.y;
      double num1 = smallToTestIfFits.x + smallToTestIfFits.width;
      double num2 = smallToTestIfFits.y + smallToTestIfFits.height;
      double num3 = bigRect.x - 0.0099999997764825821;
      double num4 = bigRect.y - 0.0099999997764825821;
      double num5 = bigRect.x + bigRect.width + 0.019999999552965164;
      double num6 = bigRect.y + bigRect.height + 0.019999999552965164;
      return num3 <= x && x <= num5 && num3 <= num1 && num1 <= num5 && num4 <= y && y <= num6 && num4 <= num2 && num2 <= num6;
    }

    public static bool RectContains(ref Rect bigRect, ref Rect smallToTestIfFits)
    {
      float x = smallToTestIfFits.x;
      float y = smallToTestIfFits.y;
      float num1 = smallToTestIfFits.x + smallToTestIfFits.width;
      float num2 = smallToTestIfFits.y + smallToTestIfFits.height;
      float num3 = bigRect.x - 0.01f;
      float num4 = bigRect.y - 0.01f;
      float num5 = (float) ((double) bigRect.x + (double) bigRect.width + 0.019999999552965164);
      float num6 = (float) ((double) bigRect.y + (double) bigRect.height + 0.019999999552965164);
      return (double) num3 <= (double) x && (double) x <= (double) num5 && (double) num3 <= (double) num1 && (double) num1 <= (double) num5 && (double) num4 <= (double) y && (double) y <= (double) num6 && (double) num4 <= (double) num2 && (double) num2 <= (double) num6;
    }

    internal static Vector2 TransformPoint(ref DRect r, Vector2 p) => new Vector2((float) (r.width * (double) p.x + r.x), (float) (r.height * (double) p.y + r.y));
  }
}
