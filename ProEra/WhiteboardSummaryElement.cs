// Decompiled with JetBrains decompiler
// Type: ProEra.WhiteboardSummaryElement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace ProEra
{
  public class WhiteboardSummaryElement : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _labelText;
    [SerializeField]
    private TextMeshProUGUI _bodyText;

    public TextMeshProUGUI LabelText => this._labelText;

    public TextMeshProUGUI BodyText => this._bodyText;
  }
}
