// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchUI2DButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  [RequireComponent(typeof (Image))]
  public class TouchUI2DButton : MonoBehaviour, IButton
  {
    [SerializeField]
    private int Id = -1;
    [SerializeField]
    private bool _hasOnExit;
    [SerializeField]
    private TMP_Text label;
    [SerializeField]
    private float _DotCheck = -0.3f;
    private Collider _buttonPresser;
    private bool _isPressable;
    private float _startPressTime = 0.25f;
    private Color _startBGColor;
    private Color _pressedColor = new Color(0.1294f, 1f, 0.5803f);
    private Image _image;
    [SerializeField]
    private Image _Background;
    private float _buttonPressAnimationDuration = 0.5f;
    private Coroutine buttonPulseAnimation;
    [Header("Hovering Effects")]
    [Space(10f)]
    [SerializeField]
    private TouchUI2DButton.TransitionOptions _HoverOption = TouchUI2DButton.TransitionOptions.v1;
    [SerializeField]
    private float _HoverDistance = 0.5f;
    private float _HoverDistanceSq = 1f;
    [SerializeField]
    private Color _HoverTextColor = Color.white;
    [SerializeField]
    private Color _HoverBGColor = Color.blue;
    [Header("Press Effects")]
    [Space(10f)]
    [SerializeField]
    private TouchUI2DButton.TransitionOptions _PressOption = TouchUI2DButton.TransitionOptions.v1;
    [SerializeField]
    private Color _PressColorStart = Color.white;
    [SerializeField]
    private Color _PressColorEnd = Color.green;
    private readonly RoutineHandle _hoverRoutine = new RoutineHandle();
    private readonly RoutineHandle _waitForPressablity = new RoutineHandle();
    private readonly RoutineHandle _pulseButtonHover = new RoutineHandle();
    private readonly RoutineHandle _v1ClickEffectRoutine = new RoutineHandle();
    private readonly RoutineHandle _pulseButtonClick = new RoutineHandle();
    private bool _isHovering;
    private bool _isLaserHovering;
    private Color _startTextColor;
    private Rect _highlightRect;
    private bool _isClicked;
    private bool bRightHandCached;
    private bool bLeftHandCached;
    private float _skewFactor = 0.375f;
    private float _skewDuration = 0.2f;
    private Transform _rightHandFingerPoint;
    private Transform _leftHandFingerPoint;

    public event System.Action onClick;

    public event Action<TouchUI2DButton> onClickInfo;

    public int GetID() => this.Id;

    public event System.Action OnHoverStart;

    public event System.Action OnHoverEnd;

    private void Start()
    {
      this.OnHoverStart += new System.Action(this.OnButtonHover);
      this.OnHoverEnd += new System.Action(this.OffButtonHover);
      if ((UnityEngine.Object) this.label == (UnityEngine.Object) null)
        this.label = this.GetComponentInChildren<TMP_Text>();
      if ((UnityEngine.Object) this.label != (UnityEngine.Object) null)
      {
        this._skewFactor *= this.label.fontSize;
        this._startTextColor = this.label.color;
      }
      if ((UnityEngine.Object) this._Background != (UnityEngine.Object) null)
        this._startBGColor = this._Background.color;
      this._highlightRect = ((RectTransform) this.transform).rect;
      this._HoverDistanceSq = this._HoverDistance * this._HoverDistance;
    }

    private bool CachedHands()
    {
      if (!this.bRightHandCached)
      {
        HandController rightHand = PlayerAvatar.GetRightHand();
        if ((UnityEngine.Object) rightHand != (UnityEngine.Object) null)
        {
          this._rightHandFingerPoint = rightHand.GetFingerTouchPoint();
          if ((UnityEngine.Object) this._rightHandFingerPoint != (UnityEngine.Object) null)
            this.bRightHandCached = true;
        }
      }
      if (!this.bLeftHandCached)
      {
        HandController leftHand = PlayerAvatar.GetLeftHand();
        if ((UnityEngine.Object) leftHand != (UnityEngine.Object) null)
        {
          this._leftHandFingerPoint = leftHand.GetFingerTouchPoint();
          if ((UnityEngine.Object) this._leftHandFingerPoint != (UnityEngine.Object) null)
            this.bLeftHandCached = true;
        }
      }
      return !this.bRightHandCached && !this.bLeftHandCached;
    }

    private void OnDestroy()
    {
      this.OnHoverStart -= new System.Action(this.OnButtonHover);
      this.OnHoverEnd -= new System.Action(this.OffButtonHover);
      this.StopAllCoroutines();
      this._hoverRoutine?.Stop();
      this._pulseButtonHover?.Stop();
      this._waitForPressablity?.Stop();
      this._v1ClickEffectRoutine?.Stop();
      this._pulseButtonClick?.Stop();
      this.onClick = (System.Action) null;
      this.onClickInfo = (Action<TouchUI2DButton>) null;
    }

    private void OnEnable()
    {
      this._buttonPresser = (Collider) null;
      this._isPressable = false;
      this._image = this.GetComponent<Image>();
      this._startBGColor = this._image.color;
      this._waitForPressablity.Run(this.WaitForPressablity());
    }

    private void OnDisable() => this._image.color = this._startBGColor;

    private IEnumerator WaitForPressablity()
    {
      yield return (object) new WaitForSeconds(this._startPressTime);
      this._isPressable = true;
    }

    private void Update()
    {
      if (this._isClicked)
        return;
      this.CheckHandleHover();
    }

    private bool CheckHandleHover()
    {
      if (!VRState.InterationWithUI.Value || this.CachedHands())
        return false;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = true;
      bool flag4 = true;
      bool flag5 = (bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser && this._isLaserHovering;
      Vector3 vector3;
      if (this.bRightHandCached && (bool) (UnityEngine.Object) this._rightHandFingerPoint)
      {
        Vector3 position = this._rightHandFingerPoint.position;
        Vector3 point = this.transform.InverseTransformPoint(position);
        vector3 = position - this.transform.position;
        int num = (double) vector3.sqrMagnitude <= (double) this._HoverDistanceSq ? 1 : 0;
        bool flag6 = this._highlightRect.Contains(point);
        flag2 = (num & (flag6 ? 1 : 0)) != 0;
        flag4 = num == 0 || !flag6;
      }
      if (this.bLeftHandCached && (bool) (UnityEngine.Object) this._leftHandFingerPoint)
      {
        Vector3 position = this._leftHandFingerPoint.position;
        Vector3 point = this.transform.InverseTransformPoint(position);
        vector3 = position - this.transform.position;
        int num = (double) vector3.sqrMagnitude <= (double) this._HoverDistanceSq ? 1 : 0;
        bool flag7 = this._highlightRect.Contains(point);
        flag1 = (num & (flag7 ? 1 : 0)) != 0;
        flag3 = num == 0 || !flag7;
      }
      if (!this._isHovering)
      {
        if (flag2 | flag1 | flag5)
        {
          System.Action onHoverStart = this.OnHoverStart;
          if (onHoverStart != null)
            onHoverStart();
        }
      }
      else if (this._isHovering && flag4 & flag3 && !flag5)
      {
        System.Action onHoverEnd = this.OnHoverEnd;
        if (onHoverEnd != null)
          onHoverEnd();
      }
      return this._isHovering;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!VRState.InterationWithUI.Value || other.gameObject.layer != LayerMask.NameToLayer("UserAvatar") || !this._isPressable)
        return;
      HandFingerID component = other.gameObject.GetComponent<HandFingerID>();
      if (!(bool) (UnityEngine.Object) component || !this.CheckPosition(component.transform.position) || !this.CheckDot(other.ClosestPoint(this.transform.position)))
        return;
      VRInputManager.SetHaptic(component.Hand);
      this.OnClick();
      this._buttonPresser = other;
    }

    private bool CheckPosition(Vector3 touchPosition) => (double) Vector3.Distance(touchPosition, this.transform.position) < 0.20000000298023224 && ((UnityEngine.Object) this._buttonPresser == (UnityEngine.Object) null || !this._hasOnExit);

    private bool CheckDot(Vector3 position) => (double) Vector3.Dot((position - this.transform.position).normalized, this.transform.forward) < (double) this._DotCheck;

    private bool CheckHoverDot(Vector3 position) => true;

    private void OnTriggerExit(Collider other)
    {
      if (!((UnityEngine.Object) other == (UnityEngine.Object) this._buttonPresser))
        return;
      this._buttonPresser = (Collider) null;
    }

    public void SimulateClick() => this.OnClick();

    public void SetLabelText(string value)
    {
      if (!((UnityEngine.Object) this.label != (UnityEngine.Object) null))
        return;
      this.label.text = value;
    }

    public void SetLabelTextColor(Color value)
    {
      if (!((UnityEngine.Object) this.label != (UnityEngine.Object) null))
        return;
      this.label.color = value;
    }

    public void SetLaserHovering(bool val) => this._isLaserHovering = val;

    public void OnButtonHover()
    {
      if (!VRState.InterationWithUI.Value || this._isHovering)
        return;
      this._isHovering = true;
      switch (this._HoverOption)
      {
        case TouchUI2DButton.TransitionOptions.v1:
          if (!((UnityEngine.Object) this.label != (UnityEngine.Object) null))
            break;
          this._hoverRoutine.Run(this.V1HoverEffectRoutine(true));
          this.label.color = this._HoverTextColor;
          break;
        case TouchUI2DButton.TransitionOptions.ChangeColor:
          this._image.color = this._pressedColor;
          break;
      }
    }

    public void OffButtonHover()
    {
      if (!this._isHovering)
        return;
      this._isHovering = false;
      switch (this._HoverOption)
      {
        case TouchUI2DButton.TransitionOptions.v1:
          if (!((UnityEngine.Object) this.label != (UnityEngine.Object) null))
            break;
          this._hoverRoutine.Run(this.V1HoverEffectRoutine(false));
          this.label.color = this._startTextColor;
          break;
        case TouchUI2DButton.TransitionOptions.ChangeColor:
          this._pulseButtonHover.Run(this.PulseButtonHover());
          break;
      }
    }

    public void OnClick(HandController handController = null)
    {
      if (!VRState.InterationWithUI.Value || !this.enabled)
        return;
      switch (this._PressOption)
      {
        case TouchUI2DButton.TransitionOptions.None:
          System.Action onClick1 = this.onClick;
          if (onClick1 != null)
            onClick1();
          Action<TouchUI2DButton> onClickInfo1 = this.onClickInfo;
          if (onClickInfo1 != null)
          {
            onClickInfo1(this);
            break;
          }
          break;
        case TouchUI2DButton.TransitionOptions.v1:
          this._hoverRoutine?.Stop();
          this._v1ClickEffectRoutine.Run(this.V1ClickEffectRoutine());
          this._isHovering = false;
          this._isClicked = false;
          System.Action onClick2 = this.onClick;
          if (onClick2 != null)
            onClick2();
          Action<TouchUI2DButton> onClickInfo2 = this.onClickInfo;
          if (onClickInfo2 != null)
          {
            onClickInfo2(this);
            break;
          }
          break;
        case TouchUI2DButton.TransitionOptions.ChangeColor:
          this._waitForPressablity?.Stop();
          this._pulseButtonClick.Run(this.PulseButtonClick());
          System.Action onClick3 = this.onClick;
          if (onClick3 != null)
            onClick3();
          Action<TouchUI2DButton> onClickInfo3 = this.onClickInfo;
          if (onClickInfo3 != null)
          {
            onClickInfo3(this);
            break;
          }
          break;
      }
      if (!((UnityEngine.Object) handController != (UnityEngine.Object) null))
        return;
      VRInputManager.SetHaptic(handController.GetDragHand());
    }

    private IEnumerator PulseButtonClick()
    {
      TouchUI2DButton touchUi2Dbutton = this;
      if (VRState.InterationWithUI.Value && touchUi2Dbutton.enabled)
      {
        if ((UnityEngine.Object) touchUi2Dbutton._image != (UnityEngine.Object) null)
          touchUi2Dbutton._image.color = touchUi2Dbutton._pressedColor;
        float elapsedTime = 0.0f;
        while ((double) elapsedTime < (double) touchUi2Dbutton._buttonPressAnimationDuration)
        {
          elapsedTime += Time.deltaTime;
          float t = elapsedTime / touchUi2Dbutton._buttonPressAnimationDuration;
          if ((UnityEngine.Object) touchUi2Dbutton._image != (UnityEngine.Object) null)
            touchUi2Dbutton._image.color = Color.Lerp(touchUi2Dbutton._pressedColor, touchUi2Dbutton._startBGColor, t);
          yield return (object) null;
        }
        if ((UnityEngine.Object) touchUi2Dbutton._image != (UnityEngine.Object) null)
          touchUi2Dbutton._image.color = touchUi2Dbutton._startBGColor;
        yield return (object) null;
      }
    }

    private IEnumerator PulseButtonHover()
    {
      TouchUI2DButton touchUi2Dbutton = this;
      if (VRState.InterationWithUI.Value && touchUi2Dbutton.enabled)
      {
        if ((UnityEngine.Object) touchUi2Dbutton._image != (UnityEngine.Object) null)
          touchUi2Dbutton._image.color = touchUi2Dbutton._pressedColor;
        float elapsedTime = 0.0f;
        while ((double) elapsedTime < (double) touchUi2Dbutton._buttonPressAnimationDuration)
        {
          elapsedTime += Time.deltaTime;
          float t = elapsedTime / touchUi2Dbutton._buttonPressAnimationDuration;
          if ((UnityEngine.Object) touchUi2Dbutton._image != (UnityEngine.Object) null)
            touchUi2Dbutton._image.color = Color.Lerp(touchUi2Dbutton._pressedColor, touchUi2Dbutton._startBGColor, t);
          yield return (object) null;
        }
        touchUi2Dbutton._image.color = touchUi2Dbutton._startBGColor;
        yield return (object) null;
      }
    }

    private IEnumerator V1HoverEffectRoutine(bool highlighted)
    {
      TouchUI2DButton touchUi2Dbutton = this;
      if (VRState.InterationWithUI.Value && touchUi2Dbutton.enabled)
      {
        float timer = 0.0f;
        Color startColor = Color.black;
        if ((UnityEngine.Object) touchUi2Dbutton._Background != (UnityEngine.Object) null)
          startColor = touchUi2Dbutton._Background.color;
        Color newBGColor = highlighted ? touchUi2Dbutton._HoverBGColor : touchUi2Dbutton._startBGColor;
        while ((double) timer < (double) touchUi2Dbutton._skewDuration)
        {
          timer += Time.deltaTime;
          float lerp = timer / touchUi2Dbutton._skewDuration;
          float num = highlighted ? timer / touchUi2Dbutton._skewDuration : (touchUi2Dbutton._skewDuration - timer) / touchUi2Dbutton._skewDuration;
          TouchUI2DButton.ApplySkew(touchUi2Dbutton.label, touchUi2Dbutton._skewFactor * num);
          if ((UnityEngine.Object) touchUi2Dbutton._Background != (UnityEngine.Object) null)
            touchUi2Dbutton.ApplyBGColorChange(startColor, newBGColor, lerp);
          yield return (object) null;
        }
        TouchUI2DButton.ApplySkew(touchUi2Dbutton.label, highlighted ? touchUi2Dbutton._skewFactor : 0.0f);
      }
    }

    private IEnumerator V1ClickEffectRoutine()
    {
      TouchUI2DButton touchUi2Dbutton = this;
      if (VRState.InterationWithUI.Value && touchUi2Dbutton.enabled)
      {
        float timer = 0.0f;
        float maxTime = 0.5f;
        if ((UnityEngine.Object) touchUi2Dbutton.label != (UnityEngine.Object) null)
          touchUi2Dbutton.label.color = touchUi2Dbutton._startTextColor;
        TouchUI2DButton.ApplySkew(touchUi2Dbutton.label, touchUi2Dbutton._skewFactor);
        while ((double) timer < (double) maxTime)
        {
          timer += Time.deltaTime;
          float lerp = timer / maxTime;
          touchUi2Dbutton.ApplyBGColorChange(touchUi2Dbutton._PressColorStart, touchUi2Dbutton._PressColorEnd, lerp);
          yield return (object) null;
        }
        touchUi2Dbutton.ApplyBGColorChange(Color.white, touchUi2Dbutton._startBGColor, 1f);
        timer = 0.0f;
        while ((double) timer < (double) touchUi2Dbutton._skewDuration)
        {
          timer += Time.deltaTime;
          float num = (touchUi2Dbutton._skewDuration - timer) / touchUi2Dbutton._skewDuration;
          TouchUI2DButton.ApplySkew(touchUi2Dbutton.label, touchUi2Dbutton._skewFactor * num);
          yield return (object) null;
        }
      }
    }

    private static void ApplySkew(TMP_Text textComponent, float skewMod)
    {
      if ((UnityEngine.Object) textComponent == (UnityEngine.Object) null || (bool) (UnityEngine.Object) textComponent.GetComponentInChildren<TMP_SubMeshUI>())
        return;
      textComponent.ForceMeshUpdate();
      UnityEngine.Mesh mesh = textComponent.mesh;
      if (!(bool) (UnityEngine.Object) mesh)
        return;
      Vector3[] vertices = mesh.vertices;
      TMP_TextInfo textInfo = textComponent.textInfo;
      int characterCount = textInfo.characterCount;
      for (int index = 0; index < characterCount; ++index)
      {
        TMP_CharacterInfo tmpCharacterInfo = textInfo.characterInfo[index];
        if (tmpCharacterInfo.isVisible)
        {
          int vertexIndex = tmpCharacterInfo.vertexIndex;
          Vector3 vector3 = Vector3.right * skewMod;
          vertices[vertexIndex + 1] += vector3;
          vertices[vertexIndex + 2] += vector3;
        }
      }
      mesh.vertices = vertices;
      textComponent.canvasRenderer.SetMesh(mesh);
    }

    private void ApplyBGColorChange(Color startColor, Color newColor, float lerp)
    {
      if (!((UnityEngine.Object) this._Background != (UnityEngine.Object) null))
        return;
      this._Background.color = Color.Lerp(startColor, newColor, lerp);
    }

    public void SwapBGSprite(Sprite s) => this._Background.sprite = s;

    public string GetButtonText() => this.label.text;

    private enum TransitionOptions
    {
      None,
      v1,
      ChangeColor,
    }
  }
}
