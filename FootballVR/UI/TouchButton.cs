// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vars;

namespace FootballVR.UI
{
  public class TouchButton : MonoBehaviour, IButton
  {
    [FormerlySerializedAs("_children")]
    [SerializeField]
    private List<RectTransform> _buttonLayers;
    [SerializeField]
    private float _recoverySpeed = 6f;
    [SerializeField]
    private float _highlightRange = 30f;
    [SerializeField]
    private float _highlightMaxDistance = 100f;
    [SerializeField]
    private uint _soundId;
    private float _expansion = 1f;
    [SerializeField]
    private int Id = -1;
    private float _normalizedPosition;
    private readonly VariableBool _buttonPressed = new VariableBool();
    protected bool _recovering;
    private bool _initialized;
    private bool _isLaserHighlighted;
    private float _topLayerPos;
    private Vector3[] _layerPositions;
    private Rect _rect;
    private Rect _highlightRect;
    private readonly List<TouchButton.ControllerData> _controllers = new List<TouchButton.ControllerData>();
    private bool _hasCanvasGroup;
    private CanvasGroup _rootCanvas;
    private UnityEngine.UI.Button m_UIButton;
    private const float _zOffset = 30f;
    private readonly Vector3 _offsetVec = new Vector3(0.0f, 0.0f, 30f);
    private EHand handInteraction = EHand.Right;
    private bool _skipUpdate = true;

    public float Expansion
    {
      get => this._expansion;
      set
      {
        this._expansion = value;
        this.UpdateTransforms();
      }
    }

    public void SetNormalizedPosition(float value)
    {
      float a = Mathf.Clamp01(value);
      if (Mathf.Approximately(a, this._normalizedPosition))
        return;
      this._normalizedPosition = a;
      this.UpdateTransforms();
      Action<float> normalizedPositionChanged = this.OnNormalizedPositionChanged;
      if (normalizedPositionChanged == null)
        return;
      normalizedPositionChanged(this._normalizedPosition);
    }

    public event Action<float> OnNormalizedPositionChanged;

    public event System.Action onClick;

    public event Action<TouchButton> onClickInfo;

    public int GetID() => this.Id;

    public static event Action<uint> OnButtonClicked;

    public VariableBool Interactable { get; } = new VariableBool(true);

    public VariableBool Highlighted { get; } = new VariableBool(false);

    public bool IsInitialized => this._initialized;

    protected virtual void Awake() => this.Initialize();

    public void Initialize()
    {
      if (this._initialized)
        return;
      for (int index = 0; index < this._buttonLayers.Count; ++index)
      {
        if (!((UnityEngine.Object) this._buttonLayers[index] != (UnityEngine.Object) null))
        {
          this._buttonLayers.RemoveAt(index);
          --index;
          Debug.LogError((object) string.Format("Removing element {0} from buttonLayers on {1} TouchButton", (object) index, (object) this.gameObject.name));
        }
      }
      this._initialized = true;
      this._rect = ((RectTransform) this.transform).rect;
      this._highlightRect = new Rect(this._rect);
      this._highlightRect.x -= this._highlightRange / 2f;
      this._highlightRect.y -= this._highlightRange / 2f;
      this._highlightRect.width += this._highlightRange;
      this._highlightRect.height += this._highlightRange;
      this._buttonPressed.OnValueChanged += new Action<bool>(this.HandlePressStateChanged);
      this._layerPositions = new Vector3[this._buttonLayers.Count];
      this._topLayerPos = 0.0f;
      this._normalizedPosition = 0.0f;
      for (int index = 0; index < this._buttonLayers.Count; ++index)
      {
        Vector3 localPosition = this._buttonLayers[index].localPosition;
        this._layerPositions[index] = localPosition;
        this._topLayerPos = Mathf.Min(this._topLayerPos, localPosition.z);
      }
      this.m_UIButton = this.gameObject.GetComponent<UnityEngine.UI.Button>();
      if (!(bool) (UnityEngine.Object) this.m_UIButton)
        this.m_UIButton = this.gameObject.AddComponent<UnityEngine.UI.Button>();
      this.m_UIButton.onClick.AddListener(new UnityAction(this.HandleButtonPressed));
    }

    private void OnDestroy() => this._buttonPressed.OnValueChanged -= new Action<bool>(this.HandlePressStateChanged);

    private void UpdateTransforms()
    {
      if (this._buttonLayers == null)
        return;
      float num = 1f - this._normalizedPosition;
      int count = this._buttonLayers.Count;
      for (int index = 0; index < count; ++index)
      {
        RectTransform buttonLayer = this._buttonLayers[index];
        if (buttonLayer == null)
        {
          Debug.LogError((object) ("Null child transform in " + this.gameObject.name));
        }
        else
        {
          Vector3 layerPosition = this._layerPositions[index];
          layerPosition.z *= num * this._expansion;
          buttonLayer.localPosition = layerPosition;
        }
      }
    }

    public void HandlePressStateChanged(bool pressed)
    {
      if (!pressed)
        return;
      this.HandleButtonPressed();
    }

