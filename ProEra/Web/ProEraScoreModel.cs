// Decompiled with JetBrains decompiler
// Type: ProEra.Web.ProEraScoreModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace ProEra.Web
{
  public class ProEraScoreModel
  {
    public Dictionary<ProEraScoreModel.DataSource, ProEraScoreModel.WeightedData> DataSources { get; } = new Dictionary<ProEraScoreModel.DataSource, ProEraScoreModel.WeightedData>()
    {
      {
        ProEraScoreModel.DataSource.Achievements,
        new ProEraScoreModel.WeightedData(0.0f, 1f)
      },
      {
        ProEraScoreModel.DataSource.TwoMinuteDrill,
        new ProEraScoreModel.WeightedData(0.0f, 1f)
      }
    };

    public float Value => this.DataSources.Keys.Sum<ProEraScoreModel.DataSource>((Func<ProEraScoreModel.DataSource, float>) (key => this.DataSources[key].Value));

    public enum DataSource
    {
      Achievements,
      TwoMinuteDrill,
    }

    public struct WeightedData
    {
      public float BaseValue { get; set; }

      public float Weight { get; set; }

      public float Value => this.BaseValue * this.Weight;

      public WeightedData(float baseValue)
      {
        this.BaseValue = baseValue;
        this.Weight = 1f;
      }

      public WeightedData(float baseValue, float weight)
      {
        this.BaseValue = baseValue;
        this.Weight = weight;
      }
    }
  }
}
