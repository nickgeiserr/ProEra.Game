// Decompiled with JetBrains decompiler
// Type: TB12.UI.GameInfoDefaultUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using Framework.UI;
using ProEra.Game;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vars;

namespace TB12.UI
{
  public class GameInfoDefaultUI : UIPanel
  {
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _ballsText;
    [SerializeField]
    private Image _ballsImage;
    [SerializeField]
    private Image _headsetsImage;
    [SerializeField]
    private float _normTextSize = 35f;
    [SerializeField]
    private float _bigTextSize = 60f;
    [SerializeField]
    private Color _normTextColor;
    [SerializeField]
    private Color _bigTextColor;
    [Header("Asset References")]
    [SerializeField]
    private Sprite _defaultSprite;
    [SerializeField]
    private Sprite _missSprite;
    [SerializeField]
    private Sprite _greenSprite;
    [SerializeField]
    private Sprite _yellowSprite;
    [SerializeField]
    private Sprite _bronzeSprite;
    [SerializeField]
    private Sprite _silverSprite;
    [SerializeField]
    private Sprite _goldSprite;
    [SerializeField]
    private Sprite _zeroStarSprite;
    [SerializeField]
    private Sprite _oneStarSprite;
    [SerializeField]
    private Sprite _twoStarSprite;
    [SerializeField]
    private Sprite _threeStarSprite;
    [Header("UI References")]
    [SerializeField]
    private Image _teamLogoImage;
    [SerializeField]
    private Image _starProgressBar;
    [SerializeField]
    private Image _starsEarnedImage;
    [SerializeField]
    private List<Image> _passResults;
    [SerializeField]
    private GameObject PassResultsRoot;

    protected override void OnInitialize()
    {
      if (AppState.IsInMiniCamp())
      {
        this.linksHandler.SetLinks(new List<EventHandle>()
        {
          EventHandle.Link<int>((Variable<int>) MiniGameScoreState.Score, new Action<int>(this.HandleScore)),
          EventHandle.Link<int>((Variable<int>) MiniGameScoreState.AttemptsRemaining, new Action<int>(this.HandleAttemptCount)),
          EventHandle.Link<MiniGameScoreState.MiniCampPassResult>(MiniGameScoreState.passResult, new Action<MiniGameScoreState.MiniCampPassResult>(this.HandleMiniCampPlayAttempt)),
          EventHandle.Link<float>((Variable<float>) MiniGameScoreState.TimeRemaining, new Action<float>(this.HandleAttemptCount)),
          EventHandle.Link<int>((Variable<int>) MiniGameScoreState.StarsEarned, new Action<int>(this.HandleStarsEarned)),
          EventHandle.Link<float>((Variable<float>) MiniGameScoreState.StarsProgress, new Action<float>(this.HandleStarProgress))
        });
        switch (AppState.GameMode)
        {
          case EGameMode.kMiniCampQBPresence:
          case EGameMode.kMiniCamRunAndShoot:
            this.PassResultsRoot.SetActive(false);
            break;
          case EGameMode.kMiniCampPrecisionPassing:
          case EGameMode.kMiniCampRollout:
            this.PassResultsRoot.SetActive(true);
            break;
        }
        this._teamLogoImage.sprite = PersistentSingleton<SaveManager>.Instance.miniCamp.SelectedEntry.GetLogo();
      }
      else
        this.linksHandler.SetLinks(new List<EventHandle>()
        {
          EventHandle.Link<int>((Variable<int>) this._gameplayStore.Score, new Action<int>(this.HandleScore)),
          EventHandle.Link<int>((Variable<int>) this._gameplayStore.AttemptsRemaining, new Action<int>(this.HandleAttemptCount))
        });
    }

    protected override void WillAppear() => this.SetStyle(AppState.GameMode == EGameMode.kAgility);

    private void SetStyle(bool dodgeMode)
    {
      this._headsetsImage.enabled = dodgeMode;
      this._ballsImage.enabled = !dodgeMode;
    }

