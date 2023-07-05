// Decompiled with JetBrains decompiler
// Type: TB12.UI.SelectPlayerItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class SelectPlayerItem : RecyclingListViewItem
  {
    [SerializeField]
    private TextMeshProUGUI _place;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _points;
    [SerializeField]
    private GameObject _baseType;
    [SerializeField]
    private GameObject _newType;

    public void SetupNew()
    {
      this._baseType.SetActive(false);
      this._newType.SetActive(true);
    }

    public void SetupProfile(int index, int score, string text)
    {
      this._place.text = index.ToString();
      this._points.text = score.ToString();
      this._name.text = text;
      this._baseType.SetActive(true);
      this._newType.SetActive(false);
    }
  }
}
