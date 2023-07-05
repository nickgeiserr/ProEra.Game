// Decompiled with JetBrains decompiler
// Type: MB3_MeshBakerCommon
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using UnityEngine;

public abstract class MB3_MeshBakerCommon : MB3_MeshBakerRoot
{
  public List<GameObject> objsToMesh;
  public bool useObjsToMeshFromTexBaker = true;
  public bool clearBuffersAfterBake = true;
  public string bakeAssetsInPlaceFolderPath;
  [HideInInspector]
  public GameObject resultPrefab;

  public abstract MB3_MeshCombiner meshCombiner { get; }

  public override MB2_TextureBakeResults textureBakeResults
  {
    get => this.meshCombiner.textureBakeResults;
    set => this.meshCombiner.textureBakeResults = value;
  }

  public override List<GameObject> GetObjectsToCombine()
  {
    if (this.useObjsToMeshFromTexBaker)
    {
      MB3_TextureBaker component = this.gameObject.GetComponent<MB3_TextureBaker>();
      if ((Object) component == (Object) null)
        component = this.gameObject.transform.parent.GetComponent<MB3_TextureBaker>();
      if ((Object) component != (Object) null)
        return component.GetObjectsToCombine();
      Debug.LogWarning((object) "Use Objects To Mesh From Texture Baker was checked but no texture baker");
      return new List<GameObject>();
    }
    if (this.objsToMesh == null)
      this.objsToMesh = new List<GameObject>();
    return this.objsToMesh;
  }

  public void EnableDisableSourceObjectRenderers(bool show)
  {
    for (int index1 = 0; index1 < this.GetObjectsToCombine().Count; ++index1)
    {
      GameObject go = this.GetObjectsToCombine()[index1];
      if ((Object) go != (Object) null)
      {
        Renderer renderer = MB_Utility.GetRenderer(go);
        if ((Object) renderer != (Object) null)
          renderer.enabled = show;
        LODGroup componentInParent = renderer.GetComponentInParent<LODGroup>();
        if ((Object) componentInParent != (Object) null)
        {
          bool flag = true;
          LOD[] loDs = componentInParent.GetLODs();
          for (int index2 = 0; index2 < loDs.Length; ++index2)
          {
            for (int index3 = 0; index3 < loDs[index2].renderers.Length; ++index3)
            {
              if ((Object) loDs[index2].renderers[index3] != (Object) renderer)
              {
                flag = false;
                break;
              }
            }
          }
          if (flag)
            componentInParent.enabled = show;
        }
      }
    }
  }

  public virtual void ClearMesh() => this.meshCombiner.ClearMesh();

  public virtual void DestroyMesh() => this.meshCombiner.DestroyMesh();

  public virtual void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods) => this.meshCombiner.DestroyMeshEditor(editorMethods);

  public virtual int GetNumObjectsInCombined() => this.meshCombiner.GetNumObjectsInCombined();

  public virtual int GetNumVerticesFor(GameObject go) => this.meshCombiner.GetNumVerticesFor(go);

  public MB3_TextureBaker GetTextureBaker()
  {
    MB3_TextureBaker component = this.GetComponent<MB3_TextureBaker>();
    if ((Object) component != (Object) null)
      return component;
    return (Object) this.transform.parent != (Object) null ? this.transform.parent.GetComponent<MB3_TextureBaker>() : (MB3_TextureBaker) null;
  }

  public abstract bool AddDeleteGameObjects(
    GameObject[] gos,
    GameObject[] deleteGOs,
    bool disableRendererInSource = true);

  public abstract bool AddDeleteGameObjectsByID(
    GameObject[] gos,
    int[] deleteGOinstanceIDs,
    bool disableRendererInSource = true);

  public virtual void Apply(
    MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
  {
    this.meshCombiner.name = this.name + "-mesh";
    this.meshCombiner.Apply(uv2GenerationMethod);
  }

  public virtual void Apply(
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
    this.meshCombiner.name = this.name + "-mesh";
    this.meshCombiner.Apply(triangles, vertices, normals, tangents, uvs, uv2, uv3, uv4, colors, bones, blendShapesFlag, uv2GenerationMethod);
  }

  public virtual bool CombinedMeshContains(GameObject go) => this.meshCombiner.CombinedMeshContains(go);

  public virtual void UpdateGameObjects(
    GameObject[] gos,
    bool recalcBounds = true,
    bool updateVertices = true,
    bool updateNormals = true,
    bool updateTangents = true,
    bool updateUV = false,
    bool updateUV1 = false,
    bool updateUV2 = false,
    bool updateColors = false,
    bool updateSkinningInfo = false)
  {
    this.meshCombiner.name = this.name + "-mesh";
    this.meshCombiner.UpdateGameObjects(gos, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
  }

  public virtual void UpdateSkinnedMeshApproximateBounds()
  {
    if (!this._ValidateForUpdateSkinnedMeshBounds())
      return;
    this.meshCombiner.UpdateSkinnedMeshApproximateBounds();
  }

  public virtual void UpdateSkinnedMeshApproximateBoundsFromBones()
  {
    if (!this._ValidateForUpdateSkinnedMeshBounds())
      return;
    this.meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBones();
  }

  public virtual void UpdateSkinnedMeshApproximateBoundsFromBounds()
  {
    if (!this._ValidateForUpdateSkinnedMeshBounds())
      return;
    this.meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBounds();
  }

  protected virtual bool _ValidateForUpdateSkinnedMeshBounds()
  {
    if (this.meshCombiner.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
    {
      Debug.LogWarning((object) "Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
      return false;
    }
    if ((Object) this.meshCombiner.resultSceneObject == (Object) null)
    {
      Debug.LogWarning((object) "Result Scene Object does not exist. No point in calling UpdateSkinnedMeshApproximateBounds.");
      return false;
    }
    if (!((Object) this.meshCombiner.resultSceneObject.GetComponentInChildren<SkinnedMeshRenderer>() == (Object) null))
      return true;
    Debug.LogWarning((object) "No SkinnedMeshRenderer on result scene object.");
    return false;
  }
}
