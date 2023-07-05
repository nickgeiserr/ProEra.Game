// Decompiled with JetBrains decompiler
// Type: RouteGraphicData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public class RouteGraphicData
{
  public int lineLength_1;
  public int lineLength_2;
  public int lineLength_3;
  public int lineAngle_1;
  public int lineAngle_2;
  public int lineAngle_3;
  public int parentAngle;
  public int blitzLocation;
  public LineEndType lineEndType;
  public ZoneType zoneType;
  public BlitzType blitzType;

  public RouteGraphicData()
  {
    this.lineLength_1 = 0;
    this.lineAngle_1 = 0;
    this.lineLength_2 = 0;
    this.lineAngle_2 = 0;
    this.lineLength_3 = 0;
    this.lineAngle_3 = 0;
    this.zoneType = ZoneType.None;
    this.blitzType = BlitzType.None;
  }

  public RouteGraphicData(BlitzType b)
  {
    this.blitzType = b;
    this.zoneType = ZoneType.None;
  }

  public RouteGraphicData(ZoneType t)
  {
    this.zoneType = t;
    this.lineEndType = LineEndType.Blank;
    this.blitzType = BlitzType.None;
  }

  public RouteGraphicData(int length1, int angle1, LineEndType endType)
  {
    this.lineLength_1 = length1;
    this.lineAngle_1 = angle1;
    this.lineLength_2 = 0;
    this.lineAngle_2 = 0;
    this.lineLength_3 = 0;
    this.lineAngle_3 = 0;
    this.zoneType = ZoneType.None;
    this.blitzType = BlitzType.None;
    this.lineEndType = endType;
  }

  public RouteGraphicData(int length1, int angle1, int length2, int angle2, LineEndType endType)
  {
    this.lineLength_1 = length1;
    this.lineAngle_1 = angle1;
    this.lineLength_2 = length2;
    this.lineAngle_2 = angle2;
    this.lineLength_3 = 0;
    this.lineAngle_3 = 0;
    this.zoneType = ZoneType.None;
    this.blitzType = BlitzType.None;
    this.lineEndType = endType;
  }

  public RouteGraphicData(
    int length1,
    int angle1,
    int length2,
    int angle2,
    int length3,
    int angle3,
    LineEndType endType)
  {
    this.lineLength_1 = length1;
    this.lineAngle_1 = angle1;
    this.lineLength_2 = length2;
    this.lineAngle_2 = angle2;
    this.lineLength_3 = length3;
    this.lineAngle_3 = angle3;
    this.zoneType = ZoneType.None;
    this.blitzType = BlitzType.None;
    this.lineEndType = endType;
  }
}
