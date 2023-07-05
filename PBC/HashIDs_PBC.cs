// Decompiled with JetBrains decompiler
// Type: PBC.HashIDs_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class HashIDs_PBC : MonoBehaviour
  {
    public int angularSpeedFloat;
    public int speedFloat;
    public int strafeSpeedFloat;
    public int strafeTurnFloat;
    public int runWeightFloat;
    public int roundKickBool;
    public int aired;
    public int shooting;
    public int gripped;
    public int frontTrigger;
    public int backTrigger;
    public int frontMirrorTrigger;
    public int backMirrorTrigger;
    public int getupFront;
    public int getupBack;
    public int getupFrontMirror;
    public int getupBackMirror;
    public int anyStateToGetupFront;
    public int anyStateToGetupBack;
    public int anyStateToGetupFrontMirror;
    public int anyStateToGetupBackMirror;

    private void Awake()
    {
      this.angularSpeedFloat = Animator.StringToHash("angularSpeed");
      this.speedFloat = Animator.StringToHash("Speed");
      this.strafeSpeedFloat = Animator.StringToHash("strafeSpeed");
      this.strafeTurnFloat = Animator.StringToHash("strafeTurn");
      this.runWeightFloat = Animator.StringToHash("runWeight");
      this.roundKickBool = Animator.StringToHash("RoundKickBool");
      this.frontTrigger = Animator.StringToHash("GetUpProneTrigger");
      this.backTrigger = Animator.StringToHash("GetUpSupineTrigger");
      this.frontMirrorTrigger = Animator.StringToHash("GetUpProneMirroredTrigger");
      this.backMirrorTrigger = Animator.StringToHash("GetUpSupineMirroredTrigger");
      this.getupFront = Animator.StringToHash("Base Layer.GetUpProne");
      this.getupBack = Animator.StringToHash("Base Layer.GetUpSupine");
      this.getupFrontMirror = Animator.StringToHash("Base Layer.GetUpProneMirrored");
      this.getupBackMirror = Animator.StringToHash("Base Layer.GetUpSupineMirrored");
      this.anyStateToGetupFront = Animator.StringToHash("Entry -> Base Layer.GetUpProne");
      this.anyStateToGetupBack = Animator.StringToHash("Entry -> Base Layer.GetUpSupine");
      this.anyStateToGetupFrontMirror = Animator.StringToHash("Entry -> Base Layer.GetUpProneMirrored");
      this.anyStateToGetupBackMirror = Animator.StringToHash("Entry -> Base Layer.GetUpSupineMirrored");
    }

    private void Start()
    {
      int num = (bool) (Object) this.GetComponent<Animator>() ? 1 : 0;
    }
  }
}
