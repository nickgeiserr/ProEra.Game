// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Season.ScoreTicker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Web;
using ProEra.Web.Models.Leaderboard;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace ProEra.Game.UI.Screens.Season
{
  [SelectionBase]
  public class ScoreTicker : MonoBehaviour
  {
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private TextMeshProUGUI _tickerText;
    [SerializeField]
    private LocalizeStringEvent _tickerLocalize;
    [SerializeField]
    private string _scoreLabel;
    [SerializeField]
    private float _animationDuration = 3f;
    [SerializeField]
    private Color _onlineColor = Color.green;
    [SerializeField]
    private Color _offlineColor = Color.red;
    private const string LocalizeAccountNameAndScore = "ScoreTicker_Text_Score";
    private const string LocalizeUnlinkedText = "ScoreTicker_Text_UnlinkedAccount";
    private string _displayName;
    private float _score;

    private string DisplayName
    {
      get => this._displayName;
      set
      {
        this._displayName = value;
        this.UpdateTickerText();
      }
    }

    private float Score
    {
      get => this._score;
      set
      {
        this._score = value;
        this.UpdateTickerText();
      }
    }

    private void Awake()
    {
      this._canvas.worldCamera = Camera.main;
      this.ValidateInspectorBinding();
    }

    private void OnEnable()
    {
      PlayerApi.LoginSuccess += new Action<SaveKeycloakUserData>(this.LoginSuccess);
      PlayerApi.LoginFailure += new System.Action(ScoreTicker.LoginFailure);
      PlayerApi.CreateUserSuccess += new Action<SaveKeycloakUserData>(this.LoginSuccess);
      PlayerApi.HighScoreChange += new Action<string>(this.UpdateProEraScore);
      PlayerApi.DeleteUserSuccess += new System.Action(this.DeleteSuccess);
      this.UpdateProEraScore();
      this.UpdateDisplayName();
      this.StartCoroutine(this.AnimateTicker());
    }

    private void OnDisable()
    {
      PlayerApi.LoginSuccess -= new Action<SaveKeycloakUserData>(this.LoginSuccess);
      PlayerApi.LoginFailure -= new System.Action(ScoreTicker.LoginFailure);
      PlayerApi.CreateUserSuccess -= new Action<SaveKeycloakUserData>(this.LoginSuccess);
      PlayerApi.HighScoreChange -= new Action<string>(this.UpdateProEraScore);
      PlayerApi.DeleteUserSuccess -= new System.Action(this.DeleteSuccess);
      this.StopAllCoroutines();
    }

    private IEnumerator AnimateTicker()
    {
      yield return (object) new WaitForFixedUpdate();
      float elapsedTime = 0.0f;
      while (true)
      {
        elapsedTime = (elapsedTime + Time.deltaTime) % this._animationDuration;
        this._tickerText.margin = new Vector4(this._tickerText.margin.x, this._tickerText.margin.y, Mathf.Lerp(-this._tickerText.renderedWidth, this._tickerText.rectTransform.rect.width, elapsedTime / this._animationDuration), this._tickerText.margin.w);
        yield return (object) null;
      }
    }

    private void LoginSuccess(SaveKeycloakUserData userData)
    {
      this.UpdateProEraScore();
      this.UpdateDisplayName();
    }

    private static void LoginFailure() => Debug.LogError((object) "Failed to update score ticker: login failed");

    private void UpdateTickerText()
    {
      if (PersistentSingleton<PlayerApi>.Instance.IsLoggedIn)
      {
        this._tickerLocalize.StringReference.Arguments = (IList<object>) new string[2]
        {
          this.DisplayName,
          this.Score.ToString()
        };
        this._tickerLocalize.StringReference.TableEntryReference = (TableEntryReference) "ScoreTicker_Text_Score";
        this._tickerText.color = this._onlineColor;
      }
      else
      {
        if (this._tickerLocalize.StringReference.Arguments != null)
          this._tickerLocalize.StringReference.Arguments.Clear();
        this._tickerLocalize.StringReference.TableEntryReference = (TableEntryReference) "ScoreTicker_Text_UnlinkedAccount";
        this._tickerText.color = this._offlineColor;
      }
    }

    private void UpdateProEraScore(string _ = null) => PersistentSingleton<PlayerApi>.Instance.GetHighScore("ProEra", (Action<ListElementModel>) (listElementModel =>
    {
      if (listElementModel == null)
      {
        Debug.LogWarning((object) "ProEra score not found. possible user does not exist in database");
      }
      else
      {
        this.Score = listElementModel.Score;
        Debug.Log((object) string.Format("Result of Get ProEra Score: {0}", (object) listElementModel));
      }
    }));

    private void UpdateDisplayName() => PersistentSingleton<PlayerApi>.Instance.GetDisplayName((Action<string>) (displayName => this.DisplayName = displayName));

    private void DeleteSuccess()
    {
      if (this._tickerLocalize.StringReference.Arguments != null)
        this._tickerLocalize.StringReference.Arguments.Clear();
      this._tickerLocalize.StringReference.TableEntryReference = (TableEntryReference) "ScoreTicker_Text_UnlinkedAccount";
      this._tickerText.color = this._offlineColor;
    }

    private void ValidateInspectorBinding()
    {
    }
  }
}
