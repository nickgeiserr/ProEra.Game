// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CircularLayoutActionLink
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;

namespace FootballVR.UI
{
  public class CircularLayoutActionLink : UIHandle
  {
    private Action<int> _action;
    private CircularLayout _circularLayout;

    internal CircularLayoutActionLink(CircularLayout circularLayout, Action<int> action)
    {
      this._circularLayout = circularLayout;
      this._action = action;
    }

    protected override void OnStateChanged(bool state)
    {
      if (state)
        this._circularLayout.OnCurrentIndexChanged += this._action;
      else
        this._circularLayout.OnCurrentIndexChanged -= this._action;
    }

    protected override void OnDispose()
    {
      this._action = (Action<int>) null;
      this._circularLayout = (CircularLayout) null;
    }
  }
}
