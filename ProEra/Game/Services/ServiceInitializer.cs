// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Services.ServiceInitializer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace ProEra.Game.Services
{
  public class ServiceInitializer : MonoBehaviour
  {
    [SerializeField]
    private MonoBehaviour[] Services;
    private const string Environment = "production";

    private async void Start()
    {
      await this.InitializeServicesAsync();
      MonoBehaviour[] monoBehaviourArray = this.Services;
      for (int index = 0; index < monoBehaviourArray.Length; ++index)
      {
        MonoBehaviour monoBehaviour = monoBehaviourArray[index];
        if (monoBehaviour is IGameService gameService)
          await gameService.InitializeAsync();
        else
          Debug.LogError((object) ("ServiceInitializer references \"" + ((Object) monoBehaviour != (Object) null ? monoBehaviour.name : "<unnamed>") + "\" which does not inherit from IGameService.)"));
      }
      monoBehaviourArray = (MonoBehaviour[]) null;
    }

    private async Task InitializeServicesAsync()
    {
      if (!Utilities.CheckForInternetConnection())
        return;
      InitializationOptions initializationOptions = new InitializationOptions();
      initializationOptions.SetEnvironmentName("production");
      await UnityServices.InitializeAsync(initializationOptions);
      if (AuthenticationService.Instance.IsSignedIn)
        return;
      await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
  }
}
