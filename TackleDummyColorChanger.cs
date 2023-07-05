// Decompiled with JetBrains decompiler
// Type: TackleDummyColorChanger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game.Sources.TeamData;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TackleDummyColorChanger
{
  private static readonly int MainColor = Shader.PropertyToID("_MainColor");
  private static readonly int HelmetColor = Shader.PropertyToID("_HelmetColor");
  private static readonly int NumberColor = Shader.PropertyToID("_NumberColor");

  private static Save_MiniCamp MiniCampData => PersistentSingleton<SaveManager>.Instance.miniCamp;

  public static void SetDummyColor(List<Renderer> renderers)
  {
    TeamDataMap dataMap = PersistentSingleton<TeamResourcesManager>.Instance.TeamData[TackleDummyColorChanger.MiniCampData.SelectedEntry.GetTeam().TeamDataIndex].DataMap;
    string[] strArray1 = dataMap.GetValue("PrimaryColor").Split(',', StringSplitOptions.None);
    string[] strArray2 = dataMap.GetValue("SecondaryColor").Split(',', StringSplitOptions.None);
    string[] strArray3 = dataMap.GetValue("AlternateColor").Split(',', StringSplitOptions.None);
    Color color1 = new Color(float.Parse(strArray1[0]) / (float) byte.MaxValue, float.Parse(strArray1[1]) / (float) byte.MaxValue, float.Parse(strArray1[2]) / (float) byte.MaxValue, (float) byte.MaxValue);
    Color color2 = new Color(float.Parse(strArray2[0]) / (float) byte.MaxValue, float.Parse(strArray2[1]) / (float) byte.MaxValue, float.Parse(strArray2[2]) / (float) byte.MaxValue, (float) byte.MaxValue);
    Color color3 = new Color(float.Parse(strArray3[0]) / (float) byte.MaxValue, float.Parse(strArray3[1]) / (float) byte.MaxValue, float.Parse(strArray3[2]) / (float) byte.MaxValue, (float) byte.MaxValue);
    foreach (Renderer renderer in renderers)
    {
      renderer.material.SetColor(TackleDummyColorChanger.MainColor, color1);
      renderer.material.SetColor(TackleDummyColorChanger.NumberColor, color2);
      renderer.material.SetColor(TackleDummyColorChanger.HelmetColor, color3);
    }
  }
}
