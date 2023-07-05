// Decompiled with JetBrains decompiler
// Type: FootballVR.AccelerationProfile
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace FootballVR
{
  public class AccelerationProfile
  {
    public float staticThreshold = 0.09f;
    public int staticFrameOffset;
    public int staticFrameCount = 40;
    public int rawDirectionFrames = 17;
    public int rawDirectionFramesOffset = 3;
    public int dotFramesOffset = 3;
    public float dotMinimum = 0.1f;
    public int minStartIndexOffset = 4;
  }
}
