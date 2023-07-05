// Decompiled with JetBrains decompiler
// Type: net.krej.AutoQualityChooser.AutoQualityChooserSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace net.krej.AutoQualityChooser
{
  [Serializable]
  public class AutoQualityChooserSettings
  {
    public bool disableAfterManualQualityChange = true;
    public bool forceBestQualityOnStart = true;
    public float minAcceptableFramerate = 20f;
    public int timeBeforeQualityDowngrade = 5;
  }
}
