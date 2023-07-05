// Decompiled with JetBrains decompiler
// Type: UDB.TimeKeeper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class TimeKeeper : SingletonBehaviour<TimeKeeper, MonoBehaviour>
  {
    public bool resetTimeOnEnable;
    private float startTime;

    public float lifeTime => Time.time - this.startTime;

    private void Start() => this.startTime = Time.time;

    private void OnEnable()
    {
      if (!this.resetTimeOnEnable)
        return;
      this.startTime = Time.time;
    }

    private void _SetTimeTo(float time) => this.startTime = Time.time - time;

    private void _ResetTime() => TimeKeeper.SetTimeTo(0.0f);

    public static void SetTimeTo(float time) => SingletonBehaviour<TimeKeeper, MonoBehaviour>.instance._SetTimeTo(time);

    public static void ResetTime() => SingletonBehaviour<TimeKeeper, MonoBehaviour>.instance._ResetTime();
  }
}
