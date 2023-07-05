// Decompiled with JetBrains decompiler
// Type: PlayerSpawnEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

public class PlayerSpawnEffect : MonoBehaviour
{
  [SerializeField]
  private Animator _animator;
  [SerializeField]
  private Renderer _renderer;
  private static readonly int VisibleProp = Animator.StringToHash("Visible");

  public void Play()
  {
    this._renderer.enabled = true;
    this._animator.SetBool(PlayerSpawnEffect.VisibleProp, true);
    this.StartCoroutine(this.DelayedDisableRenderer());
  }

  private IEnumerator DelayedDisableRenderer()
  {
    yield return (object) new WaitForSeconds(1f);
    this._renderer.enabled = false;
  }
}
