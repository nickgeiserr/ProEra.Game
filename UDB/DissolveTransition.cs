// Decompiled with JetBrains decompiler
// Type: UDB.DissolveTransition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class DissolveTransition : Transition
  {
    public SourceType source;
    public Texture sourceTexture;
    public Texture dissolveTexture;
    public AnimationCurve dissolvePower;
    public bool dissolvePowerNormalized;
    public bool useFromCameraForEmissionTexture;
    public Texture emissionTexture;
    public bool saveEmissionTextureForNextScene;
    public float emissionThickness = 0.03f;
    private Vector4 parameter;

    protected override void OnPrepare()
    {
      this.SetTranistionState(TransitionState.Prepare);
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      if (this.useFromCameraForEmissionTexture)
      {
        if ((Object) Transition.previousTexture != (Object) null)
        {
          this.emissionTexture = Transition.previousTexture;
          Transition.previousTexture = (Texture) null;
        }
        else
        {
          this.StartCoroutine(this.TakeScreenShot());
          return;
        }
      }
      this.SetupTransition();
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

    private void SetupTransition()
    {
      if (this.saveEmissionTextureForNextScene)
        Transition.previousTexture = this.emissionTexture;
      this.SetMaterialTexture(MaterialType.Source, this.source, this.sourceTexture);
      this.material.SetTexture("_DissolveTex", this.dissolveTexture);
      this.material.SetTexture("_EmissionTex", this.emissionTexture);
      this.parameter.y = this.emissionThickness;
      this.SetMaterialTexture(MaterialType.Waiting, SourceType.Texture, this.emissionTexture);
      this.waitingMaterial.SetTexture("_DissolveTex", this.dissolveTexture);
      this.waitingMaterial.SetTexture("_EmissionTex", this.emissionTexture);
      this.FinishedPreparing();
    }

    private IEnumerator TakeScreenShot()
    {
      Vector2 vector2 = new Vector2((float) Screen.width, (float) Screen.height);
      Camera main = Camera.main;
      Texture2D image = new Texture2D((int) vector2.x, (int) vector2.y);
      RenderTexture currentRT = RenderTexture.active;
      RenderTexture.active = main.targetTexture;
      main.Render();
      yield return (object) new WaitForEndOfFrame();
      image.ReadPixels(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height), 0, 0);
      image.Apply();
      RenderTexture.active = currentRT;
      this.emissionTexture = (Texture) image;
      Transition.previousTexture = (Texture) image;
      this.SetupTransition();
    }
  }
}
