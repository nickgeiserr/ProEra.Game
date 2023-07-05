// Decompiled with JetBrains decompiler
// Type: Framework.BaseState`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using System.Collections;
using UnityEngine;

namespace Framework
{
  public abstract class BaseState<StateId> : ScriptableObject where StateId : Enum
  {
    [SerializeField]
    protected SceneGroupBundle _sceneGroupBundle;
    public bool SaveSeasonDuringLoading;
    protected SceneAssetString _currentScene;

    public abstract StateId Id { get; }

    public SceneGroupBundle SceneBundle => this._sceneGroupBundle;

    public virtual bool showLoadingScreen => true;

    public virtual bool showTransition => true;

    public virtual bool allowPause => true;

    public virtual bool allowRain => false;

    public virtual bool clearFadeOnEntry => true;

    public virtual bool UnloadGameplayScene => true;

    public virtual bool AlwaysUnloadEnvironment => false;

    public virtual IEnumerator Load()
    {
      if (this._sceneGroupBundle != null && this._sceneGroupBundle.GameplayScene.IsValid())
      {
        this._currentScene = this._sceneGroupBundle.GameplayScene;
        yield return (object) PersistentSingleton<LevelManager>.Instance.LoadGameplay(this._currentScene);
      }
      yield return (object) null;
    }

    public virtual IEnumerator Unload()
    {
      if (this.UnloadGameplayScene && this._currentScene.IsValid())
        yield return (object) PersistentSingleton<LevelManager>.Instance.UnloadSceneAsync(this._currentScene);
    }

    public void Enter() => this.OnEnterState();

    public void Exit() => this.OnExitState();

    protected abstract void OnEnterState();

    protected abstract void OnExitState();

    public virtual void ClearState()
    {
    }

    public virtual void WillExit()
    {
    }

    protected Coroutine StartRoutine(IEnumerator routine) => PersistentSingleton<RoutineRunner>.Instance.StartCoroutine(routine);

    protected Coroutine DelayedExecute(float delay, Action cb) => cb != null ? PersistentSingleton<RoutineRunner>.Instance.StartCoroutine(this.DelayedExecuteRoutine(delay, cb)) : (Coroutine) null;

    private IEnumerator DelayedExecuteRoutine(float delay, Action cb)
    {
      yield return (object) new WaitForSeconds(delay);
      Action action = cb;
      if (action != null)
        action();
    }

    public void SetEnvironmentScene(SceneAssetString newEnvironmentScene) => this._sceneGroupBundle.EnvironmentScene = newEnvironmentScene;

    public virtual bool HasCameraFadeOverride(out VRCameraFade.FadeSettings settings)
    {
      settings = VRCameraFade.FadeSettings.Default;
      return false;
    }
  }
}
