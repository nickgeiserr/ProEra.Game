// Decompiled with JetBrains decompiler
// Type: FootballVR.ThumbstickLocomotion
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace FootballVR
{
  public class ThumbstickLocomotion
  {
    private readonly RoutineHandle _locomotionRoutine = new RoutineHandle();
    private float _currentSpeed;
    private bool _state;
    private Transform _leftHandDir;
    private Transform _rightHandDir;
    private const string lockerRoomSceneName = "LockerRoomUI";
    private bool playerIsInLockerRoom;
    private string _activeSceneName = string.Empty;
    private static readonly string DebugJewelCaseRoom = nameof (DebugJewelCaseRoom);
    private Vector3 movingDirection;

    public static float LocomotionTimeSinceSceneLoad { get; private set; }

    public ThumbstickLocomotion()
    {
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
      SceneManager.sceneUnloaded += new UnityAction<Scene>(this.OnSceneUnloaded);
    }

    ~ThumbstickLocomotion()
    {
      SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
      SceneManager.sceneUnloaded -= new UnityAction<Scene>(this.OnSceneUnloaded);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      string name = scene.name;
      if (name.Contains("LockerRoomUI"))
        this.playerIsInLockerRoom = true;
      if (string.IsNullOrEmpty(this._activeSceneName) || SceneManager.GetActiveScene() == scene)
        this._activeSceneName = name;
      ThumbstickLocomotion.LocomotionTimeSinceSceneLoad = 0.0f;
    }

    private void OnSceneUnloaded(Scene scene)
    {
      if (!scene.name.Contains("LockerRoomUI"))
        return;
      this.playerIsInLockerRoom = false;
    }

    public void Setup()
    {
    }

    public void SetState(bool state)
    {
      if (state == this._state)
        return;
      this._state = state;
      if (state)
      {
        this._locomotionRoutine.Run(this.LocomotionRoutine());
      }
      else
      {
        this._currentSpeed = 0.0f;
        this._locomotionRoutine.Stop();
      }
    }

    private IEnumerator LocomotionRoutine()
    {
      ThumbstickLocomotionSettings settings = ScriptableSingleton<LocomotionSettings>.Instance.ThumbstickLocomotionSettings;
      Transform camTx = PlayerCamera.Camera.transform;
      while (true)
      {
        if (this._activeSceneName == ThumbstickLocomotion.DebugJewelCaseRoom && PersistentSingleton<GamePlayerController>.Instance.IsNoClip)
        {
          int num = (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, VRInputManager.Controller.LeftHand) > 0.10000000149011612 ? 1 : 0;
          bool flag = (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, VRInputManager.Controller.RightHand) > 0.10000000149011612;
          if (num != 0)
            this.movingDirection = this.movingDirection.SetY(2f);
          else if (flag)
            this.movingDirection = this.movingDirection.SetY(-2f);
          if ((num | (flag ? 1 : 0)) != 0)
          {
            PersistentSingleton<GamePlayerController>.Instance.Move(this.movingDirection * (this._currentSpeed * Time.deltaTime));
            float unscaledDeltaTime = Time.unscaledDeltaTime;
            this._currentSpeed -= (float) ((double) Mathf.Abs(this._currentSpeed) * (double) settings.decelerationRate * (double) unscaledDeltaTime + (double) settings.decelerationConstant * (double) unscaledDeltaTime);
            yield return (object) null;
          }
        }
        Vector2 vector2_1 = VRInputManager.Get(VRInputManager.Axis2D.Primary2DAxis, VRInputManager.Controller.LeftHand);
        Vector2 vector2_2 = !this.playerIsInLockerRoom || VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand) ? VRInputManager.Get(VRInputManager.Axis2D.Primary2DAxis, VRInputManager.Controller.RightHand) : Vector2.zero;
        Vector2 vector2_3 = (double) vector2_1.magnitude > (double) vector2_2.magnitude ? vector2_1 : vector2_2;
        if ((double) vector2_3.sqrMagnitude > (double) settings.buttonPressThreshold)
        {
          float num = 1f;
          if (ControllerInput.IsSprintInputPressed(ControllerInput.InputCheckType.Either) && (double) vector2_3.y > 0.0)
            num = MathUtils.MapRange(Vector3.Angle(Vector3.forward, new Vector3(vector2_3.x, 0.0f, vector2_3.y)), 0.0f, 90f, settings.sprintAccelerationMultiplier, 1f);
          this._currentSpeed += vector2_3.magnitude * settings.acceleration * Time.deltaTime * num;
          Vector3 normalized1 = camTx.forward.SetY(0.0f).normalized;
          Vector3 normalized2 = camTx.right.SetY(0.0f).normalized;
          this.movingDirection = (normalized1 * vector2_3.y + normalized2 * vector2_3.x).normalized;
        }
        if (VRState.PlayerOneDropbackActive)
        {
          this._currentSpeed = settings.autoDropBackSpeed;
          this.movingDirection = new Vector3(0.0f, 0.0f, GameSettings.OffenseGoingNorth.Value ? -1f : 1f);
        }
        if (!VRState.PlayerOneDropbackActive)
          this._currentSpeed = Mathf.Clamp(this._currentSpeed, 0.0f, settings.maxSpeed);
        if ((double) this._currentSpeed > 9.9999997473787516E-06)
        {
          PersistentSingleton<GamePlayerController>.Instance.Move(this.movingDirection * (this._currentSpeed * Time.deltaTime));
          float unscaledDeltaTime = Time.unscaledDeltaTime;
          this._currentSpeed -= (float) ((double) Mathf.Abs(this._currentSpeed) * (double) settings.decelerationRate * (double) unscaledDeltaTime + (double) settings.decelerationConstant * (double) unscaledDeltaTime);
        }
        if ((double) Mathf.Abs(this._currentSpeed) > 0.0099999997764825821)
          ThumbstickLocomotion.LocomotionTimeSinceSceneLoad += Time.deltaTime;
        yield return (object) null;
      }
    }

    private void OnActiveSceneChanged(Scene current, Scene next) => this._activeSceneName = next.name;
  }
}
