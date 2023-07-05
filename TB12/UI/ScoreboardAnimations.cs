// Decompiled with JetBrains decompiler
// Type: TB12.UI.ScoreboardAnimations
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using ProEra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TB12.UI
{
  public class ScoreboardAnimations : MonoBehaviour
  {
    public static ScoreboardAnimations Instance;
    [SerializeField]
    private Animator ScreenContentBroadcastAnimator;
    [SerializeField]
    private GameObject GameInfo;
    [SerializeField]
    private GameObject GameInfoVert;
    [SerializeField]
    private GameObject BroadcastWipeObj;
    [SerializeField]
    private GameObject BroadcastCameraObj;
    [SerializeField]
    private Animation BroadcastAnimator;
    [SerializeField]
    private RuntimeAnimatorController RegularController;
    private UniformStore uniformStore;
    private Dictionary<ScoreboardAnimations.BoardAnimType, ScoreboardAnimations.ContentItem> _dicItems = new Dictionary<ScoreboardAnimations.BoardAnimType, ScoreboardAnimations.ContentItem>();
    private Dictionary<int, Color> _teamColors = new Dictionary<int, Color>();
    [SerializeField]
    private ScoreboardAnimations.ContentItem[] _contentItems;
    private ScoreboardAnimations.BoardAnimType currentPlay;
    private GameObject _currentGameInfo;

    private void Awake()
    {
      ScoreboardAnimations.Instance = this;
      this.uniformStore = SaveManager.GetUniformStore();
      foreach (ScoreboardAnimations.ContentItem contentItem in this._contentItems)
        this._dicItems.Add(contentItem.animType, contentItem);
      if (!((UnityEngine.Object) this.ScreenContentBroadcastAnimator != (UnityEngine.Object) null))
        return;
      this._currentGameInfo = (UnityEngine.Object) this.ScreenContentBroadcastAnimator.runtimeAnimatorController == (UnityEngine.Object) this.RegularController ? this.GameInfo : this.GameInfoVert;
    }

    private void OnDestroy()
    {
      ScoreboardAnimations.Instance = (ScoreboardAnimations) null;
      this.uniformStore = (UniformStore) null;
    }

    private void Start()
    {
      this.TurnOffScreenAnimationsForMiniCamp();
      if (!((UnityEngine.Object) this.ScreenContentBroadcastAnimator != (UnityEngine.Object) null))
        return;
      this.ScreenContentBroadcastAnimator.enabled = false;
      this.GetComponent<Animator>().enabled = false;
    }

    private void TurnOffScreenAnimationsForMiniCamp()
    {
      bool flag = false;
      for (int index = 0; index < SceneManager.sceneCount; ++index)
      {
        if (SceneManager.GetSceneAt(index).name.Contains("MiniCamp"))
          flag = true;
      }
      if (!flag)
        return;
      foreach (Behaviour componentsInChild in this.GetComponentsInChildren<Animator>())
        componentsInChild.enabled = false;
    }

    public static void SetActiveBroadcastView(bool enabled)
    {
      if (!((UnityEngine.Object) ScoreboardAnimations.Instance != (UnityEngine.Object) null) || AppState.GameMode == EGameMode.k2MD)
        return;
      ScoreboardAnimations.Instance.DoBroadcastSwitch(enabled);
    }

    private async void DoBroadcastSwitch(bool enabled)
    {
      if (this._currentGameInfo.activeSelf != enabled)
        return;
      if (this.currentPlay == ScoreboardAnimations.BoardAnimType.None)
      {
        this.BroadcastAnimator.Play();
        await Task.Delay(1250);
      }
      this._currentGameInfo.SetActive(!enabled);
    }

    public static void PlayAnimation(ScoreboardAnimations.BoardAnimType animType, int teamIndex)
    {
      if (!((UnityEngine.Object) ScoreboardAnimations.Instance != (UnityEngine.Object) null))
        return;
      ScoreboardAnimations.Instance.PlayAnimationInstanced(animType, teamIndex);
    }

    public static void DisplayBoard(ScoreboardAnimations.BoardAnimType animType)
    {
      if (!((UnityEngine.Object) ScoreboardAnimations.Instance != (UnityEngine.Object) null))
        return;
      ScoreboardAnimations.Instance.DisplayBoardInstanced(animType);
    }

    public static void HideCurrentBoard()
    {
      if (!((UnityEngine.Object) ScoreboardAnimations.Instance != (UnityEngine.Object) null))
        return;
      ScoreboardAnimations.Instance.HideCurrentBoardInstanced();
    }

    public static GameScoreboardUI[] GetScoreboards() => (UnityEngine.Object) ScoreboardAnimations.Instance != (UnityEngine.Object) null ? ScoreboardAnimations.Instance.transform.GetComponentsInChildren<GameScoreboardUI>() : (GameScoreboardUI[]) null;

    private void Update()
    {
      if (Time.frameCount % 15 != 0 || !this.PlayHasAnimation(this.currentPlay))
        return;
      ScoreboardAnimations.ContentItem dicItem = this._dicItems[this.currentPlay];
      if (dicItem == null || (double) dicItem.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99000000953674316)
        return;
      dicItem.animator.enabled = false;
      dicItem.animator.gameObject.SetActive(false);
      this.currentPlay = ScoreboardAnimations.BoardAnimType.None;
    }

    private void PlayAnimationInstanced(ScoreboardAnimations.BoardAnimType animType, int teamIndex)
    {
      if (this.currentPlay != ScoreboardAnimations.BoardAnimType.None)
      {
        ScoreboardAnimations.ContentItem dicItem = this._dicItems[this.currentPlay];
        if (dicItem != null)
        {
          if ((UnityEngine.Object) dicItem.animator != (UnityEngine.Object) null)
          {
            dicItem.animator.enabled = false;
            dicItem.animator.gameObject.SetActive(false);
          }
          if ((UnityEngine.Object) dicItem.staticObject != (UnityEngine.Object) null)
            dicItem.staticObject.SetActive(false);
        }
      }
      ScoreboardAnimations.ContentItem dicItem1 = this._dicItems[animType];
      Color color = PersistentData.GetUserTeamIndex() == teamIndex ? PersistentData.GetUserTeam().GetPrimaryColor() : PersistentData.GetCompTeam().GetPrimaryColor();
      foreach (Renderer renderer in dicItem1.rendererTeamColor)
        renderer.sharedMaterial.SetColor("_Color", color);
      if ((UnityEngine.Object) dicItem1.animator != (UnityEngine.Object) null)
      {
        dicItem1.animator.enabled = true;
        dicItem1.animator.gameObject.SetActive(true);
        dicItem1.animator.Play(0);
      }
      this.currentPlay = animType;
    }

    private void DisplayBoardInstanced(ScoreboardAnimations.BoardAnimType animType)
    {
      this.HideCurrentBoardInstanced();
      if (animType == ScoreboardAnimations.BoardAnimType.None)
        return;
      this._dicItems[animType].staticObject.SetActive(true);
      this.currentPlay = animType;
    }

    private void HideCurrentBoardInstanced()
    {
      if (this.currentPlay == ScoreboardAnimations.BoardAnimType.None)
        return;
      this._dicItems[this.currentPlay].staticObject.SetActive(false);
      this.currentPlay = ScoreboardAnimations.BoardAnimType.None;
    }

    private bool PlayHasAnimation(ScoreboardAnimations.BoardAnimType boardAnimType)
    {
      bool flag = true;
      switch (boardAnimType)
      {
        case ScoreboardAnimations.BoardAnimType.None:
        case ScoreboardAnimations.BoardAnimType.BossMode:
        case ScoreboardAnimations.BoardAnimType.Dodgeball:
        case ScoreboardAnimations.BoardAnimType.Throwing:
        case ScoreboardAnimations.BoardAnimType.Lobby:
          flag = false;
          break;
      }
      return flag;
    }

    public enum BoardAnimType
    {
      None,
      FirstDown,
      FieldGoal,
      Fumble,
      TouchDown,
      Defense,
      Interception,
      Safety,
      BossMode,
      Dodgeball,
      Throwing,
      Lobby,
    }

    [Serializable]
    public class ContentItem
    {
      public ScoreboardAnimations.BoardAnimType animType;
      public Animator animator;
      public GameObject staticObject;
      public Renderer[] rendererTeamColor;
    }
  }
}
