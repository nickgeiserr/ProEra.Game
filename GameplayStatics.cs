// Decompiled with JetBrains decompiler
// Type: GameplayStatics
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

public static class GameplayStatics
{
  public static AppEvent<Vector3, Quaternion> FistBumpEvent = new AppEvent<Vector3, Quaternion>();

  public static void PlayFirstBumpEffect(Vector3 location, Quaternion orientation) => GameplayStatics.FistBumpEvent.Trigger(location, orientation);
}
