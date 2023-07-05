// Decompiled with JetBrains decompiler
// Type: PlayerSharedUniform
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class PlayerSharedUniform : MonoBehaviour
{
  [SerializeField]
  private Material sharedPlayerMaterial;
  [SerializeField]
  private Renderer[] renderers;
  private UniformCapture.Info _uniformInfoPrev = new UniformCapture.Info();
  private UniformCapture.Info uniformInfo = new UniformCapture.Info();
  private bool isAllListenerSynced;
  private MaterialPropertyBlock materialPropertyBlock;
  private LinksHandler linksHandler = new LinksHandler();

  private PlayerCustomization playerCustomization => SaveManager.GetPlayerCustomization();

  private void Start()
  {
    int num = PersistentSingleton<SaveManager>.Instance.seasonModeData != null ? PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex : TeamDataCache.ToTeamIndex(ETeamUniformId.Ravens);
    this.materialPropertyBlock = new MaterialPropertyBlock();
    this.SetupRenderers();
    this.SetupLinkToPersistentData().ConfigureAwait(false);
    this.RefreshUniformDiffuse(num, ETeamUniformFlags.Home, true);
    this.RefreshUniformTexts(TeamDataCache.ToTeamUniformId(num), ETeamUniformFlags.Home, true);
    this.linksHandler.SetLinks(new List<EventHandle>()
    {
      this.playerCustomization.Uniform.Link<ETeamUniformId>((Action<ETeamUniformId>) (x => this.HandleUniformChanged())),
      this.playerCustomization.UniformNumber.Link<int>((Action<int>) (x => this.HandleUniformTextsChanged())),
      this.playerCustomization.LastName.Link<string>((Action<string>) (x => this.HandleUniformTextsChanged())),
      this.playerCustomization.HomeUniform.Link<bool>((Action<bool>) (x =>
      {
        this.HandleUniformChanged();
        this.HandleUniformTextsChanged();
      }))
    });
  }

  private void OnDestroy()
  {
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.Exists())
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged -= new System.Action(this.HandleGameTeamUniformChanged);
    this.linksHandler?.Clear();
  }

  private async System.Threading.Tasks.Task SetupLinkToPersistentData()
  {
    PlayerSharedUniform playerSharedUniform = this;
    while (!playerSharedUniform.isAllListenerSynced)
    {
      if (SingletonBehaviour<PersistentData, MonoBehaviour>.Exists())
      {
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged += new System.Action(playerSharedUniform.HandleGameTeamUniformChanged);
        playerSharedUniform.isAllListenerSynced = true;
        break;
      }
      await System.Threading.Tasks.Task.Delay(30);
    }
  }

  private void SetupRenderers()
  {
    foreach (Renderer renderer in this.renderers)
    {
      if (!((UnityEngine.Object) renderer == (UnityEngine.Object) null))
        renderer.sharedMaterial = this.sharedPlayerMaterial;
    }
  }

  private void HandleUniformChanged() => this.RefreshUniformDiffuse(PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex, ETeamUniformFlags.Home);

  private void HandleUniformTextsChanged() => this.RefreshUniformTexts(TeamDataCache.ToTeamUniformId(PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex), ETeamUniformFlags.Home);

  public void HandleGameTeamUniformChanged()
  {
    ETeamUniformFlags teamUniformFlags = PersistentData.userIsHome ? ETeamUniformFlags.Home : ETeamUniformFlags.Away;
    ETeamUniformId eteamUniformId = !PersistentData.userIsHome ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.Value : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.Value;
    this.RefreshUniformDiffuse(eteamUniformId, teamUniformFlags);
    this.RefreshUniformTexts(eteamUniformId, teamUniformFlags);
  }

  private void RefreshUniformDiffuse(int teamIndex, ETeamUniformFlags teamUniformFlags, bool force = false)
  {
    CacheParams cacheParams = new CacheParams(true);
    CachedTexture instanceUniformDiffuse = UniformCapture.GetInstanceUniformDiffuse(TeamDataCache.ToTeamUniformId(teamIndex), teamUniformFlags, cacheParams);
    if (instanceUniformDiffuse == null || (UnityEngine.Object) instanceUniformDiffuse.Value == (UnityEngine.Object) null)
      return;
    this.uniformInfo.CachedBaseMap = instanceUniformDiffuse;
    if (this._uniformInfoPrev.CachedBaseMap == this.uniformInfo.CachedBaseMap && !force)
      return;
    this.materialPropertyBlock.SetTexture(UniformCapture.Info.kBaseMap, (Texture) this.uniformInfo.CachedBaseMap.Value);
    this.SetupMaterial();
    if (this._uniformInfoPrev.CachedBaseMap != null)
      this._uniformInfoPrev.CachedBaseMap.CacheProprieties.DontDestroy = false;
    this._uniformInfoPrev.CachedBaseMap = this.uniformInfo.CachedBaseMap;
  }

  private void RefreshUniformDiffuse(
    ETeamUniformId uniformId,
    ETeamUniformFlags teamUniformFlags,
    bool force = false)
  {
    CacheParams cacheParams = new CacheParams(true);
    CachedTexture instanceUniformDiffuse = UniformCapture.GetInstanceUniformDiffuse(uniformId, teamUniformFlags, cacheParams);
    if (instanceUniformDiffuse == null || (UnityEngine.Object) instanceUniformDiffuse.Value == (UnityEngine.Object) null)
      return;
    this.uniformInfo.CachedBaseMap = instanceUniformDiffuse;
    if (this._uniformInfoPrev.CachedBaseMap == this.uniformInfo.CachedBaseMap && !force)
      return;
    this.materialPropertyBlock.SetTexture(UniformCapture.Info.kBaseMap, (Texture) this.uniformInfo.CachedBaseMap.Value);
    this.SetupMaterial();
    if (this._uniformInfoPrev.CachedBaseMap != null)
      this._uniformInfoPrev.CachedBaseMap.CacheProprieties.DontDestroy = false;
    this._uniformInfoPrev.CachedBaseMap = this.uniformInfo.CachedBaseMap;
  }

  private void RefreshUniformTexts(
    ETeamUniformId teamIndex,
    ETeamUniformFlags teamUniformFlags,
    bool force = false)
  {
    int indexInAtlas = 0;
    this.uniformInfo.TextsAtlas = (Texture[]) UniformCapture.UpdateMultiplayerUniform(indexInAtlas, teamIndex, this.playerCustomization.UniformNumber.Value, this.playerCustomization.LastName.Value, teamUniformFlags);
    if (this._uniformInfoPrev.TextsAtlas == this.uniformInfo.TextsAtlas && !force || this.uniformInfo.TextsAtlas == null)
      return;
    this.materialPropertyBlock.SetTexture(UniformCapture.Info.kTextsAtlasMask_0, this.uniformInfo.TextsAtlas[0]);
    this.materialPropertyBlock.SetTexture(UniformCapture.Info.kTextsAtlasMask_1, this.uniformInfo.TextsAtlas[1]);
    this.materialPropertyBlock.SetTexture(UniformCapture.Info.kTextsAtlasMask_2, this.uniformInfo.TextsAtlas[2]);
    this.materialPropertyBlock.SetInt(UniformCapture.Info.kPlayerIndex, indexInAtlas);
    this.SetupMaterial();
    this._uniformInfoPrev.TextsAtlas = this.uniformInfo.TextsAtlas;
  }

  private void SetupMaterial()
  {
    foreach (Renderer renderer in this.renderers)
    {
      if (!((UnityEngine.Object) renderer == (UnityEngine.Object) null))
        renderer.SetPropertyBlock(this.materialPropertyBlock);
    }
  }
}
