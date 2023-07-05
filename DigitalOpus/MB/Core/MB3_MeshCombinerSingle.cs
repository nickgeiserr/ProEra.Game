// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshCombinerSingle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MeshCombinerSingle : MB3_MeshCombiner
  {
    [SerializeField]
    protected List<GameObject> objectsInCombinedMesh = new List<GameObject>();
    [SerializeField]
    private int lightmapIndex = -1;
    [SerializeField]
    private List<MB3_MeshCombinerSingle.MB_DynamicGameObject> mbDynamicObjectsInCombinedMesh = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();
    private Dictionary<int, MB3_MeshCombinerSingle.MB_DynamicGameObject> _instance2combined_map = new Dictionary<int, MB3_MeshCombinerSingle.MB_DynamicGameObject>();
    [SerializeField]
    private Vector3[] verts = new Vector3[0];
    [SerializeField]
    private Vector3[] normals = new Vector3[0];
    [SerializeField]
    private Vector4[] tangents = new Vector4[0];
    [SerializeField]
    private Vector2[] uvs = new Vector2[0];
    [SerializeField]
    private Vector2[] uv2s = new Vector2[0];
    [SerializeField]
    private Vector2[] uv3s = new Vector2[0];
    [SerializeField]
    private Vector2[] uv4s = new Vector2[0];
    [SerializeField]
    private Color[] colors = new Color[0];
    [SerializeField]
    private Matrix4x4[] bindPoses = new Matrix4x4[0];
    [SerializeField]
    private Transform[] bones = new Transform[0];
    [SerializeField]
    internal MB3_MeshCombinerSingle.MBBlendShape[] blendShapes = new MB3_MeshCombinerSingle.MBBlendShape[0];
    [SerializeField]
    internal MB3_MeshCombinerSingle.MBBlendShape[] blendShapesInCombined = new MB3_MeshCombinerSingle.MBBlendShape[0];
    [SerializeField]
    private MB3_MeshCombinerSingle.SerializableIntArray[] submeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[0];
    [SerializeField]
    private UnityEngine.Mesh _mesh;
    private BoneWeight[] boneWeights = new BoneWeight[0];
    private GameObject[] empty = new GameObject[0];
    private int[] emptyIDs = new int[0];

    public override MB2_TextureBakeResults textureBakeResults
    {
      set
      {
        if (this.mbDynamicObjectsInCombinedMesh.Count > 0 && (UnityEngine.Object) this._textureBakeResults != (UnityEngine.Object) value && (UnityEngine.Object) this._textureBakeResults != (UnityEngine.Object) null && this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) "If Texture Bake Result is changed then objects currently in combined mesh may be invalid.");
        this._textureBakeResults = value;
      }
    }

    public override MB_RenderType renderType
    {
      set
      {
        if (value == MB_RenderType.skinnedMeshRenderer && this._renderType == MB_RenderType.meshRenderer && this.boneWeights.Length != this.verts.Length)
          Debug.LogError((object) "Can't set the render type to SkinnedMeshRenderer without clearing the mesh first. Try deleteing the CombinedMesh scene object.");
        this._renderType = value;
      }
    }

    public override GameObject resultSceneObject
    {
      set
      {
        if ((UnityEngine.Object) this._resultSceneObject != (UnityEngine.Object) value)
        {
          this._targetRenderer = (Renderer) null;
          if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) null && this._LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) "Result Scene Object was changed when this mesh baker component had a reference to a mesh. If mesh is being used by another object make sure to reset the mesh to none before baking to avoid overwriting the other mesh.");
        }
        this._resultSceneObject = value;
      }
    }

    private MB3_MeshCombinerSingle.MB_DynamicGameObject instance2Combined_MapGet(int gameObjectID) => this._instance2combined_map[gameObjectID];

    private void instance2Combined_MapAdd(
      int gameObjectID,
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo)
    {
      this._instance2combined_map.Add(gameObjectID, dgo);
    }

    private void instance2Combined_MapRemove(int gameObjectID) => this._instance2combined_map.Remove(gameObjectID);

    private bool instance2Combined_MapTryGetValue(
      int gameObjectID,
      out MB3_MeshCombinerSingle.MB_DynamicGameObject dgo)
    {
      return this._instance2combined_map.TryGetValue(gameObjectID, out dgo);
    }

    private int instance2Combined_MapCount() => this._instance2combined_map.Count;

    private void instance2Combined_MapClear() => this._instance2combined_map.Clear();

    private bool instance2Combined_MapContainsKey(int gameObjectID) => this._instance2combined_map.ContainsKey(gameObjectID);

    public override int GetNumObjectsInCombined() => this.mbDynamicObjectsInCombinedMesh.Count;

    public override List<GameObject> GetObjectsInCombined()
    {
      List<GameObject> objectsInCombined = new List<GameObject>();
      objectsInCombined.AddRange((IEnumerable<GameObject>) this.objectsInCombinedMesh);
      return objectsInCombined;
    }

    public UnityEngine.Mesh GetMesh()
    {
      if ((UnityEngine.Object) this._mesh == (UnityEngine.Object) null)
        this._mesh = new UnityEngine.Mesh();
      return this._mesh;
    }

    public Transform[] GetBones() => this.bones;

    public override int GetLightmapIndex() => this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout || this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping ? this.lightmapIndex : -1;

    public override int GetNumVerticesFor(GameObject go) => this.GetNumVerticesFor(go.GetInstanceID());

    public override int GetNumVerticesFor(int instanceID)
    {
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo;
      return this.instance2Combined_MapTryGetValue(instanceID, out dgo) ? dgo.numVerts : -1;
    }

    public override Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> BuildSourceBlendShapeToCombinedIndexMap()
    {
      Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> combinedIndexMap = new Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue>();
      for (int index = 0; index < this.blendShapesInCombined.Length; ++index)
        combinedIndexMap.Add(new MB3_MeshCombiner.MBBlendShapeKey(this.blendShapesInCombined[index].gameObjectID, this.blendShapesInCombined[index].indexInSource), new MB3_MeshCombiner.MBBlendShapeValue()
        {
          combinedMeshGameObject = this._targetRenderer.gameObject,
          blendShapeIndex = index
        });
      return combinedIndexMap;
    }

    private void _initialize(int numResultMats)
    {
      if (this.mbDynamicObjectsInCombinedMesh.Count == 0)
        this.lightmapIndex = -1;
      if ((UnityEngine.Object) this._mesh == (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("_initialize Creating new Mesh");
        this._mesh = this.GetMesh();
      }
      if (this.instance2Combined_MapCount() != this.mbDynamicObjectsInCombinedMesh.Count)
      {
        this.instance2Combined_MapClear();
        for (int index = 0; index < this.mbDynamicObjectsInCombinedMesh.Count; ++index)
        {
          if (this.mbDynamicObjectsInCombinedMesh[index] != null)
            this.instance2Combined_MapAdd(this.mbDynamicObjectsInCombinedMesh[index].instanceID, this.mbDynamicObjectsInCombinedMesh[index]);
        }
        this.boneWeights = this._mesh.boneWeights;
      }
      if (this.objectsInCombinedMesh.Count == 0 && this.submeshTris.Length != numResultMats)
      {
        this.submeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[numResultMats];
        for (int index = 0; index < this.submeshTris.Length; ++index)
          this.submeshTris[index] = new MB3_MeshCombinerSingle.SerializableIntArray(0);
      }
      if (this.mbDynamicObjectsInCombinedMesh.Count > 0 && this.mbDynamicObjectsInCombinedMesh[0].indexesOfBonesUsed.Length == 0 && this.renderType == MB_RenderType.skinnedMeshRenderer && this.boneWeights.Length != 0)
      {
        for (int index = 0; index < this.mbDynamicObjectsInCombinedMesh.Count; ++index)
        {
          MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index];
          HashSet<int> intSet = new HashSet<int>();
          for (int vertIdx = dynamicGameObject.vertIdx; vertIdx < dynamicGameObject.vertIdx + dynamicGameObject.numVerts; ++vertIdx)
          {
            if ((double) this.boneWeights[vertIdx].weight0 > 0.0)
              intSet.Add(this.boneWeights[vertIdx].boneIndex0);
            if ((double) this.boneWeights[vertIdx].weight1 > 0.0)
              intSet.Add(this.boneWeights[vertIdx].boneIndex1);
            if ((double) this.boneWeights[vertIdx].weight2 > 0.0)
              intSet.Add(this.boneWeights[vertIdx].boneIndex2);
            if ((double) this.boneWeights[vertIdx].weight3 > 0.0)
              intSet.Add(this.boneWeights[vertIdx].boneIndex3);
          }
          dynamicGameObject.indexesOfBonesUsed = new int[intSet.Count];
          intSet.CopyTo(dynamicGameObject.indexesOfBonesUsed);
        }
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          Debug.Log((object) "Baker used old systems that duplicated bones. Upgrading to new system by building indexesOfBonesUsed");
      }
      if (this.LOG_LEVEL < MB2_LogLevel.trace)
        return;
      Debug.Log((object) string.Format("_initialize numObjsInCombined={0}", (object) this.mbDynamicObjectsInCombinedMesh.Count));
    }

    private bool _collectMaterialTriangles(
      UnityEngine.Mesh m,
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo,
      Material[] sharedMaterials,
      OrderedDictionary sourceMats2submeshIdx_map)
    {
      int length = m.subMeshCount;
      if (sharedMaterials.Length < length)
        length = sharedMaterials.Length;
      dgo._tmpSubmeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[length];
      dgo.targetSubmeshIdxs = new int[length];
      for (int submesh = 0; submesh < length; ++submesh)
      {
        if (this._textureBakeResults.doMultiMaterial)
        {
          if (!sourceMats2submeshIdx_map.Contains((object) sharedMaterials[submesh]))
          {
            Debug.LogError((object) ("Object " + dgo.name + " has a material that was not found in the result materials maping. " + ((object) sharedMaterials[submesh])?.ToString()));
            return false;
          }
          dgo.targetSubmeshIdxs[submesh] = (int) sourceMats2submeshIdx_map[(object) sharedMaterials[submesh]];
        }
        else
          dgo.targetSubmeshIdxs[submesh] = 0;
        dgo._tmpSubmeshTris[submesh] = new MB3_MeshCombinerSingle.SerializableIntArray();
        dgo._tmpSubmeshTris[submesh].data = m.GetTriangles(submesh);
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Collecting triangles for: " + dgo.name + " submesh:" + submesh.ToString() + " maps to submesh:" + dgo.targetSubmeshIdxs[submesh].ToString() + " added:" + dgo._tmpSubmeshTris[submesh].data.Length.ToString(), (object) this.LOG_LEVEL);
      }
      return true;
    }

    private bool _collectOutOfBoundsUVRects2(
      UnityEngine.Mesh m,
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo,
      Material[] sharedMaterials,
      OrderedDictionary sourceMats2submeshIdx_map,
      Dictionary<int, MB_Utility.MeshAnalysisResult[]> meshAnalysisResults,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
    {
      if ((UnityEngine.Object) this._textureBakeResults == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "Need to bake textures into combined material");
        return false;
      }
      MB_Utility.MeshAnalysisResult[] meshAnalysisResultArray1;
      if (meshAnalysisResults.TryGetValue(m.GetInstanceID(), out meshAnalysisResultArray1))
      {
        dgo.obUVRects = new Rect[sharedMaterials.Length];
        for (int index = 0; index < dgo.obUVRects.Length; ++index)
          dgo.obUVRects[index] = meshAnalysisResultArray1[index].uvRect;
      }
      else
      {
        int subMeshCount = m.subMeshCount;
        int length = subMeshCount;
        if (sharedMaterials.Length < subMeshCount)
          length = sharedMaterials.Length;
        dgo.obUVRects = new Rect[length];
        MB_Utility.MeshAnalysisResult[] meshAnalysisResultArray2 = new MB_Utility.MeshAnalysisResult[subMeshCount];
        for (int submeshIndex = 0; submeshIndex < subMeshCount; ++submeshIndex)
        {
          if (this._textureBakeResults.resultMaterials[dgo.targetSubmeshIdxs[submeshIndex]].considerMeshUVs)
          {
            MB_Utility.hasOutOfBoundsUVs(meshChannelCache.GetUv0Raw(m), m, ref meshAnalysisResultArray2[submeshIndex], submeshIndex);
            Rect uvRect = meshAnalysisResultArray2[submeshIndex].uvRect;
            if (submeshIndex < length)
              dgo.obUVRects[submeshIndex] = uvRect;
          }
        }
        meshAnalysisResults.Add(m.GetInstanceID(), meshAnalysisResultArray2);
      }
      return true;
    }

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

    private bool _validateMeshFlags()
    {
      if (this.mbDynamicObjectsInCombinedMesh.Count > 0 && (!this._doNorm && this.doNorm || !this._doTan && this.doTan || !this._doCol && this.doCol || !this._doUV && this.doUV || !this._doUV3 && this.doUV3 || !this._doUV4 && this.doUV4))
      {
        Debug.LogError((object) "The channels have changed. There are already objects in the combined mesh that were added with a different set of channels.");
        return false;
      }
      this._doNorm = this.doNorm;
      this._doTan = this.doTan;
      this._doCol = this.doCol;
      this._doUV = this.doUV;
      this._doUV3 = this.doUV3;
      this._doUV4 = this.doUV4;
      return true;
    }

    private bool _showHide(GameObject[] goToShow, GameObject[] goToHide)
    {
      if (goToShow == null)
        goToShow = this.empty;
      if (goToHide == null)
        goToHide = this.empty;
      this._initialize(this._textureBakeResults.resultMaterials.Length);
      for (int index = 0; index < goToHide.Length; ++index)
      {
        if (!this.instance2Combined_MapContainsKey(goToHide[index].GetInstanceID()))
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Trying to hide an object " + ((object) goToHide[index])?.ToString() + " that is not in combined mesh. Did you initially bake with 'clear buffers after bake' enabled?"));
          return false;
        }
      }
      for (int index = 0; index < goToShow.Length; ++index)
      {
        if (!this.instance2Combined_MapContainsKey(goToShow[index].GetInstanceID()))
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Trying to show an object " + ((object) goToShow[index])?.ToString() + " that is not in combined mesh. Did you initially bake with 'clear buffers after bake' enabled?"));
          return false;
        }
      }
      for (int index = 0; index < goToHide.Length; ++index)
        this._instance2combined_map[goToHide[index].GetInstanceID()].show = false;
      for (int index = 0; index < goToShow.Length; ++index)
        this._instance2combined_map[goToShow[index].GetInstanceID()].show = true;
      return true;
    }

    private bool _addToCombined(
      GameObject[] goToAdd,
      int[] goToDelete,
      bool disableRendererInSource)
    {
      if (!this._validateTextureBakeResults() || !this._validateMeshFlags() || !this.ValidateTargRendererAndMeshAndResultSceneObj())
        return false;
      if (this.outputOption != MB2_OutputOptions.bakeMeshAssetsInPlace && this.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        if ((UnityEngine.Object) this._targetRenderer == (UnityEngine.Object) null || !(this._targetRenderer is SkinnedMeshRenderer))
        {
          Debug.LogError((object) "Target renderer must be set and must be a SkinnedMeshRenderer");
          return false;
        }
        if ((UnityEngine.Object) ((SkinnedMeshRenderer) this.targetRenderer).sharedMesh != (UnityEngine.Object) this._mesh)
          Debug.LogError((object) "The combined mesh was not assigned to the targetRenderer. Try using buildSceneMeshObject to set up the combined mesh correctly");
      }
      if (this._doBlendShapes && this.renderType != MB_RenderType.skinnedMeshRenderer)
      {
        Debug.LogError((object) "If doBlendShapes is set then RenderType must be skinnedMeshRenderer.");
        return false;
      }
      GameObject[] _goToAdd = goToAdd != null ? (GameObject[]) goToAdd.Clone() : this.empty;
      int[] array = goToDelete != null ? (int[]) goToDelete.Clone() : this.emptyIDs;
      if ((UnityEngine.Object) this._mesh == (UnityEngine.Object) null)
        this.DestroyMesh();
      MB2_TextureBakeResults.Material2AtlasRectangleMapper atlasRectangleMapper = new MB2_TextureBakeResults.Material2AtlasRectangleMapper(this.textureBakeResults);
      int length1 = this._textureBakeResults.resultMaterials.Length;
      this._initialize(length1);
      if (this.submeshTris.Length != length1)
      {
        Debug.LogError((object) ("The number of submeshes " + this.submeshTris.Length.ToString() + " in the combined mesh was not equal to the number of result materials " + length1.ToString() + " in the Texture Bake Result"));
        return false;
      }
      if (this._mesh.vertexCount > 0 && this._instance2combined_map.Count == 0)
        Debug.LogWarning((object) "There were vertices in the combined mesh but nothing in the MeshBaker buffers. If you are trying to bake in the editor and modify at runtime, make sure 'Clear Buffers After Bake' is unchecked.");
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        MB2_Log.LogDebug("==== Calling _addToCombined objs adding:" + _goToAdd.Length.ToString() + " objs deleting:" + array.Length.ToString() + " fixOutOfBounds:" + this.textureBakeResults.DoAnyResultMatsUseConsiderMeshUVs().ToString() + " doMultiMaterial:" + this.textureBakeResults.doMultiMaterial.ToString() + " disableRenderersInSource:" + disableRendererInSource.ToString(), (object) this.LOG_LEVEL);
      if (this._textureBakeResults.resultMaterials == null || this._textureBakeResults.resultMaterials.Length == 0)
      {
        this._textureBakeResults.resultMaterials = new MB_MultiMaterial[1];
        this._textureBakeResults.resultMaterials[0] = new MB_MultiMaterial();
        this._textureBakeResults.resultMaterials[0].combinedMaterial = this._textureBakeResults.resultMaterial;
        this._textureBakeResults.resultMaterials[0].considerMeshUVs = false;
        List<Material> materialList = this._textureBakeResults.resultMaterials[0].sourceMaterials = new List<Material>();
        for (int index = 0; index < this._textureBakeResults.materialsAndUVRects.Length; ++index)
          materialList.Add(this._textureBakeResults.materialsAndUVRects[index].material);
      }
      OrderedDictionary sourceMats2submeshIdx_map = new OrderedDictionary();
      for (int index1 = 0; index1 < length1; ++index1)
      {
        MB_MultiMaterial resultMaterial = this._textureBakeResults.resultMaterials[index1];
        for (int index2 = 0; index2 < resultMaterial.sourceMaterials.Count; ++index2)
        {
          if ((UnityEngine.Object) resultMaterial.sourceMaterials[index2] == (UnityEngine.Object) null)
          {
            Debug.LogError((object) ("Found null material in source materials for combined mesh materials " + index1.ToString()));
            return false;
          }
          if (!sourceMats2submeshIdx_map.Contains((object) resultMaterial.sourceMaterials[index2]))
            sourceMats2submeshIdx_map.Add((object) resultMaterial.sourceMaterials[index2], (object) index1);
        }
      }
      int totalDeleteVerts = 0;
      int[] numArray1 = new int[length1];
      int num1 = 0;
      List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[] dynamicGameObjectListArray = (List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[]) null;
      HashSet<int> intSet = new HashSet<int>();
      HashSet<MB3_MeshCombinerSingle.BoneAndBindpose> bonesToAdd = new HashSet<MB3_MeshCombinerSingle.BoneAndBindpose>();
      if (this.renderType == MB_RenderType.skinnedMeshRenderer && array.Length != 0)
        dynamicGameObjectListArray = this._buildBoneIdx2dgoMap();
      for (int index3 = 0; index3 < array.Length; ++index3)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dgo;
        if (this.instance2Combined_MapTryGetValue(array[index3], out dgo))
        {
          totalDeleteVerts += dgo.numVerts;
          num1 += dgo.numBlendShapes;
          if (this.renderType == MB_RenderType.skinnedMeshRenderer)
          {
            for (int index4 = 0; index4 < dgo.indexesOfBonesUsed.Length; ++index4)
            {
              if (dynamicGameObjectListArray[dgo.indexesOfBonesUsed[index4]].Contains(dgo))
              {
                dynamicGameObjectListArray[dgo.indexesOfBonesUsed[index4]].Remove(dgo);
                if (dynamicGameObjectListArray[dgo.indexesOfBonesUsed[index4]].Count == 0)
                  intSet.Add(dgo.indexesOfBonesUsed[index4]);
              }
            }
          }
          for (int index5 = 0; index5 < dgo.submeshNumTris.Length; ++index5)
            numArray1[index5] += dgo.submeshNumTris[index5];
        }
        else if (this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) "Trying to delete an object that is not in combined mesh");
      }
      List<MB3_MeshCombinerSingle.MB_DynamicGameObject> dynamicGameObjectList = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();
      Dictionary<int, MB_Utility.MeshAnalysisResult[]> dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelsCache = new MB3_MeshCombinerSingle.MeshChannelsCache(this);
      int num2 = 0;
      int[] numArray2 = new int[length1];
      int num3 = 0;
      Dictionary<Transform, int> bone2idx = new Dictionary<Transform, int>();
      for (int index = 0; index < this.bones.Length; ++index)
        bone2idx.Add(this.bones[index], index);
      for (int i = 0; i < _goToAdd.Length; i++)
      {
        if (!this.instance2Combined_MapContainsKey(_goToAdd[i].GetInstanceID()) || Array.FindIndex<int>(array, (Predicate<int>) (o => o == _goToAdd[i].GetInstanceID())) != -1)
        {
          MB3_MeshCombinerSingle.MB_DynamicGameObject dgo = new MB3_MeshCombinerSingle.MB_DynamicGameObject();
          GameObject go = _goToAdd[i];
          Material[] goMaterials = MB_Utility.GetGOMaterials(go);
          if (this.LOG_LEVEL >= MB2_LogLevel.trace)
            Debug.Log((object) string.Format("Getting {0} shared materials for {1}", (object) goMaterials.Length, (object) go));
          if (goMaterials == null)
          {
            Debug.LogError((object) ("Object " + go.name + " does not have a Renderer"));
            _goToAdd[i] = (GameObject) null;
            return false;
          }
          UnityEngine.Mesh mesh = MB_Utility.GetMesh(go);
          if ((UnityEngine.Object) mesh == (UnityEngine.Object) null)
          {
            Debug.LogError((object) ("Object " + go.name + " MeshFilter or SkinedMeshRenderer had no mesh"));
            _goToAdd[i] = (GameObject) null;
            return false;
          }
          if (MBVersion.IsRunningAndMeshNotReadWriteable(mesh))
          {
            Debug.LogError((object) ("Object " + go.name + " Mesh Importer has read/write flag set to 'false'. This needs to be set to 'true' in order to read data from this mesh."));
            _goToAdd[i] = (GameObject) null;
            return false;
          }
          Rect[] rectArray1 = new Rect[goMaterials.Length];
          Rect[] rectArray2 = new Rect[goMaterials.Length];
          Rect[] rectArray3 = new Rect[goMaterials.Length];
          string errorMsg = "";
          for (int submeshIdx = 0; submeshIdx < goMaterials.Length; ++submeshIdx)
          {
            object obj = sourceMats2submeshIdx_map[(object) goMaterials[submeshIdx]];
            if (obj == null)
            {
              Debug.LogError((object) ("Source object " + go.name + " used a material " + ((object) goMaterials[submeshIdx])?.ToString() + " that was not in the baked materials."));
              return false;
            }
            int idxInResultMats = (int) obj;
            if (!atlasRectangleMapper.TryMapMaterialToUVRect(goMaterials[submeshIdx], mesh, submeshIdx, idxInResultMats, meshChannelsCache, dictionary, out rectArray1[submeshIdx], out rectArray2[submeshIdx], out rectArray3[submeshIdx], ref errorMsg, this.LOG_LEVEL))
            {
              Debug.LogError((object) errorMsg);
              _goToAdd[i] = (GameObject) null;
              return false;
            }
          }
          if ((UnityEngine.Object) _goToAdd[i] != (UnityEngine.Object) null)
          {
            dynamicGameObjectList.Add(dgo);
            dgo.name = string.Format("{0} {1}", (object) ((object) _goToAdd[i]).ToString(), (object) _goToAdd[i].GetInstanceID());
            dgo.instanceID = _goToAdd[i].GetInstanceID();
            dgo.uvRects = rectArray1;
            dgo.encapsulatingRect = rectArray2;
            dgo.sourceMaterialTiling = rectArray3;
            dgo.numVerts = mesh.vertexCount;
            if (this._doBlendShapes)
              dgo.numBlendShapes = mesh.blendShapeCount;
            Renderer renderer = MB_Utility.GetRenderer(go);
            if (this.renderType == MB_RenderType.skinnedMeshRenderer)
              this._CollectBonesToAddForDGO(dgo, bone2idx, intSet, bonesToAdd, renderer, meshChannelsCache);
            if (this.lightmapIndex == -1)
              this.lightmapIndex = renderer.lightmapIndex;
            if (this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
            {
              if (this.lightmapIndex != renderer.lightmapIndex && this.LOG_LEVEL >= MB2_LogLevel.warn)
                Debug.LogWarning((object) ("Object " + go.name + " has a different lightmap index. Lightmapping will not work."));
              if (!MBVersion.GetActive(go) && this.LOG_LEVEL >= MB2_LogLevel.warn)
                Debug.LogWarning((object) ("Object " + go.name + " is inactive. Can only get lightmap index of active objects."));
              if (renderer.lightmapIndex == -1 && this.LOG_LEVEL >= MB2_LogLevel.warn)
                Debug.LogWarning((object) ("Object " + go.name + " does not have an index to a lightmap."));
            }
            dgo.lightmapIndex = renderer.lightmapIndex;
            dgo.lightmapTilingOffset = MBVersion.GetLightmapTilingOffset(renderer);
            if (!this._collectMaterialTriangles(mesh, dgo, goMaterials, sourceMats2submeshIdx_map))
              return false;
            dgo.meshSize = renderer.bounds.size;
            dgo.submeshNumTris = new int[length1];
            dgo.submeshTriIdxs = new int[length1];
            if (this.textureBakeResults.DoAnyResultMatsUseConsiderMeshUVs() && !this._collectOutOfBoundsUVRects2(mesh, dgo, goMaterials, sourceMats2submeshIdx_map, dictionary, meshChannelsCache))
              return false;
            num2 += dgo.numVerts;
            num3 += dgo.numBlendShapes;
            for (int index = 0; index < dgo._tmpSubmeshTris.Length; ++index)
              numArray2[dgo.targetSubmeshIdxs[index]] += dgo._tmpSubmeshTris[index].data.Length;
            dgo.invertTriangles = this.IsMirrored(go.transform.localToWorldMatrix);
          }
        }
        else
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Object " + _goToAdd[i].name + " has already been added"));
          _goToAdd[i] = (GameObject) null;
        }
      }
      for (int index = 0; index < _goToAdd.Length; ++index)
      {
        if ((UnityEngine.Object) _goToAdd[index] != (UnityEngine.Object) null & disableRendererInSource)
        {
          MB_Utility.DisableRendererInSource(_goToAdd[index]);
          if (this.LOG_LEVEL == MB2_LogLevel.trace)
            Debug.Log((object) ("Disabling renderer on " + _goToAdd[index].name + " id=" + _goToAdd[index].GetInstanceID().ToString()));
        }
      }
      int length2 = this.verts.Length + num2 - totalDeleteVerts;
      int length3 = this.bindPoses.Length + bonesToAdd.Count - intSet.Count;
      int[] numArray3 = new int[length1];
      int length4 = this.blendShapes.Length + num3 - num1;
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) ("Verts adding:" + num2.ToString() + " deleting:" + totalDeleteVerts.ToString() + " submeshes:" + numArray3.Length.ToString() + " bones:" + length3.ToString() + " blendShapes:" + length4.ToString()));
      for (int index = 0; index < numArray3.Length; ++index)
      {
        numArray3[index] = this.submeshTris[index].data.Length + numArray2[index] - numArray1[index];
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("    submesh :" + index.ToString() + " already contains:" + this.submeshTris[index].data.Length.ToString() + " tris to be Added:" + numArray2[index].ToString() + " tris to be Deleted:" + numArray1[index].ToString());
      }
      if (length2 > 65534)
      {
        Debug.LogError((object) "Cannot add objects. Resulting mesh will have more than 64k vertices. Try using a Multi-MeshBaker component. This will split the combined mesh into several meshes. You don't have to re-configure the MB2_TextureBaker. Just remove the MB2_MeshBaker component and add a MB2_MultiMeshBaker component.");
        return false;
      }
      Vector3[] destinationArray1 = (Vector3[]) null;
      Vector4[] destinationArray2 = (Vector4[]) null;
      Vector2[] destinationArray3 = (Vector2[]) null;
      Vector2[] destinationArray4 = (Vector2[]) null;
      Vector2[] destinationArray5 = (Vector2[]) null;
      Vector2[] destinationArray6 = (Vector2[]) null;
      Color[] destinationArray7 = (Color[]) null;
      MB3_MeshCombinerSingle.MBBlendShape[] destinationArray8 = (MB3_MeshCombinerSingle.MBBlendShape[]) null;
      Vector3[] destinationArray9 = new Vector3[length2];
      if (this._doNorm)
        destinationArray1 = new Vector3[length2];
      if (this._doTan)
        destinationArray2 = new Vector4[length2];
      if (this._doUV)
        destinationArray3 = new Vector2[length2];
      if (this._doUV3)
        destinationArray5 = new Vector2[length2];
      if (this._doUV4)
        destinationArray6 = new Vector2[length2];
      if (this.doUV2())
        destinationArray4 = new Vector2[length2];
      if (this._doCol)
        destinationArray7 = new Color[length2];
      if (this._doBlendShapes)
        destinationArray8 = new MB3_MeshCombinerSingle.MBBlendShape[length4];
      BoneWeight[] boneWeightArray = new BoneWeight[length2];
      Matrix4x4[] nbindPoses = new Matrix4x4[length3];
      Transform[] nbones = new Transform[length3];
      MB3_MeshCombinerSingle.SerializableIntArray[] serializableIntArrayArray = new MB3_MeshCombinerSingle.SerializableIntArray[length1];
      for (int index = 0; index < serializableIntArrayArray.Length; ++index)
        serializableIntArrayArray[index] = new MB3_MeshCombinerSingle.SerializableIntArray(numArray3[index]);
      for (int index = 0; index < array.Length; ++index)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dgo = (MB3_MeshCombinerSingle.MB_DynamicGameObject) null;
        if (this.instance2Combined_MapTryGetValue(array[index], out dgo))
          dgo._beingDeleted = true;
      }
      this.mbDynamicObjectsInCombinedMesh.Sort();
      int destinationIndex1 = 0;
      int destinationIndex2 = 0;
      int[] numArray4 = new int[length1];
      int num4 = 0;
      for (int index6 = 0; index6 < this.mbDynamicObjectsInCombinedMesh.Count; ++index6)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index6];
        if (!dynamicGameObject._beingDeleted)
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Copying obj in combined arrays idx:" + index6.ToString(), (object) this.LOG_LEVEL);
          Array.Copy((Array) this.verts, dynamicGameObject.vertIdx, (Array) destinationArray9, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doNorm)
            Array.Copy((Array) this.normals, dynamicGameObject.vertIdx, (Array) destinationArray1, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doTan)
            Array.Copy((Array) this.tangents, dynamicGameObject.vertIdx, (Array) destinationArray2, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doUV)
            Array.Copy((Array) this.uvs, dynamicGameObject.vertIdx, (Array) destinationArray3, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doUV3)
            Array.Copy((Array) this.uv3s, dynamicGameObject.vertIdx, (Array) destinationArray5, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doUV4)
            Array.Copy((Array) this.uv4s, dynamicGameObject.vertIdx, (Array) destinationArray6, destinationIndex1, dynamicGameObject.numVerts);
          if (this.doUV2())
            Array.Copy((Array) this.uv2s, dynamicGameObject.vertIdx, (Array) destinationArray4, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doCol)
            Array.Copy((Array) this.colors, dynamicGameObject.vertIdx, (Array) destinationArray7, destinationIndex1, dynamicGameObject.numVerts);
          if (this._doBlendShapes)
            Array.Copy((Array) this.blendShapes, dynamicGameObject.blendShapeIdx, (Array) destinationArray8, destinationIndex2, dynamicGameObject.numBlendShapes);
          if (this.renderType == MB_RenderType.skinnedMeshRenderer)
            Array.Copy((Array) this.boneWeights, dynamicGameObject.vertIdx, (Array) boneWeightArray, destinationIndex1, dynamicGameObject.numVerts);
          for (int index7 = 0; index7 < length1; ++index7)
          {
            int[] data = this.submeshTris[index7].data;
            int sourceIndex = dynamicGameObject.submeshTriIdxs[index7];
            int submeshNumTri = dynamicGameObject.submeshNumTris[index7];
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              MB2_Log.LogDebug("    Adjusting submesh triangles submesh:" + index7.ToString() + " startIdx:" + sourceIndex.ToString() + " num:" + submeshNumTri.ToString() + " nsubmeshTris:" + serializableIntArrayArray.Length.ToString() + " targSubmeshTidx:" + numArray4.Length.ToString(), (object) this.LOG_LEVEL);
            for (int index8 = sourceIndex; index8 < sourceIndex + submeshNumTri; ++index8)
              data[index8] = data[index8] - num4;
            Array.Copy((Array) data, sourceIndex, (Array) serializableIntArrayArray[index7].data, numArray4[index7], submeshNumTri);
          }
          dynamicGameObject.vertIdx = destinationIndex1;
          dynamicGameObject.blendShapeIdx = destinationIndex2;
          for (int index9 = 0; index9 < numArray4.Length; ++index9)
          {
            dynamicGameObject.submeshTriIdxs[index9] = numArray4[index9];
            numArray4[index9] += dynamicGameObject.submeshNumTris[index9];
          }
          destinationIndex2 += dynamicGameObject.numBlendShapes;
          destinationIndex1 += dynamicGameObject.numVerts;
        }
        else
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Not copying obj: " + index6.ToString(), (object) this.LOG_LEVEL);
          num4 += dynamicGameObject.numVerts;
        }
      }
      if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        this._CopyBonesWeAreKeepingToNewBonesArrayAndAdjustBWIndexes(intSet, bonesToAdd, nbones, nbindPoses, boneWeightArray, totalDeleteVerts);
      for (int index = this.mbDynamicObjectsInCombinedMesh.Count - 1; index >= 0; --index)
      {
        if (this.mbDynamicObjectsInCombinedMesh[index]._beingDeleted)
        {
          this.instance2Combined_MapRemove(this.mbDynamicObjectsInCombinedMesh[index].instanceID);
          this.objectsInCombinedMesh.RemoveAt(index);
          this.mbDynamicObjectsInCombinedMesh.RemoveAt(index);
        }
      }
      this.verts = destinationArray9;
      if (this._doNorm)
        this.normals = destinationArray1;
      if (this._doTan)
        this.tangents = destinationArray2;
      if (this._doUV)
        this.uvs = destinationArray3;
      if (this._doUV3)
        this.uv3s = destinationArray5;
      if (this._doUV4)
        this.uv4s = destinationArray6;
      if (this.doUV2())
        this.uv2s = destinationArray4;
      if (this._doCol)
        this.colors = destinationArray7;
      if (this._doBlendShapes)
        this.blendShapes = destinationArray8;
      if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        this.boneWeights = boneWeightArray;
      int num5 = this.bones.Length - intSet.Count;
      this.bindPoses = nbindPoses;
      this.bones = nbones;
      this.submeshTris = serializableIntArrayArray;
      int num6 = 0;
      foreach (MB3_MeshCombinerSingle.BoneAndBindpose boneAndBindpose in bonesToAdd)
      {
        nbones[num5 + num6] = boneAndBindpose.bone;
        nbindPoses[num5 + num6] = boneAndBindpose.bindPose;
        ++num6;
      }
      for (int index10 = 0; index10 < dynamicGameObjectList.Count; ++index10)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dgo = dynamicGameObjectList[index10];
        GameObject go = _goToAdd[index10];
        int num7 = destinationIndex1;
        int index11 = destinationIndex2;
        UnityEngine.Mesh mesh = MB_Utility.GetMesh(go);
        Matrix4x4 localToWorldMatrix = go.transform.localToWorldMatrix;
        Matrix4x4 matrix4x4 = localToWorldMatrix;
        matrix4x4[0, 3] = matrix4x4[1, 3] = matrix4x4[2, 3] = 0.0f;
        Vector3[] vertices = meshChannelsCache.GetVertices(mesh);
        Vector3[] vector3Array = (Vector3[]) null;
        Vector4[] vector4Array = (Vector4[]) null;
        if (this._doNorm)
          vector3Array = meshChannelsCache.GetNormals(mesh);
        if (this._doTan)
          vector4Array = meshChannelsCache.GetTangents(mesh);
        if (this.renderType != MB_RenderType.skinnedMeshRenderer)
        {
          for (int index12 = 0; index12 < vertices.Length; ++index12)
          {
            int index13 = num7 + index12;
            this.verts[num7 + index12] = localToWorldMatrix.MultiplyPoint3x4(vertices[index12]);
            if (this._doNorm)
            {
              this.normals[index13] = matrix4x4.MultiplyPoint3x4(vector3Array[index12]);
              this.normals[index13] = this.normals[index13].normalized;
            }
            if (this._doTan)
            {
              float w = vector4Array[index12].w;
              Vector3 vector3 = matrix4x4.MultiplyPoint3x4((Vector3) vector4Array[index12]);
              vector3.Normalize();
              this.tangents[index13] = (Vector4) vector3;
              this.tangents[index13].w = w;
            }
          }
        }
        else
        {
          if (this._doNorm)
            vector3Array.CopyTo((Array) this.normals, num7);
          if (this._doTan)
            vector4Array.CopyTo((Array) this.tangents, num7);
          vertices.CopyTo((Array) this.verts, num7);
        }
        int subMeshCount = mesh.subMeshCount;
        if (dgo.uvRects.Length < subMeshCount)
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Mesh " + dgo.name + " has more submeshes than materials");
          int length5 = dgo.uvRects.Length;
        }
        else if (dgo.uvRects.Length > subMeshCount && this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) ("Mesh " + dgo.name + " has fewer submeshes than materials"));
        if (this._doUV)
          this._copyAndAdjustUVsFromMesh(dgo, mesh, num7, meshChannelsCache);
        if (this.doUV2())
          this._copyAndAdjustUV2FromMesh(dgo, mesh, num7, meshChannelsCache);
        if (this._doUV3)
          meshChannelsCache.GetUv3(mesh).CopyTo((Array) this.uv3s, num7);
        if (this._doUV4)
          meshChannelsCache.GetUv4(mesh).CopyTo((Array) this.uv4s, num7);
        if (this._doCol)
          meshChannelsCache.GetColors(mesh).CopyTo((Array) this.colors, num7);
        if (this._doBlendShapes)
        {
          destinationArray8 = meshChannelsCache.GetBlendShapes(mesh, dgo.instanceID);
          destinationArray8.CopyTo((Array) this.blendShapes, index11);
        }
        if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        {
          Renderer renderer = MB_Utility.GetRenderer(go);
          this._AddBonesToNewBonesArrayAndAdjustBWIndexes(dgo, renderer, num7, nbones, boneWeightArray, meshChannelsCache);
        }
        for (int index14 = 0; index14 < numArray4.Length; ++index14)
          dgo.submeshTriIdxs[index14] = numArray4[index14];
        for (int index15 = 0; index15 < dgo._tmpSubmeshTris.Length; ++index15)
        {
          int[] data = dgo._tmpSubmeshTris[index15].data;
          for (int index16 = 0; index16 < data.Length; ++index16)
            data[index16] = data[index16] + num7;
          if (dgo.invertTriangles)
          {
            for (int index17 = 0; index17 < data.Length; index17 += 3)
            {
              int num8 = data[index17];
              data[index17] = data[index17 + 1];
              data[index17 + 1] = num8;
            }
          }
          int index18 = dgo.targetSubmeshIdxs[index15];
          data.CopyTo((Array) this.submeshTris[index18].data, numArray4[index18]);
          dgo.submeshNumTris[index18] += data.Length;
          numArray4[index18] += data.Length;
        }
        dgo.vertIdx = destinationIndex1;
        dgo.blendShapeIdx = destinationIndex2;
        this.instance2Combined_MapAdd(go.GetInstanceID(), dgo);
        this.objectsInCombinedMesh.Add(go);
        this.mbDynamicObjectsInCombinedMesh.Add(dgo);
        destinationIndex1 += vertices.Length;
        if (this._doBlendShapes)
          destinationIndex2 += destinationArray8.Length;
        for (int index19 = 0; index19 < dgo._tmpSubmeshTris.Length; ++index19)
          dgo._tmpSubmeshTris[index19] = (MB3_MeshCombinerSingle.SerializableIntArray) null;
        dgo._tmpSubmeshTris = (MB3_MeshCombinerSingle.SerializableIntArray[]) null;
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Added to combined:" + dgo.name + " verts:" + vertices.Length.ToString() + " bindPoses:" + nbindPoses.Length.ToString(), (object) this.LOG_LEVEL);
      }
      if (this.lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged_to_separate_rects)
        this._copyUV2unchangedToSeparateRects();
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        MB2_Log.LogDebug("===== _addToCombined completed. Verts in buffer: " + this.verts.Length.ToString(), (object) this.LOG_LEVEL);
      return true;
    }

    private void _copyAndAdjustUVsFromMesh(
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo,
      UnityEngine.Mesh mesh,
      int vertsIdx,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelsCache)
    {
      Vector2[] uv0Raw = meshChannelsCache.GetUv0Raw(mesh);
      bool flag1 = true;
      if (!this._textureBakeResults.DoAnyResultMatsUseConsiderMeshUVs())
      {
        Rect rect = new Rect(0.0f, 0.0f, 1f, 1f);
        bool flag2 = true;
        for (int index = 0; index < this._textureBakeResults.materialsAndUVRects.Length; ++index)
        {
          if (this._textureBakeResults.materialsAndUVRects[index].atlasRect != rect)
          {
            flag2 = false;
            break;
          }
        }
        if (flag2)
        {
          flag1 = false;
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            Debug.Log((object) "All atlases have only one texture in atlas UVs will be copied without adjusting");
        }
      }
      if (flag1)
      {
        int[] numArray1 = new int[uv0Raw.Length];
        for (int index = 0; index < numArray1.Length; ++index)
          numArray1[index] = -1;
        bool flag3 = false;
        for (int submesh = 0; submesh < dgo.targetSubmeshIdxs.Length; ++submesh)
        {
          int[] numArray2 = dgo._tmpSubmeshTris == null ? mesh.GetTriangles(submesh) : dgo._tmpSubmeshTris[submesh].data;
          DRect r2_1 = new DRect(dgo.uvRects[submesh]);
          DRect drect = !this.textureBakeResults.resultMaterials[dgo.targetSubmeshIdxs[submesh]].considerMeshUVs ? new DRect(0.0, 0.0, 1.0, 1.0) : new DRect(dgo.obUVRects[submesh]);
          DRect r2_2 = new DRect(dgo.sourceMaterialTiling[submesh]);
          DRect t = new DRect(dgo.encapsulatingRect[submesh]);
          DRect r2_3 = MB3_UVTransformUtility.InverseTransform(ref t);
          DRect r1_1 = MB3_UVTransformUtility.InverseTransform(ref drect);
          DRect r1_2 = MB3_UVTransformUtility.CombineTransforms(ref drect, ref r2_2);
          DRect r2_4 = MB3_UVTransformUtility.CombineTransforms(ref r1_2, ref r2_3);
          DRect r1_3 = MB3_UVTransformUtility.CombineTransforms(ref r1_1, ref r2_4);
          Rect rect = MB3_UVTransformUtility.CombineTransforms(ref r1_3, ref r2_1).GetRect();
          for (int index1 = 0; index1 < numArray2.Length; ++index1)
          {
            int index2 = numArray2[index1];
            if (numArray1[index2] == -1)
            {
              numArray1[index2] = submesh;
              Vector2 vector2 = uv0Raw[index2];
              vector2.x = rect.x + vector2.x * rect.width;
              vector2.y = rect.y + vector2.y * rect.height;
              this.uvs[vertsIdx + index2] = vector2;
            }
            if (numArray1[index2] != submesh)
              flag3 = true;
          }
        }
        if (flag3 && this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) (dgo.name + "has submeshes which share verticies. Adjusted uvs may not map correctly in combined atlas."));
      }
      else
        uv0Raw.CopyTo((Array) this.uvs, vertsIdx);
      if (this.LOG_LEVEL < MB2_LogLevel.trace)
        return;
      Debug.Log((object) string.Format("_copyAndAdjustUVsFromMesh copied {0} verts", (object) uv0Raw.Length));
    }

    private void _copyAndAdjustUV2FromMesh(
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo,
      UnityEngine.Mesh mesh,
      int vertsIdx,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelsCache)
    {
      Vector2[] uv2 = meshChannelsCache.GetUv2(mesh);
      if (this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
      {
        Vector4 lightmapTilingOffset = dgo.lightmapTilingOffset;
        Vector2 vector2_1 = new Vector2(lightmapTilingOffset.x, lightmapTilingOffset.y);
        Vector2 vector2_2 = new Vector2(lightmapTilingOffset.z, lightmapTilingOffset.w);
        for (int index = 0; index < uv2.Length; ++index)
        {
          Vector2 vector2_3;
          vector2_3.x = vector2_1.x * uv2[index].x;
          vector2_3.y = vector2_1.y * uv2[index].y;
          this.uv2s[vertsIdx + index] = vector2_2 + vector2_3;
        }
        if (this.LOG_LEVEL < MB2_LogLevel.trace)
          return;
        Debug.Log((object) ("_copyAndAdjustUV2FromMesh copied and modify for preserve current lightmapping " + uv2.Length.ToString()));
      }
      else
      {
        uv2.CopyTo((Array) this.uv2s, vertsIdx);
        if (this.LOG_LEVEL < MB2_LogLevel.trace)
          return;
        Debug.Log((object) ("_copyAndAdjustUV2FromMesh copied without modifying " + uv2.Length.ToString()));
      }
    }

    public override void UpdateSkinnedMeshApproximateBounds() => this.UpdateSkinnedMeshApproximateBoundsFromBounds();

    public override void UpdateSkinnedMeshApproximateBoundsFromBones()
    {
      if (this.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
      {
        if (this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
      }
      else if (this.bones.Length == 0)
      {
        if (this.verts.Length == 0 || this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "No bones in SkinnedMeshRenderer. Could not UpdateSkinnedMeshApproximateBounds.");
      }
      else if ((UnityEngine.Object) this._targetRenderer == (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Target Renderer is not set. No point in calling UpdateSkinnedMeshApproximateBounds.");
      }
      else if (!((object) this._targetRenderer).GetType().Equals(typeof (SkinnedMeshRenderer)))
      {
        if (this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Target Renderer is not a SkinnedMeshRenderer. No point in calling UpdateSkinnedMeshApproximateBounds.");
      }
      else
        MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(this.bones, (SkinnedMeshRenderer) this.targetRenderer);
    }

    public override void UpdateSkinnedMeshApproximateBoundsFromBounds()
    {
      if (this.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
      {
        if (this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Can't UpdateSkinnedMeshApproximateBoundsFromBounds when output type is bakeMeshAssetsInPlace");
      }
      else if (this.verts.Length == 0 || this.mbDynamicObjectsInCombinedMesh.Count == 0)
      {
        if (this.verts.Length == 0 || this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Nothing in SkinnedMeshRenderer. Could not UpdateSkinnedMeshApproximateBoundsFromBounds.");
      }
      else if ((UnityEngine.Object) this._targetRenderer == (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Target Renderer is not set. No point in calling UpdateSkinnedMeshApproximateBoundsFromBounds.");
      }
      else if (!((object) this._targetRenderer).GetType().Equals(typeof (SkinnedMeshRenderer)))
      {
        if (this.LOG_LEVEL < MB2_LogLevel.warn)
          return;
        Debug.LogWarning((object) "Target Renderer is not a SkinnedMeshRenderer. No point in calling UpdateSkinnedMeshApproximateBoundsFromBounds.");
      }
      else
        MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(this.objectsInCombinedMesh, (SkinnedMeshRenderer) this.targetRenderer);
    }

    private int _getNumBones(Renderer r)
    {
      if (this.renderType != MB_RenderType.skinnedMeshRenderer)
        return 0;
      switch (r)
      {
        case SkinnedMeshRenderer _:
          return ((SkinnedMeshRenderer) r).bones.Length;
        case MeshRenderer _:
          return 1;
        default:
          Debug.LogError((object) "Could not _getNumBones. Object does not have a renderer");
          return 0;
      }
    }

    private Transform[] _getBones(Renderer r) => MBVersion.GetBones(r);

    public override void Apply(
      MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod)
    {
      bool bones = false;
      if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        bones = true;
      this.Apply(true, true, this._doNorm, this._doTan, this._doUV, this.doUV2(), this._doUV3, this._doUV4, this.doCol, bones, this.doBlendShapes, uv2GenerationMethod);
    }

    public virtual void ApplyShowHide()
    {
      if (this._validationLevel >= MB2_ValidationLevel.quick && !this.ValidateTargRendererAndMeshAndResultSceneObj())
        return;
      if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) null)
      {
        if (this.renderType == MB_RenderType.meshRenderer)
        {
          MBVersion.MeshClear(this._mesh, true);
          this._mesh.vertices = this.verts;
        }
        MB3_MeshCombinerSingle.SerializableIntArray[] withShowHideApplied = this.GetSubmeshTrisWithShowHideApplied();
        if (this.textureBakeResults.doMultiMaterial)
        {
          int numNonZeroLengthSubmeshTris = this._mesh.subMeshCount = this._numNonZeroLengthSubmeshTris(withShowHideApplied);
          int submesh = 0;
          for (int index = 0; index < withShowHideApplied.Length; ++index)
          {
            if (withShowHideApplied[index].data.Length != 0)
            {
              this._mesh.SetTriangles(withShowHideApplied[index].data, submesh);
              ++submesh;
            }
          }
          this._updateMaterialsOnTargetRenderer(withShowHideApplied, numNonZeroLengthSubmeshTris);
        }
        else
          this._mesh.triangles = withShowHideApplied[0].data;
        if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        {
          if (this.verts.Length == 0)
            this.targetRenderer.enabled = false;
          else
            this.targetRenderer.enabled = true;
          bool updateWhenOffscreen = ((SkinnedMeshRenderer) this.targetRenderer).updateWhenOffscreen;
          ((SkinnedMeshRenderer) this.targetRenderer).updateWhenOffscreen = true;
          ((SkinnedMeshRenderer) this.targetRenderer).updateWhenOffscreen = updateWhenOffscreen;
        }
        if (this.LOG_LEVEL < MB2_LogLevel.trace)
          return;
        Debug.Log((object) nameof (ApplyShowHide));
      }
      else
        Debug.LogError((object) "Need to add objects to this meshbaker before calling ApplyShowHide");
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
      if (this._validationLevel >= MB2_ValidationLevel.quick && !this.ValidateTargRendererAndMeshAndResultSceneObj())
        return;
      if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.trace)
          Debug.Log((object) string.Format("Apply called tri={0} vert={1} norm={2} tan={3} uv={4} col={5} uv3={6} uv4={7} uv2={8} bone={9} blendShape{10} meshID={11}", (object) triangles, (object) vertices, (object) normals, (object) tangents, (object) uvs, (object) colors, (object) uv3, (object) uv4, (object) uv2, (object) bones, (object) this.blendShapes, (object) this._mesh.GetInstanceID()));
        if (triangles || this._mesh.vertexCount != this.verts.Length)
        {
          if (triangles && !vertices && !normals && !tangents && !uvs && !colors && !uv3 && !uv4 && !uv2 && !bones)
            MBVersion.MeshClear(this._mesh, true);
          else
            MBVersion.MeshClear(this._mesh, false);
        }
        if (vertices)
        {
          Vector3[] vector3Array = this.verts;
          if (this.verts.Length != 0)
          {
            if (this._recenterVertsToBoundsCenter && this._renderType == MB_RenderType.meshRenderer)
            {
              vector3Array = new Vector3[this.verts.Length];
              Vector3 vert1 = this.verts[0];
              Vector3 vert2 = this.verts[0];
              for (int index = 1; index < this.verts.Length; ++index)
              {
                Vector3 vert3 = this.verts[index];
                if ((double) vert1.x < (double) vert3.x)
                  vert1.x = vert3.x;
                if ((double) vert1.y < (double) vert3.y)
                  vert1.y = vert3.y;
                if ((double) vert1.z < (double) vert3.z)
                  vert1.z = vert3.z;
                if ((double) vert2.x > (double) vert3.x)
                  vert2.x = vert3.x;
                if ((double) vert2.y > (double) vert3.y)
                  vert2.y = vert3.y;
                if ((double) vert2.z > (double) vert3.z)
                  vert2.z = vert3.z;
              }
              Vector3 vector3 = (vert1 + vert2) / 2f;
              for (int index = 0; index < this.verts.Length; ++index)
                vector3Array[index] = this.verts[index] - vector3;
              this.targetRenderer.transform.position = vector3;
            }
            else
              this.targetRenderer.transform.position = Vector3.zero;
          }
          this._mesh.vertices = vector3Array;
        }
        if (triangles && (bool) (UnityEngine.Object) this._textureBakeResults)
        {
          if ((UnityEngine.Object) this._textureBakeResults == (UnityEngine.Object) null)
          {
            Debug.LogError((object) "Texture Bake Result was not set.");
          }
          else
          {
            MB3_MeshCombinerSingle.SerializableIntArray[] withShowHideApplied = this.GetSubmeshTrisWithShowHideApplied();
            int numNonZeroLengthSubmeshTris = this._mesh.subMeshCount = this._numNonZeroLengthSubmeshTris(withShowHideApplied);
            int submesh = 0;
            for (int index = 0; index < withShowHideApplied.Length; ++index)
            {
              if (withShowHideApplied[index].data.Length != 0)
              {
                this._mesh.SetTriangles(withShowHideApplied[index].data, submesh);
                ++submesh;
              }
            }
            this._updateMaterialsOnTargetRenderer(withShowHideApplied, numNonZeroLengthSubmeshTris);
          }
        }
        if (normals)
        {
          if (this._doNorm)
            this._mesh.normals = this.normals;
          else
            Debug.LogError((object) "normal flag was set in Apply but MeshBaker didn't generate normals");
        }
        if (tangents)
        {
          if (this._doTan)
            this._mesh.tangents = this.tangents;
          else
            Debug.LogError((object) "tangent flag was set in Apply but MeshBaker didn't generate tangents");
        }
        if (uvs)
        {
          if (this._doUV)
            this._mesh.uv = this.uvs;
          else
            Debug.LogError((object) "uv flag was set in Apply but MeshBaker didn't generate uvs");
        }
        if (colors)
        {
          if (this._doCol)
            this._mesh.colors = this.colors;
          else
            Debug.LogError((object) "color flag was set in Apply but MeshBaker didn't generate colors");
        }
        if (uv3)
        {
          if (this._doUV3)
            MBVersion.MeshAssignUV3(this._mesh, this.uv3s);
          else
            Debug.LogError((object) "uv3 flag was set in Apply but MeshBaker didn't generate uv3s");
        }
        if (uv4)
        {
          if (this._doUV4)
            MBVersion.MeshAssignUV4(this._mesh, this.uv4s);
          else
            Debug.LogError((object) "uv4 flag was set in Apply but MeshBaker didn't generate uv4s");
        }
        if (uv2)
        {
          if (this.doUV2())
            this._mesh.uv2 = this.uv2s;
          else
            Debug.LogError((object) ("uv2 flag was set in Apply but lightmapping option was set to " + this.lightmapOption.ToString()));
        }
        bool flag = false;
        if (this.renderType != MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout)
        {
          if (uv2GenerationMethod != null)
          {
            uv2GenerationMethod(this._mesh, this.uv2UnwrappingParamsHardAngle, this.uv2UnwrappingParamsPackMargin);
            if (this.LOG_LEVEL >= MB2_LogLevel.trace)
              Debug.Log((object) "generating new UV2 layout for the combined mesh ");
          }
          else
            Debug.LogError((object) "No GenerateUV2Delegate method was supplied. UV2 cannot be generated.");
          flag = true;
        }
        else if (this.renderType == MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout && this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) "UV2 cannot be generated for SkinnedMeshRenderer objects.");
        if (this.renderType != MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout && !flag)
          Debug.LogError((object) "Failed to generate new UV2 layout. Only works in editor.");
        if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        {
          if (this.verts.Length == 0)
            this.targetRenderer.enabled = false;
          else
            this.targetRenderer.enabled = true;
          bool updateWhenOffscreen = ((SkinnedMeshRenderer) this.targetRenderer).updateWhenOffscreen;
          ((SkinnedMeshRenderer) this.targetRenderer).updateWhenOffscreen = true;
          ((SkinnedMeshRenderer) this.targetRenderer).updateWhenOffscreen = updateWhenOffscreen;
        }
        if (bones)
        {
          this._mesh.bindposes = this.bindPoses;
          this._mesh.boneWeights = this.boneWeights;
        }
        if (blendShapesFlag && (MBVersion.GetMajorVersion() > 5 || MBVersion.GetMajorVersion() == 5 && MBVersion.GetMinorVersion() >= 3))
        {
          if (this.blendShapesInCombined.Length != this.blendShapes.Length)
            this.blendShapesInCombined = new MB3_MeshCombinerSingle.MBBlendShape[this.blendShapes.Length];
          Vector3[] vector3Array1 = new Vector3[this.verts.Length];
          Vector3[] vector3Array2 = new Vector3[this.verts.Length];
          Vector3[] vector3Array3 = new Vector3[this.verts.Length];
          MBVersion.ClearBlendShapes(this._mesh);
          for (int index1 = 0; index1 < this.blendShapes.Length; ++index1)
          {
            MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.instance2Combined_MapGet(this.blendShapes[index1].gameObjectID);
            if (dynamicGameObject != null)
            {
              for (int index2 = 0; index2 < this.blendShapes[index1].frames.Length; ++index2)
              {
                MB3_MeshCombinerSingle.MBBlendShapeFrame frame = this.blendShapes[index1].frames[index2];
                int vertIdx = dynamicGameObject.vertIdx;
                Array.Copy((Array) frame.vertices, 0, (Array) vector3Array1, vertIdx, this.blendShapes[index1].frames[index2].vertices.Length);
                Array.Copy((Array) frame.normals, 0, (Array) vector3Array2, vertIdx, this.blendShapes[index1].frames[index2].normals.Length);
                Array.Copy((Array) frame.tangents, 0, (Array) vector3Array3, vertIdx, this.blendShapes[index1].frames[index2].tangents.Length);
                MBVersion.AddBlendShapeFrame(this._mesh, this.blendShapes[index1].name + this.blendShapes[index1].gameObjectID.ToString(), frame.frameWeight, vector3Array1, vector3Array2, vector3Array3);
                this._ZeroArray(vector3Array1, vertIdx, this.blendShapes[index1].frames[index2].vertices.Length);
                this._ZeroArray(vector3Array2, vertIdx, this.blendShapes[index1].frames[index2].normals.Length);
                this._ZeroArray(vector3Array3, vertIdx, this.blendShapes[index1].frames[index2].tangents.Length);
              }
            }
            else
              Debug.LogError((object) "InstanceID in blend shape that was not in instance2combinedMap");
            this.blendShapesInCombined[index1] = this.blendShapes[index1];
          }
          ((SkinnedMeshRenderer) this._targetRenderer).sharedMesh = (UnityEngine.Mesh) null;
          ((SkinnedMeshRenderer) this._targetRenderer).sharedMesh = this._mesh;
        }
        if (triangles | vertices)
        {
          if (this.LOG_LEVEL >= MB2_LogLevel.trace)
            Debug.Log((object) "recalculating bounds on mesh.");
          this._mesh.RecalculateBounds();
        }
        if (!this._optimizeAfterBake || Application.isPlaying)
          return;
        MBVersion.OptimizeMesh(this._mesh);
      }
      else
        Debug.LogError((object) "Need to add objects to this meshbaker before calling Apply or ApplyAll");
    }

    private int _numNonZeroLengthSubmeshTris(
      MB3_MeshCombinerSingle.SerializableIntArray[] subTris)
    {
      int num = 0;
      for (int index = 0; index < subTris.Length; ++index)
      {
        if (subTris[index].data.Length != 0)
          ++num;
      }
      return num;
    }

    private void _updateMaterialsOnTargetRenderer(
      MB3_MeshCombinerSingle.SerializableIntArray[] subTris,
      int numNonZeroLengthSubmeshTris)
    {
      if (subTris.Length != this.textureBakeResults.resultMaterials.Length)
        Debug.LogError((object) "Mismatch between number of submeshes and number of result materials");
      Material[] materialArray = new Material[numNonZeroLengthSubmeshTris];
      int index1 = 0;
      for (int index2 = 0; index2 < subTris.Length; ++index2)
      {
        if (subTris[index2].data.Length != 0)
        {
          materialArray[index1] = this._textureBakeResults.resultMaterials[index2].combinedMaterial;
          ++index1;
        }
      }
      this.targetRenderer.materials = materialArray;
    }

    public MB3_MeshCombinerSingle.SerializableIntArray[] GetSubmeshTrisWithShowHideApplied()
    {
      bool flag = false;
      for (int index = 0; index < this.mbDynamicObjectsInCombinedMesh.Count; ++index)
      {
        if (!this.mbDynamicObjectsInCombinedMesh[index].show)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return this.submeshTris;
      int[] numArray1 = new int[this.submeshTris.Length];
      MB3_MeshCombinerSingle.SerializableIntArray[] withShowHideApplied = new MB3_MeshCombinerSingle.SerializableIntArray[this.submeshTris.Length];
      for (int index1 = 0; index1 < this.mbDynamicObjectsInCombinedMesh.Count; ++index1)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index1];
        if (dynamicGameObject.show)
        {
          for (int index2 = 0; index2 < dynamicGameObject.submeshNumTris.Length; ++index2)
            numArray1[index2] += dynamicGameObject.submeshNumTris[index2];
        }
      }
      for (int index = 0; index < withShowHideApplied.Length; ++index)
        withShowHideApplied[index] = new MB3_MeshCombinerSingle.SerializableIntArray(numArray1[index]);
      int[] numArray2 = new int[withShowHideApplied.Length];
      for (int index3 = 0; index3 < this.mbDynamicObjectsInCombinedMesh.Count; ++index3)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index3];
        if (dynamicGameObject.show)
        {
          for (int index4 = 0; index4 < this.submeshTris.Length; ++index4)
          {
            int[] data = this.submeshTris[index4].data;
            int num1 = dynamicGameObject.submeshTriIdxs[index4];
            int num2 = num1 + dynamicGameObject.submeshNumTris[index4];
            for (int index5 = num1; index5 < num2; ++index5)
            {
              withShowHideApplied[index4].data[numArray2[index4]] = data[index5];
              numArray2[index4] = numArray2[index4] + 1;
            }
          }
        }
      }
      return withShowHideApplied;
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
      this._updateGameObjects(gos, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV2, updateUV3, updateUV4, updateColors, updateSkinningInfo);
    }

    private void _updateGameObjects(
      GameObject[] gos,
      bool recalcBounds,
      bool updateVertices,
      bool updateNormals,
      bool updateTangents,
      bool updateUV,
      bool updateUV2,
      bool updateUV3,
      bool updateUV4,
      bool updateColors,
      bool updateSkinningInfo)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) ("UpdateGameObjects called on " + gos.Length.ToString() + " objects."));
      int numResultMats = 1;
      if (this.textureBakeResults.doMultiMaterial)
        numResultMats = this.textureBakeResults.resultMaterials.Length;
      this._initialize(numResultMats);
      if (this._mesh.vertexCount > 0 && this._instance2combined_map.Count == 0)
        Debug.LogWarning((object) "There were vertices in the combined mesh but nothing in the MeshBaker buffers. If you are trying to bake in the editor and modify at runtime, make sure 'Clear Buffers After Bake' is unchecked.");
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache = new MB3_MeshCombinerSingle.MeshChannelsCache(this);
      for (int index = 0; index < gos.Length; ++index)
        this._updateGameObject(gos[index], updateVertices, updateNormals, updateTangents, updateUV, updateUV2, updateUV3, updateUV4, updateColors, updateSkinningInfo, meshChannelCache);
      if (!recalcBounds)
        return;
      this._mesh.RecalculateBounds();
    }

    private void _updateGameObject(
      GameObject go,
      bool updateVertices,
      bool updateNormals,
      bool updateTangents,
      bool updateUV,
      bool updateUV2,
      bool updateUV3,
      bool updateUV4,
      bool updateColors,
      bool updateSkinningInfo,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
    {
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo = (MB3_MeshCombinerSingle.MB_DynamicGameObject) null;
      if (!this.instance2Combined_MapTryGetValue(go.GetInstanceID(), out dgo))
      {
        Debug.LogError((object) ("Object " + go.name + " has not been added"));
      }
      else
      {
        UnityEngine.Mesh mesh = MB_Utility.GetMesh(go);
        if (dgo.numVerts != mesh.vertexCount)
        {
          Debug.LogError((object) ("Object " + go.name + " source mesh has been modified since being added. To update it must have the same number of verts"));
        }
        else
        {
          if (this._doUV & updateUV)
            this._copyAndAdjustUVsFromMesh(dgo, mesh, dgo.vertIdx, meshChannelCache);
          if (this.doUV2() & updateUV2)
            this._copyAndAdjustUV2FromMesh(dgo, mesh, dgo.vertIdx, meshChannelCache);
          if (this.renderType == MB_RenderType.skinnedMeshRenderer & updateSkinningInfo)
          {
            Renderer renderer = MB_Utility.GetRenderer(go);
            BoneWeight[] boneWeights = meshChannelCache.GetBoneWeights(renderer, dgo.numVerts);
            Transform[] bones = this._getBones(renderer);
            int vertIdx = dgo.vertIdx;
            bool flag = false;
            for (int index = 0; index < boneWeights.Length; ++index)
            {
              if ((UnityEngine.Object) bones[boneWeights[index].boneIndex0] != (UnityEngine.Object) this.bones[this.boneWeights[vertIdx].boneIndex0])
              {
                flag = true;
                break;
              }
              this.boneWeights[vertIdx].weight0 = boneWeights[index].weight0;
              this.boneWeights[vertIdx].weight1 = boneWeights[index].weight1;
              this.boneWeights[vertIdx].weight2 = boneWeights[index].weight2;
              this.boneWeights[vertIdx].weight3 = boneWeights[index].weight3;
              ++vertIdx;
            }
            if (flag)
              Debug.LogError((object) ("Detected that some of the boneweights reference different bones than when initial added. Boneweights must reference the same bones " + dgo.name));
          }
          Matrix4x4 localToWorldMatrix = go.transform.localToWorldMatrix;
          if (updateVertices)
          {
            Vector3[] vertices = meshChannelCache.GetVertices(mesh);
            for (int index = 0; index < vertices.Length; ++index)
              this.verts[dgo.vertIdx + index] = localToWorldMatrix.MultiplyPoint3x4(vertices[index]);
          }
          localToWorldMatrix[0, 3] = localToWorldMatrix[1, 3] = localToWorldMatrix[2, 3] = 0.0f;
          if (this._doNorm & updateNormals)
          {
            Vector3[] normals = meshChannelCache.GetNormals(mesh);
            for (int index1 = 0; index1 < normals.Length; ++index1)
            {
              int index2 = dgo.vertIdx + index1;
              this.normals[index2] = localToWorldMatrix.MultiplyPoint3x4(normals[index1]);
              this.normals[index2] = this.normals[index2].normalized;
            }
          }
          if (this._doTan & updateTangents)
          {
            Vector4[] tangents = meshChannelCache.GetTangents(mesh);
            for (int index3 = 0; index3 < tangents.Length; ++index3)
            {
              int index4 = dgo.vertIdx + index3;
              float w = tangents[index3].w;
              Vector3 vector3 = localToWorldMatrix.MultiplyPoint3x4((Vector3) tangents[index3]);
              vector3.Normalize();
              this.tangents[index4] = (Vector4) vector3;
              this.tangents[index4].w = w;
            }
          }
          if (this._doCol & updateColors)
          {
            Color[] colors = meshChannelCache.GetColors(mesh);
            for (int index = 0; index < colors.Length; ++index)
              this.colors[dgo.vertIdx + index] = colors[index];
          }
          if (this._doUV3 & updateUV3)
          {
            Vector2[] uv3 = meshChannelCache.GetUv3(mesh);
            for (int index = 0; index < uv3.Length; ++index)
              this.uv3s[dgo.vertIdx + index] = uv3[index];
          }
          if (!(this._doUV4 & updateUV4))
            return;
          Vector2[] uv4 = meshChannelCache.GetUv4(mesh);
          for (int index = 0; index < uv4.Length; ++index)
            this.uv4s[dgo.vertIdx + index] = uv4[index];
        }
      }
    }

    public bool ShowHideGameObjects(GameObject[] toShow, GameObject[] toHide)
    {
      if (!((UnityEngine.Object) this.textureBakeResults == (UnityEngine.Object) null))
        return this._showHide(toShow, toHide);
      Debug.LogError((object) "TextureBakeResults must be set.");
      return false;
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
      bool disableRendererInSource)
    {
      if (this.validationLevel > MB2_ValidationLevel.none)
      {
        if (gos != null)
        {
          for (int index1 = 0; index1 < gos.Length; ++index1)
          {
            if ((UnityEngine.Object) gos[index1] == (UnityEngine.Object) null)
            {
              Debug.LogError((object) ("The " + index1.ToString() + "th object on the list of objects to combine is 'None'. Use Command-Delete on Mac OS X; Delete or Shift-Delete on Windows to remove this one element."));
              return false;
            }
            if (this.validationLevel >= MB2_ValidationLevel.robust)
            {
              for (int index2 = index1 + 1; index2 < gos.Length; ++index2)
              {
                if ((UnityEngine.Object) gos[index1] == (UnityEngine.Object) gos[index2])
                {
                  Debug.LogError((object) ("GameObject " + ((object) gos[index1])?.ToString() + " appears twice in list of game objects to add"));
                  return false;
                }
              }
            }
          }
        }
        if (deleteGOinstanceIDs != null && this.validationLevel >= MB2_ValidationLevel.robust)
        {
          for (int index3 = 0; index3 < deleteGOinstanceIDs.Length; ++index3)
          {
            for (int index4 = index3 + 1; index4 < deleteGOinstanceIDs.Length; ++index4)
            {
              if (deleteGOinstanceIDs[index3] == deleteGOinstanceIDs[index4])
              {
                Debug.LogError((object) ("GameObject " + deleteGOinstanceIDs[index3].ToString() + "appears twice in list of game objects to delete"));
                return false;
              }
            }
          }
        }
      }
      if (this._usingTemporaryTextureBakeResult && gos != null && gos.Length != 0)
      {
        MB_Utility.Destroy((UnityEngine.Object) this._textureBakeResults);
        this._textureBakeResults = (MB2_TextureBakeResults) null;
        this._usingTemporaryTextureBakeResult = false;
      }
      if ((UnityEngine.Object) this._textureBakeResults == (UnityEngine.Object) null && gos != null && gos.Length != 0 && (UnityEngine.Object) gos[0] != (UnityEngine.Object) null && !this._CreateTemporaryTextrueBakeResult(gos, this.GetMaterialsOnTargetRenderer()))
        return false;
      this.BuildSceneMeshObject(gos);
      if (!this._addToCombined(gos, deleteGOinstanceIDs, disableRendererInSource))
      {
        Debug.LogError((object) "Failed to add/delete objects to combined mesh");
        return false;
      }
      if ((UnityEngine.Object) this.targetRenderer != (UnityEngine.Object) null)
      {
        if (this.renderType == MB_RenderType.skinnedMeshRenderer)
        {
          ((SkinnedMeshRenderer) this.targetRenderer).bones = this.bones;
          this.UpdateSkinnedMeshApproximateBoundsFromBounds();
        }
        this.targetRenderer.lightmapIndex = this.GetLightmapIndex();
      }
      return true;
    }

    public override bool CombinedMeshContains(GameObject go) => this.objectsInCombinedMesh.Contains(go);

    public override void ClearBuffers()
    {
      this.verts = new Vector3[0];
      this.normals = new Vector3[0];
      this.tangents = new Vector4[0];
      this.uvs = new Vector2[0];
      this.uv2s = new Vector2[0];
      this.uv3s = new Vector2[0];
      this.uv4s = new Vector2[0];
      this.colors = new Color[0];
      this.bones = new Transform[0];
      this.bindPoses = new Matrix4x4[0];
      this.boneWeights = new BoneWeight[0];
      this.submeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[0];
      this.blendShapes = new MB3_MeshCombinerSingle.MBBlendShape[0];
      if (this.blendShapesInCombined == null)
      {
        this.blendShapesInCombined = new MB3_MeshCombinerSingle.MBBlendShape[0];
      }
      else
      {
        for (int index = 0; index < this.blendShapesInCombined.Length; ++index)
          this.blendShapesInCombined[index].frames = new MB3_MeshCombinerSingle.MBBlendShapeFrame[0];
      }
      this.mbDynamicObjectsInCombinedMesh.Clear();
      this.objectsInCombinedMesh.Clear();
      this.instance2Combined_MapClear();
      if (this._usingTemporaryTextureBakeResult)
      {
        MB_Utility.Destroy((UnityEngine.Object) this._textureBakeResults);
        this._textureBakeResults = (MB2_TextureBakeResults) null;
        this._usingTemporaryTextureBakeResult = false;
      }
      if (this.LOG_LEVEL < MB2_LogLevel.trace)
        return;
      MB2_Log.LogDebug("ClearBuffers called");
    }

    public override void ClearMesh()
    {
      if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) null)
        MBVersion.MeshClear(this._mesh, false);
      else
        this._mesh = new UnityEngine.Mesh();
      this.ClearBuffers();
    }

    public override void DestroyMesh()
    {
      if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Destroying Mesh");
        MB_Utility.Destroy((UnityEngine.Object) this._mesh);
      }
      this._mesh = new UnityEngine.Mesh();
      this.ClearBuffers();
    }

    public override void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
    {
      if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) null)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Destroying Mesh");
        editorMethods.Destroy((UnityEngine.Object) this._mesh);
      }
      this._mesh = new UnityEngine.Mesh();
      this.ClearBuffers();
    }

    public bool ValidateTargRendererAndMeshAndResultSceneObj()
    {
      if ((UnityEngine.Object) this._resultSceneObject == (UnityEngine.Object) null)
      {
        if (this._LOG_LEVEL >= MB2_LogLevel.error)
          Debug.LogError((object) "Result Scene Object was not set.");
        return false;
      }
      if ((UnityEngine.Object) this._targetRenderer == (UnityEngine.Object) null)
      {
        if (this._LOG_LEVEL >= MB2_LogLevel.error)
          Debug.LogError((object) "Target Renderer was not set.");
        return false;
      }
      if ((UnityEngine.Object) this._targetRenderer.transform.parent != (UnityEngine.Object) this._resultSceneObject.transform)
      {
        if (this._LOG_LEVEL >= MB2_LogLevel.error)
          Debug.LogError((object) "Target Renderer game object is not a child of Result Scene Object was not set.");
        return false;
      }
      if (this._renderType == MB_RenderType.skinnedMeshRenderer)
      {
        if (!(this._targetRenderer is SkinnedMeshRenderer))
        {
          if (this._LOG_LEVEL >= MB2_LogLevel.error)
            Debug.LogError((object) "Render Type is skinned mesh renderer but Target Renderer is not.");
          return false;
        }
        if ((UnityEngine.Object) ((SkinnedMeshRenderer) this._targetRenderer).sharedMesh != (UnityEngine.Object) this._mesh)
        {
          if (this._LOG_LEVEL >= MB2_LogLevel.error)
            Debug.LogError((object) "Target renderer mesh is not equal to mesh.");
          return false;
        }
      }
      if (this._renderType == MB_RenderType.meshRenderer)
      {
        if (!(this._targetRenderer is MeshRenderer))
        {
          if (this._LOG_LEVEL >= MB2_LogLevel.error)
            Debug.LogError((object) "Render Type is mesh renderer but Target Renderer is not.");
          return false;
        }
        if ((UnityEngine.Object) this._mesh != (UnityEngine.Object) this._targetRenderer.GetComponent<MeshFilter>().sharedMesh)
        {
          if (this._LOG_LEVEL >= MB2_LogLevel.error)
            Debug.LogError((object) "Target renderer mesh is not equal to mesh.");
          return false;
        }
      }
      return true;
    }

    internal static Renderer BuildSceneHierarchPreBake(
      MB3_MeshCombinerSingle mom,
      GameObject root,
      UnityEngine.Mesh m,
      bool createNewChild = false,
      GameObject[] objsToBeAdded = null)
    {
      if (mom._LOG_LEVEL >= MB2_LogLevel.trace)
        Debug.Log((object) ("Building Scene Hierarchy createNewChild=" + createNewChild.ToString()));
      MeshFilter mf = (MeshFilter) null;
      MeshRenderer mr = (MeshRenderer) null;
      SkinnedMeshRenderer smr = (SkinnedMeshRenderer) null;
      Transform transform = (Transform) null;
      if ((UnityEngine.Object) root == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "root was null.");
        return (Renderer) null;
      }
      if ((UnityEngine.Object) mom.textureBakeResults == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "textureBakeResults must be set.");
        return (Renderer) null;
      }
      if ((UnityEngine.Object) root.GetComponent<Renderer>() != (UnityEngine.Object) null)
      {
        Debug.LogError((object) "root game object cannot have a renderer component");
        return (Renderer) null;
      }
      if (!createNewChild)
      {
        if ((UnityEngine.Object) mom.targetRenderer != (UnityEngine.Object) null && (UnityEngine.Object) mom.targetRenderer.transform.parent == (UnityEngine.Object) root.transform)
        {
          transform = mom.targetRenderer.transform;
        }
        else
        {
          Renderer[] componentsInChildren = root.GetComponentsInChildren<Renderer>();
          if (componentsInChildren.Length == 1)
          {
            if ((UnityEngine.Object) componentsInChildren[0].transform.parent != (UnityEngine.Object) root.transform)
              Debug.LogError((object) "Target Renderer is not an immediate child of Result Scene Object. Try using a game object with no children as the Result Scene Object..");
            transform = componentsInChildren[0].transform;
          }
        }
      }
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null && (UnityEngine.Object) transform.parent != (UnityEngine.Object) root.transform)
        transform = (Transform) null;
      if ((UnityEngine.Object) transform == (UnityEngine.Object) null)
        transform = new GameObject(mom.name + "-mesh")
        {
          transform = {
            parent = root.transform
          }
        }.transform;
      transform.parent = root.transform;
      GameObject gameObject = transform.gameObject;
      if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        MeshRenderer component1 = gameObject.GetComponent<MeshRenderer>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) component1);
        MeshFilter component2 = gameObject.GetComponent<MeshFilter>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) component2);
        smr = gameObject.GetComponent<SkinnedMeshRenderer>();
        if ((UnityEngine.Object) smr == (UnityEngine.Object) null)
          smr = gameObject.AddComponent<SkinnedMeshRenderer>();
      }
      else
      {
        SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) component);
        mf = gameObject.GetComponent<MeshFilter>();
        if ((UnityEngine.Object) mf == (UnityEngine.Object) null)
          mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.GetComponent<MeshRenderer>();
        if ((UnityEngine.Object) mr == (UnityEngine.Object) null)
          mr = gameObject.AddComponent<MeshRenderer>();
      }
      if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        smr.bones = mom.GetBones();
        bool updateWhenOffscreen = smr.updateWhenOffscreen;
        smr.updateWhenOffscreen = true;
        smr.updateWhenOffscreen = updateWhenOffscreen;
      }
      MB3_MeshCombinerSingle._ConfigureSceneHierarch(mom, root, mr, mf, smr, m, objsToBeAdded);
      return mom.renderType == MB_RenderType.skinnedMeshRenderer ? (Renderer) smr : (Renderer) mr;
    }

    public static void BuildPrefabHierarchy(
      MB3_MeshCombinerSingle mom,
      GameObject instantiatedPrefabRoot,
      UnityEngine.Mesh m,
      bool createNewChild = false,
      GameObject[] objsToBeAdded = null)
    {
      SkinnedMeshRenderer smr = (SkinnedMeshRenderer) null;
      MeshRenderer mr = (MeshRenderer) null;
      MeshFilter mf = (MeshFilter) null;
      Transform transform = new GameObject(mom.name + "-mesh")
      {
        transform = {
          parent = instantiatedPrefabRoot.transform
        }
      }.transform;
      transform.parent = instantiatedPrefabRoot.transform;
      GameObject gameObject = transform.gameObject;
      if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        MeshRenderer component1 = gameObject.GetComponent<MeshRenderer>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) component1);
        MeshFilter component2 = gameObject.GetComponent<MeshFilter>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) component2);
        smr = gameObject.GetComponent<SkinnedMeshRenderer>();
        if ((UnityEngine.Object) smr == (UnityEngine.Object) null)
          smr = gameObject.AddComponent<SkinnedMeshRenderer>();
      }
      else
      {
        SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          MB_Utility.Destroy((UnityEngine.Object) component);
        mf = gameObject.GetComponent<MeshFilter>();
        if ((UnityEngine.Object) mf == (UnityEngine.Object) null)
          mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.GetComponent<MeshRenderer>();
        if ((UnityEngine.Object) mr == (UnityEngine.Object) null)
          mr = gameObject.AddComponent<MeshRenderer>();
      }
      if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        smr.bones = mom.GetBones();
        bool updateWhenOffscreen = smr.updateWhenOffscreen;
        smr.updateWhenOffscreen = true;
        smr.updateWhenOffscreen = updateWhenOffscreen;
      }
      MB3_MeshCombinerSingle._ConfigureSceneHierarch(mom, instantiatedPrefabRoot, mr, mf, smr, m, objsToBeAdded);
      if (!((UnityEngine.Object) mom.targetRenderer != (UnityEngine.Object) null))
        return;
      Material[] materialArray = new Material[mom.targetRenderer.sharedMaterials.Length];
      for (int index = 0; index < materialArray.Length; ++index)
        materialArray[index] = mom.targetRenderer.sharedMaterials[index];
      if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        smr.sharedMaterial = (Material) null;
        smr.sharedMaterials = materialArray;
      }
      else
      {
        mr.sharedMaterial = (Material) null;
        mr.sharedMaterials = materialArray;
      }
    }

    private static void _ConfigureSceneHierarch(
      MB3_MeshCombinerSingle mom,
      GameObject root,
      MeshRenderer mr,
      MeshFilter mf,
      SkinnedMeshRenderer smr,
      UnityEngine.Mesh m,
      GameObject[] objsToBeAdded = null)
    {
      GameObject gameObject;
      if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        gameObject = smr.gameObject;
        smr.sharedMesh = m;
        smr.lightmapIndex = mom.GetLightmapIndex();
      }
      else
      {
        gameObject = mr.gameObject;
        mf.sharedMesh = m;
        mr.lightmapIndex = mom.GetLightmapIndex();
      }
      if (mom.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping || mom.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout)
        gameObject.isStatic = true;
      if (objsToBeAdded == null || objsToBeAdded.Length == 0 || !((UnityEngine.Object) objsToBeAdded[0] != (UnityEngine.Object) null))
        return;
      bool flag1 = true;
      bool flag2 = true;
      string tag = objsToBeAdded[0].tag;
      int layer = objsToBeAdded[0].layer;
      for (int index = 0; index < objsToBeAdded.Length; ++index)
      {
        if ((UnityEngine.Object) objsToBeAdded[index] != (UnityEngine.Object) null)
        {
          if (!objsToBeAdded[index].tag.Equals(tag))
            flag1 = false;
          if (objsToBeAdded[index].layer != layer)
            flag2 = false;
        }
      }
      if (flag1)
      {
        root.tag = tag;
        gameObject.tag = tag;
      }
      if (!flag2)
        return;
      root.layer = layer;
      gameObject.layer = layer;
    }

    public void BuildSceneMeshObject(GameObject[] gos = null, bool createNewChild = false)
    {
      if ((UnityEngine.Object) this._resultSceneObject == (UnityEngine.Object) null)
        this._resultSceneObject = new GameObject("CombinedMesh-" + this.name);
      this._targetRenderer = MB3_MeshCombinerSingle.BuildSceneHierarchPreBake(this, this._resultSceneObject, this.GetMesh(), createNewChild, gos);
    }

    private bool IsMirrored(Matrix4x4 tm)
    {
      Vector3 row1 = (Vector3) tm.GetRow(0);
      Vector3 row2 = (Vector3) tm.GetRow(1);
      Vector3 row3 = (Vector3) tm.GetRow(2);
      row1.Normalize();
      row2.Normalize();
      row3.Normalize();
      return (double) Vector3.Dot(Vector3.Cross(row1, row2), row3) < 0.0;
    }

    public override void CheckIntegrity()
    {
      if (!MB3_MeshBakerRoot.DO_INTEGRITY_CHECKS)
        return;
      if (this.renderType == MB_RenderType.skinnedMeshRenderer)
      {
        for (int index1 = 0; index1 < this.mbDynamicObjectsInCombinedMesh.Count; ++index1)
        {
          MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index1];
          HashSet<int> other = new HashSet<int>();
          HashSet<int> intSet = new HashSet<int>();
          for (int vertIdx = dynamicGameObject.vertIdx; vertIdx < dynamicGameObject.vertIdx + dynamicGameObject.numVerts; ++vertIdx)
          {
            other.Add(this.boneWeights[vertIdx].boneIndex0);
            other.Add(this.boneWeights[vertIdx].boneIndex1);
            other.Add(this.boneWeights[vertIdx].boneIndex2);
            other.Add(this.boneWeights[vertIdx].boneIndex3);
          }
          for (int index2 = 0; index2 < dynamicGameObject.indexesOfBonesUsed.Length; ++index2)
            intSet.Add(dynamicGameObject.indexesOfBonesUsed[index2]);
          intSet.ExceptWith((IEnumerable<int>) other);
          if (intSet.Count > 0)
          {
            int count = other.Count;
            string str1 = count.ToString();
            count = intSet.Count;
            string str2 = count.ToString();
            Debug.LogError((object) ("The bone indexes were not the same. " + str1 + " " + str2));
          }
          for (int index3 = 0; index3 < dynamicGameObject.indexesOfBonesUsed.Length; ++index3)
          {
            if (index3 < 0 || index3 > this.bones.Length)
              Debug.LogError((object) "Bone index was out of bounds.");
          }
          if (this.renderType == MB_RenderType.skinnedMeshRenderer && dynamicGameObject.indexesOfBonesUsed.Length < 1)
            Debug.Log((object) "DGO had no bones");
        }
      }
      if (!this.doBlendShapes || this.renderType == MB_RenderType.skinnedMeshRenderer)
        return;
      Debug.LogError((object) "Blend shapes can only be used with skinned meshes.");
    }

    private void _ZeroArray(Vector3[] arr, int idx, int length)
    {
      int num = idx + length;
      for (int index = idx; index < num; ++index)
        arr[index] = Vector3.zero;
    }

    private List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[] _buildBoneIdx2dgoMap()
    {
      List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[] dynamicGameObjectListArray = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[this.bones.Length];
      for (int index = 0; index < dynamicGameObjectListArray.Length; ++index)
        dynamicGameObjectListArray[index] = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();
      for (int index1 = 0; index1 < this.mbDynamicObjectsInCombinedMesh.Count; ++index1)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index1];
        for (int index2 = 0; index2 < dynamicGameObject.indexesOfBonesUsed.Length; ++index2)
          dynamicGameObjectListArray[dynamicGameObject.indexesOfBonesUsed[index2]].Add(dynamicGameObject);
      }
      return dynamicGameObjectListArray;
    }

    private void _CollectBonesToAddForDGO(
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo,
      Dictionary<Transform, int> bone2idx,
      HashSet<int> boneIdxsToDelete,
      HashSet<MB3_MeshCombinerSingle.BoneAndBindpose> bonesToAdd,
      Renderer r,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
    {
      Matrix4x4[] matrix4x4Array = dgo._tmpCachedBindposes = meshChannelCache.GetBindposes(r);
      BoneWeight[] boneWeightArray = dgo._tmpCachedBoneWeights = meshChannelCache.GetBoneWeights(r, dgo.numVerts);
      Transform[] transformArray = dgo._tmpCachedBones = this._getBones(r);
      HashSet<int> intSet = new HashSet<int>();
      for (int index = 0; index < boneWeightArray.Length; ++index)
      {
        intSet.Add(boneWeightArray[index].boneIndex0);
        intSet.Add(boneWeightArray[index].boneIndex1);
        intSet.Add(boneWeightArray[index].boneIndex2);
        intSet.Add(boneWeightArray[index].boneIndex3);
      }
      int[] array = new int[intSet.Count];
      intSet.CopyTo(array);
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        bool flag = false;
        int index2 = array[index1];
        int index3;
        if (bone2idx.TryGetValue(transformArray[index2], out index3) && (UnityEngine.Object) transformArray[index2] == (UnityEngine.Object) this.bones[index3] && !boneIdxsToDelete.Contains(index3) && matrix4x4Array[index2] == this.bindPoses[index3])
          flag = true;
        if (!flag)
        {
          MB3_MeshCombinerSingle.BoneAndBindpose boneAndBindpose = new MB3_MeshCombinerSingle.BoneAndBindpose(transformArray[index2], matrix4x4Array[index2]);
          if (!bonesToAdd.Contains(boneAndBindpose))
            bonesToAdd.Add(boneAndBindpose);
        }
      }
      dgo._tmpIndexesOfSourceBonesUsed = array;
    }

    private void _CopyBonesWeAreKeepingToNewBonesArrayAndAdjustBWIndexes(
      HashSet<int> boneIdxsToDeleteHS,
      HashSet<MB3_MeshCombinerSingle.BoneAndBindpose> bonesToAdd,
      Transform[] nbones,
      Matrix4x4[] nbindPoses,
      BoneWeight[] nboneWeights,
      int totalDeleteVerts)
    {
      if (boneIdxsToDeleteHS.Count > 0)
      {
        int[] array = new int[boneIdxsToDeleteHS.Count];
        boneIdxsToDeleteHS.CopyTo(array);
        Array.Sort<int>(array);
        int[] numArray = new int[this.bones.Length];
        int index1 = 0;
        int index2 = 0;
        for (int index3 = 0; index3 < this.bones.Length; ++index3)
        {
          if (index2 < array.Length && array[index2] == index3)
          {
            ++index2;
            numArray[index3] = -1;
          }
          else
          {
            numArray[index3] = index1;
            nbones[index1] = this.bones[index3];
            nbindPoses[index1] = this.bindPoses[index3];
            ++index1;
          }
        }
        int num = this.boneWeights.Length - totalDeleteVerts;
        for (int index4 = 0; index4 < num; ++index4)
        {
          nboneWeights[index4].boneIndex0 = numArray[nboneWeights[index4].boneIndex0];
          nboneWeights[index4].boneIndex1 = numArray[nboneWeights[index4].boneIndex1];
          nboneWeights[index4].boneIndex2 = numArray[nboneWeights[index4].boneIndex2];
          nboneWeights[index4].boneIndex3 = numArray[nboneWeights[index4].boneIndex3];
        }
        for (int index5 = 0; index5 < this.mbDynamicObjectsInCombinedMesh.Count; ++index5)
        {
          MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index5];
          for (int index6 = 0; index6 < dynamicGameObject.indexesOfBonesUsed.Length; ++index6)
            dynamicGameObject.indexesOfBonesUsed[index6] = numArray[dynamicGameObject.indexesOfBonesUsed[index6]];
        }
      }
      else
      {
        Array.Copy((Array) this.bones, (Array) nbones, this.bones.Length);
        Array.Copy((Array) this.bindPoses, (Array) nbindPoses, this.bindPoses.Length);
      }
    }

    private void _AddBonesToNewBonesArrayAndAdjustBWIndexes(
      MB3_MeshCombinerSingle.MB_DynamicGameObject dgo,
      Renderer r,
      int vertsIdx,
      Transform[] nbones,
      BoneWeight[] nboneWeights,
      MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
    {
      Transform[] tmpCachedBones = dgo._tmpCachedBones;
      Matrix4x4[] tmpCachedBindposes = dgo._tmpCachedBindposes;
      BoneWeight[] cachedBoneWeights = dgo._tmpCachedBoneWeights;
      int[] numArray = new int[tmpCachedBones.Length];
      for (int index1 = 0; index1 < dgo._tmpIndexesOfSourceBonesUsed.Length; ++index1)
      {
        int index2 = dgo._tmpIndexesOfSourceBonesUsed[index1];
        for (int index3 = 0; index3 < nbones.Length; ++index3)
        {
          if ((UnityEngine.Object) tmpCachedBones[index2] == (UnityEngine.Object) nbones[index3] && tmpCachedBindposes[index2] == this.bindPoses[index3])
          {
            numArray[index2] = index3;
            break;
          }
        }
      }
      for (int index4 = 0; index4 < cachedBoneWeights.Length; ++index4)
      {
        int index5 = vertsIdx + index4;
        nboneWeights[index5].boneIndex0 = numArray[cachedBoneWeights[index4].boneIndex0];
        nboneWeights[index5].boneIndex1 = numArray[cachedBoneWeights[index4].boneIndex1];
        nboneWeights[index5].boneIndex2 = numArray[cachedBoneWeights[index4].boneIndex2];
        nboneWeights[index5].boneIndex3 = numArray[cachedBoneWeights[index4].boneIndex3];
        nboneWeights[index5].weight0 = cachedBoneWeights[index4].weight0;
        nboneWeights[index5].weight1 = cachedBoneWeights[index4].weight1;
        nboneWeights[index5].weight2 = cachedBoneWeights[index4].weight2;
        nboneWeights[index5].weight3 = cachedBoneWeights[index4].weight3;
      }
      for (int index = 0; index < dgo._tmpIndexesOfSourceBonesUsed.Length; ++index)
        dgo._tmpIndexesOfSourceBonesUsed[index] = numArray[dgo._tmpIndexesOfSourceBonesUsed[index]];
      dgo.indexesOfBonesUsed = dgo._tmpIndexesOfSourceBonesUsed;
      dgo._tmpIndexesOfSourceBonesUsed = (int[]) null;
      dgo._tmpCachedBones = (Transform[]) null;
      dgo._tmpCachedBindposes = (Matrix4x4[]) null;
      dgo._tmpCachedBoneWeights = (BoneWeight[]) null;
    }

    private void _copyUV2unchangedToSeparateRects()
    {
      int padding = 16;
      List<Vector2> imgWidthHeights = new List<Vector2>();
      float num1 = 1E+11f;
      float num2 = 0.0f;
      for (int index = 0; index < this.mbDynamicObjectsInCombinedMesh.Count; ++index)
      {
        float magnitude = this.mbDynamicObjectsInCombinedMesh[index].meshSize.magnitude;
        if ((double) magnitude > (double) num2)
          num2 = magnitude;
        if ((double) magnitude < (double) num1)
          num1 = magnitude;
      }
      float num3 = 1000f;
      float num4 = 10f;
      float num5 = 0.0f;
      float num6;
      if ((double) num2 - (double) num1 > (double) num3 - (double) num4)
      {
        num6 = (float) (((double) num3 - (double) num4) / ((double) num2 - (double) num1));
        num5 = num4 - num1 * num6;
      }
      else
        num6 = num3 / num2;
      for (int index = 0; index < this.mbDynamicObjectsInCombinedMesh.Count; ++index)
      {
        Vector2 vector2 = Vector2.one * (this.mbDynamicObjectsInCombinedMesh[index].meshSize.magnitude * num6 + num5);
        imgWidthHeights.Add(vector2);
      }
      AtlasPackingResult[] rects = new MB2_TexturePacker()
      {
        doPowerOfTwoTextures = false
      }.GetRects(imgWidthHeights, 8192, padding);
      for (int index = 0; index < this.mbDynamicObjectsInCombinedMesh.Count; ++index)
      {
        MB3_MeshCombinerSingle.MB_DynamicGameObject dynamicGameObject = this.mbDynamicObjectsInCombinedMesh[index];
        float x;
        float num7 = x = this.uv2s[dynamicGameObject.vertIdx].x;
        float y;
        float num8 = y = this.uv2s[dynamicGameObject.vertIdx].y;
        int num9 = dynamicGameObject.vertIdx + dynamicGameObject.numVerts;
        for (int vertIdx = dynamicGameObject.vertIdx; vertIdx < num9; ++vertIdx)
        {
          if ((double) this.uv2s[vertIdx].x < (double) num7)
            num7 = this.uv2s[vertIdx].x;
          if ((double) this.uv2s[vertIdx].x > (double) x)
            x = this.uv2s[vertIdx].x;
          if ((double) this.uv2s[vertIdx].y < (double) num8)
            num8 = this.uv2s[vertIdx].y;
          if ((double) this.uv2s[vertIdx].y > (double) y)
            y = this.uv2s[vertIdx].y;
        }
        Rect rect = rects[0].rects[index];
        for (int vertIdx = dynamicGameObject.vertIdx; vertIdx < num9; ++vertIdx)
        {
          float num10 = x - num7;
          float num11 = y - num8;
          if ((double) num10 == 0.0)
            num10 = 1f;
          if ((double) num11 == 0.0)
            num11 = 1f;
          this.uv2s[vertIdx].x = (this.uv2s[vertIdx].x - num7) / num10 * rect.width + rect.x;
          this.uv2s[vertIdx].y = (this.uv2s[vertIdx].y - num8) / num11 * rect.height + rect.y;
        }
      }
    }

    public override List<Material> GetMaterialsOnTargetRenderer()
    {
      List<Material> onTargetRenderer = new List<Material>();
      if ((UnityEngine.Object) this._targetRenderer != (UnityEngine.Object) null)
        onTargetRenderer.AddRange((IEnumerable<Material>) this._targetRenderer.sharedMaterials);
      return onTargetRenderer;
    }

    [Serializable]
    public class SerializableIntArray
    {
      public int[] data;

      public SerializableIntArray()
      {
      }

      public SerializableIntArray(int len) => this.data = new int[len];
    }

    [Serializable]
    public class MB_DynamicGameObject : IComparable<MB3_MeshCombinerSingle.MB_DynamicGameObject>
    {
      public int instanceID;
      public string name;
      public int vertIdx;
      public int blendShapeIdx;
      public int numVerts;
      public int numBlendShapes;
      public int[] indexesOfBonesUsed = new int[0];
      public int lightmapIndex = -1;
      public Vector4 lightmapTilingOffset = new Vector4(1f, 1f, 0.0f, 0.0f);
      public Vector3 meshSize = Vector3.one;
      public bool show = true;
      public bool invertTriangles;
      public int[] submeshTriIdxs;
      public int[] submeshNumTris;
      public int[] targetSubmeshIdxs;
      public Rect[] uvRects;
      public Rect[] encapsulatingRect;
      public Rect[] sourceMaterialTiling;
      public Rect[] obUVRects;
      public bool _beingDeleted;
      public int _triangleIdxAdjustment;
      [NonSerialized]
      public MB3_MeshCombinerSingle.SerializableIntArray[] _tmpSubmeshTris;
      [NonSerialized]
      public Transform[] _tmpCachedBones;
      [NonSerialized]
      public Matrix4x4[] _tmpCachedBindposes;
      [NonSerialized]
      public BoneWeight[] _tmpCachedBoneWeights;
      [NonSerialized]
      public int[] _tmpIndexesOfSourceBonesUsed;

      public int CompareTo(MB3_MeshCombinerSingle.MB_DynamicGameObject b) => this.vertIdx - b.vertIdx;
    }

    public class MeshChannels
    {
      public Vector3[] vertices;
      public Vector3[] normals;
      public Vector4[] tangents;
      public Vector2[] uv0raw;
      public Vector2[] uv0modified;
      public Vector2[] uv2;
      public Vector2[] uv3;
      public Vector2[] uv4;
      public Color[] colors;
      public BoneWeight[] boneWeights;
      public Matrix4x4[] bindPoses;
      public int[] triangles;
      public MB3_MeshCombinerSingle.MBBlendShape[] blendShapes;
    }

    [Serializable]
    public class MBBlendShapeFrame
    {
      public float frameWeight;
      public Vector3[] vertices;
      public Vector3[] normals;
      public Vector3[] tangents;
    }

    [Serializable]
    public class MBBlendShape
    {
      public int gameObjectID;
      public string name;
      public int indexInSource;
      public MB3_MeshCombinerSingle.MBBlendShapeFrame[] frames;
    }

    public class MeshChannelsCache
    {
      private MB3_MeshCombinerSingle mc;
      protected Dictionary<int, MB3_MeshCombinerSingle.MeshChannels> meshID2MeshChannels = new Dictionary<int, MB3_MeshCombinerSingle.MeshChannels>();
      private Vector2 _HALF_UV = new Vector2(0.5f, 0.5f);

      internal MeshChannelsCache(MB3_MeshCombinerSingle mcs) => this.mc = mcs;

      internal Vector3[] GetVertices(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.vertices == null)
          meshChannels.vertices = m.vertices;
        return meshChannels.vertices;
      }

      internal Vector3[] GetNormals(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.normals == null)
          meshChannels.normals = this._getMeshNormals(m);
        return meshChannels.normals;
      }

      internal Vector4[] GetTangents(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.tangents == null)
          meshChannels.tangents = this._getMeshTangents(m);
        return meshChannels.tangents;
      }

      internal Vector2[] GetUv0Raw(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.uv0raw == null)
          meshChannels.uv0raw = this._getMeshUVs(m);
        return meshChannels.uv0raw;
      }

      internal Vector2[] GetUv0Modified(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.uv0modified == null)
          meshChannels.uv0modified = (Vector2[]) null;
        return meshChannels.uv0modified;
      }

      internal Vector2[] GetUv2(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.uv2 == null)
          meshChannels.uv2 = this._getMeshUV2s(m);
        return meshChannels.uv2;
      }

      internal Vector2[] GetUv3(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.uv3 == null)
          meshChannels.uv3 = MBVersion.GetMeshUV3orUV4(m, true, this.mc.LOG_LEVEL);
        return meshChannels.uv3;
      }

      internal Vector2[] GetUv4(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.uv4 == null)
          meshChannels.uv4 = MBVersion.GetMeshUV3orUV4(m, false, this.mc.LOG_LEVEL);
        return meshChannels.uv4;
      }

      internal Color[] GetColors(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.colors == null)
          meshChannels.colors = this._getMeshColors(m);
        return meshChannels.colors;
      }

      internal Matrix4x4[] GetBindposes(Renderer r)
      {
        UnityEngine.Mesh mesh = MB_Utility.GetMesh(r.gameObject);
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(mesh.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(mesh.GetInstanceID(), meshChannels);
        }
        if (meshChannels.bindPoses == null)
          meshChannels.bindPoses = MB3_MeshCombinerSingle.MeshChannelsCache._getBindPoses(r);
        return meshChannels.bindPoses;
      }

      internal BoneWeight[] GetBoneWeights(Renderer r, int numVertsInMeshBeingAdded)
      {
        UnityEngine.Mesh mesh = MB_Utility.GetMesh(r.gameObject);
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(mesh.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(mesh.GetInstanceID(), meshChannels);
        }
        if (meshChannels.boneWeights == null)
          meshChannels.boneWeights = MB3_MeshCombinerSingle.MeshChannelsCache._getBoneWeights(r, numVertsInMeshBeingAdded);
        return meshChannels.boneWeights;
      }

      internal int[] GetTriangles(UnityEngine.Mesh m)
      {
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.triangles == null)
          meshChannels.triangles = m.triangles;
        return meshChannels.triangles;
      }

      internal MB3_MeshCombinerSingle.MBBlendShape[] GetBlendShapes(UnityEngine.Mesh m, int gameObjectID)
      {
        if (MBVersion.GetMajorVersion() <= 5 && (MBVersion.GetMajorVersion() != 5 || MBVersion.GetMinorVersion() < 3))
          return new MB3_MeshCombinerSingle.MBBlendShape[0];
        MB3_MeshCombinerSingle.MeshChannels meshChannels;
        if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
        {
          meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
          this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
        }
        if (meshChannels.blendShapes == null)
        {
          MB3_MeshCombinerSingle.MBBlendShape[] mbBlendShapeArray = new MB3_MeshCombinerSingle.MBBlendShape[m.blendShapeCount];
          int vertexCount = m.vertexCount;
          for (int shapeIndex = 0; shapeIndex < mbBlendShapeArray.Length; ++shapeIndex)
          {
            MB3_MeshCombinerSingle.MBBlendShape mbBlendShape = mbBlendShapeArray[shapeIndex] = new MB3_MeshCombinerSingle.MBBlendShape();
            mbBlendShape.frames = new MB3_MeshCombinerSingle.MBBlendShapeFrame[MBVersion.GetBlendShapeFrameCount(m, shapeIndex)];
            mbBlendShape.name = m.GetBlendShapeName(shapeIndex);
            mbBlendShape.indexInSource = shapeIndex;
            mbBlendShape.gameObjectID = gameObjectID;
            for (int frameIndex = 0; frameIndex < mbBlendShape.frames.Length; ++frameIndex)
            {
              MB3_MeshCombinerSingle.MBBlendShapeFrame mbBlendShapeFrame = mbBlendShape.frames[frameIndex] = new MB3_MeshCombinerSingle.MBBlendShapeFrame();
              mbBlendShapeFrame.frameWeight = MBVersion.GetBlendShapeFrameWeight(m, shapeIndex, frameIndex);
              mbBlendShapeFrame.vertices = new Vector3[vertexCount];
              mbBlendShapeFrame.normals = new Vector3[vertexCount];
              mbBlendShapeFrame.tangents = new Vector3[vertexCount];
              MBVersion.GetBlendShapeFrameVertices(m, shapeIndex, frameIndex, mbBlendShapeFrame.vertices, mbBlendShapeFrame.normals, mbBlendShapeFrame.tangents);
            }
          }
          meshChannels.blendShapes = mbBlendShapeArray;
          return meshChannels.blendShapes;
        }
        MB3_MeshCombinerSingle.MBBlendShape[] blendShapes = new MB3_MeshCombinerSingle.MBBlendShape[meshChannels.blendShapes.Length];
        for (int index = 0; index < blendShapes.Length; ++index)
        {
          blendShapes[index] = new MB3_MeshCombinerSingle.MBBlendShape();
          blendShapes[index].name = meshChannels.blendShapes[index].name;
          blendShapes[index].indexInSource = meshChannels.blendShapes[index].indexInSource;
          blendShapes[index].frames = meshChannels.blendShapes[index].frames;
          blendShapes[index].gameObjectID = gameObjectID;
        }
        return blendShapes;
      }

      private Color[] _getMeshColors(UnityEngine.Mesh m)
      {
        Color[] meshColors = m.colors;
        if (meshColors.Length == 0)
        {
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no colors. Generating");
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Mesh " + ((object) m)?.ToString() + " didn't have colors. Generating an array of white colors"));
          meshColors = new Color[m.vertexCount];
          for (int index = 0; index < meshColors.Length; ++index)
            meshColors[index] = Color.white;
        }
        return meshColors;
      }

      private Vector3[] _getMeshNormals(UnityEngine.Mesh m)
      {
        Vector3[] normals = m.normals;
        if (normals.Length == 0)
        {
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no normals. Generating");
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Mesh " + ((object) m)?.ToString() + " didn't have normals. Generating normals."));
          UnityEngine.Mesh o = UnityEngine.Object.Instantiate<UnityEngine.Mesh>(m);
          o.RecalculateNormals();
          normals = o.normals;
          MB_Utility.Destroy((UnityEngine.Object) o);
        }
        return normals;
      }

      private Vector4[] _getMeshTangents(UnityEngine.Mesh m)
      {
        Vector4[] outTangents = m.tangents;
        if (outTangents.Length == 0)
        {
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no tangents. Generating");
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Mesh " + ((object) m)?.ToString() + " didn't have tangents. Generating tangents."));
          Vector3[] vertices = m.vertices;
          Vector2[] uv0Raw = this.GetUv0Raw(m);
          Vector3[] meshNormals = this._getMeshNormals(m);
          outTangents = new Vector4[m.vertexCount];
          for (int submesh = 0; submesh < m.subMeshCount; ++submesh)
            this._generateTangents(m.GetTriangles(submesh), vertices, uv0Raw, meshNormals, outTangents);
        }
        return outTangents;
      }

      private Vector2[] _getMeshUVs(UnityEngine.Mesh m)
      {
        Vector2[] meshUvs = m.uv;
        if (meshUvs.Length == 0)
        {
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no uvs. Generating");
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Mesh " + ((object) m)?.ToString() + " didn't have uvs. Generating uvs."));
          meshUvs = new Vector2[m.vertexCount];
          for (int index = 0; index < meshUvs.Length; ++index)
            meshUvs[index] = this._HALF_UV;
        }
        return meshUvs;
      }

      private Vector2[] _getMeshUV2s(UnityEngine.Mesh m)
      {
        Vector2[] meshUv2s = m.uv2;
        if (meshUv2s.Length == 0)
        {
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no uv2s. Generating");
          if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
            Debug.LogWarning((object) ("Mesh " + ((object) m)?.ToString() + " didn't have uv2s. Generating uv2s."));
          if (this.mc._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged_to_separate_rects)
            Debug.LogError((object) ("Mesh " + ((object) m)?.ToString() + " did not have a UV2 channel. Nothing to copy when trying to copy UV2 to separate rects. The combined mesh will not lightmap properly. Try using generate new uv2 layout."));
          meshUv2s = new Vector2[m.vertexCount];
          for (int index = 0; index < meshUv2s.Length; ++index)
            meshUv2s[index] = this._HALF_UV;
        }
        return meshUv2s;
      }

      public static Matrix4x4[] _getBindPoses(Renderer r)
      {
        switch (r)
        {
          case SkinnedMeshRenderer _:
            return ((SkinnedMeshRenderer) r).sharedMesh.bindposes;
          case MeshRenderer _:
            return new Matrix4x4[1]{ Matrix4x4.identity };
          default:
            Debug.LogError((object) "Could not _getBindPoses. Object does not have a renderer");
            return (Matrix4x4[]) null;
        }
      }

      public static BoneWeight[] _getBoneWeights(Renderer r, int numVertsInMeshBeingAdded)
      {
        switch (r)
        {
          case SkinnedMeshRenderer _:
            return ((SkinnedMeshRenderer) r).sharedMesh.boneWeights;
          case MeshRenderer _:
            BoneWeight boneWeight = new BoneWeight();
            boneWeight.boneIndex0 = boneWeight.boneIndex1 = boneWeight.boneIndex2 = boneWeight.boneIndex3 = 0;
            boneWeight.weight0 = 1f;
            boneWeight.weight1 = boneWeight.weight2 = boneWeight.weight3 = 0.0f;
            BoneWeight[] boneWeights = new BoneWeight[numVertsInMeshBeingAdded];
            for (int index = 0; index < boneWeights.Length; ++index)
              boneWeights[index] = boneWeight;
            return boneWeights;
          default:
            Debug.LogError((object) "Could not _getBoneWeights. Object does not have a renderer");
            return (BoneWeight[]) null;
        }
      }

      private void _generateTangents(
        int[] triangles,
        Vector3[] verts,
        Vector2[] uvs,
        Vector3[] normals,
        Vector4[] outTangents)
      {
        int length1 = triangles.Length;
        int length2 = verts.Length;
        Vector3[] vector3Array1 = new Vector3[length2];
        Vector3[] vector3Array2 = new Vector3[length2];
        for (int index = 0; index < length1; index += 3)
        {
          int triangle1 = triangles[index];
          int triangle2 = triangles[index + 1];
          int triangle3 = triangles[index + 2];
          Vector3 vert1 = verts[triangle1];
          Vector3 vert2 = verts[triangle2];
          Vector3 vert3 = verts[triangle3];
          Vector2 uv1 = uvs[triangle1];
          Vector2 uv2 = uvs[triangle2];
          Vector2 uv3 = uvs[triangle3];
          float num1 = vert2.x - vert1.x;
          float num2 = vert3.x - vert1.x;
          float num3 = vert2.y - vert1.y;
          float num4 = vert3.y - vert1.y;
          float num5 = vert2.z - vert1.z;
          float num6 = vert3.z - vert1.z;
          float num7 = uv2.x - uv1.x;
          float num8 = uv3.x - uv1.x;
          float num9 = uv2.y - uv1.y;
          float num10 = uv3.y - uv1.y;
          float num11 = (float) ((double) num7 * (double) num10 - (double) num8 * (double) num9);
          if ((double) num11 == 0.0)
          {
            Debug.LogError((object) "Could not compute tangents. All UVs need to form a valid triangles in UV space. If any UV triangles are collapsed, tangents cannot be generated.");
            return;
          }
          float num12 = 1f / num11;
          Vector3 vector3_1 = new Vector3((float) ((double) num10 * (double) num1 - (double) num9 * (double) num2) * num12, (float) ((double) num10 * (double) num3 - (double) num9 * (double) num4) * num12, (float) ((double) num10 * (double) num5 - (double) num9 * (double) num6) * num12);
          Vector3 vector3_2 = new Vector3((float) ((double) num7 * (double) num2 - (double) num8 * (double) num1) * num12, (float) ((double) num7 * (double) num4 - (double) num8 * (double) num3) * num12, (float) ((double) num7 * (double) num6 - (double) num8 * (double) num5) * num12);
          vector3Array1[triangle1] += vector3_1;
          vector3Array1[triangle2] += vector3_1;
          vector3Array1[triangle3] += vector3_1;
          vector3Array2[triangle1] += vector3_2;
          vector3Array2[triangle2] += vector3_2;
          vector3Array2[triangle3] += vector3_2;
        }
        for (int index = 0; index < length2; ++index)
        {
          Vector3 normal = normals[index];
          Vector3 rhs = vector3Array1[index];
          Vector3 normalized = (rhs - normal * Vector3.Dot(normal, rhs)).normalized;
          outTangents[index] = new Vector4(normalized.x, normalized.y, normalized.z);
          outTangents[index].w = (double) Vector3.Dot(Vector3.Cross(normal, rhs), vector3Array2[index]) < 0.0 ? -1f : 1f;
        }
      }
    }

    public struct BoneAndBindpose
    {
      public Transform bone;
      public Matrix4x4 bindPose;

      public BoneAndBindpose(Transform t, Matrix4x4 bp)
      {
        this.bone = t;
        this.bindPose = bp;
      }

      public override bool Equals(object obj) => obj is MB3_MeshCombinerSingle.BoneAndBindpose boneAndBindpose && (UnityEngine.Object) this.bone == (UnityEngine.Object) boneAndBindpose.bone && this.bindPose == ((MB3_MeshCombinerSingle.BoneAndBindpose) obj).bindPose;

      public override int GetHashCode() => this.bone.GetInstanceID() % int.MaxValue ^ (int) this.bindPose[0, 0];
    }
  }
}
