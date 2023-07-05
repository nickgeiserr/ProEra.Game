// Decompiled with JetBrains decompiler
// Type: PSVRSafetyIsWithinSafeArea
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using UnityEngine;

public class PSVRSafetyIsWithinSafeArea : MonoBehaviour
{
  [SerializeField]
  private GameObject adjustYourViewFeedbackText;
  [SerializeField]
  private TouchButton okButton;
  private float maxY = 0.55f;
  private float minY = 0.47f;
  private float maxX = 0.55f;
  private float minX = 0.48f;

  private void Awake()
  {
  }

  private void Update()
  {
    Vector3 viewportPoint = PlayerCamera.Camera.WorldToViewportPoint(this.transform.position);
    if ((double) viewportPoint.x > (double) this.minX && (double) viewportPoint.x < (double) this.maxX && (double) viewportPoint.y > (double) this.minY && (double) viewportPoint.y < (double) this.maxY)
    {
      this.okButton.SetInteractible(true);
      if (!((Object) this.adjustYourViewFeedbackText != (Object) null))
        return;
      this.adjustYourViewFeedbackText.SetActive(false);
    }
    else
    {
      this.okButton.SetInteractible(false);
      if (!((Object) this.adjustYourViewFeedbackText != (Object) null))
        return;
      this.adjustYourViewFeedbackText.SetActive(true);
    }
  }
}
