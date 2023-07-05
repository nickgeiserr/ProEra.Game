// Decompiled with JetBrains decompiler
// Type: DropdownControllerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using UnityEngine;
using UnityEngine.UI;

public class DropdownControllerManager : MonoBehaviour
{
  public float scrollSpeed = 0.01f;
  private Scrollbar scrollbar;

  private void FindScrollbar() => this.scrollbar = this.GetComponentInChildren<Scrollbar>();

  private void Update()
  {
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.ManageControllerInput();
  }

  private void ManageControllerInput()
  {
    float num1 = UserManager.instance.RightStickY(Player.One);
    double num2 = (double) UserManager.instance.RightStickX(Player.One);
    if ((double) num1 > 0.40000000596046448)
    {
      this.ScrollUp();
    }
    else
    {
      if ((double) num1 >= -0.40000000596046448)
        return;
      this.ScrollDown();
    }
  }

  private void ScrollUp()
  {
    if ((Object) this.scrollbar == (Object) null)
      this.FindScrollbar();
    if (!((Object) this.scrollbar != (Object) null))
      return;
    this.scrollbar.value += this.scrollSpeed;
  }

  private void ScrollDown()
  {
    if ((Object) this.scrollbar == (Object) null)
      this.FindScrollbar();
    if (!((Object) this.scrollbar != (Object) null))
      return;
    this.scrollbar.value -= this.scrollSpeed;
  }
}
