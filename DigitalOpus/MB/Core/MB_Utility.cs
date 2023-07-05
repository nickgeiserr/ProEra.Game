// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB_Utility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class MB_Utility
  {
    public static Texture2D createTextureCopy(Texture2D source)
    {
      Texture2D textureCopy = new Texture2D(source.width, source.height, TextureFormat.ARGB32, true);
      textureCopy.SetPixels(source.GetPixels());
      return textureCopy;
    }

    public static bool ArrayBIsSubsetOfA(object[] a, object[] b)
    {
      for (int index1 = 0; index1 < b.Length; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < a.Length; ++index2)
        {
          if (a[index2] == b[index1])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return false;
      }
      return true;
    }

    public static Material[] GetGOMaterials(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return (Material[]) null;
      Material[] sourceArray = (Material[]) null;
      Mesh mesh = (Mesh) null;
      MeshRenderer component1 = go.GetComponent<MeshRenderer>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      {
        sourceArray = component1.sharedMaterials;
        MeshFilter component2 = go.GetComponent<MeshFilter>();
        mesh = !((UnityEngine.Object) component2 == (UnityEngine.Object) null) ? component2.sharedMesh : throw new Exception("Object " + ((object) go)?.ToString() + " has a MeshRenderer but no MeshFilter.");
      }
      SkinnedMeshRenderer component3 = go.GetComponent<SkinnedMeshRenderer>();
      if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
      {
        sourceArray = component3.sharedMaterials;
        mesh = component3.sharedMesh;
      }
      if (sourceArray == null)
      {
        Debug.LogError((object) ("Object " + go.name + " does not have a MeshRenderer or a SkinnedMeshRenderer component"));
        return new Material[0];
      }
      if ((UnityEngine.Object) mesh == (UnityEngine.Object) null)
      {
        Debug.LogError((object) ("Object " + go.name + " has a MeshRenderer or SkinnedMeshRenderer but no mesh."));
        return new Material[0];
      }
      if (mesh.subMeshCount < sourceArray.Length)
      {
        Debug.LogWarning((object) ("Object " + ((object) go)?.ToString() + " has only " + mesh.subMeshCount.ToString() + " submeshes and has " + sourceArray.Length.ToString() + " materials. Extra materials do nothing."));
        Material[] destinationArray = new Material[mesh.subMeshCount];
        Array.Copy((Array) sourceArray, (Array) destinationArray, destinationArray.Length);
        sourceArray = destinationArray;
      }
      return sourceArray;
    }

    public static Mesh GetMesh(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return (Mesh) null;
      MeshFilter component1 = go.GetComponent<MeshFilter>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        return component1.sharedMesh;
      SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        return component2.sharedMesh;
      Debug.LogError((object) ("Object " + go.name + " does not have a MeshFilter or a SkinnedMeshRenderer component"));
      return (Mesh) null;
    }

    public static void SetMesh(GameObject go, Mesh m)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return;
      MeshFilter component1 = go.GetComponent<MeshFilter>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      {
        component1.sharedMesh = m;
      }
      else
      {
        SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
        if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
          return;
        component2.sharedMesh = m;
      }
    }

    public static Renderer GetRenderer(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return (Renderer) null;
      MeshRenderer component1 = go.GetComponent<MeshRenderer>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        return (Renderer) component1;
      SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
      return (UnityEngine.Object) component2 != (UnityEngine.Object) null ? (Renderer) component2 : (Renderer) null;
    }

    public static void DisableRendererInSource(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return;
      MeshRenderer component1 = go.GetComponent<MeshRenderer>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      {
        component1.enabled = false;
      }
      else
      {
        SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
        if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
          return;
        component2.enabled = false;
      }
    }

    public static bool hasOutOfBoundsUVs(Mesh m, ref Rect uvBounds)
    {
      MB_Utility.MeshAnalysisResult putResultHere = new MB_Utility.MeshAnalysisResult();
      int num = MB_Utility.hasOutOfBoundsUVs(m, ref putResultHere) ? 1 : 0;
      uvBounds = putResultHere.uvRect;
      return num != 0;
    }

    public static bool hasOutOfBoundsUVs(
      Mesh m,
      ref MB_Utility.MeshAnalysisResult putResultHere,
      int submeshIndex = -1,
      int uvChannel = 0)
    {
      if ((UnityEngine.Object) m == (UnityEngine.Object) null)
      {
        putResultHere.hasOutOfBoundsUVs = false;
        return putResultHere.hasOutOfBoundsUVs;
      }
      Vector2[] uvs;
      switch (uvChannel)
      {
        case 0:
          uvs = m.uv;
          break;
        case 1:
          uvs = m.uv2;
          break;
        case 2:
          uvs = m.uv3;
          break;
        default:
          uvs = m.uv4;
          break;
      }
      return MB_Utility.hasOutOfBoundsUVs(uvs, m, ref putResultHere, submeshIndex);
    }

    public static bool hasOutOfBoundsUVs(
      Vector2[] uvs,
      Mesh m,
      ref MB_Utility.MeshAnalysisResult putResultHere,
      int submeshIndex = -1)
    {
      putResultHere.hasUVs = true;
      if (uvs.Length == 0)
      {
        putResultHere.hasUVs = false;
        putResultHere.hasOutOfBoundsUVs = false;
        putResultHere.uvRect = new Rect();
        return putResultHere.hasOutOfBoundsUVs;
      }
      if (submeshIndex >= m.subMeshCount)
      {
        putResultHere.hasOutOfBoundsUVs = false;
        putResultHere.uvRect = new Rect();
        return putResultHere.hasOutOfBoundsUVs;
      }
      float x;
      float num1;
      float y;
      float num2;
      if (submeshIndex >= 0)
      {
        int[] triangles = m.GetTriangles(submeshIndex);
        if (triangles.Length == 0)
        {
          putResultHere.hasOutOfBoundsUVs = false;
          putResultHere.uvRect = new Rect();
          return putResultHere.hasOutOfBoundsUVs;
        }
        num1 = x = uvs[triangles[0]].x;
        num2 = y = uvs[triangles[0]].y;
        for (int index1 = 0; index1 < triangles.Length; ++index1)
        {
          int index2 = triangles[index1];
          if ((double) uvs[index2].x < (double) num1)
            num1 = uvs[index2].x;
          if ((double) uvs[index2].x > (double) x)
            x = uvs[index2].x;
          if ((double) uvs[index2].y < (double) num2)
            num2 = uvs[index2].y;
          if ((double) uvs[index2].y > (double) y)
            y = uvs[index2].y;
        }
      }
      else
      {
        num1 = x = uvs[0].x;
        num2 = y = uvs[0].y;
        for (int index = 0; index < uvs.Length; ++index)
        {
          if ((double) uvs[index].x < (double) num1)
            num1 = uvs[index].x;
          if ((double) uvs[index].x > (double) x)
            x = uvs[index].x;
          if ((double) uvs[index].y < (double) num2)
            num2 = uvs[index].y;
          if ((double) uvs[index].y > (double) y)
            y = uvs[index].y;
        }
      }
      Rect rect = new Rect();
      rect.x = num1;
      rect.y = num2;
      rect.width = x - num1;
      rect.height = y - num2;
      putResultHere.hasOutOfBoundsUVs = (double) x > 1.0 || (double) num1 < 0.0 || (double) y > 1.0 || (double) num2 < 0.0;
      putResultHere.uvRect = rect;
      return putResultHere.hasOutOfBoundsUVs;
    }

    public static void setSolidColor(Texture2D t, Color c)
    {
      Color[] pixels = t.GetPixels();
      for (int index = 0; index < pixels.Length; ++index)
        pixels[index] = c;
      t.SetPixels(pixels);
      t.Apply();
    }

    public static Texture2D resampleTexture(Texture2D source, int newWidth, int newHeight)
    {
      switch (source.format)
      {
        case TextureFormat.Alpha8:
        case TextureFormat.RGB24:
        case TextureFormat.RGBA32:
        case TextureFormat.ARGB32:
        case TextureFormat.DXT1:
        case TextureFormat.BGRA32:
          Texture2D texture2D = new Texture2D(newWidth, newHeight, TextureFormat.ARGB32, true);
          float num1 = (float) newWidth;
          float num2 = (float) newHeight;
          for (int x = 0; x < newWidth; ++x)
          {
            for (int y = 0; y < newHeight; ++y)
            {
              float u = (float) x / num1;
              float v = (float) y / num2;
              texture2D.SetPixel(x, y, source.GetPixelBilinear(u, v));
            }
          }
          texture2D.Apply();
          return texture2D;
        default:
          Debug.LogError((object) "Can only resize textures in formats ARGB32, RGBA32, BGRA32, RGB24, Alpha8 or DXT");
          return (Texture2D) null;
      }
    }

    public static bool AreAllSharedMaterialsDistinct(Material[] sharedMaterials)
    {
      for (int index1 = 0; index1 < sharedMaterials.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < sharedMaterials.Length; ++index2)
        {
          if ((UnityEngine.Object) sharedMaterials[index1] == (UnityEngine.Object) sharedMaterials[index2])
            return false;
        }
      }
      return true;
    }

    public static int doSubmeshesShareVertsOrTris(Mesh m, ref MB_Utility.MeshAnalysisResult mar)
    {
      MB_Utility.MB_Triangle mbTriangle1 = new MB_Utility.MB_Triangle();
      MB_Utility.MB_Triangle mbTriangle2 = new MB_Utility.MB_Triangle();
      int[][] numArray = new int[m.subMeshCount][];
      for (int submesh = 0; submesh < m.subMeshCount; ++submesh)
        numArray[submesh] = m.GetTriangles(submesh);
      bool flag1 = false;
      bool flag2 = false;
      for (int sIdx1 = 0; sIdx1 < m.subMeshCount; ++sIdx1)
      {
        int[] ts1 = numArray[sIdx1];
        for (int sIdx2 = sIdx1 + 1; sIdx2 < m.subMeshCount; ++sIdx2)
        {
          int[] ts2 = numArray[sIdx2];
          for (int idx1 = 0; idx1 < ts1.Length; idx1 += 3)
          {
            mbTriangle1.Initialize(ts1, idx1, sIdx1);
            for (int idx2 = 0; idx2 < ts2.Length; idx2 += 3)
            {
              mbTriangle2.Initialize(ts2, idx2, sIdx2);
              if (mbTriangle1.isSame((object) mbTriangle2))
              {
                flag2 = true;
                break;
              }
              if (mbTriangle1.sharesVerts(mbTriangle2))
              {
                flag1 = true;
                break;
              }
            }
          }
        }
      }
      if (flag2)
      {
        mar.hasOverlappingSubmeshVerts = true;
        mar.hasOverlappingSubmeshTris = true;
        return 2;
      }
      if (flag1)
      {
        mar.hasOverlappingSubmeshVerts = true;
        mar.hasOverlappingSubmeshTris = false;
        return 1;
      }
      mar.hasOverlappingSubmeshTris = false;
      mar.hasOverlappingSubmeshVerts = false;
      return 0;
    }

    public static bool GetBounds(GameObject go, out Bounds b)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "go paramater was null");
        b = new Bounds(Vector3.zero, Vector3.zero);
        return false;
      }
      Renderer renderer = MB_Utility.GetRenderer(go);
      if ((UnityEngine.Object) renderer == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "GetBounds must be called on an object with a Renderer");
        b = new Bounds(Vector3.zero, Vector3.zero);
        return false;
      }
      switch (renderer)
      {
        case MeshRenderer _:
          b = renderer.bounds;
          return true;
        case SkinnedMeshRenderer _:
          b = renderer.bounds;
          return true;
        default:
          Debug.LogError((object) "GetBounds must be called on an object with a MeshRender or a SkinnedMeshRenderer.");
          b = new Bounds(Vector3.zero, Vector3.zero);
          return false;
      }
    }

    public static void Destroy(UnityEngine.Object o)
    {
      if (Application.isPlaying)
        UnityEngine.Object.Destroy(o);
      else
        UnityEngine.Object.DestroyImmediate(o, false);
    }

    public struct MeshAnalysisResult
    {
      public Rect uvRect;
      public bool hasOutOfBoundsUVs;
      public bool hasOverlappingSubmeshVerts;
      public bool hasOverlappingSubmeshTris;
      public bool hasUVs;
      public float submeshArea;
    }

    private class MB_Triangle
    {
      private int submeshIdx;
      private int[] vs = new int[3];

      public bool isSame(object obj)
      {
        MB_Utility.MB_Triangle mbTriangle = (MB_Utility.MB_Triangle) obj;
        return this.vs[0] == mbTriangle.vs[0] && this.vs[1] == mbTriangle.vs[1] && this.vs[2] == mbTriangle.vs[2] && this.submeshIdx != mbTriangle.submeshIdx;
      }

      public bool sharesVerts(MB_Utility.MB_Triangle obj) => (this.vs[0] == obj.vs[0] || this.vs[0] == obj.vs[1] || this.vs[0] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx || (this.vs[1] == obj.vs[0] || this.vs[1] == obj.vs[1] || this.vs[1] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx || (this.vs[2] == obj.vs[0] || this.vs[2] == obj.vs[1] || this.vs[2] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx;

      public void Initialize(int[] ts, int idx, int sIdx)
      {
        this.vs[0] = ts[idx];
        this.vs[1] = ts[idx + 1];
        this.vs[2] = ts[idx + 2];
        this.submeshIdx = sIdx;
        Array.Sort<int>(this.vs);
      }
    }
  }
}
