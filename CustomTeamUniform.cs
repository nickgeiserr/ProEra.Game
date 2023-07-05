// Decompiled with JetBrains decompiler
// Type: CustomTeamUniform
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System.Threading.Tasks;
using UnityEngine;

public class CustomTeamUniform : MonoBehaviour
{
  [SerializeField]
  private ETeamUniformId uniformTeam = ETeamUniformId.Ravens;
  [SerializeField]
  private ETeamUniformFlags uniformType = ETeamUniformFlags.Home;
  [SerializeField]
  private string textureID = "_BaseMap";
  [SerializeField]
  private int playerIndex = -1;
  private int shaderKey_textureID;
  private Renderer _renderer;
  private bool isInitialized;

  private void Start()
  {
    this.isInitialized = true;
    this.shaderKey_textureID = Shader.PropertyToID(this.textureID);
    this._renderer = this.GetComponent<Renderer>();
    this.TryToRefresh();
  }

  private async void TryToRefresh()
  {
    CustomTeamUniform customTeamUniform = this;
    if (await customTeamUniform.Refresh() || !customTeamUniform.enabled)
      return;
    customTeamUniform.TryToRefresh();
  }

  private async Task<bool> Refresh()
  {
    if ((Object) this._renderer == (Object) null)
      return true;
    Texture2D uniformTexture = UniformCapture.GetUniformDiffuseByID(this.uniformTeam, this.uniformType);
    if ((Object) uniformTexture == (Object) null)
      return false;
    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    this._renderer.GetPropertyBlock(mpb);
    if (this.playerIndex >= 0)
    {
      Texture2D[] textsTexture = UniformCapture.GetTextsTexture(TeamDataCache.ToTeamIndex(this.uniformTeam), this.uniformType);
      await Task.Delay(10);
      if (textsTexture == null || textsTexture.Length == 0)
        return false;
      if (mpb != null)
      {
        if ((Object) textsTexture[0] != (Object) null)
          mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_0, (Texture) textsTexture[0]);
        if ((Object) textsTexture[1] != (Object) null)
          mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_1, (Texture) textsTexture[1]);
        if ((Object) textsTexture[2] != (Object) null)
          mpb.SetTexture(UniformCapture.Info.kTextsAtlasMask_2, (Texture) textsTexture[2]);
        mpb.SetFloat(UniformCapture.Info.kPlayerIndex, (float) this.playerIndex);
      }
      textsTexture = (Texture2D[]) null;
    }
    if (mpb != null)
    {
      mpb.SetTexture(this.shaderKey_textureID, (Texture) uniformTexture);
      this._renderer.SetPropertyBlock(mpb);
    }
    return true;
  }
}
