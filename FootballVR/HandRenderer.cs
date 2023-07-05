// Decompiled with JetBrains decompiler
// Type: FootballVR.HandRenderer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using OVRTouchSample;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class HandRenderer : MonoBehaviour
  {
    [SerializeField]
    private GlovesConfigStore _glovesConfigStore;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private OVRInput.Controller m_controller;
    [SerializeField]
    private HandPose m_defaultGrabPose;
    [SerializeField]
    private Transform _realisticAttachPoint;
    [SerializeField]
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private GameObject _controllerGraphics;
    [SerializeField]
    private GameObject _controllerAnnotations;
    public readonly VariableBool Fist = new VariableBool();
    private const string ANIM_LAYER_NAME_POINT = "Point Layer";
    private const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    private const string ANIM_PARAM_NAME_FLEX = "Flex";
    private const string ANIM_PARAM_NAME_POSE = "Pose";
    private const float INPUT_RATE_CHANGE = 20f;
    private static readonly int HandPoseId = Animator.StringToHash(nameof (HandPoseId));
    private static readonly int Pinch = Animator.StringToHash(nameof (Pinch));
    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;
    private bool _isPointing;
    private bool _isGivingThumbsUp;
    protected float _pointBlend;
    protected float _thumbsUpBlend;
    protected float _flex;
    protected float _pinch;
    private bool _initialized;

    public Transform attachPoint => this._realisticAttachPoint;

    public Renderer Renderer => (Renderer) this._renderer;

    private void Start() => this.Initialize();

    public void Initialize()
    {
      if (this._initialized)
        return;
      this.m_animLayerIndexPoint = this._animator.GetLayerIndex("Point Layer");
      this.m_animLayerIndexThumb = this._animator.GetLayerIndex("Thumb Layer");
      this.m_animParamIndexFlex = Animator.StringToHash("Flex");
      this.m_animParamIndexPose = Animator.StringToHash("Pose");
      this._initialized = true;
    }

    protected virtual void Update()
    {
      this.GetInput();
      this.UpdateAnimStates();
    }

    protected void GetInput()
    {
      if (!VRState.InterationWithUI.Value)
        return;
      VRInputManager.Controller controller = this.m_controller == OVRInput.Controller.LTouch ? VRInputManager.Controller.LeftHand : VRInputManager.Controller.RightHand;
      this._isGivingThumbsUp = !VRInputManager.Get(VRInputManager.Button.Primary2DAxisTouch, controller) && !VRInputManager.Get(VRInputManager.Button.PrimaryTouch, controller) && !VRInputManager.Get(VRInputManager.Button.SecondaryTouch, controller);
      this._flex = VRInputManager.Get(VRInputManager.Axis1D.Grip, controller);
      this._pinch = VRInputManager.Get(VRInputManager.Axis1D.Trigger, controller);
      this._isPointing = !VRInputManager.Get(VRInputManager.Button.TriggerTouch, controller);
      this._pointBlend = this.InputValueRateChange(this._isPointing, this._pointBlend);
      this._thumbsUpBlend = this.InputValueRateChange(this._isGivingThumbsUp, this._thumbsUpBlend);
    }

    private float InputValueRateChange(bool isDown, float value)
    {
      float num1 = Time.deltaTime * 20f;
      float num2 = isDown ? 1f : -1f;
      return Mathf.Clamp01(value + num1 * num2);
    }

    protected void UpdateAnimStates()
    {
      this._animator.SetInteger(this.m_animParamIndexPose, (int) this.m_defaultGrabPose.PoseId);
      this._animator.SetFloat(this.m_animParamIndexFlex, this._flex);
      this._animator.SetLayerWeight(this.m_animLayerIndexPoint, this._pointBlend);
      this._animator.SetLayerWeight(this.m_animLayerIndexThumb, this._thumbsUpBlend);
      this._animator.SetFloat(HandRenderer.Pinch, this._pinch);
      this.Fist.Value = (double) this._flex > 0.89999997615814209;
    }

    public void SetRenderState(bool visible) => this._renderer.enabled = visible;

    public void SetHandPose(EHandPose handPose) => this._animator.SetInteger(HandRenderer.HandPoseId, (int) handPose);

    public void SetGraphics(bool controllerGraphics) => this._animator.gameObject.SetActive(!controllerGraphics);

    public void SetControllerAnnotations(bool annotationsEnabled)
    {
      if (!((Object) this._controllerAnnotations != (Object) null))
        return;
      this._controllerAnnotations.SetActive(annotationsEnabled);
    }

    public void ApplyCustomization(int configId, ETeamUniformId teamUniformId)
    {
      GlovesConfig glovesConfig = this._glovesConfigStore.GetGlovesConfig((EGlovesId) configId);
      GlovesMeshConfig meshConfig = this._glovesConfigStore.GetMeshConfig(glovesConfig.MeshId);
      bool flag = this.m_controller == OVRInput.Controller.RTouch;
      FootballWorld.UniformConfig uniformConfig = SaveManager.GetUniformStore().GetUniformConfig(teamUniformId, ETeamUniformFlags.Home);
      Color[] colorsMask = new Color[3]
      {
        uniformConfig.TintRed,
        uniformConfig.TintGreen,
        uniformConfig.TintBlue
      };
      this.ApplyCustomization(flag ? glovesConfig.RightHandMaterial : glovesConfig.LeftHandMaterial, flag ? meshConfig.RightMesh : meshConfig.LeftMesh, colorsMask);
    }

    public void ApplyCustomization(Material mat, UnityEngine.Mesh mesh, Color[] colorsMask)
    {
      this.Initialize();
      this._renderer.sharedMaterial = mat;
      this._renderer.sharedMesh = mesh;
      MaterialPropertyBlock properties = new MaterialPropertyBlock();
      this._renderer.GetPropertyBlock(properties);
      properties.SetColor(GlovesConfigStore.kGloveTintR, colorsMask[0]);
      properties.SetColor(GlovesConfigStore.kGloveTintG, colorsMask[1]);
      properties.SetColor(GlovesConfigStore.kGloveTintB, colorsMask[2]);
      this._renderer.SetPropertyBlock(properties);
    }
  }
}
