// Decompiled with JetBrains decompiler
// Type: UDB.EmptyTransition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class EmptyTransition : Transition
  {
    public SourceType source;
    public Texture sourceTexture;
    public Texture dissolveTexture;
    public AnimationCurve dissolvePower;
    public bool dissolvePowerNormalized;
    public bool useFromCameraForEmissionTexture;
    public Texture emissionTexture;
    public float emissionThickness = 0.03f;
    private Vector4 parameter;

    protected override void OnPrepare()
    {
      this.SetTranistionState(TransitionState.Prepare);
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      this.emissionTexture = this.CameraSnapshot(Camera.main);
      this.SetMaterialTexture(MaterialType.Source, this.source, this.sourceTexture);
      this.material.SetTexture("_DissolveTex", this.sourceTexture);
      this.material.SetTexture("_EmissionTex", this.sourceTexture);
      this.parameter.y = this.emissionThickness;
      this.SetMaterialTexture(MaterialType.Waiting, SourceType.Texture, this.emissionTexture);
      this.waitingMaterial.SetTexture("_DissolveTex", this.dissolveTexture);
      this.waitingMaterial.SetTexture("_EmissionTex", this.emissionTexture);
    }

    protected override void OnUpdate()
    {
      this.Prepare();
      this.SetTranistionState(TransitionState.Update);
      this.material.SetFloat("_t", this.currentCurveValue);
      this.parameter.x = this.dissolvePower.Evaluate(this.dissolvePowerNormalized ? this.currentTimeNormalized : this.currentTime);
      this.material.SetVector("_Params", this.parameter);
    }

    protected override void OnLingerRender(Texture source, RenderTexture destination)
    {
      this.SetTranistionState(TransitionState.Linger);
      Graphics.Blit(this.emissionTexture, destination, this.material);
    }

    protected override void OnWaitRender(Texture source, RenderTexture destination)
    {
      this.Prepare();
      this.SetTranistionState(TransitionState.Wait);
      Graphics.Blit(this.emissionTexture, destination, this.waitingMaterial);
    }
  }
}
