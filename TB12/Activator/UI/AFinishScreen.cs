// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.AFinishScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using TMPro;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class AFinishScreen : UIView
  {
    [SerializeField]
    private TextMeshProUGUI _text;

    public override Enum ViewId { get; } = (Enum) EScreens.kThankYouScreen;

    protected override void WillAppear() => this._text.text = string.Format("{0}", (object) ActivationState.Score);
  }
}
