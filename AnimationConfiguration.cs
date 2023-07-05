// Decompiled with JetBrains decompiler
// Type: AnimationConfiguration
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MxM;
using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewAnimationConfiguration", menuName = "MxM/(Extention) AnimationConfiguration")]
public class AnimationConfiguration : ScriptableObject
{
  public string calibrationProfile = "General";
  public EMxMRootMotion rootMotion = EMxMRootMotion.RootMotionApplicator;
  [Range(-1f, 1f)]
  public float timeLegUp;
  [Range(0.0f, 3f)]
  public float clampRange = 1f;
  [Range(0.0f, 100f)]
  public float pullStrength = 9f;
  [Range(0.0f, 1f)]
  public float iKLockPullDiscount = 0.666f;
  [Range(0.0f, 2f)]
  public float inputMagnitudeDeadZone = 0.105f;
  private MxMAnimator _mxmAnimator;

  public void ApplyConfiguration(GameObject playerGO)
  {
    this._mxmAnimator = playerGO.GetComponent<MxMAnimator>();
    this._mxmAnimator.RootMotion = this.rootMotion;
    this._mxmAnimator.OnSetupComplete.AddListener(new UnityAction(this.OnSetupCompleteHandler));
    playerGO.GetComponent<RootMotionApplicator>().range = this.clampRange;
  }

  private async void OnSetupCompleteHandler()
  {
    AnimationConfiguration animationConfiguration = this;
    // ISSUE: reference to a compiler-generated method
    await TaskEx.WaitUntil(new Func<bool>(animationConfiguration.\u003COnSetupCompleteHandler\u003Eb__9_0));
    animationConfiguration._mxmAnimator.SetCalibrationData(animationConfiguration.calibrationProfile);
  }
}
