// Decompiled with JetBrains decompiler
// Type: HighlightShimmerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HighlightShimmerManager : MonoBehaviour
{
  [SerializeField]
  private static int _interactionHistory;
  [SerializeField]
  private float pulseRate = 2f;
  [SerializeField]
  private List<MeshRenderer> _ShimmerObjectRenderers = new List<MeshRenderer>();
  private int highlightId = Shader.PropertyToID("_Highlight");
  private bool bFinallyStoppedTwinkling;
  private Save_GameSettings _gameSettings;

  private void Start() => this._gameSettings = PersistentSingleton<SaveManager>.Instance.gameSettings;

  private void Update()
  {
    if (this.bFinallyStoppedTwinkling)
      return;
    HighlightShimmerManager._interactionHistory = this._gameSettings._lockerInteractionHistory;
    this.UpdateAllShimmerHighlights(Mathf.PingPong(Time.time * this.pulseRate, 1f));
  }

  private void UpdateAllShimmerHighlights(float a_highlightValue)
  {
    int count = this._ShimmerObjectRenderers.Count;
    int interactionHistory = HighlightShimmerManager._interactionHistory;
    for (int index = 0; index < count; ++index)
    {
      int num = 1 << index;
      Material[] materials = this._ShimmerObjectRenderers[index]?.materials;
      if (materials != null)
      {
        if ((interactionHistory & num) != num)
        {
          this.UpdateHighlightPulse(materials, a_highlightValue);
        }
        else
        {
          this.bFinallyStoppedTwinkling = true;
          this.UpdateHighlightPulse(materials, 0.0f);
        }
        this._ShimmerObjectRenderers[index].materials = materials;
      }
    }
  }

  private void UpdateHighlightPulse(Material[] materials, float value)
  {
    foreach (Material material in materials)
      material.SetFloat(this.highlightId, value);
  }

  public static void AddToInteractionHistory(HighlightShimmerManager.ELockerRoomObjects index)
  {
    HighlightShimmerManager._interactionHistory = (int) ((HighlightShimmerManager.ELockerRoomObjects) HighlightShimmerManager._interactionHistory | index);
    PersistentSingleton<SaveManager>.Instance.gameSettings._lockerInteractionHistory = HighlightShimmerManager._interactionHistory;
    AppEvents.SaveGameSettings.Trigger();
  }

  public static void AddToInteractionHistory(int index)
  {
    if (!Mathf.IsPowerOfTwo(index))
      return;
    HighlightShimmerManager.AddToInteractionHistory((HighlightShimmerManager.ELockerRoomObjects) index);
  }

  [Flags]
  public enum ELockerRoomObjects
  {
    None = 0,
    Helmet = 1,
    SeasonTablet = 2,
    EditPlayer = 4,
    Settings = 8,
    Practice = 16, // 0x00000010
    Schedule = 32, // 0x00000020
    Trophies = 64, // 0x00000040
    Kiosk = 128, // 0x00000080
    CoachNotes = 256, // 0x00000100
  }
}
