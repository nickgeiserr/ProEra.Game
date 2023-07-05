// Decompiled with JetBrains decompiler
// Type: MB3_MultiMeshBaker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using UnityEngine;

public class MB3_MultiMeshBaker : MB3_MeshBakerCommon
{
  [SerializeField]
  protected MB3_MultiMeshCombiner _meshCombiner = new MB3_MultiMeshCombiner();

  public override MB3_MeshCombiner meshCombiner => (MB3_MeshCombiner) this._meshCombiner;

  public override bool AddDeleteGameObjects(
    GameObject[] gos,
    GameObject[] deleteGOs,
    bool disableRendererInSource)
  {
    if ((Object) this._meshCombiner.resultSceneObject == (Object) null)
      this._meshCombiner.resultSceneObject = new GameObject("CombinedMesh-" + this.name);
    this.meshCombiner.name = this.name + "-mesh";
    return this._meshCombiner.AddDeleteGameObjects(gos, deleteGOs, disableRendererInSource);
  }

  public override bool AddDeleteGameObjectsByID(
    GameObject[] gos,
    int[] deleteGOs,
    bool disableRendererInSource)
  {
    if ((Object) this._meshCombiner.resultSceneObject == (Object) null)
      this._meshCombiner.resultSceneObject = new GameObject("CombinedMesh-" + this.name);
    this.meshCombiner.name = this.name + "-mesh";
    return this._meshCombiner.AddDeleteGameObjectsByID(gos, deleteGOs, disableRendererInSource);
  }
}
