// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MBVersionConcrete
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class MBVersionConcrete : MBVersionInterface
  {
    private Vector2 _HALF_UV = new Vector2(0.5f, 0.5f);

    public string version() => "3.23.2";

    public int GetMajorVersion() => int.Parse(Application.unityVersion.Split('.')[0]);

    public int GetMinorVersion() => int.Parse(Application.unityVersion.Split('.')[1]);

    public bool GetActive(GameObject go) => go.activeInHierarchy;

    public void SetActive(GameObject go, bool isActive) => go.SetActive(isActive);

    public void SetActiveRecursively(GameObject go, bool isActive) => go.SetActive(isActive);

    public UnityEngine.Object[] FindSceneObjectsOfType(System.Type t) => UnityEngine.Object.FindObjectsOfType(t);

    public void OptimizeMesh(Mesh m)
    {
    }

    public bool IsRunningAndMeshNotReadWriteable(Mesh m) => Application.isPlaying && !m.isReadable;

    public Vector2[] GetMeshUV1s(Mesh m, MB2_LogLevel LOG_LEVEL)
    {
      if (LOG_LEVEL >= MB2_LogLevel.warn)
        MB2_Log.LogDebug("UV1 does not exist in Unity 5+");
      Vector2[] meshUv1s = m.uv;
      if (meshUv1s.Length == 0)
      {
        if (LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no uv1s. Generating");
        if (LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) ("Mesh " + ((object) m)?.ToString() + " didn't have uv1s. Generating uv1s."));
        meshUv1s = new Vector2[m.vertexCount];
        for (int index = 0; index < meshUv1s.Length; ++index)
          meshUv1s[index] = this._HALF_UV;
      }
      return meshUv1s;
    }

    public Vector2[] GetMeshUV3orUV4(Mesh m, bool get3, MB2_LogLevel LOG_LEVEL)
    {
      Vector2[] meshUv3orUv4 = !get3 ? m.uv4 : m.uv3;
      if (meshUv3orUv4.Length == 0)
      {
        if (LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Mesh " + ((object) m)?.ToString() + " has no uv" + (get3 ? "3" : "4") + ". Generating");
        meshUv3orUv4 = new Vector2[m.vertexCount];
        for (int index = 0; index < meshUv3orUv4.Length; ++index)
          meshUv3orUv4[index] = this._HALF_UV;
      }
      return meshUv3orUv4;
    }

    public void MeshClear(Mesh m, bool t) => m.Clear(t);

    public void MeshAssignUV3(Mesh m, Vector2[] uv3s) => m.uv3 = uv3s;

    public void MeshAssignUV4(Mesh m, Vector2[] uv4s) => m.uv4 = uv4s;

    public Vector4 GetLightmapTilingOffset(Renderer r) => r.lightmapScaleOffset;

    public Transform[] GetBones(Renderer r)
    {
      switch (r)
      {
        case SkinnedMeshRenderer _:
          return ((SkinnedMeshRenderer) r).bones;
        case MeshRenderer _:
          return new Transform[1]{ r.transform };
        default:
          Debug.LogError((object) "Could not getBones. Object does not have a renderer");
          return (Transform[]) null;
      }
    }

    public int GetBlendShapeFrameCount(Mesh m, int shapeIndex) => m.GetBlendShapeFrameCount(shapeIndex);

    public float GetBlendShapeFrameWeight(Mesh m, int shapeIndex, int frameIndex) => m.GetBlendShapeFrameWeight(shapeIndex, frameIndex);

    public void GetBlendShapeFrameVertices(
      Mesh m,
      int shapeIndex,
      int frameIndex,
      Vector3[] vs,
      Vector3[] ns,
      Vector3[] ts)
    {
      m.GetBlendShapeFrameVertices(shapeIndex, frameIndex, vs, ns, ts);
    }

    public void ClearBlendShapes(Mesh m) => m.ClearBlendShapes();

    public void AddBlendShapeFrame(
      Mesh m,
      string nm,
      float wt,
      Vector3[] vs,
      Vector3[] ns,
      Vector3[] ts)
    {
      m.AddBlendShapeFrame(nm, wt, vs, ns, ts);
    }
  }
}
