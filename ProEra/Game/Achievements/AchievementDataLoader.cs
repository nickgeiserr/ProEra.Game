// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.AchievementDataLoader
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.StateManagement;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12;
using TB12.AppStates;
using UnityEngine;
using UnityEngine.Events;

namespace ProEra.Game.Achievements
{
  public class AchievementDataLoader : MonoBehaviour
  {
    [SerializeField]
    private TrophyDisplay[] trophyDisplays;
    private bool sceneIsLoaded;
    private bool dataIsLoaded;
    private List<TrophyDisplay> trophiesToAnimate = new List<TrophyDisplay>();
    private RoutineHandle _sceneDataLoadHandler = new RoutineHandle();
    private bool _transitionComplete;

    private AchievementData achievementData => SaveManager.GetAchievementData();

    private void Awake()
    {
      this.achievementData.OnLoadComplete.AddListener(new UnityAction(this.AchievementData_OnLoadComplete));
      if (((IEnumerable<TrophyDisplay>) this.trophyDisplays).Count<TrophyDisplay>() == 0)
      {
        Debug.LogError((object) "TrohpyDisplays not found; Attempting to find them the hard way.");
        this.FindTrophyDisplays();
      }
      GameManager.GameLoadComplete.Link(new System.Action(this.OnSceneLoadComplete));
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnCameraFadeComplete += new System.Action(this.OnStateTranisitionComplete);
      SeasonModeManager.self.OnInitComplete += new System.Action(this.AchievementData_OnLoadComplete);
    }

    private IEnumerator Start()
    {
      yield return (object) this.AwaitSceneDataLoad();
      yield return (object) this.ShowcaseNewTrophies();
    }

    private void OnDestroy()
    {
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.AchievementData_OnLoadComplete);
      this._sceneDataLoadHandler.Stop();
    }

    private void AchievementData_OnLoadComplete()
    {
      int length = this.trophyDisplays.Length;
      for (int index = 0; index < length; ++index)
        this.trophyDisplays[index].InitializeTrophies();
      this.trophiesToAnimate = ((IEnumerable<TrophyDisplay>) this.trophyDisplays).Where<TrophyDisplay>((Func<TrophyDisplay, bool>) (trophy => trophy.IsHighlighted)).ToList<TrophyDisplay>();
      SeasonLockerRoom instance = SeasonLockerRoom.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || !((UnityEngine.Object) instance.GetTrophyRoomPlayerPivot() != (UnityEngine.Object) null))
        return;
      this.dataIsLoaded = true;
    }

    private IEnumerator ShowcaseNewTrophies()
    {
      if (!LinqExtensions.IsNullOrEmpty<TrophyDisplay>((IList<TrophyDisplay>) this.trophiesToAnimate))
      {
        Transform trophyRoomPlayerPivot = SeasonLockerRoom.Instance.GetTrophyRoomPlayerPivot();
        PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(trophyRoomPlayerPivot.position, trophyRoomPlayerPivot.rotation);
        yield return (object) new WaitForSeconds(1f);
        if (!this._transitionComplete)
        {
          WaitForEndOfFrame frameWait = new WaitForEndOfFrame();
          while (!this._transitionComplete)
            yield return (object) frameWait;
          frameWait = (WaitForEndOfFrame) null;
        }
        WaitForSeconds animationDelay = new WaitForSeconds(TrophyDisplay.AnimationDuration);
        int numTrophies = this.trophiesToAnimate.Count;
        for (int i = 0; i < numTrophies; ++i)
        {
          this.trophiesToAnimate[i].PlaySpawnAnimation();
          yield return (object) animationDelay;
        }
      }
    }

    private IEnumerator AwaitSceneDataLoad()
    {
      float elapsedTime = 0.0f;
      while (!this.sceneIsLoaded || !this.dataIsLoaded)
      {
        elapsedTime += Time.deltaTime;
        if ((double) elapsedTime >= 30.0)
        {
          Debug.LogError((object) "Failed awaiting to load scene");
          break;
        }
        yield return (object) null;
      }
    }

    private void OnSceneLoadComplete() => this.sceneIsLoaded = true;

    private void OnStateTranisitionComplete()
    {
      GameState activeState = PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState;
      if (!((UnityEngine.Object) activeState != (UnityEngine.Object) null) || !(((object) activeState).GetType() == typeof (MainMenuActivationState)))
        return;
      this._transitionComplete = true;
    }

    [ContextMenu("Update Trophy Displays")]
    public void FindTrophyDisplays() => this.trophyDisplays = this.GetComponentsInChildren<TrophyDisplay>();
  }
}
