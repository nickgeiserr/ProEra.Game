// Decompiled with JetBrains decompiler
// Type: SeasonLogoTeamColor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using UnityEngine;

public class SeasonLogoTeamColor : MonoBehaviour
{
  [SerializeField]
  private string colorID = "_TeamColor";
  private int shaderKey_colorID;
  private Renderer _renderer;
  private UniformStore _uniformStore;
  private bool isInitialized;

  private void Awake()
  {
    this.shaderKey_colorID = Shader.PropertyToID(this.colorID);
    this._renderer = this.GetComponent<Renderer>();
    SeasonModeManager.self.OnInitComplete += new System.Action(this.OnTeamChanged);
  }

  private void OnDestroy()
  {
    if ((bool) (UnityEngine.Object) SeasonModeManager.self)
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.OnTeamChanged);
    this._uniformStore = (UniformStore) null;
  }

  private void Start()
  {
    this._uniformStore = SaveManager.GetUniformStore();
    this.isInitialized = true;
  }

  private void OnEnable()
  {
    if (!this.isInitialized)
      return;
    this.Refresh();
  }

  public void OnTeamChanged() => this.Refresh();

  private void Refresh()
  {
    if ((UnityEngine.Object) this._renderer == (UnityEngine.Object) null || (UnityEngine.Object) this._uniformStore == (UnityEngine.Object) null)
      return;
    UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(TeamDataCache.ToTeamUniformId(SeasonModeManager.self.userTeamData.TeamIndex), ETeamUniformFlags.Home);
    if (uniformConfig == null || this._renderer.sharedMaterial.GetColor(this.shaderKey_colorID) == uniformConfig.FontNumbers.OutlineColor.Item2)
      return;
    this._renderer.sharedMaterial.SetColor(this.shaderKey_colorID, uniformConfig.FontNumbers.OutlineColor.Item2);
  }
}
