// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarGraphicsData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class AvatarGraphicsData : ScriptableObject
  {
    public readonly Variable<Texture2D> baseMap = new Variable<Texture2D>();
    public readonly Variable<UniformCapture.Info> uniformCaptureInfo = new Variable<UniformCapture.Info>();
    public readonly Variable<EOutlineMode> outlineMode = new Variable<EOutlineMode>(EOutlineMode.kDisabld);
  }
}
