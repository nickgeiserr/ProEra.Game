// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.AResultEnterNameView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using System;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class AResultEnterNameView : UIPanel, ICircularLayoutDataSource
  {
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private CircularLayout[] _layouts;
    [SerializeField]
    private CircularTextItem _itemPrefab;
    private static readonly string[] Chars = new string[36]
    {
      "A",
      "B",
      "C",
      "D",
      "E",
      "F",
      "G",
      "H",
      "I",
      "J",
      "K",
      "L",
      "M",
      "N",
      "O",
      "P",
      "Q",
      "R",
      "S",
      "T",
      "U",
      "V",
      "W",
      "X",
      "Y",
      "Z",
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9"
    };
    public Action onConfirmed;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => AResultEnterNameView.Chars.Length;

    public string UserName
    {
      get
      {
        string userName = "";
        foreach (CircularLayout layout in this._layouts)
        {
          CircularTextItem currentItem = (CircularTextItem) layout.CurrentItem;
          userName += currentItem.localizationText;
        }
        return userName;
      }
    }

    public void SetupItem(int itemIndex, CircularLayoutItem layoutItem)
    {
      if (itemIndex >= this.itemCount || itemIndex < 0)
        Debug.LogError((object) string.Format("Wrong index! : {0}", (object) itemIndex));
      else
        ((CircularTextItem) layoutItem).localizationText = AResultEnterNameView.Chars[itemIndex];
    }

    protected override void DidAppear() => this._okButton.onClick += new Action(this.OkHandler);

    protected override void DidDisappear()
    {
      this._okButton.onClick -= new Action(this.OkHandler);
      Action onConfirmed = this.onConfirmed;
      if (onConfirmed == null)
        return;
      onConfirmed();
    }

    [ContextMenu("OK")]
    private void OkHandler() => this.Hide();
  }
}
