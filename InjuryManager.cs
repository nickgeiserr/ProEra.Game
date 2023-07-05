// Decompiled with JetBrains decompiler
// Type: InjuryManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class InjuryManager : MonoBehaviour
{
  private static int chance_MildConcussion = 4;
  private static int chance_SevereConcussion = 4;
  private static int chance_NeckStinger = 4;
  private static int chance_SeperatedShoulder = 5;
  private static int chance_DislocatedShoulder = 4;
  private static int chance_BrokenArm = 4;
  private static int chance_BrokenFinger = 4;
  private static int chance_HerniatedDisc = 5;
  private static int chance_SpinalCordConcussion = 7;
  private static int chance_Hamstring = 8;
  private static int chance_StrainedGroin = 7;
  private static int chance_LegContusion = 7;
  private static int chance_BrokenLeg = 5;
  private static int chance_SprainedACL = 5;
  private static int chance_TornACL = 4;
  private static int chance_TornMeniscus = 5;
  private static int chance_SprainedMCL = 5;
  private static int chance_TornMCL = 4;
  private static int chance_HighAnkleSprain = 3;
  private static int chance_SprainedAnkle = 3;
  private static int chance_TurfToe = 3;
  private static int chance_HeadInjury = 7;
  private static int chance_NeckInjury = 4;
  private static int chance_ShoulderInjury = 10;
  private static int chance_ArmInjury = 7;
  private static int chance_BackInjury = 12;
  private static int chance_LegInjury = 25;
  private static int chance_KneeInjury = 25;
  private static int chance_AnkleInjury = 6;
  private static int chance_FootInjury = 4;

  public static void CreateAndSetPlayerInjury(
    TeamData teamData,
    int playerIndex,
    bool allowMildGameInjures)
  {
    bool isGameInjury = true;
    if (!allowMildGameInjures)
      isGameInjury = false;
    else if (Random.Range(0, 100) < 50)
      isGameInjury = false;
    InjuryType injuryType = InjuryManager.DetermineInjuryType(Random.Range(0, 100), isGameInjury);
    string injuryCategory = InjuryManager.GetInjuryCategory(injuryType);
    string _recoveryTimeframe;
    int _weeksToRecover;
    int _playsToRecover;
    if (isGameInjury)
    {
      _recoveryTimeframe = "";
      _weeksToRecover = 0;
      _playsToRecover = Random.Range(5, 20);
    }
    else
    {
      _recoveryTimeframe = InjuryManager.GetRecoveryTimeframe(injuryType);
      _weeksToRecover = InjuryManager.GetWeeksToRecover(injuryType) + 1;
      _playsToRecover = 10000;
    }
    StartingPosition positionSpecific = teamData.TeamDepthChart.GetStartingPosition_Specific(playerIndex);
    Injury injury = new Injury(injuryType, injuryCategory, positionSpecific, _recoveryTimeframe, _weeksToRecover, _playsToRecover);
    teamData.GetPlayer(playerIndex).CurrentInjury = injury;
    if (teamData.GetPlayer(playerIndex).AllInjuries == null)
      teamData.GetPlayer(playerIndex).AllInjuries = new List<Injury>();
    teamData.GetPlayer(playerIndex).AllInjuries.Add(injury);
  }

  private static InjuryType DetermineInjuryType(int rand, bool isGameInjury)
  {
    if (isGameInjury)
    {
      int chanceHeadInjury = InjuryManager.chance_HeadInjury;
      if (rand < chanceHeadInjury)
        return InjuryType.Head_Injury;
      int chanceNeckInjury = InjuryManager.chance_NeckInjury;
      if (rand < chanceNeckInjury)
        return InjuryType.Neck_Injury;
      int chanceShoulderInjury = InjuryManager.chance_ShoulderInjury;
      if (rand < chanceShoulderInjury)
        return InjuryType.Shoulder_Injury;
      int chanceArmInjury = InjuryManager.chance_ArmInjury;
      if (rand < chanceArmInjury)
        return InjuryType.Arm_Injury;
      int chanceBackInjury = InjuryManager.chance_BackInjury;
      if (rand < chanceBackInjury)
        return InjuryType.Back_Injury;
      int chanceLegInjury = InjuryManager.chance_LegInjury;
      if (rand < chanceLegInjury)
        return InjuryType.Leg_Injury;
      int chanceKneeInjury = InjuryManager.chance_KneeInjury;
      if (rand < chanceKneeInjury)
        return InjuryType.Knee_Injury;
      int chanceAnkleInjury = InjuryManager.chance_AnkleInjury;
      if (rand < chanceAnkleInjury)
        return InjuryType.Ankle_Injury;
      int chanceFootInjury = InjuryManager.chance_FootInjury;
      return rand < chanceFootInjury ? InjuryType.Foot_Injury : InjuryType.Leg_Injury;
    }
    int chanceMildConcussion = InjuryManager.chance_MildConcussion;
    if (rand < chanceMildConcussion)
      return InjuryType.Mild_Concussion;
    int num1 = chanceMildConcussion + InjuryManager.chance_SevereConcussion;
    if (rand < num1)
      return InjuryType.Severe_Concussion;
    int num2 = num1 + InjuryManager.chance_NeckStinger;
    if (rand < num2)
      return InjuryType.Neck_Stinger;
    int num3 = num2 + InjuryManager.chance_SeperatedShoulder;
    if (rand < num3)
      return InjuryType.Separated_Shoulder;
    int num4 = num3 + InjuryManager.chance_DislocatedShoulder;
    if (rand < num4)
      return InjuryType.Dislocated_Shoulder;
    int num5 = num4 + InjuryManager.chance_BrokenArm;
    if (rand < num5)
      return InjuryType.Broken_Arm;
    int num6 = num5 + InjuryManager.chance_BrokenFinger;
    if (rand < num6)
      return InjuryType.Broken_Finger;
    int num7 = num6 + InjuryManager.chance_HerniatedDisc;
    if (rand < num7)
      return InjuryType.Herniated_Disc;
    int num8 = num7 + InjuryManager.chance_SpinalCordConcussion;
    if (rand < num8)
      return InjuryType.Spinal_Cord_Concussion;
    int num9 = num8 + InjuryManager.chance_Hamstring;
    if (rand < num9)
      return InjuryType.Hamstring;
    int num10 = num9 + InjuryManager.chance_StrainedGroin;
    if (rand < num10)
      return InjuryType.Strained_Groin;
    int num11 = num10 + InjuryManager.chance_LegContusion;
    if (rand < num11)
      return InjuryType.Leg_Contusion;
    int num12 = num11 + InjuryManager.chance_BrokenLeg;
    if (rand < num12)
      return InjuryType.Broken_Leg;
    int num13 = num12 + InjuryManager.chance_SprainedACL;
    if (rand < num13)
      return InjuryType.Sprained_ACL;
    int num14 = num13 + InjuryManager.chance_TornACL;
    if (rand < num14)
      return InjuryType.Torn_ACL;
    int num15 = num14 + InjuryManager.chance_TornMeniscus;
    if (rand < num15)
      return InjuryType.Torn_Meniscus;
    int num16 = num15 + InjuryManager.chance_SprainedMCL;
    if (rand < num16)
      return InjuryType.Sprained_MCL;
    int num17 = num16 + InjuryManager.chance_TornMCL;
    if (rand < num17)
      return InjuryType.Torn_MCL;
    int num18 = num17 + InjuryManager.chance_HighAnkleSprain;
    if (rand < num18)
      return InjuryType.High_Ankle_Sprain;
    int num19 = num18 + InjuryManager.chance_SprainedAnkle;
    if (rand < num19)
      return InjuryType.Sprained_Ankle;
    int num20 = num19 + InjuryManager.chance_TurfToe;
    return rand < num20 ? InjuryType.Turf_Toe : InjuryType.Hamstring;
  }

  private static string GetRecoveryTimeframe(InjuryType injury)
  {
    switch (injury)
    {
      case InjuryType.Mild_Concussion:
        return "1-2 WEEKS";
      case InjuryType.Severe_Concussion:
        return "2-5 WEEKS";
      case InjuryType.Neck_Stinger:
        return "1-2 WEEKS";
      case InjuryType.Separated_Shoulder:
        return "6-8 WEEKS";
      case InjuryType.Dislocated_Shoulder:
        return "4-12 MONTHS";
      case InjuryType.Broken_Arm:
        return "4-6 WEEKS";
      case InjuryType.Broken_Finger:
        return "4-6 WEEKS";
      case InjuryType.Herniated_Disc:
        return "6-12 WEEKS";
      case InjuryType.Spinal_Cord_Concussion:
        return "1-2 WEEKS";
      case InjuryType.Hamstring:
        return "1-6 WEEKS";
      case InjuryType.Strained_Groin:
        return "1-4 WEEKS";
      case InjuryType.Leg_Contusion:
        return "1-6 WEEKS";
      case InjuryType.Broken_Leg:
        return "6-8 WEEKS";
      case InjuryType.Sprained_ACL:
        return "4-10 WEEKS";
      case InjuryType.Torn_ACL:
        return "8-12 MONTHS";
      case InjuryType.Torn_Meniscus:
        return "1-6 WEEKS";
      case InjuryType.Sprained_MCL:
        return "2-4 WEEKS";
      case InjuryType.Torn_MCL:
        return "4-8 WEEKS";
      case InjuryType.High_Ankle_Sprain:
        return "4-10 WEEKS";
      case InjuryType.Sprained_Ankle:
        return "2-6 WEEKS";
      case InjuryType.Turf_Toe:
        return "3-4 WEEKS";
      default:
        Debug.Log((object) ("Unknown InjuryType Specified: " + injury.ToString()));
        return "";
    }
  }

  private static string GetInjuryCategory(InjuryType injury)
  {
    switch (injury)
    {
      case InjuryType.Mild_Concussion:
        return "HEAD INJURY";
      case InjuryType.Severe_Concussion:
        return "HEAD INJURY";
      case InjuryType.Neck_Stinger:
        return "NECK INJURY";
      case InjuryType.Separated_Shoulder:
        return "SHOULDER INJURY";
      case InjuryType.Dislocated_Shoulder:
        return "SHOUDLER INJURY";
      case InjuryType.Broken_Arm:
        return "ARM INJURY";
      case InjuryType.Broken_Finger:
        return "HAND INJURY";
      case InjuryType.Herniated_Disc:
        return "BACK INJURY";
      case InjuryType.Spinal_Cord_Concussion:
        return "BACK INJURY";
      case InjuryType.Hamstring:
        return "LEG INJURY";
      case InjuryType.Strained_Groin:
        return "LEG INJURY";
      case InjuryType.Leg_Contusion:
        return "LEG INJURY";
      case InjuryType.Broken_Leg:
        return "LEG INJURY";
      case InjuryType.Sprained_ACL:
        return "KNEE INJURY";
      case InjuryType.Torn_ACL:
        return "KNEE INJURY";
      case InjuryType.Torn_Meniscus:
        return "KNEE INJURY";
      case InjuryType.Sprained_MCL:
        return "KNEE INJURY";
      case InjuryType.Torn_MCL:
        return "KNEE INJURY";
      case InjuryType.High_Ankle_Sprain:
        return "ANKLE INJURY";
      case InjuryType.Sprained_Ankle:
        return "ANKLE INJURY";
      case InjuryType.Turf_Toe:
        return "FOOT INJURY";
      case InjuryType.Head_Injury:
        return "HEAD INJURY";
      case InjuryType.Neck_Injury:
        return "NECK INJURY";
      case InjuryType.Shoulder_Injury:
        return "SHOULDER INJURY";
      case InjuryType.Arm_Injury:
        return "ARM INJURY";
      case InjuryType.Back_Injury:
        return "BACK INJURY";
      case InjuryType.Leg_Injury:
        return "LEG INJURY";
      case InjuryType.Knee_Injury:
        return "KNEE INJURY";
      case InjuryType.Ankle_Injury:
        return "ANKLE INJURY";
      case InjuryType.Foot_Injury:
        return "FOOT INJURY";
      default:
        Debug.Log((object) ("Unknown InjuryType Specified: " + injury.ToString()));
        return "";
    }
  }

  private static int GetWeeksToRecover(InjuryType injury)
  {
    switch (injury)
    {
      case InjuryType.Mild_Concussion:
        return Random.Range(1, 3);
      case InjuryType.Severe_Concussion:
        return Random.Range(2, 6);
      case InjuryType.Neck_Stinger:
        return Random.Range(1, 3);
      case InjuryType.Separated_Shoulder:
        return Random.Range(6, 9);
      case InjuryType.Dislocated_Shoulder:
        return 40;
      case InjuryType.Broken_Arm:
        return Random.Range(4, 7);
      case InjuryType.Broken_Finger:
        return Random.Range(4, 7);
      case InjuryType.Herniated_Disc:
        return Random.Range(6, 13);
      case InjuryType.Spinal_Cord_Concussion:
        return Random.Range(1, 3);
      case InjuryType.Hamstring:
        return Random.Range(1, 7);
      case InjuryType.Strained_Groin:
        return Random.Range(1, 5);
      case InjuryType.Leg_Contusion:
        return Random.Range(1, 7);
      case InjuryType.Broken_Leg:
        return Random.Range(6, 9);
      case InjuryType.Sprained_ACL:
        return Random.Range(4, 11);
      case InjuryType.Torn_ACL:
        return 40;
      case InjuryType.Torn_Meniscus:
        return Random.Range(1, 7);
      case InjuryType.Sprained_MCL:
        return Random.Range(2, 5);
      case InjuryType.Torn_MCL:
        return Random.Range(4, 9);
      case InjuryType.High_Ankle_Sprain:
        return Random.Range(4, 11);
      case InjuryType.Sprained_Ankle:
        return Random.Range(2, 7);
      case InjuryType.Turf_Toe:
        return Random.Range(3, 5);
      default:
        Debug.Log((object) ("Unknown InjuryType Specified: " + injury.ToString()));
        return 1;
    }
  }
}
