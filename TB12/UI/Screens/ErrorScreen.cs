// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.ErrorScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework.UI;
using System;
using UnityEngine;

namespace TB12.UI.Screens
{
  public class ErrorScreen : UIPanel
  {
    [SerializeField]
    private TouchButton _okButton;

    protected override void OnInitialize()
    {
      Transform transform = this.transform;
      transform.SetParent(UIAnchoring.PauseMenuCanvas.transform);
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
      this._okButton.onClick += new Action(this.HandleButtonPressed);
    }

    protected override void DidDisappear() => UIDispatch.SetScreensVisible(true);

    protected override void WillAppear() => UIDispatch.SetScreensVisible(false);

    public void HandleErrorState(bool active)
    {
      if (active)
        this.Show();
      else
        this.Hide();
    }

    private void HandleButtonPressed()
    {
      Debug.Log((object) "Ok pressed");
      VRState.ErrorOccurred.SetValue(false);
    }
  }
}
