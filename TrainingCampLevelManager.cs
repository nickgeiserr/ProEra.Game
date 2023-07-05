// Decompiled with JetBrains decompiler
// Type: TrainingCampLevelManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class TrainingCampLevelManager : MonoBehaviour
{
  [HideInInspector]
  public Transform PlayerStart;
  [HideInInspector]
  public Transform BallBucket;
  protected bool _gameStarted;

  private void Awake()
  {
    this.PlayerStart = this.transform.Find("PlayerStart");
    this.BallBucket = this.transform.Find("BallBucket");
  }

  public virtual void StartLevel() => this._gameStarted = true;

  public virtual void GameOver() => this._gameStarted = false;
}
