// Decompiled with JetBrains decompiler
// Type: UDB.CircularMenuManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class CircularMenuManager : SingletonBehaviour<CircularMenuManager, MonoBehaviour>
  {
    public CircularMenu circularMenu;

    private void _OpenMenu(Vector3 inputPosition) => this.circularMenu.ShowButtons(inputPosition);

    private void _CloseMenu() => this.circularMenu.HideButtons();

    public static void OpenMenu(Vector3 inputPosition) => SingletonBehaviour<CircularMenuManager, MonoBehaviour>.instance._OpenMenu(inputPosition);

    public static void CloseMenu() => SingletonBehaviour<CircularMenuManager, MonoBehaviour>.instance._CloseMenu();
  }
}
