// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchToggleGroup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR.UI
{
  public class TouchToggleGroup : MonoBehaviour, IToggleGroup
  {
    [SerializeField]
    private bool _allowDeactivation;
    private TouchToggle _currentToggle;
    private readonly List<TouchToggle> _toggles = new List<TouchToggle>();
    private bool _initialized;

    public TouchToggle CurrentToggle => this._currentToggle;

    public bool AllowDeactivation => this._allowDeactivation;

    public event Action<IToggle> OnSelectionChanged;

    public void Register(TouchToggle toggle)
    {
      if (this._toggles.Contains(toggle))
        return;
      this._toggles.Add(toggle);
      toggle.OnValueChanged += new Action<TouchToggle>(this.HandleValueChanged);
    }

    public void Unregister(TouchToggle toggle)
    {
      this._toggles.Remove(toggle);
      toggle.OnValueChanged -= new Action<TouchToggle>(this.HandleValueChanged);
    }

    private void HandleValueChanged(TouchToggle toggle)
    {
      if (!toggle.Selected)
      {
        if (!this._allowDeactivation || !((UnityEngine.Object) toggle == (UnityEngine.Object) this._currentToggle))
          return;
        toggle = (TouchToggle) null;
      }
      this._currentToggle = toggle;
      Action<IToggle> selectionChanged = this.OnSelectionChanged;
      if (selectionChanged != null)
        selectionChanged((IToggle) this._currentToggle);
      foreach (TouchToggle toggle1 in this._toggles)
      {
        if ((UnityEngine.Object) toggle1 != (UnityEngine.Object) this._currentToggle)
          toggle1.SetState(false);
      }
    }

    public void SetValueById(int id)
    {
      if ((UnityEngine.Object) this._currentToggle != (UnityEngine.Object) null && this._currentToggle.id == id)
        return;
      foreach (TouchToggle toggle in this._toggles)
      {
        if (toggle.id == id)
        {
          toggle.SetState(true);
          return;
        }
      }
      Debug.LogError((object) string.Format("Could not find toggle with id: {0}", (object) id));
    }

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      foreach (TouchToggle componentsInChild in this.GetComponentsInChildren<TouchToggle>())
        this.Register(componentsInChild);
    }

    public void DeselectAll()
    {
      foreach (TouchToggle toggle in this._toggles)
        toggle.SetState(false);
    }
  }
}
