// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlayFormationPlayerIcon
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class PlayFormationPlayerIcon : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _playerNumber;
    [SerializeField]
    private Image _iconFill;
    [SerializeField]
    private Transform[] _children;

    private void Awake()
    {
      if (this._children != null)
        return;
      this._children = new Transform[this.transform.childCount];
      for (int index = 0; index < this._children.Length; ++index)
        this._children[index] = this.transform.GetChild(index);
    }

    public void SetPosition(Vector3 pos)
    {
      this.transform.localPosition = pos;
      for (int index = 0; index < this._children.Length; ++index)
        this._children[index].localPosition = pos;
    }

    public void SetIconColor(Color c) => this._iconFill.color = c;

    public void SetPlayerNumber(int i) => this._playerNumber.text = i.ToString();

    public void SetVisible(bool state) => this._iconFill.enabled = state;

    public void OptimizeDrawCalls(Transform[] holders)
    {
      for (int index = holders.Length - 1; index >= 0; --index)
      {
        if (index < this.transform.childCount)
          this.transform.GetChild(index).SetParent(holders[index]);
      }
    }
  }
}
