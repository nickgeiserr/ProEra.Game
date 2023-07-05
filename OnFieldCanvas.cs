// Decompiled with JetBrains decompiler
// Type: OnFieldCanvas
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnFieldCanvas : MonoBehaviour
{
  public static OnFieldCanvas Instance;
  [SerializeField]
  private UniformLogoStore _store;
  [SerializeField]
  private Transform _halfTimeDisplay;
  [SerializeField]
  private Image _homeLogo;
  [SerializeField]
  private Image _awayLogo;
  [SerializeField]
  private Transform _gameOverDisplay;
  [SerializeField]
  private GameObject _simPlayLinePrefab;
  [SerializeField]
  private GameObject _arcadeScoreTextPrefab;
  [SerializeField]
  private Canvas logoCanvas;
  [SerializeField]
  private Canvas halfTimeCanvas;
  [SerializeField]
  private Canvas gameOverCanvas;
  private RoutineHandle _routineHandle;
  private Dictionary<string, Color> _colorMapping;
  private List<LineRenderer> _simulatedPlayLines;
  private int _simulatedLineCount;
  private List<WorldPopupText> _scoreTextPool;

  public static event System.Action OnGraphicsHidden;

  private void Awake()
  {
    OnFieldCanvas.Instance = this;
    this._routineHandle = new RoutineHandle();
    this._colorMapping = new Dictionary<string, Color>();
    this._simulatedPlayLines = new List<LineRenderer>();
    this._scoreTextPool = new List<WorldPopupText>();
    this.AdjustCanvasVisibility(false, false);
  }

  public void Init() => this.transform.eulerAngles = new Vector3(0.0f, (bool) Globals.UserIsHome ? -90f : 90f, 0.0f);

  public void ShowOnFieldScore(float initialDelay = 3f)
  {
    TextMeshProUGUI component = this._halfTimeDisplay.GetChild(0).GetComponent<TextMeshProUGUI>();
    int num = ProEra.Game.MatchState.Stats.GetHomeScore();
    string str1 = num.ToString();
    num = ProEra.Game.MatchState.Stats.GetAwayScore();
    string str2 = num.ToString();
    string str3 = str1 + "-" + str2;
    component.text = str3;
    this._colorMapping.Clear();
    this._routineHandle.Run(this.DoHalfTimeSequence(initialDelay));
  }

  public void ShowGameOver()
  {
    Debug.Log((object) "OnFieldCanvas: ShowGameOver");
    TeamGameStats teamGameStats1;
    TeamGameStats teamGameStats2;
    PlayerStats currentGameStats1;
    PlayerStats currentGameStats2;
    if ((bool) Globals.UserIsHome)
    {
      teamGameStats1 = ProEra.Game.MatchState.Stats.User;
      teamGameStats2 = ProEra.Game.MatchState.Stats.Comp;
      currentGameStats1 = MatchManager.instance.playersManager.userTeamData.TeamDepthChart.GetStartingQB().CurrentGameStats;
      currentGameStats2 = MatchManager.instance.playersManager.compTeamData.TeamDepthChart.GetStartingQB().CurrentGameStats;
    }
    else
    {
      teamGameStats1 = ProEra.Game.MatchState.Stats.Comp;
      teamGameStats2 = ProEra.Game.MatchState.Stats.User;
      currentGameStats1 = MatchManager.instance.playersManager.compTeamData.TeamDepthChart.GetStartingQB().CurrentGameStats;
      currentGameStats2 = MatchManager.instance.playersManager.userTeamData.TeamDepthChart.GetStartingQB().CurrentGameStats;
    }
    Debug.Log((object) ("OnFieldCanvas: homeQB?: " + (currentGameStats1 == null).ToString()));
    Debug.Log((object) ("OnFieldCanvas: homeQB?.QBCompletions.ToString(): " + currentGameStats1?.QBCompletions.ToString()));
    for (int index = 0; index < this._gameOverDisplay.childCount; ++index)
    {
      Transform child = this._gameOverDisplay.GetChild(index);
      TextMeshProUGUI component1 = child.Find("Home").GetComponent<TextMeshProUGUI>();
      TextMeshProUGUI component2 = child.Find("Away").GetComponent<TextMeshProUGUI>();
      int num1;
      float num2;
      switch (index)
      {
        case 0:
          TextMeshProUGUI textMeshProUgui1 = component1;
          num1 = ProEra.Game.MatchState.Stats.GetHomeScore();
          string str1 = num1.ToString();
          textMeshProUgui1.text = str1;
          TextMeshProUGUI textMeshProUgui2 = component2;
          num1 = ProEra.Game.MatchState.Stats.GetAwayScore();
          string str2 = num1.ToString();
          textMeshProUgui2.text = str2;
          break;
        case 1:
          float num3 = teamGameStats1.TotalPossessionTime + teamGameStats2.TotalPossessionTime;
          TextMeshProUGUI textMeshProUgui3 = component1;
          num2 = (double) teamGameStats1.TotalPossessionTime > 0.0 ? teamGameStats1.TotalPossessionTime * 100f / num3 : 0.0f;
          string str3 = num2.ToString("0") + "%";
          textMeshProUgui3.text = str3;
          TextMeshProUGUI textMeshProUgui4 = component2;
          num2 = (double) teamGameStats2.TotalPossessionTime > 0.0 ? teamGameStats2.TotalPossessionTime * 100f / num3 : 0.0f;
          string str4 = num2.ToString("0") + "%";
          textMeshProUgui4.text = str4;
          break;
        case 2:
          component1.text = teamGameStats1.Turnovers.ToString();
          component2.text = teamGameStats2.Turnovers.ToString();
          break;
        case 3:
          TextMeshProUGUI textMeshProUgui5 = component1;
          num1 = teamGameStats1.RushYards + teamGameStats1.PassYards;
          string str5 = num1.ToString();
          textMeshProUgui5.text = str5;
          TextMeshProUGUI textMeshProUgui6 = component2;
          num1 = teamGameStats2.RushYards + teamGameStats2.PassYards;
          string str6 = num1.ToString();
          textMeshProUgui6.text = str6;
          break;
        case 4:
          component1.text = teamGameStats1.PassYards.ToString();
          component2.text = teamGameStats2.PassYards.ToString();
          break;
        case 5:
          component1.text = teamGameStats1.RushYards.ToString();
          component2.text = teamGameStats2.RushYards.ToString();
          break;
        case 6:
          TextMeshProUGUI textMeshProUgui7 = component1;
          num2 = teamGameStats1.ThirdDownSuc > 0 ? (float) ((double) teamGameStats1.ThirdDownSuc / (double) teamGameStats1.ThirdDownAtt * 100.0) : 0.0f;
          string str7 = num2.ToString("0") + "%";
          textMeshProUgui7.text = str7;
          TextMeshProUGUI textMeshProUgui8 = component2;
          num2 = teamGameStats2.ThirdDownSuc > 0 ? (float) ((double) teamGameStats2.ThirdDownSuc / (double) teamGameStats2.ThirdDownAtt * 100.0) : 0.0f;
          string str8 = num2.ToString("0") + "%";
          textMeshProUgui8.text = str8;
          break;
        case 7:
          TextMeshProUGUI textMeshProUgui9 = component1;
          string str9;
          if (currentGameStats1 == null)
          {
            str9 = (string) null;
          }
          else
          {
            num1 = currentGameStats1.GetQBRating();
            str9 = num1.ToString();
          }
          textMeshProUgui9.text = str9;
          TextMeshProUGUI textMeshProUgui10 = component2;
          string str10;
          if (currentGameStats2 == null)
          {
            str10 = (string) null;
          }
          else
          {
            num1 = currentGameStats2.GetQBRating();
            str10 = num1.ToString();
          }
          textMeshProUgui10.text = str10;
          break;
        case 8:
          component1.text = currentGameStats1?.QBPassTDs.ToString();
          component2.text = currentGameStats2?.QBPassTDs.ToString();
          break;
        case 9:
          TextMeshProUGUI textMeshProUgui11 = component1;
          num2 = currentGameStats1 == null || currentGameStats1.QBCompletions <= 0 ? 0.0f : (float) ((double) currentGameStats1.QBCompletions / (double) currentGameStats1.QBAttempts * 100.0);
          string str11 = num2.ToString("0") + "%";
          textMeshProUgui11.text = str11;
          TextMeshProUGUI textMeshProUgui12 = component2;
          num2 = currentGameStats2 == null || currentGameStats2.QBCompletions <= 0 ? 0.0f : (float) ((double) currentGameStats2.QBCompletions / (double) currentGameStats2.QBAttempts * 100.0);
          string str12 = num2.ToString("0") + "%";
          textMeshProUgui12.text = str12;
          break;
        case 10:
          TextMeshProUGUI textMeshProUgui13 = component1;
          num2 = currentGameStats1 == null || currentGameStats1.QBPassYards <= 0 ? 0.0f : (float) currentGameStats1.QBPassYards / (float) currentGameStats1.QBCompletions;
          string str13 = num2.ToString("0.0");
          textMeshProUgui13.text = str13;
          TextMeshProUGUI textMeshProUgui14 = component2;
          num2 = currentGameStats2 == null || currentGameStats2.QBPassYards <= 0 ? 0.0f : (float) currentGameStats2.QBPassYards / (float) currentGameStats2.QBCompletions;
          string str14 = num2.ToString("0.0");
          textMeshProUgui14.text = str14;
          break;
        case 11:
          component1.text = currentGameStats1?.QBInts.ToString();
          component2.text = currentGameStats2?.QBInts.ToString();
          break;
        case 12:
          component1.text = currentGameStats1?.QBCompletions.ToString();
          component2.text = currentGameStats2?.QBCompletions.ToString();
          break;
        case 13:
          component1.text = currentGameStats1?.QBAttempts.ToString();
          component2.text = currentGameStats2?.QBAttempts.ToString();
          break;
      }
    }
    this._colorMapping.Clear();
    this._routineHandle.Run(this.DoGameOverSequence());
  }

  private IEnumerator DoGameOverSequence()
  {
    this.UpdateSharedCanvas();
    this.AdjustCanvasVisibility(false, true);
    this.DoFade(true);
    yield return (object) new WaitForSeconds(10f);
    this.DoFade(false);
    yield return (object) new WaitForSeconds(1f);
    this.AdjustCanvasVisibility(false, false);
    System.Action onGraphicsHidden = OnFieldCanvas.OnGraphicsHidden;
    if (onGraphicsHidden != null)
      onGraphicsHidden();
  }

  private IEnumerator DoHalfTimeSequence(float initialDelay)
  {
    yield return (object) new WaitForSeconds(initialDelay);
    this.UpdateSharedCanvas();
    this.AdjustCanvasVisibility(true, false);
    this.DoFade(true);
    yield return (object) new WaitForSeconds(6f);
    this.DoFade(false);
    yield return (object) new WaitForSeconds(1f);
    this.AdjustCanvasVisibility(false, false);
    System.Action onGraphicsHidden = OnFieldCanvas.OnGraphicsHidden;
    if (onGraphicsHidden != null)
      onGraphicsHidden();
  }

  public void AddSimulatedPlayLine(List<Vector3> points, PlayType type, bool lostYards)
  {
    Debug.Log((object) nameof (AddSimulatedPlayLine));
    Color color = Color.white;
    if (this._simulatedPlayLines.Count <= this._simulatedLineCount)
      this._simulatedPlayLines.Add(UnityEngine.Object.Instantiate<GameObject>(this._simPlayLinePrefab).GetComponent<LineRenderer>());
    LineRenderer simulatedPlayLine = this._simulatedPlayLines[this._simulatedLineCount];
    simulatedPlayLine.positionCount = points.Count;
    simulatedPlayLine.SetPositions(points.ToArray());
    if (lostYards)
    {
      color = Color.red;
    }
    else
    {
      switch (type)
      {
        case PlayType.Run:
          color = Color.blue;
          break;
        case PlayType.Pass:
          color = Color.yellow;
          break;
      }
    }
    simulatedPlayLine.startColor = color;
    simulatedPlayLine.endColor = color;
    simulatedPlayLine.enabled = true;
    ++this._simulatedLineCount;
  }

  public void HideSimulatedPlayLines()
  {
    Debug.Log((object) nameof (HideSimulatedPlayLines));
    for (int index = 0; index < this._simulatedPlayLines.Count; ++index)
    {
      if ((UnityEngine.Object) this._simulatedPlayLines[index] != (UnityEngine.Object) null)
        this._simulatedPlayLines[index].enabled = false;
      else
        Debug.LogError((object) ("OnFieldCanvas.HideSimulatedPlayLines trying to access _simulatedPlayLines[" + index.ToString() + "] which is null!!!!"));
    }
    this._simulatedLineCount = 0;
  }

  public void ShowArcadeScoreText(int score, Vector3 pos)
  {
    if (this._scoreTextPool.Count == 0)
    {
      WorldPopupText component = UnityEngine.Object.Instantiate<GameObject>(this._arcadeScoreTextPrefab, this.transform).GetComponent<WorldPopupText>();
      component.OnComplete += new Action<WorldPopupText>(this.PoolArcadeText);
      this._scoreTextPool.Add(component);
    }
    WorldPopupText worldPopupText = this._scoreTextPool[0];
    this._scoreTextPool.RemoveAt(0);
    worldPopupText.transform.position = pos;
    worldPopupText.transform.forward = (worldPopupText.transform.position with
    {
      y = 0.0f
    } - PersistentSingleton<GamePlayerController>.Instance.transform.position with
    {
      y = 0.0f
    }).normalized;
    worldPopupText.Display(score.ToString());
  }

  public void PoolArcadeText(WorldPopupText text) => this._scoreTextPool.Add(text);

  private void UpdateSharedCanvas()
  {
    this._homeLogo.sprite = this._store.GetUniformLogo(PersistentData.GetHomeTeamIndex()).teamLogo;
    this._awayLogo.sprite = this._store.GetUniformLogo(PersistentData.GetAwayTeamIndex()).teamLogo;
  }

  private void DoFade(bool fadeIn)
  {
    float num1 = 0.0f;
    float num2 = 1f;
    if ((bool) (UnityEngine.Object) this.transform)
      this.SetColorOnChildren(this.transform, fadeIn ? num1 : num2, fadeIn);
    if (!(bool) (UnityEngine.Object) this.gameObject)
      return;
    LeanTween.value(this.gameObject, fadeIn ? num1 : num2, fadeIn ? num2 : num1, 1f).setOnUpdate((Action<float>) (val =>
    {
      if (!(bool) (UnityEngine.Object) this.transform)
        return;
      this.SetColorOnChildren(this.transform, val, false);
    }));
  }

  private void SetColorOnChildren(Transform t, float alpha, bool saveColor)
  {
    for (int index = 0; index < t.childCount; ++index)
    {
      Transform child = t.GetChild(index);
      if (child.gameObject.activeSelf)
      {
        TextMeshProUGUI component1 = child.GetComponent<TextMeshProUGUI>();
        Image component2 = child.GetComponent<Image>();
        string key = t.name + child.name;
        if ((bool) (UnityEngine.Object) component1)
        {
          if (saveColor)
            this._colorMapping.Add(key, component1.color);
          Color color = this._colorMapping[key] with
          {
            a = alpha
          };
          component1.color = color;
        }
        else if ((bool) (UnityEngine.Object) component2)
        {
          if (saveColor)
            this._colorMapping.Add(key, component2.color);
          Color color = this._colorMapping[key] with
          {
            a = alpha
          };
          component2.color = color;
        }
        if (child.childCount > 0)
          this.SetColorOnChildren(child, alpha, saveColor);
      }
    }
  }

  private void AdjustCanvasVisibility(bool bShowHalftime, bool bShowGameOver)
  {
    if ((UnityEngine.Object) this.halfTimeCanvas != (UnityEngine.Object) null)
      this.halfTimeCanvas.enabled = bShowHalftime;
    if ((UnityEngine.Object) this.gameOverCanvas != (UnityEngine.Object) null)
      this.gameOverCanvas.enabled = bShowGameOver;
    if (!((UnityEngine.Object) this.logoCanvas != (UnityEngine.Object) null))
      return;
    this.logoCanvas.enabled = bShowHalftime | bShowGameOver;
  }
}
