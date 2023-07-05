// Decompiled with JetBrains decompiler
// Type: DestinationDefinitions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;

public static class DestinationDefinitions
{
  public const string Oculus_Core_LockerRoom = "destination_Core_LockerRoom";
  public const string Oculus_Core_SeasonMode = "destination_Core_SeasonMode";
  public const string Oculus_Core_ExhibitionMode = "destination_Core_ExhibitionMode";
  public const string Oculus_Core_PracticeMode = "destination_Core_PracticeMode";
  public const string Oculus_Core_Onboarding = "destination_Core_Onboarding";
  public const string Oculus_SoloMinigame_PassingChallenge = "destination_Minigame_PassingChallenge";
  public const string Oculus_SoloMinigame_AgilityDrill = "destination_Minigame_AgilityDrill";
  public const string Oculus_SoloMinigame_TwoMinuteDrill = "destination_Minigame_TwoMinuteDrill";
  public const string Oculus_Multiplayer_Lobby = "destination_Multiplayer_Lobby";
  public const string Oculus_Multiplayer_BossMode = "destination_Multiplayer_BossMode";
  public const string Oculus_Multiplayer_Dodgeball = "destination_Multiplayer_Dodgeball";
  public const string Oculus_Multiplayer_ThrowingGame = "destination_Multiplayer_ThrowingGame";
  public const string Oculus_SoloMinicamp_PocketPassing_QbPresence = "destination_Minicamp_PocketPassing";
  public const string Oculus_SoloMinicamp_DimeDropping_PrecisionPassing = "destination_Minicamp_DimeDropping";
  public const string Oculus_SoloMinicamp_Rollout = "destination_Minicamp_Rollout";
  public const string Oculus_SoloMinicamp_RunAndShoot = "destination_Minicamp_RunAndShoot";
  public const string PSVR_Core_LockerRoom = "Locker Room";
  public const string PSVR_Core_SeasonMode = "Season Mode";
  public const string PSVR_Core_PracticeMode = "Practice Mode";
  public const string PSVR_Core_ExhibitionMode = "Exhibition Mode";
  public const string PSVR_Core_Onboarding = "Onboarding Training";
  public const string PSVR_SoloMinigame_PassingChallenge = "Passing Challenge";
  public const string PSVR_SoloMinigame_AgilityDrill = "Agility Drill";
  public const string PSVR_SoloMinigame_TwoMinuteDrill = "Two Minute Drill";
  public const string PSVR_Multiplayer_Lobby = "Multiplayer Lobby (Multiplayer)";
  public const string PSVR_Multiplayer_BossMode = "Boss Mode (Multiplayer)";
  public const string PSVR_Multiplayer_Dodgeball = "Dodgeball (Multiplayer)";
  public const string PSVR_Multiplayer_ThrowingGame = "Throwing Game (Multiplayer)";
  public const string PSVR_SoloMinicamp_PocketPassing_QbPresence = "Pocket Passing (Mini Camp Tours)";
  public const string PSVR_SoloMinicamp_DimeDropping_PrecisionPassing = "Dime Dropping (Mini Camp Tours)";
  public const string PSVR_SoloMinicamp_Rollout = "Rollout (Mini Camp Tours)";
  public const string PSVR_SoloMinicamp_RunAndShoot = "Run and Shoot (Mini Camp Tours)";
  public const string PSVR2_Core_LockerRoom = "LockerRoom";
  public const string PSVR2_Core_SeasonMode = "SeasonMode";
  public const string PSVR2_Core_PracticeMode = "PracticeMode";
  public const string PSVR2_Core_ExhibitionMode = "ExhibitionMode";
  public const string PSVR2_Core_Onboarding = "OnboardingTraining";
  public const string PSVR2_SoloMinigame_PassingChallenge = "PassingChallenge";
  public const string PSVR2_SoloMinigame_AgilityDrill = "AgilityDrill";
  public const string PSVR2_SoloMinigame_TwoMinuteDrill = "TwoMinuteDrill";
  public const string PSVR2_Multiplayer_Lobby = "MultiplayerLobby";
  public const string PSVR2_Multiplayer_BossMode = "BossMode";
  public const string PSVR2_Multiplayer_Dodgeball = "Dodgeball";
  public const string PSVR2_Multiplayer_ThrowingGame = "ThrowingGame";
  public const string PSVR2_SoloMinicamp_PocketPassing_QbPresence = "PocketPassing";
  public const string PSVR2_SoloMinicamp_DimeDropping_PrecisionPassing = "DimeDropping";
  public const string PSVR2_SoloMinicamp_Rollout = "Rollout";
  public const string PSVR2_SoloMinicamp_RunAndShoot = "RunAndShoot";
  public const string SteamVR_Core_LockerRoom = "#LockerRoom";
  public const string SteamVR_Core_SeasonMode = "#SeasonMode";
  public const string SteamVR_Core_PracticeMode = "#PracticeMode";
  public const string SteamVR_Core_ExhibitionMode = "#ExhibitionMode";
  public const string SteamVR_Core_Onboarding = "#OnboardingTraining";
  public const string SteamVR_SoloMinigame_PassingChallenge = "#PassingChallenge";
  public const string SteamVR_SoloMinigame_AgilityDrill = "#AgilityDrill";
  public const string SteamVR_SoloMinigame_TwoMinuteDrill = "#TwoMinuteDrill";
  public const string SteamVR_Multiplayer_Lobby = "#MultiplayerLobby";
  public const string SteamVR_Multiplayer_BossMode = "#BossMode";
  public const string SteamVR_Multiplayer_Dodgeball = "#Dodgeball";
  public const string SteamVR_Multiplayer_ThrowingGame = "#ThrowingGame";
  public const string SteamVR_SoloMinicamp_PocketPassing_QbPresence = "#PocketPassing";
  public const string SteamVR_SoloMinicamp_DimeDropping_PrecisionPassing = "#DimeDropping";
  public const string SteamVR_SoloMinicamp_Rollout = "#Rollout";
  public const string SteamVR_SoloMinicamp_RunAndShoot = "#RunAndShoot";

