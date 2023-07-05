// Decompiled with JetBrains decompiler
// Type: UDB.TransitionRender
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (Camera))]
  public class TransitionRender : MonoBehaviour
  {
    private List<Transition> renderTransitionList;

    private void Awake() => this.renderTransitionList = new List<Transition>();

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
      for (int index = 0; index < this.renderTransitionList.Count; ++index)
      {
        if ((bool) (Object) this.renderTransitionList[index])
          this.renderTransitionList[index].OnRenderImage((Texture) source, destination);
      }
    }

    public void AddRender(Transition transition)
    {
      if (DebugManager.StateForKey("Transition Render Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if (!this.renderTransitionList.Contains(transition))
        this.renderTransitionList.Add(transition);
      this.enabled = true;
    }

    public void RemoveRender(Transition transition)
    {
      if (DebugManager.StateForKey("Transition Render Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      this.renderTransitionList.Remove(transition);
      if (this.renderTransitionList.Count != 0)
        return;
      this.enabled = false;
    }
  }
}
