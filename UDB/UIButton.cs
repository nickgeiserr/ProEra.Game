// Decompiled with JetBrains decompiler
// Type: UDB.UIButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UDB
{
  [Serializable]
  public struct UIButton
  {
    public Button button;
    public GameObject gameObject;
    public Text title;
    public Image image;

    public void AddListener(UnityAction unityAction) => this.button.onClick.AddListener(unityAction);

    public void RemoveListener(UnityAction unityAction) => this.button.onClick.RemoveListener(unityAction);

    public void RemoveAllListeners() => this.button.onClick.RemoveAllListeners();

    public void SetActive(bool active) => this.gameObject.SetActive(active);

    public void SetTitle(string title) => this.title.text = title;
  }
}