  public static string GetDestinationApiName(
    DestinationDefinitions.Destination whichDestination,
    EAppPlatform whichPlatform)
  {
    switch (whichPlatform)
    {
      case EAppPlatform.DesktopVR:
        switch (whichDestination)
        {
          case DestinationDefinitions.Destination.Core_LockerRoom:
            return "#LockerRoom";
          case DestinationDefinitions.Destination.Core_SeasonMode:
            return "#SeasonMode";
          case DestinationDefinitions.Destination.Core_ExhibitionMode:
            return "#ExhibitionMode";
          case DestinationDefinitions.Destination.Core_PracticeMode:
            return "#PracticeMode";
          case DestinationDefinitions.Destination.Core_Onboarding:
            return "#OnboardingTraining";
          case DestinationDefinitions.Destination.SoloMinigame_PassingChallenge:
            return "#PassingChallenge";
          case DestinationDefinitions.Destination.SoloMinigame_AgilityDrill:
            return "#AgilityDrill";
          case DestinationDefinitions.Destination.SoloMinigame_TwoMinuteDrill:
            return "#TwoMinuteDrill";
          case DestinationDefinitions.Destination.Multiplayer_Lobby:
            return "#MultiplayerLobby";
          case DestinationDefinitions.Destination.Multiplayer_BossMode:
            return "#BossMode";
          case DestinationDefinitions.Destination.Multiplayer_Dodgeball:
            return "#Dodgeball";
          case DestinationDefinitions.Destination.Multiplayer_ThrowingGame:
            return "#ThrowingGame";
          case DestinationDefinitions.Destination.SoloMinicamp_PocketPassing_QbPresence:
            return "#PocketPassing";
          case DestinationDefinitions.Destination.SoloMinicamp_DimeDropping_PrecisionPassing:
            return "#DimeDropping";
          case DestinationDefinitions.Destination.SoloMinicamp_Rollout:
            return "#Rollout";
          case DestinationDefinitions.Destination.SoloMinicamp_RunAndShoot:
            return "#RunAndShoot";
        }
        break;
      case EAppPlatform.PSVR:
        switch (whichDestination)
        {
          case DestinationDefinitions.Destination.Core_LockerRoom:
            return "Locker Room";
          case DestinationDefinitions.Destination.Core_SeasonMode:
            return "Season Mode";
          case DestinationDefinitions.Destination.Core_ExhibitionMode:
            return "Exhibition Mode";
          case DestinationDefinitions.Destination.Core_PracticeMode:
            return "Practice Mode";
          case DestinationDefinitions.Destination.Core_Onboarding:
            return "Onboarding Training";
          case DestinationDefinitions.Destination.SoloMinigame_PassingChallenge:
            return "Passing Challenge";
          case DestinationDefinitions.Destination.SoloMinigame_AgilityDrill:
            return "Agility Drill";
          case DestinationDefinitions.Destination.SoloMinigame_TwoMinuteDrill:
            return "Two Minute Drill";
          case DestinationDefinitions.Destination.Multiplayer_Lobby:
            return "Multiplayer Lobby (Multiplayer)";
          case DestinationDefinitions.Destination.Multiplayer_BossMode:
            return "Boss Mode (Multiplayer)";
          case DestinationDefinitions.Destination.Multiplayer_Dodgeball:
            return "Dodgeball (Multiplayer)";
          case DestinationDefinitions.Destination.Multiplayer_ThrowingGame:
            return "Throwing Game (Multiplayer)";
          case DestinationDefinitions.Destination.SoloMinicamp_PocketPassing_QbPresence:
            return "Pocket Passing (Mini Camp Tours)";
          case DestinationDefinitions.Destination.SoloMinicamp_DimeDropping_PrecisionPassing:
            return "Dime Dropping (Mini Camp Tours)";
          case DestinationDefinitions.Destination.SoloMinicamp_Rollout:
            return "Rollout (Mini Camp Tours)";
          case DestinationDefinitions.Destination.SoloMinicamp_RunAndShoot:
            return "Run and Shoot (Mini Camp Tours)";
        }
        break;
      case EAppPlatform.Quest2:
        switch (whichDestination)
        {
          case DestinationDefinitions.Destination.Core_LockerRoom:
            return "destination_Core_LockerRoom";
          case DestinationDefinitions.Destination.Core_SeasonMode:
            return "destination_Core_SeasonMode";
          case DestinationDefinitions.Destination.Core_ExhibitionMode:
            return "destination_Core_ExhibitionMode";
          case DestinationDefinitions.Destination.Core_PracticeMode:
            return "destination_Core_PracticeMode";
          case DestinationDefinitions.Destination.Core_Onboarding:
            return "destination_Core_Onboarding";
          case DestinationDefinitions.Destination.SoloMinigame_PassingChallenge:
            return "destination_Minigame_PassingChallenge";
          case DestinationDefinitions.Destination.SoloMinigame_AgilityDrill:
            return "destination_Minigame_AgilityDrill";
          case DestinationDefinitions.Destination.SoloMinigame_TwoMinuteDrill:
            return "destination_Minigame_TwoMinuteDrill";
          case DestinationDefinitions.Destination.Multiplayer_Lobby:
            return "destination_Multiplayer_Lobby";
          case DestinationDefinitions.Destination.Multiplayer_BossMode:
            return "destination_Multiplayer_BossMode";
          case DestinationDefinitions.Destination.Multiplayer_Dodgeball:
            return "destination_Multiplayer_Dodgeball";
          case DestinationDefinitions.Destination.Multiplayer_ThrowingGame:
            return "destination_Multiplayer_ThrowingGame";
          case DestinationDefinitions.Destination.SoloMinicamp_PocketPassing_QbPresence:
            return "destination_Minicamp_PocketPassing";
          case DestinationDefinitions.Destination.SoloMinicamp_DimeDropping_PrecisionPassing:
            return "destination_Minicamp_DimeDropping";
          case DestinationDefinitions.Destination.SoloMinicamp_Rollout:
            return "destination_Minicamp_Rollout";
          case DestinationDefinitions.Destination.SoloMinicamp_RunAndShoot:
            return "destination_Minicamp_RunAndShoot";
        }
        break;
      case EAppPlatform.PSVR2:
        switch (whichDestination)
        {
          case DestinationDefinitions.Destination.Core_LockerRoom:
            return "LockerRoom";
          case DestinationDefinitions.Destination.Core_SeasonMode:
            return "SeasonMode";
          case DestinationDefinitions.Destination.Core_ExhibitionMode:
            return "ExhibitionMode";
          case DestinationDefinitions.Destination.Core_PracticeMode:
            return "PracticeMode";
          case DestinationDefinitions.Destination.Core_Onboarding:
            return "OnboardingTraining";
          case DestinationDefinitions.Destination.SoloMinigame_PassingChallenge:
            return "PassingChallenge";
          case DestinationDefinitions.Destination.SoloMinigame_AgilityDrill:
            return "AgilityDrill";
          case DestinationDefinitions.Destination.SoloMinigame_TwoMinuteDrill:
            return "TwoMinuteDrill";
          case DestinationDefinitions.Destination.Multiplayer_Lobby:
            return "MultiplayerLobby";
          case DestinationDefinitions.Destination.Multiplayer_BossMode:
            return "BossMode";
          case DestinationDefinitions.Destination.Multiplayer_Dodgeball:
            return "Dodgeball";
          case DestinationDefinitions.Destination.Multiplayer_ThrowingGame:
            return "ThrowingGame";
          case DestinationDefinitions.Destination.SoloMinicamp_PocketPassing_QbPresence:
            return "PocketPassing";
          case DestinationDefinitions.Destination.SoloMinicamp_DimeDropping_PrecisionPassing:
            return "DimeDropping";
          case DestinationDefinitions.Destination.SoloMinicamp_Rollout:
            return "Rollout";
          case DestinationDefinitions.Destination.SoloMinicamp_RunAndShoot:
            return "RunAndShoot";
        }
        break;
    }
    return (string) null;
  }

