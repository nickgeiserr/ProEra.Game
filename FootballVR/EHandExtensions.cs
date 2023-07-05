// Decompiled with JetBrains decompiler
// Type: FootballVR.EHandExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace FootballVR
{
  public static class EHandExtensions
  {
    public static OVRInput.Controller GetController(this EHand eHand) => eHand == EHand.Right || eHand != EHand.Left ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
  }
}