    protected virtual void HandleButtonPressed()
    {
      if (!(bool) this.Interactable)
        return;
      VRInputManager.SetHaptic(this.handInteraction);
      System.Action onClick = this.onClick;
      if (onClick != null)
        onClick();
      Action<TouchButton> onClickInfo = this.onClickInfo;
      if (onClickInfo != null)
        onClickInfo(this);
      Action<uint> onButtonClicked = TouchButton.OnButtonClicked;
      if (onButtonClicked != null)
        onButtonClicked(this._soundId);
      TouchProximityEffect.Pause(1f);
    }

    private void OnEnable()
    {
      Canvas componentInParent = this.GetComponentInParent<Canvas>();
      if ((UnityEngine.Object) componentInParent != (UnityEngine.Object) null)
      {
        this._rootCanvas = componentInParent.rootCanvas.GetComponent<CanvasGroup>();
        this._hasCanvasGroup = (UnityEngine.Object) this._rootCanvas != (UnityEngine.Object) null;
      }
      foreach (ITouchInput touchInput in TouchUI.TouchInputs)
      {
        Vector3 position = this.transform.InverseTransformPoint(touchInput.touchPosition);
        this._controllers.Add(new TouchButton.ControllerData(touchInput, position));
      }
    }

    private void OnDisable() => this._controllers.Clear();

    protected virtual void Update()
    {
      this._skipUpdate = !this._skipUpdate;
      if (this._skipUpdate)
        return;
      bool flag1 = this._buttonPressed.Value;
      float num = this._topLayerPos * this._expansion;
      float pos = Mathf.Clamp(num * (1f - this._normalizedPosition) - this._recoverySpeed, num, 0.0f);
      this.handInteraction = EHand.Right;
      bool flag2 = this.ProcessInput(ref pos, ref this.handInteraction);
      if (!flag2 & flag1 && !this._recovering)
        this._recovering = true;
      this.SetNormalizedPosition(Mathf.InverseLerp(num, 0.0f, pos));
      if (((flag1 ? 0 : ((double) pos > 0.30000001192092896 * (double) num ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && !this._recovering)
        this._buttonPressed.Value = true;
      if ((double) pos >= 0.699999988079071 * (double) num)
        return;
      this._buttonPressed.Value = false;
      this._recovering = false;
    }

    private bool ProcessInput(ref float pos, ref EHand hand)
    {
      int count = this._controllers.Count;
      if (!(bool) this.Interactable || this._hasCanvasGroup && !this._rootCanvas.blocksRaycasts || this._recovering || !TouchUI.Enabled)
      {
        for (int index = 0; index < count; ++index)
          this._controllers[index].interacting = false;
        return false;
      }
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < count; ++index)
      {
        TouchButton.ControllerData controller = this._controllers[index];
        Vector3 point = this.transform.InverseTransformPoint(controller.position) + this._offsetVec;
        Vector3 prevHandPos = controller.prevHandPos;
        float num = pos + 30f;
        if ((double) point.z < (double) pos || (double) prevHandPos.z < (double) pos)
        {
          controller.interacting = true;
          hand = controller.hand == EHand.Left ? EHand.Right : EHand.Left;
        }
        else if ((double) point.z > (double) num && (double) prevHandPos.z > (double) num)
          controller.interacting = false;
        flag1 = ((((flag1 ? 1 : 0) | ((double) point.z <= (double) this._topLayerPos * (double) this._expansion - (double) this._highlightMaxDistance ? 0 : (this._highlightRect.Contains(point) ? 1 : (this._highlightRect.Contains(prevHandPos) ? 1 : 0)))) != 0 ? 1 : 0) | (!(bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser ? 0 : (this._isLaserHighlighted ? 1 : 0))) != 0;
        controller.prevHandPos = point;
        if (controller.interacting && flag1 && (this._rect.Contains(point) ? 1 : (this._rect.Contains(prevHandPos) ? 1 : 0)) != 0)
        {
          if ((double) pos < (double) point.z)
            pos = point.z;
          flag2 = true;
        }
      }
      this.Highlighted.SetValue(flag1);
      return flag2;
    }

    public void SimulateClick() => this.HandleButtonPressed();

    public void HighlighAsSelected(bool selected, Color unselectedCol, Color selectedCol)
    {
      if (this._buttonLayers.Count <= 1 || !((UnityEngine.Object) this._buttonLayers[1] != (UnityEngine.Object) null))
        return;
      Image component = this._buttonLayers[1].GetComponent<Image>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.color = selected ? selectedCol : unselectedCol;
    }

    public void SetInteractible(bool value)
    {
      if (this.Interactable.Value != value)
        this.Interactable.SetValue(value);
      if (!((UnityEngine.Object) this.m_UIButton != (UnityEngine.Object) null))
        return;
      this.m_UIButton.interactable = value;
    }

    public void SetLaserHighlight(bool a_isLaserHighlighted) => this._isLaserHighlighted = a_isLaserHighlighted;

    [ContextMenu("Set non interactable")]
    private void SetNonInteract() => this.Interactable.SetValue(false);

    public void SetID(int id) => this.Id = id;

    private class ControllerData
    {
      private readonly ITouchInput input;
      public Vector3 prevHandPos;
      public bool interacting;

      public Vector3 position => this.input.touchPosition;

      public EHand hand => this.input.dragHand;

      public ControllerData(ITouchInput touchInput, Vector3 position)
      {
        this.input = touchInput;
        this.prevHandPos = position;
      }
    }
  }
}
