// Decompiled with JetBrains decompiler
// Type: TouchUILocalizationDropdown
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Localization.Settings;

public class TouchUILocalizationDropdown : TouchUIButtonDropdown
{
  protected override void InitializeOptionButtonLinks()
  {
    base.InitializeOptionButtonLinks();
    this.SetButtonTextToNativeNames();
  }

  [ContextMenu("Set Text To Native Names")]
  public void SetButtonTextToNativeNames()
  {
    int index = -1;
    foreach (TouchUIButtonDropdown.ButtonActionPair option in this.m_options)
    {
      ++index;
      if ((Object) option.touchButton != (Object) null && option.touchEvent != null)
      {
        string localeNameByIndex = this.GetNativeLocaleNameByIndex(index);
        option.touchButton.SetLabelText(localeNameByIndex);
      }
    }
  }

  public string GetNativeLocaleNameByIndex(int index) => index >= 0 && index < LocalizationSettings.AvailableLocales.Locales.Count ? LocalizationSettings.AvailableLocales.Locales[index].Identifier.CultureInfo.NativeName : (string) null;

  public void SetLanguageByIndex(int index) => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
}
