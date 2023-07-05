// Decompiled with JetBrains decompiler
// Type: UniformEditorControllerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using UnityEngine;
using UnityEngine.UI;

public class UniformEditorControllerManager : MonoBehaviour
{
  public static UniformEditorControllerManager self;
  public bool usingController;
  [SerializeField]
  private UnityEngine.UI.Button defaultSelectLeft;
  [SerializeField]
  private UnityEngine.UI.Button defaultSelectRight;
  [SerializeField]
  private UnityEngine.UI.Button exitButton;
  public Slider firstColorSelectorSlider;
  public GameObject[] typingFields;
  public Dropdown teamSelect;
  public UniformEditor uniformEditorScript;
  private float h;
  private float v;
  private float m;
  [Header("Player Model Camera Movement")]
  public float moveSpeed = 1f;
  public float zoomSpeed = 5f;
  public float rotateHorizontalSpeed = 500f;
  public float rotateVerticalSpeed = 200f;

  private void Awake() => UniformEditorControllerManager.self = this;

  private void Update()
  {
    this.CheckForConnectedController();
    if (!this.usingController)
    {
      this.ManageKeyboardInput();
    }
    else
    {
      this.ManageBackButtonPress();
      this.MangePlayerModelJoystick();
      this.ManageApplyButtonPress();
      this.ManageLeftBumperPress();
      this.ManageRightBumperPress();
      this.ManageTriggerPress();
    }
  }

  public void OnEnable() => this.SelectDefaultLeft();

  public void SelectDefaultLeft() => this.defaultSelectLeft.Select();

  public void SelectDefaultRight() => this.defaultSelectRight.Select();

  public void SelectExitButton() => this.exitButton.Select();

  private void CheckForConnectedController()
  {
  }

  private void SetupScreenForController()
  {
    this.SetupDropdownsForController();
    this.DisableTypingFields();
  }

  public void EnableColorOverlay(Image overlay) => overlay.gameObject.SetActive(true);

  public void DisableColorOverlay(Image overlay) => overlay.gameObject.SetActive(false);

  private void DisableTypingFields()
  {
    for (int index = 0; index < this.typingFields.Length; ++index)
      this.typingFields[index].SetActive(false);
  }

  private void SetupDropdownsForController() => this.teamSelect.colors = this.teamSelect.colors with
  {
    highlightedColor = AxisFootballColors.brightBlue
  };

  private void ManageKeyboardInput()
  {
    if (this.uniformEditorScript.saveUniformWindow.gameObject.activeInHierarchy || this.uniformEditorScript.inputName.isFocused || this.uniformEditorScript.inputNumber.isFocused || this.uniformEditorScript.uniformNameInput.isFocused)
      return;
    this.h = Input.GetAxis("Horizontal");
    this.v = Input.GetAxis("Vertical");
    if ((double) this.h < -0.10000000149011612)
      this.RotatePlayerClockwise();
    else if ((double) this.h > 0.10000000149011612)
      this.RotatePlayerCounterClockwise();
    if ((double) this.v < -0.10000000149011612)
      this.RotatePlayerDown();
    else if ((double) this.v > 0.10000000149011612)
      this.RotatePlayerUp();
    float axis = Input.GetAxis("Mouse ScrollWheel");
    if ((double) axis > 0.0 && (double) this.uniformEditorScript.cameraTrans.localPosition.z > 0.5)
      this.ZoomIn();
    else if ((double) axis < 0.0 && (double) this.uniformEditorScript.cameraTrans.localPosition.z < 4.0)
      this.ZoomOut();
    if (Input.GetKeyDown(KeyCode.Space))
      this.uniformEditorScript.ApplyJersey();
    if (Input.GetKeyDown(KeyCode.H))
      this.uniformEditorScript.playerHidden = !this.uniformEditorScript.playerHidden;
    if (Input.GetKey(KeyCode.E))
      this.MovePlayerUp();
    else if (Input.GetKey(KeyCode.Q))
      this.MovePlayerDown();
    if (!Input.GetKeyDown(KeyCode.R))
      return;
    this.uniformEditorScript.RefreshTextures(this.uniformEditorScript.currentUniformIndex);
  }

  private void ManageTriggerPress()
  {
    if (UserManager.instance.Action4IsPressed(Player.One))
    {
      if (UserManager.instance.RightTriggerIsPressed(Player.One))
      {
        this.MovePlayerUp();
      }
      else
      {
        if (!UserManager.instance.LeftTriggerIsPressed(Player.One))
          return;
        this.MovePlayerDown();
      }
    }
    else if (UserManager.instance.RightTriggerIsPressed(Player.One))
    {
      this.ZoomIn();
    }
    else
    {
      if (!UserManager.instance.LeftTriggerIsPressed(Player.One))
        return;
      this.ZoomOut();
    }
  }

