// Decompiled with JetBrains decompiler
// Type: SidelineTeamUniform
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using System;
using System.Threading.Tasks;
using TB12;
using TB12.AppStates;
using UDB;
using UnityEngine;

public class SidelineTeamUniform : MonoBehaviour
{
  [SerializeField]
  private bool isPLayerTeam = true;
  [SerializeField]
  private string textureID = "_BaseMap";
  [SerializeField]
  private int playerIndex = -1;
  [SerializeField]
  private bool SkipBakeTextures;
  private int shaderKey_textureID;
  private Renderer _renderer;
  private bool isInitialized;
  private bool isMultiplayer;
  private bool isDestroyed;

  private void Awake() => PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnFinished += new Action<GameState>(this.OnStateFinished);

  private void OnDestroy()
  {
    this.isDestroyed = true;
    if (!PersistentSingleton<StateManager<EAppState, GameState>>.Exist())
      return;
    PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnFinished -= new Action<GameState>(this.OnStateFinished);
  }

  private void OnStateFinished(GameState obj)
  {
    GameState activeState = PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState;
    if (object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerLobby) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerBossModeGame) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerDodgeball) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerThrowGame))
      this.isMultiplayer = true;
    this.TryToRefresh().SafeFireAndForget();
  }

  private void Start()
  {
    this.isInitialized = true;
    this.shaderKey_textureID = Shader.PropertyToID(this.textureID);
    this._renderer = this.GetComponent<Renderer>();
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.Value != ETeamUniformFlags.Home)
      this.isPLayerTeam = !this.isPLayerTeam;
    if (!((UnityEngine.Object) this._renderer != (UnityEngine.Object) null) || !((UnityEngine.Object) this._renderer.material.GetTexture(this.shaderKey_textureID) == (UnityEngine.Object) null) && !((UnityEngine.Object) this._renderer.material.GetTexture(UniformCapture.Info.kTextsAtlasMask_0) == (UnityEngine.Object) null))
      return;
    this.TryToRefresh().SafeFireAndForget();
  }

  private async System.Threading.Tasks.Task TryToRefresh()
  {
    SidelineTeamUniform sidelineTeamUniform = this;
    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1.0));
    if (sidelineTeamUniform.isDestroyed)
      return;
    bool flag = await sidelineTeamUniform.Refresh();
    if (sidelineTeamUniform.isDestroyed || flag || !sidelineTeamUniform.enabled)
      return;
    sidelineTeamUniform.TryToRefresh().SafeFireAndForget();
  }

  private async Task<bool> Refresh()
  {
    if ((UnityEngine.Object) this._renderer == (UnityEngine.Object) null)
      return false;
    int uniformId = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide == 1 ? (this.isPLayerTeam ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntAwayTeam : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntHomeTeam) : (this.isPLayerTeam ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntHomeTeam : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntAwayTeam);
    int uniformFlag = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide == 1 ? (this.isPLayerTeam ? 1 : 2) : (this.isPLayerTeam ? 2 : 1);
    if (this.isMultiplayer)
      uniformId = (int) TeamDataCache.ToTeamUniformId(SeasonModeManager.self.seasonModeData.UserTeamIndex);
    Texture2D uniformTexture = UniformCapture.GetUniformDiffuse(uniformId, (ETeamUniformFlags) uniformFlag);
    await System.Threading.Tasks.Task.Delay(10);
    if (this.isDestroyed || (UnityEngine.Object) uniformTexture == (UnityEngine.Object) null)
      return false;
    Color skinTone = Color.white;
    if (!((UnityEngine.Object) SeasonModeManager.self != (UnityEngine.Object) null) || SeasonModeManager.self.userTeamData == null)
      return false;
    RosterData mainRoster = SeasonModeManager.self.userTeamData.TeamDepthChart.MainRoster;
    if (mainRoster == null)
      return false;
    GameState activeState = PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState;
    PlayerData playerData;
    if (object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerLobby) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerBossModeGame) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerDodgeball) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerThrowGame) || this.isMultiplayer)
    {
      playerData = mainRoster.GetPlayer(this.playerIndex);
      this.isMultiplayer = true;
    }
    else
      playerData = uniformFlag != 1 ? PersistentData.GetHomeTeamData().TeamDepthChart.MainRoster.GetPlayer(this.playerIndex) : PersistentData.GetAwayTeamData().TeamDepthChart.MainRoster.GetPlayer(this.playerIndex);
    CharacterParameters characterParams = SaveManager.GetCharacterCustomizationStoreMale().GetPreset(playerData.AvatarID);
    Texture2D face = await characterParams.GetFace(SaveManager.GetCharacterCustomizationStoreMale());
    if (this.isDestroyed)
      return false;
    skinTone = characterParams.GetSkinTone();
    int uniformNumber = playerData.Number;
    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    this._renderer.GetPropertyBlock(mpb);
    if (this.playerIndex >= 0)
    {
      if (!this.SkipBakeTextures)
      {
        Texture2D[] textsTexture = UniformCapture.GetTextsTexture(uniformId, (ETeamUniformFlags) uniformFlag);
        await System.Threading.Tasks.Task.Delay(10);
        if (this.isDestroyed || textsTexture == null || textsTexture.Length == 0)
          return false;
        mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_0, (Texture) textsTexture[0]);
        mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_1, (Texture) textsTexture[1]);
        mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_2, (Texture) textsTexture[2]);
        textsTexture = (Texture2D[]) null;
      }
      mpb.SetColor(UniformCapture.Info.kSkinTone, skinTone);
      mpb.SetFloat(UniformCapture.Info.kPlayerIndex, (float) uniformNumber);
    }
    mpb.SetTexture(this.shaderKey_textureID, (Texture) uniformTexture);
    this._renderer.SetPropertyBlock(mpb);
    characterParams = (CharacterParameters) null;
    mpb = (MaterialPropertyBlock) null;
    return true;
  }
}
