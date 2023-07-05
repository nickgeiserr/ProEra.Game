// Decompiled with JetBrains decompiler
// Type: SeasonLogoFloor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using System;
using System.Threading.Tasks;
using UDB;
using UnityEngine;

public class SeasonLogoFloor : MonoBehaviour
{
  private static bool isUpdated;
  [SerializeField]
  private string textureID = "_Logo";
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
  }

  private void OnEnable()
  {
    SeasonLogoFloor.isUpdated = false;
    this.TryToRefresh();
  }

  private async void TryToRefresh()
  {
    SeasonLogoFloor seasonLogoFloor = this;
    Task<bool> taskRefresh = seasonLogoFloor.Refresh();
    int num = await taskRefresh ? 1 : 0;
    if (taskRefresh.Result)
      taskRefresh = (Task<bool>) null;
    else if (!seasonLogoFloor.enabled)
    {
      taskRefresh = (Task<bool>) null;
    }
    else
    {
      await System.Threading.Tasks.Task.Delay(10);
      seasonLogoFloor.TryToRefresh();
      taskRefresh = (Task<bool>) null;
    }
  }

  public void OnTeamChanged()
  {
    SeasonLogoFloor.isUpdated = false;
    this.TryToRefresh();
  }

  private async Task<bool> Refresh()
  {
    SeasonLogoFloor seasonLogoFloor = this;
    if ((UnityEngine.Object) seasonLogoFloor._renderer == (UnityEngine.Object) null || (UnityEngine.Object) seasonLogoFloor._uniformStore == (UnityEngine.Object) null || SeasonLogoFloor.isUpdated && !seasonLogoFloor.isInstancedMaterial)
      return false;
    // ISSUE: reference to a compiler-generated method
    Task<UniformConfig> task = new Task<UniformConfig>(new Func<UniformConfig>(seasonLogoFloor.\u003CRefresh\u003Eb__13_0));
    task.Start();
    UniformConfig uniformConfig = await task;
    if (task.Result == null)
      return false;
    Texture2D fieldLogo = task.Result.GetFieldLogo(new CacheParams(false));
    seasonLogoFloor._renderer.sharedMaterial.SetTexture(seasonLogoFloor.shaderKey_textureID, (Texture) fieldLogo);
    if (!seasonLogoFloor.isInstancedMaterial)
      SeasonLogoFloor.isUpdated = true;
    return true;
  }
}
