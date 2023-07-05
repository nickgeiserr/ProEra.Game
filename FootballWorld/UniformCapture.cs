// Decompiled with JetBrains decompiler
// Type: FootballWorld.UniformCapture
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballWorld
{
  public class UniformCapture : MonoBehaviour
  {
    public static UniformCapture Instance;
    private Dictionary<int, Texture2D[]> _textsAtlasesDicHome = new Dictionary<int, Texture2D[]>();
    private Dictionary<int, Texture2D[]> _textsAtlasesDicAway = new Dictionary<int, Texture2D[]>();
    private Texture2D[] _atlasForUniqueUniforms;
    [SerializeField]
    private UniformCapture.SharedRenderTexture[] sharedRenderTextures;
    [SerializeField]
    private BakeUniformsTexts _textsAtlasBaker;
    private UniformStore _uniformStore;
    private RoutineHandle _routineHandle = new RoutineHandle();

    private static event Action<int, List<int>, List<string>, ETeamUniformFlags> OnCaptureUniformRequested;

    public static Texture2D GetUniformDiffuse(
      ETeamUniformId teamIndex,
      ETeamUniformFlags uniformFlags,
      CacheParams cacheParams)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D) null;
      return UniformCapture.Instance._uniformStore.GetUniformConfigByInt((int) teamIndex, (int) uniformFlags)?.GetBaseMap(cacheParams);
    }

    public static CachedTexture GetInstanceUniformDiffuse(
      ETeamUniformId teamIndex,
      ETeamUniformFlags uniformFlags,
      CacheParams cacheParams)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (CachedTexture) null;
      return UniformCapture.Instance._uniformStore.GetUniformConfigByInt((int) teamIndex, (int) uniformFlags)?.LoadBaseMap(cacheParams);
    }

    public static Texture2D GetUniformDiffuse(int teamIndex, ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D) null;
      return UniformCapture.Instance._uniformStore.GetUniformConfigByInt(teamIndex, (int) uniformFlags)?.GetBaseMap(new CacheParams(false));
    }

    public static Texture2D GetUniformDiffuseByID(
      ETeamUniformId uniformId,
      ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D) null;
      return UniformCapture.Instance._uniformStore.GetUniformConfig(uniformId, uniformFlags)?.GetBaseMap(new CacheParams(false));
    }

    private static Texture2D[] GetTeamTexture(int teamIndex, ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D[]) null;
      if (uniformFlags == ETeamUniformFlags.Home)
        return UniformCapture.Instance._textsAtlasesDicHome[teamIndex];
      return uniformFlags == ETeamUniformFlags.Away ? UniformCapture.Instance._textsAtlasesDicAway[teamIndex] : (Texture2D[]) null;
    }

    private static bool ExistTextureFor(int teamIndex, ETeamUniformFlags uniformFlags)
    {
      if (uniformFlags == ETeamUniformFlags.Home)
        return UniformCapture.Instance._textsAtlasesDicHome.ContainsKey(teamIndex);
      return uniformFlags == ETeamUniformFlags.Away && UniformCapture.Instance._textsAtlasesDicAway.ContainsKey(teamIndex);
    }

    private static void DestroyTexture(int teamIndex, ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return;
      if (uniformFlags == ETeamUniformFlags.Home && UniformCapture.Instance._textsAtlasesDicHome.ContainsKey(teamIndex))
      {
        foreach (Texture2D[] texture2DArray in UniformCapture.Instance._textsAtlasesDicHome.Values)
        {
          if (texture2DArray != null)
          {
            foreach (UnityEngine.Object @object in texture2DArray)
              UnityEngine.Object.Destroy(@object);
          }
        }
      }
      if (uniformFlags != ETeamUniformFlags.Away || !UniformCapture.Instance._textsAtlasesDicAway.ContainsKey(teamIndex))
        return;
      foreach (Texture2D[] texture2DArray in UniformCapture.Instance._textsAtlasesDicAway.Values)
      {
        if (texture2DArray != null)
        {
          foreach (UnityEngine.Object @object in texture2DArray)
            UnityEngine.Object.Destroy(@object);
        }
      }
    }

    public static Texture2D[] GetTextsTexture(
      int teamIndex,
      List<int> numbers,
      List<string> names,
      int uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D[]) null;
      Texture2D[] textsTexture = UniformCapture.GetTextsTexture(teamIndex, (ETeamUniformFlags) uniformFlags);
      if (textsTexture != null)
        return textsTexture;
      UniformCapture.GenerateUniforms(teamIndex, numbers, names, (ETeamUniformFlags) uniformFlags);
      return UniformCapture.GetTeamTexture(teamIndex, (ETeamUniformFlags) uniformFlags);
    }

    public static Texture2D[] GetTextsTexture(int teamIndex, ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D[]) null;
      return UniformCapture.ExistTextureFor(teamIndex, uniformFlags) ? UniformCapture.GetTeamTexture(teamIndex, uniformFlags) : (Texture2D[]) null;
    }

    public void SetTextsTexture(
      int teamIndex,
      Texture2D[] newTextsAtlas,
      ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return;
      if (UniformCapture.ExistTextureFor(teamIndex, uniformFlags))
      {
        UniformCapture.DestroyTexture(teamIndex, uniformFlags);
        if (uniformFlags == ETeamUniformFlags.Home)
          UniformCapture.Instance._textsAtlasesDicHome[teamIndex] = newTextsAtlas;
        if (uniformFlags != ETeamUniformFlags.Away)
          return;
        UniformCapture.Instance._textsAtlasesDicAway[teamIndex] = newTextsAtlas;
      }
      else
      {
        if (uniformFlags == ETeamUniformFlags.Home)
          UniformCapture.Instance._textsAtlasesDicHome.Add(teamIndex, newTextsAtlas);
        if (uniformFlags != ETeamUniformFlags.Away)
          return;
        UniformCapture.Instance._textsAtlasesDicAway.Add(teamIndex, newTextsAtlas);
      }
    }

    public static void ClearAllCachedTextures()
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return;
      if (UniformCapture.Instance._textsAtlasesDicHome != null)
      {
        foreach (Texture2D[] texture2DArray in UniformCapture.Instance._textsAtlasesDicHome.Values)
        {
          if (texture2DArray != null)
          {
            foreach (UnityEngine.Object @object in texture2DArray)
              UnityEngine.Object.Destroy(@object);
          }
        }
        UniformCapture.Instance._textsAtlasesDicHome.Clear();
        UniformCapture.Instance._textsAtlasesDicHome = new Dictionary<int, Texture2D[]>();
      }
      if (UniformCapture.Instance._textsAtlasesDicAway == null)
        return;
      foreach (Texture2D[] texture2DArray in UniformCapture.Instance._textsAtlasesDicAway.Values)
      {
        if (texture2DArray != null)
        {
          foreach (UnityEngine.Object @object in texture2DArray)
            UnityEngine.Object.Destroy(@object);
        }
      }
      UniformCapture.Instance._textsAtlasesDicAway.Clear();
      UniformCapture.Instance._textsAtlasesDicAway = new Dictionary<int, Texture2D[]>();
    }

    public static Texture2D[] UpdateMultiplayerUniform(
      int indexInAtlas,
      ETeamUniformId teamIndex,
      int number,
      string playerName,
      ETeamUniformFlags uniformFlags)
    {
      if ((UnityEngine.Object) UniformCapture.Instance == (UnityEngine.Object) null)
        return (Texture2D[]) null;
      UniformConfig uniformConfig = UniformCapture.Instance._uniformStore.GetUniformConfig(teamIndex, uniformFlags);
      UniformCapture.Instance._textsAtlasBaker.PrepareLabel(AtlasType.UniformNames, indexInAtlas, playerName, uniformConfig.FontNames);
      UniformCapture.Instance._textsAtlasBaker.PrepareLabel(AtlasType.UniformNumbersOnChest, indexInAtlas, number.ToString(), uniformConfig.FontNumbers);
      UniformCapture.Instance._textsAtlasBaker.PrepareLabel(AtlasType.UniformNumbersOnShoulders, indexInAtlas, number.ToString(), uniformConfig.FontShoulders);
      if (UniformCapture.Instance._atlasForUniqueUniforms == null)
        UniformCapture.Instance._atlasForUniqueUniforms = UniformCapture.Instance._textsAtlasBaker.PrepareTextures();
      UniformCapture.Instance._textsAtlasBaker.BakeTextures(UniformCapture.Instance._atlasForUniqueUniforms);
      return UniformCapture.Instance._atlasForUniqueUniforms;
    }

    public static void GenerateUniforms(
      int index,
      List<int> numbers,
      List<string> names,
      ETeamUniformFlags uniformFlags)
    {
      UniformCapture.Instance.RegenerateTextures(index, numbers, names, uniformFlags);
    }

    private void Awake()
    {
      UniformCapture.Instance = this;
      this._uniformStore = SaveManager.GetUniformStore();
    }

    private void OnDestroy()
    {
      UniformCapture.ClearAllCachedTextures();
      if (this._atlasForUniqueUniforms != null)
      {
        foreach (Texture2D forUniqueUniform in this._atlasForUniqueUniforms)
        {
          if (!((UnityEngine.Object) forUniqueUniform == (UnityEngine.Object) null))
            UnityEngine.Object.Destroy((UnityEngine.Object) forUniqueUniform);
        }
        this._atlasForUniqueUniforms = (Texture2D[]) null;
      }
      UniformCapture.Instance = (UniformCapture) null;
      this._uniformStore = (UniformStore) null;
    }

    private void RegenerateTextures(
      int teamIndex,
      List<int> numbers,
      List<string> names,
      ETeamUniformFlags uniformFlags)
    {
      UniformConfig uniformConfig = this._uniformStore.GetUniformConfig((ETeamUniformId) teamIndex, uniformFlags);
      Texture2D[] texture2DArray = this._textsAtlasBaker.PrepareTextures();
      this.SetTextsTexture(teamIndex, texture2DArray, uniformFlags);
      this.CaptureUniformTexts(numbers, names, uniformConfig, texture2DArray);
    }

    private void CaptureUniformTexts(
      List<int> numbers,
      List<string> names,
      UniformConfig configs,
      Texture2D[] preparedTextures)
    {
      this.BakeTextures(numbers, names, configs, preparedTextures);
    }

    private void BakeTextures(
      List<int> numbers,
      List<string> names,
      UniformConfig configs,
      Texture2D[] preparedTextures)
    {
      int count = numbers.Count;
      for (int index1 = 0; index1 < count; ++index1)
      {
        this._textsAtlasBaker.PrepareLabel(AtlasType.UniformNames, index1, names[index1], configs.FontNames);
        BakeUniformsTexts textsAtlasBaker1 = this._textsAtlasBaker;
        int index2 = index1;
        int number = numbers[index1];
        string str1 = number.ToString();
        UniformFontConfig fontNumbers = configs.FontNumbers;
        textsAtlasBaker1.PrepareLabel(AtlasType.UniformNumbersOnChest, index2, str1, fontNumbers);
        BakeUniformsTexts textsAtlasBaker2 = this._textsAtlasBaker;
        int index3 = index1;
        number = numbers[index1];
        string str2 = number.ToString();
        UniformFontConfig fontShoulders = configs.FontShoulders;
        textsAtlasBaker2.PrepareLabel(AtlasType.UniformNumbersOnShoulders, index3, str2, fontShoulders);
      }
      this._textsAtlasBaker.BakeTextures(preparedTextures);
    }

    public RenderTexture GetSharedRenderTexture(UniformCapture.SharedRenderTextureType type)
    {
      foreach (UniformCapture.SharedRenderTexture sharedRenderTexture in this.sharedRenderTextures)
      {
        if (sharedRenderTexture.Type == type)
          return sharedRenderTexture.Value;
      }
      return (RenderTexture) null;
    }

    public enum SharedRenderTextureType
    {
      Default,
      LiveBroadcast,
      Names,
      Numbers,
      TrophyScreenContent,
      UniformChest,
      UniformNames,
      UniformShoulders,
      Wipe01,
    }

    [Serializable]
    public struct SharedRenderTexture
    {
      public RenderTexture Value;
      public UniformCapture.SharedRenderTextureType Type;
    }

    public class Info
    {
      public Texture[] TextsAtlas;
      public int PlayerIndex;
      public Texture2D BaseMap;
      public CachedTexture CachedBaseMap;
      public static readonly int kBaseMap = Shader.PropertyToID("_BaseMap");
      public static readonly int kPlayerIndex = Shader.PropertyToID("_PlayerIndex");
      public static readonly int kTextsAtlasMask_0 = Shader.PropertyToID("_NamesMap");
      public static readonly int kTextsAtlasMask_1 = Shader.PropertyToID("_NumbersCMap");
      public static readonly int kTextsAtlasMask_2 = Shader.PropertyToID("_NumbersSMap");
      public static readonly int kSkinTone = Shader.PropertyToID("_SkinColor");

      public void Apply(MaterialPropertyBlock mpb, bool applyBaseTexture = true)
      {
        if (applyBaseTexture && (UnityEngine.Object) this.BaseMap != (UnityEngine.Object) null)
          mpb.SetTexture(UniformCapture.Info.kBaseMap, (Texture) this.BaseMap);
        if (this.TextsAtlas != null)
        {
          if (this.TextsAtlas.Length != 0 && (UnityEngine.Object) this.TextsAtlas[0] != (UnityEngine.Object) null)
            mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_0, this.TextsAtlas[0]);
          if (this.TextsAtlas.Length > 1 && (UnityEngine.Object) this.TextsAtlas[1] != (UnityEngine.Object) null)
            mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_1, this.TextsAtlas[1]);
          if (this.TextsAtlas.Length > 2 && (UnityEngine.Object) this.TextsAtlas[2] != (UnityEngine.Object) null)
            mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_2, this.TextsAtlas[2]);
        }
        mpb.SetInt(UniformCapture.Info.kPlayerIndex, this.PlayerIndex);
      }

      public UniformCapture.Info ShallowCopy() => (UniformCapture.Info) this.MemberwiseClone();

      public void Clear()
      {
        if (this.TextsAtlas != null)
        {
          foreach (Texture textsAtla in this.TextsAtlas)
          {
            if (!((UnityEngine.Object) textsAtla == (UnityEngine.Object) null))
              UnityEngine.Object.Destroy((UnityEngine.Object) textsAtla);
          }
          this.TextsAtlas = (Texture[]) null;
        }
        if ((UnityEngine.Object) this.BaseMap != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) this.BaseMap);
        if (this.CachedBaseMap == null)
          return;
        this.CachedBaseMap.Dispose();
      }
    }
  }
}
