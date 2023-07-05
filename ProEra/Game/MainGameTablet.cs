// Decompiled with JetBrains decompiler
// Type: ProEra.Game.MainGameTablet
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.Data;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using TB12;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace ProEra.Game
{
  public class MainGameTablet : TabletPage, ITabletPagePrevious
  {
    public static MainGameTablet Instance;
    [Space(10f)]
    [SerializeField]
    private TouchDrag3D ownerTouchDrag3D;
    [Space(10f)]
    [SerializeField]
    private AxisPlaysStore _playStore;
    private ITabletPageBase currentWindow;
    private ITabletPageBase previousWindow;
    [Space(10f)]
    [SerializeField]
    private TabletPage[] childPages;
    [Space(10f)]
    [SerializeField]
    private Image userIcon;
    [SerializeField]
    private Image compIcon;
    [SerializeField]
    private TMP_Text userScore;
    [SerializeField]
    private TMP_Text compScore;
    [Space(10f)]
    [SerializeField]
    private LocalizeStringEvent m_tabScreenTitle;
    [SerializeField]
    private TouchUI2DButton[] m_tabButtons;
    [SerializeField]
    private Transform[] m_tabContentContainers;
    [SerializeField]
    private Transform[] m_statContainers;
    [Space(10f)]
    [SerializeField]
    private Transform previousDriveResultsContainer;
    [SerializeField]
    private GameObject prefabPreviousDriveCell;
    [SerializeField]
    private Sprite _placeholder;
    [Space(10f)]
    [SerializeField]
    private Transform simResultsContainer;
    [SerializeField]
    private GameObject prefabSimResultsCell;
    [SerializeField]
    private TouchUI2DButton _simulationButton;
    [SerializeField]
    private LocalizeStringEvent simulationButtonText;
    private List<string> previousPlayNames = new List<string>();
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private GameResultCell[] poolResultCells = new GameResultCell[6];
    private int resultsCallAmount;
    private int poolLoopIndex;
    private GameResultCell _currentDriveCell;
    private GameResultCell _lastDriveCell;
    private GameResultCell _currentSimCell;
    private GameResultCell _lastSimCell;
    private GameCastManager _gcManager;
    private UniformLogoStore _uniformStore;
    private Transform _rootTransform;
    private const string LocalizationHeaderQuickSim = "SidelineTablet_Button_QuickSim";
    private const string LocalizationHeaderPreviousDrive = "SidelineTablet_Button_PreviousDrive";
    private const string LoocalizationButtonStartSim = "SidelineTablet_Button_StartSim";
    private const string LoocalizationButtonStopSim = "SidelineTablet_Button_StopSim";
    private static HashSet<EGameMode> AllowedGameModes = new HashSet<EGameMode>()
    {
      EGameMode.kAxisGame
    };

    private void Awake()
    {
      if ((UnityEngine.Object) MainGameTablet.Instance != (UnityEngine.Object) null)
      {
        int num = (UnityEngine.Object) MainGameTablet.Instance != (UnityEngine.Object) this ? 1 : 0;
      }
      MainGameTablet.Instance = this;
      this._uniformStore = SaveManager.GetUniformLogoStore();
      this._rootTransform = this.transform.root;
      this._pageType = TabletPage.Pages.GameStats;
      this.currentWindow = (ITabletPageBase) this;
      this.previousWindow = (ITabletPageBase) this;
    }

    private void Start()
    {
      if (PersistentSingleton<QuickStart>.Instance.EnableQuickStart)
        return;
      this.ReinitializeMatchStats();
      this.OpenWindow();
      foreach (TabletPage childPage in this.childPages)
      {
        if (!((UnityEngine.Object) childPage == (UnityEngine.Object) null))
          childPage.RegisterMainPage((TabletPage) this);
      }
      if ((UnityEngine.Object) this.m_tabButtons[0] != (UnityEngine.Object) null)
        this.m_tabButtons[0].onClick += new System.Action(this.HandleBackButton);
      else
        Debug.LogError((object) "m_tabButtons[0] is null in MainGameTablet.Start");
      if ((UnityEngine.Object) this.m_tabButtons[1] != (UnityEngine.Object) null)
        this.m_tabButtons[1].onClick += new System.Action(this.HandlePreviousDriveButton);
      else
        Debug.LogError((object) "m_tabButtons[1] is null in MainGameTablet.Start");
      if ((UnityEngine.Object) this.m_tabButtons[2] != (UnityEngine.Object) null)
        this.m_tabButtons[2].onClick += new System.Action(this.HandleSimButton);
      else
        Debug.LogError((object) "m_tabButtons[2] is null in MainGameTablet.Start");
      if ((UnityEngine.Object) this._simulationButton != (UnityEngine.Object) null)
        this._simulationButton.onClick += new System.Action(this.HandleSimulationButton);
      else
        Debug.LogError((object) "_simulationButton is null in MainGameTablet.Start");
      this.HandleBackButton();
      this.prefabPreviousDriveCell.SetActive(false);
      this.prefabSimResultsCell.SetActive(false);
      MainGameTablet.RefreshPlayStats();
      this.OnStadiumLoaded();
    }

    public void ReinitializeMatchStats()
    {
      this._linksHandler.Clear();
      List<EventHandle> links = new List<EventHandle>()
      {
        MatchState.CurrentMatchState.Link<EMatchState>(new Action<EMatchState>(this.Refresh))
      };
      if (MatchState.Stats.User != null)
        links.Add(MatchState.Stats.User.VarScore.Link<int>(new Action<int>(this.SetHomeScore)));
      if (MatchState.Stats.Comp != null)
        links.Add(MatchState.Stats.Comp.VarScore.Link<int>(new Action<int>(this.SetAwayScore)));
      if (MatchState.Turnover != null)
        links.Add(MatchState.Turnover.Link<bool>(new Action<bool>(this.HandleTurnover)));
      this._linksHandler.SetLinks(links);
    }

    private void OnEnable()
    {
      this.RefreshTeamsLogo();
      this.RefreshTeamsStats();
    }

    public void HandleBackButton()
    {
      this.ShowTab(0);
      this.HideNavBar();
    }

    private void HandlePreviousDriveButton()
    {
      this.ShowTab(1);
      this.ShowNavBar();
      this.m_tabScreenTitle.StringReference.TableEntryReference = (TableEntryReference) "SidelineTablet_Button_PreviousDrive";
    }

    private void HandleSimButton()
    {
      this.ShowTab(2);
      this.ShowNavBar();
      this.m_tabScreenTitle.StringReference.TableEntryReference = (TableEntryReference) "SidelineTablet_Button_QuickSim";
    }

    public static void Show()
    {
      if ((UnityEngine.Object) MainGameTablet.Instance == (UnityEngine.Object) null)
        return;
      MainGameTablet.Instance.ownerTouchDrag3D.gameObject.SetActive(true);
      MainGameTablet.Instance.SetOnOffHand();
    }

    public static void Hide()
    {
      if ((UnityEngine.Object) MainGameTablet.Instance == (UnityEngine.Object) null)
        return;
      MainGameTablet.Instance.PutMeBack();
      MainGameTablet.Instance.ownerTouchDrag3D.gameObject.SetActive(false);
      MainGameTablet.Instance.ClearAllSimResults();
    }

    private void Refresh(EMatchState newState)
    {
      if (newState != EMatchState.Beginning && (UnityEngine.Object) this._gcManager == (UnityEngine.Object) null)
      {
        this._gcManager = UnityEngine.Object.FindObjectOfType<GameCastManager>();
        if ((bool) (UnityEngine.Object) this._gcManager)
          this._gcManager.OnCastWritten += new Action<string, string>(this.SetGamecast);
      }
      if (newState.Equals((object) EMatchState.End))
        this._linksHandler.Clear();
      this.RefreshTeamsLogo();
      this.RefreshTeamsStats();
    }

    public static void RefreshPlayStats()
    {
      if ((UnityEngine.Object) MainGameTablet.Instance == (UnityEngine.Object) null)
        return;
      if (MatchState.IsPlayerOneOnOffense)
        MainGameTablet.Instance.RefreshPreviousDriveResults();
      if (MatchManager.Exists() && (bool) MatchManager.instance.ShouldSimulate)
        MainGameTablet.Instance.RefreshSimResults();
      MainGameTablet.Instance.RefreshTeamsStats();
    }

    public void ShowTab(int a_tabIndex)
    {
      if (a_tabIndex > this.m_tabContentContainers.Length)
        return;
      foreach (Component contentContainer in this.m_tabContentContainers)
        contentContainer.gameObject.SetActive(false);
      this.m_tabContentContainers[a_tabIndex].gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
      MatchState.CurrentMatchState.OnValueChanged -= new Action<EMatchState>(this.OnMatchStateChanged);
      this._linksHandler.Clear();
      if ((UnityEngine.Object) this._gcManager != (UnityEngine.Object) null)
        this._gcManager.OnCastWritten -= new Action<string, string>(this.SetGamecast);
      MainGameTablet.Instance = (MainGameTablet) null;
      if ((UnityEngine.Object) this.m_tabButtons[0] != (UnityEngine.Object) null)
        this.m_tabButtons[0].onClick -= new System.Action(this.HandleBackButton);
      else
        Debug.LogError((object) "m_tabButtons[0] is null in MainGameTablet.OnDestroy");
      if ((UnityEngine.Object) this.m_tabButtons[1] != (UnityEngine.Object) null)
        this.m_tabButtons[1].onClick -= new System.Action(this.HandlePreviousDriveButton);
      else
        Debug.LogError((object) "m_tabButtons[1] is null in MainGameTablet.OnDestroy");
      if ((UnityEngine.Object) this.m_tabButtons[2] != (UnityEngine.Object) null)
        this.m_tabButtons[2].onClick -= new System.Action(this.HandleSimButton);
      else
        Debug.LogError((object) "m_tabButtons[2] is null in MainGameTablet.OnDestroy");
      if ((UnityEngine.Object) this._simulationButton != (UnityEngine.Object) null)
        this._simulationButton.onClick -= new System.Action(this.HandleSimulationButton);
      else
        Debug.LogError((object) "_simulationButton is null in MainGameTablet.OnDestroy");
    }

    public void OpenPrevWindow()
    {
      this.currentWindow.CloseWindow();
      this.currentWindow = this.previousWindow;
      this.currentWindow.OpenWindow();
    }

    public void OpenPage(TabletPage.Pages pageType)
    {
      ITabletPageBase tabletBaseByType = this.GetTabletBaseByType(pageType);
      if (tabletBaseByType == null)
        return;
      this.previousWindow = this.currentWindow;
      this.currentWindow = tabletBaseByType;
      this.previousWindow.CloseWindow();
      this.currentWindow.OpenWindow();
    }

    private ITabletPageBase GetTabletBaseByType(TabletPage.Pages type)
    {
      foreach (TabletPage childPage in this.childPages)
      {
        if (!((UnityEngine.Object) childPage == (UnityEngine.Object) null) && childPage.GetPageType() == type)
          return (ITabletPageBase) childPage;
      }
      return (ITabletPageBase) null;
    }

    private void HandleSimulationButton()
    {
      MatchManager.instance.ShouldSimulate.Toggle();
      this.simulationButtonText.StringReference.TableEntryReference = (TableEntryReference) ((bool) MatchManager.instance.ShouldSimulate ? "SidelineTablet_Button_StopSim" : "SidelineTablet_Button_StartSim");
    }

    public void PutMeBack()
    {
      if (!((UnityEngine.Object) this.ownerTouchDrag3D != (UnityEngine.Object) null))
        return;
      this.ownerTouchDrag3D.Reset((ITouchInput) null);
    }

    public static void SelfDestroy()
    {
      if ((UnityEngine.Object) MainGameTablet.Instance == (UnityEngine.Object) null)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) MainGameTablet.Instance.ownerTouchDrag3D.gameObject);
    }

    public void SetOnOffHand()
    {
      ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? PlayerAvatar.Instance.RightController : PlayerAvatar.Instance.LeftController).Set3DObjectInHand((ITouchGrabbable) this.ownerTouchDrag3D);
      this.ownerTouchDrag3D.Lock(true);
    }

    private void SetHomeScore(int value)
    {
      ((bool) Globals.UserIsHome ? this.userScore : this.compScore).text = value.ToString();
      this.RefreshTeamsStats();
    }

    private void SetAwayScore(int value)
    {
      ((bool) Globals.UserIsHome ? this.compScore : this.userScore).text = value.ToString();
      this.RefreshTeamsStats();
    }

    private void RefreshTeamsLogo()
    {
      UniformLogo uniformLogo1 = this._uniformStore.GetUniformLogo(PersistentData.GetHomeTeamIndex());
      this.userIcon.sprite = uniformLogo1.teamLogo;
      UniformLogo uniformLogo2 = this._uniformStore.GetUniformLogo(PersistentData.GetAwayTeamIndex());
      this.compIcon.sprite = uniformLogo2.teamLogo;
      MonoBehaviour.print((object) uniformLogo1);
      MonoBehaviour.print((object) uniformLogo2);
    }

    private void RefreshTeamsStats()
    {
      TeamGameStats teamGameStats1 = (bool) Globals.UserIsHome ? MatchState.Stats.User : MatchState.Stats.Comp;
      TeamGameStats teamGameStats2 = (bool) Globals.UserIsHome ? MatchState.Stats.Comp : MatchState.Stats.User;
      for (int a_index = 0; a_index < 4; ++a_index)
      {
        TMP_Text[] componentsInChildren = this.m_statContainers[a_index].GetComponentsInChildren<TMP_Text>();
        if (componentsInChildren == null)
          break;
        componentsInChildren[1].text = this.GetStatLabelByInt(a_index);
        if (teamGameStats1 != null)
          componentsInChildren[0].text = this.GetUserStatByInt(a_index);
        if (teamGameStats2 != null)
          componentsInChildren[2].text = this.GetCompStatByInt(a_index);
      }
    }

    private string GetStatLabelByInt(int a_index)
    {
      string statLabelByInt = "";
      switch (a_index)
      {
        case 0:
          statLabelByInt = "Passing Yards";
          break;
        case 1:
          statLabelByInt = "Rush Yards";
          break;
        case 2:
          statLabelByInt = "3rd %";
          break;
        case 3:
          statLabelByInt = "Turnovers";
          break;
      }
      return statLabelByInt;
    }

    private string GetUserStatByInt(int a_index)
    {
      string userStatByInt = "";
      TeamGameStats teamGameStats = (bool) Globals.UserIsHome ? MatchState.Stats.User : MatchState.Stats.Comp;
      if (teamGameStats != null)
      {
        switch (a_index)
        {
          case 0:
            userStatByInt = teamGameStats.PassYards.ToString();
            break;
          case 1:
            userStatByInt = teamGameStats.RushYards.ToString();
            break;
          case 2:
            userStatByInt = Mathf.RoundToInt(teamGameStats.ThirdDownSuc > 0 ? (float) ((double) teamGameStats.ThirdDownSuc / (double) teamGameStats.ThirdDownAtt * 100.0) : 0.0f).ToString();
            break;
          case 3:
            userStatByInt = teamGameStats.Turnovers.ToString();
            break;
        }
      }
      return userStatByInt;
    }

    private string GetCompStatByInt(int a_index)
    {
      string compStatByInt = "";
      TeamGameStats teamGameStats = (bool) Globals.UserIsHome ? MatchState.Stats.Comp : MatchState.Stats.User;
      if (teamGameStats != null)
      {
        switch (a_index)
        {
          case 0:
            compStatByInt = teamGameStats.PassYards.ToString();
            break;
          case 1:
            compStatByInt = teamGameStats.RushYards.ToString();
            break;
          case 2:
            compStatByInt = Mathf.RoundToInt(teamGameStats.ThirdDownSuc > 0 ? (float) ((double) teamGameStats.ThirdDownSuc / (double) teamGameStats.ThirdDownAtt * 100.0) : 0.0f).ToString();
            break;
          case 3:
            compStatByInt = teamGameStats.Turnovers.ToString();
            break;
        }
      }
      return compStatByInt;
    }

    private void RefreshPreviousDriveResults()
    {
      if (!MatchManager.Exists())
        return;
      PlayManager playManager = MatchManager.instance.playManager;
      if (!((UnityEngine.Object) playManager != (UnityEngine.Object) null) || playManager.savedDefPlay == null || playManager.savedOffPlay == null)
        return;
      GameResultCell newDriveCell = this.CreateNewDriveCell();
      string message1 = playManager.savedOffPlay.GetFormation().GetBaseFormationString() + "\n" + playManager.savedOffPlay.GetFormation().GetPersonnel() + "\n" + playManager.savedOffPlay.GetPlayName();
      string message2 = playManager.savedDefPlay.GetFormation().GetBaseFormationString() + "\n" + playManager.savedDefPlay.GetFormation().GetPersonnel() + "\n" + playManager.savedDefPlay.GetPlayName();
      newDriveCell.SetOffInfo1(message1);
      newDriveCell.SetDefInfo1(message2);
      newDriveCell.SetDownAndDistanceInfo(string.Empty);
      newDriveCell.SetGamecastInfo(string.Empty);
      if (playManager.savedOffPlay != null && playManager.savedDefPlay != null)
      {
        Sprite playImage1 = this._playStore.GetPlayImage((PlayData) playManager.savedOffPlay);
        Sprite sprite1 = (UnityEngine.Object) playImage1 == (UnityEngine.Object) null ? this._placeholder : playImage1;
        newDriveCell.SetOffTeam(sprite1);
        Sprite playImage2 = this._playStore.GetPlayImage((PlayData) playManager.savedDefPlay);
        Sprite sprite2 = (UnityEngine.Object) playImage2 == (UnityEngine.Object) null ? this._placeholder : playImage2;
        newDriveCell.SetDefTeam(sprite2);
      }
      this._lastDriveCell = this._currentDriveCell;
      this._currentDriveCell = newDriveCell;
    }

    private void RefreshSimResults()
    {
      if (!MatchManager.Exists())
        return;
      GameResultCell newSimCell = this.CreateNewSimCell();
      newSimCell.SetGamecastInfo(string.Empty);
      this._lastSimCell = this._currentSimCell;
      this._currentSimCell = newSimCell;
    }

    private GameResultCell CreateNewDriveCell()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabPreviousDriveCell, Vector3.zero, Quaternion.identity, this.previousDriveResultsContainer);
      gameObject.SetActive(true);
      GameResultCell component = gameObject.GetComponent<GameResultCell>();
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
      return component;
    }

    private GameResultCell CreateNewSimCell()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabSimResultsCell, Vector3.zero, Quaternion.identity, this.simResultsContainer);
      gameObject.SetActive(true);
      GameResultCell component = gameObject.GetComponent<GameResultCell>();
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
      return component;
    }

    private void HandleTurnover(bool isTurnover)
    {
      if (MatchState.IsPlayerOneOnOffense == isTurnover)
        return;
      this.ClearAllPreviousDrivePlays();
    }

    private void ClearAllPreviousDrivePlays()
    {
      for (int index = this.previousDriveResultsContainer.childCount - 1; index >= 0; --index)
      {
        GameObject gameObject = this.previousDriveResultsContainer.GetChild(index).gameObject;
        if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) this.prefabPreviousDriveCell) && (UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
      this._currentDriveCell = this._lastDriveCell = (GameResultCell) null;
    }

    private void ClearAllSimResults()
    {
      for (int index = this.simResultsContainer.childCount - 1; index >= 0; --index)
      {
        GameObject gameObject = this.simResultsContainer.GetChild(index).gameObject;
        if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) this.prefabSimResultsCell) && (UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
      this._currentSimCell = this._lastSimCell = (GameResultCell) null;
      if (!((UnityEngine.Object) MatchManager.instance != (UnityEngine.Object) null) || !(bool) MatchManager.instance.ShouldSimulate)
        return;
      this.HandleSimulationButton();
    }

    private void SetGamecast(string downAndDistance, string cast)
    {
      bool playerOneOnOffense = MatchState.IsPlayerOneOnOffense;
      if ((UnityEngine.Object) this._currentDriveCell != (UnityEngine.Object) null & playerOneOnOffense)
      {
        this._currentDriveCell.SetDownAndDistanceInfo(downAndDistance);
        this._currentDriveCell.SetGamecastInfo(cast);
      }
      if (!((UnityEngine.Object) this._currentSimCell != (UnityEngine.Object) null) || playerOneOnOffense || !(bool) MatchManager.instance.ShouldSimulate)
        return;
      this._currentSimCell.SetGamecastInfo(cast);
    }

    public static T[] ShiftRight<T>(T[] myArray)
    {
      T[] destinationArray = new T[myArray.Length];
      T my = myArray[destinationArray.Length - 1];
      Array.Copy((Array) myArray, 0, (Array) destinationArray, 1, myArray.Length - 1);
      destinationArray[0] = my;
      return destinationArray;
    }

    public static T[] ShiftLeft<T>(T[] myArray)
    {
      T[] destinationArray = new T[myArray.Length];
      T my = myArray[0];
      Array.Copy((Array) myArray, 1, (Array) destinationArray, 0, myArray.Length - 1);
      destinationArray[destinationArray.Length - 1] = my;
      return destinationArray;
    }

    [ContextMenu("ShowNav")]
    public void ShowNavBar() => this.transform.localPosition = Vector3.down * 100f;

    [ContextMenu("HideNav")]
    public void HideNavBar() => this.transform.localPosition = Vector3.zero;

    public void OnStadiumLoaded()
    {
      this.SetEnabled(false);
      if (!MainGameTablet.AllowedGameModes.Contains(AppState.GameMode))
        return;
      MatchState.CurrentMatchState.OnValueChanged += new Action<EMatchState>(this.OnMatchStateChanged);
    }

    private void OnMatchStateChanged(EMatchState matchState) => this.SetEnabled(matchState == EMatchState.UserOnDefense);

    private void SetEnabled(bool enabled) => this._rootTransform.gameObject.SetActive(enabled);
  }
}
