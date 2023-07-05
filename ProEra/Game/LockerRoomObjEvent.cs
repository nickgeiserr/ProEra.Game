// Decompiled with JetBrains decompiler
// Type: ProEra.Game.LockerRoomObjEvent
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using System;
using System.Collections;
using TB12;
using TB12.AppStates;
using TB12.UI;
using UnityEngine;

namespace ProEra.Game
{
  [SelectionBase]
  public class LockerRoomObjEvent : MonoBehaviour
  {
    private const float CALL_DELAY = 1f;
    [SerializeField]
    private LockerRoomObjEvent.InteractEvent interactEvent;
    [SerializeField]
    private GameObject _toolTipText;
    private TouchDrag3D interactiveObject;
    private RoutineHandle routineHandle = new RoutineHandle();
    public static int CurrentActiveID = -1;

    private void Awake()
    {
      this.interactiveObject = this.GetComponent<TouchDrag3D>();
      if ((UnityEngine.Object) this.interactiveObject != (UnityEngine.Object) null)
        this.interactiveObject.OnGrabObject += new Action<ITouchInput>(this.OnGrabObject);
      if ((UnityEngine.Object) this.interactiveObject != (UnityEngine.Object) null)
        this.interactiveObject.OnDropObject += new Action<ITouchInput>(this.OnDropObject);
      if (!((UnityEngine.Object) this.interactiveObject != (UnityEngine.Object) null))
        return;
      this.interactiveObject.OnResetObject += new Action<ITouchInput>(this.OnDropObject);
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this.interactiveObject != (UnityEngine.Object) null)
        this.interactiveObject.OnGrabObject -= new Action<ITouchInput>(this.OnGrabObject);
      if ((UnityEngine.Object) this.interactiveObject != (UnityEngine.Object) null)
        this.interactiveObject.OnDropObject -= new Action<ITouchInput>(this.OnDropObject);
      if ((UnityEngine.Object) this.interactiveObject != (UnityEngine.Object) null)
        this.interactiveObject.OnResetObject -= new Action<ITouchInput>(this.OnDropObject);
      LockerRoomObjEvent.CurrentActiveID = -1;
    }

    private void OnEnable() => Debug.Log((object) "enabling locker room obj event");

    private void OnDisable()
    {
    }

