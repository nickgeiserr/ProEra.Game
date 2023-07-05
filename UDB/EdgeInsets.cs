// Decompiled with JetBrains decompiler
// Type: UDB.EdgeInsets
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  [Serializable]
  public struct EdgeInsets
  {
    public float xLeftPercent;
    public float xRightPercent;
    public float yTopPercent;
    public float yBottomPercent;

    public float leftPercent => (double) this.xLeftPercent > 1.0 ? this.xLeftPercent / 100f : this.xLeftPercent;

    public float rightPercent => (double) this.xRightPercent > 1.0 ? this.xRightPercent / 100f : this.xRightPercent;

    public float topPercent => (double) this.yTopPercent > 1.0 ? this.yTopPercent / 100f : this.yTopPercent;

    public float bottomPercent => (double) this.yBottomPercent > 1.0 ? this.yBottomPercent / 100f : this.yBottomPercent;

    public EdgeInsets(float xPercent, float yPercent)
    {
      this.xLeftPercent = xPercent;
      this.xRightPercent = xPercent;
      this.yTopPercent = yPercent;
      this.yBottomPercent = yPercent;
    }
  }
}
