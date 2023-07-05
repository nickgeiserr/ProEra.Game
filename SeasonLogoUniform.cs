// Decompiled with JetBrains decompiler
// Type: SeasonLogoUniform
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using System;
using System.Threading.Tasks;
using UDB;
using UnityEngine;

public class SeasonLogoUniform : MonoBehaviour
{
  private static bool isUpdated;
  [SerializeField]
  private string textureID = "_MainTex";
  [SerializeField]
  private bool isInstancedMaterial;
  private int shaderKey_textureID;
  private Renderer _renderer;
  private UniformStore _uniformStore;
  private bool isInitialized;

  private void Awake()
  {
    this.shaderKey_textureID = Shader.PropertyToID(this.textureID);
    this._renderer = this.GetComponent<Renderer>();
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged += new System.Action(this.OnTeamChanged);
  }

  private void OnDestroy()
  {
    if ((bool) (UnityEngine.Object) SingletonBehaviour<PersistentData, MonoBehaviour>.instance)
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged -= new System.Action(this.OnTeamChanged);
    this._uniformStore = (UniformStore) null;
  }

  private void Start()
  {
    this._uniformStore = SaveManager.GetUniformStore();
    this.isInitialized = true;
    SeasonLogoUniform.isUpdated = false;
    this.TryToRefresh();
  }

  public void OnTeamChanged()
  {
    SeasonLogoUniform.isUpdated = false;
    this.TryToRefresh();
  }

  private async void TryToRefresh()
  {
    SeasonLogoUniform seasonLogoUniform = this;
    Task<bool> taskRefresh = seasonLogoUniform.Refresh();
    int num = await taskRefresh ? 1 : 0;
    if (taskRefresh.Result)
      taskRefresh = (Task<bool>) null;
    else if (!seasonLogoUniform.enabled)
    {
      taskRefresh = (Task<bool>) null;
    }
    else
    {
      await System.Threading.Tasks.Task.Delay(10);
      seasonLogoUniform.TryToRefresh();
      taskRefresh = (Task<bool>) null;
    }
  }

  private async Task<bool> Refresh()
  {
    SeasonLogoUniform seasonLogoUniform = this;
    if ((UnityEngine.Object) seasonLogoUniform._renderer == (UnityEngine.Object) null || (UnityEngine.Object) seasonLogoUniform._uniformStore == (UnityEngine.Object) null || SeasonLogoUniform.isUpdated && !seasonLogoUniform.isInstancedMaterial)
      return false;
    // ISSUE: reference to a compiler-generated method
    Task<UniformConfig> task = new Task<UniformConfig>(new Func<UniformConfig>(seasonLogoUniform.\u003CRefresh\u003Eb__12_0));
    task.Start();
    UniformConfig uniformConfig = await task;
    if (task.Result == null)
      return false;
    Texture2D basemapAlternative = task.Result.BasemapAlternative;
    seasonLogoUniform._renderer.sharedMaterial.SetTexture(seasonLogoUniform.shaderKey_textureID, (Texture) basemapAlternative);
    if (!seasonLogoUniform.isInstancedMaterial)
      SeasonLogoUniform.isUpdated = true;
    return true;
  }
}
