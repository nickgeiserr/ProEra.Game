// Decompiled with JetBrains decompiler
// Type: TrackedStat
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine.Events;

[Serializable]
public class TrackedStat : Stat
{
  public event Action<Stat> OnValueChanged;

  public UnityEvent OnValueChangedUnityEvent { get; private set; } = new UnityEvent();

  public override void SetValue<T>(T newValue)
  {
    string statValue1 = (string) this.StatValue;
    base.SetValue<T>(newValue);
    string statValue2 = (string) this.StatValue;
    if (!(statValue1 != statValue2))
      return;
    Action<Stat> onValueChanged = this.OnValueChanged;
    if (onValueChanged != null)
      onValueChanged((Stat) this);
    this.OnValueChangedUnityEvent?.Invoke();
  }
}
