// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Services.RemoteConfig.RemoteConfigGameService
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;

namespace ProEra.Game.Services.RemoteConfig
{
  public class RemoteConfigGameService : PersistentSingleton<RemoteConfigGameService>, IGameService
  {
    public event Action<RemoteConfigGameServiceResponse> FetchCompleted;

    protected override void Awake() => RemoteConfigService.Instance.FetchCompleted += new Action<ConfigResponse>(this.OnFetchCompleted);

    public override void OnDestroy() => RemoteConfigService.Instance.FetchCompleted -= new Action<ConfigResponse>(this.OnFetchCompleted);

    private void OnFetchCompleted(ConfigResponse response)
    {
      RemoteConfigGameServiceResponse gameServiceResponse = new RemoteConfigGameServiceResponse();
      gameServiceResponse.shouldApplyRemoteConfig = response.requestOrigin != 0;
      Action<RemoteConfigGameServiceResponse> fetchCompleted = this.FetchCompleted;
      if (fetchCompleted == null)
        return;
      fetchCompleted(gameServiceResponse);
    }

    public async Task InitializeAsync()
    {
      RuntimeConfig runtimeConfig = await RemoteConfigService.Instance.FetchConfigsAsync<UserAttributes, AppAttributes>((UserAttributes) null, (AppAttributes) null);
    }

    public static float GetFloatValue(string settingName, float defaultValue) => RemoteConfigService.Instance.appConfig.GetFloat(settingName, defaultValue);

    public static int GetIntValue(string settingName, int defaultValue) => RemoteConfigService.Instance.appConfig.GetInt(settingName, defaultValue);

    public static bool GetBoolValue(string settingName, bool defaultValue) => RemoteConfigService.Instance.appConfig.GetBool(settingName, defaultValue);

    public static string GetStringValue(string settingName, string defaultValue) => RemoteConfigService.Instance.appConfig.GetString(settingName, defaultValue);

    public static long GetLongValue(string settingName, long defaultValue) => RemoteConfigService.Instance.appConfig.GetLong(settingName, defaultValue);

    public static string GetJsonValue(string settingName, string defaultValue) => RemoteConfigService.Instance.appConfig.GetJson(settingName, defaultValue);
  }
}
