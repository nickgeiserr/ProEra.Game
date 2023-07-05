// Decompiled with JetBrains decompiler
// Type: VrOrientationRotator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VrOrientationRotator : MonoBehaviour
{
  public static VrOrientationRotator Instance;
  [Header("VR SDK References")]
  [SerializeField]
  private Transform sourceTransform;
  [SerializeField]
  private Transform centerEyeAnchor;
  [Header("Rotation Properties")]
  [SerializeField]
  private int degreesPerRotation = 45;
  [SerializeField]
  private float rotationCooldownTime = 0.25f;
  [Header("Input Properties")]
  [SerializeField]
  private float minimumAxisInputToRotate = 0.75f;
  [Header("Asset References")]
  [SerializeField]
  private string lockerRoomSceneName = "LockerRoomUI";
  private bool onCooldown;
  private int numRotationsLeft;
  private int numRotationsRight;
  private bool playerIsInLockerRoom;

  private void OnEnable()
  {
    VrOrientationRotator.Instance = this;
    SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
    SceneManager.sceneUnloaded += new UnityAction<Scene>(this.OnSceneUnloaded);
  }

  private void OnDisable()
  {
    VrOrientationRotator.Instance = (VrOrientationRotator) null;
    SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
    SceneManager.sceneUnloaded -= new UnityAction<Scene>(this.OnSceneUnloaded);
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    if (!scene.name.Contains(this.lockerRoomSceneName))
      return;
    this.playerIsInLockerRoom = true;
  }

  private void OnSceneUnloaded(Scene scene)
  {
    if (!scene.name.Contains(this.lockerRoomSceneName))
      return;
    this.playerIsInLockerRoom = false;
  }

  private void Update()
  {
    bool rotateToRight;
    if (this.onCooldown || !this.playerIsInLockerRoom || !this.CheckPlayerInputRotation(out rotateToRight))
      return;
    this.RotateOrientation(this.sourceTransform, this.centerEyeAnchor, rotateToRight, (float) this.degreesPerRotation);
  }

  private bool CheckPlayerInputRotation(out bool rotateToRight)
  {
    Vector2 vector2 = VRInputManager.Get(VRInputManager.Axis2D.Primary2DAxis, VRInputManager.Controller.RightHand);
    rotateToRight = (double) vector2.x > 0.0;
    return !VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand) && (double) Mathf.Abs(vector2.x) > (double) this.minimumAxisInputToRotate;
  }

  private void RotateOrientation(
    Transform sourceTransform,
    Transform anchor,
    bool rotateToTheRight,
    float rotationAmount)
  {
    this.StartCoroutine(this.Rotate(sourceTransform, anchor, rotateToTheRight, rotationAmount));
  }

  private IEnumerator Rotate(
    Transform sourceTransform,
    Transform anchor,
    bool rotateToTheRight,
    float rotationAmount)
  {
    VrOrientationRotator orientationRotator = this;
    orientationRotator.onCooldown = true;
    sourceTransform.RotateAround(anchor.position, orientationRotator.transform.up, rotationAmount * (rotateToTheRight ? 1f : -1f));
    if (rotateToTheRight)
      ++orientationRotator.numRotationsRight;
    else
      ++orientationRotator.numRotationsLeft;
    yield return (object) new WaitForSeconds(orientationRotator.rotationCooldownTime);
    orientationRotator.onCooldown = false;
  }

  public void ResetToDefaultRotation()
  {
    if (this.numRotationsLeft == this.numRotationsRight)
      return;
    this.RotateOrientation(this.sourceTransform, this.centerEyeAnchor, this.numRotationsLeft > this.numRotationsRight, (float) (this.degreesPerRotation * Mathf.Abs(this.numRotationsLeft - this.numRotationsRight)));
    this.numRotationsRight = 0;
    this.numRotationsLeft = 0;
  }
}
