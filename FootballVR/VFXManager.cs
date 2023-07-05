// Decompiled with JetBrains decompiler
// Type: FootballVR.VFXManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class VFXManager : MonoBehaviour
  {
    [SerializeField]
    private TargetHitEffect _firstBumpEffectPrefab;
    [SerializeField]
    private float _fistBumpScale = 0.4f;
    private readonly List<TargetHitEffect> _cache = new List<TargetHitEffect>();

    private void Awake() => GameplayStatics.FistBumpEvent.OnTrigger += new Action<Vector3, Quaternion>(this.HandleFistBumpEvent);

    private void HandleFistBumpEvent(Vector3 location, Quaternion orientation)
    {
      if (this._cache.Count == 0)
      {
        TargetHitEffect effect = UnityEngine.Object.Instantiate<TargetHitEffect>(this._firstBumpEffectPrefab, location, orientation, this.transform);
        effect.gameObject.transform.localScale = new Vector3(this._fistBumpScale, this._fistBumpScale, this._fistBumpScale);
        effect.SimplePlayEffect();
        this.StartCoroutine(this.HideAndReuse(effect));
      }
      else
      {
        TargetHitEffect effect = this._cache[this._cache.Count - 1];
        this._cache.RemoveAt(this._cache.Count - 1);
        effect.gameObject.transform.SetPositionAndRotation(location, orientation);
        effect.gameObject.SetActive(true);
        effect.SimplePlayEffect();
        this.StartCoroutine(this.HideAndReuse(effect));
      }
    }

    private IEnumerator HideAndReuse(TargetHitEffect effect)
    {
      yield return (object) new WaitForSeconds(1f);
      effect.gameObject.SetActive(false);
      this._cache.Add(effect);
    }
  }
}
