// Decompiled with JetBrains decompiler
// Type: TB12.GameplayManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using TB12.AppStates;
using TB12.GameplayData;
using UnityEngine;

namespace TB12
{
  public static class GameplayManager
  {
    public static void LoadLevelActivation(EGameMode mode, ETimeOfDay timeOfDay, string stadium = null)
    {
      string id = "Activation";
      if (mode != EGameMode.kThrow && mode != EGameMode.kAgility)
        id += ((int) (AppState.Difficulty.Level + 1)).ToString();
      GameplayManager.LoadLevel(mode, id, timeOfDay, stadium);
    }

    public static void LoadLevel(GameLevel level) => GameplayManager.LoadLevel(GameplayManager.GetGameMode(level.levelType), level.levelId, level.GetTimeOfDay(), level.stadium);

    private static EGameMode GetGameMode(string modeId)
    {
      modeId = modeId.ToLower();
      switch (modeId)
      {
        case "throw":
          return EGameMode.kThrow;
        case "catch":
          return EGameMode.kCatch;
        case "agility":
          return EGameMode.kAgility;
        case "pass":
          return EGameMode.kPass;
        case "axisgame":
          return EGameMode.kAxisGame;
        case "practice":
          return EGameMode.kPracticeMode;
        default:
          return EGameMode.kUnknown;
      }
    }

    public static void LoadLevel(EGameMode mode, string id, ETimeOfDay timeOfDay, string stadium = null)
    {
      if (mode == EGameMode.kUnknown)
        Debug.LogError((object) "Cannot load unknown mode");
      else if (string.IsNullOrEmpty(id))
      {
        Debug.LogError((object) "Cannot load empty level id");
      }
      else
      {
        EAppState state = GameplayManager.GetState(mode);
        if (state == EAppState.kUnknown)
          Debug.LogError((object) string.Format("Unhandled game mode {0}", (object) mode));
        else if ((Object) GameManager.Instance != (Object) null && GameManager.Instance.IsTransitioning())
        {
          Debug.LogError((object) "Already Transitioning");
        }
        else
        {
          AppState.GameMode = mode;
          AppState.LevelId = id;
          AppEvents.LoadState(state, timeOfDay, stadium);
        }
      }
    }

    private static EAppState GetState(EGameMode mode)
    {
      switch (mode)
      {
        case EGameMode.kThrow:
          return EAppState.kThrowGame;
        case EGameMode.kCatch:
          return EAppState.kCatchGame;
        case EGameMode.kAgility:
          return EAppState.kAgilityGame;
        case EGameMode.kAxisGame:
          return EAppState.kAxisGame;
        case EGameMode.kPracticeMode:
          return EAppState.kPracticeMode;
        case EGameMode.kMinigameMode:
          return EAppState.kMinigameMode;
        case EGameMode.kTrainingCamp:
          return EAppState.kTrainingCamp;
        case EGameMode.kAISimGameMode:
          return EAppState.kAISimGame;
        case EGameMode.kMiniCampQBPresence:
          return EAppState.kMiniCampQBPresence;
        case EGameMode.kMiniCampPrecisionPassing:
          return EAppState.kMiniCampPrecisionPassing;
        case EGameMode.kMiniCamRunAndShoot:
          return EAppState.kMiniCampRunAndShoot;
        case EGameMode.kOnboarding:
          return EAppState.kOnboarding;
        case EGameMode.kDebugAnimationSelection:
          return EAppState.kDebugAnimationSelection;
        case EGameMode.kMiniCampRollout:
          return EAppState.kMiniCampRollout;
        case EGameMode.k2MD:
          return EAppState.k2MD;
        case EGameMode.kHeroMoment:
          return EAppState.kHeroMoment;
        case EGameMode.kTunnel:
          return EAppState.kTunnel;
        case EGameMode.kPass:
          return EAppState.kPassGame;
        default:
          return EAppState.kUnknown;
      }
    }
  }
}
