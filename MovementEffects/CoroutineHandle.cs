// Decompiled with JetBrains decompiler
// Type: MovementEffects.CoroutineHandle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace MovementEffects
{
  public struct CoroutineHandle : IEquatable<CoroutineHandle>
  {
    private const byte ReservedSpace = 31;
    private static readonly int[] NextIndex = new int[31]
    {
      32,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    };
    private readonly int _id;

    public byte Key => (byte) (this._id & 31);

    public CoroutineHandle(byte ind)
    {
      if (ind > (byte) 31)
        ind -= (byte) 31;
      this._id = CoroutineHandle.NextIndex[(int) ind] + (int) ind;
      CoroutineHandle.NextIndex[(int) ind] += 32;
    }

    public bool Equals(CoroutineHandle other) => this._id == other._id;

    public override bool Equals(object other) => other is CoroutineHandle other1 && this.Equals(other1);

    public static bool operator ==(CoroutineHandle a, CoroutineHandle b) => a._id == b._id;

    public static bool operator !=(CoroutineHandle a, CoroutineHandle b) => a._id != b._id;

    public override int GetHashCode() => this._id;

    public string Tag
    {
      get => Timing.GetTag(this);
      set => Timing.SetTag(this, value);
    }

    public int? Layer
    {
      get => Timing.GetLayer(this);
      set
      {
        if (!value.HasValue)
          Timing.RemoveLayer(this);
        else
          Timing.SetLayer(this, value.Value);
      }
    }

    public Segment Segment
    {
      get => Timing.GetSegment(this);
      set => Timing.SetSegment(this, value);
    }

    public bool IsRunning
    {
      get => Timing.IsRunning(this);
      set
      {
        if (value)
          return;
        Timing.KillCoroutines(this);
      }
    }

    public bool IsPaused
    {
      get => Timing.IsPaused(this);
      set
      {
        if (value)
          Timing.PauseCoroutines(this);
        else
          Timing.ResumeCoroutines(this);
      }
    }

    public bool IsValid => this.Key > (byte) 0;
  }
}
