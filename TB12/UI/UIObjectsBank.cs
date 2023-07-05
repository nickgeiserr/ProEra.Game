// Decompiled with JetBrains decompiler
// Type: TB12.UI.UIObjectsBank
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TB12.UI
{
  [CreateAssetMenu(menuName = "TB12/Screen/UI Bank", fileName = "UIObjectsBank")]
  public class UIObjectsBank : ScriptableObject
  {
    [Header("Locker room screens")]
    [Space(10f)]
    [SerializeField]
    private UIObjectsBank.UILockerRoomScreenObject[] _objectsLockerRoom;
    [Header("Front screens")]
    [Space(10f)]
    [SerializeField]
    private UIObjectsBank.UIFrontScreenObject[] _objects;

    public UIObjectsBank.UIFrontScreenObject[] GetAllFrontScreens() => this._objects;

    public UIObjectsBank.UILockerRoomScreenObject[] GetAllLockerRoomScreens() => this._objectsLockerRoom;

    [Serializable]
    public struct UIFrontScreenObject
    {
      public EScreens type;
      public AssetReference gameObjectAddress;
    }

    [Serializable]
    public struct UILockerRoomScreenObject
    {
      public ELockerRoomUI type;
      public AssetReference gameObjectAddress;
    }
  }
}