  public static string GetMultiplayerID(string destinationApiName)
  {
    string empty = string.Empty;
    string multiplayerId;
    switch (destinationApiName)
    {
      case "#BossMode":
      case "Boss Mode (Multiplayer)":
      case "destination_Multiplayer_BossMode":
        multiplayerId = "2";
        break;
      case "#Dodgeball":
      case "Dodgeball (Multiplayer)":
      case "destination_Multiplayer_Dodgeball":
        multiplayerId = "1";
        break;
      case "#MultiplayerLobby":
      case "Multiplayer Lobby (Multiplayer)":
      case "destination_Multiplayer_Lobby":
        multiplayerId = "3";
        break;
      case "#ThrowingGame":
      case "Throwing Game (Multiplayer)":
      case "destination_Multiplayer_ThrowingGame":
        multiplayerId = "0";
        break;
      default:
        multiplayerId = "3";
        break;
    }
    return multiplayerId;
  }

  public enum Destination
  {
    Core_LockerRoom = 0,
    Core_SeasonMode = 1,
    Core_ExhibitionMode = 2,
    Core_PracticeMode = 3,
    Core_Onboarding = 4,
    SoloMinigame_PassingChallenge = 10, // 0x0000000A
    SoloMinigame_AgilityDrill = 11, // 0x0000000B
    SoloMinigame_TwoMinuteDrill = 12, // 0x0000000C
    Multiplayer_Lobby = 20, // 0x00000014
    Multiplayer_BossMode = 22, // 0x00000016
    Multiplayer_Dodgeball = 23, // 0x00000017
    Multiplayer_ThrowingGame = 24, // 0x00000018
    SoloMinicamp_PocketPassing_QbPresence = 31, // 0x0000001F
    SoloMinicamp_DimeDropping_PrecisionPassing = 32, // 0x00000020
    SoloMinicamp_Rollout = 33, // 0x00000021
    SoloMinicamp_RunAndShoot = 34, // 0x00000022
  }

  public enum Platform
  {
    SteamVR,
    Quest2,
    PSVR,
  }
}
