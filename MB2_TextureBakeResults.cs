// Decompiled with JetBrains decompiler
// Type: MB2_TextureBakeResults
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MB2_TextureBakeResults : ScriptableObject
{
  private const int VERSION = 3230;
  public int version;
  public MB_MaterialAndUVRect[] materialsAndUVRects;
  public MB_MultiMaterial[] resultMaterials;
  public bool doMultiMaterial;
  public Material[] materials;
  public bool fixOutOfBoundsUVs;
  public Material resultMaterial;

  private void OnEnable()
  {
    if (this.version < 3230 && this.resultMaterials != null)
    {
      for (int index = 0; index < this.resultMaterials.Length; ++index)
        this.resultMaterials[index].considerMeshUVs = this.fixOutOfBoundsUVs;
    }
    this.version = 3230;
  }

  public static MB2_TextureBakeResults CreateForMaterialsOnRenderer(
    GameObject[] gos,
    List<Material> matsOnTargetRenderer)
  {
    HashSet<Material> materialSet = new HashSet<Material>((IEnumerable<Material>) matsOnTargetRenderer);
    for (int index1 = 0; index1 < gos.Length; ++index1)
    {
      if ((Object) gos[index1] == (Object) null)
      {
        Debug.LogError((object) string.Format("Game object {0} in list of objects to add was null", (object) index1));
        return (MB2_TextureBakeResults) null;
      }
      Material[] goMaterials = MB_Utility.GetGOMaterials(gos[index1]);
      if (goMaterials.Length == 0)
      {
        Debug.LogError((object) string.Format("Game object {0} in list of objects to add no renderer", (object) index1));
        return (MB2_TextureBakeResults) null;
      }
      for (int index2 = 0; index2 < goMaterials.Length; ++index2)
      {
        if (!materialSet.Contains(goMaterials[index2]))
          materialSet.Add(goMaterials[index2]);
      }
    }
    Material[] array = new Material[materialSet.Count];
    materialSet.CopyTo(array);
    MB2_TextureBakeResults instance = (MB2_TextureBakeResults) ScriptableObject.CreateInstance(typeof (MB2_TextureBakeResults));
    List<MB_MaterialAndUVRect> materialAndUvRectList = new List<MB_MaterialAndUVRect>();
    for (int index = 0; index < array.Length; ++index)
    {
      if ((Object) array[index] != (Object) null)
      {
        MB_MaterialAndUVRect materialAndUvRect = new MB_MaterialAndUVRect(array[index], new Rect(0.0f, 0.0f, 1f, 1f), new Rect(0.0f, 0.0f, 1f, 1f), new Rect(0.0f, 0.0f, 1f, 1f), new Rect(0.0f, 0.0f, 1f, 1f), "");
        if (!materialAndUvRectList.Contains(materialAndUvRect))
          materialAndUvRectList.Add(materialAndUvRect);
      }
    }
    Material[] materialArray;
    instance.materials = materialArray = new Material[materialAndUvRectList.Count];
    instance.resultMaterials = new MB_MultiMaterial[materialAndUvRectList.Count];
    for (int index = 0; index < materialAndUvRectList.Count; ++index)
    {
      materialArray[index] = materialAndUvRectList[index].material;
      instance.resultMaterials[index] = new MB_MultiMaterial();
      instance.resultMaterials[index].sourceMaterials = new List<Material>()
      {
        materialAndUvRectList[index].material
      };
      instance.resultMaterials[index].combinedMaterial = materialArray[index];
      instance.resultMaterials[index].considerMeshUVs = false;
    }
    instance.doMultiMaterial = array.Length != 1;
    instance.materialsAndUVRects = materialAndUvRectList.ToArray();
    return instance;
  }

  public bool DoAnyResultMatsUseConsiderMeshUVs()
  {
    if (this.resultMaterials == null)
      return false;
    for (int index = 0; index < this.resultMaterials.Length; ++index)
    {
      if (this.resultMaterials[index].considerMeshUVs)
        return true;
    }
    return false;
  }

  public bool ContainsMaterial(Material m)
  {
    for (int index = 0; index < this.materialsAndUVRects.Length; ++index)
    {
      if ((Object) this.materialsAndUVRects[index].material == (Object) m)
        return true;
    }
    return false;
  }

  public string GetDescription()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Shaders:\n");
    HashSet<Shader> shaderSet = new HashSet<Shader>();
    if (this.materialsAndUVRects != null)
    {
      for (int index = 0; index < this.materialsAndUVRects.Length; ++index)
      {
        if ((Object) this.materialsAndUVRects[index].material != (Object) null)
          shaderSet.Add(this.materialsAndUVRects[index].material.shader);
      }
    }
    foreach (Shader shader in shaderSet)
      stringBuilder.Append("  ").Append(shader.name).AppendLine();
    stringBuilder.Append("Materials:\n");
    if (this.materialsAndUVRects != null)
    {
      for (int index = 0; index < this.materialsAndUVRects.Length; ++index)
      {
        if ((Object) this.materialsAndUVRects[index].material != (Object) null)
          stringBuilder.Append("  ").Append(this.materialsAndUVRects[index].material.name).AppendLine();
      }
    }
    return stringBuilder.ToString();
  }

  public static bool IsMeshAndMaterialRectEnclosedByAtlasRect(
    Rect uvR,
    Rect sourceMaterialTiling,
    Rect samplingEncapsulatinRect,
    MB2_LogLevel logLevel)
  {
    Rect rect1 = new Rect();
    Rect r2 = sourceMaterialTiling;
    Rect rect2 = samplingEncapsulatinRect;
    MB3_UVTransformUtility.Canonicalize(ref rect2, 0.0f, 0.0f);
    Rect rect3 = MB3_UVTransformUtility.CombineTransforms(ref uvR, ref r2);
    if (logLevel >= MB2_LogLevel.trace)
      Debug.Log((object) ("uvR=" + uvR.ToString("f5") + " matR=" + r2.ToString("f5") + "Potential Rect " + rect3.ToString("f5") + " encapsulating=" + rect2.ToString("f5")));
    MB3_UVTransformUtility.Canonicalize(ref rect3, rect2.x, rect2.y);
    if (logLevel >= MB2_LogLevel.trace)
      Debug.Log((object) ("Potential Rect Cannonical " + rect3.ToString("f5") + " encapsulating=" + rect2.ToString("f5")));
    return MB3_UVTransformUtility.RectContains(ref rect2, ref rect3);
  }

  public class Material2AtlasRectangleMapper
  {
    private MB2_TextureBakeResults tbr;
    private int[] numTimesMatAppearsInAtlas;
    private MB_MaterialAndUVRect[] matsAndSrcUVRect;

    public Material2AtlasRectangleMapper(MB2_TextureBakeResults res)
    {
      this.tbr = res;
      this.matsAndSrcUVRect = res.materialsAndUVRects;
      this.numTimesMatAppearsInAtlas = new int[this.matsAndSrcUVRect.Length];
      for (int index1 = 0; index1 < this.matsAndSrcUVRect.Length; ++index1)
      {
        if (this.numTimesMatAppearsInAtlas[index1] <= 1)
        {
          int num = 1;
          for (int index2 = index1 + 1; index2 < this.matsAndSrcUVRect.Length; ++index2)
          {
            if ((Object) this.matsAndSrcUVRect[index1].material == (Object) this.matsAndSrcUVRect[index2].material)
              ++num;
          }
          this.numTimesMatAppearsInAtlas[index1] = num;
          if (num > 1)
          {
            for (int index3 = index1 + 1; index3 < this.matsAndSrcUVRect.Length; ++index3)
            {
              if ((Object) this.matsAndSrcUVRect[index1].material == (Object) this.matsAndSrcUVRect[index3].material)
                this.numTimesMatAppearsInAtlas[index3] = num;
            }
          }
        }
      }
    }

    public bool TryMapMaterialToUVRect(
      Material mat,
      UnityEngine.Mesh m,
      int submeshIdx,
      int idxInResultMats,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache,
      Dictionary<int, MB_Utility.MeshAnalysisResult[]> meshAnalysisCache,
      out Rect rectInAtlas,
      out Rect encapsulatingRect,
      out Rect sourceMaterialTilingOut,
      ref string errorMsg,
      MB2_LogLevel logLevel)
    {
      if (this.tbr.materialsAndUVRects.Length == 0 && this.tbr.materials.Length != 0)
      {
        errorMsg = "The 'Texture Bake Result' needs to be re-baked to be compatible with this version of Mesh Baker. Please re-bake using the MB3_TextureBaker.";
        rectInAtlas = new Rect();
        encapsulatingRect = new Rect();
        sourceMaterialTilingOut = new Rect();
        return false;
      }
      if ((Object) mat == (Object) null)
      {
        rectInAtlas = new Rect();
        encapsulatingRect = new Rect();
        sourceMaterialTilingOut = new Rect();
        errorMsg = string.Format("Mesh {0} Had no material on submesh {1} cannot map to a material in the atlas", (object) m.name, (object) submeshIdx);
        return false;
      }
      if (submeshIdx >= m.subMeshCount)
      {
        errorMsg = "Submesh index is greater than the number of submeshes";
        rectInAtlas = new Rect();
        encapsulatingRect = new Rect();
        sourceMaterialTilingOut = new Rect();
        return false;
      }
      int index1 = -1;
      for (int index2 = 0; index2 < this.matsAndSrcUVRect.Length; ++index2)
      {
        if ((Object) mat == (Object) this.matsAndSrcUVRect[index2].material)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
      {
        rectInAtlas = new Rect();
        encapsulatingRect = new Rect();
        sourceMaterialTilingOut = new Rect();
        errorMsg = string.Format("Material {0} could not be found in the Texture Bake Result", (object) mat.name);
        return false;
      }
      if (!this.tbr.resultMaterials[idxInResultMats].considerMeshUVs)
      {
        if (this.numTimesMatAppearsInAtlas[index1] != 1)
          Debug.LogError((object) "There is a problem with this TextureBakeResults. FixOutOfBoundsUVs is false and a material appears more than once.");
        rectInAtlas = this.matsAndSrcUVRect[index1].atlasRect;
        encapsulatingRect = this.matsAndSrcUVRect[index1].samplingEncapsulatinRect;
        sourceMaterialTilingOut = this.matsAndSrcUVRect[index1].sourceMaterialTiling;
        return true;
      }
      MB_Utility.MeshAnalysisResult[] meshAnalysisResultArray;
      if (!meshAnalysisCache.TryGetValue(m.GetInstanceID(), out meshAnalysisResultArray))
      {
        meshAnalysisResultArray = new MB_Utility.MeshAnalysisResult[m.subMeshCount];
        for (int submeshIndex = 0; submeshIndex < m.subMeshCount; ++submeshIndex)
          MB_Utility.hasOutOfBoundsUVs(meshChannelCache.GetUv0Raw(m), m, ref meshAnalysisResultArray[submeshIndex], submeshIndex);
        meshAnalysisCache.Add(m.GetInstanceID(), meshAnalysisResultArray);
      }
      bool flag = false;
      if (logLevel >= MB2_LogLevel.trace)
        Debug.Log((object) string.Format("Trying to find a rectangle in atlas capable of holding tiled sampling rect for mesh {0} using material {1}", (object) m, (object) mat));
      for (int index3 = index1; index3 < this.matsAndSrcUVRect.Length; ++index3)
      {
        if ((Object) this.matsAndSrcUVRect[index3].material == (Object) mat && MB2_TextureBakeResults.IsMeshAndMaterialRectEnclosedByAtlasRect(meshAnalysisResultArray[submeshIdx].uvRect, this.matsAndSrcUVRect[index3].sourceMaterialTiling, this.matsAndSrcUVRect[index3].samplingEncapsulatinRect, logLevel))
        {
          if (logLevel >= MB2_LogLevel.trace)
            Debug.Log((object) ("Found rect in atlas capable of containing tiled sampling rect for mesh " + ((object) m)?.ToString() + " at idx=" + index3.ToString()));
          index1 = index3;
          flag = true;
          break;
        }
      }
      if (flag)
      {
        rectInAtlas = this.matsAndSrcUVRect[index1].atlasRect;
        encapsulatingRect = this.matsAndSrcUVRect[index1].samplingEncapsulatinRect;
        sourceMaterialTilingOut = this.matsAndSrcUVRect[index1].sourceMaterialTiling;
        return true;
      }
      rectInAtlas = new Rect();
      encapsulatingRect = new Rect();
      sourceMaterialTilingOut = new Rect();
      errorMsg = string.Format("Could not find a tiled rectangle in the atlas capable of containing the uv and material tiling on mesh {0} for material {1}", (object) m.name, (object) mat);
      return false;
    }
  }
}
