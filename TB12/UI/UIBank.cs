// Decompiled with JetBrains decompiler
// Type: TB12.UI.UIBank
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TB12.UI
{
  public class UIBank : MonoBehaviour
  {
    public static UIBank instance;
    [SerializeField]
    private UIObjectsBank _uiObjectsBank;
    private Dictionary<uint, AssetReference> screensAddress = new Dictionary<uint, AssetReference>();
    private Dictionary<uint, GameObject> cachedAsyncPrefabs = new Dictionary<uint, GameObject>();

    private void Awake()
    {
      if ((UnityEngine.Object) UIBank.instance != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      }
      else
      {
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
        UIBank.instance = this;
        this.ParseObjects();
      }
    }

    private void ParseObjects()
    {
      foreach (UIObjectsBank.UIFrontScreenObject allFrontScreen in this._uiObjectsBank.GetAllFrontScreens())
      {
        if (allFrontScreen.gameObjectAddress != null)
          this.screensAddress.Add(Convert.ToUInt32((object) allFrontScreen.type), allFrontScreen.gameObjectAddress);
      }
      foreach (UIObjectsBank.UILockerRoomScreenObject lockerRoomScreen in this._uiObjectsBank.GetAllLockerRoomScreens())
      {
        if (lockerRoomScreen.gameObjectAddress != null)
        {
          uint uint32 = Convert.ToUInt32((object) lockerRoomScreen.type);
          if (this.screensAddress.ContainsKey(uint32))
            Debug.LogError((object) ("UIBank - ParseObjects - KEY ALREADY EXISTS : " + uint32.ToString()));
          else
            this.screensAddress.Add(uint32, lockerRoomScreen.gameObjectAddress);
        }
      }
    }

    public static async Task<UIView> InstantiateUI(uint viewId, Transform targetParent)
    {
      GameObject gameObject = await UIBank.PrepareView(viewId, targetParent);
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return (UIView) null;
      UIView component = gameObject.GetComponent<UIView>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return (UIView) null;
      if (!UIBank.instance.cachedAsyncPrefabs.ContainsKey(viewId))
        UIBank.instance.cachedAsyncPrefabs.Add(viewId, gameObject);
      else
        Debug.LogError((object) ("UIBank - InstantiateUI - cachedAsyncPrefabs ALREADY EXISTS : " + viewId.ToString()));
      component.Initialize();
      component.OnViewDeactivated += new Action<UIView>(UIBank.instance.HandleViewDeactivated);
      return component;
    }

    private void HandleViewDeactivated(UIView view)
    {
      uint id = view.id;
      view.OnViewDeactivated -= new Action<UIView>(UIBank.instance.HandleViewDeactivated);
      AddressablesData.DestroyGameObject(UIBank.instance.cachedAsyncPrefabs[id]);
      UIBank.instance.cachedAsyncPrefabs.Remove(id);
    }

    private static async Task<GameObject> PrepareView(uint viewId, Transform parent) => await AddressablesData.instance.InstantiateAsync(UIBank.instance.screensAddress[viewId], Vector3.zero, Quaternion.Euler(Vector3.zero), parent);
  }
}
