// Decompiled with JetBrains decompiler
// Type: Framework.LevelManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Framework
{
  public class LevelManager : PersistentSingleton<LevelManager>
  {
    private readonly List<string> _loadedLevels = new List<string>();
    private SceneAssetString? _currentEnvironmentScene;
    private GameObjectReference _currentEnvironmentObject;

    public IEnumerator LoadGameplay(SceneAssetString scene, LoadSceneMode mode = LoadSceneMode.Additive)
    {
      yield return (object) this.LoadSceneAsync(scene, mode);
    }

    public IEnumerator LoadEnvironment(AssetReference prefab, SceneAssetString sceneTimeOfDay)
    {
      LevelManager levelManager = this;
      if ((UnityEngine.Object) levelManager._currentEnvironmentObject.Value != (UnityEngine.Object) null)
      {
        if (levelManager._currentEnvironmentObject.AssetGUID == prefab.AssetGUID)
        {
          yield break;
        }
        else
        {
          levelManager.UnloadObject(levelManager._currentEnvironmentObject);
          levelManager._currentEnvironmentObject = new GameObjectReference();
        }
      }
      if (levelManager._currentEnvironmentScene.HasValue && levelManager._currentEnvironmentScene.Value.IsValid())
        yield return (object) levelManager.UnloadSceneAsync(levelManager._currentEnvironmentScene.Value);
      levelManager._currentEnvironmentScene = new SceneAssetString?();
      yield return (object) levelManager.LoadEnvironment(sceneTimeOfDay);
      yield return (object) levelManager.LoadEnvironmentAsync(prefab);
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(levelManager.\u003CLoadEnvironment\u003Eb__4_0));
      yield return (object) null;
    }

    private async Task LoadEnvironmentAsync(AssetReference assetReference)
    {
      GameObjectReference environmentObject = this._currentEnvironmentObject;
      this._currentEnvironmentObject.Value = await AddressablesData.instance.InstantiateAsync(assetReference, Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 90f, 0.0f)), (Transform) null);
      this._currentEnvironmentObject.AssetGUID = assetReference.AssetGUID;
    }

    public IEnumerator LoadEnvironment(SceneAssetString scene)
    {
      if (this._currentEnvironmentScene.HasValue && this._currentEnvironmentScene.Value.IsValid())
      {
        if (this._currentEnvironmentScene.Value.Name == scene.Name)
        {
          yield break;
        }
        else
        {
          yield return (object) this.UnloadSceneAsync(this._currentEnvironmentScene.Value);
          this._currentEnvironmentScene = new SceneAssetString?();
        }
      }
      if ((UnityEngine.Object) this._currentEnvironmentObject.Value != (UnityEngine.Object) null)
        this.UnloadObject(this._currentEnvironmentObject);
      this._currentEnvironmentObject = new GameObjectReference();
      yield return (object) this.LoadSceneAsync(scene);
      SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.Name));
      this._currentEnvironmentScene = new SceneAssetString?(scene);
    }

    public IEnumerator UnloadCurrentEnvironment()
    {
      if (this._currentEnvironmentScene.HasValue && this._currentEnvironmentScene.Value.IsValid())
      {
        yield return (object) this.UnloadSceneAsync(this._currentEnvironmentScene.Value);
        this._currentEnvironmentScene = new SceneAssetString?();
      }
      if ((UnityEngine.Object) this._currentEnvironmentObject.Value != (UnityEngine.Object) null)
        this.UnloadObject(this._currentEnvironmentObject);
      this._currentEnvironmentObject = new GameObjectReference();
    }

    private IEnumerator LoadSceneAsync(SceneAssetString scene, LoadSceneMode mode = LoadSceneMode.Additive)
    {
      if (scene.IsValid() && this.should_load_level(scene))
      {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene.Name, mode);
        if (op != null)
        {
          while (!op.isDone)
            yield return (object) null;
          this._loadedLevels.Add(scene.Guid);
          yield return (object) null;
        }
      }
    }

    public void UnloadCurrentStadium()
    {
      if ((UnityEngine.Object) this._currentEnvironmentObject.Value != (UnityEngine.Object) null)
        this.UnloadObject(this._currentEnvironmentObject);
      this._currentEnvironmentObject = new GameObjectReference();
    }

    private void UnloadObject(GameObjectReference target) => AddressablesData.DestroyGameObject(target.Value);

    public IEnumerator UnloadSceneAsync(SceneAssetString scene)
    {
      if (scene.IsValid())
      {
        AsyncOperation op = (AsyncOperation) null;
        try
        {
          op = SceneManager.UnloadSceneAsync(scene.Name);
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
        }
        if (op != null)
        {
          while (!op.isDone)
            yield return (object) null;
          this._loadedLevels.Remove(scene.Guid);
        }
      }
    }

    public void ForceReloadLevel()
    {
      this.StartCoroutine(this.UnloadSceneAsync(this._currentEnvironmentScene.Value));
      this._currentEnvironmentScene = new SceneAssetString?();
    }

    private bool should_load_level(SceneAssetString level) => !this._loadedLevels.Contains(level.Guid);
  }
}
