// Decompiled with JetBrains decompiler
// Type: MB3_TextureBaker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MB3_TextureBaker : MB3_MeshBakerRoot
{
  public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;
  [SerializeField]
  protected MB2_TextureBakeResults _textureBakeResults;
  [SerializeField]
  protected int _atlasPadding = 1;
  [SerializeField]
  protected int _maxAtlasSize = 4096;
  [SerializeField]
  protected bool _resizePowerOfTwoTextures;
  [SerializeField]
  protected bool _fixOutOfBoundsUVs;
  [SerializeField]
  protected int _maxTilingBakeSize = 1024;
  [SerializeField]
  protected MB2_PackingAlgorithmEnum _packingAlgorithm = MB2_PackingAlgorithmEnum.MeshBakerTexturePacker;
  [SerializeField]
  protected bool _meshBakerTexturePackerForcePowerOfTwo = true;
  [SerializeField]
  protected List<ShaderTextureProperty> _customShaderProperties = new List<ShaderTextureProperty>();
  [SerializeField]
  protected List<string> _customShaderPropNames_Depricated = new List<string>();
  [SerializeField]
  protected bool _doMultiMaterial;
  [SerializeField]
  protected bool _doMultiMaterialSplitAtlasesIfTooBig = true;
  [SerializeField]
  protected bool _doMultiMaterialSplitAtlasesIfOBUVs = true;
  [SerializeField]
  protected Material _resultMaterial;
  [SerializeField]
  protected bool _considerNonTextureProperties;
  [SerializeField]
  protected bool _doSuggestTreatment = true;
  private MB3_TextureBaker.CreateAtlasesCoroutineResult _coroutineResult;
  public MB_MultiMaterial[] resultMaterials = new MB_MultiMaterial[0];
  public List<GameObject> objsToMesh;
  public MB3_TextureBaker.OnCombinedTexturesCoroutineSuccess onBuiltAtlasesSuccess;
  public MB3_TextureBaker.OnCombinedTexturesCoroutineFail onBuiltAtlasesFail;
  public MB_AtlasesAndRects[] OnCombinedTexturesCoroutineAtlasesAndRects;

  public override MB2_TextureBakeResults textureBakeResults
  {
    get => this._textureBakeResults;
    set => this._textureBakeResults = value;
  }

  public virtual int atlasPadding
  {
    get => this._atlasPadding;
    set => this._atlasPadding = value;
  }

  public virtual int maxAtlasSize
  {
    get => this._maxAtlasSize;
    set => this._maxAtlasSize = value;
  }

  public virtual bool resizePowerOfTwoTextures
  {
    get => this._resizePowerOfTwoTextures;
    set => this._resizePowerOfTwoTextures = value;
  }

  public virtual bool fixOutOfBoundsUVs
  {
    get => this._fixOutOfBoundsUVs;
    set => this._fixOutOfBoundsUVs = value;
  }

  public virtual int maxTilingBakeSize
  {
    get => this._maxTilingBakeSize;
    set => this._maxTilingBakeSize = value;
  }

  public virtual MB2_PackingAlgorithmEnum packingAlgorithm
  {
    get => this._packingAlgorithm;
    set => this._packingAlgorithm = value;
  }

  public bool meshBakerTexturePackerForcePowerOfTwo
  {
    get => this._meshBakerTexturePackerForcePowerOfTwo;
    set => this._meshBakerTexturePackerForcePowerOfTwo = value;
  }

  public virtual List<ShaderTextureProperty> customShaderProperties
  {
    get => this._customShaderProperties;
    set => this._customShaderProperties = value;
  }

  public virtual List<string> customShaderPropNames
  {
    get => this._customShaderPropNames_Depricated;
    set => this._customShaderPropNames_Depricated = value;
  }

  public virtual bool doMultiMaterial
  {
    get => this._doMultiMaterial;
    set => this._doMultiMaterial = value;
  }

  public virtual bool doMultiMaterialSplitAtlasesIfTooBig
  {
    get => this._doMultiMaterialSplitAtlasesIfTooBig;
    set => this._doMultiMaterialSplitAtlasesIfTooBig = value;
  }

  public virtual bool doMultiMaterialSplitAtlasesIfOBUVs
  {
    get => this._doMultiMaterialSplitAtlasesIfOBUVs;
    set => this._doMultiMaterialSplitAtlasesIfOBUVs = value;
  }

  public virtual Material resultMaterial
  {
    get => this._resultMaterial;
    set => this._resultMaterial = value;
  }

  public bool considerNonTextureProperties
  {
    get => this._considerNonTextureProperties;
    set => this._considerNonTextureProperties = value;
  }

  public bool doSuggestTreatment
  {
    get => this._doSuggestTreatment;
    set => this._doSuggestTreatment = value;
  }

  public MB3_TextureBaker.CreateAtlasesCoroutineResult CoroutineResult => this._coroutineResult;

  public override List<GameObject> GetObjectsToCombine()
  {
    if (this.objsToMesh == null)
      this.objsToMesh = new List<GameObject>();
    return this.objsToMesh;
  }

  public MB_AtlasesAndRects[] CreateAtlases() => this.CreateAtlases((ProgressUpdateDelegate) null);

  public IEnumerator CreateAtlasesCoroutine(
    ProgressUpdateDelegate progressInfo,
    MB3_TextureBaker.CreateAtlasesCoroutineResult coroutineResult,
    bool saveAtlasesAsAssets = false,
    MB2_EditorMethodsInterface editorMethods = null,
    float maxTimePerFrame = 0.01f)
  {
    MB3_TextureBaker mom = this;
    MBVersionConcrete mbVersionConcrete = new MBVersionConcrete();
    if (!MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning && (mbVersionConcrete.GetMajorVersion() < 5 || mbVersionConcrete.GetMajorVersion() == 5 && mbVersionConcrete.GetMinorVersion() < 3))
    {
      Debug.LogError((object) "Running the texture combiner as a coroutine only works in Unity 5.3 and higher");
      coroutineResult.success = false;
    }
    else
    {
      mom.OnCombinedTexturesCoroutineAtlasesAndRects = (MB_AtlasesAndRects[]) null;
      if ((double) maxTimePerFrame <= 0.0)
      {
        Debug.LogError((object) "maxTimePerFrame must be a value greater than zero");
        coroutineResult.isFinished = true;
      }
      else
      {
        MB2_ValidationLevel validationLevel = Application.isPlaying ? MB2_ValidationLevel.quick : MB2_ValidationLevel.robust;
        if (!MB3_MeshBakerRoot.DoCombinedValidate((MB3_MeshBakerRoot) mom, MB_ObjsToCombineTypes.dontCare, (MB2_EditorMethodsInterface) null, validationLevel))
          coroutineResult.isFinished = true;
        else if (mom._doMultiMaterial && !mom._ValidateResultMaterials())
        {
          coroutineResult.isFinished = true;
        }
        else
        {
          if (!mom._doMultiMaterial)
          {
            if ((UnityEngine.Object) mom._resultMaterial == (UnityEngine.Object) null)
            {
              Debug.LogError((object) "Combined Material is null please create and assign a result material.");
              coroutineResult.isFinished = true;
              yield break;
            }
            else
            {
              Shader shader = mom._resultMaterial.shader;
              for (int index = 0; index < mom.objsToMesh.Count; ++index)
              {
                foreach (Material goMaterial in MB_Utility.GetGOMaterials(mom.objsToMesh[index]))
                {
                  if ((UnityEngine.Object) goMaterial != (UnityEngine.Object) null && (UnityEngine.Object) goMaterial.shader != (UnityEngine.Object) shader)
                    Debug.LogWarning((object) ("Game object " + ((object) mom.objsToMesh[index])?.ToString() + " does not use shader " + ((object) shader)?.ToString() + " it may not have the required textures. If not small solid color textures will be generated."));
                }
              }
            }
          }
          MB3_TextureCombiner combiner = mom.CreateAndConfigureTextureCombiner();
          combiner.saveAtlasesAsAssets = saveAtlasesAsAssets;
          int length = 1;
          if (mom._doMultiMaterial)
            length = mom.resultMaterials.Length;
          mom.OnCombinedTexturesCoroutineAtlasesAndRects = new MB_AtlasesAndRects[length];
          for (int index = 0; index < mom.OnCombinedTexturesCoroutineAtlasesAndRects.Length; ++index)
            mom.OnCombinedTexturesCoroutineAtlasesAndRects[index] = new MB_AtlasesAndRects();
          for (int i = 0; i < mom.OnCombinedTexturesCoroutineAtlasesAndRects.Length; ++i)
          {
            List<Material> allowedMaterialsFilter = (List<Material>) null;
            Material resultMaterial;
            if (mom._doMultiMaterial)
            {
              allowedMaterialsFilter = mom.resultMaterials[i].sourceMaterials;
              resultMaterial = mom.resultMaterials[i].combinedMaterial;
              combiner.fixOutOfBoundsUVs = mom.resultMaterials[i].considerMeshUVs;
            }
            else
              resultMaterial = mom._resultMaterial;
            Debug.Log((object) string.Format("Creating atlases for result material {0} using shader {1}", (object) resultMaterial, (object) resultMaterial.shader));
            MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult coroutineResult2 = new MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult();
            yield return (object) combiner.CombineTexturesIntoAtlasesCoroutine(progressInfo, mom.OnCombinedTexturesCoroutineAtlasesAndRects[i], resultMaterial, mom.objsToMesh, allowedMaterialsFilter, editorMethods, coroutineResult2, maxTimePerFrame);
            coroutineResult.success = coroutineResult2.success;
            if (!coroutineResult.success)
            {
              coroutineResult.isFinished = true;
              yield break;
            }
            else
              coroutineResult2 = (MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult) null;
          }
          mom.unpackMat2RectMap(mom.textureBakeResults);
          mom.textureBakeResults.doMultiMaterial = mom._doMultiMaterial;
          if (mom._doMultiMaterial)
          {
            mom.textureBakeResults.resultMaterials = mom.resultMaterials;
          }
          else
          {
            MB_MultiMaterial[] mbMultiMaterialArray = new MB_MultiMaterial[1]
            {
              new MB_MultiMaterial()
            };
            mbMultiMaterialArray[0].combinedMaterial = mom._resultMaterial;
            mbMultiMaterialArray[0].considerMeshUVs = mom._fixOutOfBoundsUVs;
            mbMultiMaterialArray[0].sourceMaterials = new List<Material>();
            mbMultiMaterialArray[0].sourceMaterials.AddRange((IEnumerable<Material>) mom.textureBakeResults.materials);
            mom.textureBakeResults.resultMaterials = mbMultiMaterialArray;
          }
          foreach (MB3_MeshBakerRoot componentsInChild in mom.GetComponentsInChildren<MB3_MeshBakerCommon>())
            componentsInChild.textureBakeResults = mom.textureBakeResults;
          if (mom.LOG_LEVEL >= MB2_LogLevel.info)
            Debug.Log((object) "Created Atlases");
          coroutineResult.isFinished = true;
          if (coroutineResult.success && mom.onBuiltAtlasesSuccess != null)
            mom.onBuiltAtlasesSuccess();
          if (!coroutineResult.success && mom.onBuiltAtlasesFail != null)
            mom.onBuiltAtlasesFail();
        }
      }
    }
  }

  public MB_AtlasesAndRects[] CreateAtlases(
    ProgressUpdateDelegate progressInfo,
    bool saveAtlasesAsAssets = false,
    MB2_EditorMethodsInterface editorMethods = null)
  {
    MB_AtlasesAndRects[] atlases = (MB_AtlasesAndRects[]) null;
    try
    {
      this._coroutineResult = new MB3_TextureBaker.CreateAtlasesCoroutineResult();
      MB3_TextureCombiner.RunCorutineWithoutPause(this.CreateAtlasesCoroutine(progressInfo, this._coroutineResult, saveAtlasesAsAssets, editorMethods, 1000f), 0);
      if (this._coroutineResult.success)
      {
        if ((UnityEngine.Object) this.textureBakeResults != (UnityEngine.Object) null)
          atlases = this.OnCombinedTexturesCoroutineAtlasesAndRects;
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    finally
    {
      if (saveAtlasesAsAssets && atlases != null)
      {
        for (int index1 = 0; index1 < atlases.Length; ++index1)
        {
          MB_AtlasesAndRects mbAtlasesAndRects = atlases[index1];
          if (mbAtlasesAndRects != null && mbAtlasesAndRects.atlases != null)
          {
            for (int index2 = 0; index2 < mbAtlasesAndRects.atlases.Length; ++index2)
            {
              if ((UnityEngine.Object) mbAtlasesAndRects.atlases[index2] != (UnityEngine.Object) null)
              {
                if (editorMethods != null)
                  editorMethods.Destroy((UnityEngine.Object) mbAtlasesAndRects.atlases[index2]);
                else
                  MB_Utility.Destroy((UnityEngine.Object) mbAtlasesAndRects.atlases[index2]);
              }
            }
          }
        }
      }
    }
    return atlases;
  }

  private void unpackMat2RectMap(MB2_TextureBakeResults tbr)
  {
    List<Material> materialList = new List<Material>();
    List<MB_MaterialAndUVRect> materialAndUvRectList = new List<MB_MaterialAndUVRect>();
    List<Rect> rectList = new List<Rect>();
    for (int index1 = 0; index1 < this.OnCombinedTexturesCoroutineAtlasesAndRects.Length; ++index1)
    {
      List<MB_MaterialAndUVRect> mat2rectMap = this.OnCombinedTexturesCoroutineAtlasesAndRects[index1].mat2rect_map;
      if (mat2rectMap != null)
      {
        for (int index2 = 0; index2 < mat2rectMap.Count; ++index2)
        {
          materialAndUvRectList.Add(mat2rectMap[index2]);
          materialList.Add(mat2rectMap[index2].material);
          rectList.Add(mat2rectMap[index2].atlasRect);
        }
      }
    }
    tbr.materials = materialList.ToArray();
    tbr.materialsAndUVRects = materialAndUvRectList.ToArray();
  }

  public MB3_TextureCombiner CreateAndConfigureTextureCombiner() => new MB3_TextureCombiner()
  {
    LOG_LEVEL = this.LOG_LEVEL,
    atlasPadding = this._atlasPadding,
    maxAtlasSize = this._maxAtlasSize,
    customShaderPropNames = this._customShaderProperties,
    fixOutOfBoundsUVs = this._fixOutOfBoundsUVs,
    maxTilingBakeSize = this._maxTilingBakeSize,
    packingAlgorithm = this._packingAlgorithm,
    meshBakerTexturePackerForcePowerOfTwo = this._meshBakerTexturePackerForcePowerOfTwo,
    resizePowerOfTwoTextures = this._resizePowerOfTwoTextures,
    considerNonTextureProperties = this._considerNonTextureProperties
  };

  public static void ConfigureNewMaterialToMatchOld(Material newMat, Material original)
  {
    if ((UnityEngine.Object) original == (UnityEngine.Object) null)
    {
      Debug.LogWarning((object) ("Original material is null, could not copy properties to " + ((object) newMat)?.ToString() + ". Setting shader to " + ((object) newMat.shader)?.ToString()));
    }
    else
    {
      newMat.shader = original.shader;
      newMat.CopyPropertiesFromMaterial(original);
      ShaderTextureProperty[] texPropertyNames = MB3_TextureCombiner.shaderTexPropertyNames;
      for (int index = 0; index < texPropertyNames.Length; ++index)
      {
        Vector2 one = Vector2.one;
        Vector2 zero = Vector2.zero;
        if (newMat.HasProperty(texPropertyNames[index].name))
        {
          newMat.SetTextureOffset(texPropertyNames[index].name, zero);
          newMat.SetTextureScale(texPropertyNames[index].name, one);
        }
      }
    }
  }

  private string PrintSet(HashSet<Material> s)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Material material in s)
      stringBuilder.Append(((object) material)?.ToString() + ",");
    return stringBuilder.ToString();
  }

  private bool _ValidateResultMaterials()
  {
    HashSet<Material> materialSet1 = new HashSet<Material>();
    for (int index1 = 0; index1 < this.objsToMesh.Count; ++index1)
    {
      if ((UnityEngine.Object) this.objsToMesh[index1] != (UnityEngine.Object) null)
      {
        Material[] goMaterials = MB_Utility.GetGOMaterials(this.objsToMesh[index1]);
        for (int index2 = 0; index2 < goMaterials.Length; ++index2)
        {
          if ((UnityEngine.Object) goMaterials[index2] != (UnityEngine.Object) null)
            materialSet1.Add(goMaterials[index2]);
        }
      }
    }
    HashSet<Material> materialSet2 = new HashSet<Material>();
    for (int index3 = 0; index3 < this.resultMaterials.Length; ++index3)
    {
      MB_MultiMaterial resultMaterial = this.resultMaterials[index3];
      if ((UnityEngine.Object) resultMaterial.combinedMaterial == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "Combined Material is null please create and assign a result material.");
        return false;
      }
      Shader shader = resultMaterial.combinedMaterial.shader;
      for (int index4 = 0; index4 < resultMaterial.sourceMaterials.Count; ++index4)
      {
        if ((UnityEngine.Object) resultMaterial.sourceMaterials[index4] == (UnityEngine.Object) null)
        {
          Debug.LogError((object) "There are null entries in the list of Source Materials");
          return false;
        }
        if ((UnityEngine.Object) shader != (UnityEngine.Object) resultMaterial.sourceMaterials[index4].shader)
          Debug.LogWarning((object) ("Source material " + ((object) resultMaterial.sourceMaterials[index4])?.ToString() + " does not use shader " + ((object) shader)?.ToString() + " it may not have the required textures. If not empty textures will be generated."));
        if (materialSet2.Contains(resultMaterial.sourceMaterials[index4]))
        {
          Debug.LogError((object) ("A Material " + ((object) resultMaterial.sourceMaterials[index4])?.ToString() + " appears more than once in the list of source materials in the source material to combined mapping. Each source material must be unique."));
          return false;
        }
        materialSet2.Add(resultMaterial.sourceMaterials[index4]);
      }
    }
    if (materialSet1.IsProperSubsetOf((IEnumerable<Material>) materialSet2))
    {
      materialSet2.ExceptWith((IEnumerable<Material>) materialSet1);
      Debug.LogWarning((object) ("There are materials in the mapping that are not used on your source objects: " + this.PrintSet(materialSet2)));
    }
    if (this.resultMaterials == null || this.resultMaterials.Length == 0 || !materialSet2.IsProperSubsetOf((IEnumerable<Material>) materialSet1))
      return true;
    materialSet1.ExceptWith((IEnumerable<Material>) materialSet2);
    Debug.LogError((object) ("There are materials on the objects to combine that are not in the mapping: " + this.PrintSet(materialSet1)));
    return false;
  }

  public delegate void OnCombinedTexturesCoroutineSuccess();

  public delegate void OnCombinedTexturesCoroutineFail();

  public class CreateAtlasesCoroutineResult
  {
    public bool success = true;
    public bool isFinished;
  }
}
