// Decompiled with JetBrains decompiler
// Type: GameplayConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game.Services.RemoteConfig;
using System;
using System.Collections.Generic;
using System.Reflection;

public class GameplayConfig : PersistentSingleton<GameplayConfig>
{
  private Dictionary<System.Type, IGameplayConfig> gameplayConfigs = new Dictionary<System.Type, IGameplayConfig>();

  public T GetConfig<T>() where T : IGameplayConfig
  {
    IGameplayConfig config;
    if (!this.gameplayConfigs.TryGetValue(typeof (T), out config))
    {
      config = this.gameObject.AddComponent(typeof (T)) as IGameplayConfig;
      this.gameplayConfigs[typeof (T)] = config;
    }
    return (T) config;
  }

  protected override void Awake()
  {
    base.Awake();
    this.GatherConfigItems();
    this.gameplayConfigs = new Dictionary<System.Type, IGameplayConfig>();
    foreach (IGameplayConfig component in this.GetComponents<IGameplayConfig>())
      this.gameplayConfigs[component.GetType()] = component;
    PersistentSingleton<RemoteConfigGameService>.Instance.FetchCompleted += new Action<RemoteConfigGameServiceResponse>(this.ApplyRemoteSettings);
  }

  public override void OnDestroy() => PersistentSingleton<RemoteConfigGameService>.Instance.FetchCompleted -= new Action<RemoteConfigGameServiceResponse>(this.ApplyRemoteSettings);

  private void ApplyRemoteSettings(RemoteConfigGameServiceResponse response)
  {
    if (!response.shouldApplyRemoteConfig)
      return;
    foreach (IGameplayConfig gameplayConfig in this.gameplayConfigs.Values)
    {
      if (gameplayConfig is IHasRemoteConfigSettings remoteConfigSettings)
        remoteConfigSettings.ApplyRemoteConfig();
    }
  }

  public bool GatherConfigItems()
  {
    bool flag = false;
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
      foreach (System.Type type in assembly.GetTypes())
      {
        if (typeof (IGameplayConfig).IsAssignableFrom(type) && type.IsClass && (UnityEngine.Object) this.gameObject != (UnityEngine.Object) null && (UnityEngine.Object) this.gameObject.GetComponent(type) == (UnityEngine.Object) null)
        {
          this.gameObject.AddComponent(type);
          flag = true;
        }
      }
    }
    return flag;
  }
}
