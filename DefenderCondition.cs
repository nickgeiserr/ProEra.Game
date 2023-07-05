// Decompiled with JetBrains decompiler
// Type: DefenderCondition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NTeract/Slot Conditions/Defender Condition", fileName = "DefenderCondition")]
public class DefenderCondition : SlotCondition
{
  private static Dictionary<NteractAgent, PlayerAI> _playerAICache;

  private static void AddPlayerAIsToCache(
    Dictionary<NteractAgent, PlayerAI> cache,
    List<PlayerAI> playerAIList)
  {
    for (int index = 0; index < playerAIList.Count; ++index)
    {
      PlayerAI playerAi = playerAIList[index];
      cache.Add(playerAi.GetComponent<NteractAgent>(), playerAi);
    }
  }

  public override bool Evaluate(NteractAgent evaluatedAgent)
  {
    PlayerAI playerAi1 = (PlayerAI) null;
    if (DefenderCondition._playerAICache == null)
    {
      DefenderCondition._playerAICache = new Dictionary<NteractAgent, PlayerAI>();
      DefenderCondition.AddPlayerAIsToCache(DefenderCondition._playerAICache, MatchManager.instance.playersManager.curCompScriptRef);
      DefenderCondition.AddPlayerAIsToCache(DefenderCondition._playerAICache, MatchManager.instance.playersManager.curUserScriptRef);
    }
    else if (!DefenderCondition._playerAICache.TryGetValue(evaluatedAgent, out playerAi1))
    {
      DefenderCondition._playerAICache.Clear();
      DefenderCondition.AddPlayerAIsToCache(DefenderCondition._playerAICache, MatchManager.instance.playersManager.curCompScriptRef);
      DefenderCondition.AddPlayerAIsToCache(DefenderCondition._playerAICache, MatchManager.instance.playersManager.curUserScriptRef);
    }
    PlayerAI playerAi2 = (Object) playerAi1 == (Object) null ? DefenderCondition._playerAICache[evaluatedAgent] : playerAi1;
    GameObject ballHolder = MatchManager.instance.playersManager.ballHolder;
    if ((Object) ballHolder != (Object) null)
    {
      PlayerAI component = ballHolder.GetComponent<PlayerAI>();
      return playerAi2.teamIndex != component.teamIndex || playerAi2.onOffense != component.onOffense;
    }
    return !Game.IsKickoff ? (!Game.IsTurnover ? !playerAi2.onOffense : playerAi2.onOffense) : (!Game.IsTurnover ? playerAi2.onOffense : !playerAi2.onOffense);
  }
}
