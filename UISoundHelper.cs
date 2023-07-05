// Decompiled with JetBrains decompiler
// Type: UISoundHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class UISoundHelper : MonoBehaviour
{
  public void PlayButtonToggle() => UISoundManager.instance.PlayButtonToggle();

  public void PlayButtonClick() => UISoundManager.instance.PlayButtonClick();

  public void PlayButtonBack() => UISoundManager.instance.PlayButtonBack();

  public void PlayTabSwipe() => UISoundManager.instance.PlayTabSwipe();
}
