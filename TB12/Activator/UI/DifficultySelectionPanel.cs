// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.DifficultySelectionPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.Activator.UI
{
  public class DifficultySelectionPanel : MonoBehaviour
  {
    [SerializeField]
    private TouchToggle _easy;
    [SerializeField]
    private TouchToggle _medium;
    [SerializeField]
    private TouchToggle _hard;
    [SerializeField]
    private TouchToggleGroup _difficultyGroup;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private float[] _ranges;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      this._easy.SetId((Enum) EDifficulty.Rookie);
      this._medium.SetId((Enum) EDifficulty.Pro);
      this._hard.SetId((Enum) EDifficulty.AllStar);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._difficultyGroup.Link<EDifficulty>(AppState.DifficultyLevel),
        AppState.DifficultyLevel.Link<EDifficulty>((Action<EDifficulty>) (level => this._slider.SetValueWithoutNotify(this._ranges[(int) level])))
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();
  }
}
