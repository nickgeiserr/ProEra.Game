// Decompiled with JetBrains decompiler
// Type: TB12.SeasonTeamPlayerType
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using System;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class SeasonTeamPlayerType : UIView
  {
    [SerializeField]
    private TouchButton _proButton;
    [SerializeField]
    private TouchButton _capButton;

    public override Enum ViewId { get; } = (Enum) EScreens.kSeasonPlayerType;

    protected override void OnInitialize()
    {
    }

    private void HandleProSelected() => UIDispatch.LockerRoomScreen.CloseScreen();
  }
}
