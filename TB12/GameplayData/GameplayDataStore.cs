// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.GameplayDataStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.GameplayData
{
  [CreateAssetMenu(menuName = "TB12/Stores/GameplayDataStore")]
  public class GameplayDataStore : ScriptableObject
  {
    public long timeStamp;
    public GameplayLevels GameplayLevels;
    private readonly Dictionary<string, List<PassScenario>> _passScenarios = new Dictionary<string, List<PassScenario>>();
    private readonly Dictionary<string, PassRoute> _routes = new Dictionary<string, PassRoute>();
    private readonly Dictionary<string, ReceiverRoute> _routeDatas = new Dictionary<string, ReceiverRoute>();
    private bool _initialized;
    public const string gameplayDataPath = "Assets/5-Model/Data/P_GameplayLevels.asset";

    private void OnEnable() => this._initialized = false;

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      this._passScenarios.Clear();
      foreach (PassScenario passSet in this.GameplayLevels.PassSets)
      {
        List<PassScenario> passScenarioList;
        if (!this._passScenarios.TryGetValue(passSet.id, out passScenarioList))
          this._passScenarios.Add(passSet.id, passScenarioList = new List<PassScenario>());
        passScenarioList.Add(passSet);
      }
      foreach (KeyValuePair<string, List<PassScenario>> passScenario in this._passScenarios)
        passScenario.Value.Sort((Comparison<PassScenario>) ((x, y) => x.order - y.order));
      this._routes.Clear();
      foreach (PassRoute passRoute in this.GameplayLevels.RouteMapping)
      {
        this._routes[passRoute.id] = passRoute;
        if (!string.IsNullOrEmpty(passRoute.routeName))
          passRoute.routeName = passRoute.routeName.Trim().ToLower();
      }
      this._routeDatas.Clear();
      foreach (ReceiverRoute routeData in this.GameplayLevels.RouteDatas)
        this._routeDatas[routeData.name.Trim().ToLower()] = routeData;
    }

    public CatchChallenge GetCatchingChallenge(string id) => this.GameplayLevels.CatchLevels.Find<CatchChallenge>((Predicate<CatchChallenge>) (x => x.id == id));

    public void UpdateGameplayData(GameplayLevels data, long timestamp)
    {
      if ((UnityEngine.Object) data == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "Attempting to set null GameplayData");
      }
      else
      {
        GameplayLevels gameplayLevels = this.GameplayLevels;
        this.GameplayLevels = data;
        this.timeStamp = timestamp;
        this._initialized = false;
        this._passScenarios.Clear();
        this._routeDatas.Clear();
        this._routes.Clear();
      }
    }

    public List<PassScenario> GetPassScenarios(string id)
    {
      List<PassScenario> passScenarioList;
      return !this._passScenarios.TryGetValue(id, out passScenarioList) ? (List<PassScenario>) null : passScenarioList;
    }

    public PassChallenge GetPassingChallenge(string id) => this.GameplayLevels.PassLevels.Find<PassChallenge>((Predicate<PassChallenge>) (x => x.id == id));

    public AgilityChallenge GetAgilityChallenge(string id) => this.GameplayLevels.AgilityLevels.Find<AgilityChallenge>((Predicate<AgilityChallenge>) (x => x.id == id));

    public AgilityChallengeV1 GetAgilityChallengeV1(string id) => this.GameplayLevels.AgilityLevelsV1.Find<AgilityChallengeV1>((Predicate<AgilityChallengeV1>) (x => x.id == id));

    public ThrowLevel GetThrowChallenge(string id) => this.GameplayLevels.ThrowLevels.Find<ThrowLevel>((Predicate<ThrowLevel>) (x => x.id == id));

    public ReceiverRoute GetRoute(string routeName)
    {
      PassRoute passRoute;
      if (!this._routes.TryGetValue(routeName, out passRoute))
        return (ReceiverRoute) null;
      ReceiverRoute receiverRoute;
      return !this._routeDatas.TryGetValue(passRoute.routeName, out receiverRoute) ? (ReceiverRoute) null : receiverRoute;
    }

    public List<ReceiverRoute> GetRoutes(IEnumerable<string> scenarioRoutes)
    {
      List<ReceiverRoute> routes = new List<ReceiverRoute>();
      foreach (string scenarioRoute in scenarioRoutes)
      {
        ReceiverRoute route = this.GetRoute(scenarioRoute);
        if (route != null)
          routes.Add(route);
      }
      return routes;
    }
  }
}
