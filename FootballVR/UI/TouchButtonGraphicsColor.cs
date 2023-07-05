// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchButtonGraphicsColor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class TouchButtonGraphicsColor : MonoBehaviour
  {
    [SerializeField]
    private TouchButton _touchButton;
    [SerializeField]
    private Graphic _graphic;
    [SerializeField]
    private Color _defaultColor;
    [SerializeField]
    private Color _disabledColor;
    private EventHandle handle;

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this._touchButton == (UnityEngine.Object) null))
        return;
      this._touchButton = this.GetComponent<TouchButton>();
    }

    private void Awake() => this.handle = this._touchButton.Interactable.Link<bool>(new Action<bool>(this.HandleInteractable));

    private void OnEnable() => this.handle.SetState(true);

    private void OnDisable() => this.handle.SetState(false);

    private void OnDestroy() => this.handle.Dispose();

    private void HandleInteractable(bool interactable) => this._graphic.color = interactable ? this._defaultColor : this._disabledColor;

    public void SetButtonGraphicState(bool useActiveColor) => this._graphic.color = useActiveColor ? this._defaultColor : this._disabledColor;
  }
}