    private void OnGrabObject(ITouchInput touchInput)
    {
      if (touchInput != null)
        VRInputManager.SetHaptic(touchInput.dragHand);
      else
        VRInputManager.SetHaptic(EHand.Right);
      int num = Mathf.Abs(this.GetInstanceID());
      if (LockerRoomObjEvent.CurrentActiveID > 0 && LockerRoomObjEvent.CurrentActiveID != num)
        return;
      LockerRoomObjEvent.CurrentActiveID = num;
      if ((UnityEngine.Object) this._toolTipText != (UnityEngine.Object) null)
        this._toolTipText.SetActive(false);
      if ((bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode)
      {
        if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          UIDispatch.RightTopWristScreen2.DisplayView(EScreens.kWristDropItem);
        else
          UIDispatch.LeftTopWristScreen2.DisplayView(EScreens.kWristDropItem);
      }
      if (!VRState.InterationWithUI.Value)
        return;
      this.routineHandle.Stop();
      this.routineHandle.Run(this.CallWithDelay(touchInput));
    }

    private void OnDropObject(ITouchInput touchInput)
    {
      if (this.interactEvent == LockerRoomObjEvent.InteractEvent.Tablet)
      {
        if (touchInput != null && touchInput.dragHand == EHand.Right)
          VRInputManager.LeftLaserInput = false;
        if (touchInput != null && touchInput.dragHand == EHand.Left)
          VRInputManager.RightLaserInput = false;
        if (touchInput == null)
        {
          VRInputManager.LeftLaserInput = false;
          VRInputManager.RightLaserInput = false;
        }
      }
      if ((UnityEngine.Object) this._toolTipText != (UnityEngine.Object) null)
        this._toolTipText.SetActive(true);
      LockerRoomObjEvent.CurrentActiveID = -1;
    }

    public void SimulateClick() => this.OnGrabObject((ITouchInput) null);

    private IEnumerator CallWithDelay(ITouchInput touchInput)
    {
      LockerRoomObjEvent lockerRoomObjEvent = this;
      yield return (object) new WaitForSeconds(1f);
      if ((UnityEngine.Object) GameManager.Instance != (UnityEngine.Object) null && GameManager.Instance.IsTransitioning())
      {
        Debug.LogError((object) "Transitioning Levels");
      }
      else
      {
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Helmet)
        {
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          if (PersistentSingleton<SaveManager>.Instance.SeasonModeDataExists())
          {
            AppState.SeasonMode.Value = ESeasonMode.kLoad;
            SeasonModeManager.self.PlayWeek_Normal();
          }
          else
          {
            AppState.SeasonMode.Value = ESeasonMode.kNew;
            GameplayManager.LoadLevelActivation(EGameMode.kAxisGame, ETimeOfDay.Clear);
          }
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.WaterBottle)
        {
          VREvents.BlinkMovePlayer.Trigger(1f, new Vector3(-2f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 90f, 0.0f)));
          VRState.LocomotionEnabled.SetValue(false);
          UIDispatch.FrontScreen.DisplayView(EScreens.kSettings);
          if ((bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode)
          {
            Debug.Log((object) ("touchInput.dragHand: " + touchInput.dragHand.ToString()));
            ((HandData) touchInput).hand.DropCurrentItem();
            if (touchInput.dragHand == EHand.Left)
              UIDispatch.LeftTopWristScreen2.HideView(EScreens.kWristDropItem);
            else
              UIDispatch.RightTopWristScreen2.HideView(EScreens.kWristDropItem);
          }
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Ball)
        {
          AppState.SeasonMode.Value = ESeasonMode.kUnknown;
          GameplayManager.LoadLevelActivation(EGameMode.kPracticeMode, ETimeOfDay.Clear);
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Keys)
        {
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          SeasonLockerRoom instance = SeasonLockerRoom.Instance;
          if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
          {
            Transform trophyRoomPlayerPivot = instance.GetTrophyRoomPlayerPivot();
            if ((UnityEngine.Object) trophyRoomPlayerPivot != (UnityEngine.Object) null)
              VREvents.BlinkMovePlayer.Trigger(1f, trophyRoomPlayerPivot.position, trophyRoomPlayerPivot.rotation);
          }
        }
        int interactEvent1 = (int) lockerRoomObjEvent.interactEvent;
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Tablet)
        {
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          if (touchInput != null && touchInput.dragHand == EHand.Right)
            VRInputManager.LeftLaserInput = true;
          if (touchInput != null && touchInput.dragHand == EHand.Left)
            VRInputManager.RightLaserInput = true;
        }
        int interactEvent2 = (int) lockerRoomObjEvent.interactEvent;
        int interactEvent3 = (int) lockerRoomObjEvent.interactEvent;
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Shoes)
        {
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          UIDispatch.DisplayCAP();
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.TrainingCamp)
        {
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          AppState.SeasonMode.Value = ESeasonMode.kUnknown;
          GameplayManager.LoadLevelActivation(EGameMode.kTrainingCamp, ETimeOfDay.Clear);
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Onboarding)
        {
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          AppState.SeasonMode.Value = ESeasonMode.kUnknown;
          GameplayManager.LoadLevelActivation(EGameMode.kOnboarding, ETimeOfDay.Clear);
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.Credits)
        {
          VREvents.BlinkMovePlayer.Trigger(1f, new Vector3(-2f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
          UIDispatch.FrontScreen.DisplayView(EScreens.kCredits);
        }
        if (lockerRoomObjEvent.interactEvent == LockerRoomObjEvent.InteractEvent.VacationTickets)
        {
          UIDispatch.FrontScreen.DisplayView(EScreens.kContinueSeasonOrNew);
          UnityEngine.Object.Destroy((UnityEngine.Object) lockerRoomObjEvent.gameObject, 3f);
        }
      }
    }

    public enum InteractEvent
    {
      Undefined,
      Helmet,
      WaterBottle,
      Ball,
      Keys,
      Pad,
      Tablet,
      TrophyHat,
      TrophyBall,
      Shoes,
      TrainingCamp,
      Onboarding,
      Credits,
      VacationTickets,
      CoachesNotes,
    }
  }
}
