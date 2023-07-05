// Decompiled with JetBrains decompiler
// Type: UDB.SpawnedSFXPlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class SpawnedSFXPlayer : SFXPlayer
  {
    private static GameObject spawnedSFXPlayerObject;
    private static SpawnedSFXPlayer spawnedSFXPlayer;

    protected override void OnAwake()
    {
      if ((Object) SpawnedSFXPlayer.spawnedSFXPlayer == (Object) null)
        Object.DontDestroyOnLoad((Object) this.gameObject);
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    public override void PlayAudioClip(AudioClip audioClip) => base.PlayAudioClip(audioClip);

    public override void Play() => base.Play();

    public override void Stop() => base.Stop();

    protected override void AudioClipFinished(AudioClip audioClip)
    {
      base.AudioClipFinished(audioClip);
      PoolManager.Recycle(this.gameObject);
    }

    protected override void AudioClipStarted(AudioClip audioClip)
    {
    }

    public static void Play2DAudioClip(AudioClip audioClip, SFXPlayer.OnSFXFinish sfxFinishCallback = null)
    {
      SpawnedSFXPlayer.spawnedSFXPlayerObject = PoolManager.Spawn(nameof (SpawnedSFXPlayer));
      SpawnedSFXPlayer.spawnedSFXPlayer = SpawnedSFXPlayer.spawnedSFXPlayerObject.GetComponent<SpawnedSFXPlayer>();
      if (sfxFinishCallback != null)
        SpawnedSFXPlayer.spawnedSFXPlayer.sfxFinishCallback += sfxFinishCallback;
      SpawnedSFXPlayer.spawnedSFXPlayerObject.SetActive(true);
      SpawnedSFXPlayer.spawnedSFXPlayer.PlayAudioClip(audioClip);
    }
  }
}
