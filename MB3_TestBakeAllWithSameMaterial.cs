// Decompiled with JetBrains decompiler
// Type: MB3_TestBakeAllWithSameMaterial
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using UnityEngine;

public class MB3_TestBakeAllWithSameMaterial : MonoBehaviour
{
  public GameObject[] listOfObjsToCombineGood;
  public GameObject[] listOfObjsToCombineBad;

  private void Start() => this.testCombine();

  private void testCombine()
  {
    MB3_MeshCombinerSingle meshCombinerSingle = new MB3_MeshCombinerSingle();
    Debug.Log((object) "About to bake 1");
    meshCombinerSingle.AddDeleteGameObjects(this.listOfObjsToCombineGood, (GameObject[]) null, true);
    meshCombinerSingle.Apply();
    meshCombinerSingle.UpdateGameObjects(this.listOfObjsToCombineGood, true, true, true, true, false, false, false, false, false, false);
    meshCombinerSingle.Apply();
    meshCombinerSingle.AddDeleteGameObjects((GameObject[]) null, this.listOfObjsToCombineGood, true);
    meshCombinerSingle.Apply();
    Debug.Log((object) "Did bake 1");
    Debug.Log((object) "About to bake 2 should get error that one material doesn't match");
    meshCombinerSingle.AddDeleteGameObjects(this.listOfObjsToCombineBad, (GameObject[]) null, true);
    meshCombinerSingle.Apply();
    Debug.Log((object) "Did bake 2");
    Debug.Log((object) "Doing same with multi mesh combiner");
    MB3_MultiMeshCombiner multiMeshCombiner = new MB3_MultiMeshCombiner();
    Debug.Log((object) "About to bake 3");
    multiMeshCombiner.AddDeleteGameObjects(this.listOfObjsToCombineGood, (GameObject[]) null, true);
    multiMeshCombiner.Apply();
    multiMeshCombiner.UpdateGameObjects(this.listOfObjsToCombineGood, true, true, true, true, false, false, false, false, false, false);
    multiMeshCombiner.Apply();
    multiMeshCombiner.AddDeleteGameObjects((GameObject[]) null, this.listOfObjsToCombineGood, true);
    multiMeshCombiner.Apply();
    Debug.Log((object) "Did bake 3");
    Debug.Log((object) "About to bake 4  should get error that one material doesn't match");
    multiMeshCombiner.AddDeleteGameObjects(this.listOfObjsToCombineBad, (GameObject[]) null, true);
    multiMeshCombiner.Apply();
    Debug.Log((object) "Did bake 4");
  }
}
