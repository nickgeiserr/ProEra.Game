// Decompiled with JetBrains decompiler
// Type: TB12.UI.TrainingCampSuccessPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TB12.AppStates;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class TrainingCampSuccessPanel : SuccessPanel
  {
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _speedText;
    [SerializeField]
    private TextMeshProUGUI _bonusText;
    private int _currentScore;
    private int _nextScore;
    private float _scoreCountSpeed = 0.5f;
    private float _timer;
    private readonly RoutineHandle _resultsRoutine = new RoutineHandle();

    public override bool canContinue => true;

    protected override void WillAppear()
    {
      this.gameObject.SetActive(true);
      this._store = GameManager.Instance.CurrentGameplayStore;
      this._bonusText.text = this._store.LevelBonusScore.Value.ToString("#,##0");
      this._speedText.text = this._store.TimeBonusScore.Value.ToString("#,##0");
      this._scoreText.text = this._store.Score.Value.ToString("#,##0");
      this._resultsRoutine.Run(this.DoResultsAnimation());
    }

    private IEnumerator DoResultsAnimation()
    {
      RectTransform bonusParent = this._bonusText.transform.parent.GetComponent<RectTransform>();
      RectTransform speedParent = this._speedText.transform.parent.GetComponent<RectTransform>();
      yield return (object) new WaitForSeconds(1f);
      LeanTween.moveLocalY(bonusParent.gameObject, bonusParent.localPosition.y + 30f, 0.5f);
      LeanTween.moveLocalY(speedParent.gameObject, speedParent.localPosition.y + 30f, 0.5f);
      yield return (object) new WaitForSeconds(0.5f);
      this._speedText.transform.parent.gameObject.SetActive(false);
      this._currentScore = this._store.Score.Value;
      this._nextScore = this._currentScore + this._store.TimeBonusScore.Value;
      this._timer = 0.0f;
      yield return (object) new WaitForSeconds(0.5f);
      this._scoreText.text = this._nextScore.ToString("#,##0");
      LeanTween.moveLocalY(bonusParent.gameObject, bonusParent.localPosition.y + 30f, 0.5f);
      LeanTween.moveLocalY(speedParent.gameObject, speedParent.localPosition.y + 30f, 0.5f);
      yield return (object) new WaitForSeconds(0.5f);
      this._bonusText.transform.parent.gameObject.SetActive(false);
      this._currentScore = this._nextScore;
      this._nextScore = this._currentScore + this._store.LevelBonusScore.Value;
      this._timer = 0.0f;
      yield return (object) new WaitForSeconds(0.5f);
    }

    private void Update()
    {
      if ((double) this._timer >= (double) this._scoreCountSpeed)
        return;
      this._timer += Time.deltaTime;
      this._scoreText.text = Mathf.Lerp((float) this._currentScore, (float) this._nextScore, this._timer / this._scoreCountSpeed).ToString("#,##0");
    }
  }
}
