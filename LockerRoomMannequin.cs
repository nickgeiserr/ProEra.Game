// Decompiled with JetBrains decompiler
// Type: LockerRoomMannequin
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using System;
using System.Collections;
using UnityEngine;

public class LockerRoomMannequin : MonoBehaviour
{
  public Animator animator;
  public Transform ball;
  public Transform leftHand;
  public Transform leftBallJoint;
  public Transform rightHand;
  public Transform rightBallJoint;
  public float proximityThreshold = 0.15f;
  public float orientation = 325f;
  public float posCompensation = 0.25f;
  public float rotCompensation = 10f;
  [SerializeField]
  private Coroutine _randomizeAnimationRoutine;
  private Quaternion _defaultRotation;
  private Vector3 _defaultPosition;
  private PlayerProfile _playerProfile;
  private AvatarGraphics _avatarGraphics;

  private PlayerProfile playerProfile
  {
    get
    {
      if ((UnityEngine.Object) this._playerProfile == (UnityEngine.Object) null)
        this._playerProfile = SaveManager.GetPlayerProfile();
      return this._playerProfile;
    }
  }

  private TeamBallMatStore _teamBallMaterialStore => SaveManager.GetTeamBallMatStore();

  private AvatarGraphics avatarGraphics
  {
    get
    {
      if ((UnityEngine.Object) this._avatarGraphics == (UnityEngine.Object) null)
        this._avatarGraphics = this.gameObject.GetComponent<AvatarGraphics>();
      return this._avatarGraphics;
    }
  }

  private void OnEnable()
  {
    if (this._randomizeAnimationRoutine != null)
      this.StopCoroutine(this._randomizeAnimationRoutine);
    this._randomizeAnimationRoutine = this.StartCoroutine(this.RandomizeAnimation());
    this._defaultPosition = this.transform.position;
    this.avatarGraphics.SetupSavePlayer(this.playerProfile.Customization);
    this.playerProfile.Customization.BodyModelId.OnValueChanged += new Action<int>(this.BodyModelValueChanged);
    this.playerProfile.Customization.AvatarPresetId.OnValueChanged += new Action<string>(this.AvatarPresetIdOnOnValueChanged);
    this.playerProfile.Customization.MultiplayerTeamBallId.OnValueChanged += new Action<int>(this.TeamBallOnValueChange);
    this.TeamBallOnValueChange((int) this.playerProfile.Customization.MultiplayerTeamBallId);
  }

  private void TeamBallOnValueChange(int a_index)
  {
    ETeamBallID id = (ETeamBallID) a_index;
    MeshRenderer componentInChildren = this.ball.GetComponentInChildren<MeshRenderer>();
    if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
      return;
    Material teamBallMaterial = this._teamBallMaterialStore.GetTeamBallConfig(id).teamBallMaterial;
    componentInChildren.materials = new Material[1]
    {
      teamBallMaterial
    };
  }

  private void AvatarPresetIdOnOnValueChanged(string obj) => this.avatarGraphics.ConfigAvatar(obj);

  private void BodyModelValueChanged(int value) => this.avatarGraphics.SetupBody(value);

  private void OnDisable()
  {
    if ((UnityEngine.Object) this._playerProfile != (UnityEngine.Object) null)
    {
      this._playerProfile.Customization.BodyModelId.OnValueChanged -= new Action<int>(this.BodyModelValueChanged);
      this._playerProfile.Customization.AvatarPresetId.OnValueChanged -= new Action<string>(this.AvatarPresetIdOnOnValueChanged);
      this._playerProfile.Customization.MultiplayerTeamBallId.OnValueChanged -= new Action<int>(this.TeamBallOnValueChange);
    }
    if (this._randomizeAnimationRoutine == null)
      return;
    this.StopCoroutine(this._randomizeAnimationRoutine);
  }

  private void Update()
  {
    if ((double) Vector3.Distance(this.rightHand.position, this.rightBallJoint.position) < (double) this.proximityThreshold || (double) Vector3.Distance(this.leftHand.position, this.leftBallJoint.position) < (double) this.proximityThreshold)
      this.ball.gameObject.SetActive(true);
    if ((double) Vector3.Distance(this.rightHand.position, this.rightBallJoint.position) < (double) this.proximityThreshold && (double) Vector3.Distance(this.leftHand.position, this.leftBallJoint.position) < (double) this.proximityThreshold)
      return;
    this.ball.gameObject.SetActive(false);
  }

  private void LateUpdate()
  {
    if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("A_LookerRoom_Misc_QuaterbackIdle"))
      return;
    this._defaultRotation = Quaternion.Euler(0.0f, this.orientation, 0.0f);
    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, this._defaultRotation, this.rotCompensation * Time.deltaTime);
    this.transform.position = Vector3.MoveTowards(this.transform.position, this._defaultPosition, this.posCompensation * Time.deltaTime);
  }

  private IEnumerator RandomizeAnimation()
  {
    while (true)
    {
      this.animator.SetInteger("AnimationBranch", UnityEngine.Random.Range(0, 2));
      yield return (object) new WaitForSeconds(5f);
    }
  }
}
