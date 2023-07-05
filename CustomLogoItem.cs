// Decompiled with JetBrains decompiler
// Type: CustomLogoItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;
using UnityEngine.UI;

public class CustomLogoItem : MonoBehaviour
{
  [SerializeField]
  private GameObject mainItem_GO;
  [SerializeField]
  private Image logo_Img;
  private int logoIndex;

  public void SetData(Sprite image, int index)
  {
    this.logo_Img.sprite = image;
    this.logoIndex = index;
    this.ShowItem();
  }

  public void ShowItem() => this.mainItem_GO.SetActive(true);

  public void HideItem() => this.mainItem_GO.SetActive(false);

  public void SelectLogo() => SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.logoEditor.SelectLogo(this.logoIndex);
}
