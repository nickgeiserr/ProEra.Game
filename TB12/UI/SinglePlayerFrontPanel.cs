// Decompiled with JetBrains decompiler
// Type: TB12.UI.SinglePlayerFrontPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using TB12.GameplayData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class SinglePlayerFrontPanel : UIPanel
  {
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Image[] _starsImages;

    public void ShowChallenge(GameLevel level, ProfileProgress.Entry data)
    {
      this._titleText.text = "Level " + level.name;
      string[] qb = level.qb;
      int num = qb == null ? 0 : (qb.Length != 0 ? 1 : 0);
      string str = num != 0 ? qb[0] : string.Empty;
      if (num != 0)
      {
        for (int index = 1; index < qb.Length; ++index)
          str = str + ", " + qb[index];
      }
      this._levelText.text = string.IsNullOrEmpty(str) ? level.description.ToUpper() ?? "" : level.description.ToUpper() + "\r\nVS\r\n" + str;
      foreach (Behaviour starsImage in this._starsImages)
        starsImage.enabled = false;
      if (data != null)
      {
        this._starsImages[Mathf.Clamp(data.Star, 1, 3) - 1].enabled = true;
        this._scoreText.text = string.Format("SCORE\r\n{0}", (object) data.Score);
      }
      else
        this._scoreText.text = "INCOMPLETE";
    }
  }
}
