// Decompiled with JetBrains decompiler
// Type: UDB.DistortTransition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class DistortTransition : Transition
  {
    public SourceType source;
    public Texture sourceTexture;
    public Texture distortTexture;
    public float distortTime = 0.35f;
    public AnimationCurve distortMagnitude;
    public bool distortMagnitudeNormalized;
    public Vector2 force = new Vector2(0.2f, 0.2f);
    private Vector4 parameter;

    protected override void OnPrepare()
    {
      this.SetTranistionState(TransitionState.Prepare);
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      this.SetMaterialTexture(MaterialType.Source, this.source, this.sourceTexture);
      this.material.SetTexture("_DistortTex", this.distortTexture);
      this.parameter.x = this.force.x;
      this.parameter.y = this.force.y;
      this.parameter.z = this.distortTime;
      this.material.SetVector("_Params", this.parameter);
    }

    protected override void OnUpdate()
    {
      this.Prepare();
      this.material.SetFloat("_t", this.currentCurveValue);
      this.material.SetFloat("_distortT", this.distortMagnitude.Evaluate(this.distortMagnitudeNormalized ? this.currentTimeNormalized : this.currentTime));
    }

    protected override void OnLingerRender(Texture source, RenderTexture destination) => this.SetTranistionState(TransitionState.Linger);

    protected override void OnWaitRender(Texture source, RenderTexture destination)
    {
      this.Prepare();
      this.SetTranistionState(TransitionState.Wait);
    }
  }
}
