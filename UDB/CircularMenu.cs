// Decompiled with JetBrains decompiler
// Type: UDB.CircularMenu
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class CircularMenu : CachedMonoBehaviour
  {
    public bool isActive;
    public bool hasButtonSelected;
    protected List<GameObject> buttons = new List<GameObject>();
    private GameObject newButton;
    private CircularMenuButton newCircularMenuButton;

    public CircularMenuOption[] options => this.GetButtonOptions();

    private void Update()
    {
      if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        return;
      if (this.hasButtonSelected)
        this.HandleSelectedButton();
      else
        this.HideButtons();
    }

    internal void HandleSelectedButton() => this.HandleCustomSelectedButton();

    internal void HideButtons()
    {
      if (!this.isActive)
        return;
      if (this.buttons != null)
      {
        int count = this.buttons.Count;
        for (int index = 0; index < count; ++index)
        {
          ScenePoolManager.Recycle(this.buttons[0]);
          this.buttons.RemoveAt(0);
        }
      }
      this.isActive = false;
    }

    private IEnumerator AnimateButtons()
    {
      CircularMenu circularMenu = this;
      for (int i = 0; i < circularMenu.options.Length; ++i)
      {
        circularMenu.newButton = circularMenu.SpawnNewCircularMenuButton();
        circularMenu.newCircularMenuButton = circularMenu.newButton.GetComponent<CircularMenuButton>();
        circularMenu.buttons.Add(circularMenu.newButton);
        circularMenu.newCircularMenuButton.Claim(circularMenu);
        circularMenu.newCircularMenuButton.ApplyCircularMenuOption(circularMenu.options[i], circularMenu.options.Length, i);
        circularMenu.newButton.SetActive(true);
        circularMenu.newCircularMenuButton.AnimationON();
        yield return (object) new WaitForSeconds(0.06f);
      }
    }

    public void ShowButtons(Vector3 position)
    {
      this.transform.position = position;
      this.ShowButtons();
    }

    public void ShowButtons()
    {
      this.isActive = true;
      this.StartCoroutine(this.AnimateButtons());
    }

    protected virtual CircularMenuOption[] GetButtonOptions() => new CircularMenuOption[0];

    protected virtual GameObject SpawnNewCircularMenuButton() => (GameObject) null;

    protected virtual void HandleCustomSelectedButton()
    {
    }

    public virtual void SelectButton(CircularMenuButton circularMenuButton) => this.hasButtonSelected = true;

    public virtual void DeselectButton() => this.hasButtonSelected = false;
  }
}
