// Decompiled with JetBrains decompiler
// Type: TB12.UI.LeaderboardEntryItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class LeaderboardEntryItem : RecyclingListViewItem
  {
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public void Setup(Color color, string userName, int score)
    {
      this._image.color = color;
      this._nameText.text = userName;
      this._scoreText.text = string.Format("{0}", (object) score);
    }
  }
}
