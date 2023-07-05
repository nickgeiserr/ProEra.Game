// Decompiled with JetBrains decompiler
// Type: UDB.ToolboxBelt
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (AnimatedLayoutGroup))]
  public class ToolboxBelt : ToolboxItem
  {
    public bool isExpanded = true;
    public float animationDuration = 0.4f;
    public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1f, 1f);
    private int animationDirection;
    private float animationProgress = 1f;
    public ToolboxItem[] _itemsCahche;
    private AnimatedLayoutGroup _layout;
    private float progress;

    public ToolboxItem[] itemsCahche
    {
      get
      {
        if (this._itemsCahche == null)
          this._itemsCahche = this.GetComponentsInChildrenFirstLevel<ToolboxItem>();
        return this._itemsCahche;
      }
      private set => this._itemsCahche = value;
    }

    private AnimatedLayoutGroup layout
    {
      get
      {
        if ((Object) this._layout == (Object) null)
        {
          this._layout = (AnimatedLayoutGroup) this.GetComponent<AnimatedHorizontalLayoutGroup>();
          if ((Object) this._layout == (Object) null)
            this._layout = (AnimatedLayoutGroup) this.GetComponent<AnimatedVerticalLayoutGroup>();
        }
        return this._layout;
      }
    }

    private void Update()
    {
      if (this.animationDirection == 0)
        return;
      this.animationProgress += Time.deltaTime / this.animationDuration * (float) this.animationDirection;
      this.progress = Mathf.Clamp01(this.animationProgress);
      this.progress = this.animationCurve.Evaluate(this.progress);
      this.Animate(this.progress, false);
      if (((double) this.animationProgress <= 1.0 || this.animationDirection != 1) && ((double) this.animationProgress >= 0.0 || this.animationDirection != -1))
        return;
      this.animationDirection = 0;
      Task.NewTask(this.RebuildLayoutInNextFrame());
    }

    protected override void Start() => base.Start();

    public override bool Animate(float Progress, bool forceHide = false)
    {
      for (int index = 0; index < this.itemsCahche.Length; ++index)
      {
        if (((object) this.itemsCahche[index]).GetType() == typeof (ToolboxBelt))
        {
          ToolboxBelt toolboxBelt = (ToolboxBelt) this.itemsCahche[index];
          if (toolboxBelt.isExpanded != this.isExpanded)
            toolboxBelt.Toggle();
        }
        else
          this.itemsCahche[index].Animate(Progress, true);
      }
      return this.layout.childCount > 0;
    }

    public void Toggle(bool recurrsive = false)
    {
      if (this.isExpanded)
        this.Shrink(recurrsive);
      else
        this.Expand(recurrsive);
    }

    public void Expand(bool recurrsive = false)
    {
      if (this.isExpanded)
        return;
      this.itemsCahche = this.GetComponentsInChildrenFirstLevel<ToolboxItem>();
      this.isExpanded = true;
      this.animationDirection = 1;
      if (!recurrsive)
        return;
      for (int index = 0; index < this.itemsCahche.Length; ++index)
      {
        if (((object) this.itemsCahche[index]).GetType() == typeof (ToolboxBelt))
          ((ToolboxBelt) this.itemsCahche[index]).Expand(recurrsive);
      }
    }

    public void Shrink(bool recurrsive = false)
    {
      if (!this.isExpanded)
        return;
      this.itemsCahche = this.GetComponentsInChildrenFirstLevel<ToolboxItem>();
      this.isExpanded = false;
      this.animationDirection = -1;
      if (!recurrsive)
        return;
      for (int index = 0; index < this.itemsCahche.Length; ++index)
      {
        if (((object) this.itemsCahche[index]).GetType() == typeof (ToolboxBelt))
          ((ToolboxBelt) this.itemsCahche[index]).Shrink(recurrsive);
      }
    }

    public void OnItemClick(ToolboxButton item)
    {
      if (item.isToggle)
      {
        item.isToggled = !item.isToggled;
      }
      else
      {
        this.itemsCahche = (ToolboxItem[]) this.GetComponentsInChildrenFirstLevel<ToolboxButton>();
        for (int index = 0; index < this.itemsCahche.Length; ++index)
        {
          if (((object) this.itemsCahche[index]).GetType() == typeof (ToolboxButton))
            ((ToolboxButton) this.itemsCahche[index]).isSelected = false;
        }
        item.isSelected = true;
      }
    }

    public T[] GetComponentsInChildrenFirstLevel<T>(bool includeInactive = true)
    {
      List<T> objList = new List<T>();
      T[] componentsInChildren = this.GetComponentsInChildren<T>(includeInactive);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((Object) ((Component) (object) componentsInChildren[index]).transform.parent == (Object) this.transform)
          objList.Add(componentsInChildren[index]);
      }
      return objList.ToArray();
    }
  }
}
