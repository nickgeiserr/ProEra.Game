// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.AgilityChallengeV1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace TB12.GameplayData
{
  [Serializable]
  public class AgilityChallengeV1
  {
    public string id;
    public float attackerSpeed = 5f;
    public float timeBetweenAttacks = 1.2f;
    public float minDistance = 4f;
    public float maxDistance = 8f;
    public int[] playersInWaves = new int[3]{ 2, 2, 3 };
    public float maxIncomingAngle = 45f;
    public int frenzyPlayerCount = 25;
    public float frenzyTimeDecrease = 0.01f;
    public int failCountAllowed = 3;
    public float frenzyTimeBetweenAttacks = 1f;
  }
}
