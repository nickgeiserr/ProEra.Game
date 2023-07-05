// Decompiled with JetBrains decompiler
// Type: UDB.SceneRegistry
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class SceneRegistry : SingletonBehaviour<SceneRegistry, MonoBehaviour>
  {
    private static SceneRegistry self;
    public StringRegistry<int> _stringRegistry;
    public int _unqueID;
    public int nextID;

    public StringRegistry<int> stringRegistry
    {
      get
      {
        if (this._stringRegistry == null)
          this._stringRegistry = new StringRegistry<int>();
        return this._stringRegistry;
      }
    }

    public int uniqueID
    {
      get
      {
        this.nextID = this._unqueID;
        ++this._unqueID;
        return this.nextID;
      }
    }

    protected override void OnInstanceInit()
    {
      if ((Object) SceneRegistry.self == (Object) null)
      {
        SceneRegistry.self = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    private void _Register(string key)
    {
      if (this.stringRegistry.ContainsKey(key))
        return;
      this.stringRegistry.Register(key, this.uniqueID);
    }

    private bool _IsKeyRegistered(string key) => this.stringRegistry.ContainsKey(key);

    private int _ValueForKey(string key) => this.stringRegistry.GetValue(key);

    public static void Register(string key)
    {
      if (key.IsEmptyOrWhiteSpaceOrNull())
        return;
      SingletonBehaviour<SceneRegistry, MonoBehaviour>.instance._Register(key);
    }

    public static bool IsKeyRegistered(string key) => !key.IsEmptyOrWhiteSpaceOrNull() && SingletonBehaviour<SceneRegistry, MonoBehaviour>.instance._IsKeyRegistered(key);

    public static int ValueForKey(string key)
    {
      if (key.IsEmptyOrWhiteSpaceOrNull())
        return -1;
      if (!SceneRegistry.IsKeyRegistered(key))
        SceneRegistry.Register(key);
      return SingletonBehaviour<SceneRegistry, MonoBehaviour>.instance._ValueForKey(key);
    }
  }
}
