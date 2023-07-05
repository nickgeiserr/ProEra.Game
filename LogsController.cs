// Decompiled with JetBrains decompiler
// Type: LogsController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using ProEra.Game;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LogsController : PersistentSingleton<LogsController>
{
  [SerializeField]
  private Startup startup;
  [SerializeField]
  private GameObject prefabScreen;
  [SerializeField]
  private Transform parentForScreen;
  [SerializeField]
  private GameObject prefabTablet;
  private bool ShowFirstCriticalError;
  private LogsSettings _settings;
  private GameObject screenPreview;
  private bool _isInitialized;
  private bool _isLogsHandled;
  private bool _isEnabled;
  private int _amountLogsDefault;
  private int _amountLogsWarning;
  private int _amountLogsCritical;
  private LogsController.MessageData _msgDefault = new LogsController.MessageData(0);
  private LogsController.MessageData _msgWarnings = new LogsController.MessageData(1);
  private LogsController.MessageData _msgCriticals = new LogsController.MessageData(2);
  [HideInInspector]
  public int CurrentMessagesType;
  private const string newLine = "\r\n";
  private float _timerShowContent = -1f;
  private const float INPUT_DELAY = 1.5f;

  protected override void Awake()
  {
    base.Awake();
    UnityEngine.Object.Destroy((UnityEngine.Object) this);
  }

  public void Initialize()
  {
    if (this._isInitialized)
    {
      Debug.LogWarning((object) "LogsPreview already is initialized");
    }
    else
    {
      LogsSettings logsSettings = LogsSettings.Load();
      if (logsSettings == null)
      {
        this._settings = new LogsSettings();
        this._settings.Save();
      }
      else
        this._settings = logsSettings;
      if (!this._settings.isActive)
      {
        this._isInitialized = false;
      }
      else
      {
        this.ShowFirstCriticalError = this._settings.FirstCriticalErrorOnScreen;
        Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
        this._isLogsHandled = true;
        Debug.Log((object) "LogsPreview -> Initialize");
        this._isInitialized = true;
      }
    }
  }

  public LogsSettings GetSettings() => !this._isInitialized ? (LogsSettings) null : this._settings;

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!this._isInitialized || !this._isLogsHandled)
      return;
    Application.logMessageReceived -= new Application.LogCallback(this.HandleLog);
  }

  private void HandleLog(string msg, string trace, LogType logType)
  {
    string stacktrace = trace ?? string.Empty;
    Task.Run((System.Action) (() =>
    {
      if (logType == LogType.Error || logType == LogType.Exception || logType == LogType.Assert)
        ++this._amountLogsCritical;
      if (logType == LogType.Warning)
        ++this._amountLogsWarning;
      if (logType == LogType.Log)
        ++this._amountLogsDefault;
      if (stacktrace.Length > 200)
      {
        int num1 = 0;
        int length = 0;
        for (; num1 < 3; ++num1)
        {
          int num2 = stacktrace.IndexOf('\n', length + 1);
          if (num2 > 0)
            length = num2;
          else
            break;
        }
        stacktrace = stacktrace.Substring(0, length);
      }
      this._msgDefault.OptimizeBuffer();
      this._msgWarnings.OptimizeBuffer();
      this._msgCriticals.OptimizeBuffer();
      int num = 0;
      string str1 = "<color=white>";
      if (logType == LogType.Error || logType == LogType.Exception || logType == LogType.Assert)
      {
        str1 = "<color=red>";
        num = 2;
      }
      if (logType == LogType.Warning)
      {
        str1 = "<color=yellow>";
        num = 1;
      }
      string str2 = str1 + msg + "\r\n" + (!string.IsNullOrEmpty(stacktrace) ? stacktrace + "\r\n" : string.Empty) + "</color>\r\n";
      if (num == 0)
        this._msgDefault.SetMessage(str2);
      if (num == 1)
        this._msgWarnings.SetMessage(str2);
      if (num != 2)
        return;
      this._msgCriticals.SetMessage(str2);
    }));
  }

  private void Update()
  {
    if (!this._isInitialized || !this._isLogsHandled)
      return;
    if (this.ShowFirstCriticalError && this._amountLogsCritical > 0 && (UnityEngine.Object) this.screenPreview == (UnityEngine.Object) null)
    {
      this.CreateScreen();
      this.ShowFirstCriticalError = false;
    }
    if (VRInputManager.Get(VRInputManager.Button.Primary2DAxisClick, VRInputManager.Controller.RightHand))
    {
      if ((double) this._timerShowContent < 0.0)
        this._timerShowContent = Time.time + 1.5f;
      if ((double) Time.time <= (double) this._timerShowContent)
        return;
      this._timerShowContent = Time.time + 1.5f;
      this.ShowPopup(!this._isEnabled);
    }
    else
      this._timerShowContent = -1f;
  }

  private void ShowPopup(bool value)
  {
    this._isEnabled = value;
    if ((UnityEngine.Object) this.startup != (UnityEngine.Object) null && !this.startup.isInitialized)
    {
      if (value)
      {
        this.CreateScreen();
      }
      else
      {
        if (!((UnityEngine.Object) this.screenPreview != (UnityEngine.Object) null))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.screenPreview);
      }
    }
    else if (value)
    {
      this.CreateTablet();
    }
    else
    {
      if ((bool) (UnityEngine.Object) this.screenPreview)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.screenPreview);
      if ((UnityEngine.Object) PlayerAvatar.Instance != (UnityEngine.Object) null && (UnityEngine.Object) PlayerAvatar.Instance.LeftController != (UnityEngine.Object) null)
        PlayerAvatar.Instance.LeftController.SetWristbandColliderEnabled(true);
      DevConsolePage.SelfDestroy();
    }
  }

  private void CreateTablet()
  {
    Debug.Log((object) "LogsController -> CreateTablet");
    if ((UnityEngine.Object) DevConsolePage.Instance != (UnityEngine.Object) null)
      DevConsolePage.Instance.SetOnLeftHand();
    else
      UnityEngine.Object.Instantiate<GameObject>(this.prefabTablet).GetComponentInChildren<DevConsolePage>().SetOnLeftHand();
  }

  private void CreateScreen()
  {
    Debug.Log((object) "LogsController -> CreateScreen");
    if ((UnityEngine.Object) this.screenPreview != (UnityEngine.Object) null)
      return;
    this.screenPreview = UnityEngine.Object.Instantiate<GameObject>(this.prefabScreen);
    if (!((UnityEngine.Object) this.screenPreview != (UnityEngine.Object) null))
      return;
    this.screenPreview.transform.parent = this.parentForScreen;
    this.screenPreview.transform.localPosition = new Vector3(0.0f, 0.0f, 1.7f);
    this.screenPreview.transform.localRotation = Quaternion.Euler(Vector3.zero);
  }

  public (string, int) GetPreparedLogs()
  {
    LogsController.MessageData messageData = (LogsController.MessageData) null;
    if (this.CurrentMessagesType == 0)
      messageData = this._msgDefault;
    if (this.CurrentMessagesType == 1)
      messageData = this._msgWarnings;
    if (this.CurrentMessagesType == 2)
      messageData = this._msgCriticals;
    if (messageData == null || messageData._messages.Count == 0)
      return (string.Empty, 0);
    string str = string.Empty;
    for (int index = 0; index < messageData._messages.Count; ++index)
      str = messageData._messages[index] + str;
    return (str, messageData._rows);
  }

  public int GetLogsAmountDefault() => this._amountLogsDefault;

  public int GetLogsAmountWarning() => this._amountLogsWarning;

  public int GetLogsAmountCritical() => this._amountLogsCritical;

  public void ClearLogs()
  {
    this._msgDefault.Clear();
    this._msgWarnings.Clear();
    this._msgCriticals.Clear();
    this._amountLogsDefault = 0;
    this._amountLogsWarning = 0;
    this._amountLogsCritical = 0;
  }

  public class MessageData
  {
    public List<string> _messages = new List<string>();
    public int maxMessages = 15;
    public int _rows;
    private int messageType;
    private int charsInRow = 72;

    public MessageData(int msgType) => this.messageType = msgType;

    public void OptimizeBuffer()
    {
      if (this._messages.Count != this.maxMessages)
        return;
      this._rows -= Mathf.CeilToInt((float) this._messages[this._messages.Count - 1].Length / (float) this.charsInRow);
      this._messages.RemoveAt(this._messages.Count - 1);
    }

    public void SetMessage(string value)
    {
      this._rows += Mathf.CeilToInt((float) value.Length / (float) this.charsInRow);
      this._messages.Insert(0, value);
    }

    public void Clear()
    {
      this._rows = 0;
      this._messages.Clear();
      this._messages = new List<string>();
    }
  }
}
