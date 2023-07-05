// Decompiled with JetBrains decompiler
// Type: MB_PrepareObjectsForDynamicBatchingDescription
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB_PrepareObjectsForDynamicBatchingDescription : MonoBehaviour
{
  private void OnGUI() => GUILayout.Label("This scene creates a combined material and meshes with adjusted UVs so objects \n can share a material and be batched by Unity's static/dynamic batching.\n Output has been set to 'bakeMeshAssetsInPlace' on the Mesh Baker\n Position, Scale and Rotation will be baked into meshes so place them appropriately.\n Dynamic batching requires objects with uniform scale. You can fix non-uniform scale here\n After baking you need to duplicate your source prefab assets and replace the  \n meshes and materials with the generated ones.\n");
}
