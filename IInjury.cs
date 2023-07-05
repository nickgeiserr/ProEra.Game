// Decompiled with JetBrains decompiler
// Type: IInjury
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public interface IInjury
{
  string injuryCategory { get; set; }

  int weeksRemaining { get; set; }

  InjuryType injuryType { get; set; }

  int weeksToRecover { get; set; }

  StartingPosition startingPosition { get; set; }

  int playsRemaining { get; set; }
}
