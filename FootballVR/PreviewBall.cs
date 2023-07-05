// Decompiled with JetBrains decompiler
// Type: FootballVR.PreviewBall
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class PreviewBall : MonoBehaviour
  {
    [SerializeField]
    private LineRenderer _trail;
    [SerializeField]
    private PlayerProfile _playerProfile;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake() => this._linksHandler.SetLinks(new List<EventHandle>()
    {
      this._playerProfile.Customization.TrailColor.Link<Color>(new Action<Color>(this.SetTrailColor))
    });

    private void OnDestroy() => this._linksHandler.Clear();

    private void SetTrailColor(Color color)
    {
      this._trail.startColor = color;
      this._trail.endColor = color;
    }
  }
}
