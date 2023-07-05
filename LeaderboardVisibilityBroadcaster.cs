// Decompiled with JetBrains decompiler
// Type: LeaderboardVisibilityBroadcaster
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

public class LeaderboardVisibilityBroadcaster : MonoBehaviour
{
  public static VariableBool ShowScreen = new VariableBool();

  public void BroadcastVisible() => LeaderboardVisibilityBroadcaster.ShowScreen.SetValue(true);

  public void BroadcastHidden() => LeaderboardVisibilityBroadcaster.ShowScreen.SetValue(false);
}
