// Decompiled with JetBrains decompiler
// Type: UDB.TransitionPlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class TransitionPlayer : SerializedCachedMonoBehaviour
  {
    public Transition outTransition;
    public Transition inTransition;
    public bool playingInTransition;
    public bool playingOutTransition;

    protected void AddAsInTransition(Transition transition)
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.inTransition != (Object) null && this.inTransition.isPlaying)
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant add as in transition becuase inTransition is currently playing."));
      }
      else
        this.inTransition = transition;
    }

    protected void AddAsOutTransition(Transition transition)
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.outTransition != (Object) null && this.outTransition.isPlaying)
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant add as out transition becuase outTransition is currently playing."));
      }
      else
        this.outTransition = transition;
    }

    protected void RemoveInTransition(Transition transition)
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if (((object) transition).Equals((object) this.inTransition) && this.inTransition.isPlaying)
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant remove as in transition becuase inTransition is currently playing."));
      }
      else
        this.inTransition = transition;
    }

    protected void RemoveOutTransition(Transition transition)
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if (((object) transition).Equals((object) this.outTransition) && this.outTransition.isPlaying)
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant add as out transition becuase outTransition is currently playing."));
      }
      else
        this.outTransition = transition;
    }

    protected void WaitInTransition()
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.inTransition != (Object) null && !this.inTransition.isPlaying)
      {
        this.inTransition.Wait();
      }
      else
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant wait inTransition becuase it is null or playing."));
      }
    }

    protected void WaitOutTransition()
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.outTransition != (Object) null && !this.outTransition.isPlaying)
      {
        this.outTransition.Wait();
      }
      else
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant wait outTransition becuase it is null or playing."));
      }
    }

    protected void PrepareInTransition()
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.inTransition != (Object) null && !this.inTransition.isPlaying)
      {
        this.inTransition.Prepare();
      }
      else
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant play inTransition becuase it is null or playing."));
      }
    }

    protected void PrepareOutTransition()
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.outTransition != (Object) null && !this.outTransition.isPlaying)
      {
        this.outTransition.Prepare();
      }
      else
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant play outTransition becuase it is null or playing."));
      }
    }

    protected void PlayInTransition()
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.inTransition != (Object) null && !this.inTransition.isPlaying)
      {
        this.playingInTransition = true;
        this.StartCoroutine(this.DoInTransition());
      }
      else
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant play inTransition becuase it is null or playing."));
      }
    }

    protected void PlayOutTransition()
    {
      if (DebugManager.StateForKey("TransitionObjectKeys Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if ((Object) this.outTransition != (Object) null && !this.outTransition.isPlaying)
      {
        this.playingOutTransition = true;
        this.StartCoroutine(this.DoOutTransition());
      }
      else
      {
        if (!DebugManager.StateForKey("TransitionObjectKeys Warnings"))
          return;
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Cant play outTransition becuase it is null or playing."));
      }
    }

    protected IEnumerator DoInTransition()
    {
      if ((bool) (Object) this.outTransition)
      {
        yield return (object) new WaitForEndOfFrame();
        this.outTransition.End();
      }
      if ((bool) (Object) this.inTransition)
      {
        this.inTransition.Play();
        while (this.inTransition.isPlaying)
          yield return (object) null;
        this.inTransition.End();
      }
      this.playingInTransition = false;
    }

    protected IEnumerator DoOutTransition()
    {
      if ((bool) (Object) this.outTransition)
      {
        this.outTransition.Play();
        while (this.outTransition.isPlaying)
          yield return (object) null;
      }
      else if ((bool) (Object) this.inTransition)
      {
        this.inTransition.Prepare();
        this.inTransition.ActivateRender(true);
      }
      this.playingOutTransition = false;
    }
  }
}
