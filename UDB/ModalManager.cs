// Decompiled with JetBrains decompiler
// Type: UDB.ModalManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;

namespace UDB
{
  public class ModalManager : SingletonBehaviour<ModalManager, MonoBehaviour>
  {
    public TextModalController _textModalController;
    public IconModalController _iconModalController;
    public PictureModalController _pictureModalController;
    public GameObject modalPanelHolder;
    public Transform _modalTransform;

    private TextModalController textModalController
    {
      get
      {
        if ((Object) this._textModalController == (Object) null)
          this._textModalController = this.GetComponent<TextModalController>();
        return this._textModalController;
      }
    }

    private IconModalController iconModalController
    {
      get
      {
        if ((Object) this._iconModalController == (Object) null)
          this._iconModalController = this.GetComponent<IconModalController>();
        return this._iconModalController;
      }
    }

    private PictureModalController pictureModalController
    {
      get
      {
        if ((Object) this._pictureModalController == (Object) null)
          this._pictureModalController = this.GetComponent<PictureModalController>();
        return this._pictureModalController;
      }
    }

    public Transform modalTransform
    {
      get
      {
        if ((Object) this._modalTransform == (Object) null)
          this._modalTransform = this.GetComponent<Transform>();
        return this._modalTransform;
      }
    }

    private new void Awake()
    {
    }

    private void _CloseModal()
    {
      this.modalPanelHolder.SetActive(false);
      this.iconModalController.ClosePanel();
      this.textModalController.ClosePanel();
      this.pictureModalController.ClosePanel();
    }

    private void _ShowModal(
      string title,
      string message,
      UnityAction[] unityActions,
      string[] buttonText,
      Sprite icon = null,
      Sprite picture = null)
    {
      if ((Object) icon == (Object) null && (Object) picture == (Object) null)
      {
        this.iconModalController.ClosePanel();
        this.pictureModalController.ClosePanel();
        this.textModalController.ShowModal(title, message, unityActions, buttonText);
      }
      else if ((Object) icon == (Object) null)
      {
        this.textModalController.ClosePanel();
        this.iconModalController.ClosePanel();
        this.pictureModalController.ShowModal(title, message, unityActions, buttonText, picture);
      }
      else
      {
        this.textModalController.ClosePanel();
        this.pictureModalController.ClosePanel();
        this.iconModalController.ShowModal(title, message, unityActions, buttonText, icon);
      }
      this.modalPanelHolder.SetActive(true);
    }

    private void _ShowModal(TextModalPanelDetails textModalPanelDetails)
    {
      this.iconModalController.ClosePanel();
      this.pictureModalController.ClosePanel();
      this.textModalController.ShowModal(textModalPanelDetails);
      this.modalPanelHolder.SetActive(true);
    }

    private void _ShowModal(IconModalPanelDetails iconModalPanelDetails)
    {
      this.textModalController.ClosePanel();
      this.pictureModalController.ClosePanel();
      this.iconModalController.ShowModal(iconModalPanelDetails);
      this.modalPanelHolder.SetActive(true);
    }

    private void _ShowModal(PictureModalPanelDetails pictureModalPanelDetails)
    {
      this.textModalController.ClosePanel();
      this.iconModalController.ClosePanel();
      this.pictureModalController.ShowModal(pictureModalPanelDetails);
      this.modalPanelHolder.SetActive(true);
    }

    private void _ShowModal(TextModalDetails textModalDetails, UnityAction[] unityActions) => this._ShowModal(new TextModalPanelDetails(textModalDetails, unityActions));

    private void _ShowModal(IconModalDetails iconModalDetails, UnityAction[] unityActions) => this._ShowModal(new IconModalPanelDetails(iconModalDetails, unityActions));

    private void _ShowModal(PictureModalDetails pictureModalDetails, UnityAction[] unityActions) => this._ShowModal(new PictureModalPanelDetails(pictureModalDetails, unityActions));

    public static void CloseModal() => SingletonBehaviour<ModalManager, MonoBehaviour>.instance._CloseModal();

    public static void ShowModal(
      string title,
      string message,
      UnityAction[] unityActions,
      string[] buttonText,
      Sprite icon = null,
      Sprite picture = null)
    {
      SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(title, message, unityActions, buttonText, icon, picture);
    }

    public static void ShowModal(IconModalPanelDetails iconModalPanelDetails) => SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(iconModalPanelDetails);

    public static void ShowModal(TextModalPanelDetails textModalPanelDetails) => SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(textModalPanelDetails);

    public static void ShowModal(PictureModalPanelDetails pictureModalPanelDetails) => SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(pictureModalPanelDetails);

    public static void ShowModal(TextModalDetails textModalDetails, UnityAction[] unityActions) => SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(textModalDetails, unityActions);

    public static void ShowModal(IconModalDetails iconModalDetails, UnityAction[] unityActions) => SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(iconModalDetails, unityActions);

    public static void ShowModal(
      PictureModalDetails pictureModalDetails,
      UnityAction[] unityActions)
    {
      SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(pictureModalDetails, unityActions);
    }

    public static void OkModal(
      string title,
      string message,
      UnityAction ok,
      Sprite icon = null,
      Sprite picture = null)
    {
      UnityAction[] unityActions = new UnityAction[1]{ ok };
      string[] buttonText = new string[1]{ "Ok" };
      SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(title, message, unityActions, buttonText, icon, picture);
    }

    public static void YesNoModal(
      string title,
      string message,
      UnityAction yes,
      UnityAction no,
      Sprite icon = null,
      Sprite picture = null)
    {
      UnityAction[] unityActions = new UnityAction[2]
      {
        yes,
        no
      };
      string[] buttonText = new string[2]{ "Yes", "No" };
      SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(title, message, unityActions, buttonText, icon, picture);
    }

    public static void YesNoCancelModal(
      string title,
      string message,
      UnityAction yes,
      UnityAction no,
      UnityAction cancel,
      Sprite icon = null,
      Sprite picture = null)
    {
      UnityAction[] unityActions = new UnityAction[3]
      {
        yes,
        no,
        cancel
      };
      string[] buttonText = new string[3]
      {
        "Yes",
        "No",
        "Cancel"
      };
      SingletonBehaviour<ModalManager, MonoBehaviour>.instance._ShowModal(title, message, unityActions, buttonText, icon, picture);
    }
  }
}
