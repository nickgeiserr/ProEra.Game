// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.GameplayLevels
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace TB12.GameplayData
{
  [CreateAssetMenu(fileName = "GameplayLevels", menuName = "TB12/Data/GameplayLevels", order = 1)]
  public class GameplayLevels : ScriptableObject
  {
    public GameLevel[] GameLevels;
    public ThrowLevel[] ThrowLevels;
    public PassChallenge[] PassLevels;
    public CatchChallenge[] CatchLevels;
    public AgilityChallenge[] AgilityLevels;
    public AgilityChallengeV1[] AgilityLevelsV1;
    public PassScenario[] PassSets;
    public PassRoute[] RouteMapping;
    public ReceiverRoute[] RouteDatas;

    public Dictionary<string, List<PassChallenge>> GetPassChallengesById(int id)
    {
      string str1 = string.Format("challenge0{0}", (object) id);
      string str2 = string.Format("challenge0{0}", (object) (id + 1));
      int num1 = -1;
      int num2 = -1;
      for (int index = 0; index < this.PassLevels.Length; ++index)
      {
        if (this.PassLevels[index].id == str1)
          num1 = index;
        if (this.PassLevels[index].id == str2)
          num2 = index;
        if (num1 >= 0 && num2 >= 0)
          break;
      }
      Dictionary<string, List<PassChallenge>> passChallengesById = new Dictionary<string, List<PassChallenge>>();
      for (int index = num1 + 1; index < num2; ++index)
      {
        string name = this.PassLevels[index].name;
        if (!passChallengesById.ContainsKey(name))
          passChallengesById.Add(name, new List<PassChallenge>());
        passChallengesById[name].Add(this.PassLevels[index]);
      }
      return passChallengesById;
    }

    public List<CatchChallenge> GetCatchChallenges()
    {
      List<CatchChallenge> catchChallenges = new List<CatchChallenge>();
      foreach (CatchChallenge catchLevel in this.CatchLevels)
        catchChallenges.Add(catchLevel);
      return catchChallenges;
    }
  }
}
