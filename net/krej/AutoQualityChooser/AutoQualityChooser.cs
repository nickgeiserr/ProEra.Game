// Decompiled with JetBrains decompiler
// Type: net.krej.AutoQualityChooser.AutoQualityChooser
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using net.krej.FPSCounter;
using UnityEngine;
using UnityEngine.Events;

namespace net.krej.AutoQualityChooser
{
  [RequireComponent(typeof (FramerateCounter))]
  public class AutoQualityChooser : net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>
  {
    public UnityEvent onQualityChange;
    public AutoQualityChooserSettings settings = new AutoQualityChooserSettings();
    private FramerateCounter framerateCounter;
    public int secondsBeforeDecreasingQuality = 5;
    private readonly QualityChanger qualityChanger = new QualityChanger();

    private void Awake()
    {
      if (!net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>.AreTooManyOnScene())
        return;
      Object.Destroy((Object) this.gameObject);
    }

    private void Start()
    {
      this.framerateCounter = this.GetComponent<FramerateCounter>();
      if (this.settings.forceBestQualityOnStart)
        this.qualityChanger.SetQuality(QualitySettings.names.Length - 1);
      this.framerateCounter.ResetTimeLeft();
      this.framerateCounter.onFramerateCalculated.AddListener(new UnityAction(this.OnFramerateUpdated));
      this.ResetQualityDowngradeTimer();
    }

    private void ResetQualityDowngradeTimer() => this.secondsBeforeDecreasingQuality = this.settings.timeBeforeQualityDowngrade;

    private void OnFramerateUpdated()
    {
      if (!this.enabled)
        return;
      if (this.IsFramerateTooLow())
        --this.secondsBeforeDecreasingQuality;
      else
        this.ResetQualityDowngradeTimer();
      if (this.secondsBeforeDecreasingQuality >= 0)
        return;
      this.DecreaseQuality();
    }

    private bool IsFramerateTooLow() => (double) this.framerateCounter.currentFrameRate < (double) this.settings.minAcceptableFramerate;

    private void DecreaseQuality()
    {
      this.qualityChanger.DecreaseQuality();
      this.ResetQualityDowngradeTimer();
    }
  }
}
