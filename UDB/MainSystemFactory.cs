// Decompiled with JetBrains decompiler
// Type: UDB.MainSystemFactory
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using UnityEngine;

namespace UDB
{
  public class MainSystemFactory : SingletonBehaviour<MainSystemFactory, MonoBehaviour>
  {
    public GameObject projectCorePrefab;
    public GameObject musicDirectorPrefab;
    public GameObject debugManagerPrefab;

    protected override void OnInstanceInit() => Timer.Register(0.01f, new System.Action(this.TimerCallback));

    private void TimerCallback()
    {
      if (!SerializedSingletonBehaviour<Core, SerializedMonoBehaviour>.Exists())
        UnityEngine.Object.Instantiate<GameObject>(this.projectCorePrefab);
      if (!SingletonBehaviour<MusicDirector, MonoBehaviour>.Exists())
        UnityEngine.Object.Instantiate<GameObject>(this.musicDirectorPrefab);
      if (SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>.Exists())
        return;
      UnityEngine.Object.Instantiate<GameObject>(this.debugManagerPrefab);
    }

    private new void Awake() => Timer.Register(0.01f, new System.Action(this.TimerCallback));
  }
}
