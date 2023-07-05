// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.ButtonText
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace FootballVR.UI
{
  public class ButtonText : ButtonGraphic
  {
    [SerializeField]
    private TextMeshProUGUI _textComponent;
    [SerializeField]
    private string _text;
    private static bool _autoFindText;

    public string text
    {
      get => this._text;
      set
      {
        this._text = value;
        this._textComponent.text = value;
      }
    }

    protected override void Awake() => base.Awake();

    private void OnValidate()
    {
      if ((Object) this._textComponent != (Object) null)
      {
        this._textComponent.text = this._text;
      }
      else
      {
        if (!ButtonText._autoFindText)
          return;
        TextMeshProUGUI[] componentsInChildren = this.GetComponentsInChildren<TextMeshProUGUI>();
        if (componentsInChildren == null || componentsInChildren.Length != 1 || !((Object) componentsInChildren[0] != (Object) null))
          return;
        this._textComponent = componentsInChildren[0];
        this._text = this._textComponent.text;
        this._containerTx = this._textComponent.transform.parent;
      }
    }

    public void SetTextVertexColor(Color a_newColor) => this._textComponent.color = a_newColor;
  }
}
