// Decompiled with JetBrains decompiler
// Type: HandoffConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class HandoffConfig : MonoBehaviour, IGameplayConfig
{
  [Tooltip("If the handoff should have happened, and the QB is this far from the target, consider the handoff to be aborted.")]
  [SerializeField]
  private float maxYardsFromTargetToAbortHandoff = 3f;
  [Header("Read Option")]
  [Tooltip("The probability that the handoff giver will keep the ball rather than handing it off to the receiver.")]
  [Range(0.0f, 1f)]
  public float chanceToKeepBall = 0.5f;

  public float MaxDistanceFromTargetToAbortHandoff => Field.ConvertYardsToDistance(this.maxYardsFromTargetToAbortHandoff);
}
