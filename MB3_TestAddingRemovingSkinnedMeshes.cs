// Decompiled with JetBrains decompiler
// Type: MB3_TestAddingRemovingSkinnedMeshes
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

public class MB3_TestAddingRemovingSkinnedMeshes : MonoBehaviour
{
  public MB3_MeshBaker meshBaker;
  public GameObject[] g;

  private void Start() => this.StartCoroutine(this.TestScript());

  private IEnumerator TestScript()
  {
    Debug.Log((object) "Test 1 adding 0,1,2");
    this.meshBaker.AddDeleteGameObjects(new GameObject[3]
    {
      this.g[0],
      this.g[1],
      this.g[2]
    }, (GameObject[]) null);
    this.meshBaker.Apply();
    this.meshBaker.meshCombiner.CheckIntegrity();
    yield return (object) new WaitForSeconds(3f);
    Debug.Log((object) "Test 2 remove 1 and add 3,4,5");
    this.meshBaker.AddDeleteGameObjects(new GameObject[3]
    {
      this.g[3],
      this.g[4],
      this.g[5]
    }, new GameObject[1]{ this.g[1] });
    this.meshBaker.Apply();
    this.meshBaker.meshCombiner.CheckIntegrity();
    yield return (object) new WaitForSeconds(3f);
    Debug.Log((object) "Test 3 remove 0,2,5 and add 1");
    this.meshBaker.AddDeleteGameObjects(new GameObject[1]
    {
      this.g[1]
    }, new GameObject[3]{ this.g[3], this.g[4], this.g[5] });
    this.meshBaker.Apply();
    this.meshBaker.meshCombiner.CheckIntegrity();
    yield return (object) new WaitForSeconds(3f);
    Debug.Log((object) "Test 3 remove all remaining");
    this.meshBaker.AddDeleteGameObjects((GameObject[]) null, new GameObject[3]
    {
      this.g[0],
      this.g[1],
      this.g[2]
    });
    this.meshBaker.Apply();
    this.meshBaker.meshCombiner.CheckIntegrity();
    yield return (object) new WaitForSeconds(3f);
    Debug.Log((object) "Test 3 add all");
    this.meshBaker.AddDeleteGameObjects(this.g, (GameObject[]) null);
    this.meshBaker.Apply();
    this.meshBaker.meshCombiner.CheckIntegrity();
    yield return (object) new WaitForSeconds(1f);
    Debug.Log((object) "Done");
  }
}
