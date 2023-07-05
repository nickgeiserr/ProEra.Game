// Decompiled with JetBrains decompiler
// Type: TeamPlayCalling
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class TeamPlayCalling
{
  [SerializeField]
  [Key(0)]
  public Dictionary<string, int> nameValuePairs = new Dictionary<string, int>();
  [IgnoreMember]
  public static string[] downString = new string[4]
  {
    "1stAnd",
    "2ndAnd",
    "3rdAnd",
    "4thAnd"
  };
  [IgnoreMember]
  public static string[] distanceString = new string[4]
  {
    "Close_",
    "Short_",
    "Medium_",
    "Long_"
  };
  [IgnoreMember]
  public static int CLOSE = 4;
  [IgnoreMember]
  public static int SHORT = 8;
  [IgnoreMember]
  public static int MEDIUM = 12;

  public void AddNameValuePair(string name, int value) => this.nameValuePairs.Add(name, value);

  public int LoadValue(string key)
  {
    if (this.nameValuePairs.ContainsKey(key))
      return this.nameValuePairs[key];
    Debug.Log((object) ("No value found for key: " + key));
    return 0;
  }

  public void SetValue(string key, int value)
  {
    if (!this.nameValuePairs.ContainsKey(key))
      Debug.Log((object) "Key not found in dictionary.");
    else
      this.nameValuePairs[key] = value;
  }

  public void ValidateDefaultValues()
  {
    for (int downIndex = 0; downIndex < 4; ++downIndex)
    {
      for (int distanceIndex = 0; distanceIndex < 4; ++distanceIndex)
      {
        string str = this.BuildKey(downIndex, distanceIndex);
        int num1 = this.LoadValue(str + "Run");
        int num2 = this.LoadValue(str + "Pass");
        int num3 = this.LoadValue(str + "InsideRun");
        int num4 = this.LoadValue(str + "OutsideRun");
        int num5 = this.LoadValue(str + "QbKeeper");
        int num6 = this.LoadValue(str + "ReadOption");
        int num7 = this.LoadValue(str + "ShortPass");
        int num8 = this.LoadValue(str + "MidPass");
        int num9 = this.LoadValue(str + "DeepPass");
        int num10 = this.LoadValue(str + "ScreenPass");
        int num11 = this.LoadValue(str + "PlayAction");
        int num12 = this.LoadValue(str + "ManCoverage");
        int num13 = this.LoadValue(str + "ManZoneDouble");
        int num14 = this.LoadValue(str + "CoverTwo");
        int num15 = this.LoadValue(str + "CoverThree");
        int num16 = this.LoadValue(str + "CoverFour");
        int num17 = this.LoadValue(str + "ManBlitz");
        int num18 = this.LoadValue(str + "ZoneBlitz");
        int num19 = num2;
        int num20 = num1 + num19;
        int num21 = num3 + num4 + num5 + num6;
        int num22 = num7 + num8 + num9 + num10 + num11;
        int num23 = num12 + num13 + num14 + num15 + num16 + num17 + num18;
        if (num20 != 100)
        {
          this.SetValue(str + "Run", 50);
          this.SetValue(str + "Pass", 50);
        }
        if (num21 != 100)
        {
          this.SetValue(str + "InsideRun", 50);
          this.SetValue(str + "OutsideRun", 50);
          this.SetValue(str + "QbKeeper", 0);
          this.SetValue(str + "ReadOption", 0);
        }
        if (num22 != 100)
        {
          this.SetValue(str + "ShortPass", 20);
          this.SetValue(str + "MidPass", 20);
          this.SetValue(str + "DeepPass", 20);
          this.SetValue(str + "ScreenPass", 20);
          this.SetValue(str + "PlayAction", 20);
        }
        if (num23 != 100)
        {
          this.SetValue(str + "ManCoverage", 30);
          this.SetValue(str + "ManZoneDouble", 10);
          this.SetValue(str + "CoverTwo", 15);
          this.SetValue(str + "CoverThree", 15);
          this.SetValue(str + "CoverFour", 10);
          this.SetValue(str + "ManBlitz", 10);
          this.SetValue(str + "ZoneBlitz", 10);
        }
      }
    }
  }

  public string BuildKey(int downIndex, int distanceIndex) => TeamPlayCalling.downString[downIndex] + TeamPlayCalling.distanceString[distanceIndex];

  private string BuildDownAndDistanceKey(int down, int distance)
  {
    if (down > 4)
      return "";
    string str = TeamPlayCalling.downString[down - 1];
    return distance >= TeamPlayCalling.CLOSE ? (distance >= TeamPlayCalling.SHORT ? (distance >= TeamPlayCalling.MEDIUM ? str + TeamPlayCalling.distanceString[3] : str + TeamPlayCalling.distanceString[2]) : str + TeamPlayCalling.distanceString[1]) : str + TeamPlayCalling.distanceString[0];
  }

  public PlayConcept SelectPlayConcept_Offense(int down, int distance)
  {
    string str = this.BuildDownAndDistanceKey(down, distance);
    int num1 = this.LoadValue(str + "Run");
    if (UnityEngine.Random.Range(0, 100) < num1)
    {
      int num2 = this.LoadValue(str + "InsideRun");
      int num3 = this.LoadValue(str + "OutsideRun") + num2;
      int num4 = this.LoadValue(str + "QbKeeper") + num3;
      this.LoadValue(str + "ReadOption");
      int num5 = UnityEngine.Random.Range(0, 100);
      if (num5 < num2)
        return PlayConcept.Inside_Run;
      if (num5 < num3)
        return PlayConcept.Outside_Run;
      return num5 < num4 ? PlayConcept.QB_Keeper : PlayConcept.Read_Option;
    }
    int num6 = this.LoadValue(str + "ShortPass");
    int num7 = this.LoadValue(str + "MidPass") + num6;
    int num8 = this.LoadValue(str + "DeepPass") + num7;
    int num9 = this.LoadValue(str + "ScreenPass") + num8;
    int num10 = UnityEngine.Random.Range(0, 100);
    if (num10 < num6)
      return PlayConcept.Short_Pass;
    if (num10 < num7)
      return PlayConcept.Mid_Pass;
    if (num10 < num8)
      return PlayConcept.Deep_Pass;
    return num10 < num9 ? PlayConcept.Screen_Pass : PlayConcept.Play_Action;
  }

  public PlayConcept SelectPlayConcept_Defense(int down, int distance)
  {
    string str1 = TeamPlayCalling.downString[down - 1];
    string str2 = distance >= TeamPlayCalling.CLOSE ? (distance >= TeamPlayCalling.SHORT ? (distance >= TeamPlayCalling.MEDIUM ? str1 + TeamPlayCalling.distanceString[3] : str1 + TeamPlayCalling.distanceString[2]) : str1 + TeamPlayCalling.distanceString[1]) : str1 + TeamPlayCalling.distanceString[0];
    int num1 = this.LoadValue(str2 + "ManCoverage");
    int num2 = this.LoadValue(str2 + "ManZoneDouble") + num1;
    int num3 = this.LoadValue(str2 + "CoverTwo") + num2;
    int num4 = this.LoadValue(str2 + "CoverThree") + num3;
    int num5 = this.LoadValue(str2 + "CoverFour") + num4;
    int num6 = this.LoadValue(str2 + "ManBlitz") + num5;
    int num7 = UnityEngine.Random.Range(0, 100);
    if (num7 < num1)
      return PlayConcept.Man_Coverage;
    if (num7 < num2)
      return PlayConcept.Man_Zone_Double;
    if (num7 < num3)
      return PlayConcept.Cover_Two;
    if (num7 < num4)
      return PlayConcept.Cover_Three;
    if (num7 < num5)
      return PlayConcept.Cover_Four;
    if (!Game.IsPlayerOneOnDefense && !GameSettings.GetDifficulty().OpponentCanBlitz)
      return PlayConcept.Cover_Two;
    return num7 < num6 ? PlayConcept.Man_Blitz : PlayConcept.Zone_Blitz;
  }
}
