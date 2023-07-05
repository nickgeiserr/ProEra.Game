// Decompiled with JetBrains decompiler
// Type: TB12.IntelAnimationFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class IntelAnimationFlow : MonoBehaviour
  {
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private IntelAnimationStore _store;
    [SerializeField]
    private Transform _playerPos;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly int _clipProgressHash = Animator.StringToHash("ClipProgress");

    private void Awake()
    {
      this._store.Reset();
      ControllerInput input1 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right).input;
      ControllerInput input2 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left).input;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._store.isPlaying.Link<bool>((Action<bool>) (isPlaying => this._animator.speed = isPlaying ? 1f : 0.0f)),
        this._store.progress.Link<float>((Action<float>) (progress => this._animator.SetFloat(this._clipProgressHash, progress))),
        input1.buttonOne.Link<bool>((Action<bool>) (state =>
        {
          if (state)
            this._store.isPlaying.Toggle();
          if (!(bool) this._store.isPlaying)
            return;
          this.StartPlayback();
        })),
        input1.buttonTwo.Link<bool>((Action<bool>) (state => this._store.isRewinding = state)),
        input2.buttonOne.Link<bool>((Action<bool>) (state =>
        {
          if (!state)
            return;
          UIDispatch.FrontScreen.ToggleView(EScreens.kIntelAnimation);
        }))
      });
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._playerPos.position, this._playerPos.rotation);
      this.StartPlayback();
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void StartPlayback() => this._store.length.Value = this._animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

    private void Update()
    {
      if (!(bool) this._store.isPlaying && !this._store.isRewinding)
        return;
      this._store.progress.Value = Mathf.Clamp01((float) this._store.progress + Time.deltaTime / this._store.length.Value * (this._store.isRewinding ? -2f : 1f));
    }
  }
}
