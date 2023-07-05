// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MultiMeshCombiner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MultiMeshCombiner : MB3_MeshCombiner
  {
    private static GameObject[] empty = new GameObject[0];
    private static int[] emptyIDs = new int[0];
    public Dictionary<int, MB3_MultiMeshCombiner.CombinedMesh> obj2MeshCombinerMap = new Dictionary<int, MB3_MultiMeshCombiner.CombinedMesh>();
    [SerializeField]
    public List<MB3_MultiMeshCombiner.CombinedMesh> meshCombiners = new List<MB3_MultiMeshCombiner.CombinedMesh>();
    [SerializeField]
    private int _maxVertsInMesh = (int) ushort.MaxValue;

    public override MB2_LogLevel LOG_LEVEL
    {
      get => this._LOG_LEVEL;
      set
      {
        this._LOG_LEVEL = value;
        for (int index = 0; index < this.meshCombiners.Count; ++index)
          this.meshCombiners[index].combinedMesh.LOG_LEVEL = value;
      }
    }

    public override MB2_ValidationLevel validationLevel
    {
      set
      {
        this._validationLevel = value;
        for (int index = 0; index < this.meshCombiners.Count; ++index)
          this.meshCombiners[index].combinedMesh.validationLevel = this._validationLevel;
      }
      get => this._validationLevel;
    }

    public int maxVertsInMesh
    {
      get => this._maxVertsInMesh;
      set
      {
        if (this.obj2MeshCombinerMap.Count > 0)
          return;
        if (value < 3)
          Debug.LogError((object) "Max verts in mesh must be greater than three.");
        else if (value > (int) ushort.MaxValue)
          Debug.LogError((object) "Meshes in unity cannot have more than 65535 vertices.");
        else
          this._maxVertsInMesh = value;
      }
    }

    public override int GetNumObjectsInCombined() => this.obj2MeshCombinerMap.Count;

    public override int GetNumVerticesFor(GameObject go)
    {
      MB3_MultiMeshCombiner.CombinedMesh combinedMesh = (MB3_MultiMeshCombiner.CombinedMesh) null;
      return this.obj2MeshCombinerMap.TryGetValue(go.GetInstanceID(), out combinedMesh) ? combinedMesh.combinedMesh.GetNumVerticesFor(go) : -1;
    }

    public override int GetNumVerticesFor(int gameObjectID)
    {
      MB3_MultiMeshCombiner.CombinedMesh combinedMesh = (MB3_MultiMeshCombiner.CombinedMesh) null;
      return this.obj2MeshCombinerMap.TryGetValue(gameObjectID, out combinedMesh) ? combinedMesh.combinedMesh.GetNumVerticesFor(gameObjectID) : -1;
    }

    public override List<GameObject> GetObjectsInCombined()
    {
      List<GameObject> objectsInCombined = new List<GameObject>();
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        objectsInCombined.AddRange((IEnumerable<GameObject>) this.meshCombiners[index].combinedMesh.GetObjectsInCombined());
      return objectsInCombined;
    }

    public override int GetLightmapIndex() => this.meshCombiners.Count > 0 ? this.meshCombiners[0].combinedMesh.GetLightmapIndex() : -1;

    public override bool CombinedMeshContains(GameObject go) => this.obj2MeshCombinerMap.ContainsKey(go.GetInstanceID());

    private bool _validateTextureBakeResults()
    {
      if ((UnityEngine.Object) this._textureBakeResults == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "Texture Bake Results is null. Can't combine meshes.");
        return false;
      }
      if (this._textureBakeResults.materialsAndUVRects == null || this._textureBakeResults.materialsAndUVRects.Length == 0)
      {
        Debug.LogError((object) "Texture Bake Results has no materials in material to sourceUVRect map. Try baking materials. Can't combine meshes.");
        return false;
      }
      if (this._textureBakeResults.resultMaterials == null || this._textureBakeResults.resultMaterials.Length == 0)
      {
        if (this._textureBakeResults.materialsAndUVRects != null && this._textureBakeResults.materialsAndUVRects.Length != 0 && !this._textureBakeResults.doMultiMaterial && (UnityEngine.Object) this._textureBakeResults.resultMaterial != (UnityEngine.Object) null)
        {
          MB_MultiMaterial[] mbMultiMaterialArray = this._textureBakeResults.resultMaterials = new MB_MultiMaterial[1];
          mbMultiMaterialArray[0] = new MB_MultiMaterial();
          mbMultiMaterialArray[0].combinedMaterial = this._textureBakeResults.resultMaterial;
          mbMultiMaterialArray[0].considerMeshUVs = this._textureBakeResults.fixOutOfBoundsUVs;
          List<Material> materialList = mbMultiMaterialArray[0].sourceMaterials = new List<Material>();
          for (int index = 0; index < this._textureBakeResults.materialsAndUVRects.Length; ++index)
          {
            if (!materialList.Contains(this._textureBakeResults.materialsAndUVRects[index].material))
              materialList.Add(this._textureBakeResults.materialsAndUVRects[index].material);
          }
        }
        else
        {
          Debug.LogError((object) "Texture Bake Results has no result materials. Try baking materials. Can't combine meshes.");
          return false;
        }
      }
      return true;
    }

    public override void Apply(
      MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod)
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
      {
        if (this.meshCombiners[index].isDirty)
        {
          this.meshCombiners[index].combinedMesh.Apply(uv2GenerationMethod);
          this.meshCombiners[index].isDirty = false;
        }
      }
    }

    public override void Apply(
      bool triangles,
      bool vertices,
      bool normals,
      bool tangents,
      bool uvs,
      bool uv2,
      bool uv3,
      bool uv4,
      bool colors,
      bool bones = false,
      bool blendShapesFlag = false,
      MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
      {
        if (this.meshCombiners[index].isDirty)
        {
          this.meshCombiners[index].combinedMesh.Apply(triangles, vertices, normals, tangents, uvs, uv2, uv3, uv4, colors, bones, blendShapesFlag, uv2GenerationMethod);
          this.meshCombiners[index].isDirty = false;
        }
      }
    }

    public override void UpdateSkinnedMeshApproximateBounds()
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        this.meshCombiners[index].combinedMesh.UpdateSkinnedMeshApproximateBounds();
    }

    public override void UpdateSkinnedMeshApproximateBoundsFromBones()
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        this.meshCombiners[index].combinedMesh.UpdateSkinnedMeshApproximateBoundsFromBones();
    }

    public override void UpdateSkinnedMeshApproximateBoundsFromBounds()
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        this.meshCombiners[index].combinedMesh.UpdateSkinnedMeshApproximateBoundsFromBounds();
    }

    public override void UpdateGameObjects(
      GameObject[] gos,
      bool recalcBounds = true,
      bool updateVertices = true,
      bool updateNormals = true,
      bool updateTangents = true,
      bool updateUV = false,
      bool updateUV2 = false,
      bool updateUV3 = false,
      bool updateUV4 = false,
      bool updateColors = false,
      bool updateSkinningInfo = false)
    {
      if (gos == null)
      {
        Debug.LogError((object) "list of game objects cannot be null");
      }
      else
      {
        for (int index = 0; index < this.meshCombiners.Count; ++index)
          this.meshCombiners[index].gosToUpdate.Clear();
        for (int index = 0; index < gos.Length; ++index)
        {
          MB3_MultiMeshCombiner.CombinedMesh combinedMesh = (MB3_MultiMeshCombiner.CombinedMesh) null;
          this.obj2MeshCombinerMap.TryGetValue(gos[index].GetInstanceID(), out combinedMesh);
          if (combinedMesh != null)
            combinedMesh.gosToUpdate.Add(gos[index]);
          else
            Debug.LogWarning((object) ("Object " + ((object) gos[index])?.ToString() + " is not in the combined mesh."));
        }
        for (int index = 0; index < this.meshCombiners.Count; ++index)
        {
          if (this.meshCombiners[index].gosToUpdate.Count > 0)
          {
            this.meshCombiners[index].isDirty = true;
            GameObject[] array = this.meshCombiners[index].gosToUpdate.ToArray();
            this.meshCombiners[index].combinedMesh.UpdateGameObjects(array, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV2, updateUV3, updateUV4, updateColors, updateSkinningInfo);
          }
        }
      }
    }

    public override bool AddDeleteGameObjects(
      GameObject[] gos,
      GameObject[] deleteGOs,
      bool disableRendererInSource = true)
    {
      int[] deleteGOinstanceIDs = (int[]) null;
      if (deleteGOs != null)
      {
        deleteGOinstanceIDs = new int[deleteGOs.Length];
        for (int index = 0; index < deleteGOs.Length; ++index)
        {
          if ((UnityEngine.Object) deleteGOs[index] == (UnityEngine.Object) null)
            Debug.LogError((object) ("The " + index.ToString() + "th object on the list of objects to delete is 'Null'"));
          else
            deleteGOinstanceIDs[index] = deleteGOs[index].GetInstanceID();
        }
      }
      return this.AddDeleteGameObjectsByID(gos, deleteGOinstanceIDs, disableRendererInSource);
    }

    public override bool AddDeleteGameObjectsByID(
      GameObject[] gos,
      int[] deleteGOinstanceIDs,
      bool disableRendererInSource = true)
    {
      if (this._usingTemporaryTextureBakeResult && gos != null && gos.Length != 0)
      {
        MB_Utility.Destroy((UnityEngine.Object) this._textureBakeResults);
        this._textureBakeResults = (MB2_TextureBakeResults) null;
        this._usingTemporaryTextureBakeResult = false;
      }
      if ((UnityEngine.Object) this._textureBakeResults == (UnityEngine.Object) null && gos != null && gos.Length != 0 && (UnityEngine.Object) gos[0] != (UnityEngine.Object) null && !this._CreateTemporaryTextrueBakeResult(gos, this.GetMaterialsOnTargetRenderer()) || !this._validate(gos, deleteGOinstanceIDs))
        return false;
      this._distributeAmongBakers(gos, deleteGOinstanceIDs);
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        MB2_Log.LogDebug("MB2_MultiMeshCombiner.AddDeleteGameObjects numCombinedMeshes: " + this.meshCombiners.Count.ToString() + " added:" + gos?.ToString() + " deleted:" + deleteGOinstanceIDs?.ToString() + " disableRendererInSource:" + disableRendererInSource.ToString() + " maxVertsPerCombined:" + this._maxVertsInMesh.ToString());
      return this._bakeStep1(gos, deleteGOinstanceIDs, disableRendererInSource);
    }

    private bool _validate(GameObject[] gos, int[] deleteGOinstanceIDs)
    {
      if (this._validationLevel == MB2_ValidationLevel.none)
        return true;
      if (this._maxVertsInMesh < 3)
        Debug.LogError((object) ("Invalid value for maxVertsInMesh=" + this._maxVertsInMesh.ToString()));
      this._validateTextureBakeResults();
      if (gos != null)
      {
        for (int index1 = 0; index1 < gos.Length; ++index1)
        {
          if ((UnityEngine.Object) gos[index1] == (UnityEngine.Object) null)
          {
            Debug.LogError((object) ("The " + index1.ToString() + "th object on the list of objects to combine is 'None'. Use Command-Delete on Mac OS X; Delete or Shift-Delete on Windows to remove this one element."));
            return false;
          }
          if (this._validationLevel >= MB2_ValidationLevel.robust)
          {
            for (int index2 = index1 + 1; index2 < gos.Length; ++index2)
            {
              if ((UnityEngine.Object) gos[index1] == (UnityEngine.Object) gos[index2])
              {
                Debug.LogError((object) ("GameObject " + ((object) gos[index1])?.ToString() + "appears twice in list of game objects to add"));
                return false;
              }
            }
            if (this.obj2MeshCombinerMap.ContainsKey(gos[index1].GetInstanceID()))
            {
              bool flag = false;
              if (deleteGOinstanceIDs != null)
              {
                for (int index3 = 0; index3 < deleteGOinstanceIDs.Length; ++index3)
                {
                  if (deleteGOinstanceIDs[index3] == gos[index1].GetInstanceID())
                    flag = true;
                }
              }
              if (!flag)
              {
                Debug.LogError((object) ("GameObject " + ((object) gos[index1])?.ToString() + " is already in the combined mesh " + gos[index1].GetInstanceID().ToString()));
                return false;
              }
            }
          }
        }
      }
      if (deleteGOinstanceIDs != null && this._validationLevel >= MB2_ValidationLevel.robust)
      {
        for (int index4 = 0; index4 < deleteGOinstanceIDs.Length; ++index4)
        {
          for (int index5 = index4 + 1; index5 < deleteGOinstanceIDs.Length; ++index5)
          {
            if (deleteGOinstanceIDs[index4] == deleteGOinstanceIDs[index5])
            {
              Debug.LogError((object) ("GameObject " + deleteGOinstanceIDs[index4].ToString() + "appears twice in list of game objects to delete"));
              return false;
            }
          }
          if (!this.obj2MeshCombinerMap.ContainsKey(deleteGOinstanceIDs[index4]))
            Debug.LogWarning((object) ("GameObject with instance ID " + deleteGOinstanceIDs[index4].ToString() + " on the list of objects to delete is not in the combined mesh."));
        }
      }
      return true;
    }

    private void _distributeAmongBakers(GameObject[] gos, int[] deleteGOinstanceIDs)
    {
      if (gos == null)
        gos = MB3_MultiMeshCombiner.empty;
      if (deleteGOinstanceIDs == null)
        deleteGOinstanceIDs = MB3_MultiMeshCombiner.emptyIDs;
      if ((UnityEngine.Object) this.resultSceneObject == (UnityEngine.Object) null)
        this.resultSceneObject = new GameObject("CombinedMesh-" + this.name);
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        this.meshCombiners[index].extraSpace = this._maxVertsInMesh - this.meshCombiners[index].combinedMesh.GetMesh().vertexCount;
      for (int index = 0; index < deleteGOinstanceIDs.Length; ++index)
      {
        MB3_MultiMeshCombiner.CombinedMesh combinedMesh = (MB3_MultiMeshCombiner.CombinedMesh) null;
        if (this.obj2MeshCombinerMap.TryGetValue(deleteGOinstanceIDs[index], out combinedMesh))
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("MB2_MultiMeshCombiner.Removing " + deleteGOinstanceIDs[index].ToString() + " from meshCombiner " + this.meshCombiners.IndexOf(combinedMesh).ToString());
          combinedMesh.numVertsInListToDelete += combinedMesh.combinedMesh.GetNumVerticesFor(deleteGOinstanceIDs[index]);
          combinedMesh.gosToDelete.Add(deleteGOinstanceIDs[index]);
        }
        else
          Debug.LogWarning((object) ("Object " + deleteGOinstanceIDs[index].ToString() + " in the list of objects to delete is not in the combined mesh."));
      }
      for (int index1 = 0; index1 < gos.Length; ++index1)
      {
        GameObject go = gos[index1];
        int vertexCount = MB_Utility.GetMesh(go).vertexCount;
        MB3_MultiMeshCombiner.CombinedMesh combinedMesh = (MB3_MultiMeshCombiner.CombinedMesh) null;
        for (int index2 = 0; index2 < this.meshCombiners.Count; ++index2)
        {
          if (this.meshCombiners[index2].extraSpace + this.meshCombiners[index2].numVertsInListToDelete - this.meshCombiners[index2].numVertsInListToAdd > vertexCount)
          {
            combinedMesh = this.meshCombiners[index2];
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            {
              MB2_Log.LogDebug("MB2_MultiMeshCombiner.Added " + ((object) gos[index1])?.ToString() + " to combinedMesh " + index2.ToString(), (object) this.LOG_LEVEL);
              break;
            }
            break;
          }
        }
        if (combinedMesh == null)
        {
          combinedMesh = new MB3_MultiMeshCombiner.CombinedMesh(this.maxVertsInMesh, this._resultSceneObject, this._LOG_LEVEL);
          this._setMBValues(combinedMesh.combinedMesh);
          this.meshCombiners.Add(combinedMesh);
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("MB2_MultiMeshCombiner.Created new combinedMesh");
        }
        combinedMesh.gosToAdd.Add(go);
        combinedMesh.numVertsInListToAdd += vertexCount;
      }
    }

    private bool _bakeStep1(
      GameObject[] gos,
      int[] deleteGOinstanceIDs,
      bool disableRendererInSource)
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
      {
        MB3_MultiMeshCombiner.CombinedMesh meshCombiner = this.meshCombiners[index];
        if ((UnityEngine.Object) meshCombiner.combinedMesh.targetRenderer == (UnityEngine.Object) null)
        {
          meshCombiner.combinedMesh.resultSceneObject = this._resultSceneObject;
          meshCombiner.combinedMesh.BuildSceneMeshObject(gos, true);
          if (this._LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("BuildSO combiner {0} goID {1} targetRenID {2} meshID {3}", (object) index, (object) meshCombiner.combinedMesh.targetRenderer.gameObject.GetInstanceID(), (object) meshCombiner.combinedMesh.targetRenderer.GetInstanceID(), (object) meshCombiner.combinedMesh.GetMesh().GetInstanceID());
        }
        else if ((UnityEngine.Object) meshCombiner.combinedMesh.targetRenderer.transform.parent != (UnityEngine.Object) this.resultSceneObject.transform)
        {
          Debug.LogError((object) "targetRender objects must be children of resultSceneObject");
          return false;
        }
        if (meshCombiner.gosToAdd.Count > 0 || meshCombiner.gosToDelete.Count > 0)
        {
          meshCombiner.combinedMesh.AddDeleteGameObjectsByID(meshCombiner.gosToAdd.ToArray(), meshCombiner.gosToDelete.ToArray(), disableRendererInSource);
          if (this._LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Baked combiner {0} obsAdded {1} objsRemoved {2} goID {3} targetRenID {4} meshID {5}", (object) index, (object) meshCombiner.gosToAdd.Count, (object) meshCombiner.gosToDelete.Count, (object) meshCombiner.combinedMesh.targetRenderer.gameObject.GetInstanceID(), (object) meshCombiner.combinedMesh.targetRenderer.GetInstanceID(), (object) meshCombiner.combinedMesh.GetMesh().GetInstanceID());
        }
        Renderer targetRenderer = meshCombiner.combinedMesh.targetRenderer;
        UnityEngine.Mesh mesh = meshCombiner.combinedMesh.GetMesh();
        if (targetRenderer is MeshRenderer)
          targetRenderer.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        else
          ((SkinnedMeshRenderer) targetRenderer).sharedMesh = mesh;
      }
      for (int index1 = 0; index1 < this.meshCombiners.Count; ++index1)
      {
        MB3_MultiMeshCombiner.CombinedMesh meshCombiner = this.meshCombiners[index1];
        for (int index2 = 0; index2 < meshCombiner.gosToDelete.Count; ++index2)
          this.obj2MeshCombinerMap.Remove(meshCombiner.gosToDelete[index2]);
      }
      for (int index3 = 0; index3 < this.meshCombiners.Count; ++index3)
      {
        MB3_MultiMeshCombiner.CombinedMesh meshCombiner = this.meshCombiners[index3];
        for (int index4 = 0; index4 < meshCombiner.gosToAdd.Count; ++index4)
          this.obj2MeshCombinerMap.Add(meshCombiner.gosToAdd[index4].GetInstanceID(), meshCombiner);
        if (meshCombiner.gosToAdd.Count > 0 || meshCombiner.gosToDelete.Count > 0)
        {
          meshCombiner.gosToDelete.Clear();
          meshCombiner.gosToAdd.Clear();
          meshCombiner.numVertsInListToDelete = 0;
          meshCombiner.numVertsInListToAdd = 0;
          meshCombiner.isDirty = true;
        }
      }
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      {
        string str1 = "Meshes in combined:";
        int num;
        for (int index = 0; index < this.meshCombiners.Count; ++index)
        {
          string[] strArray = new string[6]
          {
            str1,
            " mesh",
            index.ToString(),
            "(",
            null,
            null
          };
          num = this.meshCombiners[index].combinedMesh.GetObjectsInCombined().Count;
          strArray[4] = num.ToString();
          strArray[5] = ")\n";
          str1 = string.Concat(strArray);
        }
        string str2 = str1;
        num = this.resultSceneObject.transform.childCount;
        string str3 = num.ToString();
        MB2_Log.LogDebug(str2 + "children in result: " + str3, (object) this.LOG_LEVEL);
      }
      return this.meshCombiners.Count > 0;
    }

    public override Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> BuildSourceBlendShapeToCombinedIndexMap()
    {
      Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> combinedIndexMap = new Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue>();
      for (int index1 = 0; index1 < this.meshCombiners.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.meshCombiners[index1].combinedMesh.blendShapes.Length; ++index2)
        {
          MB3_MeshCombinerSingle.MBBlendShape blendShape = this.meshCombiners[index1].combinedMesh.blendShapes[index2];
          combinedIndexMap.Add(new MB3_MeshCombiner.MBBlendShapeKey(blendShape.gameObjectID, blendShape.indexInSource), new MB3_MeshCombiner.MBBlendShapeValue()
          {
            combinedMeshGameObject = this.meshCombiners[index1].combinedMesh.targetRenderer.gameObject,
            blendShapeIndex = index2
          });
        }
      }
      return combinedIndexMap;
    }

    public override void ClearBuffers()
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        this.meshCombiners[index].combinedMesh.ClearBuffers();
      this.obj2MeshCombinerMap.Clear();
    }

    public override void ClearMesh() => this.DestroyMesh();

    public override void DestroyMesh()
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
      {
        if ((UnityEngine.Object) this.meshCombiners[index].combinedMesh.targetRenderer != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) this.meshCombiners[index].combinedMesh.targetRenderer.gameObject);
        this.meshCombiners[index].combinedMesh.ClearMesh();
      }
      this.obj2MeshCombinerMap.Clear();
      this.meshCombiners.Clear();
    }

    public override void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
    {
      for (int index = 0; index < this.meshCombiners.Count; ++index)
      {
        if ((UnityEngine.Object) this.meshCombiners[index].combinedMesh.targetRenderer != (UnityEngine.Object) null)
          editorMethods.Destroy((UnityEngine.Object) this.meshCombiners[index].combinedMesh.targetRenderer.gameObject);
        this.meshCombiners[index].combinedMesh.ClearMesh();
      }
      this.obj2MeshCombinerMap.Clear();
      this.meshCombiners.Clear();
    }

    private void _setMBValues(MB3_MeshCombinerSingle targ)
    {
      targ.validationLevel = this._validationLevel;
      targ.renderType = this.renderType;
      targ.outputOption = MB2_OutputOptions.bakeIntoSceneObject;
      targ.lightmapOption = this.lightmapOption;
      targ.textureBakeResults = this.textureBakeResults;
      targ.doNorm = this.doNorm;
      targ.doTan = this.doTan;
      targ.doCol = this.doCol;
      targ.doUV = this.doUV;
      targ.doUV3 = this.doUV3;
      targ.doUV4 = this.doUV4;
      targ.doBlendShapes = this.doBlendShapes;
      targ.optimizeAfterBake = this.optimizeAfterBake;
      targ.recenterVertsToBoundsCenter = this.recenterVertsToBoundsCenter;
      targ.uv2UnwrappingParamsHardAngle = this.uv2UnwrappingParamsHardAngle;
      targ.uv2UnwrappingParamsPackMargin = this.uv2UnwrappingParamsPackMargin;
    }

    public override List<Material> GetMaterialsOnTargetRenderer()
    {
      HashSet<Material> collection = new HashSet<Material>();
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        collection.UnionWith((IEnumerable<Material>) this.meshCombiners[index].combinedMesh.GetMaterialsOnTargetRenderer());
      return new List<Material>((IEnumerable<Material>) collection);
    }

    public override void CheckIntegrity()
    {
      if (!MB3_MeshBakerRoot.DO_INTEGRITY_CHECKS)
        return;
      for (int index = 0; index < this.meshCombiners.Count; ++index)
        this.meshCombiners[index].combinedMesh.CheckIntegrity();
    }

    [Serializable]
    public class CombinedMesh
    {
      public MB3_MeshCombinerSingle combinedMesh;
      public int extraSpace = -1;
      public int numVertsInListToDelete;
      public int numVertsInListToAdd;
      public List<GameObject> gosToAdd;
      public List<int> gosToDelete;
      public List<GameObject> gosToUpdate;
      public bool isDirty;

      public CombinedMesh(int maxNumVertsInMesh, GameObject resultSceneObject, MB2_LogLevel ll)
      {
        this.combinedMesh = new MB3_MeshCombinerSingle();
        this.combinedMesh.resultSceneObject = resultSceneObject;
        this.combinedMesh.LOG_LEVEL = ll;
        this.extraSpace = maxNumVertsInMesh;
        this.numVertsInListToDelete = 0;
        this.numVertsInListToAdd = 0;
        this.gosToAdd = new List<GameObject>();
        this.gosToDelete = new List<int>();
        this.gosToUpdate = new List<GameObject>();
      }

      public bool isEmpty()
      {
        List<GameObject> gameObjectList = new List<GameObject>();
        gameObjectList.AddRange((IEnumerable<GameObject>) this.combinedMesh.GetObjectsInCombined());
        for (int index1 = 0; index1 < this.gosToDelete.Count; ++index1)
        {
          for (int index2 = 0; index2 < gameObjectList.Count; ++index2)
          {
            if (gameObjectList[index2].GetInstanceID() == this.gosToDelete[index1])
            {
              gameObjectList.RemoveAt(index2);
              break;
            }
          }
        }
        return gameObjectList.Count == 0;
      }
    }
  }
}
