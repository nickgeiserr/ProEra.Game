// Decompiled with JetBrains decompiler
// Type: FootballWorld.AtlasPattern
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FootballWorld
{
  [Serializable]
  public class AtlasPattern : IDisposable
  {
    [SerializeField]
    private AtlasType atlasType;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private TextMeshProUGUI templateTextLabel;
    [SerializeField]
    private Vector2 sheetSize;
    [SerializeField]
    private List<TextMeshProUGUI> _preparedTextLabels = new List<TextMeshProUGUI>();

    ~AtlasPattern()
    {
    }

    public AtlasType GetAtlasType() => this.atlasType;

    public Camera GetCamera() => this.camera;

    public TextMeshProUGUI GetTemplateLabel() => this.templateTextLabel;

    public Vector2 GetSheetSize() => this.sheetSize;

    public List<TextMeshProUGUI> GetPreparedLabels() => this._preparedTextLabels;

    public void Reset() => this._preparedTextLabels = new List<TextMeshProUGUI>();

    public void Dispose() => GC.SuppressFinalize((object) this);
  }
}
