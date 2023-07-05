// Decompiled with JetBrains decompiler
// Type: UDB.ToolboxItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UDB
{
  public class ToolboxItem : CachedMonoBehaviour
  {
    public bool alwaysVisibleInParent;
    public bool alwaysVisible;
    protected ToolboxBelt parentToolbox;
    [NonSerialized]
    public Vector2 startSize;

    protected virtual void Start()
    {
      this.startSize = this.rectTransform.sizeDelta;
      this.parentToolbox = this.transform.parent.GetComponent<ToolboxBelt>();
    }

    public virtual bool Animate(float Progress, bool forceHide = false)
    {
      this.rectTransform.sizeDelta = this.startSize * Progress;
      if (!this.alwaysVisibleInParent | forceHide && !this.alwaysVisible)
      {
        this.rectTransform.sizeDelta = this.startSize * Progress;
        if ((double) Progress == 0.0 && this.gameObject.activeSelf)
          this.gameObject.SetActive(false);
        else if ((double) Progress > 0.0 && !this.gameObject.activeSelf)
          this.gameObject.SetActive(true);
      }
      else
        this.rectTransform.sizeDelta = this.startSize * Mathf.Max(1f, Progress);
      return this.gameObject.activeSelf;
    }

    public IEnumerator RebuildLayoutInNextFrame()
    {
      ToolboxItem toolboxItem = this;
      yield return (object) new WaitForEndOfFrame();
      ToolboxBelt toolbox = (ToolboxBelt) null;
      if ((UnityEngine.Object) toolboxItem.parentToolbox != (UnityEngine.Object) null)
        toolbox = toolboxItem.parentToolbox;
      else if (((object) toolboxItem).GetType() == typeof (ToolboxBelt))
        toolbox = (ToolboxBelt) toolboxItem;
      else
        yield return (object) null;
      while ((UnityEngine.Object) toolbox.parentToolbox != (UnityEngine.Object) null)
        toolbox = toolbox.parentToolbox;
      LayoutRebuilder.ForceRebuildLayoutImmediate(toolbox.rectTransform);
    }
  }
}
