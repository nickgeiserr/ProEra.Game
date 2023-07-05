// Decompiled with JetBrains decompiler
// Type: PlayerCollisions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MovementEffects;
using RootMotion.Dynamics;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class PlayerCollisions : CachedMonoBehaviour
{
  public PlayerAI myAI;
  public BehaviourPuppet behaviour;
  public PuppetMaster puppet;
  private Collider[] children;
  public int numOfCollisions;
  private List<PlayerCollisions.MuscleColliders> muscleColliders = new List<PlayerCollisions.MuscleColliders>();

  private void Start() => this.enabled = false;

  public void Initialize()
  {
    MatchManager.instance.playManager.OnPlayActiveEvent += new Action<bool>(this.ResetCount);
    this.myAI.IsTackledEvent += new PlayerAI.OnIsTackledEvent(this.IsTackledChanged);
  }

  private void OnDisable()
  {
    MatchManager.instance.playManager.OnPlayActiveEvent -= new Action<bool>(this.ResetCount);
    if (!((UnityEngine.Object) this.myAI != (UnityEngine.Object) null))
      return;
    this.myAI.IsTackledEvent -= new PlayerAI.OnIsTackledEvent(this.IsTackledChanged);
  }

  public void ResetCount(bool playActive)
  {
    if (!playActive)
      return;
    this.numOfCollisions = 0;
    for (int index = 0; index < this.muscleColliders.Count; ++index)
    {
      this.muscleColliders[index].inCollision = false;
      this.muscleColliders[index].muscle.props.pinWeight = 1f;
      this.muscleColliders[index].muscle.props.muscleWeight = 1f;
    }
  }

  public void IsTackledChanged(bool tackled)
  {
    if (!tackled)
      return;
    for (int index = 0; index < this.puppet.muscles.Length; ++index)
    {
      this.puppet.muscles[index].props.muscleWeight = 0.5f;
      this.puppet.muscles[index].props.pinWeight = 1f;
    }
  }

  public void EnterCollision(Collision collisionInfo, int index)
  {
    if ((UnityEngine.Object) this.myAI == (UnityEngine.Object) null || this.myAI.IsTackled || this.myAI.isTackling)
      return;
    if (collisionInfo.gameObject.layer == 17)
    {
      if (!((UnityEngine.Object) MatchManager.instance.playManager != (UnityEngine.Object) null) || !Game.IsPlayActive)
        return;
      PlayerAI playerAi = MatchManager.instance.playersManager.GetPlayerAI(collisionInfo.transform.root);
      if (!((UnityEngine.Object) playerAi != (UnityEngine.Object) null))
        return;
      this.myAI.OnCollisionEnter_Handler(playerAi.gameObject, playerAi);
    }
    else
    {
      if (collisionInfo.gameObject.layer != 12 && collisionInfo.gameObject.layer != 14 || this.muscleColliders[index].inCollision)
        return;
      switch (this.muscleColliders[index].muscle.props.group)
      {
        case Muscle.Group.Hips:
          return;
        case Muscle.Group.Spine:
          ++this.numOfCollisions;
          this.puppet.SetMuscleWeightsRecursive(HumanBodyBones.Chest, 1f, 0.5f);
          break;
        case Muscle.Group.Head:
          ++this.numOfCollisions;
          this.muscleColliders[index].muscle.props.pinWeight = 0.0f;
          break;
        case Muscle.Group.Arm:
          ++this.numOfCollisions;
          this.puppet.SetMuscleWeightsRecursive(HumanBodyBones.LeftUpperArm, 1f, 0.0f);
          this.puppet.SetMuscleWeightsRecursive(HumanBodyBones.RightUpperArm, 1f, 0.0f);
          break;
        case Muscle.Group.Hand:
          ++this.numOfCollisions;
          this.muscleColliders[index].muscle.props.pinWeight = 0.0f;
          break;
        case Muscle.Group.Leg:
          return;
        case Muscle.Group.Foot:
          return;
      }
      this.muscleColliders[index].inCollision = true;
      this.puppet.mappingWeight = 0.5f;
    }
  }

  public void OutCollision(Collision collisionInfo, int index)
  {
    if ((UnityEngine.Object) this.myAI == (UnityEngine.Object) null || this.numOfCollisions == 0 || !this.muscleColliders[index].inCollision || this.muscleColliders[index].muscle.props.group == Muscle.Group.Hips || this.muscleColliders[index].muscle.props.group == Muscle.Group.Leg || this.muscleColliders[index].muscle.props.group == Muscle.Group.Foot)
      return;
    --this.numOfCollisions;
    this.muscleColliders[index].inCollision = false;
    Timing.RunCoroutine(this._OutCollision(index), Segment.FixedUpdate);
  }

  private IEnumerator<float> _OutCollision(int index)
  {
    float weight = this.muscleColliders[index].muscle.props.pinWeight;
    while (!this.muscleColliders[index].inCollision && (double) weight < 1.0 && !this.myAI.IsTackled && !this.myAI.isTackling && Game.IsPlayActive)
    {
      weight += Time.fixedDeltaTime * 3f;
      switch (this.muscleColliders[index].muscle.props.group)
      {
        case Muscle.Group.Spine:
          this.puppet.SetMuscleWeightsRecursive(HumanBodyBones.Chest, 1f, weight);
          break;
        case Muscle.Group.Head:
          this.muscleColliders[index].muscle.props.pinWeight = weight;
          break;
        case Muscle.Group.Arm:
          this.puppet.SetMuscleWeightsRecursive(HumanBodyBones.LeftUpperArm, 1f, weight);
          this.puppet.SetMuscleWeightsRecursive(HumanBodyBones.RightUpperArm, 1f, weight);
          break;
        case Muscle.Group.Hand:
          this.muscleColliders[index].muscle.props.pinWeight = weight;
          break;
      }
      this.puppet.mappingWeight = 1f - weight;
      yield return 0.0f;
    }
  }

  public class MuscleColliders
  {
    public Muscle muscle;
    public Collider collider;
    public bool inCollision;

    public MuscleColliders(Muscle m, Collider c)
    {
      this.muscle = m;
      this.collider = c;
    }
  }
}
