// Decompiled with JetBrains decompiler
// Type: MB3_MeshBakerRoot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using UnityEngine;

public abstract class MB3_MeshBakerRoot : MonoBehaviour
{
  public static bool DO_INTEGRITY_CHECKS = true;
  public Vector3 sortAxis;

  [HideInInspector]
  public abstract MB2_TextureBakeResults textureBakeResults { get; set; }

  public virtual List<GameObject> GetObjectsToCombine() => (List<GameObject>) null;

  public static bool DoCombinedValidate(
    MB3_MeshBakerRoot mom,
    MB_ObjsToCombineTypes objToCombineType,
    MB2_EditorMethodsInterface editorMethods,
    MB2_ValidationLevel validationLevel)
  {
    if ((Object) mom.textureBakeResults == (Object) null)
    {
      Debug.LogError((object) ("Need to set Texture Bake Result on " + ((object) mom)?.ToString()));
      return false;
    }
    if (mom is MB3_MeshBakerCommon)
    {
      MB3_TextureBaker textureBaker = ((MB3_MeshBakerCommon) mom).GetTextureBaker();
      if ((Object) textureBaker != (Object) null && (Object) textureBaker.textureBakeResults != (Object) mom.textureBakeResults)
        Debug.LogWarning((object) "Texture Bake Result on this component is not the same as the Texture Bake Result on the MB3_TextureBaker.");
    }
    Dictionary<int, MB_Utility.MeshAnalysisResult> dictionary = (Dictionary<int, MB_Utility.MeshAnalysisResult>) null;
    if (validationLevel == MB2_ValidationLevel.robust)
      dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult>();
    List<GameObject> objectsToCombine1 = mom.GetObjectsToCombine();
    for (int index1 = 0; index1 < objectsToCombine1.Count; ++index1)
    {
      GameObject go = objectsToCombine1[index1];
      if ((Object) go == (Object) null)
      {
        Debug.LogError((object) ("The list of objects to combine contains a null at position." + index1.ToString() + " Select and use [shift] delete to remove"));
        return false;
      }
      for (int index2 = index1 + 1; index2 < objectsToCombine1.Count; ++index2)
      {
        if ((Object) objectsToCombine1[index1] == (Object) objectsToCombine1[index2])
        {
          Debug.LogError((object) ("The list of objects to combine contains duplicates at " + index1.ToString() + " and " + index2.ToString()));
          return false;
        }
      }
      if (MB_Utility.GetGOMaterials(go).Length == 0)
      {
        Debug.LogError((object) ("Object " + ((object) go)?.ToString() + " in the list of objects to be combined does not have a material"));
        return false;
      }
      UnityEngine.Mesh mesh = MB_Utility.GetMesh(go);
      if ((Object) mesh == (Object) null)
      {
        Debug.LogError((object) ("Object " + ((object) go)?.ToString() + " in the list of objects to be combined does not have a mesh"));
        return false;
      }
      if ((Object) mesh != (Object) null && !Application.isEditor && Application.isPlaying && mom.textureBakeResults.doMultiMaterial && validationLevel >= MB2_ValidationLevel.robust)
      {
        MB_Utility.MeshAnalysisResult mar;
        if (!dictionary.TryGetValue(mesh.GetInstanceID(), out mar))
        {
          MB_Utility.doSubmeshesShareVertsOrTris(mesh, ref mar);
          dictionary.Add(mesh.GetInstanceID(), mar);
        }
        if (mar.hasOverlappingSubmeshVerts)
          Debug.LogWarning((object) ("Object " + ((object) objectsToCombine1[index1])?.ToString() + " in the list of objects to combine has overlapping submeshes (submeshes share vertices). If the UVs associated with the shared vertices are important then this bake may not work. If you are using multiple materials then this object can only be combined with objects that use the exact same set of textures (each atlas contains one texture). There may be other undesirable side affects as well. Mesh Master, available in the asset store can fix overlapping submeshes."));
      }
    }
    if (mom is MB3_MeshBaker)
    {
      List<GameObject> objectsToCombine2 = mom.GetObjectsToCombine();
      if (objectsToCombine2 == null || objectsToCombine2.Count == 0)
      {
        Debug.LogError((object) "No meshes to combine. Please assign some meshes to combine.");
        return false;
      }
      if (mom is MB3_MeshBaker && ((MB3_MeshBakerCommon) mom).meshCombiner.renderType == MB_RenderType.skinnedMeshRenderer && !editorMethods.ValidateSkinnedMeshes(objectsToCombine2))
        return false;
    }
    editorMethods?.CheckPrefabTypes(objToCombineType, objectsToCombine1);
    return true;
  }

  public class ZSortObjects
  {
    public Vector3 sortAxis;

    public void SortByDistanceAlongAxis(List<GameObject> gos)
    {
      if (this.sortAxis == Vector3.zero)
      {
        Debug.LogError((object) "The sort axis cannot be the zero vector.");
      }
      else
      {
        Debug.Log((object) ("Z sorting meshes along axis numObjs=" + gos.Count.ToString()));
        List<MB3_MeshBakerRoot.ZSortObjects.Item> objList = new List<MB3_MeshBakerRoot.ZSortObjects.Item>();
        Quaternion rotation = Quaternion.FromToRotation(this.sortAxis, Vector3.forward);
        for (int index = 0; index < gos.Count; ++index)
        {
          if ((Object) gos[index] != (Object) null)
          {
            MB3_MeshBakerRoot.ZSortObjects.Item obj = new MB3_MeshBakerRoot.ZSortObjects.Item()
            {
              point = gos[index].transform.position,
              go = gos[index]
            };
            obj.point = rotation * obj.point;
            objList.Add(obj);
          }
        }
        objList.Sort((IComparer<MB3_MeshBakerRoot.ZSortObjects.Item>) new MB3_MeshBakerRoot.ZSortObjects.ItemComparer());
        for (int index = 0; index < gos.Count; ++index)
          gos[index] = objList[index].go;
      }
    }

    public class Item
    {
      public GameObject go;
      public Vector3 point;
    }

    public class ItemComparer : IComparer<MB3_MeshBakerRoot.ZSortObjects.Item>
    {
      public int Compare(
        MB3_MeshBakerRoot.ZSortObjects.Item a,
        MB3_MeshBakerRoot.ZSortObjects.Item b)
      {
        return (int) Mathf.Sign(b.point.z - a.point.z);
      }
    }
  }
}
