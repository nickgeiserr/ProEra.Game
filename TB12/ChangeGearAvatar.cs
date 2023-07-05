// Decompiled with JetBrains decompiler
// Type: TB12.ChangeGearAvatar
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TB12
{
  public class ChangeGearAvatar : MonoBehaviour
  {
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private List<string> _teamNames;
    [SerializeField]
    private List<Texture2D> _textures;
    [SerializeField]
    private List<AssetReferenceTexture2D> _textureReferences;
    private int _currentIndex;
    private MaterialPropertyBlock _mpb;

    public string Selection => this._teamNames[this._currentIndex];

    private void Awake()
    {
      this._mpb = new MaterialPropertyBlock();
      this._renderer.GetPropertyBlock(this._mpb);
    }

    public void Setup(string team)
    {
      int num = this._teamNames.IndexOf(team);
      if (num < 0)
      {
        Debug.LogError((object) "Something wen't wrong with team selection, falling back to Ravens");
        num = 0;
      }
      this._currentIndex = num;
      this.update_selection();
    }

    public void GetData(
      out string teamName,
      out Texture2D texture,
      out AssetReferenceTexture2D textureRef)
    {
      teamName = this._teamNames[this._currentIndex];
      texture = this._textures[this._currentIndex];
      textureRef = this._textureReferences[this._currentIndex];
    }

    public string Next()
    {
      if (this._currentIndex >= this._teamNames.Count - 1)
        this._currentIndex = 0;
      else
        ++this._currentIndex;
      this.update_selection();
      return this.Selection;
    }

    public string Previous()
    {
      if (this._currentIndex <= 0)
        this._currentIndex = this._teamNames.Count - 1;
      else
        --this._currentIndex;
      this.update_selection();
      return this.Selection;
    }

    private void update_selection()
    {
      Texture2D texture = this._textures[this._currentIndex];
      this._mpb.SetTexture(WorldConstants.Player.Basemap, (Texture) texture);
      this._renderer.SetPropertyBlock(this._mpb);
    }
  }
}
