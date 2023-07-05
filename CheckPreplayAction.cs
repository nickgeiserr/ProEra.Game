// Decompiled with JetBrains decompiler
// Type: CheckPreplayAction
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckPreplayAction : Conditional
{
  public SharedBool ShouldGetToFormPos;
  public SharedBool ShouldDoShowBlitz;
  public SharedBool ShouldDoDefenseShift;

  public override TaskStatus OnUpdate() => this.ShouldGetToFormPos.Value || this.ShouldDoShowBlitz.Value || this.ShouldDoDefenseShift.Value ? TaskStatus.Success : TaskStatus.Failure;
}
