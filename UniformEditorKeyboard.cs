// Decompiled with JetBrains decompiler
// Type: UniformEditorKeyboard
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class UniformEditorKeyboard : MonoBehaviour
{
  public GameObject mainWindow;
  [SerializeField]
  private int maxCharacters = 25;
  [SerializeField]
  private Selectable selectOnOpen;
  [SerializeField]
  private UniformEditor uniformEditor;
  public Text inputText;

  private void Start() => this.inputText.text = "";

  public void ShowWindow()
  {
    this.uniformEditor.savedUniformSelect.Hide();
    this.mainWindow.SetActive(true);
    this.inputText.text = "";
    this.selectOnOpen.Select();
  }

  public void HideWindow() => this.mainWindow.SetActive(false);

  public void AddCharacter(string c)
  {
    if (this.inputText.text.Length >= this.maxCharacters)
      return;
    this.inputText.text += c;
  }

  public void SendBackspace()
  {
    if (this.inputText.text.Length <= 0)
      return;
    this.inputText.text = this.inputText.text.Remove(this.inputText.text.Length - 1);
  }

  public void SendSpace() => this.AddCharacter(" ");

  public void Cancel()
  {
    this.HideWindow();
    this.uniformEditor.ShowUniformSaveWindow();
  }

  public void Approve()
  {
    this.uniformEditor.ShowUniformSaveWindow();
    this.uniformEditor.uniformNameInput.text = this.inputText.text;
    this.HideWindow();
  }
}
