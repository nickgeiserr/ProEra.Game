// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerCollisionHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Sequences;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class PlayerCollisionHandler : MonoBehaviour
  {
    [SerializeField]
    private HandsDataModel _handsDataModel;
    [SerializeField]
    private ArmSwingLocomotion _locomotion;
    [SerializeField]
    private CollisionEffect _collisionEffect;
    [SerializeField]
    private UserBodyCollider _userBodyCollider;
    private Transform _gamePlayerTx;
    private Transform _camTx;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly UnityEngine.RaycastHit[] _rayHitCache = new UnityEngine.RaycastHit[50];
    private WallCollisionMechanic _wallCollision;
    private TackleMechanic _tackleMechanic;
    private bool _incapacitated;
    public static readonly VariableBool IsDown = new VariableBool();
    [EditorSetting(ESettingType.Utility)]
    private static bool multiplayerAutoGetUp = true;
    private readonly RoutineHandle _pushRoutine = new RoutineHandle();
    private readonly RoutineHandle _tackleRoutine = new RoutineHandle();

    private CollisionSettings _settings => ScriptableSingleton<CollisionSettings>.Instance;

    private void Awake()
    {
      this._camTx = PlayerCamera.Camera.transform;
      this._gamePlayerTx = PersistentSingleton<GamePlayerController>.Instance.transform;
      this._wallCollision = new WallCollisionMechanic(this._camTx, this._settings.wallCollisionSettings, this._rayHitCache);
      this._tackleMechanic = new TackleMechanic(this._camTx, this._settings.tackleSettings, this._handsDataModel, this._rayHitCache);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VRState.CollisionEnabled.Link<bool>((Action<bool>) (state => this._userBodyCollider.CollisionEnabled = state)),
        VREvents.GetUp.Link(new System.Action(this.HandleGetUp)),
        VREvents.PlayerCollision.Link<GameObject>((Action<GameObject>) (collisionObject =>
        {
          this._collisionEffect.Run();
          this._tackleRoutine.Run(this.TackleRoutine(collisionObject));
        })),
        VREvents.PlayerPushed.Link<Vector3, bool>((Action<Vector3, bool>) ((impulse, knockdown) => this.ApplyPush(impulse, knockdown))),
        VREvents.PlayerKnockdown.Link<Vector3>((Action<Vector3>) (impulse => this._collisionEffect.RunWithFall(impulse)))
      });
      this._userBodyCollider.OnCollision += new Action<Collider>(VREvents.UserCollision.Trigger);
      this._collisionEffect.OnFallDown += new System.Action(this.HandleFallDown);
      PersistentSingleton<GamePlayerController>.Instance.InitializeCollision(this);
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void Update()
    {
      if (!this._incapacitated || (double) this._camTx.position.y >= 0.10000000149011612)
        return;
      float num = 0.1f - this._camTx.position.y;
      Vector3 position = this._gamePlayerTx.position;
      position.y += num;
      this._gamePlayerTx.position = position;
      if ((double) this._camTx.position.y >= 0.10000000149011612)
        return;
      VREvents.PlayerHitGround.Trigger();
    }

    private void HandleFallDown()
    {
      this._incapacitated = true;
      PlayerCollisionHandler.IsDown.SetValue(true);
    }

    private void HandleGetUp() => this.StartCoroutine(this.GetUpRoutine());

    private IEnumerator GetUpRoutine()
    {
      PlayerCollisionHandler.IsDown.SetValue(false);
      yield return (object) GamePlayerController.CameraFade.Fade(this._settings.GetUpFadeDuration);
      PersistentSingleton<GamePlayerController>.Instance.ResetRotation();
      this.ResetState();
      GamePlayerController.CameraFade.Clear(this._settings.GetUpFadeDuration);
    }

    public bool TryMove(Vector3 moveVector)
    {
      if ((bool) (VariableBool) VRState.BigSizeMode)
        return true;
      if (this._incapacitated)
      {
        this._locomotion.CurrentSpeed = 0.0f;
        return false;
      }
      if (!this._settings.WallCollisionEnabled || !this._wallCollision.HitsAnyWalls(moveVector))
        return true;
      this._locomotion.CurrentSpeed = 0.0f;
      return false;
    }

    public void ApplyPush(Vector3 impulseVector, bool knockDown, bool forceDropBall = false) => this._pushRoutine.Run(this.ApplyPushRoutine(impulseVector, knockDown, forceDropBall));

    private IEnumerator ApplyPushRoutine(Vector3 impulseVector, bool knockDown, bool forceDropBall = false)
    {
      this._incapacitated = true;
      if (knockDown & forceDropBall)
        this._handsDataModel.ResetHandsState();
      if (knockDown)
      {
        this._collisionEffect.RunWithFall(impulseVector, impulseVector);
      }
      else
      {
        this._collisionEffect.Run(impulseVector);
        this._incapacitated = false;
      }
      yield return (object) new WaitForSeconds(4f);
    }

    private IEnumerator TackleRoutine(GameObject collisionObject)
    {
      Debug.Log((object) "Tackle Detected...");
      if ((bool) ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled)
      {
        Debug.Log((object) "Running Immersive Tackle...");
        TackleSettings tackleSettings = this._settings.tackleSettings;
        BulletTimeInfo bulletTime = GamePlayerController.BulletTime;
        TackleMechanic.TackleResult result;
        if (this._tackleMechanic.HitPlayerResult(collisionObject, this._locomotion.normalizedSpeed, out result))
        {
          Debug.Log((object) "Tackle is Valid!");
          float num = result.highImpact ? tackleSettings.pushImpactIntensity : tackleSettings.pushIntensity * Time.deltaTime;
          Vector3 vector3 = num * result.impactVector;
          Debug.Log((object) string.Format("<b>impactCoef is {0} and tackleResult.impactVector is {1}</b>", (object) num, (object) result.impactVector));
          bulletTime.EnterBulletTime();
          this._locomotion.CurrentSpeed = 0.0f;
          yield return (object) this.ApplyPushRoutine(-result.impactVector, true);
          bulletTime.ExitBulletTime();
          Debug.Log((object) "<b>Out of Bullet Time</b>");
        }
        else
          Debug.Log((object) "lol no");
        bulletTime = (BulletTimeInfo) null;
      }
    }

    public void ResetState()
    {
      this._collisionEffect.Stop();
      this._incapacitated = false;
    }
  }
}
