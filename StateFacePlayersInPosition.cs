// Decompiled with JetBrains decompiler
// Type: StateFacePlayersInPosition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class StateFacePlayersInPosition : BaseStateMachineBehaviour
{
  public float fieldOfView = 100f;
  public List<string> eligiblePositionGroups;
  public List<float> proximityTiers;
  private List<HashedString> _eligiblePositionGroups;
  private List<PlayerIdentity> _eligiblePlayers;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    if (this._eligiblePositionGroups == null)
    {
      this._eligiblePositionGroups = new List<HashedString>();
      for (int index = 0; index < this.eligiblePositionGroups.Count; ++index)
        this._eligiblePositionGroups.Add(new HashedString(this.eligiblePositionGroups[index]));
    }
    this._eligiblePlayers = (this._player.teamId == 2 ? this.gameboard.homePlayers : this.gameboard.awayPlayers).FindAll((Predicate<PlayerIdentity>) (x => this._eligiblePositionGroups.Contains(x.positionGroup)));
  }

  public override void OnStateUpdate(
    Animator animator,
    AnimatorStateInfo stateInfo,
    int layerIndex)
  {
    base.OnStateUpdate(animator, stateInfo, layerIndex);
    Vector3 localPosition1 = this._player.transform.localPosition;
    Quaternion rotation = this._telegraphy.transform.rotation;
    double x = (double) rotation.eulerAngles.y - (double) this.fieldOfView / 2.0;
    rotation = this._telegraphy.transform.rotation;
    double y = (double) rotation.eulerAngles.y + (double) this.fieldOfView / 2.0;
    Vector2 vector2 = new Vector2((float) x, (float) y);
    List<Vector3> vector3List1 = new List<Vector3>();
    for (int index = 0; index < this._eligiblePlayers.Count; ++index)
    {
      Vector3 localPosition2 = this._eligiblePlayers[index].transform.localPosition;
      if ((double) Vector3.Angle(this._player.transform.forward, localPosition2 - localPosition1) <= (double) this.fieldOfView / 2.0)
        vector3List1.Add(localPosition2);
    }
    if (vector3List1.Count <= 0)
      return;
    List<Vector3> vector3List2 = new List<Vector3>();
    for (int index1 = 0; index1 < this.proximityTiers.Count; ++index1)
    {
      for (int index2 = 0; index2 < vector3List1.Count; ++index2)
      {
        if ((double) Vector3.Distance(localPosition1, vector3List1[index2]) <= (double) this.proximityTiers[index1])
          vector3List2.Add(vector3List1[index2]);
      }
      if (vector3List2.Count > 0)
        break;
    }
    if (vector3List2.Count <= 0)
      return;
    Vector3 zero = Vector3.zero;
    for (int index = 0; index < vector3List2.Count; ++index)
      zero += vector3List2[index];
    this._telegraphy.BodyOrientation = Vector3.SignedAngle(Vector3.forward, zero / (float) vector3List2.Count - localPosition1, Vector3.up);
  }
}
