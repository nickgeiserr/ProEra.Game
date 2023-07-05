// Decompiled with JetBrains decompiler
// Type: MB_BatchPrepareObjectsForDynamicBatchingDescription
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB_BatchPrepareObjectsForDynamicBatchingDescription : MonoBehaviour
{
  private void OnGUI() => GUILayout.Label("This scene is set up to create a combined material and meshes with adjusted UVs so \n objects can share a material and be batched by Unity's static/dynamic batching.\n This scene has added a BatchPrefabBaker component to a Mesh and Material Baker which \n  can bake many prefabs (each of which can have several renderers) in one click.\n The batching tool accepts prefab assets instead of scene objects. \n");
}
