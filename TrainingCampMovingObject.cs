// Decompiled with JetBrains decompiler
// Type: TrainingCampMovingObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class TrainingCampMovingObject : MonoBehaviour
{
  [SerializeField]
  public TrainingCampMovingObjectData MovementData;
  [HideInInspector]
  public Vector3 _startPosition;
  private int _movementDataIndex;
  private bool _increment;

  private void Start()
  {
    this._startPosition = this.transform.position;
    this._movementDataIndex = 0;
    this.MoveToNextPosition();
  }

  private void Update()
  {
  }

  public void MoveToNextPosition()
  {
    Debug.Log((object) nameof (MoveToNextPosition));
    if (this._movementDataIndex >= 0)
    {
      TrainingCampMovingObjectData.MovementData movementData = this.MovementData.MovementPattern[this._movementDataIndex];
      Vector3 vector3 = Vector3.zero;
      switch (movementData.direction)
      {
        case TrainingCampMovingObjectData.Direction.Left:
          vector3 = -this.transform.right;
          break;
        case TrainingCampMovingObjectData.Direction.Right:
          vector3 = this.transform.right;
          break;
        case TrainingCampMovingObjectData.Direction.Forward:
          vector3 = this.transform.forward;
          break;
        case TrainingCampMovingObjectData.Direction.Backward:
          vector3 = -this.transform.forward;
          break;
        case TrainingCampMovingObjectData.Direction.Up:
          vector3 = this.transform.up;
          break;
        case TrainingCampMovingObjectData.Direction.Down:
          vector3 = -this.transform.up;
          break;
      }
      LeanTween.move(this.gameObject, this.transform.position + vector3 * (float) movementData.distance, movementData.time).setEase(movementData.easeType).setOnComplete((System.Action) (() => this.MoveToNextPosition()));
    }
    else
    {
      TrainingCampMovingObjectData.MovementData movementData = this.MovementData.MovementPattern[0];
      LeanTween.move(this.gameObject, this._startPosition, movementData.time).setEase(movementData.easeType).setOnComplete((System.Action) (() => this.MoveToNextPosition()));
      this._increment = true;
    }
    if (this._increment)
      ++this._movementDataIndex;
    else
      --this._movementDataIndex;
    if (this._movementDataIndex < this.MovementData.MovementPattern.Length)
      return;
    if (this.MovementData.PatternLoopType == TrainingCampMovingObjectData.LoopType.Loop)
    {
      this._movementDataIndex = -1;
    }
    else
    {
      this._movementDataIndex = this.MovementData.MovementPattern.Length - 2;
      this._increment = !this._increment;
    }
  }
}
