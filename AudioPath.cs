// Decompiled with JetBrains decompiler
// Type: AudioPath
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioPath
{
  public string path;
  public int count;
  public AudioClip[] audioFiles;
  public bool saveAudioAfterUse;

  public AudioPath(string _path, int _numberOfFiles, bool saveAudio)
  {
    this.path = _path;
    this.count = _numberOfFiles;
    this.saveAudioAfterUse = false;
    if (!this.saveAudioAfterUse)
      return;
    this.audioFiles = new AudioClip[this.count];
  }

  public AudioPath(string p, string filterFiles = null)
  {
    this.path = p;
    this.saveAudioAfterUse = true;
    if (filterFiles == null)
    {
      this.audioFiles = AddressablesData.instance.LoadAssetsSync<AudioClip>(AddressablesData.CorrectingAssetKey(this.path));
    }
    else
    {
      List<AudioClip> audioClipList = new List<AudioClip>();
      AudioClip[] audioClipArray = AddressablesData.instance.LoadAssetsSync<AudioClip>(AddressablesData.CorrectingAssetKey(this.path));
      for (int index = 0; index < audioClipArray.Length; ++index)
      {
        if (audioClipArray[index].name.StartsWith(filterFiles))
          audioClipList.Add(audioClipArray[index]);
      }
      this.audioFiles = audioClipList.ToArray();
    }
    this.count = this.audioFiles.Length;
  }
}
