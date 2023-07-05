// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Water.MeshContainer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UnityStandardAssets.Water
{
  public class MeshContainer
  {
    public Mesh mesh;
    public Vector3[] vertices;
    public Vector3[] normals;

    public MeshContainer(Mesh m)
    {
      this.mesh = m;
      this.vertices = m.vertices;
      this.normals = m.normals;
    }

    public void Update()
    {
      this.mesh.vertices = this.vertices;
      this.mesh.normals = this.normals;
    }
  }
}
