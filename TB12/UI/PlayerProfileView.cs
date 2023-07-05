// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlayerProfileView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProjectConstants;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class PlayerProfileView : MonoBehaviour
  {
    [SerializeField]
    private PlayerProgressStore _playerProgress;
    [SerializeField]
    private TextMeshProUGUI _playerNames;
    [SerializeField]
    private TextMeshProUGUI _playerPoints;
    [SerializeField]
    private TextMeshProUGUI _currentLevelText;
    [SerializeField]
    private TextMeshProUGUI _nextLevelText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Vector2 _progressBounds = Vector2.zero;
    private MaterialPropertyBlock _mpb;

    private void Awake()
    {
      if (this._mpb != null)
        return;
      this._mpb = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
      ProfileProgress progress = this._playerProgress.Progress;
      this._playerNames.text = this._playerProgress.PlayerName;
      this._playerPoints.text = progress.Points.ToString();
      this._currentLevelText.text = progress.Level.ToString();
      this._nextLevelText.text = (progress.Level + 1).ToString();
      this._descriptionText.text = string.Format("QB LEVEL {0}", (object) progress.Level);
      this.SetProgress(progress.Progress);
    }

    private void SetProgress(float value)
    {
      float num = MathUtils.MapRange(value, 0.0f, 1f, this._progressBounds.x, this._progressBounds.y);
      this._meshRenderer.GetPropertyBlock(this._mpb, 4);
      this._mpb.SetFloat(Shaders.Progress, num);
      this._meshRenderer.SetPropertyBlock(this._mpb);
    }
  }
}
