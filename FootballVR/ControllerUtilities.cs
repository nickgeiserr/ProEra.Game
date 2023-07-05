// Decompiled with JetBrains decompiler
// Type: FootballVR.ControllerUtilities
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace FootballVR
{
  public class ControllerUtilities
  {
    private static IEnumerator VibrateEffectRoutine(
      InputDevice xrController,
      bool activate,
      float duration)
    {
      float startTime = Time.realtimeSinceStartup;
      float endTime = Time.realtimeSinceStartup + duration;
      if (activate)
      {
        while ((double) endTime > (double) Time.realtimeSinceStartup)
        {
          xrController.SendHapticImpulse(0U, Mathf.InverseLerp(startTime, endTime, Time.realtimeSinceStartup) * 1000f, 0.03f);
          yield return (object) null;
        }
      }
      else
      {
        while ((double) endTime > (double) Time.realtimeSinceStartup)
        {
          xrController.SendHapticImpulse(0U, (1f - Mathf.InverseLerp(startTime, endTime, Time.realtimeSinceStartup)) * 1000f, 0.03f);
          yield return (object) null;
        }
      }
    }

    private static Vector3 GetAngularVelocity(EHand controller)
    {
      Vector3 angularVelocity = Vector3.zero;
      List<XRNodeState> xrNodeStateList = new List<XRNodeState>();
      InputTracking.GetNodeStates(xrNodeStateList);
      XRNode xrNode = ControllerUtilities.GetXRNode(controller);
      foreach (XRNodeState xrNodeState in xrNodeStateList)
      {
        if (xrNodeState.nodeType == xrNode)
          xrNodeState.TryGetAngularVelocity(out angularVelocity);
      }
      return angularVelocity;
    }

    private static XRNode GetXRNode(EHand controller) => controller != EHand.Right ? XRNode.LeftHand : XRNode.RightHand;

    public static void InteractionHaptics(
      EHand controller,
      bool acceleratingVibration,
      float duration)
    {
      InputDevice deviceAtXrNode = InputDevices.GetDeviceAtXRNode(ControllerUtilities.GetXRNode(controller));
      if (!deviceAtXrNode.isValid)
        return;
      HapticCapabilities capabilities;
      deviceAtXrNode.TryGetHapticCapabilities(out capabilities);
      if (!capabilities.supportsImpulse)
        return;
      RoutineRunner.StartRoutine(ControllerUtilities.VibrateEffectRoutine(deviceAtXrNode, acceleratingVibration, duration));
    }

    public static void InteractionHaptics2Hands(bool acceleratingVibration, float duration)
    {
      ControllerUtilities.InteractionHaptics(EHand.Left, acceleratingVibration, duration);
      ControllerUtilities.InteractionHaptics(EHand.Right, acceleratingVibration, duration);
    }
  }
}
