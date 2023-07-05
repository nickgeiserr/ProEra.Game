// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshCombiner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public abstract class MB3_MeshCombiner
  {
    [SerializeField]
    protected MB2_LogLevel _LOG_LEVEL = MB2_LogLevel.info;
    [SerializeField]
    protected MB2_ValidationLevel _validationLevel = MB2_ValidationLevel.robust;
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected MB2_TextureBakeResults _textureBakeResults;
    [SerializeField]
    protected GameObject _resultSceneObject;
    [SerializeField]
    protected Renderer _targetRenderer;
    [SerializeField]
    protected MB_RenderType _renderType;
    [SerializeField]
    protected MB2_OutputOptions _outputOption;
    [SerializeField]
    protected MB2_LightmapOptions _lightmapOption = MB2_LightmapOptions.ignore_UV2;
    [SerializeField]
    protected bool _doNorm = true;
    [SerializeField]
    protected bool _doTan = true;
    [SerializeField]
    protected bool _doCol;
    [SerializeField]
    protected bool _doUV = true;
    [SerializeField]
    protected bool _doUV3;
    [SerializeField]
    protected bool _doUV4;
    [SerializeField]
    protected bool _doBlendShapes;
    [SerializeField]
    protected bool _recenterVertsToBoundsCenter;
    [SerializeField]
    public bool _optimizeAfterBake = true;
    [SerializeField]
    public float uv2UnwrappingParamsHardAngle = 60f;
    [SerializeField]
    public float uv2UnwrappingParamsPackMargin = 0.005f;
    protected bool _usingTemporaryTextureBakeResult;

    public static bool EVAL_VERSION => false;

    public virtual MB2_LogLevel LOG_LEVEL
    {
      get => this._LOG_LEVEL;
      set => this._LOG_LEVEL = value;
    }

    public virtual MB2_ValidationLevel validationLevel
    {
      get => this._validationLevel;
      set => this._validationLevel = value;
    }

    public string name
    {
      get => this._name;
      set => this._name = value;
    }

    public virtual MB2_TextureBakeResults textureBakeResults
    {
      get => this._textureBakeResults;
      set => this._textureBakeResults = value;
    }

    public virtual GameObject resultSceneObject
    {
      get => this._resultSceneObject;
      set => this._resultSceneObject = value;
    }

    public virtual Renderer targetRenderer
    {
      get => this._targetRenderer;
      set
      {
        if ((UnityEngine.Object) this._targetRenderer != (UnityEngine.Object) null && (UnityEngine.Object) this._targetRenderer != (UnityEngine.Object) value)
          Debug.LogWarning((object) "Previous targetRenderer was not null. Combined mesh may be being used by more than one Renderer");
        this._targetRenderer = value;
      }
    }

    public virtual MB_RenderType renderType
    {
      get => this._renderType;
      set => this._renderType = value;
    }

    public virtual MB2_OutputOptions outputOption
    {
      get => this._outputOption;
      set => this._outputOption = value;
    }

    public virtual MB2_LightmapOptions lightmapOption
    {
      get => this._lightmapOption;
      set => this._lightmapOption = value;
    }

    public virtual bool doNorm
    {
      get => this._doNorm;
      set => this._doNorm = value;
    }

    public virtual bool doTan
    {
      get => this._doTan;
      set => this._doTan = value;
    }

    public virtual bool doCol
    {
      get => this._doCol;
      set => this._doCol = value;
    }

    public virtual bool doUV
    {
      get => this._doUV;
      set => this._doUV = value;
    }

    public virtual bool doUV1
    {
      get => false;
      set
      {
      }
    }

    public virtual bool doUV2() => this._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged || this._lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping || this._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged_to_separate_rects;

    public virtual bool doUV3
    {
      get => this._doUV3;
      set => this._doUV3 = value;
    }

    public virtual bool doUV4
    {
      get => this._doUV4;
      set => this._doUV4 = value;
    }

    public virtual bool doBlendShapes
    {
      get => this._doBlendShapes;
      set => this._doBlendShapes = value;
    }

    public virtual bool recenterVertsToBoundsCenter
    {
      get => this._recenterVertsToBoundsCenter;
      set => this._recenterVertsToBoundsCenter = value;
    }

    public bool optimizeAfterBake
    {
      get => this._optimizeAfterBake;
      set => this._optimizeAfterBake = value;
    }

    public abstract int GetLightmapIndex();

    public abstract void ClearBuffers();

    public abstract void ClearMesh();

    public abstract void DestroyMesh();

    public abstract void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods);

    public abstract List<GameObject> GetObjectsInCombined();

    public abstract int GetNumObjectsInCombined();

    public abstract int GetNumVerticesFor(GameObject go);

    public abstract int GetNumVerticesFor(int instanceID);

    public abstract Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> BuildSourceBlendShapeToCombinedIndexMap();

    public virtual void Apply() => this.Apply((MB3_MeshCombiner.GenerateUV2Delegate) null);

    public abstract void Apply(
      MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod);

    public abstract void Apply(
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
      bool blendShapeFlag = false,
      MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null);

    public abstract void UpdateGameObjects(
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
      bool updateSkinningInfo = false);

    public abstract bool AddDeleteGameObjects(
      GameObject[] gos,
      GameObject[] deleteGOs,
      bool disableRendererInSource = true);

    public abstract bool AddDeleteGameObjectsByID(
      GameObject[] gos,
      int[] deleteGOinstanceIDs,
      bool disableRendererInSource);

    public abstract bool CombinedMeshContains(GameObject go);

    public abstract void UpdateSkinnedMeshApproximateBounds();

    public abstract void UpdateSkinnedMeshApproximateBoundsFromBones();

    public abstract void CheckIntegrity();

    public abstract void UpdateSkinnedMeshApproximateBoundsFromBounds();

    public static void UpdateSkinnedMeshApproximateBoundsFromBonesStatic(
      Transform[] bs,
      SkinnedMeshRenderer smr)
    {
      Vector3 position1 = bs[0].position;
      Vector3 position2 = bs[0].position;
      for (int index = 1; index < bs.Length; ++index)
      {
        Vector3 position3 = bs[index].position;
        if ((double) position3.x < (double) position2.x)
          position2.x = position3.x;
        if ((double) position3.y < (double) position2.y)
          position2.y = position3.y;
        if ((double) position3.z < (double) position2.z)
          position2.z = position3.z;
        if ((double) position3.x > (double) position1.x)
          position1.x = position3.x;
        if ((double) position3.y > (double) position1.y)
          position1.y = position3.y;
        if ((double) position3.z > (double) position1.z)
          position1.z = position3.z;
      }
      Vector3 vector3_1 = (position1 + position2) / 2f;
      Vector3 vector3_2 = position1 - position2;
      Matrix4x4 worldToLocalMatrix = smr.worldToLocalMatrix;
      Bounds bounds = new Bounds((Vector3) (worldToLocalMatrix * (Vector4) vector3_1), (Vector3) (worldToLocalMatrix * (Vector4) vector3_2));
      smr.localBounds = bounds;
    }

    public static void UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(
      List<GameObject> objectsInCombined,
      SkinnedMeshRenderer smr)
    {
      Bounds b = new Bounds();
      Bounds bounds1 = new Bounds();
      if (MB_Utility.GetBounds(objectsInCombined[0], out b))
      {
        Bounds bounds2 = b;
        for (int index = 1; index < objectsInCombined.Count; ++index)
        {
          if (MB_Utility.GetBounds(objectsInCombined[index], out b))
          {
            bounds2.Encapsulate(b);
          }
          else
          {
            Debug.LogError((object) "Could not get bounds. Not updating skinned mesh bounds");
            return;
          }
        }
        smr.localBounds = bounds2;
      }
      else
        Debug.LogError((object) "Could not get bounds. Not updating skinned mesh bounds");
    }

    protected virtual bool _CreateTemporaryTextrueBakeResult(
      GameObject[] gos,
      List<Material> matsOnTargetRenderer)
    {
      if (this.GetNumObjectsInCombined() > 0)
      {
        Debug.LogError((object) "Can't add objects if there are already objects in combined mesh when 'Texture Bake Result' is not set. Perhaps enable 'Clear Buffers After Bake'");
        return false;
      }
      this._usingTemporaryTextureBakeResult = true;
      this._textureBakeResults = MB2_TextureBakeResults.CreateForMaterialsOnRenderer(gos, matsOnTargetRenderer);
      return true;
    }

    public abstract List<Material> GetMaterialsOnTargetRenderer();

    public delegate void GenerateUV2Delegate(UnityEngine.Mesh m, float hardAngle, float packMargin);

    public class MBBlendShapeKey
    {
      public int gameObjecID;
      public int blendShapeIndexInSrc;

      public MBBlendShapeKey(int srcSkinnedMeshRenderGameObjectID, int blendShapeIndexInSource)
      {
        this.gameObjecID = srcSkinnedMeshRenderGameObjectID;
        this.blendShapeIndexInSrc = blendShapeIndexInSource;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is MB3_MeshCombiner.MBBlendShapeKey) || obj == null)
          return false;
        MB3_MeshCombiner.MBBlendShapeKey mbBlendShapeKey = (MB3_MeshCombiner.MBBlendShapeKey) obj;
        return this.gameObjecID == mbBlendShapeKey.gameObjecID && this.blendShapeIndexInSrc == mbBlendShapeKey.blendShapeIndexInSrc;
      }

      public override int GetHashCode() => (23 * 31 + this.gameObjecID) * 31 + this.blendShapeIndexInSrc;
    }

    public class MBBlendShapeValue
    {
      public GameObject combinedMeshGameObject;
      public int blendShapeIndex;
    }
  }
}
