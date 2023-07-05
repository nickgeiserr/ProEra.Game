// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerBodyController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR.Multiplayer;
using FootballWorld;
using Framework;
using Framework.Data;
using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Vars;

namespace FootballVR
{
  public class PlayerBodyController : MonoBehaviour
  {
    private UniformStore _uniformStore;
    [SerializeField]
    private AvatarGraphics _avatarGraphics;
    [SerializeField]
    private VRIK _vrik;
    [SerializeField]
    private SkinnedMeshRenderer _bodyRenderer;
    [SerializeField]
    private Transform _headTx;
    [SerializeField]
    private Collider _tackleCollider;
    [SerializeField]
    private RenderStateTracker _renderState;
    [SerializeField]
    private float _vrScale = 1f;
    [Space(10f)]
    [SerializeField]
    protected PlayerBodyController.BodyMesh[] maleMeshRefs;
    [SerializeField]
    protected PlayerBodyController.BodyMesh[] femaleMeshRefs;
    private HeadTiltSettings _settings;
    private bool _initialized;
    private float _playerHeight = 1.5f;
    private float _playerCurrentHeight = 1.5f;
    private Vector3Cache _playerHeightCache;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    public readonly VariableBool ShowBody = new VariableBool(true);
    private float _timeAccumulated;
    private Vector3 _playerPos;
    private bool _visible;

    public event Action<BallObject> OnBallCollision;

    public AvatarGraphics AvatarGraphicsComponent => this._avatarGraphics;

    private Vector3 playerPos
    {
      get => this._playerPos;
      set
      {
        this._playerPos = value;
        value.y = this._settings.BodyY;
        this.transform.position = value;
      }
    }

    private Vector3 GetHeadPos()
    {
      Vector3 position = this._headTx.position;
      position.y /= this._headTx.lossyScale.x;
      return position;
    }

    private void Awake() => this.Initialize();

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._uniformStore = SaveManager.GetUniformStore();
      this._initialized = true;
      this._settings = ScriptableSingleton<LocomotionSettings>.Instance.LeanDetectionSettings;
      this._playerCurrentHeight = this.playerPos.y;
      if ((double) this._playerCurrentHeight < 1.5)
        this._playerCurrentHeight = 1.5f;
      this._playerHeight = this._playerCurrentHeight - 0.1f;
      this._playerHeightCache = new Vector3Cache(30);
      this._linksHandler.AddLink(this.ShowBody.Link<bool>(new Action<bool>(this.PlayerBodyToggleHandler)));
      this._avatarGraphics.Initialize();
    }

