// Decompiled with JetBrains decompiler
// Type: ForwardProgressTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ForwardProgressTracker : MonoBehaviour
{
  [HideInInspector]
  public float forwardProgressLine;
  [HideInInspector]
  public bool trackingIsActive;
  [HideInInspector]
  private BallManager ballManager;

  private void Awake()
  {
    this.forwardProgressLine = 0.0f;
    this.trackingIsActive = true;
    this.ballManager = this.gameObject.GetComponent<BallManager>();
  }

  private bool CanUpdateIfMovingBackward()
  {
    MatchManager instance = MatchManager.instance;
    PlayersManager playersManager = (Object) instance != (Object) null ? instance.playersManager : (PlayersManager) null;
    PlayerAI ballHolderScript = (Object) playersManager != (Object) null ? playersManager.ballHolderScript : (PlayerAI) null;
    return ((Object) ballHolderScript != (Object) null ? (ballHolderScript.nteractAgent.IsInsideInteraction ? 1 : 0) : 0) == 0;
  }

  private void Update()
  {
    if (!Game.IsPlayActive || !((Object) this.ballManager != (Object) null))
      return;
    float closestPointToEndzone = this.ballManager.GetClosestPointToEndzone();
    if (!Field.FurtherDownfield(closestPointToEndzone, this.forwardProgressLine) && !this.CanUpdateIfMovingBackward())
      return;
    this.forwardProgressLine = closestPointToEndzone;
  }

  public void OnDrawGizmos()
  {
    Gizmos.color = Game.IsPlayActive ? (this.CanUpdateIfMovingBackward() ? Color.white : Color.yellow) : Color.red;
    Gizmos.DrawLine(new Vector3(Field.LEFT_OUT_OF_BOUNDS, 0.0f, this.forwardProgressLine), new Vector3(Field.RIGHT_OUT_OF_BOUNDS, 0.0f, this.forwardProgressLine));
  }
}
