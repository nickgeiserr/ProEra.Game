// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_TextureCombiner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_TextureCombiner
  {
    public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;
    public static ShaderTextureProperty[] shaderTexPropertyNames = new ShaderTextureProperty[19]
    {
      new ShaderTextureProperty("_MainTex", false),
      new ShaderTextureProperty("_BumpMap", true),
      new ShaderTextureProperty("_Normal", true),
      new ShaderTextureProperty("_BumpSpecMap", false),
      new ShaderTextureProperty("_DecalTex", false),
      new ShaderTextureProperty("_Detail", false),
      new ShaderTextureProperty("_GlossMap", false),
      new ShaderTextureProperty("_Illum", false),
      new ShaderTextureProperty("_LightTextureB0", false),
      new ShaderTextureProperty("_ParallaxMap", false),
      new ShaderTextureProperty("_ShadowOffset", false),
      new ShaderTextureProperty("_TranslucencyMap", false),
      new ShaderTextureProperty("_SpecMap", false),
      new ShaderTextureProperty("_SpecGlossMap", false),
      new ShaderTextureProperty("_TranspMap", false),
      new ShaderTextureProperty("_MetallicGlossMap", false),
      new ShaderTextureProperty("_OcclusionMap", false),
      new ShaderTextureProperty("_EmissionMap", false),
      new ShaderTextureProperty("_DetailMask", false)
    };
    [SerializeField]
    protected MB2_TextureBakeResults _textureBakeResults;
    [SerializeField]
    protected int _atlasPadding = 1;
    [SerializeField]
    protected int _maxAtlasSize = 1;
    [SerializeField]
    protected bool _resizePowerOfTwoTextures;
    [SerializeField]
    protected bool _fixOutOfBoundsUVs;
    [SerializeField]
    protected int _maxTilingBakeSize = 1024;
    [SerializeField]
    protected bool _saveAtlasesAsAssets;
    [SerializeField]
    protected MB2_PackingAlgorithmEnum _packingAlgorithm;
    [SerializeField]
    protected bool _meshBakerTexturePackerForcePowerOfTwo = true;
    [SerializeField]
    protected List<ShaderTextureProperty> _customShaderPropNames = new List<ShaderTextureProperty>();
    [SerializeField]
    protected bool _normalizeTexelDensity;
    [SerializeField]
    protected bool _considerNonTextureProperties;
    protected TextureBlender resultMaterialTextureBlender;
    protected TextureBlender[] textureBlenders = new TextureBlender[0];
    protected List<Texture2D> _temporaryTextures = new List<Texture2D>();
    public static bool _RunCorutineWithoutPauseIsRunning = false;
    private int __step2_CalculateIdealSizesForTexturesInAtlasAndPadding;
    private Rect[] __createAtlasesMBTexturePacker;
    private static bool LOG_LEVEL_TRACE_MERGE_MAT_SUBRECTS = true;

    public MB2_TextureBakeResults textureBakeResults
    {
      get => this._textureBakeResults;
      set => this._textureBakeResults = value;
    }

    public int atlasPadding
    {
      get => this._atlasPadding;
      set => this._atlasPadding = value;
    }

    public int maxAtlasSize
    {
      get => this._maxAtlasSize;
      set => this._maxAtlasSize = value;
    }

    public bool resizePowerOfTwoTextures
    {
      get => this._resizePowerOfTwoTextures;
      set => this._resizePowerOfTwoTextures = value;
    }

    public bool fixOutOfBoundsUVs
    {
      get => this._fixOutOfBoundsUVs;
      set => this._fixOutOfBoundsUVs = value;
    }

    public int maxTilingBakeSize
    {
      get => this._maxTilingBakeSize;
      set => this._maxTilingBakeSize = value;
    }

    public bool saveAtlasesAsAssets
    {
      get => this._saveAtlasesAsAssets;
      set => this._saveAtlasesAsAssets = value;
    }

    public MB2_PackingAlgorithmEnum packingAlgorithm
    {
      get => this._packingAlgorithm;
      set => this._packingAlgorithm = value;
    }

    public bool meshBakerTexturePackerForcePowerOfTwo
    {
      get => this._meshBakerTexturePackerForcePowerOfTwo;
      set => this._meshBakerTexturePackerForcePowerOfTwo = value;
    }

    public List<ShaderTextureProperty> customShaderPropNames
    {
      get => this._customShaderPropNames;
      set => this._customShaderPropNames = value;
    }

    public bool considerNonTextureProperties
    {
      get => this._considerNonTextureProperties;
      set => this._considerNonTextureProperties = value;
    }

    public static void RunCorutineWithoutPause(IEnumerator cor, int recursionDepth)
    {
      if (recursionDepth == 0)
        MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning = true;
      if (recursionDepth > 20)
      {
        UnityEngine.Debug.LogError((object) "Recursion Depth Exceeded.");
      }
      else
      {
        while (cor.MoveNext())
        {
          switch (cor.Current)
          {
            case IEnumerator _:
              MB3_TextureCombiner.RunCorutineWithoutPause((IEnumerator) cor.Current, recursionDepth + 1);
              continue;
            default:
              continue;
          }
        }
        if (recursionDepth != 0)
          return;
        MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning = false;
      }
    }

    public bool CombineTexturesIntoAtlases(
      ProgressUpdateDelegate progressInfo,
      MB_AtlasesAndRects resultAtlasesAndRects,
      Material resultMaterial,
      List<GameObject> objsToMesh,
      List<Material> allowedMaterialsFilter,
      MB2_EditorMethodsInterface textureEditorMethods = null,
      List<AtlasPackingResult> packingResults = null,
      bool onlyPackRects = false)
    {
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result = new MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult();
      MB3_TextureCombiner.RunCorutineWithoutPause(this._CombineTexturesIntoAtlases(progressInfo, result, resultAtlasesAndRects, resultMaterial, objsToMesh, allowedMaterialsFilter, textureEditorMethods, packingResults, onlyPackRects), 0);
      return result.success;
    }

    public IEnumerator CombineTexturesIntoAtlasesCoroutine(
      ProgressUpdateDelegate progressInfo,
      MB_AtlasesAndRects resultAtlasesAndRects,
      Material resultMaterial,
      List<GameObject> objsToMesh,
      List<Material> allowedMaterialsFilter,
      MB2_EditorMethodsInterface textureEditorMethods = null,
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult coroutineResult = null,
      float maxTimePerFrame = 0.01f,
      List<AtlasPackingResult> packingResults = null,
      bool onlyPackRects = false)
    {
      if (!MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning && (MBVersion.GetMajorVersion() < 5 || MBVersion.GetMajorVersion() == 5 && MBVersion.GetMinorVersion() < 3))
      {
        UnityEngine.Debug.LogError((object) "Running the texture combiner as a coroutine only works in Unity 5.3 and higher");
        yield return (object) null;
      }
      coroutineResult.success = true;
      coroutineResult.isFinished = false;
      if ((double) maxTimePerFrame <= 0.0)
      {
        UnityEngine.Debug.LogError((object) "maxTimePerFrame must be a value greater than zero");
        coroutineResult.isFinished = true;
      }
      else
      {
        yield return (object) this._CombineTexturesIntoAtlases(progressInfo, coroutineResult, resultAtlasesAndRects, resultMaterial, objsToMesh, allowedMaterialsFilter, textureEditorMethods, packingResults, onlyPackRects);
        coroutineResult.isFinished = true;
      }
    }

    private static bool InterfaceFilter(System.Type typeObj, object criteriaObj) => typeObj.ToString() == criteriaObj.ToString();

    private void _LoadTextureBlenders()
    {
      string filterCriteria = "DigitalOpus.MB.Core.TextureBlender";
      TypeFilter filter = new TypeFilter(MB3_TextureCombiner.InterfaceFilter);
      List<System.Type> typeList = new List<System.Type>();
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        IEnumerable enumerable = (IEnumerable) null;
        try
        {
          enumerable = (IEnumerable) assembly.GetTypes();
        }
        catch (Exception ex)
        {
          ex.Equals((object) null);
        }
        if (enumerable != null)
        {
          foreach (System.Type type in assembly.GetTypes())
          {
            if (type.FindInterfaces(filter, (object) filterCriteria).Length != 0)
              typeList.Add(type);
          }
        }
      }
      TextureBlender textureBlender = (TextureBlender) null;
      List<TextureBlender> textureBlenderList = new List<TextureBlender>();
      foreach (System.Type type in typeList)
      {
        if (!type.IsAbstract && !type.IsInterface)
        {
          TextureBlender instance = (TextureBlender) Activator.CreateInstance(type);
          if (instance is TextureBlenderFallback)
            textureBlender = instance;
          else
            textureBlenderList.Add(instance);
        }
      }
      if (textureBlender != null)
        textureBlenderList.Add(textureBlender);
      this.textureBlenders = textureBlenderList.ToArray();
      if (this.LOG_LEVEL < MB2_LogLevel.debug)
        return;
      UnityEngine.Debug.Log((object) string.Format("Loaded {0} TextureBlenders.", (object) this.textureBlenders.Length));
    }

    private bool _CollectPropertyNames(
      Material resultMaterial,
      List<ShaderTextureProperty> texPropertyNames)
    {
      for (int i = 0; i < texPropertyNames.Count; i++)
      {
        ShaderTextureProperty shaderTextureProperty = this._customShaderPropNames.Find((Predicate<ShaderTextureProperty>) (x => x.name.Equals(texPropertyNames[i].name)));
        if (shaderTextureProperty != null)
          this._customShaderPropNames.Remove(shaderTextureProperty);
      }
      Material material = resultMaterial;
      if ((UnityEngine.Object) material == (UnityEngine.Object) null)
      {
        UnityEngine.Debug.LogError((object) "Please assign a result material. The combined mesh will use this material.");
        return false;
      }
      string str = "";
      for (int index = 0; index < MB3_TextureCombiner.shaderTexPropertyNames.Length; ++index)
      {
        if (material.HasProperty(MB3_TextureCombiner.shaderTexPropertyNames[index].name))
        {
          str = str + ", " + MB3_TextureCombiner.shaderTexPropertyNames[index].name;
          if (!texPropertyNames.Contains(MB3_TextureCombiner.shaderTexPropertyNames[index]))
            texPropertyNames.Add(MB3_TextureCombiner.shaderTexPropertyNames[index]);
          if (material.GetTextureOffset(MB3_TextureCombiner.shaderTexPropertyNames[index].name) != new Vector2(0.0f, 0.0f) && this.LOG_LEVEL >= MB2_LogLevel.warn)
            UnityEngine.Debug.LogWarning((object) "Result material has non-zero offset. This is may be incorrect.");
          if (material.GetTextureScale(MB3_TextureCombiner.shaderTexPropertyNames[index].name) != new Vector2(1f, 1f) && this.LOG_LEVEL >= MB2_LogLevel.warn)
            UnityEngine.Debug.LogWarning((object) "Result material should have tiling of 1,1");
        }
      }
      for (int index = 0; index < this._customShaderPropNames.Count; ++index)
      {
        if (material.HasProperty(this._customShaderPropNames[index].name))
        {
          str = str + ", " + this._customShaderPropNames[index].name;
          texPropertyNames.Add(this._customShaderPropNames[index]);
          if (material.GetTextureOffset(this._customShaderPropNames[index].name) != new Vector2(0.0f, 0.0f) && this.LOG_LEVEL >= MB2_LogLevel.warn)
            UnityEngine.Debug.LogWarning((object) "Result material has non-zero offset. This is probably incorrect.");
          if (material.GetTextureScale(this._customShaderPropNames[index].name) != new Vector2(1f, 1f) && this.LOG_LEVEL >= MB2_LogLevel.warn)
            UnityEngine.Debug.LogWarning((object) "Result material should probably have tiling of 1,1.");
        }
        else if (this.LOG_LEVEL >= MB2_LogLevel.warn)
          UnityEngine.Debug.LogWarning((object) ("Result material shader does not use property " + this._customShaderPropNames[index].name + " in the list of custom shader property names"));
      }
      return true;
    }

    private IEnumerator _CombineTexturesIntoAtlases(
      ProgressUpdateDelegate progressInfo,
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result,
      MB_AtlasesAndRects resultAtlasesAndRects,
      Material resultMaterial,
      List<GameObject> objsToMesh,
      List<Material> allowedMaterialsFilter,
      MB2_EditorMethodsInterface textureEditorMethods,
      List<AtlasPackingResult> atlasPackingResult,
      bool onlyPackRects)
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      bool flag;
      try
      {
        this._temporaryTextures.Clear();
        if (textureEditorMethods != null)
        {
          textureEditorMethods.Clear();
          textureEditorMethods.OnPreTextureBake();
        }
        if (objsToMesh == null || objsToMesh.Count == 0)
        {
          UnityEngine.Debug.LogError((object) "No meshes to combine. Please assign some meshes to combine.");
          result.success = false;
          flag = false;
        }
        else if (this._atlasPadding < 0)
        {
          UnityEngine.Debug.LogError((object) "Atlas padding must be zero or greater.");
          result.success = false;
          flag = false;
        }
        else if (this._maxTilingBakeSize < 2 || this._maxTilingBakeSize > 4096)
        {
          UnityEngine.Debug.LogError((object) "Invalid value for max tiling bake size.");
          result.success = false;
          flag = false;
        }
        else
        {
          for (int index = 0; index < objsToMesh.Count; ++index)
          {
            foreach (UnityEngine.Object goMaterial in MB_Utility.GetGOMaterials(objsToMesh[index]))
            {
              if (goMaterial == (UnityEngine.Object) null)
              {
                UnityEngine.Debug.LogError((object) ("Game object " + ((object) objsToMesh[index])?.ToString() + " has a null material"));
                result.success = false;
                flag = false;
                goto label_34;
              }
            }
          }
          if (progressInfo != null)
            progressInfo("Collecting textures for " + objsToMesh.Count.ToString() + " meshes.", 0.01f);
          List<ShaderTextureProperty> texPropertyNames = new List<ShaderTextureProperty>();
          if (!this._CollectPropertyNames(resultMaterial, texPropertyNames))
          {
            result.success = false;
            flag = false;
          }
          else
          {
            if (this._considerNonTextureProperties)
            {
              this._LoadTextureBlenders();
              this.resultMaterialTextureBlender = this.FindMatchingTextureBlender(resultMaterial.shader.name);
              if (this.resultMaterialTextureBlender != null)
              {
                if (this.LOG_LEVEL >= MB2_LogLevel.debug)
                  UnityEngine.Debug.Log((object) ("Using _considerNonTextureProperties found a TextureBlender for result material. Using: " + this.resultMaterialTextureBlender?.ToString()));
              }
              else
              {
                if (this.LOG_LEVEL >= MB2_LogLevel.error)
                  UnityEngine.Debug.LogWarning((object) "Using _considerNonTextureProperties could not find a TextureBlender that matches the shader on the result material. Using the Fallback Texture Blender.");
                this.resultMaterialTextureBlender = (TextureBlender) new TextureBlenderFallback();
              }
            }
            if (onlyPackRects)
              yield return (object) this.__RunTexturePacker(result, texPropertyNames, objsToMesh, allowedMaterialsFilter, textureEditorMethods, atlasPackingResult);
            else
              yield return (object) this.__CombineTexturesIntoAtlases(progressInfo, result, resultAtlasesAndRects, resultMaterial, texPropertyNames, objsToMesh, allowedMaterialsFilter, textureEditorMethods);
            yield break;
          }
        }
label_34:;
      }
      finally
      {
        this._destroyTemporaryTextures();
        if (textureEditorMethods != null)
        {
          textureEditorMethods.RestoreReadFlagsAndFormats(progressInfo);
          textureEditorMethods.OnPostTextureBake();
        }
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) ("===== Done creating atlases for " + ((object) resultMaterial)?.ToString() + " Total time to create atlases " + sw.Elapsed.ToString()));
      }
      return flag;
    }

    private IEnumerator __CombineTexturesIntoAtlases(
      ProgressUpdateDelegate progressInfo,
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result,
      MB_AtlasesAndRects resultAtlasesAndRects,
      Material resultMaterial,
      List<ShaderTextureProperty> texPropertyNames,
      List<GameObject> objsToMesh,
      List<Material> allowedMaterialsFilter,
      MB2_EditorMethodsInterface textureEditorMethods)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        string[] strArray = new string[6];
        strArray[0] = "__CombineTexturesIntoAtlases texture properties in shader:";
        int count = texPropertyNames.Count;
        strArray[1] = count.ToString();
        strArray[2] = " objsToMesh:";
        count = objsToMesh.Count;
        strArray[3] = count.ToString();
        strArray[4] = " _fixOutOfBoundsUVs:";
        strArray[5] = this._fixOutOfBoundsUVs.ToString();
        UnityEngine.Debug.Log((object) string.Concat(strArray));
      }
      if (progressInfo != null)
        progressInfo("Collecting textures ", 0.01f);
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures = new List<MB3_TextureCombiner.MB_TexSet>();
      List<GameObject> usedObjsToMesh = new List<GameObject>();
      yield return (object) this.__Step1_CollectDistinctMatTexturesAndUsedObjects(progressInfo, result, objsToMesh, allowedMaterialsFilter, texPropertyNames, textureEditorMethods, distinctMaterialTextures, usedObjsToMesh);
      if (result.success)
      {
        if (MB3_MeshCombiner.EVAL_VERSION)
        {
          bool flag = true;
          for (int index1 = 0; index1 < distinctMaterialTextures.Count; ++index1)
          {
            for (int index2 = 0; index2 < distinctMaterialTextures[index1].matsAndGOs.mats.Count; ++index2)
            {
              if (!distinctMaterialTextures[index1].matsAndGOs.mats[index2].mat.shader.name.EndsWith("Diffuse") && !distinctMaterialTextures[index1].matsAndGOs.mats[index2].mat.shader.name.EndsWith("Bumped Diffuse"))
              {
                UnityEngine.Debug.LogError((object) ("The free version of Mesh Baker only works with Diffuse and Bumped Diffuse Shaders. The full version can be used with any shader. Material " + distinctMaterialTextures[index1].matsAndGOs.mats[index2].mat.name + " uses shader " + distinctMaterialTextures[index1].matsAndGOs.mats[index2].mat.shader.name));
                flag = false;
              }
            }
          }
          if (!flag)
          {
            result.success = false;
            yield break;
          }
        }
        bool[] allTexturesAreNullAndSameColor = new bool[texPropertyNames.Count];
        yield return (object) this.__Step2_CalculateIdealSizesForTexturesInAtlasAndPadding(progressInfo, result, distinctMaterialTextures, texPropertyNames, allTexturesAreNullAndSameColor, textureEditorMethods);
        if (result.success)
        {
          int inAtlasAndPadding = this.__step2_CalculateIdealSizesForTexturesInAtlasAndPadding;
          yield return (object) this.__Step3_BuildAndSaveAtlasesAndStoreResults(result, progressInfo, distinctMaterialTextures, texPropertyNames, allTexturesAreNullAndSameColor, inAtlasAndPadding, textureEditorMethods, resultAtlasesAndRects, resultMaterial);
        }
      }
    }

    private IEnumerator __RunTexturePacker(
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result,
      List<ShaderTextureProperty> texPropertyNames,
      List<GameObject> objsToMesh,
      List<Material> allowedMaterialsFilter,
      MB2_EditorMethodsInterface textureEditorMethods,
      List<AtlasPackingResult> packingResult)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        string[] strArray = new string[6];
        strArray[0] = "__RunTexturePacker texture properties in shader:";
        int count = texPropertyNames.Count;
        strArray[1] = count.ToString();
        strArray[2] = " objsToMesh:";
        count = objsToMesh.Count;
        strArray[3] = count.ToString();
        strArray[4] = " _fixOutOfBoundsUVs:";
        strArray[5] = this._fixOutOfBoundsUVs.ToString();
        UnityEngine.Debug.Log((object) string.Concat(strArray));
      }
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures = new List<MB3_TextureCombiner.MB_TexSet>();
      List<GameObject> usedObjsToMesh = new List<GameObject>();
      yield return (object) this.__Step1_CollectDistinctMatTexturesAndUsedObjects((ProgressUpdateDelegate) null, result, objsToMesh, allowedMaterialsFilter, texPropertyNames, textureEditorMethods, distinctMaterialTextures, usedObjsToMesh);
      if (result.success)
      {
        bool[] allTexturesAreNullAndSameColor = new bool[texPropertyNames.Count];
        yield return (object) this.__Step2_CalculateIdealSizesForTexturesInAtlasAndPadding((ProgressUpdateDelegate) null, result, distinctMaterialTextures, texPropertyNames, allTexturesAreNullAndSameColor, textureEditorMethods);
        if (result.success)
        {
          int inAtlasAndPadding = this.__step2_CalculateIdealSizesForTexturesInAtlasAndPadding;
          foreach (AtlasPackingResult atlasPackingResult in this.__Step3_RunTexturePacker(distinctMaterialTextures, inAtlasAndPadding))
            packingResult.Add(atlasPackingResult);
        }
      }
    }

    private IEnumerator __Step1_CollectDistinctMatTexturesAndUsedObjects(
      ProgressUpdateDelegate progressInfo,
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result,
      List<GameObject> allObjsToMesh,
      List<Material> allowedMaterialsFilter,
      List<ShaderTextureProperty> texPropertyNames,
      MB2_EditorMethodsInterface textureEditorMethods,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      List<GameObject> usedObjsToMesh)
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      MB3_TextureCombiner mb3TextureCombiner1 = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      bool flag1 = false;
      Dictionary<int, MB_Utility.MeshAnalysisResult[]> dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
      for (int index1 = 0; index1 < allObjsToMesh.Count; ++index1)
      {
        GameObject go = allObjsToMesh[index1];
        if (progressInfo != null)
          progressInfo("Collecting textures for " + ((object) go)?.ToString(), (float) ((double) index1 / (double) allObjsToMesh.Count / 2.0));
        if (mb3TextureCombiner1.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) ("Collecting textures for object " + ((object) go)?.ToString()));
        if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        {
          UnityEngine.Debug.LogError((object) "The list of objects to mesh contained nulls.");
          result.success = false;
          return false;
        }
        UnityEngine.Mesh mesh = MB_Utility.GetMesh(go);
        if ((UnityEngine.Object) mesh == (UnityEngine.Object) null)
        {
          UnityEngine.Debug.LogError((object) ("Object " + go.name + " in the list of objects to mesh has no mesh."));
          result.success = false;
          return false;
        }
        Material[] goMaterials = MB_Utility.GetGOMaterials(go);
        if (goMaterials.Length == 0)
        {
          UnityEngine.Debug.LogError((object) ("Object " + go.name + " in the list of objects has no materials."));
          result.success = false;
          return false;
        }
        MB_Utility.MeshAnalysisResult[] meshAnalysisResultArray;
        if (!dictionary.TryGetValue(mesh.GetInstanceID(), out meshAnalysisResultArray))
        {
          meshAnalysisResultArray = new MB_Utility.MeshAnalysisResult[mesh.subMeshCount];
          for (int index2 = 0; index2 < mesh.subMeshCount; ++index2)
          {
            MB_Utility.hasOutOfBoundsUVs(mesh, ref meshAnalysisResultArray[index2], index2);
            if (mb3TextureCombiner1._normalizeTexelDensity)
              meshAnalysisResultArray[index2].submeshArea = mb3TextureCombiner1.GetSubmeshArea(mesh, index2);
            if (mb3TextureCombiner1._fixOutOfBoundsUVs && !meshAnalysisResultArray[index2].hasUVs)
            {
              meshAnalysisResultArray[index2].uvRect = new Rect(0.0f, 0.0f, 1f, 1f);
              UnityEngine.Debug.LogWarning((object) ("Mesh for object " + ((object) go)?.ToString() + " has no UV channel but 'consider UVs' is enabled. Assuming UVs will be generated filling 0,0,1,1 rectangle."));
            }
          }
          dictionary.Add(mesh.GetInstanceID(), meshAnalysisResultArray);
        }
        if (mb3TextureCombiner1._fixOutOfBoundsUVs && mb3TextureCombiner1.LOG_LEVEL >= MB2_LogLevel.trace)
          UnityEngine.Debug.Log((object) ("Mesh Analysis for object " + ((object) go)?.ToString() + " numSubmesh=" + meshAnalysisResultArray.Length.ToString() + " HasOBUV=" + meshAnalysisResultArray[0].hasOutOfBoundsUVs.ToString() + " UVrectSubmesh0=" + meshAnalysisResultArray[0].uvRect.ToString()));
        for (int index3 = 0; index3 < goMaterials.Length; ++index3)
        {
          MB3_TextureCombiner mb3TextureCombiner = mb3TextureCombiner1;
          if (progressInfo != null)
            progressInfo(string.Format("Collecting textures for {0} submesh {1}", (object) go, (object) index3), (float) ((double) index1 / (double) allObjsToMesh.Count / 2.0));
          Material m = goMaterials[index3];
          if (allowedMaterialsFilter == null || allowedMaterialsFilter.Contains(m))
          {
            flag1 = flag1 || meshAnalysisResultArray[index3].hasOutOfBoundsUVs;
            if (m.name.Contains("(Instance)"))
            {
              UnityEngine.Debug.LogError((object) ("The sharedMaterial on object " + go.name + " has been 'Instanced'. This was probably caused by a script accessing the meshRender.material property in the editor.  The material to UV Rectangle mapping will be incorrect. To fix this recreate the object from its prefab or re-assign its material from the correct asset."));
              result.success = false;
              return false;
            }
            if (mb3TextureCombiner1._fixOutOfBoundsUVs && !MB_Utility.AreAllSharedMaterialsDistinct(goMaterials) && mb3TextureCombiner1.LOG_LEVEL >= MB2_LogLevel.warn)
              UnityEngine.Debug.LogWarning((object) ("Object " + go.name + " uses the same material on multiple submeshes. This may generate strange resultAtlasesAndRects especially when used with fix out of bounds uvs. Try duplicating the material."));
            MB3_TextureCombiner.MeshBakerMaterialTexture[] tss = new MB3_TextureCombiner.MeshBakerMaterialTexture[texPropertyNames.Count];
            for (int index4 = 0; index4 < texPropertyNames.Count; ++index4)
            {
              Texture2D tx = (Texture2D) null;
              Vector2 s = Vector2.one;
              Vector2 o = Vector2.zero;
              float texelDens = 0.0f;
              if (m.HasProperty(texPropertyNames[index4].name))
              {
                Texture texture = m.GetTexture(texPropertyNames[index4].name);
                if ((UnityEngine.Object) texture != (UnityEngine.Object) null)
                {
                  if (texture is Texture2D)
                  {
                    tx = (Texture2D) texture;
                    TextureFormat format = tx.format;
                    bool flag2 = false;
                    if (!Application.isPlaying && textureEditorMethods != null)
                      flag2 = textureEditorMethods.IsNormalMap(tx);
                    if (format != TextureFormat.ARGB32 && format != TextureFormat.RGBA32 && format != TextureFormat.BGRA32 && format != TextureFormat.RGB24 && format != TextureFormat.Alpha8 || flag2)
                    {
                      if (Application.isPlaying && mb3TextureCombiner1._packingAlgorithm != MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Fast)
                      {
                        UnityEngine.Debug.LogError((object) ("Object " + go.name + " in the list of objects to mesh uses Texture " + tx.name + " uses format " + format.ToString() + " that is not in: ARGB32, RGBA32, BGRA32, RGB24, Alpha8 or DXT. These textures cannot be resized at runtime. Try changing texture format. If format says 'compressed' try changing it to 'truecolor'"));
                        result.success = false;
                        return false;
                      }
                      tx = (Texture2D) m.GetTexture(texPropertyNames[index4].name);
                    }
                  }
                  else
                  {
                    UnityEngine.Debug.LogError((object) ("Object " + go.name + " in the list of objects to mesh uses a Texture that is not a Texture2D. Cannot build atlases."));
                    result.success = false;
                    return false;
                  }
                }
                if ((UnityEngine.Object) tx != (UnityEngine.Object) null && mb3TextureCombiner1._normalizeTexelDensity)
                  texelDens = (double) meshAnalysisResultArray[index4].submeshArea != 0.0 ? (float) (tx.width * tx.height) / meshAnalysisResultArray[index4].submeshArea : 0.0f;
                s = m.GetTextureScale(texPropertyNames[index4].name);
                o = m.GetTextureOffset(texPropertyNames[index4].name);
              }
              tss[index4] = new MB3_TextureCombiner.MeshBakerMaterialTexture(tx, o, s, texelDens);
            }
            Vector2 uvScale = new Vector2(meshAnalysisResultArray[index3].uvRect.width, meshAnalysisResultArray[index3].uvRect.height);
            Vector2 uvOffset = new Vector2(meshAnalysisResultArray[index3].uvRect.x, meshAnalysisResultArray[index3].uvRect.y);
            MB3_TextureCombiner.MB_TexSet setOfTexs = new MB3_TextureCombiner.MB_TexSet(tss, uvOffset, uvScale);
            MB3_TextureCombiner.MatAndTransformToMerged transformToMerged = new MB3_TextureCombiner.MatAndTransformToMerged(m);
            setOfTexs.matsAndGOs.mats.Add(transformToMerged);
            MB3_TextureCombiner.MB_TexSet mbTexSet = distinctMaterialTextures.Find((Predicate<MB3_TextureCombiner.MB_TexSet>) (x => x.IsEqual((object) setOfTexs, mb3TextureCombiner._fixOutOfBoundsUVs, mb3TextureCombiner._considerNonTextureProperties, mb3TextureCombiner.resultMaterialTextureBlender)));
            if (mbTexSet != null)
              setOfTexs = mbTexSet;
            else
              distinctMaterialTextures.Add(setOfTexs);
            if (!setOfTexs.matsAndGOs.mats.Contains(transformToMerged))
              setOfTexs.matsAndGOs.mats.Add(transformToMerged);
            if (!setOfTexs.matsAndGOs.gos.Contains(go))
            {
              setOfTexs.matsAndGOs.gos.Add(go);
              if (!usedObjsToMesh.Contains(go))
                usedObjsToMesh.Add(go);
            }
          }
        }
      }
      if (mb3TextureCombiner1.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) string.Format("Step1_CollectDistinctTextures collected {0} sets of textures fixOutOfBoundsUV={1} considerNonTextureProperties={2}", (object) distinctMaterialTextures.Count, (object) mb3TextureCombiner1._fixOutOfBoundsUVs, (object) mb3TextureCombiner1._considerNonTextureProperties));
      if (distinctMaterialTextures.Count == 0)
      {
        UnityEngine.Debug.LogError((object) "None of the source object materials matched any of the allowed materials for this submesh.");
        result.success = false;
        return false;
      }
      mb3TextureCombiner1.MergeOverlappingDistinctMaterialTexturesAndCalcMaterialSubrects(distinctMaterialTextures, mb3TextureCombiner1.fixOutOfBoundsUVs);
      if (mb3TextureCombiner1.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) ("Total time Step1_CollectDistinctTextures " + stopwatch.ElapsedMilliseconds.ToString("f5")));
      return false;
    }

    private IEnumerator __Step2_CalculateIdealSizesForTexturesInAtlasAndPadding(
      ProgressUpdateDelegate progressInfo,
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      List<ShaderTextureProperty> texPropertyNames,
      bool[] allTexturesAreNullAndSameColor,
      MB2_EditorMethodsInterface textureEditorMethods)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      for (int index1 = 0; index1 < texPropertyNames.Count; ++index1)
      {
        bool flag1 = true;
        bool flag2 = true;
        for (int index2 = 0; index2 < distinctMaterialTextures.Count; ++index2)
        {
          if ((UnityEngine.Object) distinctMaterialTextures[index2].ts[index1].t != (UnityEngine.Object) null)
          {
            flag1 = false;
            break;
          }
          if (this._considerNonTextureProperties)
          {
            for (int index3 = index2 + 1; index3 < distinctMaterialTextures.Count; ++index3)
            {
              if (this.resultMaterialTextureBlender.GetColorIfNoTexture(distinctMaterialTextures[index2].matsAndGOs.mats[0].mat, texPropertyNames[index1]) != this.resultMaterialTextureBlender.GetColorIfNoTexture(distinctMaterialTextures[index3].matsAndGOs.mats[0].mat, texPropertyNames[index1]))
              {
                flag2 = false;
                break;
              }
            }
          }
        }
        allTexturesAreNullAndSameColor[index1] = flag1 & flag2;
        if (this.LOG_LEVEL >= MB2_LogLevel.trace)
          UnityEngine.Debug.Log((object) string.Format("AllTexturesAreNullAndSameColor prop: {0} val:{1}", (object) texPropertyNames[index1].name, (object) allTexturesAreNullAndSameColor[index1]));
      }
      int num = this._atlasPadding;
      if (distinctMaterialTextures.Count == 1 && !this._fixOutOfBoundsUVs)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.info)
          UnityEngine.Debug.Log((object) "All objects use the same textures in this set of atlases. Original textures will be reused instead of creating atlases.");
        num = 0;
      }
      else
      {
        if (allTexturesAreNullAndSameColor.Length != texPropertyNames.Count)
          UnityEngine.Debug.LogError((object) "allTexturesAreNullAndSameColor array must be the same length of texPropertyNames.");
        for (int index4 = 0; index4 < distinctMaterialTextures.Count; ++index4)
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            UnityEngine.Debug.Log((object) ("Calculating ideal sizes for texSet TexSet " + index4.ToString() + " of " + distinctMaterialTextures.Count.ToString()));
          MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[index4];
          distinctMaterialTexture.idealWidth = 1;
          distinctMaterialTexture.idealHeight = 1;
          int x1 = 1;
          int x2 = 1;
          if (distinctMaterialTexture.ts.Length != texPropertyNames.Count)
            UnityEngine.Debug.LogError((object) "length of arrays in each element of distinctMaterialTextures must be texPropertyNames.Count");
          for (int index5 = 0; index5 < texPropertyNames.Count; ++index5)
          {
            MB3_TextureCombiner.MeshBakerMaterialTexture t = distinctMaterialTexture.ts[index5];
            Vector2 vector2 = t.matTilingRect.size;
            if (!vector2.Equals(Vector2.one) && distinctMaterialTextures.Count > 1 && this.LOG_LEVEL >= MB2_LogLevel.warn)
            {
              string[] strArray = new string[6]
              {
                "Texture ",
                ((object) t.t)?.ToString(),
                "is tiled by ",
                null,
                null,
                null
              };
              vector2 = t.matTilingRect.size;
              strArray[3] = vector2.ToString();
              strArray[4] = " tiling will be baked into a texture with maxSize:";
              strArray[5] = this._maxTilingBakeSize.ToString();
              UnityEngine.Debug.LogWarning((object) string.Concat(strArray));
            }
            if (!distinctMaterialTexture.obUVscale.Equals(Vector2.one) && distinctMaterialTextures.Count > 1 && this._fixOutOfBoundsUVs && this.LOG_LEVEL >= MB2_LogLevel.warn)
            {
              string[] strArray = new string[6]
              {
                "Texture ",
                ((object) t.t)?.ToString(),
                "has out of bounds UVs that effectively tile by ",
                null,
                null,
                null
              };
              vector2 = distinctMaterialTexture.obUVscale;
              strArray[3] = vector2.ToString();
              strArray[4] = " tiling will be baked into a texture with maxSize:";
              strArray[5] = this._maxTilingBakeSize.ToString();
              UnityEngine.Debug.LogWarning((object) string.Concat(strArray));
            }
            if (!allTexturesAreNullAndSameColor[index5] && (UnityEngine.Object) t.t == (UnityEngine.Object) null)
            {
              if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                UnityEngine.Debug.Log((object) "No source texture creating a 16x16 texture.");
              t.t = this._createTemporaryTexture(16, 16, TextureFormat.ARGB32, true);
              if (this._considerNonTextureProperties && this.resultMaterialTextureBlender != null)
              {
                Color colorIfNoTexture = this.resultMaterialTextureBlender.GetColorIfNoTexture(distinctMaterialTexture.matsAndGOs.mats[0].mat, texPropertyNames[index5]);
                if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                  UnityEngine.Debug.Log((object) ("Setting texture to solid color " + colorIfNoTexture.ToString()));
                MB_Utility.setSolidColor(t.t, colorIfNoTexture);
              }
              else
              {
                Color colorIfNoTexture = MB3_TextureCombiner.GetColorIfNoTexture(texPropertyNames[index5]);
                MB_Utility.setSolidColor(t.t, colorIfNoTexture);
              }
              t.encapsulatingSamplingRect = !this.fixOutOfBoundsUVs ? new DRect(0.0f, 0.0f, 1f, 1f) : distinctMaterialTexture.obUVrect;
            }
            if ((UnityEngine.Object) t.t != (UnityEngine.Object) null)
            {
              Vector2 offset2Dimensions = this.GetAdjustedForScaleAndOffset2Dimensions(t, distinctMaterialTexture.obUVoffset, distinctMaterialTexture.obUVscale);
              if ((int) ((double) offset2Dimensions.x * (double) offset2Dimensions.y) > x1 * x2)
              {
                if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                {
                  string[] strArray = new string[8];
                  strArray[0] = "    matTex ";
                  strArray[1] = ((object) t.t)?.ToString();
                  strArray[2] = " ";
                  vector2 = offset2Dimensions;
                  strArray[3] = vector2.ToString();
                  strArray[4] = " has a bigger size than ";
                  strArray[5] = x1.ToString();
                  strArray[6] = " ";
                  strArray[7] = x2.ToString();
                  UnityEngine.Debug.Log((object) string.Concat(strArray));
                }
                x1 = (int) offset2Dimensions.x;
                x2 = (int) offset2Dimensions.y;
              }
            }
          }
          if (this._resizePowerOfTwoTextures)
          {
            if (x1 <= num * 5)
              UnityEngine.Debug.LogWarning((object) string.Format("Some of the textures have widths close to the size of the padding. It is not recommended to use _resizePowerOfTwoTextures with widths this small.", (object) distinctMaterialTexture.ToString()));
            if (x2 <= num * 5)
              UnityEngine.Debug.LogWarning((object) string.Format("Some of the textures have heights close to the size of the padding. It is not recommended to use _resizePowerOfTwoTextures with heights this small.", (object) distinctMaterialTexture.ToString()));
            if (this.IsPowerOfTwo(x1))
              x1 -= num * 2;
            if (this.IsPowerOfTwo(x2))
              x2 -= num * 2;
            if (x1 < 1)
              x1 = 1;
            if (x2 < 1)
              x2 = 1;
          }
          if (this.LOG_LEVEL >= MB2_LogLevel.trace)
            UnityEngine.Debug.Log((object) ("    Ideal size is " + x1.ToString() + " " + x2.ToString()));
          distinctMaterialTexture.idealWidth = x1;
          distinctMaterialTexture.idealHeight = x2;
        }
      }
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) ("Total time Step2 Calculate Ideal Sizes part1: " + stopwatch.Elapsed.ToString()));
      if (distinctMaterialTextures.Count > 1 && this._packingAlgorithm != MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Fast)
      {
        for (int index6 = 0; index6 < distinctMaterialTextures.Count; ++index6)
        {
          for (int index7 = 0; index7 < texPropertyNames.Count; ++index7)
          {
            Texture2D t = distinctMaterialTextures[index6].ts[index7].t;
            if ((UnityEngine.Object) t != (UnityEngine.Object) null && textureEditorMethods != null)
            {
              if (progressInfo != null)
                progressInfo(string.Format("Convert texture {0} to readable format ", (object) t), 0.5f);
              textureEditorMethods.AddTextureFormat(t, texPropertyNames[index7].isNormalMap);
            }
          }
        }
      }
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) ("Total time Step2 Calculate Ideal Sizes part2: " + stopwatch.Elapsed.ToString()));
      this.__step2_CalculateIdealSizesForTexturesInAtlasAndPadding = num;
      yield break;
    }

    private AtlasPackingResult[] __Step3_RunTexturePacker(
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      int _padding)
    {
      AtlasPackingResult[] atlasPackingResultArray = this.__RuntTexturePackerOnly(distinctMaterialTextures, _padding);
      for (int index1 = 0; index1 < atlasPackingResultArray.Length; ++index1)
      {
        List<MB3_TextureCombiner.MatsAndGOs> matsAndGosList = new List<MB3_TextureCombiner.MatsAndGOs>();
        atlasPackingResultArray[index1].data = (object) matsAndGosList;
        for (int index2 = 0; index2 < atlasPackingResultArray[index1].srcImgIdxs.Length; ++index2)
        {
          MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[atlasPackingResultArray[index1].srcImgIdxs[index2]];
          matsAndGosList.Add(distinctMaterialTexture.matsAndGOs);
        }
      }
      return atlasPackingResultArray;
    }

    private IEnumerator __Step3_BuildAndSaveAtlasesAndStoreResults(
      MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result,
      ProgressUpdateDelegate progressInfo,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      List<ShaderTextureProperty> texPropertyNames,
      bool[] allTexturesAreNullAndSameColor,
      int _padding,
      MB2_EditorMethodsInterface textureEditorMethods,
      MB_AtlasesAndRects resultAtlasesAndRects,
      Material resultMaterial)
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      int numAtlases = texPropertyNames.Count;
      StringBuilder report = new StringBuilder();
      int num;
      if (numAtlases > 0)
      {
        report = new StringBuilder();
        report.AppendLine("Report");
        for (int index1 = 0; index1 < distinctMaterialTextures.Count; ++index1)
        {
          MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[index1];
          report.AppendLine("----------");
          report.Append("This set of textures will be resized to:" + distinctMaterialTexture.idealWidth.ToString() + "x" + distinctMaterialTexture.idealHeight.ToString() + "\n");
          for (int index2 = 0; index2 < distinctMaterialTexture.ts.Length; ++index2)
          {
            if ((UnityEngine.Object) distinctMaterialTexture.ts[index2].t != (UnityEngine.Object) null)
            {
              StringBuilder stringBuilder1 = report;
              string[] strArray = new string[9]
              {
                "   [",
                texPropertyNames[index2].name,
                " ",
                distinctMaterialTexture.ts[index2].t.name,
                " ",
                null,
                null,
                null,
                null
              };
              num = distinctMaterialTexture.ts[index2].t.width;
              strArray[5] = num.ToString();
              strArray[6] = "x";
              num = distinctMaterialTexture.ts[index2].t.height;
              strArray[7] = num.ToString();
              strArray[8] = "]";
              string str1 = string.Concat(strArray);
              stringBuilder1.Append(str1);
              if (distinctMaterialTexture.ts[index2].matTilingRect.size != Vector2.one || distinctMaterialTexture.ts[index2].matTilingRect.min != Vector2.zero)
              {
                StringBuilder stringBuilder2 = report;
                Vector2 vector2 = distinctMaterialTexture.ts[index2].matTilingRect.size;
                string str2 = vector2.ToString("G4");
                vector2 = distinctMaterialTexture.ts[index2].matTilingRect.min;
                string str3 = vector2.ToString("G4");
                stringBuilder2.AppendFormat(" material scale {0} offset{1} ", (object) str2, (object) str3);
              }
              if (distinctMaterialTexture.obUVscale != Vector2.one || distinctMaterialTexture.obUVoffset != Vector2.zero)
                report.AppendFormat(" obUV scale {0} offset{1} ", (object) distinctMaterialTexture.obUVscale.ToString("G4"), (object) distinctMaterialTexture.obUVoffset.ToString("G4"));
              report.AppendLine("");
            }
            else
            {
              report.Append("   [" + texPropertyNames[index2].name + " null ");
              if (allTexturesAreNullAndSameColor[index2])
                report.Append("no atlas will be created all textures null]\n");
              else
                report.AppendFormat("a 16x16 texture will be created]\n");
            }
          }
          report.AppendLine("");
          report.Append("Materials using:");
          for (int index3 = 0; index3 < distinctMaterialTexture.matsAndGOs.mats.Count; ++index3)
            report.Append(distinctMaterialTexture.matsAndGOs.mats[index3].mat.name + ", ");
          report.AppendLine("");
        }
      }
      GC.Collect();
      Texture2D[] atlases = new Texture2D[numAtlases];
      TimeSpan elapsed;
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        elapsed = sw.Elapsed;
        UnityEngine.Debug.Log((object) ("time Step 3 Create And Save Atlases part 1 " + elapsed.ToString()));
      }
      Rect[] rectArray;
      if (this._packingAlgorithm == MB2_PackingAlgorithmEnum.UnitysPackTextures)
        rectArray = this.__CreateAtlasesUnityTexturePacker(progressInfo, numAtlases, distinctMaterialTextures, texPropertyNames, allTexturesAreNullAndSameColor, resultMaterial, atlases, textureEditorMethods, _padding);
      else if (this._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker)
      {
        yield return (object) this.__CreateAtlasesMBTexturePacker(progressInfo, numAtlases, distinctMaterialTextures, texPropertyNames, allTexturesAreNullAndSameColor, resultMaterial, atlases, textureEditorMethods, _padding);
        rectArray = this.__createAtlasesMBTexturePacker;
      }
      else
        rectArray = this.__CreateAtlasesMBTexturePackerFast(progressInfo, numAtlases, distinctMaterialTextures, texPropertyNames, allTexturesAreNullAndSameColor, resultMaterial, atlases, textureEditorMethods, _padding);
      float elapsedMilliseconds = (float) sw.ElapsedMilliseconds;
      this.AdjustNonTextureProperties(resultMaterial, texPropertyNames, distinctMaterialTextures, this._considerNonTextureProperties, textureEditorMethods);
      if (progressInfo != null)
        progressInfo("Building Report", 0.7f);
      StringBuilder stringBuilder3 = new StringBuilder();
      stringBuilder3.AppendLine("---- Atlases ------");
      for (int index = 0; index < numAtlases; ++index)
      {
        if ((UnityEngine.Object) atlases[index] != (UnityEngine.Object) null)
        {
          StringBuilder stringBuilder4 = stringBuilder3;
          string[] strArray = new string[6]
          {
            "Created Atlas For: ",
            texPropertyNames[index].name,
            " h=",
            null,
            null,
            null
          };
          num = atlases[index].height;
          strArray[3] = num.ToString();
          strArray[4] = " w=";
          num = atlases[index].width;
          strArray[5] = num.ToString();
          string str = string.Concat(strArray);
          stringBuilder4.AppendLine(str);
        }
        else if (allTexturesAreNullAndSameColor[index])
          stringBuilder3.AppendLine("Did not create atlas for " + texPropertyNames[index].name + " because all source textures were null.");
      }
      report.Append(stringBuilder3.ToString());
      List<MB_MaterialAndUVRect> materialAndUvRectList = new List<MB_MaterialAndUVRect>();
      for (int index4 = 0; index4 < distinctMaterialTextures.Count; ++index4)
      {
        List<MB3_TextureCombiner.MatAndTransformToMerged> mats = distinctMaterialTextures[index4].matsAndGOs.mats;
        Rect samplingEncapsulatinRect = new Rect(0.0f, 0.0f, 1f, 1f);
        if (distinctMaterialTextures[index4].ts.Length != 0)
          samplingEncapsulatinRect = !distinctMaterialTextures[index4].allTexturesUseSameMatTiling ? distinctMaterialTextures[index4].obUVrect.GetRect() : distinctMaterialTextures[index4].ts[0].encapsulatingSamplingRect.GetRect();
        for (int index5 = 0; index5 < mats.Count; ++index5)
        {
          MB_MaterialAndUVRect materialAndUvRect = new MB_MaterialAndUVRect(mats[index5].mat, rectArray[index4], mats[index5].samplingRectMatAndUVTiling.GetRect(), mats[index5].materialTiling.GetRect(), samplingEncapsulatinRect, mats[index5].objName);
          if (!materialAndUvRectList.Contains(materialAndUvRect))
            materialAndUvRectList.Add(materialAndUvRect);
        }
      }
      resultAtlasesAndRects.atlases = atlases;
      resultAtlasesAndRects.texPropertyNames = ShaderTextureProperty.GetNames(texPropertyNames);
      resultAtlasesAndRects.mat2rect_map = materialAndUvRectList;
      if (progressInfo != null)
        progressInfo("Restoring Texture Formats & Read Flags", 0.8f);
      this._destroyTemporaryTextures();
      textureEditorMethods?.RestoreReadFlagsAndFormats(progressInfo);
      if (report != null && this.LOG_LEVEL >= MB2_LogLevel.info)
        UnityEngine.Debug.Log((object) report.ToString());
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) ("Time Step 3 Create And Save Atlases part 3 " + ((float) sw.ElapsedMilliseconds - elapsedMilliseconds).ToString("f5")));
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        elapsed = sw.Elapsed;
        UnityEngine.Debug.Log((object) ("Total time Step 3 Create And Save Atlases " + elapsed.ToString()));
      }
    }

    private AtlasPackingResult[] __RuntTexturePackerOnly(
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      int _padding)
    {
      AtlasPackingResult[] atlasPackingResultArray;
      if (distinctMaterialTextures.Count == 1 && !this._fixOutOfBoundsUVs)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) "Only one image per atlas. Will re-use original texture");
        atlasPackingResultArray = new AtlasPackingResult[1]
        {
          new AtlasPackingResult()
        };
        atlasPackingResultArray[0].rects = new Rect[1];
        atlasPackingResultArray[0].srcImgIdxs = new int[1];
        atlasPackingResultArray[0].rects[0] = new Rect(0.0f, 0.0f, 1f, 1f);
        Texture2D texture2D = (Texture2D) null;
        MB3_TextureCombiner.MeshBakerMaterialTexture bakerMaterialTexture = (MB3_TextureCombiner.MeshBakerMaterialTexture) null;
        if (distinctMaterialTextures[0].ts.Length != 0)
        {
          bakerMaterialTexture = distinctMaterialTextures[0].ts[0];
          texture2D = bakerMaterialTexture.t;
        }
        atlasPackingResultArray[0].atlasX = (UnityEngine.Object) texture2D == (UnityEngine.Object) null ? 16 : bakerMaterialTexture.t.width;
        atlasPackingResultArray[0].atlasY = (UnityEngine.Object) texture2D == (UnityEngine.Object) null ? 16 : bakerMaterialTexture.t.height;
        atlasPackingResultArray[0].usedW = (UnityEngine.Object) texture2D == (UnityEngine.Object) null ? 16 : bakerMaterialTexture.t.width;
        atlasPackingResultArray[0].usedH = (UnityEngine.Object) texture2D == (UnityEngine.Object) null ? 16 : bakerMaterialTexture.t.height;
      }
      else
      {
        List<Vector2> imgWidthHeights = new List<Vector2>();
        for (int index = 0; index < distinctMaterialTextures.Count; ++index)
          imgWidthHeights.Add(new Vector2((float) distinctMaterialTextures[index].idealWidth, (float) distinctMaterialTextures[index].idealHeight));
        atlasPackingResultArray = new MB2_TexturePacker()
        {
          doPowerOfTwoTextures = this._meshBakerTexturePackerForcePowerOfTwo
        }.GetRects(imgWidthHeights, this._maxAtlasSize, _padding, true);
      }
      return atlasPackingResultArray;
    }

    private IEnumerator __CreateAtlasesMBTexturePacker(
      ProgressUpdateDelegate progressInfo,
      int numAtlases,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      List<ShaderTextureProperty> texPropertyNames,
      bool[] allTexturesAreNullAndSameColor,
      Material resultMaterial,
      Texture2D[] atlases,
      MB2_EditorMethodsInterface textureEditorMethods,
      int _padding)
    {
      Rect[] uvRects;
      if (distinctMaterialTextures.Count == 1 && !this._fixOutOfBoundsUVs)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) "Only one image per atlas. Will re-use original texture");
        uvRects = new Rect[1]
        {
          new Rect(0.0f, 0.0f, 1f, 1f)
        };
        for (int index = 0; index < numAtlases; ++index)
        {
          MB3_TextureCombiner.MeshBakerMaterialTexture t = distinctMaterialTextures[0].ts[index];
          atlases[index] = t.t;
          resultMaterial.SetTexture(texPropertyNames[index].name, (Texture) atlases[index]);
          resultMaterial.SetTextureScale(texPropertyNames[index].name, t.matTilingRect.size);
          resultMaterial.SetTextureOffset(texPropertyNames[index].name, t.matTilingRect.min);
        }
      }
      else
      {
        List<Vector2> imgWidthHeights = new List<Vector2>();
        for (int index = 0; index < distinctMaterialTextures.Count; ++index)
          imgWidthHeights.Add(new Vector2((float) distinctMaterialTextures[index].idealWidth, (float) distinctMaterialTextures[index].idealHeight));
        MB2_TexturePacker mb2TexturePacker = new MB2_TexturePacker();
        mb2TexturePacker.doPowerOfTwoTextures = this._meshBakerTexturePackerForcePowerOfTwo;
        int atlasSizeX = 1;
        int atlasSizeY = 1;
        int maxAtlasSize = this._maxAtlasSize;
        AtlasPackingResult[] rects = mb2TexturePacker.GetRects(imgWidthHeights, maxAtlasSize, _padding);
        atlasSizeX = rects[0].atlasX;
        atlasSizeY = rects[0].atlasY;
        uvRects = rects[0].rects;
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) ("Generated atlas will be " + atlasSizeX.ToString() + "x" + atlasSizeY.ToString() + " (Max atlas size for platform: " + maxAtlasSize.ToString() + ")"));
        int num;
        for (int propIdx = 0; propIdx < numAtlases; num = propIdx++)
        {
          Texture2D texture2D;
          if (allTexturesAreNullAndSameColor[propIdx])
          {
            texture2D = (Texture2D) null;
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              UnityEngine.Debug.Log((object) ("=== Not creating atlas for " + texPropertyNames[propIdx].name + " because textures are null and default value parameters are the same."));
          }
          else
          {
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              UnityEngine.Debug.Log((object) ("=== Creating atlas for " + texPropertyNames[propIdx].name));
            GC.Collect();
            Color[][] atlasPixels = new Color[atlasSizeY][];
            for (int index = 0; index < atlasPixels.Length; ++index)
              atlasPixels[index] = new Color[atlasSizeX];
            bool isNormalMap = false;
            if (texPropertyNames[propIdx].isNormalMap)
              isNormalMap = true;
            for (int texSetIdx = 0; texSetIdx < distinctMaterialTextures.Count; ++texSetIdx)
            {
              string msg = "Creating Atlas '" + texPropertyNames[propIdx].name + "' texture " + distinctMaterialTextures[texSetIdx]?.ToString();
              if (progressInfo != null)
                progressInfo(msg, 0.01f);
              MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[texSetIdx];
              if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                UnityEngine.Debug.Log((object) string.Format("Adding texture {0} to atlas {1}", (UnityEngine.Object) distinctMaterialTexture.ts[propIdx].t == (UnityEngine.Object) null ? (object) "null" : (object) ((object) distinctMaterialTexture.ts[propIdx].t).ToString(), (object) texPropertyNames[propIdx]));
              Rect rect = uvRects[texSetIdx];
              Texture2D t = distinctMaterialTexture.ts[propIdx].t;
              int targX = Mathf.RoundToInt(rect.x * (float) atlasSizeX);
              int targY = Mathf.RoundToInt(rect.y * (float) atlasSizeY);
              int targW = Mathf.RoundToInt(rect.width * (float) atlasSizeX);
              int targH = Mathf.RoundToInt(rect.height * (float) atlasSizeY);
              if (targW == 0 || targH == 0)
                UnityEngine.Debug.LogError((object) "Image in atlas has no height or width");
              if (progressInfo != null)
                progressInfo(msg + " set ReadWrite flag", 0.01f);
              textureEditorMethods?.SetReadWriteFlag(t, true, true);
              if (progressInfo != null)
                progressInfo(msg + "Copying to atlas: '" + ((object) distinctMaterialTexture.ts[propIdx].t)?.ToString() + "'", 0.02f);
              DRect encapsulatingSamplingRect = distinctMaterialTexture.ts[propIdx].encapsulatingSamplingRect;
              yield return (object) this.CopyScaledAndTiledToAtlas(distinctMaterialTexture.ts[propIdx], distinctMaterialTexture, texPropertyNames[propIdx], encapsulatingSamplingRect, targX, targY, targW, targH, this._fixOutOfBoundsUVs, this._maxTilingBakeSize, atlasPixels, atlasSizeX, isNormalMap, progressInfo);
            }
            yield return (object) numAtlases;
            if (progressInfo != null)
              progressInfo("Applying changes to atlas: '" + texPropertyNames[propIdx].name + "'", 0.03f);
            texture2D = new Texture2D(atlasSizeX, atlasSizeY, TextureFormat.ARGB32, true);
            for (int y = 0; y < atlasPixels.Length; ++y)
              texture2D.SetPixels(0, y, atlasSizeX, 1, atlasPixels[y]);
            texture2D.Apply();
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            {
              string[] strArray = new string[6]
              {
                "Saving atlas ",
                texPropertyNames[propIdx].name,
                " w=",
                null,
                null,
                null
              };
              num = texture2D.width;
              strArray[3] = num.ToString();
              strArray[4] = " h=";
              num = texture2D.height;
              strArray[5] = num.ToString();
              UnityEngine.Debug.Log((object) string.Concat(strArray));
            }
            atlasPixels = (Color[][]) null;
          }
          atlases[propIdx] = texture2D;
          if (progressInfo != null)
            progressInfo("Saving atlas: '" + texPropertyNames[propIdx].name + "'", 0.04f);
          if (this._saveAtlasesAsAssets && textureEditorMethods != null)
            textureEditorMethods.SaveAtlasToAssetDatabase(atlases[propIdx], texPropertyNames[propIdx], propIdx, resultMaterial);
          else
            resultMaterial.SetTexture(texPropertyNames[propIdx].name, (Texture) atlases[propIdx]);
          resultMaterial.SetTextureOffset(texPropertyNames[propIdx].name, Vector2.zero);
          resultMaterial.SetTextureScale(texPropertyNames[propIdx].name, Vector2.one);
          this._destroyTemporaryTextures();
        }
      }
      this.__createAtlasesMBTexturePacker = uvRects;
    }

    private Rect[] __CreateAtlasesMBTexturePackerFast(
      ProgressUpdateDelegate progressInfo,
      int numAtlases,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      List<ShaderTextureProperty> texPropertyNames,
      bool[] allTexturesAreNullAndSameColor,
      Material resultMaterial,
      Texture2D[] atlases,
      MB2_EditorMethodsInterface textureEditorMethods,
      int _padding)
    {
      Rect[] texturePackerFast;
      if (distinctMaterialTextures.Count == 1 && !this._fixOutOfBoundsUVs)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) "Only one image per atlas. Will re-use original texture");
        texturePackerFast = new Rect[1]
        {
          new Rect(0.0f, 0.0f, 1f, 1f)
        };
        for (int index = 0; index < numAtlases; ++index)
        {
          MB3_TextureCombiner.MeshBakerMaterialTexture t = distinctMaterialTextures[0].ts[index];
          atlases[index] = t.t;
          resultMaterial.SetTexture(texPropertyNames[index].name, (Texture) atlases[index]);
          resultMaterial.SetTextureScale(texPropertyNames[index].name, t.matTilingRect.size);
          resultMaterial.SetTextureOffset(texPropertyNames[index].name, t.matTilingRect.min);
        }
      }
      else
      {
        List<Vector2> imgWidthHeights = new List<Vector2>();
        for (int index = 0; index < distinctMaterialTextures.Count; ++index)
          imgWidthHeights.Add(new Vector2((float) distinctMaterialTextures[index].idealWidth, (float) distinctMaterialTextures[index].idealHeight));
        MB2_TexturePacker mb2TexturePacker = new MB2_TexturePacker();
        mb2TexturePacker.doPowerOfTwoTextures = this._meshBakerTexturePackerForcePowerOfTwo;
        int maxAtlasSize = this._maxAtlasSize;
        AtlasPackingResult[] rects = mb2TexturePacker.GetRects(imgWidthHeights, maxAtlasSize, _padding);
        int atlasX = rects[0].atlasX;
        int atlasY = rects[0].atlasY;
        texturePackerFast = rects[0].rects;
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) ("Generated atlas will be " + atlasX.ToString() + "x" + atlasY.ToString() + " (Max atlas size for platform: " + maxAtlasSize.ToString() + ")"));
        GameObject o = (GameObject) null;
        try
        {
          o = new GameObject("MBrenderAtlasesGO");
          MB3_AtlasPackerRenderTexture packerRenderTexture = o.AddComponent<MB3_AtlasPackerRenderTexture>();
          o.AddComponent<Camera>();
          if (this._considerNonTextureProperties && this.LOG_LEVEL >= MB2_LogLevel.warn)
            UnityEngine.Debug.LogWarning((object) "Blend Non-Texture Properties has limited functionality when used with Mesh Baker Texture Packer Fast.");
          for (int index = 0; index < numAtlases; ++index)
          {
            Texture2D texture2D;
            if (allTexturesAreNullAndSameColor[index])
            {
              texture2D = (Texture2D) null;
              if (this.LOG_LEVEL >= MB2_LogLevel.debug)
                UnityEngine.Debug.Log((object) ("Not creating atlas for " + texPropertyNames[index].name + " because textures are null and default value parameters are the same."));
            }
            else
            {
              GC.Collect();
              if (progressInfo != null)
                progressInfo("Creating Atlas '" + texPropertyNames[index].name + "'", 0.01f);
              if (this.LOG_LEVEL >= MB2_LogLevel.debug)
                UnityEngine.Debug.Log((object) ("About to render " + texPropertyNames[index].name + " isNormal=" + texPropertyNames[index].isNormalMap.ToString()));
              packerRenderTexture.LOG_LEVEL = this.LOG_LEVEL;
              packerRenderTexture.width = atlasX;
              packerRenderTexture.height = atlasY;
              packerRenderTexture.padding = _padding;
              packerRenderTexture.rects = texturePackerFast;
              packerRenderTexture.textureSets = distinctMaterialTextures;
              packerRenderTexture.indexOfTexSetToRender = index;
              packerRenderTexture.texPropertyName = texPropertyNames[index];
              packerRenderTexture.isNormalMap = texPropertyNames[index].isNormalMap;
              packerRenderTexture.fixOutOfBoundsUVs = this._fixOutOfBoundsUVs;
              packerRenderTexture.considerNonTextureProperties = this._considerNonTextureProperties;
              packerRenderTexture.resultMaterialTextureBlender = this.resultMaterialTextureBlender;
              texture2D = packerRenderTexture.OnRenderAtlas(this);
              if (this.LOG_LEVEL >= MB2_LogLevel.debug)
                UnityEngine.Debug.Log((object) ("Saving atlas " + texPropertyNames[index].name + " w=" + texture2D.width.ToString() + " h=" + texture2D.height.ToString() + " id=" + texture2D.GetInstanceID().ToString()));
            }
            atlases[index] = texture2D;
            if (progressInfo != null)
              progressInfo("Saving atlas: '" + texPropertyNames[index].name + "'", 0.04f);
            if (this._saveAtlasesAsAssets && textureEditorMethods != null)
              textureEditorMethods.SaveAtlasToAssetDatabase(atlases[index], texPropertyNames[index], index, resultMaterial);
            else
              resultMaterial.SetTexture(texPropertyNames[index].name, (Texture) atlases[index]);
            resultMaterial.SetTextureOffset(texPropertyNames[index].name, Vector2.zero);
            resultMaterial.SetTextureScale(texPropertyNames[index].name, Vector2.one);
            this._destroyTemporaryTextures();
          }
        }
        catch (Exception ex)
        {
          UnityEngine.Debug.LogException(ex);
        }
        finally
        {
          if ((UnityEngine.Object) o != (UnityEngine.Object) null)
            MB_Utility.Destroy((UnityEngine.Object) o);
        }
      }
      return texturePackerFast;
    }

    private Rect[] __CreateAtlasesUnityTexturePacker(
      ProgressUpdateDelegate progressInfo,
      int numAtlases,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      List<ShaderTextureProperty> texPropertyNames,
      bool[] allTexturesAreNullAndSameColor,
      Material resultMaterial,
      Texture2D[] atlases,
      MB2_EditorMethodsInterface textureEditorMethods,
      int _padding)
    {
      Rect[] rs;
      if (distinctMaterialTextures.Count == 1 && !this._fixOutOfBoundsUVs)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) "Only one image per atlas. Will re-use original texture");
        rs = new Rect[1]{ new Rect(0.0f, 0.0f, 1f, 1f) };
        for (int index = 0; index < numAtlases; ++index)
        {
          MB3_TextureCombiner.MeshBakerMaterialTexture t = distinctMaterialTextures[0].ts[index];
          atlases[index] = t.t;
          resultMaterial.SetTexture(texPropertyNames[index].name, (Texture) atlases[index]);
          resultMaterial.SetTextureScale(texPropertyNames[index].name, t.matTilingRect.size);
          resultMaterial.SetTextureOffset(texPropertyNames[index].name, t.matTilingRect.min);
        }
      }
      else
      {
        long num1 = 0;
        int w = 1;
        int h = 1;
        rs = (Rect[]) null;
        for (int index1 = 0; index1 < numAtlases; ++index1)
        {
          Texture2D texture2D1;
          if (allTexturesAreNullAndSameColor[index1])
          {
            texture2D1 = (Texture2D) null;
          }
          else
          {
            int num2;
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            {
              string str1 = index1.ToString();
              num2 = this._temporaryTextures.Count;
              string str2 = num2.ToString();
              UnityEngine.Debug.LogWarning((object) ("Beginning loop " + str1 + " num temporary textures " + str2));
            }
            for (int index2 = 0; index2 < distinctMaterialTextures.Count; ++index2)
            {
              MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[index2];
              int idealWidth = distinctMaterialTexture.idealWidth;
              int idealHeight = distinctMaterialTexture.idealHeight;
              Texture2D texture2D2 = distinctMaterialTexture.ts[index1].t;
              if ((UnityEngine.Object) texture2D2 == (UnityEngine.Object) null)
              {
                texture2D2 = distinctMaterialTexture.ts[index1].t = this._createTemporaryTexture(idealWidth, idealHeight, TextureFormat.ARGB32, true);
                if (this._considerNonTextureProperties && this.resultMaterialTextureBlender != null)
                {
                  Color colorIfNoTexture = this.resultMaterialTextureBlender.GetColorIfNoTexture(distinctMaterialTexture.matsAndGOs.mats[0].mat, texPropertyNames[index1]);
                  if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                    UnityEngine.Debug.Log((object) ("Setting texture to solid color " + colorIfNoTexture.ToString()));
                  MB_Utility.setSolidColor(texture2D2, colorIfNoTexture);
                }
                else
                {
                  Color colorIfNoTexture = MB3_TextureCombiner.GetColorIfNoTexture(texPropertyNames[index1]);
                  MB_Utility.setSolidColor(texture2D2, colorIfNoTexture);
                }
              }
              if (progressInfo != null)
                progressInfo("Adjusting for scale and offset " + ((object) texture2D2)?.ToString(), 0.01f);
              textureEditorMethods?.SetReadWriteFlag(texture2D2, true, true);
              Texture2D t = this.GetAdjustedForScaleAndOffset2(distinctMaterialTexture.ts[index1], distinctMaterialTexture.obUVoffset, distinctMaterialTexture.obUVscale);
              if (t.width != idealWidth || t.height != idealHeight)
              {
                if (progressInfo != null)
                  progressInfo("Resizing texture '" + ((object) t)?.ToString() + "'", 0.01f);
                if (this.LOG_LEVEL >= MB2_LogLevel.debug)
                {
                  string[] strArray = new string[10];
                  strArray[0] = "Copying and resizing texture ";
                  strArray[1] = texPropertyNames[index1].name;
                  strArray[2] = " from ";
                  num2 = t.width;
                  strArray[3] = num2.ToString();
                  strArray[4] = "x";
                  num2 = t.height;
                  strArray[5] = num2.ToString();
                  strArray[6] = " to ";
                  strArray[7] = idealWidth.ToString();
                  strArray[8] = "x";
                  strArray[9] = idealHeight.ToString();
                  UnityEngine.Debug.LogWarning((object) string.Concat(strArray));
                }
                t = this._resizeTexture(t, idealWidth, idealHeight);
              }
              distinctMaterialTexture.ts[index1].t = t;
            }
            Texture2D[] texture2DArray = new Texture2D[distinctMaterialTextures.Count];
            for (int index3 = 0; index3 < distinctMaterialTextures.Count; ++index3)
            {
              Texture2D t = distinctMaterialTextures[index3].ts[index1].t;
              num1 += (long) (t.width * t.height);
              if (this._considerNonTextureProperties)
                t = this.TintTextureWithTextureCombiner(t, distinctMaterialTextures[index3], texPropertyNames[index1]);
              texture2DArray[index3] = t;
            }
            textureEditorMethods?.CheckBuildSettings(num1);
            if (Math.Sqrt((double) num1) > 3500.0 && this.LOG_LEVEL >= MB2_LogLevel.warn)
              UnityEngine.Debug.LogWarning((object) "The maximum possible atlas size is 4096. Textures may be shrunk");
            texture2D1 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
            if (progressInfo != null)
              progressInfo("Packing texture atlas " + texPropertyNames[index1].name, 0.25f);
            if (index1 == 0)
            {
              double num3;
              if (progressInfo != null)
              {
                ProgressUpdateDelegate progressUpdateDelegate = progressInfo;
                num3 = Math.Sqrt((double) num1);
                string msg = "Estimated min size of atlases: " + num3.ToString("F0");
                progressUpdateDelegate(msg, 0.1f);
              }
              if (this.LOG_LEVEL >= MB2_LogLevel.info)
              {
                num3 = Math.Sqrt((double) num1);
                UnityEngine.Debug.Log((object) ("Estimated atlas minimum size:" + num3.ToString("F0")));
              }
              this._addWatermark(texture2DArray);
              if (distinctMaterialTextures.Count == 1 && !this._fixOutOfBoundsUVs)
              {
                rs = new Rect[1]
                {
                  new Rect(0.0f, 0.0f, 1f, 1f)
                };
                texture2D1 = this._copyTexturesIntoAtlas(texture2DArray, _padding, rs, texture2DArray[0].width, texture2DArray[0].height);
              }
              else
              {
                int maximumAtlasSize = 4096;
                rs = texture2D1.PackTextures(texture2DArray, _padding, maximumAtlasSize, false);
              }
              if (this.LOG_LEVEL >= MB2_LogLevel.info)
              {
                num2 = texture2D1.width;
                string str3 = num2.ToString();
                num2 = texture2D1.height;
                string str4 = num2.ToString();
                UnityEngine.Debug.Log((object) ("After pack textures atlas size " + str3 + " " + str4));
              }
              w = texture2D1.width;
              h = texture2D1.height;
              texture2D1.Apply();
            }
            else
            {
              if (progressInfo != null)
                progressInfo("Copying Textures Into: " + texPropertyNames[index1].name, 0.1f);
              texture2D1 = this._copyTexturesIntoAtlas(texture2DArray, _padding, rs, w, h);
            }
          }
          atlases[index1] = texture2D1;
          if (this._saveAtlasesAsAssets && textureEditorMethods != null)
            textureEditorMethods.SaveAtlasToAssetDatabase(atlases[index1], texPropertyNames[index1], index1, resultMaterial);
          resultMaterial.SetTextureOffset(texPropertyNames[index1].name, Vector2.zero);
          resultMaterial.SetTextureScale(texPropertyNames[index1].name, Vector2.one);
          this._destroyTemporaryTextures();
          GC.Collect();
        }
      }
      return rs;
    }

    private void _addWatermark(Texture2D[] texToPack)
    {
    }

    private Texture2D _addWatermark(Texture2D texToPack) => texToPack;

    private Texture2D _copyTexturesIntoAtlas(
      Texture2D[] texToPack,
      int padding,
      Rect[] rs,
      int w,
      int h)
    {
      Texture2D t = new Texture2D(w, h, TextureFormat.ARGB32, true);
      MB_Utility.setSolidColor(t, Color.clear);
      for (int index = 0; index < rs.Length; ++index)
      {
        Rect r = rs[index];
        Texture2D source = texToPack[index];
        int x = Mathf.RoundToInt(r.x * (float) w);
        int y = Mathf.RoundToInt(r.y * (float) h);
        int num1 = Mathf.RoundToInt(r.width * (float) w);
        int num2 = Mathf.RoundToInt(r.height * (float) h);
        if (source.width != num1 && source.height != num2)
        {
          source = MB_Utility.resampleTexture(source, num1, num2);
          this._temporaryTextures.Add(source);
        }
        t.SetPixels(x, y, num1, num2, source.GetPixels());
      }
      t.Apply();
      return t;
    }

    private bool IsPowerOfTwo(int x) => (x & x - 1) == 0;

    private void MergeOverlappingDistinctMaterialTexturesAndCalcMaterialSubrects(
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      bool fixOutOfBoundsUVs)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) nameof (MergeOverlappingDistinctMaterialTexturesAndCalcMaterialSubrects));
      int num1 = 0;
      for (int index1 = 0; index1 < distinctMaterialTextures.Count; ++index1)
      {
        MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[index1];
        int num2 = -1;
        bool flag = true;
        DRect drect = new DRect();
        for (int index2 = 0; index2 < distinctMaterialTexture.ts.Length; ++index2)
        {
          if (num2 != -1)
          {
            if ((UnityEngine.Object) distinctMaterialTexture.ts[index2].t != (UnityEngine.Object) null && drect != distinctMaterialTexture.ts[index2].matTilingRect)
              flag = false;
          }
          else if ((UnityEngine.Object) distinctMaterialTexture.ts[index2].t != (UnityEngine.Object) null)
          {
            num2 = index2;
            drect = distinctMaterialTexture.ts[index2].matTilingRect;
          }
        }
        if (MB3_TextureCombiner.LOG_LEVEL_TRACE_MERGE_MAT_SUBRECTS)
          UnityEngine.Debug.LogFormat("TextureSet {0} allTexturesUseSameMatTiling = {1}", (object) index1, (object) flag);
        if (flag)
        {
          distinctMaterialTexture.allTexturesUseSameMatTiling = true;
        }
        else
        {
          if (this.LOG_LEVEL <= MB2_LogLevel.info || MB3_TextureCombiner.LOG_LEVEL_TRACE_MERGE_MAT_SUBRECTS)
            UnityEngine.Debug.Log((object) string.Format("Textures in material(s) do not all use the same material tiling. This set of textures will not be considered for merge: {0} ", (object) distinctMaterialTexture.GetDescription()));
          distinctMaterialTexture.allTexturesUseSameMatTiling = false;
        }
      }
      for (int index3 = 0; index3 < distinctMaterialTextures.Count; ++index3)
      {
        MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[index3];
        DRect drect = !fixOutOfBoundsUVs ? new DRect(0.0, 0.0, 1.0, 1.0) : new DRect(distinctMaterialTexture.obUVoffset, distinctMaterialTexture.obUVscale);
        for (int index4 = 0; index4 < distinctMaterialTexture.matsAndGOs.mats.Count; ++index4)
        {
          distinctMaterialTexture.matsAndGOs.mats[index4].obUVRectIfTilingSame = drect;
          distinctMaterialTexture.matsAndGOs.mats[index4].objName = distinctMaterialTextures[index3].matsAndGOs.gos[0].name;
        }
        distinctMaterialTexture.CalcInitialFullSamplingRects(fixOutOfBoundsUVs);
        distinctMaterialTexture.CalcMatAndUVSamplingRects();
      }
      List<int> intList = new List<int>();
      for (int index5 = 0; index5 < distinctMaterialTextures.Count; ++index5)
      {
        MB3_TextureCombiner.MB_TexSet distinctMaterialTexture1 = distinctMaterialTextures[index5];
        for (int index6 = index5 + 1; index6 < distinctMaterialTextures.Count; ++index6)
        {
          MB3_TextureCombiner.MB_TexSet distinctMaterialTexture2 = distinctMaterialTextures[index6];
          if (distinctMaterialTexture2.AllTexturesAreSameForMerge(distinctMaterialTexture1, this._considerNonTextureProperties, this.resultMaterialTextureBlender))
          {
            double num3 = 0.0;
            double num4 = 0.0;
            DRect drect1 = new DRect();
            int num5 = -1;
            for (int index7 = 0; index7 < distinctMaterialTexture1.ts.Length; ++index7)
            {
              if ((UnityEngine.Object) distinctMaterialTexture1.ts[index7].t != (UnityEngine.Object) null && num5 == -1)
                num5 = index7;
            }
            if (num5 != -1)
            {
              DRect uvRect1 = distinctMaterialTexture2.matsAndGOs.mats[0].samplingRectMatAndUVTiling;
              for (int index8 = 1; index8 < distinctMaterialTexture2.matsAndGOs.mats.Count; ++index8)
                uvRect1 = MB3_UVTransformUtility.GetEncapsulatingRect(ref uvRect1, ref distinctMaterialTexture2.matsAndGOs.mats[index8].samplingRectMatAndUVTiling);
              DRect drect2 = distinctMaterialTexture1.matsAndGOs.mats[0].samplingRectMatAndUVTiling;
              for (int index9 = 1; index9 < distinctMaterialTexture1.matsAndGOs.mats.Count; ++index9)
                drect2 = MB3_UVTransformUtility.GetEncapsulatingRect(ref drect2, ref distinctMaterialTexture1.matsAndGOs.mats[index9].samplingRectMatAndUVTiling);
              drect1 = MB3_UVTransformUtility.GetEncapsulatingRect(ref uvRect1, ref drect2);
              num3 += drect1.width * drect1.height;
              num4 += uvRect1.width * uvRect1.height + drect2.width * drect2.height;
            }
            else
              drect1 = new DRect(0.0f, 0.0f, 1f, 1f);
            if (num3 < num4)
            {
              ++num1;
              StringBuilder stringBuilder = (StringBuilder) null;
              if (this.LOG_LEVEL >= MB2_LogLevel.info)
              {
                stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("About To Merge:\n   TextureSet1 {0}\n   TextureSet2 {1}\n", (object) distinctMaterialTexture2.GetDescription(), (object) distinctMaterialTexture1.GetDescription());
                if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                {
                  for (int index10 = 0; index10 < distinctMaterialTexture2.matsAndGOs.mats.Count; ++index10)
                    stringBuilder.AppendFormat("tx1 Mat {0} matAndMeshUVRect {1} fullSamplingRect {2}\n", (object) distinctMaterialTexture2.matsAndGOs.mats[index10].mat, (object) distinctMaterialTexture2.matsAndGOs.mats[index10].samplingRectMatAndUVTiling, (object) distinctMaterialTexture2.ts[0].encapsulatingSamplingRect);
                  for (int index11 = 0; index11 < distinctMaterialTexture1.matsAndGOs.mats.Count; ++index11)
                    stringBuilder.AppendFormat("tx2 Mat {0} matAndMeshUVRect {1} fullSamplingRect {2}\n", (object) distinctMaterialTexture1.matsAndGOs.mats[index11].mat, (object) distinctMaterialTexture1.matsAndGOs.mats[index11].samplingRectMatAndUVTiling, (object) distinctMaterialTexture1.ts[0].encapsulatingSamplingRect);
                }
              }
              for (int index12 = 0; index12 < distinctMaterialTexture1.matsAndGOs.gos.Count; ++index12)
              {
                if (!distinctMaterialTexture2.matsAndGOs.gos.Contains(distinctMaterialTexture1.matsAndGOs.gos[index12]))
                  distinctMaterialTexture2.matsAndGOs.gos.Add(distinctMaterialTexture1.matsAndGOs.gos[index12]);
              }
              for (int index13 = 0; index13 < distinctMaterialTexture1.matsAndGOs.mats.Count; ++index13)
                distinctMaterialTexture2.matsAndGOs.mats.Add(distinctMaterialTexture1.matsAndGOs.mats[index13]);
              distinctMaterialTexture2.matsAndGOs.mats.Sort((IComparer<MB3_TextureCombiner.MatAndTransformToMerged>) new MB3_TextureCombiner.SamplingRectEnclosesComparer());
              for (int index14 = 0; index14 < distinctMaterialTexture2.ts.Length; ++index14)
                distinctMaterialTexture2.ts[index14].encapsulatingSamplingRect = drect1;
              if (!intList.Contains(index5))
                intList.Add(index5);
              if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              {
                if (this.LOG_LEVEL >= MB2_LogLevel.trace)
                {
                  stringBuilder.AppendFormat("=== After Merge TextureSet {0}\n", (object) distinctMaterialTexture2.GetDescription());
                  for (int index15 = 0; index15 < distinctMaterialTexture2.matsAndGOs.mats.Count; ++index15)
                    stringBuilder.AppendFormat("tx1 Mat {0} matAndMeshUVRect {1} fullSamplingRect {2}\n", (object) distinctMaterialTexture2.matsAndGOs.mats[index15].mat, (object) distinctMaterialTexture2.matsAndGOs.mats[index15].samplingRectMatAndUVTiling, (object) distinctMaterialTexture2.ts[0].encapsulatingSamplingRect);
                  if (MB3_MeshBakerRoot.DO_INTEGRITY_CHECKS && MB3_MeshBakerRoot.DO_INTEGRITY_CHECKS)
                    this.DoIntegrityCheckMergedAtlasRects(distinctMaterialTextures);
                }
                UnityEngine.Debug.Log((object) stringBuilder.ToString());
                break;
              }
              break;
            }
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              UnityEngine.Debug.Log((object) string.Format("Considered merging {0} and {1} but there was not enough overlap. It is more efficient to bake these to separate rectangles.", (object) distinctMaterialTexture2.GetDescription(), (object) distinctMaterialTexture1.GetDescription()));
          }
        }
      }
      for (int index = intList.Count - 1; index >= 0; --index)
        distinctMaterialTextures.RemoveAt(intList[index]);
      intList.Clear();
      if (this.LOG_LEVEL >= MB2_LogLevel.info)
        UnityEngine.Debug.Log((object) string.Format("MergeOverlappingDistinctMaterialTexturesAndCalcMaterialSubrects complete merged {0}", (object) num1));
      if (!MB3_MeshBakerRoot.DO_INTEGRITY_CHECKS)
        return;
      this.DoIntegrityCheckMergedAtlasRects(distinctMaterialTextures);
    }

    private void DoIntegrityCheckMergedAtlasRects(
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures)
    {
      if (!MB3_MeshBakerRoot.DO_INTEGRITY_CHECKS)
        return;
      for (int index1 = 0; index1 < distinctMaterialTextures.Count; ++index1)
      {
        MB3_TextureCombiner.MB_TexSet distinctMaterialTexture = distinctMaterialTextures[index1];
        if (distinctMaterialTexture.allTexturesUseSameMatTiling)
        {
          for (int index2 = 0; index2 < distinctMaterialTexture.matsAndGOs.mats.Count; ++index2)
          {
            MB3_TextureCombiner.MatAndTransformToMerged mat = distinctMaterialTexture.matsAndGOs.mats[index2];
            DRect rectIfTilingSame = mat.obUVRectIfTilingSame;
            DRect materialTiling = mat.materialTiling;
            if (!MB2_TextureBakeResults.IsMeshAndMaterialRectEnclosedByAtlasRect(rectIfTilingSame.GetRect(), materialTiling.GetRect(), distinctMaterialTexture.ts[0].encapsulatingSamplingRect.GetRect(), this.LOG_LEVEL))
            {
              UnityEngine.Debug.LogErrorFormat("mesh " + distinctMaterialTexture.matsAndGOs.mats[index2].objName + "\n uv=" + rectIfTilingSame.ToString() + "\n mat=" + materialTiling.GetRect().ToString("f5") + "\n samplingRect=" + distinctMaterialTexture.matsAndGOs.mats[index2].samplingRectMatAndUVTiling.GetRect().ToString("f4") + "\n encapsulatingRect " + distinctMaterialTexture.ts[0].encapsulatingSamplingRect.GetRect().ToString("f4") + "\n");
              UnityEngine.Debug.LogErrorFormat(string.Format("Integrity check failed. " + distinctMaterialTexture.matsAndGOs.mats[index2].objName + " Encapsulating sampling rect failed to contain potentialRect\n"));
            }
          }
        }
      }
    }

    private Vector2 GetAdjustedForScaleAndOffset2Dimensions(
      MB3_TextureCombiner.MeshBakerMaterialTexture source,
      Vector2 obUVoffset,
      Vector2 obUVscale)
    {
      if (source.matTilingRect.x == 0.0 && source.matTilingRect.y == 0.0 && source.matTilingRect.width == 1.0 && source.matTilingRect.height == 1.0)
      {
        if (!this._fixOutOfBoundsUVs)
          return new Vector2((float) source.t.width, (float) source.t.height);
        if ((double) obUVoffset.x == 0.0 && (double) obUVoffset.y == 0.0 && (double) obUVscale.x == 1.0 && (double) obUVscale.y == 1.0)
          return new Vector2((float) source.t.width, (float) source.t.height);
      }
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        string[] strArray = new string[6]
        {
          "GetAdjustedForScaleAndOffset2Dimensions: ",
          ((object) source.t)?.ToString(),
          " ",
          null,
          null,
          null
        };
        Vector2 vector2 = obUVoffset;
        strArray[3] = vector2.ToString();
        strArray[4] = " ";
        vector2 = obUVscale;
        strArray[5] = vector2.ToString();
        UnityEngine.Debug.Log((object) string.Concat(strArray));
      }
      float x = (float) source.encapsulatingSamplingRect.width * (float) source.t.width;
      float y = (float) source.encapsulatingSamplingRect.height * (float) source.t.height;
      if ((double) x > (double) this._maxTilingBakeSize)
        x = (float) this._maxTilingBakeSize;
      if ((double) y > (double) this._maxTilingBakeSize)
        y = (float) this._maxTilingBakeSize;
      if ((double) x < 1.0)
        x = 1f;
      if ((double) y < 1.0)
        y = 1f;
      return new Vector2(x, y);
    }

    public Texture2D GetAdjustedForScaleAndOffset2(
      MB3_TextureCombiner.MeshBakerMaterialTexture source,
      Vector2 obUVoffset,
      Vector2 obUVscale)
    {
      if (source.matTilingRect.x == 0.0 && source.matTilingRect.y == 0.0 && source.matTilingRect.width == 1.0 && source.matTilingRect.height == 1.0 && (!this._fixOutOfBoundsUVs || (double) obUVoffset.x == 0.0 && (double) obUVoffset.y == 0.0 && (double) obUVscale.x == 1.0 && (double) obUVscale.y == 1.0))
        return source.t;
      Vector2 offset2Dimensions = this.GetAdjustedForScaleAndOffset2Dimensions(source, obUVoffset, obUVscale);
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        string[] strArray = new string[6]
        {
          "GetAdjustedForScaleAndOffset2: ",
          ((object) source.t)?.ToString(),
          " ",
          null,
          null,
          null
        };
        Vector2 vector2 = obUVoffset;
        strArray[3] = vector2.ToString();
        strArray[4] = " ";
        vector2 = obUVscale;
        strArray[5] = vector2.ToString();
        UnityEngine.Debug.LogWarning((object) string.Concat(strArray));
      }
      float x1 = offset2Dimensions.x;
      float y1 = offset2Dimensions.y;
      float width = (float) source.matTilingRect.width;
      float height = (float) source.matTilingRect.height;
      float num1 = (float) source.matTilingRect.x;
      float num2 = (float) source.matTilingRect.y;
      if (this._fixOutOfBoundsUVs)
      {
        width *= obUVscale.x;
        height *= obUVscale.y;
        num1 = (float) source.matTilingRect.x * obUVscale.x + obUVoffset.x;
        num2 = (float) source.matTilingRect.y * obUVscale.y + obUVoffset.y;
      }
      Texture2D temporaryTexture = this._createTemporaryTexture((int) x1, (int) y1, TextureFormat.ARGB32, true);
      for (int x2 = 0; x2 < temporaryTexture.width; ++x2)
      {
        for (int y2 = 0; y2 < temporaryTexture.height; ++y2)
        {
          float u = (float) x2 / x1 * width + num1;
          float v = (float) y2 / y1 * height + num2;
          temporaryTexture.SetPixel(x2, y2, source.t.GetPixelBilinear(u, v));
        }
      }
      temporaryTexture.Apply();
      return temporaryTexture;
    }

    internal static DRect GetSourceSamplingRect(
      MB3_TextureCombiner.MeshBakerMaterialTexture source,
      Vector2 obUVoffset,
      Vector2 obUVscale)
    {
      DRect matTilingRect = source.matTilingRect;
      DRect r2 = new DRect(obUVoffset, obUVscale);
      return MB3_UVTransformUtility.CombineTransforms(ref matTilingRect, ref r2);
    }

    private Texture2D TintTextureWithTextureCombiner(
      Texture2D t,
      MB3_TextureCombiner.MB_TexSet sourceMaterial,
      ShaderTextureProperty shaderPropertyName)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.trace)
        UnityEngine.Debug.Log((object) string.Format("Blending texture {0} mat {1} with non-texture properties using TextureBlender {2}", (object) t.name, (object) sourceMaterial.matsAndGOs.mats[0].mat, (object) this.resultMaterialTextureBlender));
      this.resultMaterialTextureBlender.OnBeforeTintTexture(sourceMaterial.matsAndGOs.mats[0].mat, shaderPropertyName.name);
      t = this._createTextureCopy(t);
      for (int y = 0; y < t.height; ++y)
      {
        Color[] pixels = t.GetPixels(0, y, t.width, 1);
        for (int index = 0; index < pixels.Length; ++index)
          pixels[index] = this.resultMaterialTextureBlender.OnBlendTexturePixel(shaderPropertyName.name, pixels[index]);
        t.SetPixels(0, y, t.width, 1, pixels);
      }
      t.Apply();
      return t;
    }

    public IEnumerator CopyScaledAndTiledToAtlas(
      MB3_TextureCombiner.MeshBakerMaterialTexture source,
      MB3_TextureCombiner.MB_TexSet sourceMaterial,
      ShaderTextureProperty shaderPropertyName,
      DRect srcSamplingRect,
      int targX,
      int targY,
      int targW,
      int targH,
      bool _fixOutOfBoundsUVs,
      int maxSize,
      Color[][] atlasPixels,
      int atlasWidth,
      bool isNormalMap,
      ProgressUpdateDelegate progressInfo = null)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) ("CopyScaledAndTiledToAtlas: " + ((object) source.t)?.ToString() + " inAtlasX=" + targX.ToString() + " inAtlasY=" + targY.ToString() + " inAtlasW=" + targW.ToString() + " inAtlasH=" + targH.ToString()));
      float num1 = (float) targW;
      float num2 = (float) targH;
      float num3 = (float) srcSamplingRect.width;
      float num4 = (float) srcSamplingRect.height;
      float x = (float) srcSamplingRect.x;
      float y = (float) srcSamplingRect.y;
      int w = (int) num1;
      int h = (int) num2;
      Texture2D texture2D1 = source.t;
      if ((UnityEngine.Object) texture2D1 == (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.trace)
          UnityEngine.Debug.Log((object) "No source texture creating a 16x16 texture.");
        texture2D1 = this._createTemporaryTexture(16, 16, TextureFormat.ARGB32, true);
        num3 = 1f;
        num4 = 1f;
        if (this._considerNonTextureProperties && this.resultMaterialTextureBlender != null)
        {
          Color colorIfNoTexture = this.resultMaterialTextureBlender.GetColorIfNoTexture(sourceMaterial.matsAndGOs.mats[0].mat, shaderPropertyName);
          if (this.LOG_LEVEL >= MB2_LogLevel.trace)
            UnityEngine.Debug.Log((object) ("Setting texture to solid color " + colorIfNoTexture.ToString()));
          MB_Utility.setSolidColor(texture2D1, colorIfNoTexture);
        }
        else
        {
          Color colorIfNoTexture = MB3_TextureCombiner.GetColorIfNoTexture(shaderPropertyName);
          MB_Utility.setSolidColor(texture2D1, colorIfNoTexture);
        }
      }
      if (this._considerNonTextureProperties && this.resultMaterialTextureBlender != null)
        texture2D1 = this.TintTextureWithTextureCombiner(texture2D1, sourceMaterial, shaderPropertyName);
      Texture2D texture2D2 = this._addWatermark(texture2D1);
      for (int index1 = 0; index1 < w; ++index1)
      {
        if (progressInfo != null && w > 0)
          progressInfo("CopyScaledAndTiledToAtlas " + ((float) ((double) index1 / (double) w * 100.0)).ToString("F0"), 0.2f);
        for (int index2 = 0; index2 < h; ++index2)
        {
          float u = (float) index1 / num1 * num3 + x;
          float v = (float) index2 / num2 * num4 + y;
          atlasPixels[targY + index2][targX + index1] = texture2D2.GetPixelBilinear(u, v);
        }
      }
      for (int index3 = 0; index3 < w; ++index3)
      {
        for (int index4 = 1; index4 <= this.atlasPadding; ++index4)
        {
          atlasPixels[targY - index4][targX + index3] = atlasPixels[targY][targX + index3];
          atlasPixels[targY + h - 1 + index4][targX + index3] = atlasPixels[targY + h - 1][targX + index3];
        }
      }
      for (int index5 = 0; index5 < h; ++index5)
      {
        for (int index6 = 1; index6 <= this._atlasPadding; ++index6)
        {
          atlasPixels[targY + index5][targX - index6] = atlasPixels[targY + index5][targX];
          atlasPixels[targY + index5][targX + w + index6 - 1] = atlasPixels[targY + index5][targX + w - 1];
        }
      }
      for (int i = 1; i <= this._atlasPadding; ++i)
      {
        for (int j = 1; j <= this._atlasPadding; ++j)
        {
          atlasPixels[targY - j][targX - i] = atlasPixels[targY][targX];
          atlasPixels[targY + h - 1 + j][targX - i] = atlasPixels[targY + h - 1][targX];
          atlasPixels[targY + h - 1 + j][targX + w + i - 1] = atlasPixels[targY + h - 1][targX + w - 1];
          atlasPixels[targY - j][targX + w + i - 1] = atlasPixels[targY][targX + w - 1];
          yield return (object) null;
        }
        yield return (object) null;
      }
    }

    public Texture2D _createTemporaryTexture(int w, int h, TextureFormat texFormat, bool mipMaps)
    {
      Texture2D t = new Texture2D(w, h, texFormat, mipMaps);
      MB_Utility.setSolidColor(t, Color.clear);
      this._temporaryTextures.Add(t);
      return t;
    }

    internal Texture2D _createTextureCopy(Texture2D t)
    {
      Texture2D textureCopy = MB_Utility.createTextureCopy(t);
      this._temporaryTextures.Add(textureCopy);
      return textureCopy;
    }

    private Texture2D _resizeTexture(Texture2D t, int w, int h)
    {
      Texture2D texture2D = MB_Utility.resampleTexture(t, w, h);
      this._temporaryTextures.Add(texture2D);
      return texture2D;
    }

    private void _destroyTemporaryTextures()
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        UnityEngine.Debug.Log((object) ("Destroying " + this._temporaryTextures.Count.ToString() + " temporary textures"));
      for (int index = 0; index < this._temporaryTextures.Count; ++index)
        MB_Utility.Destroy((UnityEngine.Object) this._temporaryTextures[index]);
      this._temporaryTextures.Clear();
    }

    public void SuggestTreatment(
      List<GameObject> objsToMesh,
      Material[] resultMaterials,
      List<ShaderTextureProperty> _customShaderPropNames)
    {
      this._customShaderPropNames = _customShaderPropNames;
      StringBuilder stringBuilder = new StringBuilder();
      Dictionary<int, MB_Utility.MeshAnalysisResult[]> dictionary1 = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
      for (int index1 = 0; index1 < objsToMesh.Count; ++index1)
      {
        GameObject go = objsToMesh[index1];
        if (!((UnityEngine.Object) go == (UnityEngine.Object) null))
        {
          Material[] goMaterials = MB_Utility.GetGOMaterials(objsToMesh[index1]);
          if (goMaterials.Length > 1)
          {
            stringBuilder.AppendFormat("\nObject {0} uses {1} materials. Possible treatments:\n", (object) objsToMesh[index1].name, (object) goMaterials.Length);
            stringBuilder.AppendFormat("  1) Collapse the submeshes together into one submesh in the combined mesh. Each of the original submesh materials will map to a different UV rectangle in the atlas(es) used by the combined material.\n");
            stringBuilder.AppendFormat("  2) Use the multiple materials feature to map submeshes in the source mesh to submeshes in the combined mesh.\n");
          }
          UnityEngine.Mesh mesh = MB_Utility.GetMesh(go);
          MB_Utility.MeshAnalysisResult[] meshAnalysisResultArray;
          if (!dictionary1.TryGetValue(mesh.GetInstanceID(), out meshAnalysisResultArray))
          {
            meshAnalysisResultArray = new MB_Utility.MeshAnalysisResult[mesh.subMeshCount];
            MB_Utility.doSubmeshesShareVertsOrTris(mesh, ref meshAnalysisResultArray[0]);
            for (int submeshIndex = 0; submeshIndex < mesh.subMeshCount; ++submeshIndex)
            {
              MB_Utility.hasOutOfBoundsUVs(mesh, ref meshAnalysisResultArray[submeshIndex], submeshIndex);
              meshAnalysisResultArray[submeshIndex].hasOverlappingSubmeshTris = meshAnalysisResultArray[0].hasOverlappingSubmeshTris;
              meshAnalysisResultArray[submeshIndex].hasOverlappingSubmeshVerts = meshAnalysisResultArray[0].hasOverlappingSubmeshVerts;
            }
            dictionary1.Add(mesh.GetInstanceID(), meshAnalysisResultArray);
          }
          for (int index2 = 0; index2 < goMaterials.Length; ++index2)
          {
            if (meshAnalysisResultArray[index2].hasOutOfBoundsUVs)
            {
              DRect drect = new DRect(meshAnalysisResultArray[index2].uvRect);
              stringBuilder.AppendFormat("\nObject {0} submesh={1} material={2} uses UVs outside the range 0,0 .. 1,1 to create tiling that tiles the box {3},{4} .. {5},{6}. This is a problem because the UVs outside the 0,0 .. 1,1 rectangle will pick up neighboring textures in the atlas. Possible Treatments:\n", (object) go, (object) index2, (object) goMaterials[index2], (object) drect.x.ToString("G4"), (object) drect.y.ToString("G4"), (object) (drect.x + drect.width).ToString("G4"), (object) (drect.y + drect.height).ToString("G4"));
              stringBuilder.AppendFormat("    1) Ignore the problem. The tiling may not affect result significantly.\n");
              stringBuilder.AppendFormat("    2) Use the 'fix out of bounds UVs' feature to bake the tiling and scale the UVs to fit in the 0,0 .. 1,1 rectangle.\n");
              stringBuilder.AppendFormat("    3) Use the Multiple Materials feature to map the material on this submesh to its own submesh in the combined mesh. No other materials should map to this submesh. This will result in only one texture in the atlas(es) and the UVs should tile correctly.\n");
              stringBuilder.AppendFormat("    4) Combine only meshes that use the same (or subset of) the set of materials on this mesh. The original material(s) can be applied to the result\n");
            }
          }
          if (meshAnalysisResultArray[0].hasOverlappingSubmeshVerts)
          {
            stringBuilder.AppendFormat("\nObject {0} has submeshes that share vertices. This is a problem because each vertex can have only one UV coordinate and may be required to map to different positions in the various atlases that are generated. Possible treatments:\n", (object) objsToMesh[index1]);
            stringBuilder.AppendFormat(" 1) Ignore the problem. The vertices may not affect the result.\n");
            stringBuilder.AppendFormat(" 2) Use the Multiple Materials feature to map the submeshs that overlap to their own submeshs in the combined mesh. No other materials should map to this submesh. This will result in only one texture in the atlas(es) and the UVs should tile correctly.\n");
            stringBuilder.AppendFormat(" 3) Combine only meshes that use the same (or subset of) the set of materials on this mesh. The original material(s) can be applied to the result\n");
          }
        }
      }
      Dictionary<Material, List<GameObject>> dictionary2 = new Dictionary<Material, List<GameObject>>();
      for (int index3 = 0; index3 < objsToMesh.Count; ++index3)
      {
        if ((UnityEngine.Object) objsToMesh[index3] != (UnityEngine.Object) null)
        {
          Material[] goMaterials = MB_Utility.GetGOMaterials(objsToMesh[index3]);
          for (int index4 = 0; index4 < goMaterials.Length; ++index4)
          {
            if ((UnityEngine.Object) goMaterials[index4] != (UnityEngine.Object) null)
            {
              List<GameObject> gameObjectList;
              if (!dictionary2.TryGetValue(goMaterials[index4], out gameObjectList))
              {
                gameObjectList = new List<GameObject>();
                dictionary2.Add(goMaterials[index4], gameObjectList);
              }
              if (!gameObjectList.Contains(objsToMesh[index3]))
                gameObjectList.Add(objsToMesh[index3]);
            }
          }
        }
      }
      List<ShaderTextureProperty> texPropertyNames = new List<ShaderTextureProperty>();
      for (int index5 = 0; index5 < resultMaterials.Length; ++index5)
      {
        this._CollectPropertyNames(resultMaterials[index5], texPropertyNames);
        foreach (Material key in dictionary2.Keys)
        {
          for (int index6 = 0; index6 < texPropertyNames.Count; ++index6)
          {
            if (key.HasProperty(texPropertyNames[index6].name))
            {
              Texture texture = key.GetTexture(texPropertyNames[index6].name);
              if ((UnityEngine.Object) texture != (UnityEngine.Object) null)
              {
                Vector2 textureOffset = key.GetTextureOffset(texPropertyNames[index6].name);
                Vector3 textureScale = (Vector3) key.GetTextureScale(texPropertyNames[index6].name);
                if ((double) textureOffset.x < 0.0 || (double) textureOffset.x + (double) textureScale.x > 1.0 || (double) textureOffset.y < 0.0 || (double) textureOffset.y + (double) textureScale.y > 1.0)
                {
                  stringBuilder.AppendFormat("\nMaterial {0} used by objects {1} uses texture {2} that is tiled (scale={3} offset={4}). If there is more than one texture in the atlas  then Mesh Baker will bake the tiling into the atlas. If the baked tiling is large then quality can be lost. Possible treatments:\n", (object) key, (object) this.PrintList(dictionary2[key]), (object) texture, (object) textureScale, (object) textureOffset);
                  stringBuilder.AppendFormat("  1) Use the baked tiling.\n");
                  stringBuilder.AppendFormat("  2) Use the Multiple Materials feature to map the material on this object/submesh to its own submesh in the combined mesh. No other materials should map to this submesh. The original material can be applied to this submesh.\n");
                  stringBuilder.AppendFormat("  3) Combine only meshes that use the same (or subset of) the set of textures on this mesh. The original material can be applied to the result.\n");
                }
              }
            }
          }
        }
      }
      UnityEngine.Debug.Log(stringBuilder.Length != 0 ? (object) ("====== There are possible problems with these meshes that may prevent them from combining well. TREATMENT SUGGESTIONS (copy and paste to text editor if too big) =====\n" + stringBuilder.ToString()) : (object) "====== No problems detected. These meshes should combine well ====\n  If there are problems with the combined meshes please report the problem to digitalOpus.ca so we can improve Mesh Baker.");
    }

    private TextureBlender FindMatchingTextureBlender(string shaderName)
    {
      for (int index = 0; index < this.textureBlenders.Length; ++index)
      {
        if (this.textureBlenders[index].DoesShaderNameMatch(shaderName))
          return this.textureBlenders[index];
      }
      return (TextureBlender) null;
    }

    private void AdjustNonTextureProperties(
      Material mat,
      List<ShaderTextureProperty> texPropertyNames,
      List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures,
      bool considerTintColor,
      MB2_EditorMethodsInterface editorMethods)
    {
      if ((UnityEngine.Object) mat == (UnityEngine.Object) null || texPropertyNames == null)
        return;
      if (this._considerNonTextureProperties)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) ("Adjusting non texture properties using TextureBlender for shader: " + mat.shader.name));
        this.resultMaterialTextureBlender.SetNonTexturePropertyValuesOnResultMaterial(mat);
      }
      else
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          UnityEngine.Debug.Log((object) "Adjusting non texture properties on result material");
        for (int index = 0; index < texPropertyNames.Count; ++index)
        {
          string name = texPropertyNames[index].name;
          if (name.Equals("_MainTex"))
          {
            if (mat.HasProperty("_Color"))
            {
              try
              {
                if (considerTintColor)
                  mat.SetColor("_Color", Color.white);
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (name.Equals("_BumpMap"))
          {
            if (mat.HasProperty("_BumpScale"))
            {
              try
              {
                mat.SetFloat("_BumpScale", 1f);
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (name.Equals("_ParallaxMap"))
          {
            if (mat.HasProperty("_Parallax"))
            {
              try
              {
                mat.SetFloat("_Parallax", 0.02f);
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (name.Equals("_OcclusionMap"))
          {
            if (mat.HasProperty("_OcclusionStrength"))
            {
              try
              {
                mat.SetFloat("_OcclusionStrength", 1f);
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (name.Equals("_EmissionMap"))
          {
            if (mat.HasProperty("_EmissionColor"))
            {
              try
              {
                mat.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f, 0.0f));
              }
              catch (Exception ex)
              {
              }
            }
            if (mat.HasProperty("_EmissionScaleUI"))
            {
              try
              {
                mat.SetFloat("_EmissionScaleUI", 1f);
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        editorMethods?.CommitChangesToAssets();
      }
    }

    public static Color GetColorIfNoTexture(ShaderTextureProperty texProperty)
    {
      if (texProperty.isNormalMap)
        return new Color(0.5f, 0.5f, 1f);
      if (texProperty.name.Equals("_MetallicGlossMap"))
        return new Color(0.0f, 0.0f, 0.0f, 1f);
      if (texProperty.name.Equals("_ParallaxMap"))
        return new Color(0.0f, 0.0f, 0.0f, 0.0f);
      if (texProperty.name.Equals("_OcclusionMap"))
        return new Color(1f, 1f, 1f, 1f);
      if (texProperty.name.Equals("_EmissionMap"))
        return new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return texProperty.name.Equals("_DetailMask") ? new Color(0.0f, 0.0f, 0.0f, 0.0f) : new Color(1f, 1f, 1f, 0.0f);
    }

    private Color32 ConvertNormalFormatFromUnity_ToStandard(Color32 c)
    {
      Vector3 zero = Vector3.zero with
      {
        x = (float) ((double) c.a * 2.0 - 1.0),
        y = (float) ((double) c.g * 2.0 - 1.0)
      };
      zero.z = Mathf.Sqrt((float) (1.0 - (double) zero.x * (double) zero.x - (double) zero.y * (double) zero.y));
      return new Color32()
      {
        a = 1,
        r = (byte) (((double) zero.x + 1.0) * 0.5),
        g = (byte) (((double) zero.y + 1.0) * 0.5),
        b = (byte) (((double) zero.z + 1.0) * 0.5)
      };
    }

    private float GetSubmeshArea(UnityEngine.Mesh m, int submeshIdx)
    {
      if (submeshIdx >= m.subMeshCount || submeshIdx < 0)
        return 0.0f;
      Vector3[] vertices = m.vertices;
      int[] indices = m.GetIndices(submeshIdx);
      float submeshArea = 0.0f;
      for (int index = 0; index < indices.Length; index += 3)
      {
        Vector3 vector3_1 = vertices[indices[index]];
        Vector3 vector3_2 = vertices[indices[index + 1]];
        Vector3 vector3_3 = vertices[indices[index + 2]];
        Vector3 vector3_4 = Vector3.Cross(vector3_2 - vector3_1, vector3_3 - vector3_1);
        submeshArea += vector3_4.magnitude / 2f;
      }
      return submeshArea;
    }

    private string PrintList(List<GameObject> gos)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < gos.Count; ++index)
        stringBuilder.Append(((object) gos[index])?.ToString() + ",");
      return stringBuilder.ToString();
    }

    public class MeshBakerMaterialTexture
    {
      public Texture2D t;
      public float texelDensity;
      public DRect encapsulatingSamplingRect;
      public DRect matTilingRect;

      public MeshBakerMaterialTexture()
      {
      }

      public MeshBakerMaterialTexture(Texture2D tx) => this.t = tx;

      public MeshBakerMaterialTexture(Texture2D tx, Vector2 o, Vector2 s, float texelDens)
      {
        this.t = tx;
        this.matTilingRect = new DRect(o, s);
        this.texelDensity = texelDens;
      }
    }

    public class MatAndTransformToMerged
    {
      public Material mat;
      public DRect obUVRectIfTilingSame = new DRect(0.0f, 0.0f, 1f, 1f);
      public DRect samplingRectMatAndUVTiling;
      public DRect materialTiling;
      public string objName;

      public MatAndTransformToMerged(Material m) => this.mat = m;

      public override bool Equals(object obj)
      {
        if (obj is MB3_TextureCombiner.MatAndTransformToMerged)
        {
          MB3_TextureCombiner.MatAndTransformToMerged transformToMerged = (MB3_TextureCombiner.MatAndTransformToMerged) obj;
          if ((UnityEngine.Object) transformToMerged.mat == (UnityEngine.Object) this.mat && transformToMerged.obUVRectIfTilingSame == this.obUVRectIfTilingSame)
            return true;
        }
        return false;
      }

      public override int GetHashCode() => ((object) this.mat).GetHashCode() ^ this.obUVRectIfTilingSame.GetHashCode() ^ this.samplingRectMatAndUVTiling.GetHashCode();
    }

    public class SamplingRectEnclosesComparer : 
      IComparer<MB3_TextureCombiner.MatAndTransformToMerged>
    {
      public int Compare(
        MB3_TextureCombiner.MatAndTransformToMerged x,
        MB3_TextureCombiner.MatAndTransformToMerged y)
      {
        if (x.samplingRectMatAndUVTiling.Equals((object) y.samplingRectMatAndUVTiling))
          return 0;
        return x.samplingRectMatAndUVTiling.Encloses(y.samplingRectMatAndUVTiling) ? -1 : 1;
      }
    }

    public class MatsAndGOs
    {
      public List<MB3_TextureCombiner.MatAndTransformToMerged> mats;
      public List<GameObject> gos;
    }

    public class MB_TexSet
    {
      public MB3_TextureCombiner.MeshBakerMaterialTexture[] ts;
      public MB3_TextureCombiner.MatsAndGOs matsAndGOs;
      public bool allTexturesUseSameMatTiling;
      public Vector2 obUVoffset = new Vector2(0.0f, 0.0f);
      public Vector2 obUVscale = new Vector2(1f, 1f);
      public int idealWidth;
      public int idealHeight;

      public DRect obUVrect => new DRect(this.obUVoffset, this.obUVscale);

      public MB_TexSet(
        MB3_TextureCombiner.MeshBakerMaterialTexture[] tss,
        Vector2 uvOffset,
        Vector2 uvScale)
      {
        this.ts = tss;
        this.obUVoffset = uvOffset;
        this.obUVscale = uvScale;
        this.allTexturesUseSameMatTiling = false;
        this.matsAndGOs = new MB3_TextureCombiner.MatsAndGOs();
        this.matsAndGOs.mats = new List<MB3_TextureCombiner.MatAndTransformToMerged>();
        this.matsAndGOs.gos = new List<GameObject>();
      }

      public bool IsEqual(
        object obj,
        bool fixOutOfBoundsUVs,
        bool considerNonTextureProperties,
        TextureBlender resultMaterialTextureBlender)
      {
        if (!(obj is MB3_TextureCombiner.MB_TexSet))
          return false;
        MB3_TextureCombiner.MB_TexSet mbTexSet = (MB3_TextureCombiner.MB_TexSet) obj;
        if (mbTexSet.ts.Length != this.ts.Length)
          return false;
        for (int index = 0; index < this.ts.Length; ++index)
        {
          if (this.ts[index].matTilingRect != mbTexSet.ts[index].matTilingRect || (UnityEngine.Object) this.ts[index].t != (UnityEngine.Object) mbTexSet.ts[index].t || considerNonTextureProperties && resultMaterialTextureBlender != null && !resultMaterialTextureBlender.NonTexturePropertiesAreEqual(this.matsAndGOs.mats[0].mat, mbTexSet.matsAndGOs.mats[0].mat))
            return false;
        }
        return (!fixOutOfBoundsUVs || (double) this.obUVoffset.x == (double) mbTexSet.obUVoffset.x && (double) this.obUVoffset.y == (double) mbTexSet.obUVoffset.y) && (!fixOutOfBoundsUVs || (double) this.obUVscale.x == (double) mbTexSet.obUVscale.x && (double) this.obUVscale.y == (double) mbTexSet.obUVscale.y);
      }

      public void CalcInitialFullSamplingRects(bool fixOutOfBoundsUVs)
      {
        DRect drect = new DRect(0.0f, 0.0f, 1f, 1f);
        if (fixOutOfBoundsUVs)
          drect = this.obUVrect;
        for (int index = 0; index < this.ts.Length; ++index)
        {
          if ((UnityEngine.Object) this.ts[index].t != (UnityEngine.Object) null)
          {
            DRect matTilingRect = this.ts[index].matTilingRect;
            DRect r1 = !fixOutOfBoundsUVs ? new DRect(0.0, 0.0, 1.0, 1.0) : this.obUVrect;
            this.ts[index].encapsulatingSamplingRect = MB3_UVTransformUtility.CombineTransforms(ref r1, ref matTilingRect);
            drect = this.ts[index].encapsulatingSamplingRect;
          }
        }
        for (int index = 0; index < this.ts.Length; ++index)
        {
          if ((UnityEngine.Object) this.ts[index].t == (UnityEngine.Object) null)
            this.ts[index].encapsulatingSamplingRect = drect;
        }
      }

      public void CalcMatAndUVSamplingRects()
      {
        if (this.allTexturesUseSameMatTiling)
        {
          DRect r2 = new DRect(0.0f, 0.0f, 1f, 1f);
          for (int index = 0; index < this.ts.Length; ++index)
          {
            if ((UnityEngine.Object) this.ts[index].t != (UnityEngine.Object) null)
              r2 = this.ts[index].matTilingRect;
          }
          for (int index = 0; index < this.matsAndGOs.mats.Count; ++index)
          {
            this.matsAndGOs.mats[index].materialTiling = r2;
            this.matsAndGOs.mats[index].samplingRectMatAndUVTiling = MB3_UVTransformUtility.CombineTransforms(ref this.matsAndGOs.mats[index].obUVRectIfTilingSame, ref r2);
          }
        }
        else
        {
          for (int index = 0; index < this.matsAndGOs.mats.Count; ++index)
          {
            DRect r2 = new DRect(0.0f, 0.0f, 1f, 1f);
            this.matsAndGOs.mats[index].materialTiling = r2;
            this.matsAndGOs.mats[index].samplingRectMatAndUVTiling = MB3_UVTransformUtility.CombineTransforms(ref this.matsAndGOs.mats[index].obUVRectIfTilingSame, ref r2);
          }
        }
      }

      public bool AllTexturesAreSameForMerge(
        MB3_TextureCombiner.MB_TexSet other,
        bool considerNonTextureProperties,
        TextureBlender resultMaterialTextureBlender)
      {
        if (other.ts.Length != this.ts.Length || !other.allTexturesUseSameMatTiling || !this.allTexturesUseSameMatTiling)
          return false;
        int num = -1;
        for (int index = 0; index < this.ts.Length; ++index)
        {
          if ((UnityEngine.Object) this.ts[index].t != (UnityEngine.Object) other.ts[index].t)
            return false;
          if (num == -1 && (UnityEngine.Object) this.ts[index].t != (UnityEngine.Object) null)
            num = index;
          if (considerNonTextureProperties && resultMaterialTextureBlender != null && !resultMaterialTextureBlender.NonTexturePropertiesAreEqual(this.matsAndGOs.mats[0].mat, other.matsAndGOs.mats[0].mat))
            return false;
        }
        if (num != -1)
        {
          for (int index = 0; index < this.ts.Length; ++index)
          {
            if ((UnityEngine.Object) this.ts[index].t != (UnityEngine.Object) other.ts[index].t)
              return false;
          }
        }
        return true;
      }

      internal string GetDescription()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("[GAME_OBJS=");
        for (int index = 0; index < this.matsAndGOs.gos.Count; ++index)
          stringBuilder.AppendFormat("{0},", (object) this.matsAndGOs.gos[index].name);
        stringBuilder.AppendFormat("MATS=");
        for (int index = 0; index < this.matsAndGOs.mats.Count; ++index)
          stringBuilder.AppendFormat("{0},", (object) this.matsAndGOs.mats[index].mat.name);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
      }

      internal string GetMatSubrectDescriptions()
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < this.matsAndGOs.mats.Count; ++index)
          stringBuilder.AppendFormat("\n    {0}={1},", (object) this.matsAndGOs.mats[index].mat.name, (object) this.matsAndGOs.mats[index].samplingRectMatAndUVTiling);
        return stringBuilder.ToString();
      }
    }

    public class CombineTexturesIntoAtlasesCoroutineResult
    {
      public bool success = true;
      public bool isFinished;
    }
  }
}
