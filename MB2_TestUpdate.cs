// Decompiled with JetBrains decompiler
// Type: MB2_TestUpdate
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB2_TestUpdate : MonoBehaviour
{
  public MB3_MeshBaker meshbaker;
  public MB3_MultiMeshBaker multiMeshBaker;
  public GameObject[] objsToMove;
  public GameObject objWithChangingUVs;
  private Vector2[] uvs;
  private UnityEngine.Mesh m;

  private void Start()
  {
    this.meshbaker.AddDeleteGameObjects(this.objsToMove, (GameObject[]) null);
    this.meshbaker.AddDeleteGameObjects(new GameObject[1]
    {
      this.objWithChangingUVs
    }, (GameObject[]) null);
    this.m = this.objWithChangingUVs.GetComponent<MeshFilter>().sharedMesh;
    this.uvs = this.m.uv;
    this.meshbaker.Apply();
    this.multiMeshBaker.AddDeleteGameObjects(this.objsToMove, (GameObject[]) null);
    this.multiMeshBaker.AddDeleteGameObjects(new GameObject[1]
    {
      this.objWithChangingUVs
    }, (GameObject[]) null);
    this.m = this.objWithChangingUVs.GetComponent<MeshFilter>().sharedMesh;
    this.uvs = this.m.uv;
    this.multiMeshBaker.Apply();
  }

  private void LateUpdate()
  {
    this.meshbaker.UpdateGameObjects(this.objsToMove, false);
    Vector2[] uv1 = this.m.uv;
    for (int index = 0; index < uv1.Length; ++index)
      uv1[index] = Mathf.Sin(Time.time) * this.uvs[index];
    this.m.uv = uv1;
    this.meshbaker.UpdateGameObjects(new GameObject[1]
    {
      this.objWithChangingUVs
    }, updateUV: true);
    this.meshbaker.Apply(false, true, true, true, true, false, false, false, false);
    this.multiMeshBaker.UpdateGameObjects(this.objsToMove, false);
    Vector2[] uv2 = this.m.uv;
    for (int index = 0; index < uv2.Length; ++index)
      uv2[index] = Mathf.Sin(Time.time) * this.uvs[index];
    this.m.uv = uv2;
    this.multiMeshBaker.UpdateGameObjects(new GameObject[1]
    {
      this.objWithChangingUVs
    }, updateUV: true);
    this.multiMeshBaker.Apply(false, true, true, true, true, false, false, false, false);
  }
}
