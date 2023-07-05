// Decompiled with JetBrains decompiler
// Type: UDB.CircularMenuButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UDB
{
  public class CircularMenuButton : 
    CachedMonoBehaviour,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler,
    IPoolableObject
  {
    public Image circle;
    public Image icon;
    public CircularMenu menu;
    public float animSpeed = 8f;
    public float diameter = 1f;
    private Color colorBase;
    private float theta;
    private float xPos;
    private float yPos;
    private float animationTimer;

    public void OnPoolableSpawned()
    {
    }

    public void OnPoolableActivate()
    {
    }

    public void OnPoolableDeactivate()
    {
    }

    public void OnPoolableRecycle()
    {
    }

    public void ApplyCircularMenuOption(
      CircularMenuOption circularMenuOption,
      int length,
      int index)
    {
      this.theta = this.diameter * (6.28318548f / (float) length) * (float) index;
      this.xPos = Mathf.Sin(this.theta);
      this.yPos = Mathf.Cos(this.theta);
      this.transform.localPosition = new Vector3(this.xPos, this.yPos, 0.0f) * 100f * this.diameter;
      this.circle.color = circularMenuOption.color;
      this.icon.sprite = circularMenuOption.sprite;
      this.ApplyCustomCircularMenuOption(circularMenuOption, length, index);
    }

    public void Claim(CircularMenu circularMenu)
    {
      this.transform.SetParent(circularMenu.transform, false);
      this.menu = circularMenu;
    }

    public void AnimationON() => this.StartCoroutine(this.AnimateButton());

    private void DeselectTimerFinished()
    {
      this.menu.DeselectButton();
      this.circle.color = this.colorBase;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      this.menu.SelectButton(this);
      this.colorBase = this.circle.color;
      this.circle.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData) => Timer.Register(0.1f, new System.Action(this.DeselectTimerFinished));

    private IEnumerator AnimateButton()
    {
      CircularMenuButton circularMenuButton = this;
      circularMenuButton.transform.localScale = Vector3.zero;
      circularMenuButton.animationTimer = 0.0f;
      while ((double) circularMenuButton.animationTimer < 1.0 / (double) circularMenuButton.animSpeed)
      {
        circularMenuButton.animationTimer += Time.deltaTime;
        circularMenuButton.transform.localScale = Vector3.one * circularMenuButton.animationTimer * circularMenuButton.animSpeed * circularMenuButton.diameter;
        yield return (object) null;
      }
      circularMenuButton.transform.localScale = Vector3.one * circularMenuButton.diameter;
    }

    protected virtual void ApplyCustomCircularMenuOption(
      CircularMenuOption circularMenuOption,
      int length,
      int inde)
    {
    }
  }
}
