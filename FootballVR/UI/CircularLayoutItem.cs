// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CircularLayoutItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR.UI
{
  public abstract class CircularLayoutItem : MonoBehaviour
  {
    private bool initialized;
    private bool _isOn;
    [SerializeField]
    private GameObject IsFocusedGameObject;

    public int index { get; set; } = -1;

    public bool isOn
    {
      get => this._isOn;
      set
      {
        if (this._isOn == value && this.initialized)
          return;
        this.initialized = true;
        this._isOn = value;
        this.OnStateChanged(this._isOn);
      }
    }

    public RectTransform RectTransform => (RectTransform) this.transform;

    public abstract RectTransform TextGroup { get; }

    public abstract float alpha { set; }

    protected virtual void OnStateChanged(bool state)
    {
      if (!((Object) this.IsFocusedGameObject != (Object) null))
        return;
      this.IsFocusedGameObject.SetActive(state);
    }

    public abstract void SetVisible(bool state);

    public void InvalidateCache()
    {
      this.index = -1;
      this.initialized = false;
    }
  }
}