    public void InitializeLinks()
    {
      this._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        this._settings.AverageFrameCount.Link<int>((Action<int>) (size => this._playerHeightCache.SetSize(size))),
        ScriptableSingleton<VRSettings>.Instance.PlayerBodyScale.Link<float>((Action<float>) (scale => this.transform.localScale = Vector3.one * scale)),
        this._renderState.IsRendering.Link<bool>(new Action<bool>(this.HandleRenderState))
      });
      this.SyncPosition();
    }

    private void HandleRenderState(bool state)
    {
      this._vrik.enabled = state;
      if (!state)
        return;
      this._vrik.UpdateSolverExternal();
    }

    private void OnDestroy() => this.Deinitialize();

    private void Deinitialize()
    {
      if (!this._initialized)
        return;
      this._linksHandler.Clear();
      this._uniformStore = (UniformStore) null;
    }

    public async Task SetBodyGraphics(global::EBodyType bodyType, bool remote = false)
    {
      this._tackleCollider.enabled = remote;
      if (bodyType == global::EBodyType.Male)
        this._avatarGraphics.SetMultiplayerAvatarBody(await AddressablesData.instance.LoadAsync<UnityEngine.Mesh>(this.maleMeshRefs[0].meshRef, new CacheParams(false)));
      if (bodyType != global::EBodyType.Female)
        return;
      this._avatarGraphics.SetMultiplayerAvatarBody(await AddressablesData.instance.LoadAsync<UnityEngine.Mesh>(this.femaleMeshRefs[0].meshRef, new CacheParams(false)));
    }

    public void SyncPosition()
    {
      this.playerPos = this.GetHeadPos();
      this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, this._headTx.rotation.eulerAngles.y, 0.0f));
    }

    public float RecalculateHeight()
    {
      float y = this._vrik.references.root.position.y;
      this._vrik.references.root.localScale *= (float) (((double) this._vrik.solver.spine.headTarget.position.y - (double) y) / ((double) this._vrik.references.head.position.y - (double) y)) * this._vrScale;
      return this._vrik.references.root.localScale.x;
    }

    public void AdjustPlayerHeight(float value) => this._vrik.references.root.localScale = new Vector3(value, value, value);

    public void UpdateHeightCheck() => this.ShowBody.SetValue((double) this.GetHeadPos().y > 1.2000000476837158);

    private void PlayerBodyToggleHandler(bool value) => this.gameObject.SetActive((bool) this.ShowBody && this._visible);

    private void LateUpdate()
    {
      Vector3 headPos = this.GetHeadPos();
      this._playerCurrentHeight = Mathf.Lerp(this._playerCurrentHeight, headPos.y, this._settings.HeightLerpFactor);
      this._timeAccumulated += Time.unscaledDeltaTime;
      if ((double) this._timeAccumulated > (double) this._settings.SampleTime)
      {
        this._timeAccumulated -= this._settings.SampleTime;
        this._playerHeightCache.PushValue(headPos.SetY(this._playerCurrentHeight));
        float y = this._playerHeightCache.AverageValue().y;
        bool flag = (double) y > (double) this._playerHeight;
        this._playerHeight = Mathf.Lerp(this._playerHeight, y, flag ? this._settings.LerpUpFactor : this._settings.LerpDownFactor / 1000f);
      }
      if ((double) this._playerCurrentHeight > (double) this._playerHeight - (double) this._settings.HeightFlexibility * 1.2000000476837158)
      {
        this.playerPos = headPos;
      }
      else
      {
        Vector3 vector3_1 = (this.playerPos - headPos).SetY(0.0f);
        if (this._settings.BlockForwardOffset)
        {
          Vector3 vector3_2 = this._headTx.forward.SetY(0.0f);
          if ((double) Vector3.Dot(vector3_1, vector3_2) > 0.0)
          {
            Vector3 vector3_3 = Vector3.Project(vector3_1, vector3_2);
            vector3_1 -= vector3_3;
            this.playerPos = headPos.SetY(0.0f) + vector3_1;
          }
        }
        if (!this._settings.ApplyMaxDistance || (double) vector3_1.magnitude <= (double) this._settings.MaxBodyDistance)
          return;
        this.playerPos = headPos.SetY(0.0f) + vector3_1.normalized * this._settings.MaxBodyDistance;
      }
    }

    public void SetCustomization(ETeamUniformId uniformId, bool homeUniform) => this._avatarGraphics.SetBasemap(this._uniformStore.GetUniformConfig(uniformId, homeUniform ? ETeamUniformFlags.Home : ETeamUniformFlags.Away).BasemapAlternative);

    public void SetSkinColor(Color skinColor)
    {
    }

    public void SetVisible(bool visible)
    {
      this._visible = visible;
      this.PlayerBodyToggleHandler(visible);
    }

    private void OnCollisionEnter(Collision other)
    {
      BallObjectNetworked component;
      if (!WorldConstants.Layers.Interactables.ContainsLayer(other.gameObject.layer) || !other.gameObject.TryGetComponent<BallObjectNetworked>(out component))
        return;
      Action<BallObject> onBallCollision = this.OnBallCollision;
      if (onBallCollision == null)
        return;
      onBallCollision((BallObject) component);
    }

    public void SetUniformData(int playerID, PlayerCustomization customization)
    {
      Debug.Log((object) ("SetUniformData " + playerID.ToString() + " " + customization.Uniform?.ToString() + " " + (string) customization.LastName));
      ETeamUniformId eteamUniformId = customization.Uniform.Value;
      int number = customization.UniformNumber.Value;
      string playerName = customization.LastName.Value;
      int num = playerID + 1;
      FootballWorld.UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(eteamUniformId, ETeamUniformFlags.Home);
      UniformCapture.Info info = new UniformCapture.Info()
      {
        BaseMap = uniformConfig.BasemapAlternative,
        PlayerIndex = num
      };
      Texture2D[] texture2DArray = UniformCapture.UpdateMultiplayerUniform(num, eteamUniformId, number, playerName, ETeamUniformFlags.Home);
      info.TextsAtlas = (Texture[]) texture2DArray;
      this._avatarGraphics.SetLod(false);
      this._avatarGraphics.ApplyUniformData(info, true);
      this._avatarGraphics.SetTextsMap(texture2DArray, num);
    }

    public void SetupBaseTextlessUniform(int playerID, PlayerCustomization customization)
    {
      Debug.Log((object) ("SetupBaseTextlessUniform " + playerID.ToString() + " " + customization.Uniform?.ToString() + " " + (string) customization.LastName));
      ETeamUniformId team = customization.Uniform.Value;
      int num1 = customization.UniformNumber.Value;
      string str = customization.LastName.Value;
      int num2 = playerID + 1;
      FootballWorld.UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(team, ETeamUniformFlags.Home);
      UniformCapture.Info info = new UniformCapture.Info()
      {
        BaseMap = uniformConfig.BasemapAlternative,
        PlayerIndex = num2
      };
      this._avatarGraphics.SetLod(false);
      this._avatarGraphics.ApplyUniformData(info, true);
    }

    [Serializable]
    public class BodyMesh
    {
      public CharacterCustomizationStore.BodyType BodyType;
      public AssetReference meshRef;
    }
  }
}
