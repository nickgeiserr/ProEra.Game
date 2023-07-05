// Decompiled with JetBrains decompiler
// Type: TB12.ThrowMultiplayerLeaderboard
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12
{
  public class ThrowMultiplayerLeaderboard : MonoBehaviour
  {
    [SerializeField]
    private List<TextMeshProUGUI> _names;
    [SerializeField]
    private List<TextMeshProUGUI> _scores;
    [SerializeField]
    private List<Image> _images;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private MultiplayerStore _multiplayerStore;
    [SerializeField]
    private UniformLogoStore _customUniformStore;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private bool _trackOnlyScore;

    private void Awake()
    {
      this.HideAll();
      this._multiplayerStore.OnDataChanged.Link(new System.Action(this.DataChangedHandler));
      LeaderboardVisibilityBroadcaster.ShowScreen.OnValueChanged += new Action<bool>(this.SetCanvasVisibility);
      this.SetCanvasVisibility((bool) LeaderboardVisibilityBroadcaster.ShowScreen);
      this.DataChangedHandler();
    }

    private void OnDestroy() => LeaderboardVisibilityBroadcaster.ShowScreen.OnValueChanged -= new Action<bool>(this.SetCanvasVisibility);

    private void HideAll()
    {
      if (UnityState.quitting)
        return;
      foreach (Behaviour name in this._names)
        name.enabled = false;
      foreach (Behaviour score in this._scores)
        score.enabled = false;
      foreach (Behaviour image in this._images)
        image.enabled = false;
    }

    private void SetCanvasVisibility(bool visible) => this._canvas.enabled = visible;

    private void DataChangedHandler()
    {
      int num = Mathf.Min(this._multiplayerStore.PlayerDatas.Count, 6);
      this.HideAll();
      for (int index = 0; index < num; ++index)
      {
        PlayerStatsData playerData = this._multiplayerStore.PlayerDatas[index];
        PlayerCustomization playerCustomization;
        if (this._playerProfile.Profiles.TryGetValue(playerData.playerId, out playerCustomization))
        {
          this._names[index].enabled = true;
          this._scores[index].enabled = true;
          this._images[index].enabled = true;
          UniformLogo uniformLogo = this._customUniformStore.GetUniformLogo(playerCustomization.Uniform.Value);
          this._scores[index].text = this._trackOnlyScore ? string.Format("{0}", (object) playerData.score) : string.Format("{0}/{1}   {2}", (object) playerData.throwHits, (object) playerData.throwsMade, (object) playerData.score);
          this._names[index].text = (string) playerCustomization.LastName;
          this._images[index].sprite = uniformLogo.teamLogo;
        }
      }
    }
  }
}
