// Decompiled with JetBrains decompiler
// Type: BakeTexturesAtRuntime
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using UnityEngine;

public class BakeTexturesAtRuntime : MonoBehaviour
{
  public GameObject target;
  private float elapsedTime;
  private MB3_TextureBaker.CreateAtlasesCoroutineResult result = new MB3_TextureBaker.CreateAtlasesCoroutineResult();

  private void OnGUI()
  {
    GUILayout.Label("Time to bake textures: " + this.elapsedTime.ToString());
    if (GUILayout.Button("Combine textures & build combined mesh all at once"))
    {
      MB3_MeshBaker componentInChildren = this.target.GetComponentInChildren<MB3_MeshBaker>();
      MB3_TextureBaker component = this.target.GetComponent<MB3_TextureBaker>();
      component.textureBakeResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
      component.resultMaterial = new Material(Shader.Find("Diffuse"));
      float realtimeSinceStartup = Time.realtimeSinceStartup;
      component.CreateAtlases();
      this.elapsedTime = Time.realtimeSinceStartup - realtimeSinceStartup;
      componentInChildren.ClearMesh();
      componentInChildren.textureBakeResults = component.textureBakeResults;
      componentInChildren.AddDeleteGameObjects(component.GetObjectsToCombine().ToArray(), (GameObject[]) null);
      componentInChildren.Apply();
    }
    if (!GUILayout.Button("Combine textures & build combined mesh using coroutine"))
      return;
    Debug.Log((object) ("Starting to bake textures on frame " + Time.frameCount.ToString()));
    MB3_TextureBaker component1 = this.target.GetComponent<MB3_TextureBaker>();
    component1.textureBakeResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
    component1.resultMaterial = new Material(Shader.Find("Diffuse"));
    component1.onBuiltAtlasesSuccess = new MB3_TextureBaker.OnCombinedTexturesCoroutineSuccess(this.OnBuiltAtlasesSuccess);
    this.StartCoroutine(component1.CreateAtlasesCoroutine((ProgressUpdateDelegate) null, this.result));
  }

  private void OnBuiltAtlasesSuccess()
  {
    Debug.Log((object) "Calling success callback. baking meshes");
    MB3_MeshBaker componentInChildren = this.target.GetComponentInChildren<MB3_MeshBaker>();
    MB3_TextureBaker component = this.target.GetComponent<MB3_TextureBaker>();
    if (this.result.isFinished && this.result.success)
    {
      componentInChildren.ClearMesh();
      componentInChildren.textureBakeResults = component.textureBakeResults;
      componentInChildren.AddDeleteGameObjects(component.GetObjectsToCombine().ToArray(), (GameObject[]) null);
      componentInChildren.Apply();
    }
    Debug.Log((object) ("Completed baking textures on frame " + Time.frameCount.ToString()));
  }
}
