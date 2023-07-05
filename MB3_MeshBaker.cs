// Decompiled with JetBrains decompiler
// Type: MB3_MeshBaker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using UnityEngine;

public class MB3_MeshBaker : MB3_MeshBakerCommon
{
  [SerializeField]
  protected MB3_MeshCombinerSingle _meshCombiner = new MB3_MeshCombinerSingle();

  public override MB3_MeshCombiner meshCombiner => (MB3_MeshCombiner) this._meshCombiner;

  public void BuildSceneMeshObject() => this._meshCombiner.BuildSceneMeshObject();

  public virtual bool ShowHide(GameObject[] gos, GameObject[] deleteGOs) => this._meshCombiner.ShowHideGameObjects(gos, deleteGOs);

  public virtual void ApplyShowHide() => this._meshCombiner.ApplyShowHide();

  public override bool AddDeleteGameObjects(
    GameObject[] gos,
    GameObject[] deleteGOs,
    bool disableRendererInSource)
  {
    this._meshCombiner.name = this.name + "-mesh";
    return this._meshCombiner.AddDeleteGameObjects(gos, deleteGOs, disableRendererInSource);
  }

  public override bool AddDeleteGameObjectsByID(
    GameObject[] gos,
    int[] deleteGOinstanceIDs,
    bool disableRendererInSource)
  {
    this._meshCombiner.name = this.name + "-mesh";
    return this._meshCombiner.AddDeleteGameObjectsByID(gos, deleteGOinstanceIDs, disableRendererInSource);
  }
}
