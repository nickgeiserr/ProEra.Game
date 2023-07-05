// Decompiled with JetBrains decompiler
// Type: UDB.GameObjectExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public static class GameObjectExtensions
  {
    public static Bounds GetCombinedBounds(this GameObject gameObject) => gameObject.transform.GetCombinedBounds();

    public static GameObject AddChild(this GameObject gameObject) => gameObject.AddChild("New Game Object");

    public static GameObject AddChild(this GameObject gameObject, string name)
    {
      GameObject gameObject1 = gameObject.AddChild((GameObject) null);
      gameObject1.name = name;
      return gameObject1;
    }

    public static GameObject AddChild(this GameObject gameObject, GameObject prefab)
    {
      GameObject gameObject1 = (UnityEngine.Object) prefab != (UnityEngine.Object) null ? UnityEngine.Object.Instantiate<GameObject>(prefab) : new GameObject();
      if ((UnityEngine.Object) gameObject1 == (UnityEngine.Object) null || (UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return gameObject1;
      Transform transform = gameObject1.transform;
      transform.SetParent(gameObject.transform);
      transform.Reset();
      gameObject1.layer = gameObject.layer;
      return gameObject1;
    }

    public static GameObject[] GetChildren(this GameObject gameObject)
    {
      Transform[] children1 = gameObject.transform.GetChildren();
      GameObject[] children2 = new GameObject[children1.Length];
      for (int index = 0; index < children1.Length; ++index)
        children2[index] = children1[index].gameObject;
      return children2;
    }

    public static GameObject GetChildWithName(this GameObject gameObject, string name)
    {
      GameObject[] children = gameObject.GetChildren();
      for (int index = 0; index < children.Length; ++index)
      {
        if (children[index].name.IsEqual(name))
          return children[index];
      }
      return (GameObject) null;
    }

    public static void StripCloneFromName(this GameObject gameObject) => gameObject.name = gameObject.GetNameWithoutClone();

    public static string GetNameWithoutClone(this GameObject gameObject)
    {
      string name = gameObject.name;
      int length = name.IndexOf("(Clone)", StringComparison.Ordinal);
      return length == -1 ? name : name.Substring(0, length);
    }
  }
}
