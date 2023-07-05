// Decompiled with JetBrains decompiler
// Type: SkinnedMeshCombiner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshCombiner : MonoBehaviour
{
  private List<CombineInstance> combineInstances;
  private List<SkinnedMeshRenderer> smRenderers;
  private List<BoneWeight> boneWeights;
  private List<Transform> bones;
  private List<Matrix4x4> bindposes;
  private int[] meshIndex;

  private void Start()
  {
    this.transform.parent.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.transform.localScale = new Vector3(1f, 1f, 1f);
    this.smRenderers = new List<SkinnedMeshRenderer>();
    foreach (Transform transform in this.transform)
    {
      if ((Object) transform.GetComponent<SkinnedMeshRenderer>() != (Object) null)
        this.smRenderers.Add(transform.GetComponent<SkinnedMeshRenderer>());
    }
    this.bones = new List<Transform>();
    this.boneWeights = new List<BoneWeight>();
    this.combineInstances = new List<CombineInstance>();
    int length = 0;
    foreach (SkinnedMeshRenderer smRenderer in this.smRenderers)
      length += smRenderer.sharedMesh.subMeshCount;
    this.meshIndex = new int[length];
    int num = 0;
    for (int index = 0; index < this.smRenderers.Count; ++index)
    {
      SkinnedMeshRenderer smRenderer = this.smRenderers[index];
      foreach (BoneWeight boneWeight in smRenderer.sharedMesh.boneWeights)
      {
        boneWeight.boneIndex0 += num;
        boneWeight.boneIndex1 += num;
        boneWeight.boneIndex2 += num;
        boneWeight.boneIndex3 += num;
        this.boneWeights.Add(boneWeight);
      }
      num += smRenderer.bones.Length;
      foreach (Transform bone in smRenderer.bones)
        this.bones.Add(bone);
      CombineInstance combineInstance = new CombineInstance();
      combineInstance.mesh = smRenderer.sharedMesh;
      this.meshIndex[index] = combineInstance.mesh.vertexCount;
      combineInstance.transform = smRenderer.transform.localToWorldMatrix;
      this.combineInstances.Add(combineInstance);
      smRenderer.gameObject.SetActive(false);
    }
    this.bindposes = new List<Matrix4x4>();
    for (int index = 0; index < this.bones.Count; ++index)
      this.bindposes.Add(this.bones[index].worldToLocalMatrix * this.transform.worldToLocalMatrix);
    this.CombineMeshes();
  }

  public void CombineMeshes()
  {
    List<Texture2D> texture2DList = new List<Texture2D>();
    for (int index = 0; index < this.smRenderers.Count; ++index)
    {
      SkinnedMeshRenderer smRenderer = this.smRenderers[index];
      if ((Object) smRenderer.material.mainTexture != (Object) null)
        texture2DList.Add(smRenderer.GetComponent<Renderer>().material.mainTexture as Texture2D);
    }
    this.transform.parent.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.transform.localScale = new Vector3(1f, 1f, 1f);
    SkinnedMeshRenderer skinnedMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
    if ((Object) skinnedMeshRenderer == (Object) null)
      skinnedMeshRenderer = this.gameObject.AddComponent<SkinnedMeshRenderer>();
    skinnedMeshRenderer.sharedMesh = new UnityEngine.Mesh();
    skinnedMeshRenderer.sharedMesh.CombineMeshes(this.combineInstances.ToArray(), true, true);
    Texture2D texture2D = new Texture2D(128, 128);
    Rect[] rectArray = texture2D.PackTextures(texture2DList.ToArray(), 0);
    Vector2[] uv = skinnedMeshRenderer.sharedMesh.uv;
    Vector2[] vector2Array = new Vector2[uv.Length];
    int index1 = 0;
    int num = 0;
    for (int index2 = 0; index2 < vector2Array.Length; ++index2)
    {
      vector2Array[index2].x = Mathf.Lerp(rectArray[index1].xMin, rectArray[index1].xMax, uv[index2].x);
      vector2Array[index2].y = Mathf.Lerp(rectArray[index1].yMin, rectArray[index1].yMax, uv[index2].y);
      if (index2 >= this.meshIndex[index1] + num)
      {
        num += this.meshIndex[index1];
        ++index1;
      }
    }
    Material material = new Material(Shader.Find("Diffuse"));
    material.mainTexture = (Texture) texture2D;
    skinnedMeshRenderer.sharedMesh.uv = vector2Array;
    skinnedMeshRenderer.sharedMaterial = material;
    skinnedMeshRenderer.bones = this.bones.ToArray();
    skinnedMeshRenderer.sharedMesh.boneWeights = this.boneWeights.ToArray();
    skinnedMeshRenderer.sharedMesh.bindposes = this.bindposes.ToArray();
    skinnedMeshRenderer.sharedMesh.RecalculateBounds();
  }
}
