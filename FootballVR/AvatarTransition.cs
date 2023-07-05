// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarTransition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public static class AvatarTransition
  {
    private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");
    private static MaterialPropertyBlock _mpb;
    private static bool _initialized;
    private static readonly int Fade = Shader.PropertyToID("_Fade");

    private static MaterialPropertyBlock GetMPB()
    {
      if (AvatarTransition._initialized)
        return AvatarTransition._mpb;
      AvatarTransition._initialized = true;
      return AvatarTransition._mpb = new MaterialPropertyBlock();
    }

    public static IEnumerator Apply(
      AvatarGraphics avatar,
      bool appear,
      float duration = 2f,
      float delay = 0.0f,
      bool enableAvatars = true)
    {
      return AvatarTransition.Apply((IList<AvatarGraphics>) new AvatarGraphics[1]
      {
        avatar
      }, appear, duration, delay, enableAvatars: enableAvatars);
    }

    public static IEnumerator Apply(
      IList<AvatarGraphics> avatars,
      bool appear,
      float duration = 2f,
      float delay = 0.0f,
      bool global = false,
      bool enableAvatars = true)
    {
      UniformStore.DissolvedEffect.SetValue(true);
      if (global)
      {
        foreach (AvatarGraphics avatar in (IEnumerable<AvatarGraphics>) avatars)
          avatar.StopTransition();
      }
      float startValue = appear ? 1f : 0.0f;
      float endValue = appear ? 0.0f : 1f;
      AvatarTransition.ApplyValue(avatars, startValue);
      if ((double) delay > 0.0)
        yield return (object) new WaitForSeconds(delay);
      if (enableAvatars)
      {
        foreach (Component avatar in (IEnumerable<AvatarGraphics>) avatars)
          avatar.gameObject.SetActive(true);
      }
      float elapsedTime = 0.0f;
      float t = 0.0f;
      while ((double) t < 1.0)
      {
        yield return (object) null;
        elapsedTime += Time.deltaTime;
        t = elapsedTime / duration;
        AvatarTransition.ApplyValue(avatars, Mathf.Lerp(startValue, endValue, t));
      }
      AvatarTransition.ApplyValue(avatars, endValue);
      if (global)
        UniformStore.DissolvedEffect.SetValue(false);
      if (!appear)
      {
        foreach (Component avatar in (IEnumerable<AvatarGraphics>) avatars)
          avatar.gameObject.SetActive(false);
      }
    }

    public static void ApplyValue(IList<AvatarGraphics> avatars, float value)
    {
      MaterialPropertyBlock mpb = AvatarTransition.GetMPB();
      foreach (AvatarGraphics avatar in (IEnumerable<AvatarGraphics>) avatars)
      {
        IReadOnlyList<Renderer> renderers = avatar.Renderers;
        int count1 = renderers.Count;
        for (int index = 0; index < count1; ++index)
        {
          renderers[index].GetPropertyBlock(mpb);
          mpb.SetFloat(AvatarTransition.Dissolve, value);
          renderers[index].SetPropertyBlock(mpb);
        }
        IReadOnlyList<Renderer> shadowRenderers = avatar.ShadowRenderers;
        int count2 = shadowRenderers.Count;
        for (int index = 0; index < count2; ++index)
        {
          shadowRenderers[index].GetPropertyBlock(mpb);
          mpb.SetFloat(AvatarTransition.Fade, value);
          shadowRenderers[index].SetPropertyBlock(mpb);
        }
      }
    }
  }
}
