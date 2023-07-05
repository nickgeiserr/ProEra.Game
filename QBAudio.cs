// Decompiled with JetBrains decompiler
// Type: QBAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class QBAudio : MonoBehaviour
{
  private string rootFolder = "qbaudio/";
  public AudioPath audiblePOne;
  public AudioPath audiblePTwo;
  public AudioPath cadencePOne;
  public AudioPath cadencePTwo;
  public AudioPath snapPOne;
  public AudioPath snapPTwo;

  private PlayersManager playersManager => MatchManager.instance.playersManager;

  public void LoadAudioPaths() => this.SetPaths("DEFAULT/", "DEFAULT/");

  public void SetVolume(float vol)
  {
  }

  private void SetPaths(string teamFolderPOne, string teamFolderPTwo)
  {
    string str1 = "AUDIBLE";
    this.audiblePOne = new AudioPath(this.rootFolder + teamFolderPOne + str1);
    string str2 = "CADENCE";
    this.cadencePOne = new AudioPath(this.rootFolder + teamFolderPOne + str2);
    string str3 = "SNAP";
    this.snapPOne = new AudioPath(this.rootFolder + teamFolderPOne + str3);
    string str4 = "AUDIBLE";
    this.audiblePTwo = new AudioPath(this.rootFolder + teamFolderPTwo + str4);
    string str5 = "CADENCE";
    this.cadencePTwo = new AudioPath(this.rootFolder + teamFolderPTwo + str5);
    string str6 = "SNAP";
    this.snapPTwo = new AudioPath(this.rootFolder + teamFolderPTwo + str6);
  }

  public void PlayAudible()
  {
    int index = Random.Range(0, this.audiblePOne.count) + 1;
    if (Game.IsPlayerOneOnOffense)
      this.PlayAudioClip(AudioManager.self.GetAudioClip(this.audiblePOne, index));
    else
      this.PlayAudioClip(AudioManager.self.GetAudioClip(this.audiblePTwo, index));
  }

  public void PlaySnap()
  {
    int index = Random.Range(0, this.snapPOne.count) + 1;
    if (Game.IsPlayerOneOnOffense)
      this.PlayAudioClip(AudioManager.self.GetAudioClip(this.snapPOne, index));
    else
      this.PlayAudioClip(AudioManager.self.GetAudioClip(this.snapPTwo, index));
  }

  public void PlayCadence()
  {
  }

  private void PlayAudioClip(AudioClip audioClip)
  {
  }
}
