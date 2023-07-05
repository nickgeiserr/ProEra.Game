// Decompiled with JetBrains decompiler
// Type: PlusPlusPlus.SITA_Slave
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using RootMotion.Dynamics;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PlusPlusPlus
{
  public class SITA_Slave : MonoBehaviour
  {
    public SITA_Master master;
    public Transform slaveOverride;
    public PuppetMaster puppetMaster;
    public SITA_Slave.Frames rec = new SITA_Slave.Frames();
    public List<SITA_Slave.Frames> savedRec = new List<SITA_Slave.Frames>();
    public int cloneID;
    public Transform clone;
    private Quaternion tempRot;
    private Thread recordingThread;

    private void OnEnable()
    {
      if (!((UnityEngine.Object) this.master != (UnityEngine.Object) null))
        return;
      if ((UnityEngine.Object) this.puppetMaster != (UnityEngine.Object) null)
        this.master.RecordFrameEvent += new SITA_Master.OnRecordFrame(this.RecordFrameSpecial);
      else
        this.master.RecordFrameEvent += new SITA_Master.OnRecordFrame(this.RecordFrame);
      this.master.PlayFrameEvent += new SITA_Master.OnPlayFrame(this.PlayFrame);
    }

    private void OnDisable()
    {
      if ((UnityEngine.Object) this.puppetMaster != (UnityEngine.Object) null)
        this.master.RecordFrameEvent -= new SITA_Master.OnRecordFrame(this.RecordFrameSpecial);
      else
        this.master.RecordFrameEvent -= new SITA_Master.OnRecordFrame(this.RecordFrame);
      this.master.PlayFrameEvent -= new SITA_Master.OnPlayFrame(this.PlayFrame);
    }

    public void AssignMaster(SITA_Master m)
    {
      this.master = m;
      if ((UnityEngine.Object) this.puppetMaster != (UnityEngine.Object) null)
        this.master.RecordFrameEvent += new SITA_Master.OnRecordFrame(this.RecordFrameSpecial);
      else
        this.master.RecordFrameEvent += new SITA_Master.OnRecordFrame(this.RecordFrame);
      this.master.PlayFrameEvent += new SITA_Master.OnPlayFrame(this.PlayFrame);
    }

    public void RecordFrame()
    {
      this.rec.position.Add(this.transform.position);
      this.rec.rotation.Add(this.transform.rotation);
    }

    public void RecordFrameSpecial()
    {
      if (this.puppetMaster.mode == PuppetMaster.Mode.Active && (double) this.puppetMaster.mappingWeight >= 0.5 && (UnityEngine.Object) this.slaveOverride != (UnityEngine.Object) null)
      {
        this.rec.position.Add(this.slaveOverride.position);
        this.rec.rotation.Add(this.slaveOverride.rotation);
      }
      else if ((UnityEngine.Object) this.slaveOverride != (UnityEngine.Object) null)
      {
        this.rec.position.Add(this.transform.position);
        this.rec.rotation.Add(this.transform.rotation);
      }
      else
      {
        this.rec.position.Add(this.transform.localPosition);
        this.rec.rotation.Add(this.transform.localRotation);
      }
    }

    public void PlayFrame()
    {
      if (SITA_Engine.instance.controller == PlaybackModes.Play || SITA_Engine.instance.controller == PlaybackModes.FastForward || SITA_Engine.instance.controller == PlaybackModes.SlowForward || SITA_Engine.instance.controller == PlaybackModes.FrameForward)
      {
        if (SITA_Engine.instance.frame + 1 >= this.savedRec[SITA_Engine.instance.replayIndex].position.Count)
          return;
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].position[SITA_Engine.instance.frame], this.savedRec[SITA_Engine.instance.replayIndex].position[SITA_Engine.instance.frame + 1], SITA_Engine.instance.timeStep).setOnUpdate((Action<Vector3>) (value =>
        {
          if ((UnityEngine.Object) this.slaveOverride != (UnityEngine.Object) null)
            this.clone.position = value;
          else
            this.clone.localPosition = value;
        }));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].x, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame + 1].x, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value => this.tempRot.x = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].y, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame + 1].y, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value => this.tempRot.y = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].z, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame + 1].z, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value => this.tempRot.z = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].w, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame + 1].w, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value =>
        {
          this.tempRot.w = value;
          if ((UnityEngine.Object) this.slaveOverride != (UnityEngine.Object) null)
            this.clone.rotation = this.tempRot;
          else
            this.clone.localRotation = this.tempRot;
        }));
      }
      else
      {
        if (SITA_Engine.instance.frame <= 0)
          return;
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].position[SITA_Engine.instance.frame], this.savedRec[SITA_Engine.instance.replayIndex].position[SITA_Engine.instance.frame - 1], SITA_Engine.instance.timeStep).setOnUpdate((Action<Vector3>) (value => this.clone.position = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].x, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame - 1].x, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value => this.tempRot.x = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].y, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame - 1].y, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value => this.tempRot.y = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].z, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame - 1].z, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value => this.tempRot.z = value));
        LeanTween.value(this.gameObject, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame].w, this.savedRec[SITA_Engine.instance.replayIndex].rotation[SITA_Engine.instance.frame - 1].w, SITA_Engine.instance.timeStep).setOnUpdate((Action<float>) (value =>
        {
          this.tempRot.w = value;
          this.clone.rotation = this.tempRot;
        }));
      }
    }

    public void SaveLastRecording()
    {
      this.savedRec.Add(this.rec);
      this.rec = new SITA_Slave.Frames();
    }

    [Serializable]
    public class Frames
    {
      public List<Vector3> position = new List<Vector3>();
      public List<Quaternion> rotation = new List<Quaternion>();
    }
  }
}
