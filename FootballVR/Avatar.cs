// Decompiled with JetBrains decompiler
// Type: FootballVR.Avatar
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using RootMotion.FinalIK;
using UnityEngine;

namespace FootballVR
{
  [SelectionBase]
  public class Avatar : MonoBehaviour
  {
    [SerializeField]
    private GameObject _ikSetup;
    [SerializeField]
    private PlayerCollision _playerCollision;
    [SerializeField]
    private PlayerIdentity _identity;
    [SerializeField]
    private AvatarGraphics _avatarGraphics;
    [SerializeField]
    private Transform _spotTarget;
    [SerializeField]
    private AvatarSkinData _avatarSkinData;
    [SerializeField]
    private Transform _headTx;
    [SerializeField]
    private HandPoser[] _handPosers;
    [EditorSetting(ESettingType.Utility)]
    private static bool forceAvatarCollisionPhysics;
    public BehaviourController behaviourController;

    public Transform HeadTx => this._headTx;

    public Vector3 SpotTarget => this._spotTarget.position;

    public PlayerIdentity Identity => this._identity;

    public int DataKey { get; set; }

    public AvatarGraphics Graphics => this._avatarGraphics;

    public EOutlineMode Outline
    {
      set => this._avatarGraphics.avatarGraphicsData.outlineMode.Value = value;
    }

    private void Awake()
    {
      this._avatarGraphics.Initialize();
      if (ScriptableSingleton<AvatarsSettings>.Instance.PlayersCollisionPhysics || Avatar.forceAvatarCollisionPhysics)
        return;
      Objects.SafeDestroy((Object) this.GetComponent<Rigidbody>());
      Objects.SafeDestroy((Object) this.GetComponent<Collider>());
      foreach (Object bodyCollider in this._playerCollision.bodyColliders)
        Objects.SafeDestroy(bodyCollider);
      foreach (Object handPoser in this._handPosers)
        Objects.SafeDestroy(handPoser);
      Objects.SafeDestroy((Object) this.GetComponent<InteractionSystem>());
      Objects.SafeDestroy((Object) this.GetComponent<BallCatchInteraction>());
      Objects.SafeDestroy((Object) this._playerCollision);
      Objects.SafeDestroy((Object) this._playerCollision.fullBodyBipedIK);
      Objects.SafeDestroy((Object) this._ikSetup);
    }

    public void Deinit()
    {
      this.DataKey = 0;
      if (!((Object) this != (Object) null) || !((Object) this.gameObject != (Object) null))
        return;
      this.gameObject.SetActive(false);
    }

    public void Appear(float duration = 2f, float delay = 0.5f) => this._avatarGraphics.Appear(duration, delay);

    public void Disappear(float duration = 2f, float delay = 1f) => this._avatarGraphics.Disappear(duration, delay);
  }
}
