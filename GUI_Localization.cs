// Decompiled with JetBrains decompiler
// Type: GUI_Localization
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GUI_Localization : MonoBehaviour
{
  private static Dictionary<string, string> nameValuePairs;
  private string language;

  private void Awake()
  {
    GUI_Localization.nameValuePairs = new Dictionary<string, string>();
    this.LoadLanguage(this.language);
  }

  private void LoadLanguage(string lang)
  {
    string[] strArray1 = new string[0];
    string[] strArray2;
    try
    {
      strArray2 = File.ReadAllText(ModManager.appPath + "/" + ModManager.modPath + "/" + lang + ".txt").Split("\n"[0], StringSplitOptions.None);
    }
    catch
    {
      Debug.Log((object) "No Language Found, returning.");
      return;
    }
    for (int index = 0; index < strArray2.Length; ++index)
    {
      if (strArray2[index].Length <= 0 || strArray2[index][0] != '#')
      {
        string[] strArray3 = strArray2[index].Split('=', StringSplitOptions.None);
        if (strArray3.Length == 2)
        {
          string key = Utils.TrimString(strArray3[0]);
          string str = Utils.TrimString(strArray3[1]);
          if (key.Length > 0 && str.Length > 0)
            GUI_Localization.nameValuePairs.Add(key, str);
        }
      }
    }
  }

  public static string GetLocalizedText(string text) => GUI_Localization.nameValuePairs.ContainsKey(text) ? GUI_Localization.nameValuePairs[text] : text;
}
