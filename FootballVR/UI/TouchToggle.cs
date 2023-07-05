// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchToggle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using UnityEngine;

namespace FootballVR.UI
{
  public class TouchToggle : TouchButton, IToggle
  {
    [SerializeField]
    private TouchToggleGroup _toggleGroup;
    [SerializeField]
    private bool _canUncheck = true;

    public bool Selected { get; private set; }

    public event Action<bool> OnStateChanged;

    public event Action<TouchToggle> OnValueChanged;

    public int id { get; set; }

    public void SetId(Enum value) => this.id = Convert.ToInt32((object) value);

    protected override void Awake()
    {
      base.Awake();
      if (!((UnityEngine.Object) this._toggleGroup != (UnityEngine.Object) null))
        return;
      this._toggleGroup.Register(this);
    }

    protected override void HandleButtonPressed()
    {
      if (this.Selected && (!this._canUncheck || (UnityEngine.Object) this._toggleGroup != (UnityEngine.Object) null && (UnityEngine.Object) this._toggleGroup.CurrentToggle == (UnityEngine.Object) this && !this._toggleGroup.AllowDeactivation))
        return;
      this.SetState(!this.Selected);
      base.HandleButtonPressed();
    }

    protected override void Update()
    {
      if (this.Selected)
        return;
      base.Update();
    }

    public void SetState(bool state, bool silent = false)
    {
      this.Initialize();
      if (this.Selected == state)
        return;
      if (state)
        this.SetNormalizedPosition(1f);
      else
        this._recovering = true;
      this.Selected = state;
      if (silent)
        return;
      Action<bool> onStateChanged = this.OnStateChanged;
      if (onStateChanged != null)
        onStateChanged(state);
      Action<TouchToggle> onValueChanged = this.OnValueChanged;
      if (onValueChanged == null)
        return;
      onValueChanged(this);
    }

    public void SetToggleGroup(TouchToggleGroup group)
    {
      if ((UnityEngine.Object) this._toggleGroup != (UnityEngine.Object) null && (UnityEngine.Object) this._toggleGroup != (UnityEngine.Object) group)
        this._toggleGroup.Unregister(this);
      this._toggleGroup = group;
      if (!((UnityEngine.Object) this._toggleGroup != (UnityEngine.Object) null))
        return;
      this._toggleGroup.Register(this);
    }
  }
}
