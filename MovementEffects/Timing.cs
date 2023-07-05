// Decompiled with JetBrains decompiler
// Type: MovementEffects.Timing
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MovementEffects
{
  public class Timing : MonoBehaviour
  {
    [Tooltip("How quickly the SlowUpdate segment ticks.")]
    public float TimeBetweenSlowUpdateCalls = 0.142857149f;
    [Tooltip("How much data should be sent to the profiler window when it's open.")]
    public Timing.DebugInfoType ProfilerDebugAmount = Timing.DebugInfoType.SeperateCoroutines;
    [Tooltip("When using manual timeframe, should it run automatically after the update loop or only when TriggerManualTimframeUpdate is called.")]
    public bool AutoTriggerManualTimeframe = true;
    [Tooltip("A count of the number of Update coroutines that are currently running.")]
    [Space(12f)]
    public int UpdateCoroutines;
    [Tooltip("A count of the number of FixedUpdate coroutines that are currently running.")]
    public int FixedUpdateCoroutines;
    [Tooltip("A count of the number of LateUpdate coroutines that are currently running.")]
    public int LateUpdateCoroutines;
    [Tooltip("A count of the number of SlowUpdate coroutines that are currently running.")]
    public int SlowUpdateCoroutines;
    [Tooltip("A count of the number of RealtimeUpdate coroutines that are currently running.")]
    public int RealtimeUpdateCoroutines;
    [Tooltip("A count of the number of EditorUpdate coroutines that are currently running.")]
    public int EditorUpdateCoroutines;
    [Tooltip("A count of the number of EditorSlowUpdate coroutines that are currently running.")]
    public int EditorSlowUpdateCoroutines;
    [Tooltip("A count of the number of EndOfFrame coroutines that are currently running.")]
    public int EndOfFrameCoroutines;
    [Tooltip("A count of the number of ManualTimeframe coroutines that are currently running.")]
    public int ManualTimeframeCoroutines;
    [HideInInspector]
    public double localTime;
    [HideInInspector]
    public float deltaTime;
    public Action<Exception> OnError;
    public Func<double, double> SetManualTimeframeTime;
    public static Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>> ReplacementFunction;
    public static readonly float WaitForOneFrame = 0.0f;
    private bool _runningUpdate;
    private bool _runningSlowUpdate;
    private bool _runningFixedUpdate;
    private bool _runningLateUpdate;
    private bool _runningRealtimeUpdate;
    private bool _runningEditorUpdate;
    private bool _runningEditorSlowUpdate;
    private int _nextUpdateProcessSlot;
    private int _nextLateUpdateProcessSlot;
    private int _nextFixedUpdateProcessSlot;
    private int _nextSlowUpdateProcessSlot;
    private int _nextRealtimeUpdateProcessSlot;
    private int _nextEditorUpdateProcessSlot;
    private int _nextEditorSlowUpdateProcessSlot;
    private int _nextEndOfFrameProcessSlot;
    private int _nextManualTimeframeProcessSlot;
    private double _lastUpdateTime;
    private double _lastLateUpdateTime;
    private double _lastFixedUpdateTime;
    private double _lastSlowUpdateTime;
    private double _lastRealtimeUpdateTime;
    private double _lastEditorUpdateTime;
    private double _lastEditorSlowUpdateTime;
    private double _lastManualTimeframeTime;
    private ushort _framesSinceUpdate;
    private ushort _expansions = 1;
    private byte _instanceID;
    private bool _EOFPumpRan;
    private readonly WaitForEndOfFrame _EOFWaitObject = new WaitForEndOfFrame();
    private readonly Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>> _waitingTriggers = new Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>();
    private readonly Queue<Exception> _exceptions = new Queue<Exception>();
    private readonly Dictionary<CoroutineHandle, Timing.ProcessIndex> _handleToIndex = new Dictionary<CoroutineHandle, Timing.ProcessIndex>();
    private readonly Dictionary<Timing.ProcessIndex, CoroutineHandle> _indexToHandle = new Dictionary<Timing.ProcessIndex, CoroutineHandle>();
    private readonly Dictionary<Timing.ProcessIndex, string> _processTags = new Dictionary<Timing.ProcessIndex, string>();
    private readonly Dictionary<string, HashSet<Timing.ProcessIndex>> _taggedProcesses = new Dictionary<string, HashSet<Timing.ProcessIndex>>();
    private readonly Dictionary<Timing.ProcessIndex, int> _processLayers = new Dictionary<Timing.ProcessIndex, int>();
    private readonly Dictionary<int, HashSet<Timing.ProcessIndex>> _layeredProcesses = new Dictionary<int, HashSet<Timing.ProcessIndex>>();
    private IEnumerator<float>[] UpdateProcesses = new IEnumerator<float>[256];
    private IEnumerator<float>[] LateUpdateProcesses = new IEnumerator<float>[8];
    private IEnumerator<float>[] FixedUpdateProcesses = new IEnumerator<float>[64];
    private IEnumerator<float>[] SlowUpdateProcesses = new IEnumerator<float>[64];
    private IEnumerator<float>[] RealtimeUpdateProcesses = new IEnumerator<float>[8];
    private IEnumerator<float>[] EditorUpdateProcesses = new IEnumerator<float>[8];
    private IEnumerator<float>[] EditorSlowUpdateProcesses = new IEnumerator<float>[8];
    private IEnumerator<float>[] EndOfFrameProcesses = new IEnumerator<float>[8];
    private IEnumerator<float>[] ManualTimeframeProcesses = new IEnumerator<float>[8];
    private bool[] UpdatePaused = new bool[256];
    private bool[] LateUpdatePaused = new bool[8];
    private bool[] FixedUpdatePaused = new bool[64];
    private bool[] SlowUpdatePaused = new bool[64];
    private bool[] RealtimeUpdatePaused = new bool[8];
    private bool[] EditorUpdatePaused = new bool[8];
    private bool[] EditorSlowUpdatePaused = new bool[8];
    private bool[] EndOfFramePaused = new bool[8];
    private bool[] ManualTimeframePaused = new bool[8];
    private const ushort FramesUntilMaintenance = 64;
    private const int ProcessArrayChunkSize = 64;
    private const int InitialBufferSizeLarge = 256;
    private const int InitialBufferSizeMedium = 64;
    private const int InitialBufferSizeSmall = 8;
    private static readonly Dictionary<byte, Timing> ActiveInstances = new Dictionary<byte, Timing>();
    private static Timing _instance;

    public static float LocalTime => (float) Timing.Instance.localTime;

    public static float DeltaTime => Timing.Instance.deltaTime;

    public static event Action OnPreExecute;

    public static Timing Instance
    {
      get
      {
        if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || !(bool) (UnityEngine.Object) Timing._instance.gameObject)
        {
          GameObject gameObject1 = GameObject.Find("Movement Effects");
          System.Type type = System.Type.GetType("MovementEffects.Movement, MovementOverTime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
          if ((UnityEngine.Object) gameObject1 == (UnityEngine.Object) null)
          {
            GameObject gameObject2 = new GameObject();
            gameObject2.name = "Movement Effects";
            GameObject target = gameObject2;
            UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
            if (type != (System.Type) null)
              target.AddComponent(type);
            Timing._instance = target.AddComponent<Timing>();
          }
          else
          {
            if (type != (System.Type) null && (UnityEngine.Object) gameObject1.GetComponent(type) == (UnityEngine.Object) null)
              gameObject1.AddComponent(type);
            Timing._instance = gameObject1.GetComponent<Timing>() ?? gameObject1.AddComponent<Timing>();
          }
        }
        return Timing._instance;
      }
      set => Timing._instance = value;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null)
        Timing._instance = this;
      else
        this.deltaTime = Timing._instance.deltaTime;
      this._instanceID = (byte) 1;
      while (Timing.ActiveInstances.ContainsKey(this._instanceID))
        ++this._instanceID;
      if (this._instanceID == (byte) 32)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
        throw new OverflowException("You are only allowed 31 instances of MEC at one time.");
      }
      Timing.ActiveInstances.Add(this._instanceID, this);
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) this)
        Timing._instance = (Timing) null;
      Timing.ActiveInstances.Remove(this._instanceID);
    }

    private void OnEnable()
    {
      if (this._nextEditorUpdateProcessSlot > 0 || this._nextEditorSlowUpdateProcessSlot > 0)
        this.OnEditorStart();
      if (this._nextEndOfFrameProcessSlot <= 0)
        return;
      this.RunCoroutineSingletonOnInstance(this._EOFPumpWatcher(), "MEC_EOFPumpWatcher");
    }

    private void Update()
    {
      if (Timing.OnPreExecute != null)
        Timing.OnPreExecute();
      if (this._lastSlowUpdateTime + (double) this.TimeBetweenSlowUpdateCalls < (double) Time.realtimeSinceStartup && this._nextSlowUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.SlowUpdate
        };
        this._runningSlowUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextSlowUpdateProcessSlot; ++key.i)
        {
          if (!this.SlowUpdatePaused[key.i] && this.SlowUpdateProcesses[key.i] != null && this.localTime >= (double) this.SlowUpdateProcesses[key.i].Current)
          {
            int profilerDebugAmount1 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.SlowUpdateProcesses[key.i].MoveNext())
                this.SlowUpdateProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.SlowUpdateProcesses[key.i] != null)
              {
                if (float.IsNaN(this.SlowUpdateProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.SlowUpdateProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.SlowUpdateProcesses[key.i] = Timing.ReplacementFunction(this.SlowUpdateProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.SlowUpdateProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount2 = (int) this.ProfilerDebugAmount;
          }
        }
        this._runningSlowUpdate = false;
      }
      if (this._nextRealtimeUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.RealtimeUpdate
        };
        this._runningRealtimeUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextRealtimeUpdateProcessSlot; ++key.i)
        {
          if (!this.RealtimeUpdatePaused[key.i] && this.RealtimeUpdateProcesses[key.i] != null && this.localTime >= (double) this.RealtimeUpdateProcesses[key.i].Current)
          {
            int profilerDebugAmount3 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.RealtimeUpdateProcesses[key.i].MoveNext())
                this.RealtimeUpdateProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.RealtimeUpdateProcesses[key.i] != null)
              {
                if (float.IsNaN(this.RealtimeUpdateProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.RealtimeUpdateProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.RealtimeUpdateProcesses[key.i] = Timing.ReplacementFunction(this.RealtimeUpdateProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.RealtimeUpdateProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount4 = (int) this.ProfilerDebugAmount;
          }
        }
        this._runningRealtimeUpdate = false;
      }
      if (this._nextUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.Update
        };
        this._runningUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextUpdateProcessSlot; ++key.i)
        {
          if (!this.UpdatePaused[key.i] && this.UpdateProcesses[key.i] != null && this.localTime >= (double) this.UpdateProcesses[key.i].Current)
          {
            int profilerDebugAmount5 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.UpdateProcesses[key.i].MoveNext())
                this.UpdateProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.UpdateProcesses[key.i] != null)
              {
                if (float.IsNaN(this.UpdateProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.UpdateProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.UpdateProcesses[key.i] = Timing.ReplacementFunction(this.UpdateProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.UpdateProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount6 = (int) this.ProfilerDebugAmount;
          }
        }
        this._runningUpdate = false;
      }
      if (this.AutoTriggerManualTimeframe)
      {
        this.TriggerManualTimeframeUpdate();
      }
      else
      {
        if (++this._framesSinceUpdate > (ushort) 64)
        {
          this._framesSinceUpdate = (ushort) 0;
          int profilerDebugAmount7 = (int) this.ProfilerDebugAmount;
          this.RemoveUnused();
          int profilerDebugAmount8 = (int) this.ProfilerDebugAmount;
        }
        if (this._exceptions.Count > 0)
          throw this._exceptions.Dequeue();
      }
    }

    private void FixedUpdate()
    {
      if (Timing.OnPreExecute != null)
        Timing.OnPreExecute();
      if (this._nextFixedUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.FixedUpdate
        };
        this._runningFixedUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextFixedUpdateProcessSlot; ++key.i)
        {
          if (!this.FixedUpdatePaused[key.i] && this.FixedUpdateProcesses[key.i] != null && this.localTime >= (double) this.FixedUpdateProcesses[key.i].Current)
          {
            int profilerDebugAmount1 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.FixedUpdateProcesses[key.i].MoveNext())
                this.FixedUpdateProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.FixedUpdateProcesses[key.i] != null)
              {
                if (float.IsNaN(this.FixedUpdateProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.FixedUpdateProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.FixedUpdateProcesses[key.i] = Timing.ReplacementFunction(this.FixedUpdateProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.FixedUpdateProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount2 = (int) this.ProfilerDebugAmount;
          }
        }
        this._runningFixedUpdate = false;
      }
      if (this._exceptions.Count > 0)
        throw this._exceptions.Dequeue();
    }

    private void LateUpdate()
    {
      if (Timing.OnPreExecute != null)
        Timing.OnPreExecute();
      if (this._nextLateUpdateProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.LateUpdate
        };
        this._runningLateUpdate = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextLateUpdateProcessSlot; ++key.i)
        {
          if (!this.LateUpdatePaused[key.i] && this.LateUpdateProcesses[key.i] != null && this.localTime >= (double) this.LateUpdateProcesses[key.i].Current)
          {
            int profilerDebugAmount1 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.LateUpdateProcesses[key.i].MoveNext())
                this.LateUpdateProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.LateUpdateProcesses[key.i] != null)
              {
                if (float.IsNaN(this.LateUpdateProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.LateUpdateProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.LateUpdateProcesses[key.i] = Timing.ReplacementFunction(this.LateUpdateProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.LateUpdateProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount2 = (int) this.ProfilerDebugAmount;
          }
        }
        this._runningLateUpdate = false;
      }
      if (this._exceptions.Count > 0)
      {
        Exception exception = this._exceptions.Dequeue();
        Debug.LogError((object) ("e: " + exception?.ToString()));
        throw exception;
      }
    }

    public void TriggerManualTimeframeUpdate()
    {
      if (Timing.OnPreExecute != null)
        Timing.OnPreExecute();
      if (this._nextManualTimeframeProcessSlot > 0)
      {
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.ManualTimeframe
        };
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextManualTimeframeProcessSlot; ++key.i)
        {
          if (!this.ManualTimeframePaused[key.i] && this.ManualTimeframeProcesses[key.i] != null && this.localTime >= (double) this.ManualTimeframeProcesses[key.i].Current)
          {
            int profilerDebugAmount1 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.ManualTimeframeProcesses[key.i].MoveNext())
                this.ManualTimeframeProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.ManualTimeframeProcesses[key.i] != null)
              {
                if (float.IsNaN(this.ManualTimeframeProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.ManualTimeframeProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.ManualTimeframeProcesses[key.i] = Timing.ReplacementFunction(this.ManualTimeframeProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.ManualTimeframeProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount2 = (int) this.ProfilerDebugAmount;
          }
        }
      }
      if (++this._framesSinceUpdate > (ushort) 64)
      {
        this._framesSinceUpdate = (ushort) 0;
        int profilerDebugAmount3 = (int) this.ProfilerDebugAmount;
        this.RemoveUnused();
        int profilerDebugAmount4 = (int) this.ProfilerDebugAmount;
      }
      if (this._exceptions.Count > 0)
        throw this._exceptions.Dequeue();
    }

    private bool OnEditorStart() => false;

    private IEnumerator<float> _EOFPumpWatcher()
    {
      Timing timing = this;
      while (timing._nextEndOfFrameProcessSlot > 0)
      {
        if (!timing._EOFPumpRan)
          ((MonoBehaviour) timing).StartCoroutine(timing._EOFPump());
        timing._EOFPumpRan = false;
        yield return 0.0f;
      }
      timing._EOFPumpRan = false;
    }

    private IEnumerator _EOFPump()
    {
      while (this._nextEndOfFrameProcessSlot > 0)
      {
        yield return (object) this._EOFWaitObject;
        if (Timing.OnPreExecute != null)
          Timing.OnPreExecute();
        Timing.ProcessIndex key = new Timing.ProcessIndex()
        {
          seg = Segment.EndOfFrame
        };
        this._EOFPumpRan = true;
        this.UpdateTimeValues(key.seg);
        for (key.i = 0; key.i < this._nextEndOfFrameProcessSlot; ++key.i)
        {
          if (!this.EndOfFramePaused[key.i] && this.EndOfFrameProcesses[key.i] != null && this.localTime >= (double) this.EndOfFrameProcesses[key.i].Current)
          {
            int profilerDebugAmount1 = (int) this.ProfilerDebugAmount;
            try
            {
              if (!this.EndOfFrameProcesses[key.i].MoveNext())
                this.EndOfFrameProcesses[key.i] = (IEnumerator<float>) null;
              else if (this.EndOfFrameProcesses[key.i] != null)
              {
                if (float.IsNaN(this.EndOfFrameProcesses[key.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.EndOfFrameProcesses[key.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.EndOfFrameProcesses[key.i] = Timing.ReplacementFunction(this.EndOfFrameProcesses[key.i], this._indexToHandle[key]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                    --key.i;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.EndOfFrameProcesses[key.i] = (IEnumerator<float>) null;
            }
            int profilerDebugAmount2 = (int) this.ProfilerDebugAmount;
          }
        }
      }
    }

    private void RemoveUnused()
    {
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator = this._waitingTriggers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        KeyValuePair<CoroutineHandle, HashSet<Timing.ProcessData>> current = enumerator.Current;
        if (current.Value.Count == 0)
        {
          Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>> waitingTriggers = this._waitingTriggers;
          current = enumerator.Current;
          CoroutineHandle key = current.Key;
          waitingTriggers.Remove(key);
          enumerator = this._waitingTriggers.GetEnumerator();
        }
        else
        {
          Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex1 = this._handleToIndex;
          current = enumerator.Current;
          CoroutineHandle key1 = current.Key;
          if (handleToIndex1.ContainsKey(key1))
          {
            Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex2 = this._handleToIndex;
            current = enumerator.Current;
            CoroutineHandle key2 = current.Key;
            if (this.CoindexIsNull(handleToIndex2[key2]))
            {
              current = enumerator.Current;
              this.CloseWaitingProcess(current.Key);
              enumerator = this._waitingTriggers.GetEnumerator();
            }
          }
        }
      }
      Timing.ProcessIndex processIndex;
      Timing.ProcessIndex coindexTo;
      processIndex.seg = coindexTo.seg = Segment.Update;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextUpdateProcessSlot; ++processIndex.i)
      {
        if (this.UpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.UpdateProcesses[coindexTo.i] = this.UpdateProcesses[processIndex.i];
            this.UpdatePaused[coindexTo.i] = this.UpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextUpdateProcessSlot; ++processIndex.i)
      {
        this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.UpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.UpdateCoroutines = this._nextUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.FixedUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextFixedUpdateProcessSlot; ++processIndex.i)
      {
        if (this.FixedUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.FixedUpdateProcesses[coindexTo.i] = this.FixedUpdateProcesses[processIndex.i];
            this.FixedUpdatePaused[coindexTo.i] = this.FixedUpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextFixedUpdateProcessSlot; ++processIndex.i)
      {
        this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.FixedUpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.FixedUpdateCoroutines = this._nextFixedUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.LateUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextLateUpdateProcessSlot; ++processIndex.i)
      {
        if (this.LateUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.LateUpdateProcesses[coindexTo.i] = this.LateUpdateProcesses[processIndex.i];
            this.LateUpdatePaused[coindexTo.i] = this.LateUpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextLateUpdateProcessSlot; ++processIndex.i)
      {
        this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.LateUpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.LateUpdateCoroutines = this._nextLateUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.SlowUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextSlowUpdateProcessSlot; ++processIndex.i)
      {
        if (this.SlowUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.SlowUpdateProcesses[coindexTo.i] = this.SlowUpdateProcesses[processIndex.i];
            this.SlowUpdatePaused[coindexTo.i] = this.SlowUpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextSlowUpdateProcessSlot; ++processIndex.i)
      {
        this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.SlowUpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.SlowUpdateCoroutines = this._nextSlowUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.RealtimeUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextRealtimeUpdateProcessSlot; ++processIndex.i)
      {
        if (this.RealtimeUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.RealtimeUpdateProcesses[coindexTo.i] = this.RealtimeUpdateProcesses[processIndex.i];
            this.RealtimeUpdatePaused[coindexTo.i] = this.RealtimeUpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextRealtimeUpdateProcessSlot; ++processIndex.i)
      {
        this.RealtimeUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.RealtimeUpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.RealtimeUpdateCoroutines = this._nextRealtimeUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.EndOfFrame;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextEndOfFrameProcessSlot; ++processIndex.i)
      {
        if (this.EndOfFrameProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.EndOfFrameProcesses[coindexTo.i] = this.EndOfFrameProcesses[processIndex.i];
            this.EndOfFramePaused[coindexTo.i] = this.EndOfFramePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextEndOfFrameProcessSlot; ++processIndex.i)
      {
        this.EndOfFrameProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.EndOfFramePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.EndOfFrameCoroutines = this._nextEndOfFrameProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.ManualTimeframe;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextManualTimeframeProcessSlot; ++processIndex.i)
      {
        if (this.ManualTimeframeProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.ManualTimeframeProcesses[coindexTo.i] = this.ManualTimeframeProcesses[processIndex.i];
            this.ManualTimeframePaused[coindexTo.i] = this.ManualTimeframePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextManualTimeframeProcessSlot; ++processIndex.i)
      {
        this.ManualTimeframeProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.ManualTimeframePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.ManualTimeframeCoroutines = this._nextManualTimeframeProcessSlot = coindexTo.i;
    }

    private void EditorRemoveUnused()
    {
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator = this._waitingTriggers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex1 = this._handleToIndex;
        KeyValuePair<CoroutineHandle, HashSet<Timing.ProcessData>> current = enumerator.Current;
        CoroutineHandle key1 = current.Key;
        if (handleToIndex1.ContainsKey(key1))
        {
          Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex2 = this._handleToIndex;
          current = enumerator.Current;
          CoroutineHandle key2 = current.Key;
          if (this.CoindexIsNull(handleToIndex2[key2]))
          {
            current = enumerator.Current;
            this.CloseWaitingProcess(current.Key);
            enumerator = this._waitingTriggers.GetEnumerator();
          }
        }
      }
      Timing.ProcessIndex processIndex;
      Timing.ProcessIndex coindexTo;
      processIndex.seg = coindexTo.seg = Segment.EditorUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextEditorUpdateProcessSlot; ++processIndex.i)
      {
        if (this.EditorUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.EditorUpdateProcesses[coindexTo.i] = this.EditorUpdateProcesses[processIndex.i];
            this.EditorUpdatePaused[coindexTo.i] = this.EditorUpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextEditorUpdateProcessSlot; ++processIndex.i)
      {
        this.EditorUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.EditorUpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.EditorUpdateCoroutines = this._nextEditorUpdateProcessSlot = coindexTo.i;
      processIndex.seg = coindexTo.seg = Segment.EditorSlowUpdate;
      for (processIndex.i = coindexTo.i = 0; processIndex.i < this._nextEditorSlowUpdateProcessSlot; ++processIndex.i)
      {
        if (this.EditorSlowUpdateProcesses[processIndex.i] != null)
        {
          if (processIndex.i != coindexTo.i)
          {
            this.EditorSlowUpdateProcesses[coindexTo.i] = this.EditorSlowUpdateProcesses[processIndex.i];
            this.EditorUpdatePaused[coindexTo.i] = this.EditorUpdatePaused[processIndex.i];
            this.MoveGraffiti(processIndex, coindexTo);
          }
          ++coindexTo.i;
        }
      }
      for (processIndex.i = coindexTo.i; processIndex.i < this._nextEditorSlowUpdateProcessSlot; ++processIndex.i)
      {
        this.EditorSlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
        this.EditorSlowUpdatePaused[processIndex.i] = false;
        this.RemoveGraffiti(processIndex);
      }
      this.EditorSlowUpdateCoroutines = this._nextEditorSlowUpdateProcessSlot = coindexTo.i;
    }

    public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine) => coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(), (string) null, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();

    public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, string tag) => coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(), tag, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();

    public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, int layer) => coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), (string) null, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();

    public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, int layer, string tag) => coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();

    public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment) => coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, segment, new int?(), (string) null, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();

    public static CoroutineHandle RunCoroutine(
      IEnumerator<float> coroutine,
      Segment segment,
      string tag)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, segment, new int?(), tag, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();
    }

    public static CoroutineHandle RunCoroutine(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, segment, new int?(layer), (string) null, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();
    }

    public static CoroutineHandle RunCoroutine(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer,
      string tag)
    {
      return coroutine != null ? Timing.Instance.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(Timing.Instance._instanceID), true) : new CoroutineHandle();
    }

    public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine) => coroutine != null ? this.RunCoroutineInternal(coroutine, Segment.Update, new int?(), (string) null, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();

    public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, string tag) => coroutine != null ? this.RunCoroutineInternal(coroutine, Segment.Update, new int?(), tag, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();

    public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, int layer) => coroutine != null ? this.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), (string) null, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();

    public CoroutineHandle RunCoroutineOnInstance(
      IEnumerator<float> coroutine,
      int layer,
      string tag)
    {
      return coroutine != null ? this.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();
    }

    public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment) => coroutine != null ? this.RunCoroutineInternal(coroutine, segment, new int?(), (string) null, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();

    public CoroutineHandle RunCoroutineOnInstance(
      IEnumerator<float> coroutine,
      Segment segment,
      string tag)
    {
      return coroutine != null ? this.RunCoroutineInternal(coroutine, segment, new int?(), tag, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();
    }

    public CoroutineHandle RunCoroutineOnInstance(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer)
    {
      return coroutine != null ? this.RunCoroutineInternal(coroutine, segment, new int?(layer), (string) null, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();
    }

    public CoroutineHandle RunCoroutineOnInstance(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer,
      string tag)
    {
      return coroutine != null ? this.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(this._instanceID), true) : new CoroutineHandle();
    }

    public static CoroutineHandle RunCoroutineSingleton(
      IEnumerator<float> coroutine,
      string tag,
      bool overwrite = false)
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(tag);
      else if (Timing._instance._taggedProcesses.ContainsKey(tag))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = Timing._instance._taggedProcesses[tag].GetEnumerator();
        if (enumerator.MoveNext())
          return Timing._instance._indexToHandle[enumerator.Current];
      }
      return Timing._instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(), tag, new CoroutineHandle(Timing._instance._instanceID), true);
    }

    public static CoroutineHandle RunCoroutineSingleton(
      IEnumerator<float> coroutine,
      int layer,
      bool overwrite = false)
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(layer);
      else if (Timing._instance._layeredProcesses.ContainsKey(layer))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = Timing._instance._layeredProcesses[layer].GetEnumerator();
        if (enumerator.MoveNext())
          return Timing._instance._indexToHandle[enumerator.Current];
      }
      return Timing._instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), (string) null, new CoroutineHandle(Timing._instance._instanceID), true);
    }

    public static CoroutineHandle RunCoroutineSingleton(
      IEnumerator<float> coroutine,
      int layer,
      string tag,
      bool overwrite = false)
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
      {
        Timing.KillCoroutines(layer, tag);
        return Timing._instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(Timing._instance._instanceID), true);
      }
      if (!Timing._instance._taggedProcesses.ContainsKey(tag) || !Timing._instance._layeredProcesses.ContainsKey(layer))
        return Timing._instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(Timing._instance._instanceID), true);
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = Timing._instance._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (Timing._instance._processLayers.ContainsKey(enumerator.Current) && Timing._instance._processLayers[enumerator.Current] == layer)
          return Timing._instance._indexToHandle[enumerator.Current];
      }
      return Timing._instance.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(Timing._instance._instanceID), true);
    }

    public static CoroutineHandle RunCoroutineSingleton(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer,
      bool overwrite = false)
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(layer);
      else if (Timing._instance._layeredProcesses.ContainsKey(layer))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = Timing._instance._layeredProcesses[layer].GetEnumerator();
        if (enumerator.MoveNext())
          return Timing._instance._indexToHandle[enumerator.Current];
      }
      return Timing._instance.RunCoroutineInternal(coroutine, segment, new int?(layer), (string) null, new CoroutineHandle(Timing._instance._instanceID), true);
    }

    public static CoroutineHandle RunCoroutineSingleton(
      IEnumerator<float> coroutine,
      Segment segment,
      string tag,
      bool overwrite = false)
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(tag);
      else if (Timing._instance._taggedProcesses.ContainsKey(tag))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = Timing._instance._taggedProcesses[tag].GetEnumerator();
        if (enumerator.MoveNext())
          return Timing._instance._indexToHandle[enumerator.Current];
      }
      return Timing._instance.RunCoroutineInternal(coroutine, segment, new int?(), tag, new CoroutineHandle(Timing._instance._instanceID), true);
    }

    public static CoroutineHandle RunCoroutineSingleton(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer,
      string tag,
      bool overwrite = false)
    {
      if ((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null || coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
      {
        Timing.KillCoroutines(layer, tag);
        return Timing._instance.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(Timing._instance._instanceID), true);
      }
      if (!Timing._instance._taggedProcesses.ContainsKey(tag) || !Timing._instance._layeredProcesses.ContainsKey(layer))
        return Timing._instance.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(Timing._instance._instanceID), true);
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = Timing._instance._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (Timing._instance._processLayers.ContainsKey(enumerator.Current) && Timing._instance._processLayers[enumerator.Current] == layer)
          return Timing._instance._indexToHandle[enumerator.Current];
      }
      return Timing._instance.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(Timing._instance._instanceID), true);
    }

    public CoroutineHandle RunCoroutineSingletonOnInstance(
      IEnumerator<float> coroutine,
      int layer,
      bool overwrite = false)
    {
      if (coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(layer);
      else if (this._layeredProcesses.ContainsKey(layer))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
        if (enumerator.MoveNext())
          return this._indexToHandle[enumerator.Current];
      }
      return this.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), (string) null, new CoroutineHandle(this._instanceID), true);
    }

    public CoroutineHandle RunCoroutineSingletonOnInstance(
      IEnumerator<float> coroutine,
      string tag,
      bool overwrite = false)
    {
      if (coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(tag);
      else if (this._taggedProcesses.ContainsKey(tag))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
        if (enumerator.MoveNext())
          return this._indexToHandle[enumerator.Current];
      }
      return this.RunCoroutineInternal(coroutine, Segment.Update, new int?(), tag, new CoroutineHandle(this._instanceID), true);
    }

    public CoroutineHandle RunCoroutineSingletonOnInstance(
      IEnumerator<float> coroutine,
      int layer,
      string tag,
      bool overwrite = false)
    {
      if (coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
      {
        Timing.KillCoroutines(layer, tag);
        return this.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(this._instanceID), true);
      }
      if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
        return this.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(this._instanceID), true);
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (this._processLayers.ContainsKey(enumerator.Current) && this._processLayers[enumerator.Current] == layer)
          return this._indexToHandle[enumerator.Current];
      }
      return this.RunCoroutineInternal(coroutine, Segment.Update, new int?(layer), tag, new CoroutineHandle(this._instanceID), true);
    }

    public CoroutineHandle RunCoroutineSingletonOnInstance(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer,
      bool overwrite = false)
    {
      if (coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(layer);
      else if (this._layeredProcesses.ContainsKey(layer))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
        if (enumerator.MoveNext())
          return this._indexToHandle[enumerator.Current];
      }
      return this.RunCoroutineInternal(coroutine, segment, new int?(layer), (string) null, new CoroutineHandle(this._instanceID), true);
    }

    public CoroutineHandle RunCoroutineSingletonOnInstance(
      IEnumerator<float> coroutine,
      Segment segment,
      string tag,
      bool overwrite = false)
    {
      if (coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
        Timing.KillCoroutines(tag);
      else if (this._taggedProcesses.ContainsKey(tag))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
        if (enumerator.MoveNext())
          return this._indexToHandle[enumerator.Current];
      }
      return this.RunCoroutineInternal(coroutine, segment, new int?(), tag, new CoroutineHandle(this._instanceID), true);
    }

    public CoroutineHandle RunCoroutineSingletonOnInstance(
      IEnumerator<float> coroutine,
      Segment segment,
      int layer,
      string tag,
      bool overwrite = false)
    {
      if (coroutine == null)
        return new CoroutineHandle();
      if (overwrite)
      {
        Timing.KillCoroutines(layer, tag);
        return this.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(this._instanceID), true);
      }
      if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
        return this.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(this._instanceID), true);
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (this._processLayers.ContainsKey(enumerator.Current) && this._processLayers[enumerator.Current] == layer)
          return this._indexToHandle[enumerator.Current];
      }
      return this.RunCoroutineInternal(coroutine, segment, new int?(layer), tag, new CoroutineHandle(this._instanceID), true);
    }

    private CoroutineHandle RunCoroutineInternal(
      IEnumerator<float> coroutine,
      Segment segment,
      int? layer,
      string tag,
      CoroutineHandle handle,
      bool prewarm)
    {
      Timing.ProcessIndex processIndex = new Timing.ProcessIndex()
      {
        seg = segment
      };
      if (this._handleToIndex.ContainsKey(handle))
      {
        this._indexToHandle.Remove(this._handleToIndex[handle]);
        this._handleToIndex.Remove(handle);
      }
      switch (segment)
      {
        case Segment.Update:
          if (this._nextUpdateProcessSlot >= this.UpdateProcesses.Length)
          {
            IEnumerator<float>[] updateProcesses = this.UpdateProcesses;
            bool[] updatePaused = this.UpdatePaused;
            this.UpdateProcesses = new IEnumerator<float>[this.UpdateProcesses.Length + 64 * (int) this._expansions++];
            this.UpdatePaused = new bool[this.UpdateProcesses.Length];
            for (int index = 0; index < updateProcesses.Length; ++index)
            {
              this.UpdateProcesses[index] = updateProcesses[index];
              this.UpdatePaused[index] = updatePaused[index];
            }
          }
          processIndex.i = this._nextUpdateProcessSlot++;
          this.UpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningUpdate & prewarm)
          {
            try
            {
              this._runningUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.UpdateProcesses[processIndex.i].MoveNext())
                this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.UpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.UpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.UpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.UpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.UpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningUpdate = false;
            }
          }
          return handle;
        case Segment.FixedUpdate:
          if (this._nextFixedUpdateProcessSlot >= this.FixedUpdateProcesses.Length)
          {
            IEnumerator<float>[] fixedUpdateProcesses = this.FixedUpdateProcesses;
            bool[] fixedUpdatePaused = this.FixedUpdatePaused;
            this.FixedUpdateProcesses = new IEnumerator<float>[this.FixedUpdateProcesses.Length + 64 * (int) this._expansions++];
            this.FixedUpdatePaused = new bool[this.FixedUpdateProcesses.Length];
            for (int index = 0; index < fixedUpdateProcesses.Length; ++index)
            {
              this.FixedUpdateProcesses[index] = fixedUpdateProcesses[index];
              this.FixedUpdatePaused[index] = fixedUpdatePaused[index];
            }
          }
          processIndex.i = this._nextFixedUpdateProcessSlot++;
          this.FixedUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningFixedUpdate & prewarm)
          {
            try
            {
              this._runningFixedUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.FixedUpdateProcesses[processIndex.i].MoveNext())
                this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.FixedUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.FixedUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.FixedUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.FixedUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.FixedUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningFixedUpdate = false;
            }
          }
          return handle;
        case Segment.LateUpdate:
          if (this._nextLateUpdateProcessSlot >= this.LateUpdateProcesses.Length)
          {
            IEnumerator<float>[] lateUpdateProcesses = this.LateUpdateProcesses;
            bool[] lateUpdatePaused = this.LateUpdatePaused;
            this.LateUpdateProcesses = new IEnumerator<float>[this.LateUpdateProcesses.Length + 64 * (int) this._expansions++];
            this.LateUpdatePaused = new bool[this.LateUpdateProcesses.Length];
            for (int index = 0; index < lateUpdateProcesses.Length; ++index)
            {
              this.LateUpdateProcesses[index] = lateUpdateProcesses[index];
              this.LateUpdatePaused[index] = lateUpdatePaused[index];
            }
          }
          processIndex.i = this._nextLateUpdateProcessSlot++;
          this.LateUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningLateUpdate & prewarm)
          {
            try
            {
              this._runningLateUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.LateUpdateProcesses[processIndex.i].MoveNext())
                this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.LateUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.LateUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.LateUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.LateUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.LateUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningLateUpdate = false;
            }
          }
          return handle;
        case Segment.SlowUpdate:
          if (this._nextSlowUpdateProcessSlot >= this.SlowUpdateProcesses.Length)
          {
            IEnumerator<float>[] slowUpdateProcesses = this.SlowUpdateProcesses;
            bool[] slowUpdatePaused = this.SlowUpdatePaused;
            this.SlowUpdateProcesses = new IEnumerator<float>[this.SlowUpdateProcesses.Length + 64 * (int) this._expansions++];
            this.SlowUpdatePaused = new bool[this.SlowUpdateProcesses.Length];
            for (int index = 0; index < slowUpdateProcesses.Length; ++index)
            {
              this.SlowUpdateProcesses[index] = slowUpdateProcesses[index];
              this.SlowUpdatePaused[index] = slowUpdatePaused[index];
            }
          }
          processIndex.i = this._nextSlowUpdateProcessSlot++;
          this.SlowUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningSlowUpdate & prewarm)
          {
            try
            {
              this._runningSlowUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.SlowUpdateProcesses[processIndex.i].MoveNext())
                this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.SlowUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.SlowUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.SlowUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.SlowUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.SlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningSlowUpdate = false;
            }
          }
          return handle;
        case Segment.RealtimeUpdate:
          if (this._nextRealtimeUpdateProcessSlot >= this.RealtimeUpdateProcesses.Length)
          {
            IEnumerator<float>[] realtimeUpdateProcesses = this.RealtimeUpdateProcesses;
            bool[] realtimeUpdatePaused = this.RealtimeUpdatePaused;
            this.RealtimeUpdateProcesses = new IEnumerator<float>[this.RealtimeUpdateProcesses.Length + 64 * (int) this._expansions++];
            this.RealtimeUpdatePaused = new bool[this.RealtimeUpdateProcesses.Length];
            for (int index = 0; index < realtimeUpdateProcesses.Length; ++index)
            {
              this.RealtimeUpdateProcesses[index] = realtimeUpdateProcesses[index];
              this.RealtimeUpdatePaused[index] = realtimeUpdatePaused[index];
            }
          }
          processIndex.i = this._nextRealtimeUpdateProcessSlot++;
          this.RealtimeUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningRealtimeUpdate & prewarm)
          {
            try
            {
              this._runningRealtimeUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.RealtimeUpdateProcesses[processIndex.i].MoveNext())
                this.RealtimeUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.RealtimeUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.RealtimeUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.RealtimeUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.RealtimeUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.RealtimeUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.RealtimeUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningRealtimeUpdate = false;
            }
          }
          return handle;
        case Segment.EditorUpdate:
          if (!this.OnEditorStart())
            return new CoroutineHandle();
          if (this._nextEditorUpdateProcessSlot >= this.EditorUpdateProcesses.Length)
          {
            IEnumerator<float>[] editorUpdateProcesses = this.EditorUpdateProcesses;
            bool[] editorUpdatePaused = this.EditorUpdatePaused;
            this.EditorUpdateProcesses = new IEnumerator<float>[this.EditorUpdateProcesses.Length + 64 * (int) this._expansions++];
            this.EditorUpdatePaused = new bool[this.EditorUpdateProcesses.Length];
            for (int index = 0; index < editorUpdateProcesses.Length; ++index)
            {
              this.EditorUpdateProcesses[index] = editorUpdateProcesses[index];
              this.EditorUpdatePaused[index] = editorUpdatePaused[index];
            }
          }
          processIndex.i = this._nextEditorUpdateProcessSlot++;
          this.EditorUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningEditorUpdate & prewarm)
          {
            try
            {
              this._runningEditorUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.EditorUpdateProcesses[processIndex.i].MoveNext())
                this.EditorUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.EditorUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.EditorUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.EditorUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.EditorUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.EditorUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.EditorUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningEditorUpdate = false;
            }
          }
          return handle;
        case Segment.EditorSlowUpdate:
          if (!this.OnEditorStart())
            return new CoroutineHandle();
          if (this._nextEditorSlowUpdateProcessSlot >= this.EditorSlowUpdateProcesses.Length)
          {
            IEnumerator<float>[] slowUpdateProcesses = this.EditorSlowUpdateProcesses;
            bool[] slowUpdatePaused = this.EditorSlowUpdatePaused;
            this.EditorSlowUpdateProcesses = new IEnumerator<float>[this.EditorSlowUpdateProcesses.Length + 64 * (int) this._expansions++];
            this.EditorSlowUpdatePaused = new bool[this.EditorSlowUpdateProcesses.Length];
            for (int index = 0; index < slowUpdateProcesses.Length; ++index)
            {
              this.EditorSlowUpdateProcesses[index] = slowUpdateProcesses[index];
              this.EditorSlowUpdatePaused[index] = slowUpdatePaused[index];
            }
          }
          processIndex.i = this._nextEditorSlowUpdateProcessSlot++;
          this.EditorSlowUpdateProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          if (!this._runningEditorSlowUpdate & prewarm)
          {
            try
            {
              this._runningEditorSlowUpdate = true;
              this.SetTimeValues(processIndex.seg);
              if (!this.EditorSlowUpdateProcesses[processIndex.i].MoveNext())
                this.EditorSlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
              else if (this.EditorSlowUpdateProcesses[processIndex.i] != null)
              {
                if (float.IsNaN(this.EditorSlowUpdateProcesses[processIndex.i].Current))
                {
                  if (Timing.ReplacementFunction == null)
                  {
                    this.EditorSlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
                  }
                  else
                  {
                    this.EditorSlowUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.EditorSlowUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
                    Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) null;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              if (this.OnError == null)
                this._exceptions.Enqueue(ex);
              else
                this.OnError(ex);
              this.EditorSlowUpdateProcesses[processIndex.i] = (IEnumerator<float>) null;
            }
            finally
            {
              this._runningEditorSlowUpdate = false;
            }
          }
          return handle;
        case Segment.EndOfFrame:
          if (this._nextEndOfFrameProcessSlot >= this.EndOfFrameProcesses.Length)
          {
            IEnumerator<float>[] ofFrameProcesses = this.EndOfFrameProcesses;
            bool[] endOfFramePaused = this.EndOfFramePaused;
            this.EndOfFrameProcesses = new IEnumerator<float>[this.EndOfFrameProcesses.Length + 64 * (int) this._expansions++];
            this.EndOfFramePaused = new bool[this.EndOfFrameProcesses.Length];
            for (int index = 0; index < ofFrameProcesses.Length; ++index)
            {
              this.EndOfFrameProcesses[index] = ofFrameProcesses[index];
              this.EndOfFramePaused[index] = endOfFramePaused[index];
            }
          }
          processIndex.i = this._nextEndOfFrameProcessSlot++;
          this.EndOfFrameProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          this.RunCoroutineSingletonOnInstance(this._EOFPumpWatcher(), "MEC_EOFPumpWatcher");
          return handle;
        case Segment.ManualTimeframe:
          if (this._nextManualTimeframeProcessSlot >= this.ManualTimeframeProcesses.Length)
          {
            IEnumerator<float>[] timeframeProcesses = this.ManualTimeframeProcesses;
            bool[] manualTimeframePaused = this.ManualTimeframePaused;
            this.ManualTimeframeProcesses = new IEnumerator<float>[this.ManualTimeframeProcesses.Length + 64 * (int) this._expansions++];
            this.ManualTimeframePaused = new bool[this.ManualTimeframeProcesses.Length];
            for (int index = 0; index < timeframeProcesses.Length; ++index)
            {
              this.ManualTimeframeProcesses[index] = timeframeProcesses[index];
              this.ManualTimeframePaused[index] = manualTimeframePaused[index];
            }
          }
          processIndex.i = this._nextManualTimeframeProcessSlot++;
          this.ManualTimeframeProcesses[processIndex.i] = coroutine;
          if (tag != null)
            this.AddTag(tag, processIndex);
          if (layer.HasValue)
            this.AddLayer(layer.Value, processIndex);
          this._indexToHandle.Add(processIndex, handle);
          this._handleToIndex.Add(handle, processIndex);
          return handle;
        default:
          return new CoroutineHandle();
      }
    }

    public static int KillCoroutines() => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillCoroutinesOnInstance() : 0;

    public int KillCoroutinesOnInstance()
    {
      int num = this._nextUpdateProcessSlot + this._nextLateUpdateProcessSlot + this._nextFixedUpdateProcessSlot + this._nextSlowUpdateProcessSlot + this._nextRealtimeUpdateProcessSlot + this._nextEditorUpdateProcessSlot + this._nextEditorSlowUpdateProcessSlot + this._nextEndOfFrameProcessSlot + this._nextManualTimeframeProcessSlot;
      this.UpdateProcesses = new IEnumerator<float>[256];
      this.UpdatePaused = new bool[256];
      this.UpdateCoroutines = 0;
      this._nextUpdateProcessSlot = 0;
      this.LateUpdateProcesses = new IEnumerator<float>[8];
      this.LateUpdatePaused = new bool[8];
      this.LateUpdateCoroutines = 0;
      this._nextLateUpdateProcessSlot = 0;
      this.FixedUpdateProcesses = new IEnumerator<float>[64];
      this.FixedUpdatePaused = new bool[64];
      this.FixedUpdateCoroutines = 0;
      this._nextFixedUpdateProcessSlot = 0;
      this.SlowUpdateProcesses = new IEnumerator<float>[64];
      this.SlowUpdatePaused = new bool[64];
      this.SlowUpdateCoroutines = 0;
      this._nextSlowUpdateProcessSlot = 0;
      this.RealtimeUpdateProcesses = new IEnumerator<float>[8];
      this.RealtimeUpdatePaused = new bool[8];
      this.RealtimeUpdateCoroutines = 0;
      this._nextRealtimeUpdateProcessSlot = 0;
      this.EditorUpdateProcesses = new IEnumerator<float>[8];
      this.EditorUpdatePaused = new bool[8];
      this.EditorUpdateCoroutines = 0;
      this._nextEditorUpdateProcessSlot = 0;
      this.EditorSlowUpdateProcesses = new IEnumerator<float>[8];
      this.EditorSlowUpdatePaused = new bool[8];
      this.EditorSlowUpdateCoroutines = 0;
      this._nextEditorSlowUpdateProcessSlot = 0;
      this.EndOfFrameProcesses = new IEnumerator<float>[8];
      this.EndOfFramePaused = new bool[8];
      this.EndOfFrameCoroutines = 0;
      this._nextEndOfFrameProcessSlot = 0;
      this.ManualTimeframeProcesses = new IEnumerator<float>[8];
      this.ManualTimeframePaused = new bool[8];
      this.ManualTimeframeCoroutines = 0;
      this._nextManualTimeframeProcessSlot = 0;
      this._processTags.Clear();
      this._taggedProcesses.Clear();
      this._processLayers.Clear();
      this._layeredProcesses.Clear();
      this._handleToIndex.Clear();
      this._indexToHandle.Clear();
      this._waitingTriggers.Clear();
      this._expansions = (ushort) ((int) this._expansions / 2 + 1);
      this.ResetTimeCountOnInstance();
      return num;
    }

    public static int KillCoroutines(CoroutineHandle handle) => !Timing.ActiveInstances.ContainsKey(handle.Key) ? 0 : Timing.GetInstance(handle.Key).KillCoroutinesOnInstance(handle);

    public int KillCoroutinesOnInstance(CoroutineHandle handle)
    {
      bool flag = false;
      if (this._handleToIndex.ContainsKey(handle))
      {
        if (this._waitingTriggers.ContainsKey(handle))
          this.CloseWaitingProcess(handle);
        flag = this.CoindexExtract(this._handleToIndex[handle]) != null;
        this.RemoveGraffiti(this._handleToIndex[handle]);
      }
      return !flag ? 0 : 1;
    }

    public static int KillCoroutines(string tag) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillCoroutinesOnInstance(tag) : 0;

    public int KillCoroutinesOnInstance(string tag)
    {
      if (tag == null)
        return 0;
      int num = 0;
      while (this._taggedProcesses.ContainsKey(tag))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
        enumerator.MoveNext();
        if (this.CoindexKill(enumerator.Current))
        {
          if (this._waitingTriggers.ContainsKey(this._indexToHandle[enumerator.Current]))
            this.CloseWaitingProcess(this._indexToHandle[enumerator.Current]);
          ++num;
        }
        this.RemoveGraffiti(enumerator.Current);
      }
      return num;
    }

    public static int KillCoroutines(int layer) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillCoroutinesOnInstance(layer) : 0;

    public int KillCoroutinesOnInstance(int layer)
    {
      int num = 0;
      while (this._layeredProcesses.ContainsKey(layer))
      {
        HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
        enumerator.MoveNext();
        if (this.CoindexKill(enumerator.Current))
        {
          if (this._waitingTriggers.ContainsKey(this._indexToHandle[enumerator.Current]))
            this.CloseWaitingProcess(this._indexToHandle[enumerator.Current]);
          ++num;
        }
        this.RemoveGraffiti(enumerator.Current);
      }
      return num;
    }

    public static int KillCoroutines(int layer, string tag) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.KillCoroutinesOnInstance(layer, tag) : 0;

    public int KillCoroutinesOnInstance(int layer, string tag)
    {
      if (tag == null)
        return this.KillCoroutinesOnInstance(layer);
      if (!this._layeredProcesses.ContainsKey(layer) || !this._taggedProcesses.ContainsKey(tag))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (!this.CoindexIsNull(enumerator.Current) && this._layeredProcesses[layer].Contains(enumerator.Current) && this.CoindexKill(enumerator.Current))
        {
          if (this._waitingTriggers.ContainsKey(this._indexToHandle[enumerator.Current]))
            this.CloseWaitingProcess(this._indexToHandle[enumerator.Current]);
          ++num;
          this.RemoveGraffiti(enumerator.Current);
          if (this._taggedProcesses.ContainsKey(tag) && this._layeredProcesses.ContainsKey(layer))
            enumerator = this._taggedProcesses[tag].GetEnumerator();
          else
            break;
        }
      }
      return num;
    }

    public static Timing GetInstance(byte ID) => !Timing.ActiveInstances.ContainsKey(ID) ? (Timing) null : Timing.ActiveInstances[ID];

    public static float WaitForSeconds(float waitTime)
    {
      if (float.IsNaN(waitTime))
        waitTime = 0.0f;
      return Timing.LocalTime + waitTime;
    }

    public float WaitForSecondsOnInstance(float waitTime)
    {
      if (float.IsNaN(waitTime))
        waitTime = 0.0f;
      return (float) this.localTime + waitTime;
    }

    private void UpdateTimeValues(Segment segment)
    {
      switch (segment)
      {
        case Segment.Update:
          this.deltaTime = Time.deltaTime;
          this._lastUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastUpdateTime;
          break;
        case Segment.FixedUpdate:
          this.deltaTime = Time.deltaTime;
          this._lastFixedUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastFixedUpdateTime;
          break;
        case Segment.LateUpdate:
          this.deltaTime = Time.deltaTime;
          this._lastLateUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastLateUpdateTime;
          break;
        case Segment.SlowUpdate:
          this.deltaTime = this._lastSlowUpdateTime != 0.0 ? Time.realtimeSinceStartup - (float) this._lastSlowUpdateTime : this.TimeBetweenSlowUpdateCalls;
          this.localTime = this._lastSlowUpdateTime = (double) Time.realtimeSinceStartup;
          break;
        case Segment.RealtimeUpdate:
          this.deltaTime = Time.unscaledDeltaTime;
          this._lastRealtimeUpdateTime += (double) this.deltaTime;
          this.localTime = this._lastRealtimeUpdateTime;
          break;
        case Segment.EndOfFrame:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastUpdateTime;
          break;
        case Segment.ManualTimeframe:
          this.localTime = this.SetManualTimeframeTime == null ? (double) Time.time : this.SetManualTimeframeTime(this._lastManualTimeframeTime);
          this.deltaTime = (float) (this.localTime - this._lastManualTimeframeTime);
          if ((double) this.deltaTime > (double) Time.maximumDeltaTime)
            this.deltaTime = Time.maximumDeltaTime;
          this._lastManualTimeframeTime = this.localTime;
          break;
      }
    }

    private void SetTimeValues(Segment segment)
    {
      switch (segment)
      {
        case Segment.Update:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastUpdateTime;
          break;
        case Segment.FixedUpdate:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastFixedUpdateTime;
          break;
        case Segment.LateUpdate:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastLateUpdateTime;
          break;
        case Segment.SlowUpdate:
          this.deltaTime = Time.realtimeSinceStartup - (float) this._lastSlowUpdateTime;
          this.localTime = this._lastSlowUpdateTime;
          break;
        case Segment.RealtimeUpdate:
          this.deltaTime = Time.unscaledDeltaTime;
          this.localTime = this._lastRealtimeUpdateTime;
          break;
        case Segment.EndOfFrame:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastUpdateTime;
          break;
        case Segment.ManualTimeframe:
          this.deltaTime = Time.deltaTime;
          this.localTime = this._lastManualTimeframeTime;
          break;
      }
    }

    private double GetSegmentTime(Segment segment)
    {
      switch (segment)
      {
        case Segment.Update:
          return this._lastUpdateTime;
        case Segment.FixedUpdate:
          return this._lastFixedUpdateTime;
        case Segment.LateUpdate:
          return this._lastLateUpdateTime;
        case Segment.SlowUpdate:
          return this._lastSlowUpdateTime;
        case Segment.RealtimeUpdate:
          return this._lastRealtimeUpdateTime;
        case Segment.EditorUpdate:
          return this._lastEditorUpdateTime;
        case Segment.EditorSlowUpdate:
          return this._lastEditorSlowUpdateTime;
        case Segment.EndOfFrame:
          return this._lastUpdateTime;
        case Segment.ManualTimeframe:
          return this._lastManualTimeframeTime;
        default:
          return 0.0;
      }
    }

    public void ResetTimeCountOnInstance()
    {
      this.localTime = 0.0;
      this._lastUpdateTime = 0.0;
      this._lastLateUpdateTime = 0.0;
      this._lastFixedUpdateTime = 0.0;
      this._lastRealtimeUpdateTime = 0.0;
      this._EOFPumpRan = false;
    }

    public static int PauseCoroutines() => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.PauseCoroutinesOnInstance() : 0;

    public int PauseCoroutinesOnInstance()
    {
      int num = 0;
      for (int index = 0; index < this._nextUpdateProcessSlot; ++index)
      {
        if (!this.UpdatePaused[index] && this.UpdateProcesses[index] != null)
        {
          this.UpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextLateUpdateProcessSlot; ++index)
      {
        if (!this.LateUpdatePaused[index] && this.LateUpdateProcesses[index] != null)
        {
          this.LateUpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextFixedUpdateProcessSlot; ++index)
      {
        if (!this.FixedUpdatePaused[index] && this.FixedUpdateProcesses[index] != null)
        {
          this.FixedUpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextSlowUpdateProcessSlot; ++index)
      {
        if (!this.SlowUpdatePaused[index] && this.SlowUpdateProcesses[index] != null)
        {
          this.SlowUpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextRealtimeUpdateProcessSlot; ++index)
      {
        if (!this.RealtimeUpdatePaused[index] && this.RealtimeUpdateProcesses[index] != null)
        {
          this.RealtimeUpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextEditorUpdateProcessSlot; ++index)
      {
        if (!this.EditorUpdatePaused[index] && this.EditorUpdateProcesses[index] != null)
        {
          this.EditorUpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextEditorSlowUpdateProcessSlot; ++index)
      {
        if (!this.EditorSlowUpdatePaused[index] && this.EditorSlowUpdateProcesses[index] != null)
        {
          this.EditorSlowUpdatePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextEndOfFrameProcessSlot; ++index)
      {
        if (!this.EndOfFramePaused[index] && this.EndOfFrameProcesses[index] != null)
        {
          this.EndOfFramePaused[index] = true;
          ++num;
        }
      }
      for (int index = 0; index < this._nextManualTimeframeProcessSlot; ++index)
      {
        if (!this.ManualTimeframePaused[index] && this.ManualTimeframeProcesses[index] != null)
        {
          this.ManualTimeframePaused[index] = true;
          ++num;
        }
      }
      return num;
    }

    public static int PauseCoroutines(string tag) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.PauseCoroutinesOnInstance(tag) : 0;

    public int PauseCoroutinesOnInstance(string tag)
    {
      if (tag == null || !this._taggedProcesses.ContainsKey(tag))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (!this.CoindexIsNull(enumerator.Current) && !this.CoindexSetPause(enumerator.Current))
          ++num;
      }
      return num;
    }

    public static int PauseCoroutines(int layer) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.PauseCoroutinesOnInstance(layer) : 0;

    public int PauseCoroutinesOnInstance(int layer)
    {
      if (!this._layeredProcesses.ContainsKey(layer))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (!this.CoindexIsNull(enumerator.Current) && !this.CoindexSetPause(enumerator.Current))
          ++num;
      }
      return num;
    }

    public static int PauseCoroutines(int layer, string tag) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.PauseCoroutinesOnInstance(layer, tag) : 0;

    public int PauseCoroutinesOnInstance(int layer, string tag)
    {
      if (tag == null)
        return this.PauseCoroutinesOnInstance(layer);
      if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (this._processLayers.ContainsKey(enumerator.Current) && this._processLayers[enumerator.Current] == layer && !this.CoindexIsNull(enumerator.Current) && !this.CoindexSetPause(enumerator.Current))
          ++num;
      }
      return num;
    }

    public static int PauseCoroutines(CoroutineHandle handle) => !Timing.ActiveInstances.ContainsKey(handle.Key) ? 0 : Timing.GetInstance(handle.Key).PauseCoroutinesOnInstance(handle);

    public int PauseCoroutinesOnInstance(CoroutineHandle handle) => !this._handleToIndex.ContainsKey(handle) || this.CoindexIsNull(this._handleToIndex[handle]) || this.CoindexSetPause(this._handleToIndex[handle]) ? 0 : 1;

    public static int ResumeCoroutines() => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.ResumeCoroutinesOnInstance() : 0;

    public int ResumeCoroutinesOnInstance()
    {
      int num1 = 0;
      for (int index = 0; index < this._nextUpdateProcessSlot; ++index)
      {
        if (this.UpdatePaused[index] && this.UpdateProcesses[index] != null)
        {
          this.UpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextLateUpdateProcessSlot; ++index)
      {
        if (this.LateUpdatePaused[index] && this.LateUpdateProcesses[index] != null)
        {
          this.LateUpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextFixedUpdateProcessSlot; ++index)
      {
        if (this.FixedUpdatePaused[index] && this.FixedUpdateProcesses[index] != null)
        {
          this.FixedUpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextSlowUpdateProcessSlot; ++index)
      {
        if (this.SlowUpdatePaused[index] && this.SlowUpdateProcesses[index] != null)
        {
          this.SlowUpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextRealtimeUpdateProcessSlot; ++index)
      {
        if (this.RealtimeUpdatePaused[index] && this.RealtimeUpdateProcesses[index] != null)
        {
          this.RealtimeUpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextEditorUpdateProcessSlot; ++index)
      {
        if (this.EditorUpdatePaused[index] && this.EditorUpdateProcesses[index] != null)
        {
          this.EditorUpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextEditorSlowUpdateProcessSlot; ++index)
      {
        if (this.EditorSlowUpdatePaused[index] && this.EditorSlowUpdateProcesses[index] != null)
        {
          this.EditorSlowUpdatePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextEndOfFrameProcessSlot; ++index)
      {
        if (this.EndOfFramePaused[index] && this.EndOfFrameProcesses[index] != null)
        {
          this.EndOfFramePaused[index] = false;
          ++num1;
        }
      }
      for (int index = 0; index < this._nextManualTimeframeProcessSlot; ++index)
      {
        if (this.ManualTimeframePaused[index] && this.ManualTimeframeProcesses[index] != null)
        {
          this.ManualTimeframePaused[index] = false;
          ++num1;
        }
      }
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator1 = this._waitingTriggers.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        int num2 = 0;
        KeyValuePair<CoroutineHandle, HashSet<Timing.ProcessData>> current = enumerator1.Current;
        HashSet<Timing.ProcessData>.Enumerator enumerator2 = current.Value.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          if (this._handleToIndex.ContainsKey(enumerator2.Current.Handle) && !this.CoindexIsNull(this._handleToIndex[enumerator2.Current.Handle]))
          {
            this.CoindexSetPause(this._handleToIndex[enumerator2.Current.Handle]);
            ++num2;
          }
          else
          {
            current = enumerator1.Current;
            current.Value.Remove(enumerator2.Current);
            num2 = 0;
            current = enumerator1.Current;
            enumerator2 = current.Value.GetEnumerator();
          }
        }
        num1 -= num2;
      }
      return num1;
    }

    public static int ResumeCoroutines(string tag) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.ResumeCoroutinesOnInstance(tag) : 0;

    public int ResumeCoroutinesOnInstance(string tag)
    {
      if (tag == null || !this._taggedProcesses.ContainsKey(tag))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator1 = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator1.MoveNext())
      {
        if (!this.CoindexIsNull(enumerator1.Current) && this.CoindexSetPause(enumerator1.Current, false))
          ++num;
      }
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator2 = this._waitingTriggers.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        HashSet<Timing.ProcessData>.Enumerator enumerator3 = enumerator2.Current.Value.GetEnumerator();
        while (enumerator3.MoveNext())
        {
          if (this._handleToIndex.ContainsKey(enumerator3.Current.Handle) && !this.CoindexIsNull(this._handleToIndex[enumerator3.Current.Handle]) && !this.CoindexSetPause(this._handleToIndex[enumerator3.Current.Handle]))
            --num;
        }
      }
      return num;
    }

    public static int ResumeCoroutines(int layer) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.ResumeCoroutinesOnInstance(layer) : 0;

    public int ResumeCoroutinesOnInstance(int layer)
    {
      if (!this._layeredProcesses.ContainsKey(layer))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator1 = this._layeredProcesses[layer].GetEnumerator();
      while (enumerator1.MoveNext())
      {
        if (!this.CoindexIsNull(enumerator1.Current) && this.CoindexSetPause(enumerator1.Current, false))
          ++num;
      }
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator2 = this._waitingTriggers.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        HashSet<Timing.ProcessData>.Enumerator enumerator3 = enumerator2.Current.Value.GetEnumerator();
        while (enumerator3.MoveNext())
        {
          if (this._handleToIndex.ContainsKey(enumerator3.Current.Handle) && !this.CoindexIsNull(this._handleToIndex[enumerator3.Current.Handle]) && !this.CoindexSetPause(this._handleToIndex[enumerator3.Current.Handle]))
            --num;
        }
      }
      return num;
    }

    public static int ResumeCoroutines(int layer, string tag) => !((UnityEngine.Object) Timing._instance == (UnityEngine.Object) null) ? Timing._instance.ResumeCoroutinesOnInstance(layer, tag) : 0;

    public int ResumeCoroutinesOnInstance(int layer, string tag)
    {
      if (tag == null)
        return this.ResumeCoroutinesOnInstance(layer);
      if (!this._layeredProcesses.ContainsKey(layer) || !this._taggedProcesses.ContainsKey(tag))
        return 0;
      int num = 0;
      HashSet<Timing.ProcessIndex>.Enumerator enumerator1 = this._taggedProcesses[tag].GetEnumerator();
      while (enumerator1.MoveNext())
      {
        if (!this.CoindexIsNull(enumerator1.Current) && this._layeredProcesses[layer].Contains(enumerator1.Current) && this.CoindexSetPause(enumerator1.Current, false))
          ++num;
      }
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator2 = this._waitingTriggers.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        HashSet<Timing.ProcessData>.Enumerator enumerator3 = enumerator2.Current.Value.GetEnumerator();
        while (enumerator3.MoveNext())
        {
          if (this._handleToIndex.ContainsKey(enumerator3.Current.Handle) && !this.CoindexIsNull(this._handleToIndex[enumerator3.Current.Handle]) && !this.CoindexSetPause(this._handleToIndex[enumerator3.Current.Handle]))
            --num;
        }
      }
      return num;
    }

    public static int ResumeCoroutines(CoroutineHandle handle) => !Timing.ActiveInstances.ContainsKey(handle.Key) ? 0 : Timing.GetInstance(handle.Key).ResumeCoroutinesOnInstance(handle);

    public int ResumeCoroutinesOnInstance(CoroutineHandle handle)
    {
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator1 = this._waitingTriggers.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        HashSet<Timing.ProcessData>.Enumerator enumerator2 = enumerator1.Current.Value.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          if (enumerator2.Current.Handle == handle)
            return 0;
        }
      }
      return !this._handleToIndex.ContainsKey(handle) || this.CoindexIsNull(this._handleToIndex[handle]) || !this.CoindexSetPause(this._handleToIndex[handle], false) ? 0 : 1;
    }

    public static string GetTag(CoroutineHandle handle)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      return !((UnityEngine.Object) instance != (UnityEngine.Object) null) || !instance._handleToIndex.ContainsKey(handle) || !instance._processTags.ContainsKey(instance._handleToIndex[handle]) ? (string) null : instance._processTags[instance._handleToIndex[handle]];
    }

    public static int? GetLayer(CoroutineHandle handle)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      return !((UnityEngine.Object) instance != (UnityEngine.Object) null) || !instance._handleToIndex.ContainsKey(handle) || !instance._processLayers.ContainsKey(instance._handleToIndex[handle]) ? new int?() : new int?(instance._processLayers[instance._handleToIndex[handle]]);
    }

    public static Segment GetSegment(CoroutineHandle handle)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      return !((UnityEngine.Object) instance != (UnityEngine.Object) null) || !instance._handleToIndex.ContainsKey(handle) ? Segment.Invalid : instance._handleToIndex[handle].seg;
    }

    public static bool SetTag(CoroutineHandle handle, string newTag, bool overwriteExisting = true)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]) || !overwriteExisting && instance._processTags.ContainsKey(instance._handleToIndex[handle]))
        return false;
      if (newTag == null)
      {
        instance.RemoveTag(instance._handleToIndex[handle]);
        return true;
      }
      if (instance._processTags.ContainsKey(instance._handleToIndex[handle]))
      {
        if (instance._taggedProcesses[instance._processTags[instance._handleToIndex[handle]]].Count <= 1)
          instance._taggedProcesses.Remove(instance._processTags[instance._handleToIndex[handle]]);
        else
          instance._taggedProcesses[instance._processTags[instance._handleToIndex[handle]]].Remove(instance._handleToIndex[handle]);
        instance._processTags[instance._handleToIndex[handle]] = newTag;
      }
      else
        instance._processTags.Add(instance._handleToIndex[handle], newTag);
      if (instance._taggedProcesses.ContainsKey(newTag))
        instance._taggedProcesses[newTag].Add(instance._handleToIndex[handle]);
      else
        instance._taggedProcesses.Add(newTag, new HashSet<Timing.ProcessIndex>()
        {
          instance._handleToIndex[handle]
        });
      return true;
    }

    public static bool SetLayer(CoroutineHandle handle, int newLayer, bool overwriteExisting = true)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]) || !overwriteExisting && instance._processLayers.ContainsKey(instance._handleToIndex[handle]))
        return false;
      if (instance._processLayers.ContainsKey(instance._handleToIndex[handle]))
      {
        if (instance._layeredProcesses[instance._processLayers[instance._handleToIndex[handle]]].Count <= 1)
          instance._layeredProcesses.Remove(instance._processLayers[instance._handleToIndex[handle]]);
        else
          instance._layeredProcesses[instance._processLayers[instance._handleToIndex[handle]]].Remove(instance._handleToIndex[handle]);
        instance._processLayers[instance._handleToIndex[handle]] = newLayer;
      }
      else
        instance._processLayers.Add(instance._handleToIndex[handle], newLayer);
      if (instance._layeredProcesses.ContainsKey(newLayer))
        instance._layeredProcesses[newLayer].Add(instance._handleToIndex[handle]);
      else
        instance._layeredProcesses.Add(newLayer, new HashSet<Timing.ProcessIndex>()
        {
          instance._handleToIndex[handle]
        });
      return true;
    }

    public static bool SetSegment(CoroutineHandle handle, Segment newSegment)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]))
        return false;
      instance.RunCoroutineInternal(instance.CoindexExtract(instance._handleToIndex[handle]), newSegment, instance._processLayers.ContainsKey(instance._handleToIndex[handle]) ? new int?(instance._processLayers[instance._handleToIndex[handle]]) : new int?(), instance._processTags.ContainsKey(instance._handleToIndex[handle]) ? instance._processTags[instance._handleToIndex[handle]] : (string) null, handle, false);
      return true;
    }

    public static bool RemoveTag(CoroutineHandle handle) => Timing.SetTag(handle, (string) null);

    public static bool RemoveLayer(CoroutineHandle handle)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]))
        return false;
      instance.RemoveLayer(instance._handleToIndex[handle]);
      return true;
    }

    public static bool IsRunning(CoroutineHandle handle)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      return (UnityEngine.Object) instance != (UnityEngine.Object) null && instance._handleToIndex.ContainsKey(handle) && !instance.CoindexIsNull(instance._handleToIndex[handle]);
    }

    public static bool IsPaused(CoroutineHandle handle)
    {
      Timing instance = Timing.GetInstance(handle.Key);
      return (UnityEngine.Object) instance != (UnityEngine.Object) null && instance._handleToIndex.ContainsKey(handle) && !instance.CoindexIsNull(instance._handleToIndex[handle]) && !instance.CoindexIsPaused(instance._handleToIndex[handle]);
    }

    private void AddTag(string tag, Timing.ProcessIndex coindex)
    {
      this._processTags.Add(coindex, tag);
      if (this._taggedProcesses.ContainsKey(tag))
        this._taggedProcesses[tag].Add(coindex);
      else
        this._taggedProcesses.Add(tag, new HashSet<Timing.ProcessIndex>()
        {
          coindex
        });
    }

    private void AddLayer(int layer, Timing.ProcessIndex coindex)
    {
      this._processLayers.Add(coindex, layer);
      if (this._layeredProcesses.ContainsKey(layer))
        this._layeredProcesses[layer].Add(coindex);
      else
        this._layeredProcesses.Add(layer, new HashSet<Timing.ProcessIndex>()
        {
          coindex
        });
    }

    private void RemoveTag(Timing.ProcessIndex coindex)
    {
      if (!this._processTags.ContainsKey(coindex))
        return;
      if (this._taggedProcesses[this._processTags[coindex]].Count > 1)
        this._taggedProcesses[this._processTags[coindex]].Remove(coindex);
      else
        this._taggedProcesses.Remove(this._processTags[coindex]);
      this._processTags.Remove(coindex);
    }

    private void RemoveLayer(Timing.ProcessIndex coindex)
    {
      if (!this._processLayers.ContainsKey(coindex))
        return;
      if (this._layeredProcesses[this._processLayers[coindex]].Count > 1)
        this._layeredProcesses[this._processLayers[coindex]].Remove(coindex);
      else
        this._layeredProcesses.Remove(this._processLayers[coindex]);
      this._processLayers.Remove(coindex);
    }

    private void RemoveGraffiti(Timing.ProcessIndex coindex)
    {
      if (this._processLayers.ContainsKey(coindex))
      {
        if (this._layeredProcesses[this._processLayers[coindex]].Count > 1)
          this._layeredProcesses[this._processLayers[coindex]].Remove(coindex);
        else
          this._layeredProcesses.Remove(this._processLayers[coindex]);
        this._processLayers.Remove(coindex);
      }
      if (this._processTags.ContainsKey(coindex))
      {
        if (this._taggedProcesses[this._processTags[coindex]].Count > 1)
          this._taggedProcesses[this._processTags[coindex]].Remove(coindex);
        else
          this._taggedProcesses.Remove(this._processTags[coindex]);
        this._processTags.Remove(coindex);
      }
      if (!this._indexToHandle.ContainsKey(coindex))
        return;
      this._handleToIndex.Remove(this._indexToHandle[coindex]);
      this._indexToHandle.Remove(coindex);
    }

    private void MoveGraffiti(Timing.ProcessIndex coindexFrom, Timing.ProcessIndex coindexTo)
    {
      this.RemoveGraffiti(coindexTo);
      int key1;
      if (this._processLayers.TryGetValue(coindexFrom, out key1))
      {
        this._layeredProcesses[key1].Remove(coindexFrom);
        this._layeredProcesses[key1].Add(coindexTo);
        this._processLayers.Add(coindexTo, key1);
        this._processLayers.Remove(coindexFrom);
      }
      string key2;
      if (this._processTags.TryGetValue(coindexFrom, out key2))
      {
        this._taggedProcesses[key2].Remove(coindexFrom);
        this._taggedProcesses[key2].Add(coindexTo);
        this._processTags.Add(coindexTo, key2);
        this._processTags.Remove(coindexFrom);
      }
      this._handleToIndex[this._indexToHandle[coindexFrom]] = coindexTo;
      this._indexToHandle.Add(coindexTo, this._indexToHandle[coindexFrom]);
      this._indexToHandle.Remove(coindexFrom);
    }

    private IEnumerator<float> CoindexExtract(Timing.ProcessIndex coindex)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          IEnumerator<float> updateProcess = this.UpdateProcesses[coindex.i];
          this.UpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return updateProcess;
        case Segment.FixedUpdate:
          IEnumerator<float> fixedUpdateProcess = this.FixedUpdateProcesses[coindex.i];
          this.FixedUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return fixedUpdateProcess;
        case Segment.LateUpdate:
          IEnumerator<float> lateUpdateProcess = this.LateUpdateProcesses[coindex.i];
          this.LateUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return lateUpdateProcess;
        case Segment.SlowUpdate:
          IEnumerator<float> slowUpdateProcess1 = this.SlowUpdateProcesses[coindex.i];
          this.SlowUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return slowUpdateProcess1;
        case Segment.RealtimeUpdate:
          IEnumerator<float> realtimeUpdateProcess = this.RealtimeUpdateProcesses[coindex.i];
          this.RealtimeUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return realtimeUpdateProcess;
        case Segment.EditorUpdate:
          IEnumerator<float> editorUpdateProcess = this.EditorUpdateProcesses[coindex.i];
          this.EditorUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return editorUpdateProcess;
        case Segment.EditorSlowUpdate:
          IEnumerator<float> slowUpdateProcess2 = this.EditorSlowUpdateProcesses[coindex.i];
          this.EditorSlowUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return slowUpdateProcess2;
        case Segment.EndOfFrame:
          IEnumerator<float> endOfFrameProcess = this.EndOfFrameProcesses[coindex.i];
          this.EndOfFrameProcesses[coindex.i] = (IEnumerator<float>) null;
          return endOfFrameProcess;
        case Segment.ManualTimeframe:
          IEnumerator<float> timeframeProcess = this.ManualTimeframeProcesses[coindex.i];
          this.ManualTimeframeProcesses[coindex.i] = (IEnumerator<float>) null;
          return timeframeProcess;
        default:
          return (IEnumerator<float>) null;
      }
    }

    private bool CoindexIsNull(Timing.ProcessIndex coindex)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          return this.UpdateProcesses[coindex.i] == null;
        case Segment.FixedUpdate:
          return this.FixedUpdateProcesses[coindex.i] == null;
        case Segment.LateUpdate:
          return this.LateUpdateProcesses[coindex.i] == null;
        case Segment.SlowUpdate:
          return this.SlowUpdateProcesses[coindex.i] == null;
        case Segment.RealtimeUpdate:
          return this.RealtimeUpdateProcesses[coindex.i] == null;
        case Segment.EditorUpdate:
          return this.EditorUpdateProcesses[coindex.i] == null;
        case Segment.EditorSlowUpdate:
          return this.EditorSlowUpdateProcesses[coindex.i] == null;
        case Segment.EndOfFrame:
          return this.EndOfFrameProcesses[coindex.i] == null;
        case Segment.ManualTimeframe:
          return this.ManualTimeframeProcesses[coindex.i] == null;
        default:
          return true;
      }
    }

    private IEnumerator<float> CoindexPeek(Timing.ProcessIndex coindex)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          return this.UpdateProcesses[coindex.i];
        case Segment.FixedUpdate:
          return this.FixedUpdateProcesses[coindex.i];
        case Segment.LateUpdate:
          return this.LateUpdateProcesses[coindex.i];
        case Segment.SlowUpdate:
          return this.SlowUpdateProcesses[coindex.i];
        case Segment.RealtimeUpdate:
          return this.RealtimeUpdateProcesses[coindex.i];
        case Segment.EditorUpdate:
          return this.EditorUpdateProcesses[coindex.i];
        case Segment.EditorSlowUpdate:
          return this.EditorSlowUpdateProcesses[coindex.i];
        case Segment.EndOfFrame:
          return this.EndOfFrameProcesses[coindex.i];
        case Segment.ManualTimeframe:
          return this.ManualTimeframeProcesses[coindex.i];
        default:
          return (IEnumerator<float>) null;
      }
    }

    private bool CoindexKill(Timing.ProcessIndex coindex)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          int num1 = this.UpdateProcesses[coindex.i] != null ? 1 : 0;
          this.UpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num1 != 0;
        case Segment.FixedUpdate:
          int num2 = this.FixedUpdateProcesses[coindex.i] != null ? 1 : 0;
          this.FixedUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num2 != 0;
        case Segment.LateUpdate:
          int num3 = this.LateUpdateProcesses[coindex.i] != null ? 1 : 0;
          this.LateUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num3 != 0;
        case Segment.SlowUpdate:
          int num4 = this.SlowUpdateProcesses[coindex.i] != null ? 1 : 0;
          this.SlowUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num4 != 0;
        case Segment.RealtimeUpdate:
          int num5 = this.RealtimeUpdateProcesses[coindex.i] != null ? 1 : 0;
          this.RealtimeUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num5 != 0;
        case Segment.EditorUpdate:
          int num6 = this.UpdateProcesses[coindex.i] != null ? 1 : 0;
          this.EditorUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num6 != 0;
        case Segment.EditorSlowUpdate:
          int num7 = this.EditorSlowUpdateProcesses[coindex.i] != null ? 1 : 0;
          this.EditorSlowUpdateProcesses[coindex.i] = (IEnumerator<float>) null;
          return num7 != 0;
        case Segment.EndOfFrame:
          int num8 = this.EndOfFrameProcesses[coindex.i] != null ? 1 : 0;
          this.EndOfFrameProcesses[coindex.i] = (IEnumerator<float>) null;
          return num8 != 0;
        case Segment.ManualTimeframe:
          int num9 = this.ManualTimeframeProcesses[coindex.i] != null ? 1 : 0;
          this.ManualTimeframeProcesses[coindex.i] = (IEnumerator<float>) null;
          return num9 != 0;
        default:
          return false;
      }
    }

    private bool CoindexSetPause(Timing.ProcessIndex coindex, bool newPausedState = true)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          int num1 = this.UpdatePaused[coindex.i] ? 1 : 0;
          this.UpdatePaused[coindex.i] = newPausedState;
          return num1 != 0;
        case Segment.FixedUpdate:
          int num2 = this.FixedUpdatePaused[coindex.i] ? 1 : 0;
          this.FixedUpdatePaused[coindex.i] = newPausedState;
          return num2 != 0;
        case Segment.LateUpdate:
          int num3 = this.LateUpdatePaused[coindex.i] ? 1 : 0;
          this.LateUpdatePaused[coindex.i] = newPausedState;
          return num3 != 0;
        case Segment.SlowUpdate:
          int num4 = this.SlowUpdatePaused[coindex.i] ? 1 : 0;
          this.SlowUpdatePaused[coindex.i] = newPausedState;
          return num4 != 0;
        case Segment.RealtimeUpdate:
          int num5 = this.RealtimeUpdatePaused[coindex.i] ? 1 : 0;
          this.RealtimeUpdatePaused[coindex.i] = newPausedState;
          return num5 != 0;
        case Segment.EditorUpdate:
          int num6 = this.EditorUpdatePaused[coindex.i] ? 1 : 0;
          this.EditorUpdatePaused[coindex.i] = newPausedState;
          return num6 != 0;
        case Segment.EditorSlowUpdate:
          int num7 = this.EditorSlowUpdatePaused[coindex.i] ? 1 : 0;
          this.EditorSlowUpdatePaused[coindex.i] = newPausedState;
          return num7 != 0;
        case Segment.EndOfFrame:
          int num8 = this.EndOfFramePaused[coindex.i] ? 1 : 0;
          this.EndOfFramePaused[coindex.i] = newPausedState;
          return num8 != 0;
        case Segment.ManualTimeframe:
          int num9 = this.ManualTimeframePaused[coindex.i] ? 1 : 0;
          this.ManualTimeframePaused[coindex.i] = newPausedState;
          return num9 != 0;
        default:
          return false;
      }
    }

    private bool CoindexIsPaused(Timing.ProcessIndex coindex)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          return this.UpdatePaused[coindex.i];
        case Segment.FixedUpdate:
          return this.FixedUpdatePaused[coindex.i];
        case Segment.LateUpdate:
          return this.LateUpdatePaused[coindex.i];
        case Segment.SlowUpdate:
          return this.SlowUpdatePaused[coindex.i];
        case Segment.RealtimeUpdate:
          return this.RealtimeUpdatePaused[coindex.i];
        case Segment.EditorUpdate:
          return this.EditorUpdatePaused[coindex.i];
        case Segment.EditorSlowUpdate:
          return this.EditorSlowUpdatePaused[coindex.i];
        case Segment.EndOfFrame:
          return this.EndOfFramePaused[coindex.i];
        case Segment.ManualTimeframe:
          return this.ManualTimeframePaused[coindex.i];
        default:
          return false;
      }
    }

    private void CoindexReplace(Timing.ProcessIndex coindex, IEnumerator<float> replacement)
    {
      switch (coindex.seg)
      {
        case Segment.Update:
          this.UpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.FixedUpdate:
          this.FixedUpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.LateUpdate:
          this.LateUpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.SlowUpdate:
          this.SlowUpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.RealtimeUpdate:
          this.RealtimeUpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.EditorUpdate:
          this.EditorUpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.EditorSlowUpdate:
          this.EditorSlowUpdateProcesses[coindex.i] = replacement;
          break;
        case Segment.EndOfFrame:
          this.EndOfFrameProcesses[coindex.i] = replacement;
          break;
        case Segment.ManualTimeframe:
          this.ManualTimeframeProcesses[coindex.i] = replacement;
          break;
      }
    }

    public static float WaitUntilDone(IEnumerator<float> newCoroutine) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine), true);

    public static float WaitUntilDone(IEnumerator<float> newCoroutine, string tag) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, tag), true);

    public static float WaitUntilDone(IEnumerator<float> newCoroutine, int layer) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, layer), true);

    public static float WaitUntilDone(IEnumerator<float> newCoroutine, int layer, string tag) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, layer, tag), true);

    public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment), true);

    public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment, string tag) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment, tag), true);

    public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment, int layer) => Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment, layer), true);

    public static float WaitUntilDone(
      IEnumerator<float> newCoroutine,
      Segment segment,
      int layer,
      string tag)
    {
      return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment, layer, tag), true);
    }

    public static float WaitUntilDone(CoroutineHandle otherCoroutine) => Timing.WaitUntilDone(otherCoroutine, true);

    public static float WaitUntilDone(CoroutineHandle otherCoroutine, bool warnOnIssue)
    {
      Timing inst = Timing.GetInstance(otherCoroutine.Key);
      if ((UnityEngine.Object) inst != (UnityEngine.Object) null && inst._handleToIndex.ContainsKey(otherCoroutine))
      {
        if (inst.CoindexIsNull(inst._handleToIndex[otherCoroutine]))
          return 0.0f;
        if (!inst._waitingTriggers.ContainsKey(otherCoroutine))
        {
          inst.CoindexReplace(inst._handleToIndex[otherCoroutine], inst._StartWhenDone(otherCoroutine, inst.CoindexPeek(inst._handleToIndex[otherCoroutine])));
          inst._waitingTriggers.Add(otherCoroutine, new HashSet<Timing.ProcessData>());
        }
        Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
        {
          if (handle == otherCoroutine)
          {
            if (warnOnIssue)
              Debug.LogWarning((object) "A coroutine attempted to wait for itself.");
            return coptr;
          }
          if ((int) handle.Key != (int) otherCoroutine.Key)
          {
            if (warnOnIssue)
              Debug.LogWarning((object) "A coroutine attempted to wait for a coroutine running on a different MEC instance.");
            return coptr;
          }
          inst._waitingTriggers[otherCoroutine].Add(new Timing.ProcessData()
          {
            Handle = handle,
            PauseTime = (double) coptr.Current > inst.GetSegmentTime(inst._handleToIndex[handle].seg) ? coptr.Current - (float) inst.GetSegmentTime(inst._handleToIndex[handle].seg) : 0.0f
          });
          inst.CoindexSetPause(inst._handleToIndex[handle]);
          return coptr;
        });
        return float.NaN;
      }
      if (warnOnIssue)
        Debug.LogWarning((object) ("WaitUntilDone cannot hold: The coroutine handle that was passed in is invalid.\n" + otherCoroutine.ToString()));
      return 0.0f;
    }

    private IEnumerator<float> _StartWhenDone(CoroutineHandle handle, IEnumerator<float> proc)
    {
      if (this._waitingTriggers.ContainsKey(handle))
      {
        try
        {
          if ((double) proc.Current > this.localTime)
            yield return proc.Current;
          while (proc.MoveNext())
            yield return proc.Current;
        }
        finally
        {
          this.CloseWaitingProcess(handle);
        }
      }
    }

    private void CloseWaitingProcess(CoroutineHandle handle)
    {
      if (!this._waitingTriggers.ContainsKey(handle))
        return;
      HashSet<Timing.ProcessData>.Enumerator enumerator = this._waitingTriggers[handle].GetEnumerator();
      this._waitingTriggers.Remove(handle);
      while (enumerator.MoveNext())
      {
        if (this._handleToIndex.ContainsKey(enumerator.Current.Handle))
        {
          Timing.ProcessIndex coindex = this._handleToIndex[enumerator.Current.Handle];
          if ((double) enumerator.Current.PauseTime > 0.0)
            this.CoindexReplace(coindex, Timing._InjectDelay(this.CoindexPeek(coindex), (float) this.GetSegmentTime(coindex.seg) + enumerator.Current.PauseTime));
          this.CoindexSetPause(coindex, false);
        }
      }
    }

    public static float WaitUntilDone(UnityWebRequest www)
    {
      if (www == null || www.isDone)
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) => Timing._StartWhenDone(www, coptr));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      UnityWebRequest www,
      IEnumerator<float> pausedProc)
    {
      while (!www.isDone)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((_param1, _param2) => pausedProc);
      yield return float.NaN;
    }

    public static float WaitUntilDone(AsyncOperation operation)
    {
      if (operation == null || operation.isDone)
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) => Timing._StartWhenDone(operation, coptr));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      AsyncOperation operation,
      IEnumerator<float> pausedProc)
    {
      while (!operation.isDone)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((_param1, _param2) => pausedProc);
      yield return float.NaN;
    }

    public static float WaitUntilDone(CustomYieldInstruction operation)
    {
      if (operation == null || !operation.keepWaiting)
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) => Timing._StartWhenDone(operation, coptr));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      CustomYieldInstruction operation,
      IEnumerator<float> pausedProc)
    {
      while (operation.keepWaiting)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((_param1, _param2) => pausedProc);
      yield return float.NaN;
    }

    public static float WaitUntilTrue(Func<bool> evaluatorFunc)
    {
      if (evaluatorFunc == null || evaluatorFunc())
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) => Timing._StartWhenDone(evaluatorFunc, false, coptr));
      return float.NaN;
    }

    public static float WaitUntilFalse(Func<bool> evaluatorFunc)
    {
      if (evaluatorFunc == null || !evaluatorFunc())
        return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) => Timing._StartWhenDone(evaluatorFunc, true, coptr));
      return float.NaN;
    }

    private static IEnumerator<float> _StartWhenDone(
      Func<bool> evaluatorFunc,
      bool continueOn,
      IEnumerator<float> pausedProc)
    {
      while (evaluatorFunc() == continueOn)
        yield return 0.0f;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((_param1, _param2) => pausedProc);
      yield return float.NaN;
    }

    private static IEnumerator<float> _InjectDelay(IEnumerator<float> proc, float returnAt)
    {
      yield return returnAt;
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((_param1, _param2) => proc);
      yield return float.NaN;
    }

    public bool LockCoroutine(CoroutineHandle coroutine, CoroutineHandle key)
    {
      if ((int) coroutine.Key != (int) this._instanceID || key == new CoroutineHandle() || key.Key != (byte) 0)
        return false;
      if (!this._waitingTriggers.ContainsKey(key))
        this._waitingTriggers.Add(key, new HashSet<Timing.ProcessData>());
      this._waitingTriggers[key].Add(new Timing.ProcessData()
      {
        Handle = coroutine
      });
      this.CoindexSetPause(this._handleToIndex[coroutine]);
      return true;
    }

    public bool UnlockCoroutine(CoroutineHandle coroutine, CoroutineHandle key)
    {
      if ((int) coroutine.Key != (int) this._instanceID || key == new CoroutineHandle() || !this._handleToIndex.ContainsKey(coroutine) || !this._waitingTriggers.ContainsKey(key))
        return false;
      Timing.ProcessData processData = new Timing.ProcessData()
      {
        Handle = coroutine
      };
      this._waitingTriggers[key].Remove(processData);
      bool newPausedState = false;
      Dictionary<CoroutineHandle, HashSet<Timing.ProcessData>>.Enumerator enumerator = this._waitingTriggers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.Value.Contains(processData))
          newPausedState = true;
      }
      this.CoindexSetPause(this._handleToIndex[coroutine], newPausedState);
      return true;
    }

    public static float SwitchCoroutine(Segment newSegment)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing instance = Timing.GetInstance(handle.Key);
        Timing.ProcessIndex key = instance._handleToIndex[handle];
        instance.RunCoroutineInternal(coptr, newSegment, instance._processLayers.ContainsKey(key) ? new int?(instance._processLayers[key]) : new int?(), instance._processTags.ContainsKey(key) ? instance._processTags[key] : (string) null, handle, false);
        return (IEnumerator<float>) null;
      });
      return float.NaN;
    }

    public static float SwitchCoroutine(Segment newSegment, string newTag)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing instance = Timing.GetInstance(handle.Key);
        Timing.ProcessIndex key = instance._handleToIndex[handle];
        instance.RunCoroutineInternal(coptr, newSegment, instance._processLayers.ContainsKey(key) ? new int?(instance._processLayers[key]) : new int?(), newTag, handle, false);
        return (IEnumerator<float>) null;
      });
      return float.NaN;
    }

    public static float SwitchCoroutine(Segment newSegment, int newLayer)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing instance = Timing.GetInstance(handle.Key);
        Timing.ProcessIndex key = instance._handleToIndex[handle];
        instance.RunCoroutineInternal(coptr, newSegment, new int?(newLayer), instance._processTags.ContainsKey(key) ? instance._processTags[key] : (string) null, handle, false);
        return (IEnumerator<float>) null;
      });
      return float.NaN;
    }

    public static float SwitchCoroutine(Segment newSegment, int newLayer, string newTag)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing.GetInstance(handle.Key).RunCoroutineInternal(coptr, newSegment, new int?(newLayer), newTag, handle, false);
        return (IEnumerator<float>) null;
      });
      return float.NaN;
    }

    public static float SwitchCoroutine(string newTag)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing instance = Timing.GetInstance(handle.Key);
        instance.RemoveTag(instance._handleToIndex[handle]);
        if (newTag != null)
          instance.AddTag(newTag, instance._handleToIndex[handle]);
        return coptr;
      });
      return float.NaN;
    }

    public static float SwitchCoroutine(int newLayer)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing instance = Timing.GetInstance(handle.Key);
        instance.RemoveLayer(instance._handleToIndex[handle]);
        instance.AddLayer(newLayer, instance._handleToIndex[handle]);
        return coptr;
      });
      return float.NaN;
    }

    public static float SwitchCoroutine(int newLayer, string newTag)
    {
      Timing.ReplacementFunction = (Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>) ((coptr, handle) =>
      {
        Timing instance = Timing.GetInstance(handle.Key);
        instance.RemoveLayer(instance._handleToIndex[handle]);
        instance.AddLayer(newLayer, instance._handleToIndex[handle]);
        instance.RemoveTag(instance._handleToIndex[handle]);
        if (newTag != null)
          instance.AddTag(newTag, instance._handleToIndex[handle]);
        return coptr;
      });
      return float.NaN;
    }

    public static CoroutineHandle CallDelayed(float delay, Action action) => action != null ? Timing.RunCoroutine(Timing.Instance._DelayedCall(delay, action)) : new CoroutineHandle();

    public CoroutineHandle CallDelayedOnInstance(float delay, Action action) => action != null ? this.RunCoroutineOnInstance(this._DelayedCall(delay, action)) : new CoroutineHandle();

    private IEnumerator<float> _DelayedCall(float delay, Action action)
    {
      yield return this.WaitForSecondsOnInstance(delay);
      action();
    }

    public static CoroutineHandle CallPeriodically(
      float timeframe,
      float period,
      Action action,
      Action onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, period, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public CoroutineHandle CallPeriodicallyOnInstance(
      float timeframe,
      float period,
      Action action,
      Action onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously(timeframe, period, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public static CoroutineHandle CallPeriodically(
      float timeframe,
      float period,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, period, action, onDone), timing) : new CoroutineHandle();
    }

    public CoroutineHandle CallPeriodicallyOnInstance(
      float timeframe,
      float period,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously(timeframe, period, action, onDone), timing) : new CoroutineHandle();
    }

    public static CoroutineHandle CallContinuously(float timeframe, Action action, Action onDone = null) => action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, 0.0f, action, onDone), Segment.Update) : new CoroutineHandle();

    public CoroutineHandle CallContinuouslyOnInstance(
      float timeframe,
      Action action,
      Action onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously(timeframe, 0.0f, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public static CoroutineHandle CallContinuously(
      float timeframe,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously(timeframe, 0.0f, action, onDone), timing) : new CoroutineHandle();
    }

    public CoroutineHandle CallContinuouslyOnInstance(
      float timeframe,
      Action action,
      Segment timing,
      Action onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously(timeframe, 0.0f, action, onDone), timing) : new CoroutineHandle();
    }

    private IEnumerator<float> _CallContinuously(
      float timeframe,
      float period,
      Action action,
      Action onDone)
    {
      double startTime = this.localTime;
      while (this.localTime <= startTime + (double) timeframe)
      {
        yield return this.WaitForSecondsOnInstance(period);
        action();
      }
      if (onDone != null)
        onDone();
    }

    public static CoroutineHandle CallPeriodically<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Action<T> onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, period, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public CoroutineHandle CallPeriodicallyOnInstance<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Action<T> onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, period, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public static CoroutineHandle CallPeriodically<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, period, action, onDone), timing) : new CoroutineHandle();
    }

    public CoroutineHandle CallPeriodicallyOnInstance<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, period, action, onDone), timing) : new CoroutineHandle();
    }

    public static CoroutineHandle CallContinuously<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Action<T> onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, 0.0f, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public CoroutineHandle CallContinuouslyOnInstance<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Action<T> onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, 0.0f, action, onDone), Segment.Update) : new CoroutineHandle();
    }

    public static CoroutineHandle CallContinuously<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      return action != null ? Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, timeframe, 0.0f, action, onDone), timing) : new CoroutineHandle();
    }

    public CoroutineHandle CallContinuouslyOnInstance<T>(
      T reference,
      float timeframe,
      Action<T> action,
      Segment timing,
      Action<T> onDone = null)
    {
      return action != null ? this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, timeframe, 0.0f, action, onDone), timing) : new CoroutineHandle();
    }

    private IEnumerator<float> _CallContinuously<T>(
      T reference,
      float timeframe,
      float period,
      Action<T> action,
      Action<T> onDone = null)
    {
      double startTime = this.localTime;
      while (this.localTime <= startTime + (double) timeframe)
      {
        yield return this.WaitForSecondsOnInstance(period);
        action(reference);
      }
      if (onDone != null)
        onDone(reference);
    }

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public Coroutine StartCoroutine(IEnumerator routine) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public new Coroutine StartCoroutine(string methodName, object value) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public new Coroutine StartCoroutine(string methodName) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
    public Coroutine StartCoroutine_Auto(IEnumerator routine) => (Coroutine) null;

    [Obsolete("Unity coroutine function, use KillCoroutine instead.", true)]
    public new void StopCoroutine(string methodName)
    {
    }

    [Obsolete("Unity coroutine function, use KillCoroutine instead.", true)]
    public void StopCoroutine(IEnumerator routine)
    {
    }

    [Obsolete("Unity coroutine function, use KillCoroutine instead.", true)]
    public new void StopCoroutine(Coroutine routine)
    {
    }

    [Obsolete("Unity coroutine function, use KillAllCoroutines instead.", true)]
    public new void StopAllCoroutines()
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void Destroy(UnityEngine.Object obj)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void Destroy(UnityEngine.Object obj, float f)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyObject(UnityEngine.Object obj)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyObject(UnityEngine.Object obj, float f)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyImmediate(UnityEngine.Object obj)
    {
    }

    [Obsolete("Use your own GameObject for this.", true)]
    public new static void DestroyImmediate(UnityEngine.Object obj, bool b)
    {
    }

    [Obsolete("Just.. no.", true)]
    public new static T FindObjectOfType<T>() where T : UnityEngine.Object => default (T);

    [Obsolete("Just.. no.", true)]
    public static UnityEngine.Object FindObjectOfType(System.Type t) => (UnityEngine.Object) null;

    [Obsolete("Just.. no.", true)]
    public new static T[] FindObjectsOfType<T>() where T : UnityEngine.Object => (T[]) null;

    [Obsolete("Just.. no.", true)]
    public static UnityEngine.Object[] FindObjectsOfType(System.Type t) => (UnityEngine.Object[]) null;

    [Obsolete("Just.. no.", true)]
    public new static void print(object message)
    {
    }

    public enum DebugInfoType
    {
      None,
      SeperateCoroutines,
      SeperateTags,
    }

    private struct ProcessData : IEquatable<Timing.ProcessData>
    {
      public CoroutineHandle Handle;
      public float PauseTime;

      public bool Equals(Timing.ProcessData other) => this.Handle == other.Handle;

      public override bool Equals(object other) => other is Timing.ProcessData other1 && this.Equals(other1);

      public override int GetHashCode() => this.Handle.GetHashCode();
    }

    private struct ProcessIndex : IEquatable<Timing.ProcessIndex>
    {
      public Segment seg;
      public int i;

      public bool Equals(Timing.ProcessIndex other) => this.seg == other.seg && this.i == other.i;

      public override bool Equals(object other) => other is Timing.ProcessIndex other1 && this.Equals(other1);

      public static bool operator ==(Timing.ProcessIndex a, Timing.ProcessIndex b) => a.seg == b.seg && a.i == b.i;

      public static bool operator !=(Timing.ProcessIndex a, Timing.ProcessIndex b) => a.seg != b.seg || a.i != b.i;

      public override int GetHashCode() => (int) (this.seg - 4) * 306783378 + this.i;
    }
  }
}
