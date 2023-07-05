// Decompiled with JetBrains decompiler
// Type: PlusPlusPlus.SITA_Master
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlusPlusPlus
{
  public class SITA_Master : MonoBehaviour
  {
    public bool manuallyAssignComponents;
    public bool manuallyAssignSlaves;
    public Transform[] trans;
    public MeshRenderer[] meshes;
    public Collider[] colliders;
    public Rigidbody[] rigidBodies;
    public List<SITA_Slave> slaves = new List<SITA_Slave>();
    public SITA_Master.Clone clone = new SITA_Master.Clone();
    public List<SITA_Master.ReplayMeta> replayMeta = new List<SITA_Master.ReplayMeta>();
    public SITA_Master.SavedState savedState = new SITA_Master.SavedState();

    public event SITA_Master.OnRecordFrame RecordFrameEvent;

    public event SITA_Master.OnPlayFrame PlayFrameEvent;

    private void Start() => this.Initialize();

    private void OnEnable()
    {
      SITA_Engine.instance.Register(this);
      SITA_Engine.instance.RecordFrame += new SITA_Engine.OnRecordFrame(this.RecordFrame);
      SITA_Engine.instance.PlayFrame += new SITA_Engine.OnPlayFrame(this.PlayFrame);
      SITA_Engine.instance.ReplayingEvent += new SITA_Engine.OnReplayingEvent(this.ReplayEvent);
    }

    private void OnDisable()
    {
      SITA_Engine.instance.Unregister(this);
      SITA_Engine.instance.RecordFrame -= new SITA_Engine.OnRecordFrame(this.RecordFrame);
      SITA_Engine.instance.PlayFrame -= new SITA_Engine.OnPlayFrame(this.PlayFrame);
      SITA_Engine.instance.ReplayingEvent -= new SITA_Engine.OnReplayingEvent(this.ReplayEvent);
    }

    private void Initialize()
    {
      if (!this.manuallyAssignComponents)
      {
        this.trans = this.GetComponentsInChildren<Transform>();
        this.meshes = this.GetComponentsInChildren<MeshRenderer>();
        this.colliders = this.GetComponentsInChildren<Collider>();
        this.rigidBodies = this.GetComponentsInChildren<Rigidbody>();
      }
      if (!this.manuallyAssignSlaves)
      {
        for (int index = 0; index < this.trans.Length; ++index)
        {
          this.slaves.Add(this.trans[index].gameObject.AddComponent<SITA_Slave>());
          this.slaves[this.slaves.Count - 1].AssignMaster(this);
        }
      }
      else
        this.GetComponentsInChildren<SITA_Slave>(this.slaves);
    }

    public void SaveRecording()
    {
      for (int index = 0; index < this.slaves.Count; ++index)
        this.slaves[index].SaveLastRecording();
    }

    public void RecordFrame() => this.RecordFrameEvent();

    public void PlayFrame() => this.PlayFrameEvent();

    public void ReplayEvent(bool replay)
    {
      if (replay)
        this.StartReplay();
      else
        this.StopReplay();
    }

    public void StartReplay()
    {
      this.clone.gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gameObject);
      this.clone.gameObject.transform.localScale = this.transform.localScale;
      this.clone.transform = this.clone.gameObject.GetComponentsInChildren<Transform>();
      for (int index = 0; index < this.slaves.Count; ++index)
      {
        this.slaves[index].clone = this.clone.transform[index];
        this.clone.transform[index].position = this.slaves[index].savedRec[SITA_Engine.instance.replayIndex].position[0];
        this.clone.transform[index].rotation = this.slaves[index].savedRec[SITA_Engine.instance.replayIndex].rotation[0];
      }
      foreach (UnityEngine.Object componentsInChild in this.clone.gameObject.GetComponentsInChildren<MonoBehaviour>())
        UnityEngine.Object.Destroy(componentsInChild);
      foreach (UnityEngine.Object componentsInChild in this.clone.gameObject.GetComponentsInChildren<Rigidbody>())
        UnityEngine.Object.Destroy(componentsInChild);
      foreach (UnityEngine.Object componentsInChild in this.clone.gameObject.GetComponentsInChildren<Collider>())
        UnityEngine.Object.Destroy(componentsInChild);
      this.SaveState();
      for (int index = 0; index < this.meshes.Length; ++index)
        this.meshes[index].enabled = false;
      for (int index = 0; index < this.colliders.Length; ++index)
        this.colliders[index].enabled = false;
      for (int index = 0; index < this.rigidBodies.Length; ++index)
        this.rigidBodies[index].isKinematic = true;
    }

    public void StopReplay()
    {
      for (int index = 0; index < this.slaves.Count; ++index)
        LeanTween.cancel(this.slaves[index].gameObject);
      UnityEngine.Object.Destroy((UnityEngine.Object) this.clone.gameObject);
      for (int index = 0; index < this.meshes.Length; ++index)
        this.meshes[index].enabled = true;
      for (int index = 0; index < this.colliders.Length; ++index)
        this.colliders[index].enabled = true;
      this.RestoreState();
    }

    public void SaveState()
    {
      this.savedState.SaveTransform(this.trans);
      this.savedState.SaveMesh(this.meshes);
      this.savedState.SaveCollider(this.colliders);
      this.savedState.SaveRigidbody(this.rigidBodies);
    }

    public void RestoreState()
    {
      this.savedState.RestoreTransform(ref this.trans);
      this.savedState.RestoreMesh(ref this.meshes);
      this.savedState.RestoreCollider(ref this.colliders);
      this.savedState.RestoreRigidbody(ref this.rigidBodies);
    }

    public delegate void OnRecordFrame();

    public delegate void OnPlayFrame();

    [Serializable]
    public class Clone
    {
      public GameObject gameObject;
      public Transform[] transform;
    }

    public class ReplayMeta
    {
      public List<byte[]> meshBytes = new List<byte[]>();
    }

    [Serializable]
    public class SavedState
    {
      public Vector3[] position;
      public Quaternion[] rotation;
      public bool[] meshEnabled;
      public bool[] colliderEnabled;
      public bool[] isKinematic;
      public Vector3[] velocity;

      public void SaveTransform(Transform[] trans)
      {
        this.position = new Vector3[trans.Length];
        this.rotation = new Quaternion[trans.Length];
        for (int index = 0; index < trans.Length; ++index)
        {
          this.position[index] = trans[index].position;
          this.rotation[index] = trans[index].rotation;
        }
      }

      public void RestoreTransform(ref Transform[] trans)
      {
        for (int index = 0; index < trans.Length; ++index)
        {
          trans[index].position = this.position[index];
          trans[index].rotation = this.rotation[index];
        }
      }

      public void SaveMesh(MeshRenderer[] mesh)
      {
        this.meshEnabled = new bool[mesh.Length];
        for (int index = 0; index < mesh.Length; ++index)
          this.meshEnabled[index] = mesh[index].enabled;
      }

      public void RestoreMesh(ref MeshRenderer[] mesh)
      {
        for (int index = 0; index < mesh.Length; ++index)
          mesh[index].enabled = this.meshEnabled[index];
      }

      public void SaveCollider(Collider[] collider)
      {
        this.colliderEnabled = new bool[collider.Length];
        for (int index = 0; index < collider.Length; ++index)
          this.colliderEnabled[index] = collider[index].enabled;
      }

      public void RestoreCollider(ref Collider[] collider)
      {
        for (int index = 0; index < collider.Length; ++index)
          collider[index].enabled = this.colliderEnabled[index];
      }

      public void SaveRigidbody(Rigidbody[] rigidbody)
      {
        this.isKinematic = new bool[rigidbody.Length];
        this.velocity = new Vector3[rigidbody.Length];
        for (int index = 0; index < rigidbody.Length; ++index)
        {
          this.isKinematic[index] = rigidbody[index].isKinematic;
          this.velocity[index] = rigidbody[index].velocity;
        }
      }

      public void RestoreRigidbody(ref Rigidbody[] rigidbody)
      {
        for (int index = 0; index < rigidbody.Length; ++index)
        {
          rigidbody[index].isKinematic = this.isKinematic[index];
          rigidbody[index].velocity = this.velocity[index];
        }
      }
    }
  }
}
