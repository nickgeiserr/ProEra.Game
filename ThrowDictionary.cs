// Decompiled with JetBrains decompiler
// Type: ThrowDictionary
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ThrowDictionary", fileName = "ThrowDictionary")]
public class ThrowDictionary : ScriptableObject
{
  [SerializeField]
  private ThrowDictionary.ThrowSet _shortThrows;
  [SerializeField]
  private float _maxShortThrowDistance = 20f;
  [SerializeField]
  private ThrowDictionary.ThrowSet _mediumThrows;
  [SerializeField]
  private float _maxMediumThrowDistance = 40f;
  [SerializeField]
  private ThrowDictionary.ThrowSet _longThrows;

  public AnimationEventController GetThrow(
    Transform throwerTransform,
    Vector3 targetPosition,
    bool leftHanded)
  {
    Vector3 position = throwerTransform.position;
    float num1 = Vector3.Distance(position.SetY(0.0f), targetPosition.SetY(0.0f));
    ThrowDictionary.ThrowSet throwSet = (double) num1 >= (double) this._maxShortThrowDistance ? ((double) num1 >= (double) this._maxMediumThrowDistance ? this._longThrows : this._mediumThrows) : this._shortThrows;
    float num2 = Vector3.SignedAngle((leftHanded ? 1f : -1f) * throwerTransform.right.SetY(0.0f).normalized, (targetPosition - position).SetY(0.0f).normalized, Vector3.up);
    AnimationEventController animationEventController = throwSet.frontThrowD1;
    if ((double) num2 > 67.5 && (double) num2 < 180.0)
      animationEventController = throwSet.farRightThrowD3;
    else if ((double) num2 > 22.5 && (double) num2 < 67.5)
      animationEventController = throwSet.rightThrowD2;
    else if ((double) num2 > -22.5 && (double) num2 < 22.5)
      animationEventController = throwSet.frontThrowD1;
    else if ((double) num2 > -67.5 && (double) num2 < -22.5)
      animationEventController = throwSet.leftThrowD8;
    else if ((double) num2 > -180.0 || (double) num2 < -67.5)
      animationEventController = throwSet.farLeftThrowD7;
    return animationEventController;
  }

  [Serializable]
  private class ThrowSet
  {
    public AnimationEventController farLeftThrowD7;
    public AnimationEventController leftThrowD8;
    public AnimationEventController frontThrowD1;
    public AnimationEventController rightThrowD2;
    public AnimationEventController farRightThrowD3;
  }
}
