// Decompiled with JetBrains decompiler
// Type: net.krej.AutoQualityChooser.QualityChanger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace net.krej.AutoQualityChooser
{
  public class QualityChanger
  {
    private int currQuality = -1;
    public string currentQuality;

    public void IncreaseQuality() => this.AddToQuality(1);

    public void DecreaseQuality() => this.AddToQuality(-1);

    private void AddToQuality(int amount)
    {
      if (net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>.Instance.settings.disableAfterManualQualityChange && this.currQuality >= 0 && this.currQuality != QualitySettings.GetQualityLevel())
      {
        net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>.Instance.enabled = false;
      }
      else
      {
        this.currQuality = QualitySettings.GetQualityLevel();
        this.SetQuality(this.currQuality + amount);
      }
    }

    public void SetQuality(int value)
    {
      if (value < 0)
        return;
      if (value >= QualitySettings.names.Length)
        value = QualitySettings.names.Length - 1;
      this.currQuality = QualitySettings.GetQualityLevel();
      if (value != this.currQuality)
      {
        QualitySettings.SetQualityLevel(value, false);
        this.currQuality = QualitySettings.GetQualityLevel();
        this.currentQuality = this.currQuality.ToString() + " (" + QualitySettings.names[this.currQuality] + ")";
        if (net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>.Instance.onQualityChange == null)
          return;
        net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>.Instance.onQualityChange.Invoke();
      }
      else
        this.currentQuality = this.currQuality.ToString() + " (" + QualitySettings.names[this.currQuality] + ")";
    }

    public static string GetCurrentQualityName()
    {
      int qualityLevel = QualitySettings.GetQualityLevel();
      return string.Format("{0}/{1}: {2}", (object) (qualityLevel + 1), (object) QualitySettings.names.Length, (object) QualitySettings.names[qualityLevel]);
    }
  }
}
