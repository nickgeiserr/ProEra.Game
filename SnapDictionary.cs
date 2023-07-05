// Decompiled with JetBrains decompiler
// Type: SnapDictionary
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class SnapDictionary : ScriptableObject
{
  public InteractionController underCenter3StepDrop;
  public InteractionController underCenter5StepDrop;
  public InteractionController underCenter7StepDrop;
  public InteractionController shotgun1StepDrop;
  public InteractionController shotgun3StepDrop;
  public InteractionController pistol1StepDrop;
  public InteractionController pistol3StepDrop;
  public InteractionController qBKneel;
  public InteractionController qBSpike;
  public InteractionController qbKeeper;

  public InteractionController GetController(DropbackType dropbackType, float centerDistance)
  {
    if (Game.IsQBKneel || dropbackType == DropbackType.Kneel)
      return this.qBKneel;
    if (Game.IsQBSpike || dropbackType == DropbackType.Spike)
      return this.qBSpike;
    if (Game.IsQBKeeper)
      return this.qbKeeper;
    if ((double) centerDistance < 1.0)
    {
      if (dropbackType == DropbackType.ThreeStep)
        return this.underCenter3StepDrop;
      return dropbackType == DropbackType.FiveStep ? this.underCenter5StepDrop : this.underCenter7StepDrop;
    }
    return (double) centerDistance < 4.0 ? (dropbackType == DropbackType.ThreeStep ? this.pistol3StepDrop : this.pistol1StepDrop) : (dropbackType == DropbackType.ThreeStep ? this.shotgun3StepDrop : this.shotgun1StepDrop);
  }
}
