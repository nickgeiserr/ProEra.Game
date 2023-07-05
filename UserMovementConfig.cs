// Decompiled with JetBrains decompiler
// Type: UserMovementConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class UserMovementConfig : MonoBehaviour, IGameplayConfig
{
  [Tooltip("If the user is within this distance of a teammate, slow his move speed down")]
  [SerializeField]
  private float MaxYardsToTeammateForSlowdown = 1f;
  [Tooltip("If the user is running through a teammate, scale his speed by this much")]
  public float TeammateSlowdownSpeedScale = 0.33f;
  [Tooltip("IF the user is in front of a defender and this close, consider them tackled if they have the ball.")]
  [SerializeField]
  private float MaxYardsToOpponentForAutoTackle = 1f;
  [Header("Distance")]
  [Tooltip("As the user gets farther past the LOS, he will slow down gradually until he is this far, at which the slowdown will plateau.")]
  [SerializeField]
  private float YardsPastLOSForMaxSlowdown = 10f;
  [Tooltip("When the user is very far from the LOS, this is the smallest speed scale they will experience.")]
  public float SpeedScaleAtMaxDistanceFromLOS = 0.5f;

  [HideInInspector]
  public float MaxDistanceToTeammateForSlowdown => Field.ConvertYardsToDistance(this.MaxYardsToTeammateForSlowdown);

  [HideInInspector]
  public float MaxDistanceToOpponentForAutoTackle => Field.ConvertYardsToDistance(this.MaxYardsToOpponentForAutoTackle);

  [HideInInspector]
  public float DistancePastLOSForMaxSlowdown => Field.ConvertYardsToDistance(this.YardsPastLOSForMaxSlowdown);
}
