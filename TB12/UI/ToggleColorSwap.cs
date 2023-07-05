// Decompiled with JetBrains decompiler
// Type: TB12.UI.ToggleColorSwap
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class ToggleColorSwap : MonoBehaviour
  {
    [SerializeField]
    private TouchToggle _toggle;
    [SerializeField]
    private Graphic _image;
    [SerializeField]
    private Color _idleColor;
    [SerializeField]
    private Color _activeColor;
    [SerializeField]
    private bool _applyInactive;
    [SerializeField]
    private Color _inactiveColor;

    private void Awake()
    {
      this.ToggleHandle(this._toggle);
      this._toggle.OnValueChanged += new Action<TouchToggle>(this.ToggleHandle);
      this._toggle.Interactable.OnValueChanged += new Action<bool>(this.HandleInteractableChanged);
    }

    private void OnDestroy()
    {
      this._toggle.OnValueChanged -= new Action<TouchToggle>(this.ToggleHandle);
      this._toggle.Interactable.OnValueChanged -= new Action<bool>(this.HandleInteractableChanged);
    }

    private void ToggleHandle(TouchToggle toggle)
    {
      if (this._applyInactive && !(bool) toggle.Interactable)
        this._image.color = this._inactiveColor;
      else
        this._image.color = toggle.Selected ? this._activeColor : this._idleColor;
    }

    private void Reset()
    {
      if (!((UnityEngine.Object) this._toggle == (UnityEngine.Object) null))
        return;
      this._toggle = this.GetComponent<TouchToggle>();
    }

    private void HandleInteractableChanged(bool interactable) => this.ToggleHandle(this._toggle);
  }
}
