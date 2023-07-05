// Decompiled with JetBrains decompiler
// Type: TwoPointConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class TwoPointConfig : MonoBehaviour, IGameplayConfig
{
  [Tooltip("Only consider going for two if there is less than this much time left in the game.")]
  public float maxSecondsLeftInGame = 240f;
  [Tooltip("Go for two when the point differential after scoring the touchdown is in this list.")]
  public int[] pointDifferentials;
}
