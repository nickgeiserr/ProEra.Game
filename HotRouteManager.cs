// Decompiled with JetBrains decompiler
// Type: HotRouteManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using ProEra.Game;
using ProEra.Game.Sources;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HotRouteManager : MonoBehaviour, IHotRouteManager
{
  [SerializeField]
  private GameObject[] selectRecBtns;
  [SerializeField]
  private Transform[] selectRecTrans;
  private List<PlayerAI> validRec;
  private float[][] hotRoutes;
  private string[] hotRouteNames;
  private bool isSelectingReceiver;
  private bool isSelectingHotRoute;
  private int activeRec;
  private TextMeshProUGUI[] _hotRouteTextBoxes;
  private GameObject _blockBtn;

  private void Awake() => UI.HotRouteManager = (IHotRouteManager) this;

  private void Start()
  {
    this.HideReceiverSelectButtons();
    this.validRec = new List<PlayerAI>();
    this.hotRoutes = new float[7][];
    this.isSelectingReceiver = false;
    this.isSelectingHotRoute = false;
    this.LoadHotRoutes();
  }

  private void LoadHotRoutes()
  {
    this.hotRoutes[0] = Plays.self.out5.GetRoutePoints();
    this.hotRoutes[1] = Plays.self.hitch10in.GetRoutePoints();
    this.hotRoutes[2] = Plays.self.in5.GetRoutePoints();
    this.hotRoutes[3] = Plays.self.corner10.GetRoutePoints();
    this.hotRoutes[4] = Plays.self.fly.GetRoutePoints();
    this.hotRoutes[5] = Plays.self.post10.GetRoutePoints();
    this.hotRoutes[6] = Plays.self.runBlockTE.GetRoutePoints();
    this.hotRouteNames = new string[7]
    {
      "OUT",
      "HITCH",
      "IN",
      "CORNER",
      "FLY",
      "POST",
      "BLOCK"
    };
  }

  private void Update() => this.ManageInput();

  public void ShowReceiverSelect()
  {
    List<PlayerAI> playerAiList = !global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curCompScriptRef : MatchManager.instance.playersManager.curUserScriptRef;
    this.validRec.Clear();
    for (int index = 6; index < 11; ++index)
      this.validRec.Add(playerAiList[index]);
    for (int index = 0; index < this.validRec.Count; ++index)
      this.selectRecBtns[index].SetActive(true);
    for (int index1 = 0; index1 < this.validRec.Count - 1; ++index1)
    {
      int index2 = index1;
      for (int index3 = index1 + 1; index3 < this.validRec.Count; ++index3)
      {
        if ((double) this.validRec[index3].trans.position.x <= (double) this.validRec[index2].trans.position.x)
          index2 = index3;
      }
      PlayerAI playerAi = this.validRec[index1];
      this.validRec[index1] = this.validRec[index2];
      this.validRec[index2] = playerAi;
    }
    this.StartCoroutine(this.SetIsSelectingHotRoute(false));
    this.StartCoroutine(this.SetIsSelectingReceiver(true));
  }

  public IEnumerator SetIsSelectingReceiver(bool isSelecting)
  {
    yield return (object) null;
    this.isSelectingReceiver = isSelecting;
    if (this.isSelectingReceiver)
      this.ShowReceiverSelectButtons();
    else
      this.HideReceiverSelectButtons();
  }

  public IEnumerator SetIsSelectingHotRoute(bool isSelecting)
  {
    yield return (object) null;
    this.isSelectingHotRoute = isSelecting;
  }

  private void ShowReceiverSelectButtons()
  {
    for (int index = 0; index < this.selectRecTrans.Length; ++index)
      this.selectRecBtns[index].SetActive(true);
  }

  public void HideReceiverSelectButtons()
  {
    for (int index = 0; index < this.selectRecTrans.Length; ++index)
    {
      this.selectRecTrans[index].position = new Vector3(-10000f, 0.0f, 0.0f);
      this.selectRecBtns[index].SetActive(false);
    }
  }

  public bool IsSelectingReceiver() => this.isSelectingReceiver;

  public bool IsSelectingHotRoute() => this.isSelectingHotRoute;

  public void ShowRoutesForReceiver(int playerIndex)
  {
    if (global::Game.IsPlayerOneOnOffense)
    {
      this._hotRouteTextBoxes = UI.PrePlayWindowP1.GetHotRouteNamesTMPros();
      this._blockBtn = UI.PrePlayWindowP1.GetHotRouteBlockButton();
      UI.PrePlayWindowP1.ShowHotRouteControls();
    }
    else
    {
      this._hotRouteTextBoxes = UI.PrePlayWindowP2.GetHotRouteNamesTMPros();
      this._blockBtn = UI.PrePlayWindowP2.GetHotRouteBlockButton();
      UI.PrePlayWindowP2.ShowHotRouteControls();
    }
    if (this.validRec[playerIndex].position != "WR")
      this._blockBtn.SetActive(true);
    else
      this._blockBtn.SetActive(false);
    if ((double) this.validRec[playerIndex].trans.position.x > 0.0)
    {
      this._hotRouteTextBoxes[0].text = this.hotRouteNames[2];
      this._hotRouteTextBoxes[2].text = this.hotRouteNames[0];
      this._hotRouteTextBoxes[3].text = this.hotRouteNames[5];
      this._hotRouteTextBoxes[5].text = this.hotRouteNames[3];
    }
    else
    {
      this._hotRouteTextBoxes[0].text = this.hotRouteNames[0];
      this._hotRouteTextBoxes[2].text = this.hotRouteNames[2];
      this._hotRouteTextBoxes[3].text = this.hotRouteNames[3];
      this._hotRouteTextBoxes[5].text = this.hotRouteNames[5];
    }
    this.activeRec = playerIndex;
    this.HideReceiverSelectButtons();
    this.isSelectingReceiver = false;
    this.StartCoroutine(this.SetIsSelectingHotRoute());
  }

  private IEnumerator SetIsSelectingHotRoute()
  {
    yield return (object) null;
    this.isSelectingHotRoute = true;
  }

  public void SelectHotRoute(int i)
  {
    this.HideReceiverSelectButtons();
    this.StartCoroutine(this.SetIsSelectingHotRoute(false));
    if (global::Game.IsPlayerOneOnOffense)
      UI.PrePlayWindowP1.EndHotRouteSelection();
    else if (global::Game.Is2PMatch)
      UI.PrePlayWindowP2.EndHotRouteSelection();
    PlayerAI playerAi = this.validRec[this.activeRec];
    if (PlayState.PlayType.Value != PlayType.Pass)
      return;
    if ((double) playerAi.trans.position.x > 0.0)
    {
      switch (i)
      {
        case 0:
          i = 2;
          break;
        case 2:
          i = 0;
          break;
        case 3:
          i = 5;
          break;
        case 5:
          i = 3;
          break;
      }
    }
    float[] p = new float[this.hotRoutes[i].Length];
    p[0] = this.hotRoutes[i][0];
    for (int index = 1; index < this.hotRoutes[i].Length; index += 3)
    {
      p[index] = this.hotRoutes[i][index];
      p[index + 1] = this.hotRoutes[i][index + 1];
      p[index + 2] = this.hotRoutes[i][index + 2] - 1f;
    }
    if (playerAi.playerPosition == Position.TE)
    {
      if (i == 6)
      {
        p = Plays.self.runBlockTE.GetRoutePoints();
        playerAi.SetTEPassBlock();
      }
      else
        playerAi.SetTERunRoute();
    }
    playerAi.SetPlayerRoutePath(p);
  }

  private void ManageInput()
  {
    Player userIndex = global::Game.IsPlayerOneOnOffense ? Player.One : Player.Two;
    if (this.isSelectingReceiver)
    {
      if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && this.selectRecBtns[0].activeInHierarchy)
        this.ShowRoutesForReceiver(0);
      else if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && this.selectRecBtns[1].activeInHierarchy)
        this.ShowRoutesForReceiver(1);
      else if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && this.selectRecBtns[2].activeInHierarchy)
        this.ShowRoutesForReceiver(2);
      else if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) && this.selectRecBtns[3].activeInHierarchy)
        this.ShowRoutesForReceiver(3);
      else if ((Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) && this.selectRecBtns[4].activeInHierarchy)
        this.ShowRoutesForReceiver(4);
      if (UserManager.instance.Action1WasPressed(userIndex) && this.selectRecBtns[0].activeInHierarchy)
        this.ShowRoutesForReceiver(0);
      else if (UserManager.instance.Action3WasPressed(userIndex) && this.selectRecBtns[1].activeInHierarchy)
        this.ShowRoutesForReceiver(1);
      else if (UserManager.instance.Action4WasPressed(userIndex) && this.selectRecBtns[2].activeInHierarchy)
        this.ShowRoutesForReceiver(2);
      else if (UserManager.instance.Action2WasPressed(userIndex) && this.selectRecBtns[3].activeInHierarchy)
      {
        this.ShowRoutesForReceiver(3);
      }
      else
      {
        if (!UserManager.instance.RightBumperWasPressed(userIndex) || !this.selectRecBtns[4].activeInHierarchy)
          return;
        this.ShowRoutesForReceiver(4);
      }
    }
    else
    {
      if (!this.isSelectingHotRoute)
        return;
      if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        this.SelectHotRoute(0);
      else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        this.SelectHotRoute(1);
      else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        this.SelectHotRoute(2);
      else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        this.SelectHotRoute(3);
      else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        this.SelectHotRoute(4);
      else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        this.SelectHotRoute(5);
      else if (Input.GetKeyDown(KeyCode.Alpha7) && (UI.PrePlayWindowP1.GetHotRouteBlockButton().activeInHierarchy || UI.PrePlayWindowP2.GetHotRouteBlockButton().activeInHierarchy))
        this.SelectHotRoute(6);
      if ((double) UserManager.instance.LeftStickX(userIndex) < -0.5)
        this.SelectHotRoute(0);
      else if ((double) UserManager.instance.LeftStickY(userIndex) > 0.5)
        this.SelectHotRoute(1);
      else if ((double) UserManager.instance.LeftStickX(userIndex) > 0.5)
        this.SelectHotRoute(2);
      else if ((double) UserManager.instance.LeftStickY(userIndex) < -0.5)
        this.SelectHotRoute(3);
      else if (UserManager.instance.Action1WasPressed(userIndex))
        this.SelectHotRoute(4);
      else if (UserManager.instance.Action2WasPressed(userIndex))
      {
        this.SelectHotRoute(5);
      }
      else
      {
        if (!UserManager.instance.Action3WasPressed(userIndex) || !UI.PrePlayWindowP1.GetHotRouteBlockButton().activeInHierarchy && !UI.PrePlayWindowP2.GetHotRouteBlockButton().activeInHierarchy)
          return;
        this.SelectHotRoute(6);
      }
    }
  }
}