    private void HandleMiniCampPlayAttempt(
      MiniGameScoreState.MiniCampPassResult miniCampPlayResult)
    {
      if (miniCampPlayResult == null)
        return;
      if (miniCampPlayResult.miniCampPassResult == MiniGameScoreState.EMiniCampPassResult.ResetGamePassThrough)
      {
        foreach (Image passResult in this._passResults)
          passResult.sprite = this._defaultSprite;
      }
      else
      {
        Debug.Log((object) ("MiniCampPlayResult- " + miniCampPlayResult.passResultIndex.ToString()));
        Debug.Log((object) ("MiniCampPlayResult- " + miniCampPlayResult.miniCampPassResult.ToString()));
        if (this._passResults.Count > miniCampPlayResult.passResultIndex)
        {
          Sprite playResultSprite = this.GetPlayResultSprite(miniCampPlayResult.miniCampPassResult);
          if ((UnityEngine.Object) playResultSprite == (UnityEngine.Object) null)
          {
            Console.Error.WriteLine("Pass result sprite is null");
            this._passResults[miniCampPlayResult.passResultIndex].sprite = this._defaultSprite;
          }
          else
            this._passResults[miniCampPlayResult.passResultIndex].sprite = playResultSprite;
        }
        else
          Console.Error.WriteLine("Out of bounds");
      }
    }

    private Sprite GetPlayResultSprite(
      MiniGameScoreState.EMiniCampPassResult miniCampPlayResult)
    {
      switch (miniCampPlayResult)
      {
        case MiniGameScoreState.EMiniCampPassResult.Empty:
          return (Sprite) null;
        case MiniGameScoreState.EMiniCampPassResult.Green:
          return this._greenSprite;
        case MiniGameScoreState.EMiniCampPassResult.Yellow:
          return this._yellowSprite;
        case MiniGameScoreState.EMiniCampPassResult.Bronze:
          return this._bronzeSprite;
        case MiniGameScoreState.EMiniCampPassResult.Silver:
          return this._silverSprite;
        case MiniGameScoreState.EMiniCampPassResult.Gold:
          return this._goldSprite;
        case MiniGameScoreState.EMiniCampPassResult.Miss:
          return this._missSprite;
        default:
          return (Sprite) null;
      }
    }

    private void HandleAttemptCount(int ballsRemaining)
    {
      if (ballsRemaining < 0)
        ballsRemaining = 0;
      bool flag = ballsRemaining <= 3;
      this._ballsText.fontSize = flag ? this._bigTextSize : this._normTextSize;
      this._ballsText.color = flag ? this._bigTextColor : this._normTextColor;
      this._ballsText.text = ballsRemaining.ToString();
    }

    private void HandleAttemptCount(float ballsRemaining)
    {
      if ((double) ballsRemaining < 0.0)
        ballsRemaining = 0.0f;
      bool flag = (double) ballsRemaining <= 3.0;
      this._ballsText.fontSize = flag ? this._bigTextSize : this._normTextSize;
      this._ballsText.color = flag ? this._bigTextColor : this._normTextColor;
      this._ballsText.text = ballsRemaining.ToString("0.00");
      if (AppState.GameMode == EGameMode.kMiniCampQBPresence)
        return;
      this._ballsText.text = "0";
    }

    private void HandleScore(int newScore) => this._scoreText.text = newScore.ToString();

    private void HandleStarProgress(float starProgress) => this._starProgressBar.fillAmount = starProgress;

    private void HandleStarsEarned(int starsEarned)
    {
      Image starsEarnedImage = this._starsEarnedImage;
      Sprite sprite;
      switch (starsEarned)
      {
        case 0:
          sprite = this._zeroStarSprite;
          break;
        case 1:
          sprite = this._oneStarSprite;
          break;
        case 2:
          sprite = this._twoStarSprite;
          break;
        case 3:
          sprite = this._threeStarSprite;
          break;
        default:
          sprite = this._starsEarnedImage.sprite;
          break;
      }
      starsEarnedImage.sprite = sprite;
    }
  }
}
