// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.AcknowledgeableAward
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

namespace ProEra.Game.Achievements
{
  [MessagePackObject(false)]
  [Serializable]
  public class AcknowledgeableAward
  {
    [Key(0)]
    public string Name = string.Empty;
    [Key(1)]
    public bool Acknowledged;
    [Key(2)]
    public bool HasBeenAwarded;

    [IgnoreMember]
    public bool ShouldPlaySfx { get; private set; }

    public void Init(string name = null, bool acknowledged = false)
    {
      this.Name = name;
      this.Acknowledged = acknowledged;
    }

    public void GrantAward()
    {
      this.HasBeenAwarded = true;
      this.ShouldPlaySfx = true;
    }

    public void DisableFx() => this.ShouldPlaySfx = false;
  }
}
