// Decompiled with JetBrains decompiler
// Type: FieldGrassLayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections.Generic;
using TB12;
using UnityEngine;

public class FieldGrassLayer : MonoBehaviour
{
  [SerializeField]
  private Material SuperbowlMaterial;
  private readonly LinksHandler linksHandler = new LinksHandler();

  private void Start() => this.linksHandler.SetLinks(new List<EventHandle>()
  {
    WorldState.CrowdEnabled.Link<bool>((Action<bool>) (unused => this.CheckSuperbowlCase()))
  });

  private void OnDestroy() => this.linksHandler.Dispose();

  private void CheckSuperbowlCase()
  {
    if (AppState.SeasonMode.Value == ESeasonMode.kUnknown || SeasonModeManager.self.GetCurrentGameRound() != SeasonModeGameRound.SuperBowl)
      return;
    MeshRenderer component = this.gameObject.GetComponent<MeshRenderer>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    component.sharedMaterial = this.SuperbowlMaterial;
    FieldController.CheckForSuperbowl((Renderer) component);
  }
}
