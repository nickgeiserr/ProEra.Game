// Decompiled with JetBrains decompiler
// Type: UDB.Core
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using UnityEngine;

namespace UDB
{
  public class Core : SerializedSingletonBehaviour<Core, SerializedMonoBehaviour>
  {
    public static SceneState sceneState;
    private static Core self;

    private void Awake()
    {
      if ((Object) Core.self == (Object) null)
      {
        Core.self = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    private void Start() => UserSettingAudio.Load();
  }
}
