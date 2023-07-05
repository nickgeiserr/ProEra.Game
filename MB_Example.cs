// Decompiled with JetBrains decompiler
// Type: MB_Example
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB_Example : MonoBehaviour
{
  public MB3_MeshBaker meshbaker;
  public GameObject[] objsToCombine;

  private void Start()
  {
    this.meshbaker.AddDeleteGameObjects(this.objsToCombine, (GameObject[]) null);
    this.meshbaker.Apply();
  }

  private void LateUpdate()
  {
    this.meshbaker.UpdateGameObjects(this.objsToCombine);
    this.meshbaker.Apply(false, true, true, true, false, false, false, false, false);
  }

  private void OnGUI() => GUILayout.Label("Dynamically updates the vertices, normals and tangents in combined mesh every frame.\nThis is similar to dynamic batching. It is not recommended to do this every frame.\nAlso consider baking the mesh renderer objects into a skinned mesh renderer\nThe skinned mesh approach is faster for objects that need to move independently of each other every frame.");
}
