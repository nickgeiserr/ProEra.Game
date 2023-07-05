// Decompiled with JetBrains decompiler
// Type: PocketLevelManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using System.Collections;
using TB12;
using TB12.UI;
using UnityEngine;

public class PocketLevelManager : TrainingCampLevelManager
{
  public PocketLevelManager.PocketTarget[] PocketTargets;
  private float _spawnTimer;
  private int _currentTarget;

  private void Start() => PracticeTarget.OnTargetHit += new Action<int, bool, bool, PracticeTarget>(this.OnTargetHit);

  private void OnTargetHit(int arg1, bool arg2, bool arg3, PracticeTarget arg4) => UnityEngine.Object.FindObjectOfType<TrainingCampFlow>().OnTargetTouched();

  private void Update()
  {
    if (!this._gameStarted)
      return;
    this._spawnTimer += Time.deltaTime;
    if ((double) this._spawnTimer < (double) this.PocketTargets[this._currentTarget].SpawnDelay)
      return;
    int currentTarget = this._currentTarget;
    this.PocketTargets[this._currentTarget].target.SetActive(true);
    GameplayUI.PointTo(this.PocketTargets[this._currentTarget].target.transform, "");
    this.StartCoroutine(this.TurnOffTarget(currentTarget, this.PocketTargets[this._currentTarget].Lifetime));
    ++this._currentTarget;
    if (this._currentTarget < this.PocketTargets.Length)
      return;
    this._gameStarted = false;
  }

  private IEnumerator TurnOffTarget(int index, float lifeTime)
  {
    yield return (object) new WaitForSeconds(lifeTime);
    this.PocketTargets[index].target.SetActive(false);
  }

  public override void StartLevel()
  {
    this._currentTarget = 0;
    for (int index = 0; index < this.PocketTargets.Length; ++index)
      this.PocketTargets[index].target.SetActive(false);
    base.StartLevel();
  }

  [Serializable]
  public struct PocketTarget
  {
    public GameObject target;
    public float SpawnDelay;
    public float Lifetime;
  }
}
