// Decompiled with JetBrains decompiler
// Type: SuperSimTuningConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Services.RemoteConfig;
using UnityEngine;

public class SuperSimTuningConfig : MonoBehaviour, IGameplayConfig, IHasRemoteConfigSettings
{
  [Tooltip("The longest time for the QB to pass the ball.")]
  public float MaxThrowTime = 6f;
  [Tooltip("The shortest time for the QB to pass the ball.")]
  public float MinThrowTime = 2.5f;
  [Tooltip("The the chance of a sack on a QB pass play.")]
  public float SackChance = 0.08f;
  [Tooltip("The  shortest time that a sack can happen.")]
  public float MinSackTime = 2f;
  [Tooltip("The min time since snap where QB can throwaway the ball.")]
  public float MinThrowAwayTime = 4f;
  [Tooltip("The normalized [0 - 1] Chance for QB to throwaway the ball.")]
  public float ThrowAwayChance = 0.1f;
  [Tooltip("Time to run off the clock for a spike play")]
  public float SpikePlayTime;
  [Header("Pass Accuracy")]
  [Tooltip("The distance considered \"short\" for accuracy calculation purposes")]
  public float ShortPassDistance = 10f;
  [Tooltip("The distance considered \"deep\" for accuracy calculation purposes")]
  public float DeepPassDistance = 50f;
  [Tooltip("QB accuracy rating that corresponds with the lowest accuracy chances")]
  public float MinAccuracyRating = 70f;
  [Tooltip("QB accuracy rating that corresponds with the highest accuracy chances")]
  public float MaxAccuracyRating = 99f;
  [Tooltip("Chance that a high-rated QB is accurate on a short throw")]
  public float MaxShortAccuracyChance = 0.95f;
  [Tooltip("Chance that a low-rated QB is accurate on a short throw")]
  public float MinShortAccuracyChance = 0.7f;
  [Tooltip("Chance that a high-rated QB is accurate on a deep throw")]
  public float MaxDeepAccuracyChance = 0.8f;
  [Tooltip("Chance that a low-rated QB is accurate on a deep throw")]
  public float MinDeepAccuracyChance = 0.6f;
  [Header("Interceptions")]
  [Tooltip("Rating that corresponds to MinInterceptionChance")]
  public float MinInterceptionRating = 70f;
  [Tooltip("Rating that corresponds to MaxInterceptionChance")]
  public float MaxInterceptionRating = 99f;
  [Tooltip("Chance that a relatively poor defender will intercept a bad pass")]
  public float MinInterceptionChance = 0.05f;
  [Tooltip("Chance that a relatively good defender will intercept a bad pass")]
  public float MaxInterceptionChance = 0.2f;
  [Header("Tackling")]
  [Tooltip("The maximum chance of a broken tackle for any given tackle check during the sim")]
  public float MaxBreakTackleChance = 0.1f;

  public float GetGoodPassChance(float passDistance, float accuracyRating)
  {
    float t = MathUtils.MapRange(passDistance, this.ShortPassDistance, this.DeepPassDistance, 0.0f, 1f);
    float OutRangeA = Mathf.Lerp(this.MinShortAccuracyChance, this.MinDeepAccuracyChance, t);
    float OutRangeB = Mathf.Lerp(this.MaxShortAccuracyChance, this.MaxDeepAccuracyChance, t);
    return MathUtils.MapRange(accuracyRating, this.MinAccuracyRating, this.MaxAccuracyRating, OutRangeA, OutRangeB);
  }

  public float GetInterceptionChance(float interceptionRating) => MathUtils.MapRange(interceptionRating, this.MinInterceptionRating, this.MaxInterceptionRating, this.MinInterceptionChance, this.MaxInterceptionChance);

  public void ApplyRemoteConfig()
  {
    this.MaxThrowTime = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxThrowTime", this.MaxThrowTime);
    this.MinThrowTime = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinThrowTime", this.MinThrowTime);
    this.SackChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.SackChance", this.SackChance);
    this.MinSackTime = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinSackTime", this.MinSackTime);
    this.MinThrowAwayTime = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinThrowAwayTime", this.MinThrowAwayTime);
    this.ThrowAwayChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.ThrowAwayChance", this.ThrowAwayChance);
    this.SpikePlayTime = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.SpikePlayTime", this.SpikePlayTime);
    this.ShortPassDistance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.ShortPassDistance", this.ShortPassDistance);
    this.DeepPassDistance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.DeepPassDistance", this.DeepPassDistance);
    this.MinAccuracyRating = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinAccuracyRating", this.MinAccuracyRating);
    this.MaxAccuracyRating = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxAccuracyRating", this.MaxAccuracyRating);
    this.MaxShortAccuracyChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxShortAccuracyChance", this.MaxShortAccuracyChance);
    this.MinShortAccuracyChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinShortAccuracyChance", this.MinShortAccuracyChance);
    this.MaxDeepAccuracyChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxDeepAccuracyChance", this.MaxDeepAccuracyChance);
    this.MinDeepAccuracyChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinDeepAccuracyChance", this.MinDeepAccuracyChance);
    this.MinInterceptionRating = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinInterceptionRating", this.MinInterceptionRating);
    this.MaxInterceptionRating = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxInterceptionRating", this.MaxInterceptionRating);
    this.MinInterceptionChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MinInterceptionChance", this.MinInterceptionChance);
    this.MaxInterceptionChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxInterceptionChance", this.MaxInterceptionChance);
    this.MaxBreakTackleChance = RemoteConfigGameService.GetFloatValue("GameplayConfig.SidelineSim.MaxBreakTackleChance", this.MaxBreakTackleChance);
  }
}