  private void ManageBackButtonPress()
  {
    if (!UserManager.instance.Action2WasPressed(Player.One))
      return;
    if (this.uniformEditorScript.confirmExitWindow.gameObject.activeInHierarchy)
      this.uniformEditorScript.HideConfirmExitWindow();
    else if (this.uniformEditorScript.confirmOverrideWindow.gameObject.activeInHierarchy)
      this.uniformEditorScript.HideOverrideConfirmation();
    else if (this.uniformEditorScript.keyboard.mainWindow.activeInHierarchy)
      this.uniformEditorScript.keyboard.Cancel();
    else if (this.uniformEditorScript.saveUniformWindow.gameObject.activeInHierarchy)
      this.uniformEditorScript.HideUniformSaveWindow();
    else if (this.uniformEditorScript.deleteUniformWindow.gameObject.activeInHierarchy)
      this.uniformEditorScript.HideUniformDeleteWindow();
    else if (this.uniformEditorScript.IsColorSelectorActive())
    {
      this.uniformEditorScript.CloseColorSelector();
      this.defaultSelectLeft.Select();
    }
    else
      this.uniformEditorScript.ShowConfirmExitWindow();
  }

  private void ManageApplyButtonPress()
  {
    if (!UserManager.instance.Action3WasPressed(Player.One))
      return;
    this.uniformEditorScript.ApplyJersey();
  }

  private void ManageLeftBumperPress()
  {
    if (this.uniformEditorScript.IsColorSelectorActive() || !UserManager.instance.LeftBumperWasPressed(Player.One))
      return;
    this.SelectDefaultLeft();
  }

  private void ManageRightBumperPress()
  {
    if (this.uniformEditorScript.IsColorSelectorActive() || !UserManager.instance.RightBumperWasPressed(Player.One))
      return;
    this.SelectDefaultRight();
  }

  private void MangePlayerModelJoystick()
  {
    this.h = UserManager.instance.RightStickX(Player.One);
    this.v = UserManager.instance.RightStickY(Player.One);
    if ((double) this.h < -0.5)
      this.RotatePlayerClockwise();
    else if ((double) this.h > 0.5)
      this.RotatePlayerCounterClockwise();
    if ((double) this.v < -0.5)
    {
      this.RotatePlayerDown();
    }
    else
    {
      if ((double) this.v <= 0.5)
        return;
      this.RotatePlayerUp();
    }
  }

  private void MovePlayerUp() => this.uniformEditorScript.playerTrans.position += Vector3.up * this.moveSpeed * Time.deltaTime;

  private void MovePlayerDown() => this.uniformEditorScript.playerTrans.position -= Vector3.up * this.moveSpeed * Time.deltaTime;

  private void RotatePlayerClockwise() => this.uniformEditorScript.playerTrans.eulerAngles = new Vector3(0.0f, this.uniformEditorScript.playerTrans.eulerAngles.y + this.rotateHorizontalSpeed * Time.deltaTime, 0.0f);

  private void RotatePlayerCounterClockwise() => this.uniformEditorScript.playerTrans.eulerAngles = new Vector3(0.0f, this.uniformEditorScript.playerTrans.eulerAngles.y - this.rotateHorizontalSpeed * Time.deltaTime, 0.0f);

  private void RotatePlayerDown() => this.uniformEditorScript.cameraHolderTrans.eulerAngles = new Vector3(this.uniformEditorScript.cameraHolderTrans.eulerAngles.x - this.rotateVerticalSpeed * Time.deltaTime, 0.0f, 0.0f);

  private void RotatePlayerUp() => this.uniformEditorScript.cameraHolderTrans.eulerAngles = new Vector3(this.uniformEditorScript.cameraHolderTrans.eulerAngles.x + this.rotateVerticalSpeed * Time.deltaTime, 0.0f, 0.0f);

  private void ZoomIn() => this.uniformEditorScript.cameraTrans.localPosition = new Vector3(0.0f, this.uniformEditorScript.cameraTrans.localPosition.y, this.uniformEditorScript.cameraTrans.localPosition.z - this.zoomSpeed * Time.deltaTime);

  private void ZoomOut() => this.uniformEditorScript.cameraTrans.localPosition = new Vector3(0.0f, this.uniformEditorScript.cameraTrans.localPosition.y, this.uniformEditorScript.cameraTrans.localPosition.z + this.zoomSpeed * Time.deltaTime);
}
