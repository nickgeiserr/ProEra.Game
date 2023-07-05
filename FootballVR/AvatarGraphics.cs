// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarGraphics
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class AvatarGraphics : MonoBehaviour
  {
    public CharacterCustomizationStore.Gender avatarGender;
    private CharacterCustomizationStore _characterStore;
    [SerializeField]
    private bool _isPlayer;
    [SerializeField]
    private LODGroup _lODGroup;
    [SerializeField]
    protected List<Renderer> _renderers;
    [SerializeField]
    private Renderer[] _shadowRenderers;
    [SerializeField]
    private VariableBool _noGraphics;
    [SerializeField]
    private Renderer[] _lowerLegShadows;
    [SerializeField]
    private Renderer[] _upperLegShadows;
    [SerializeField]
    private Renderer[] _upperArmShadows;
    [SerializeField]
    private Renderer[] _lowerArmShadows;
    [SerializeField]
    private Renderer[] _handShadows;
    [SerializeField]
    private Renderer[] _bodyShadows;
    [SerializeField]
    private Renderer[] _headShadows;
    [SerializeField]
    private Renderer[] _hipShadows;
    [SerializeField]
    private Renderer[] _feetShadows;
    [Space(15f)]
    [Header("Avatar config")]
    [SerializeField]
    private SkinnedMeshRenderer[] headRenderer;
    [SerializeField]
    private SkinnedMeshRenderer[] bodyRenderer;
    [SerializeField]
    protected AvatarGraphicsData _avatarGraphicsData;
    private MaterialPropertyBlock _mpb;
    private Material _ambientShadowMat;
    private readonly RoutineHandle _appearRoutine = new RoutineHandle();
    private bool _initialized;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private static readonly int OffsetX = Shader.PropertyToID("_OffsetX");
    private static readonly int OffsetY = Shader.PropertyToID("_OffsetY");
    private static readonly int ScaleX = Shader.PropertyToID("_ScaleX");
    private static readonly int ScaleY = Shader.PropertyToID("_ScaleY");
    private CharacterParameters _currentCharacterParam;

    public CharacterCustomizationStore charactersStore
    {
      get
      {
        if ((UnityEngine.Object) this._characterStore == (UnityEngine.Object) null)
          this._characterStore = this.avatarGender != CharacterCustomizationStore.Gender.Male ? SaveManager.GetCharacterCustomizationStoreFemale() : SaveManager.GetCharacterCustomizationStoreMale();
        return this._characterStore;
      }
      set => this._characterStore = value;
    }

    public AvatarGraphicsData avatarGraphicsData
    {
      get
      {
        if ((UnityEngine.Object) this._avatarGraphicsData == (UnityEngine.Object) null)
          this._avatarGraphicsData = ScriptableObject.CreateInstance<AvatarGraphicsData>();
        return this._avatarGraphicsData;
      }
      set
      {
        this._linksHandler.Clear();
        this._avatarGraphicsData = value;
        this.SetupLinks();
      }
    }

    private MaterialPropertyBlock Mpb => this._mpb ?? (this._mpb = new MaterialPropertyBlock());

    public VariableBool NoGraphics => this._noGraphics;

    public IReadOnlyList<Renderer> Renderers => (IReadOnlyList<Renderer>) this._renderers;

    public IReadOnlyList<Renderer> ShadowRenderers => (IReadOnlyList<Renderer>) this._shadowRenderers;

    private void SetupLinks()
    {
      GameGraphicsSettings instance = ScriptableSingleton<GameGraphicsSettings>.Instance;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        instance.PlanarShadows.Link<bool>(new Action<bool>(this.HandlePlanarShadows)),
        instance.AmbientShadows.Link<bool>(new Action<bool>(this.HandleAmbientShadows)),
        this.avatarGraphicsData.baseMap.Link<Texture2D>(new Action<Texture2D>(this.SetBasemap)),
        this.avatarGraphicsData.uniformCaptureInfo.Link<UniformCapture.Info>(new Action<UniformCapture.Info>(this.ApplyUniform)),
        this.avatarGraphicsData.outlineMode.Link<EOutlineMode>(new Action<EOutlineMode>(this.SetOutline)),
        this._noGraphics.Link<bool>(new Action<bool>(this.HandleNoGraphics))
      });
    }

    private void Awake() => this.Initialize();

    public void Initialize()
    {
      if (this._initialized)
        return;
      this.InitializeShadowRenderers();
      this._initialized = true;
    }

    private void HandleNoGraphics(bool noGraphics)
    {
      foreach (Renderer renderer in this._renderers)
      {
        if (!this._isPlayer && !(renderer is MeshRenderer))
          renderer.enabled = !noGraphics;
      }
      foreach (Renderer shadowRenderer in this._shadowRenderers)
        shadowRenderer.enabled = !noGraphics;
    }

    public void EnableShadows(bool enabled)
    {
      foreach (Component upperLegShadow in this._upperLegShadows)
        upperLegShadow.gameObject.SetActive(enabled);
      foreach (Component lowerLegShadow in this._lowerLegShadows)
        lowerLegShadow.gameObject.SetActive(enabled);
      foreach (Component upperArmShadow in this._upperArmShadows)
        upperArmShadow.gameObject.SetActive(enabled);
      foreach (Component lowerArmShadow in this._lowerArmShadows)
        lowerArmShadow.gameObject.SetActive(enabled);
      foreach (Component handShadow in this._handShadows)
        handShadow.gameObject.SetActive(enabled);
      foreach (Component bodyShadow in this._bodyShadows)
        bodyShadow.gameObject.SetActive(enabled);
      foreach (Component headShadow in this._headShadows)
        headShadow.gameObject.SetActive(enabled);
      foreach (Component hipShadow in this._hipShadows)
        hipShadow.gameObject.SetActive(enabled);
      foreach (Component feetShadow in this._feetShadows)
        feetShadow.gameObject.SetActive(enabled);
    }

    private void InitializeShadowRenderers()
    {
      MaterialPropertyBlock mpb = this.Mpb;
      foreach (Renderer upperLegShadow in this._upperLegShadows)
      {
        upperLegShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.329f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.0f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.461f);
        mpb.SetFloat(AvatarGraphics.ScaleY, -0.12f);
        upperLegShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer lowerLegShadow in this._lowerLegShadows)
      {
        lowerLegShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.0f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.0f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.51f);
        mpb.SetFloat(AvatarGraphics.ScaleY, 0.0f);
        lowerLegShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer upperArmShadow in this._upperArmShadows)
      {
        upperArmShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.329f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.0f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.419f);
        mpb.SetFloat(AvatarGraphics.ScaleY, -0.19f);
        upperArmShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer lowerArmShadow in this._lowerArmShadows)
      {
        lowerArmShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.0f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.0f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.454f);
        mpb.SetFloat(AvatarGraphics.ScaleY, -0.04f);
        lowerArmShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer handShadow in this._handShadows)
      {
        handShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.686f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.0f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.461f);
        mpb.SetFloat(AvatarGraphics.ScaleY, -0.18f);
        handShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer bodyShadow in this._bodyShadows)
      {
        bodyShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.328f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.358f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.769f);
        mpb.SetFloat(AvatarGraphics.ScaleY, 0.03f);
        bodyShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer headShadow in this._headShadows)
      {
        headShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.0f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.638f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.56f);
        mpb.SetFloat(AvatarGraphics.ScaleY, 0.0f);
        headShadow.SetPropertyBlock(this._mpb);
      }
      foreach (Renderer hipShadow in this._hipShadows)
      {
        hipShadow.GetPropertyBlock(this._mpb);
        mpb.SetFloat(AvatarGraphics.OffsetX, 0.657f);
        mpb.SetFloat(AvatarGraphics.OffsetY, 0.358f);
        mpb.SetFloat(AvatarGraphics.ScaleX, 0.559f);
        mpb.SetFloat(AvatarGraphics.ScaleY, -0.04f);
        hipShadow.SetPropertyBlock(this._mpb);
      }
    }

    private void HandleAmbientShadows(bool state)
    {
      if (this._shadowRenderers == null)
        return;
      foreach (Renderer shadowRenderer in this._shadowRenderers)
      {
        if ((UnityEngine.Object) shadowRenderer != (UnityEngine.Object) null)
          shadowRenderer.enabled = state;
      }
    }

    private void HandlePlanarShadows(bool planarShadowsEnabled)
    {
      UniformStore uniformStore = SaveManager.GetUniformStore();
      if ((UnityEngine.Object) uniformStore == (UnityEngine.Object) null)
        return;
      foreach (Renderer renderer in this._renderers)
      {
        Material[] sharedMaterials = renderer.sharedMaterials;
        Material[] materialArray1;
        if (sharedMaterials.Length > 1)
        {
          Material[] materialArray2;
          if (!planarShadowsEnabled)
            materialArray2 = new Material[2]
            {
              sharedMaterials[0],
              sharedMaterials[1]
            };
          else
            materialArray2 = new Material[3]
            {
              sharedMaterials[0],
              sharedMaterials[1],
              uniformStore.planarShadowMat
            };
          materialArray1 = materialArray2;
        }
        else
        {
          Material[] materialArray3;
          if (!planarShadowsEnabled)
            materialArray3 = new Material[1]
            {
              sharedMaterials[0]
            };
          else
            materialArray3 = new Material[2]
            {
              sharedMaterials[0],
              uniformStore.planarShadowMat
            };
          materialArray1 = materialArray3;
        }
        renderer.sharedMaterials = materialArray1;
      }
    }

    protected void OnEnable()
    {
      if ((UnityEngine.Object) this._avatarGraphicsData == (UnityEngine.Object) null)
        this._avatarGraphicsData = ScriptableObject.CreateInstance<AvatarGraphicsData>();
      this.SetupLinks();
    }

    private void OnDisable() => this._linksHandler.Clear();

    private void OnDestroy() => this._appearRoutine.Stop();

    public void Appear(float duration = 2f, float delay = 0.5f) => this._appearRoutine.Run(AvatarTransition.Apply(this, true, duration, delay));

    public void Disappear(float duration = 2f, float delay = 1f)
    {
      this._appearRoutine.Stop();
      if (!this.gameObject.activeInHierarchy)
        return;
      this._appearRoutine.Run(AvatarTransition.Apply(this, false, duration, delay));
    }

    public void StopTransition() => this._appearRoutine.Stop();

    public void SetBasemap(Texture2D value)
    {
      if ((UnityEngine.Object) value == (UnityEngine.Object) null)
        return;
      MaterialPropertyBlock mpb = this.Mpb;
      int count = this._renderers.Count;
      int id = Shader.PropertyToID("_BaseMap");
      for (int index = 0; index < count; ++index)
      {
        this._renderers[index].GetPropertyBlock(mpb);
        mpb.SetTexture(id, (Texture) value);
        this._renderers[index].SetPropertyBlock(mpb);
      }
    }

    public void SetFace(Texture2D value)
    {
      if ((UnityEngine.Object) value == (UnityEngine.Object) null)
        return;
      int id = Shader.PropertyToID("_MainTex");
      MaterialPropertyBlock mpb = this.Mpb;
      for (int index = 0; index < this.headRenderer.Length; ++index)
      {
        this.headRenderer[index].GetPropertyBlock(mpb);
        mpb.SetTexture(id, (Texture) value);
        this.headRenderer[index].SetPropertyBlock(mpb);
      }
    }

    public void SetTextsMap(Texture2D[] value, int playerIndex)
    {
      if (value == null)
        return;
      Shader.PropertyToID("_BaseMap");
      int id1 = Shader.PropertyToID("_PlayerIndex");
      int id2 = Shader.PropertyToID("_NamesMap");
      int id3 = Shader.PropertyToID("_NumbersCMap");
      int id4 = Shader.PropertyToID("_NumbersSMap");
      MaterialPropertyBlock mpb = this.Mpb;
      int count = this._renderers.Count;
      for (int index = 0; index < count; ++index)
      {
        this._renderers[index].GetPropertyBlock(mpb);
        if (value != null)
        {
          mpb.SetTexture(id2, (Texture) value[0]);
          mpb.SetTexture(id3, (Texture) value[1]);
          mpb.SetTexture(id4, (Texture) value[2]);
        }
        mpb.SetInt(id1, playerIndex);
        this._renderers[index].SetPropertyBlock(mpb);
      }
    }

    private void ApplyUniform(UniformCapture.Info info) => this.ApplyUniformData(info, true);

    public void ApplyUniformData(UniformCapture.Info info, bool applyBasemap)
    {
      if (info == null)
        return;
      MaterialPropertyBlock mpb = this.Mpb;
      int count = this._renderers.Count;
      for (int index = 0; index < count; ++index)
      {
        this._renderers[index].GetPropertyBlock(mpb);
        info.Apply(mpb, applyBasemap);
        this._renderers[index].SetPropertyBlock(mpb);
      }
    }

    private void SetSkinColor(Color color)
    {
      MaterialPropertyBlock mpb = this.Mpb;
      int count = this._renderers.Count;
      for (int index = 0; index < count; ++index)
      {
        this._renderers[index].GetPropertyBlock(mpb);
        mpb.SetColor(WorldConstants.Player.SkinColor, color);
        this._renderers[index].SetPropertyBlock(mpb);
      }
    }

    private void SetOutline(EOutlineMode value)
    {
      UniformStore uniformStore = SaveManager.GetUniformStore();
      if ((UnityEngine.Object) uniformStore == (UnityEngine.Object) null)
        return;
      Color color = uniformStore.OutlineIdleColor;
      float num = value == EOutlineMode.kDisabld ? 0.0f : 1f;
      bool flag = false;
      switch (value)
      {
        case EOutlineMode.kIdle:
          flag = true;
          break;
        case EOutlineMode.kHighlight:
          color = uniformStore.OutlineIdleColor;
          flag = true;
          break;
        case EOutlineMode.kAttacking:
          color = uniformStore.OutlineAttackingColor;
          break;
        case EOutlineMode.kPreparing:
          color = uniformStore.OutlinePreparingColor;
          break;
      }
      MaterialPropertyBlock mpb = this.Mpb;
      int count = this._renderers.Count;
      for (int index = 0; index < count; ++index)
      {
        this._renderers[index].GetPropertyBlock(mpb);
        mpb.SetFloat(WorldConstants.Player.Fresnel, num);
        mpb.SetFloat(WorldConstants.Player.FresnelTimed, flag ? 1f : 0.0f);
        mpb.SetColor(WorldConstants.Player.FresnelColor, color);
        this._renderers[index].SetPropertyBlock(mpb);
      }
    }

    public void SetLod(bool state, int quality = 0)
    {
      if ((UnityEngine.Object) this._lODGroup == (UnityEngine.Object) null)
        return;
      this._lODGroup.enabled = state;
      for (int index = 0; index < this._renderers.Count; ++index)
        this._renderers[index].enabled = state || index == quality;
    }

    public void SetRenderers(SkinnedMeshRenderer[] renderers)
    {
      if (this._renderers == null)
        this._renderers = new List<Renderer>();
      else
        this._renderers.Clear();
      this._renderers.AddRange((IEnumerable<Renderer>) renderers);
    }

    public CharacterParameters GetParams(string avatarID) => this.charactersStore.GetPreset(avatarID);

    public CharacterParameters ConfigAvatar(string avatarID, Position playerType = Position.WR)
    {
      this._currentCharacterParam = this.GetParams(avatarID);
      this.SetupAvatar(this._currentCharacterParam, this.charactersStore, playerType);
      return this._currentCharacterParam;
    }

    public async Task SetupAvatar(
      CharacterParameters data,
      CharacterCustomizationStore baseStore,
      Position playerType = Position.WR)
    {
      if (data != null)
        this._currentCharacterParam = data;
      int i = 0;
      UnityEngine.Mesh[] meshes = await data.GetHead().GetMeshWithLODs();
      SkinnedMeshRenderer[] skinnedMeshRendererArray = this.headRenderer;
      int index;
      for (index = 0; index < skinnedMeshRendererArray.Length; ++index)
      {
        SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRendererArray[index];
        if (!((UnityEngine.Object) skinnedMeshRenderer == (UnityEngine.Object) null) && i < meshes.Length)
        {
          skinnedMeshRenderer.sharedMesh = meshes[i];
          ++i;
          await Task.Delay(10);
        }
      }
      skinnedMeshRendererArray = (SkinnedMeshRenderer[]) null;
      int shaderFaceTexID = Shader.PropertyToID("_MainTex");
      skinnedMeshRendererArray = this.headRenderer;
      for (index = 0; index < skinnedMeshRendererArray.Length; ++index)
      {
        SkinnedMeshRenderer headLod = skinnedMeshRendererArray[index];
        if (!((UnityEngine.Object) headLod == (UnityEngine.Object) null))
        {
          MaterialPropertyBlock _mpb = new MaterialPropertyBlock();
          headLod.GetPropertyBlock(_mpb);
          MaterialPropertyBlock materialPropertyBlock = _mpb;
          int nameID = shaderFaceTexID;
          materialPropertyBlock.SetTexture(nameID, (Texture) await data.GetFace(this.charactersStore));
          materialPropertyBlock = (MaterialPropertyBlock) null;
          headLod.SetPropertyBlock(_mpb);
          _mpb = (MaterialPropertyBlock) null;
          headLod = (SkinnedMeshRenderer) null;
        }
      }
      skinnedMeshRendererArray = (SkinnedMeshRenderer[]) null;
      i = 0;
      data.Gloves = playerType == Position.WR ? CharacterCustomizationStore.Gloves.WithGloves : CharacterCustomizationStore.Gloves.WithoutGloves;
      meshes = await data.GetBody(baseStore).GetMeshWithLODs();
      skinnedMeshRendererArray = this.bodyRenderer;
      for (index = 0; index < skinnedMeshRendererArray.Length; ++index)
      {
        SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRendererArray[index];
        if (!((UnityEngine.Object) skinnedMeshRenderer == (UnityEngine.Object) null) && i < meshes.Length)
        {
          skinnedMeshRenderer.sharedMesh = meshes[i];
          ++i;
          MaterialPropertyBlock properties = new MaterialPropertyBlock();
          skinnedMeshRenderer.GetPropertyBlock(properties);
          properties.SetColor(WorldConstants.Player.SkinColor, data.GetSkinTone());
          skinnedMeshRenderer.SetPropertyBlock(properties);
          await Task.Delay(10);
        }
      }
      skinnedMeshRendererArray = (SkinnedMeshRenderer[]) null;
      meshes = (UnityEngine.Mesh[]) null;
    }

    public void SetMultiplayerAvatarBody(UnityEngine.Mesh mesh)
    {
      foreach (SkinnedMeshRenderer skinnedMeshRenderer in this.bodyRenderer)
      {
        if (!((UnityEngine.Object) skinnedMeshRenderer == (UnityEngine.Object) null))
          skinnedMeshRenderer.sharedMesh = mesh;
      }
    }

    public void SetHeadState(bool state)
    {
      foreach (Renderer renderer in this.headRenderer)
        renderer.enabled = state;
    }

    public async void SetHead(CharacterCustomizationStore.MeshWithLod headMesh)
    {
      SkinnedMeshRenderer[] skinnedMeshRendererArray = this.headRenderer;
      for (int index = 0; index < skinnedMeshRendererArray.Length; ++index)
      {
        SkinnedMeshRenderer rend = skinnedMeshRendererArray[index];
        rend.sharedMesh = (await headMesh.GetMeshWithLODs())[0];
        rend = (SkinnedMeshRenderer) null;
      }
      skinnedMeshRendererArray = (SkinnedMeshRenderer[]) null;
    }

    public void SetupBody(int id)
    {
      CharacterCustomizationStore.BodyType bodyType = CharacterCustomizationStore.BodyType.Athletic;
      switch (id)
      {
        case 0:
          bodyType = CharacterCustomizationStore.BodyType.Athletic;
          break;
        case 1:
          bodyType = CharacterCustomizationStore.BodyType.Skinny;
          break;
        case 2:
          bodyType = CharacterCustomizationStore.BodyType.Fat;
          break;
      }
      this._currentCharacterParam.bodyType = bodyType;
      this.SetupAvatar(this._currentCharacterParam, this.charactersStore);
    }

    public void SetupSavePlayer(PlayerCustomization data)
    {
      this._currentCharacterParam = this.charactersStore.GetPreset(data.AvatarPresetId.Value);
      if (data.AvatarCustomized.Value)
        this.SetupBody(data.BodyModelId.Value);
      else
        this.SetupAvatar(this._currentCharacterParam, this.charactersStore);
    }

    public void SetStoreByGender(CharacterCustomizationStore.Gender gender)
    {
      this.avatarGender = gender;
      this.charactersStore = gender == CharacterCustomizationStore.Gender.Male ? SaveManager.GetCharacterCustomizationStoreMale() : SaveManager.GetCharacterCustomizationStoreFemale();
    }
  }
}
