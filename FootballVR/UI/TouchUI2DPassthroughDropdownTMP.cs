// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchUI2DPassthroughDropdownTMP
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using TMPro;
using UnityEngine;

namespace FootballVR.UI
{
  [RequireComponent(typeof (TMP_Dropdown), typeof (IButton))]
  public class TouchUI2DPassthroughDropdownTMP : MonoBehaviour
  {
    private IButton thisButton;
    private TMP_Dropdown thisDropdown;
    private BoxCollider thisCollider;

    private void Start()
    {
      this.thisButton = this.GetComponent<IButton>();
      this.thisDropdown = this.GetComponent<TMP_Dropdown>();
      this.thisCollider = this.GetComponent<BoxCollider>();
      this.thisButton.onClick += new Action(this.ToggleExpand);
    }

    private void Update()
    {
    }

    [ContextMenu("ToggleExpand")]
    private void ToggleExpand()
    {
      if ((UnityEngine.Object) this.thisDropdown == (UnityEngine.Object) null)
        return;
      if (this.thisDropdown.IsExpanded)
        this.thisDropdown.Hide();
      else
        this.thisDropdown.Show();
    }
  }
}
