// Decompiled with JetBrains decompiler
// Type: net.krej.Singleton.Singleton`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace net.krej.Singleton
{
  public class Singleton<T> : SingletonBase where T : MonoBehaviour
  {
    public const string LOG_TAG = "[singleton] ";
    private static T instance;

    public static T Instance
    {
      get
      {
        net.krej.Singleton.Singleton<T>.Instantiate();
        return net.krej.Singleton.Singleton<T>.instance;
      }
    }

    public static T InstanceOrNull => net.krej.Singleton.Singleton<T>.instance ?? (net.krej.Singleton.Singleton<T>.instance = (T) Object.FindObjectOfType(typeof (T)));

    public static bool Exists() => (Object) net.krej.Singleton.Singleton<T>.InstanceOrNull != (Object) null;

    public override void Elect() => net.krej.Singleton.Singleton<T>.instance = this as T;

    public static void Instantiate()
    {
      if ((object) net.krej.Singleton.Singleton<T>.instance != null)
        return;
      Object[] objectsOfType = Object.FindObjectsOfType(typeof (T));
      if (objectsOfType.Length > 1)
        Debug.LogError((object) ("[singleton] <B>Doubleton?</B> Do you really want two instances of <B><i>" + typeof (T).Name + "</i></B>?\n"), objectsOfType[1]);
      if (objectsOfType.Length >= 1)
        net.krej.Singleton.Singleton<T>.instance = (T) objectsOfType[0];
      if ((object) net.krej.Singleton.Singleton<T>.instance != null)
        return;
      Debug.Log((object) ("[singleton] Creating " + typeof (T).Name + " Singleton instance on the fly. \n\tTo have it's fields configured, add it manually to a GameObject"));
      net.krej.Singleton.Singleton<T>.instance = new GameObject(typeof (T).Name).AddComponent<T>();
      net.krej.Singleton.Singleton<T>.instance.SendMessage("OnInstantiate");
    }

    public static bool AreTooManyOnScene() => Object.FindObjectsOfType(typeof (T)).Length > 1;

    protected virtual void OnInstantiate()
    {
    }

    public static Transform STransform => net.krej.Singleton.Singleton<T>.Instance.transform;

    public static Vector3 SPosition
    {
      get => net.krej.Singleton.Singleton<T>.Instance.transform.position;
      set => net.krej.Singleton.Singleton<T>.Instance.transform.position = value;
    }

    public static GameObject SGameObject => net.krej.Singleton.Singleton<T>.Instance.gameObject;

    public static Rigidbody SRigidbody => net.krej.Singleton.Singleton<T>.Instance.GetComponent<Rigidbody>();

    public virtual void OnDestroy() => net.krej.Singleton.Singleton<T>.instance = default (T);
  }
}
