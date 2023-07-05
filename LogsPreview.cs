// Decompiled with JetBrains decompiler
// Type: LogsPreview
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogsPreview : MonoBehaviour
{
  [SerializeField]
  private Transform rootPivot;
  [Space(10f)]
  [SerializeField]
  private TouchUI2DButton btnClear;
  [Space(10f)]
  [SerializeField]
  private TouchUI2DButton btnClose;
  [SerializeField]
  private TouchUI2DScrollRect btnScrollLogs;
  [Space(10f)]
  [SerializeField]
  private TouchUI2DButton btnLogTypeDefault;
  [SerializeField]
  private TouchUI2DButton btnLogTypeWarning;
  [SerializeField]
  private TouchUI2DButton btnLogTypeCritical;
  [Space(10f)]
  [SerializeField]
  private ScrollRect scrollRect;
  [SerializeField]
  private TMP_Text labelLogs;
  [SerializeField]
  private float sizePerRow = 0.05f;
  [SerializeField]
  private float sizeConst = 0.01f;
  private int prevMsgType;
  [SerializeField]
  private bool ShowOnlyCriticals = true;
  public float _VALUE = 100f;
  private RectTransform _rectLogs;
  private bool _isInitialized;
  private bool _isLastLine = true;

  private void Awake() => this.Initialize();

  public void Initialize()
  {
    if (this._isInitialized)
    {
      Debug.LogWarning((object) "LogsPreview already is initialized");
    }
    else
    {
      Debug.Log((object) "LogsPreview -> Initialize");
      if (!Debug.isDebugBuild)
      {
        if ((UnityEngine.Object) this.transform.parent != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) this.transform.parent);
        else
          UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
      else
      {
        this._rectLogs = this.labelLogs.gameObject.GetComponent<RectTransform>();
        if ((UnityEngine.Object) this.btnClear != (UnityEngine.Object) null)
          this.btnClear.onClick += new System.Action(this.HandleClearButton);
        if ((UnityEngine.Object) this.btnClose != (UnityEngine.Object) null)
          this.btnClose.onClick += new System.Action(this.HandleCloseButton);
        if ((UnityEngine.Object) this.btnLogTypeDefault != (UnityEngine.Object) null)
          this.btnLogTypeDefault.onClick += new System.Action(this.HandleLogsDefault);
        if ((UnityEngine.Object) this.btnLogTypeWarning != (UnityEngine.Object) null)
          this.btnLogTypeWarning.onClick += new System.Action(this.HandleLogsWarnings);
        if ((UnityEngine.Object) this.btnLogTypeCritical != (UnityEngine.Object) null)
          this.btnLogTypeCritical.onClick += new System.Action(this.HandleLogsCriticals);
        this._isInitialized = true;
      }
    }
  }

  private void OnEnable()
  {
    if (!this.ShowOnlyCriticals)
      return;
    this.prevMsgType = PersistentSingleton<LogsController>.Instance.CurrentMessagesType;
    PersistentSingleton<LogsController>.Instance.CurrentMessagesType = 2;
  }

  private void OnDisable()
  {
    if (!this.ShowOnlyCriticals || !((UnityEngine.Object) PersistentSingleton<LogsController>.Instance != (UnityEngine.Object) null))
      return;
    PersistentSingleton<LogsController>.Instance.CurrentMessagesType = this.prevMsgType;
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this.btnClear != (UnityEngine.Object) null)
      this.btnClear.onClick -= new System.Action(this.HandleClearButton);
    if ((UnityEngine.Object) this.btnClose != (UnityEngine.Object) null)
      this.btnClose.onClick -= new System.Action(this.HandleCloseButton);
    if ((UnityEngine.Object) this.btnLogTypeDefault != (UnityEngine.Object) null)
      this.btnLogTypeDefault.onClick -= new System.Action(this.HandleLogsDefault);
    if ((UnityEngine.Object) this.btnLogTypeWarning != (UnityEngine.Object) null)
      this.btnLogTypeWarning.onClick -= new System.Action(this.HandleLogsWarnings);
    if (!((UnityEngine.Object) this.btnLogTypeCritical != (UnityEngine.Object) null))
      return;
    this.btnLogTypeCritical.onClick -= new System.Action(this.HandleLogsCriticals);
  }

  private void HandleLogsDefault() => PersistentSingleton<LogsController>.Instance.CurrentMessagesType = 0;

  private void HandleLogsWarnings() => PersistentSingleton<LogsController>.Instance.CurrentMessagesType = 1;

  private void HandleLogsCriticals() => PersistentSingleton<LogsController>.Instance.CurrentMessagesType = 2;

  private void Update() => this.UpdateAllLabels();

  private void UpdateAllLabels()
  {
    if ((UnityEngine.Object) PersistentSingleton<LogsController>.Instance == (UnityEngine.Object) null)
      return;
    (string, int) preparedLogs = PersistentSingleton<LogsController>.Instance.GetPreparedLogs();
    if (preparedLogs.Item1 != null)
      this.labelLogs.text = preparedLogs.Item1;
    this._rectLogs.sizeDelta = this._rectLogs.sizeDelta.SetY((float) preparedLogs.Item2 * this.sizePerRow + this.sizeConst);
    this._rectLogs.sizeDelta = this._rectLogs.sizeDelta.SetY(this.labelLogs.GetPreferredValues().y);
    this.scrollRect.content.sizeDelta = new Vector2(this.scrollRect.content.sizeDelta.x, this._rectLogs.sizeDelta.y / (this.scrollRect.content.localScale.y / this._rectLogs.localScale.y));
    if (!this._isLastLine && (double) this.scrollRect.normalizedPosition.y <= 0.05000000074505806)
      this._isLastLine = true;
    if ((UnityEngine.Object) this.btnScrollLogs != (UnityEngine.Object) null && this._isLastLine && this.btnScrollLogs.IsTouched)
      this._isLastLine = false;
    if (this._isLastLine)
      this.scrollRect.normalizedPosition = Vector2.zero;
    this.UpdateMessagesCounters();
  }

  private void UpdateMessagesCounters()
  {
    this.UpdateLabelCounter(this.btnLogTypeDefault, PersistentSingleton<LogsController>.Instance.GetLogsAmountDefault().ToString());
    this.UpdateLabelCounter(this.btnLogTypeWarning, PersistentSingleton<LogsController>.Instance.GetLogsAmountWarning().ToString());
    this.UpdateLabelCounter(this.btnLogTypeCritical, PersistentSingleton<LogsController>.Instance.GetLogsAmountCritical().ToString());
  }

  private void UpdateLabelCounter(TouchUI2DButton target, string value)
  {
    if ((UnityEngine.Object) target == (UnityEngine.Object) null)
      return;
    if (!target.gameObject.activeSelf)
      target.gameObject.SetActive(true);
    target.SetLabelText(value);
  }

  private void HandleClearButton()
  {
    PersistentSingleton<LogsController>.Instance.ClearLogs();
    this._isLastLine = true;
  }

  private void HandleCloseButton() => this.ShowContent(false);

  private void ShowContent(bool value) => this.rootPivot.gameObject.SetActive(value);
}
