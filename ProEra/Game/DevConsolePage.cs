// Decompiled with JetBrains decompiler
// Type: ProEra.Game.DevConsolePage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VRKeyboard.Utils;

namespace ProEra.Game
{
  public class DevConsolePage : TabletPage
  {
    private static DevConsolePage _instance;
    private ITabletPageBase currentWindow;
    private ITabletPageBase previousWindow;
    [SerializeField]
    private TabletPage[] childPages;
    [SerializeField]
    private TouchUI2DButton _btnClose;
    [SerializeField]
    private KeyboardManager _keyboard;
    [SerializeField]
    private TouchDrag3D ownerTouchDrag3D;
    [Space(10f)]
    [SerializeField]
    private TouchUI2DButton _btnNextPage;
    [SerializeField]
    private TouchUI2DButton _btnPrevPage;
    [SerializeField]
    private TMP_Text labelPageName;
    private List<TabletPage.Pages> devPages = new List<TabletPage.Pages>();

    public static DevConsolePage Instance
    {
      get
      {
        if ((UnityEngine.Object) DevConsolePage._instance == (UnityEngine.Object) null)
          DevConsolePage._instance = UnityEngine.Object.FindObjectOfType<DevConsolePage>();
        return DevConsolePage._instance;
      }
    }

    private void Awake()
    {
      DevConsolePage._instance = this;
      Array values = Enum.GetValues(typeof (TabletPage.Pages));
      string[] names = Enum.GetNames(typeof (TabletPage.Pages));
      int index = 0;
      foreach (string str in names)
      {
        if (str.Contains("Dev"))
          this.devPages.Add((TabletPage.Pages) values.GetValue(index));
        ++index;
      }
      this._btnClose.onClick += new Action(this.HandleCloseButton);
      this._btnNextPage.onClick += new Action(this.HandleNextPage);
      this._btnPrevPage.onClick += new Action(this.HandlePrevPage);
      this._pageType = TabletPage.Pages.DevConsole;
      this.currentWindow = (ITabletPageBase) this;
      this.previousWindow = (ITabletPageBase) this;
      this.labelPageName.text = this.currentWindow.GetPageType().ToString().Substring(3);
    }

    private void Start()
    {
      this.OpenWindow();
      foreach (TabletPage childPage in this.childPages)
      {
        if (!((UnityEngine.Object) childPage == (UnityEngine.Object) null))
          childPage.RegisterMainPage((TabletPage) this);
      }
    }

    private void OnDestroy()
    {
      this._btnClose.onClick -= new Action(this.HandleCloseButton);
      this._btnNextPage.onClick -= new Action(this.HandleNextPage);
      this._btnPrevPage.onClick -= new Action(this.HandlePrevPage);
      DevConsolePage._instance = (DevConsolePage) null;
    }

    private void HandleCloseButton() => DevConsolePage.SelfDestroy();

    private void HandleNextPage()
    {
      TabletPage.Pages pageType = this.currentWindow.GetPageType();
      if (pageType == this.devPages[this.devPages.Count - 1])
        this.OpenPage(this.devPages[0]);
      else
        this.OpenPage(pageType + 1);
    }

    private void HandlePrevPage()
    {
      TabletPage.Pages pageType = this.currentWindow.GetPageType();
      if (pageType == this.devPages[0])
        this.OpenPage(this.devPages[this.devPages.Count - 1]);
      else
        this.OpenPage(pageType - 1);
    }

    public static void Show()
    {
      Debug.Log((object) "DevConsolePage -> Show");
      if ((UnityEngine.Object) DevConsolePage.Instance == (UnityEngine.Object) null)
        return;
      DevConsolePage.Instance.ownerTouchDrag3D.gameObject.SetActive(true);
      DevConsolePage.Instance.SetOnLeftHand();
    }

    public static void Hide()
    {
      Debug.Log((object) "DevConsolePage -> Hide");
      if ((UnityEngine.Object) DevConsolePage.Instance == (UnityEngine.Object) null)
        return;
      DevConsolePage.Instance.PutMeBack();
      DevConsolePage.Instance.ownerTouchDrag3D.gameObject.SetActive(false);
    }

    public static void SelfDestroy()
    {
      if ((UnityEngine.Object) DevConsolePage.Instance == (UnityEngine.Object) null || (UnityEngine.Object) DevConsolePage.Instance.ownerTouchDrag3D == (UnityEngine.Object) null || (UnityEngine.Object) DevConsolePage.Instance.ownerTouchDrag3D.gameObject == (UnityEngine.Object) null)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) DevConsolePage.Instance.ownerTouchDrag3D.gameObject);
    }

    public void PutMeBack()
    {
      if (!((UnityEngine.Object) this.ownerTouchDrag3D != (UnityEngine.Object) null))
        return;
      this.ownerTouchDrag3D.Reset((ITouchInput) null);
    }

    public bool SetOnLeftHand()
    {
      if ((UnityEngine.Object) PlayerAvatar.Instance == (UnityEngine.Object) null || (UnityEngine.Object) PlayerAvatar.Instance.LeftController == (UnityEngine.Object) null)
        return false;
      PlayerAvatar.Instance.LeftController.Set3DObjectInHand((ITouchGrabbable) this.ownerTouchDrag3D);
      PlayerAvatar.Instance.LeftController.SetWristbandColliderEnabled(false);
      this.ownerTouchDrag3D.Lock(true);
      return true;
    }

    public KeyboardManager GetKeyboard() => this._keyboard;

    public void ShowKeyboard(TMP_InputField input)
    {
      this._keyboard.input = input;
      this._keyboard.gameObject.SetActive(true);
    }

    public void OpenPrevWindow()
    {
      this.currentWindow.CloseWindow();
      this.currentWindow = this.previousWindow;
      this.currentWindow.OpenWindow();
    }

    public void OpenPage(TabletPage.Pages pageType)
    {
      ITabletPageBase tabletBaseByType = this.GetTabletBaseByType(pageType);
      if (tabletBaseByType == null)
        return;
      this.previousWindow = this.currentWindow;
      this.currentWindow = tabletBaseByType;
      this.previousWindow.CloseWindow();
      this.currentWindow.OpenWindow();
      this.labelPageName.text = this.currentWindow.GetPageType().ToString().Substring(3);
    }

    private ITabletPageBase GetTabletBaseByType(TabletPage.Pages type)
    {
      foreach (TabletPage childPage in this.childPages)
      {
        if (!((UnityEngine.Object) childPage == (UnityEngine.Object) null) && childPage.GetPageType() == type)
          return (ITabletPageBase) childPage;
      }
      return (ITabletPageBase) null;
    }
  }
}
