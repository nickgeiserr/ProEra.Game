// Decompiled with JetBrains decompiler
// Type: TunnelTeamUniform
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System.Threading.Tasks;
using UDB;
using UnityEngine;

public class TunnelTeamUniform : MonoBehaviour
{
  [SerializeField]
  private int playerIndex = -1;
  [SerializeField]
  private bool SkipBakeTextures;
  private Renderer _renderer;
  private bool isInitialized;

  private void Start()
  {
    this.isInitialized = true;
    this._renderer = this.GetComponent<Renderer>();
    this.TryToRefresh().ConfigureAwait(false);
  }

  private async System.Threading.Tasks.Task TryToRefresh()
  {
    TunnelTeamUniform tunnelTeamUniform = this;
    do
      ;
    while (!await tunnelTeamUniform.Refresh() && tunnelTeamUniform.enabled);
  }

  private async Task<bool> Refresh()
  {
    if ((Object) this._renderer == (Object) null)
      return true;
    int uniformId = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide == 1 ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntAwayTeam : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntHomeTeam;
    int uniformFlag = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide == 1 ? 1 : 2;
    Texture2D uniformTexture = UniformCapture.GetUniformDiffuse(uniformId, (ETeamUniformFlags) uniformFlag);
    await System.Threading.Tasks.Task.Delay(10);
    if ((Object) uniformTexture == (Object) null)
      return false;
    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    this._renderer.GetPropertyBlock(mpb);
    if (this.playerIndex >= 0)
    {
      if (!this.SkipBakeTextures)
      {
        Texture2D[] textsTexture = UniformCapture.GetTextsTexture(uniformId, (ETeamUniformFlags) uniformFlag);
        await System.Threading.Tasks.Task.Delay(10);
        if (textsTexture == null || textsTexture.Length == 0)
          return false;
        mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_0, (Texture) textsTexture[0]);
        mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_1, (Texture) textsTexture[1]);
        mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_2, (Texture) textsTexture[2]);
        textsTexture = (Texture2D[]) null;
      }
      mpb.SetFloat(UniformCapture.Info.kPlayerIndex, (float) this.playerIndex);
    }
    mpb.SetTexture(UniformCapture.Info.kBaseMap, (Texture) uniformTexture);
    this._renderer.SetPropertyBlock(mpb);
    return true;
  }
}
