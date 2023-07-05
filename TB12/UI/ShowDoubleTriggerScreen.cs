// Decompiled with JetBrains decompiler
// Type: TB12.UI.ShowDoubleTriggerScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using UnityEngine;

namespace TB12.UI
{
  public class ShowDoubleTriggerScreen : UIView
  {
    [SerializeField]
    private GameObject _agilityGameIntro;
    public GameObject[] oculusSprites;
    public GameObject[] psvrSprites;

    public override Enum ViewId { get; } = (Enum) EScreens.kHikeIntro;

    private void OnEnable()
    {
      if (!((UnityEngine.Object) this._agilityGameIntro != (UnityEngine.Object) null))
        return;
      this._agilityGameIntro.SetActive(AppState.GameMode == EGameMode.kAgility);
    }

    private void Start()
    {
    }

    private void SetSpritesActive(GameObject[] Sprites, bool Active)
    {
      foreach (GameObject sprite in Sprites)
        sprite.SetActive(Active);
    }
  }
}
