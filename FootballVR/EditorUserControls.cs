// Decompiled with JetBrains decompiler
// Type: FootballVR.EditorUserControls
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR
{
  public class EditorUserControls : MonoBehaviour
  {
    public static Action OnSnapCalled;
    public static Action<float> OnKeyboardThrowCalled;
    public static Action OnReplayCalled;
    public static Action<int> OnAudibleScroll;
    public static Action OnConfirmPlay;
    public static Action OnHurryUpKeyPressed;
    public static Action OnTimeOutKeyPressed;
  }
}
