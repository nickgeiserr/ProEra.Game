// Decompiled with JetBrains decompiler
// Type: TB12.IButtonEventInvoker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace TB12
{
  public class IButtonEventInvoker : MonoBehaviour
  {
    [SerializeField]
    private IButton SourceButton;
    [SerializeField]
    private UnityEvent OnButtonPressed = new UnityEvent();

    private void Awake()
    {
      if (this.SourceButton == null)
      {
        IButton component;
        if (this.TryGetComponent<IButton>(out component))
        {
          this.SourceButton = component;
        }
        else
        {
          Debug.LogWarning((object) ("Cannot find an IButton for gameobject " + this.name + ". Please attach one before moving on."));
          return;
        }
      }
      this.SourceButton.Link(new Action(this.FireEvent));
    }

    private void FireEvent() => this.OnButtonPressed?.Invoke();
  }
}
