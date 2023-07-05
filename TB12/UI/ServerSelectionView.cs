// Decompiled with JetBrains decompiler
// Type: TB12.UI.ServerSelectionView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI
{
  public class ServerSelectionView : UIView
  {
    [SerializeField]
    private TouchToggleGroup _toggleGroup;
    [SerializeField]
    private TouchLayoutBuilder _layout;
    [SerializeField]
    private TouchButton _okButton;

    public override Enum ViewId { get; } = (Enum) ELockerRoomUI.kSelectServer;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._okButton, (Action) (() => UIDispatch.LockerRoomScreen.DisplayView(ELockerRoomUI.kMultiplayer)))
    });

    protected override void WillAppear()
    {
      List<ServerRegion> regionData = NetworkState.RegionData;
      int count = regionData.Count;
      string[] buttonTexts = new string[count];
      string[] ids = new string[count];
      for (int index = 0; index < count; ++index)
      {
        ServerRegion serverRegion = regionData[index];
        string str = string.Format("{0}\r\n[{1}]", (object) serverRegion.code.ToUpperInvariant(), (object) regionData[index].latency);
        if (serverRegion.isBest)
          str += "\r\n[BEST]";
        else if (serverRegion.code == "us")
          str += "\r\n[Default]";
        buttonTexts[index] = str;
        ids[index] = serverRegion.code;
      }
      this._layout.BuildButtons((IList<string>) buttonTexts, (IList<string>) ids);
      IReadOnlyCollection<TouchLayoutButton> elements = this._layout.Elements;
      string region = NetworkState.Region;
      foreach (TouchLayoutButton touchLayoutButton in (IEnumerable<TouchLayoutButton>) elements)
      {
        if (!((UnityEngine.Object) touchLayoutButton == (UnityEngine.Object) null) && !(touchLayoutButton.id != region))
        {
          if (!(touchLayoutButton.Button is TouchToggle button))
            break;
          button.SetState(true);
          break;
        }
      }
    }

    protected override void WillDisappear()
    {
      TouchToggle currentToggle = this._toggleGroup.CurrentToggle;
      if ((UnityEngine.Object) currentToggle == (UnityEngine.Object) null)
        return;
      TouchLayoutButton component = currentToggle.GetComponent<TouchLayoutButton>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      NetworkState.Region = component.id;
    }
  }
}
