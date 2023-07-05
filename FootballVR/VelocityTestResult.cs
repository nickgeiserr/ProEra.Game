// Decompiled with JetBrains decompiler
// Type: FootballVR.VelocityTestResult
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace FootballVR
{
  public class VelocityTestResult
  {
    public float averageDelta;
    public float averageDotProduct;
    public float averageDirectionDelta;
    public float medianDirectionDelta;
    public float averageMagnitudeDelta;
    public float maxDelta;
    public int frameCount;
    public bool lerped;
    public float lerpVal;
    public float minThreshold;
    public float dotThreshold;
    public float rawDirFrameCount;
    public bool separateMagnitude;

    public override string ToString()
    {
      string str = string.Format("AvgDelta: {0}. AvgDirDelta: {1}. MedianDirDelta: {2}, Max delta: {3}. Avg Mag Delta: {4}. DOT {5} ", (object) this.averageDelta, (object) this.averageDirectionDelta, (object) this.medianDirectionDelta, (object) this.maxDelta, (object) this.averageMagnitudeDelta, (object) this.averageDotProduct) + string.Format("Considered[{0}], DotThreshold[{1}], MinThreshold [{2}], RawDirFrameCount[{3}], SeparateMag [{4}]", (object) this.frameCount, (object) this.dotThreshold, (object) this.minThreshold, (object) this.rawDirFrameCount, (object) this.separateMagnitude);
      if (this.lerped)
        str += string.Format(" LERP [{0}]", (object) this.lerpVal);
      return str;
    }
  }
}
