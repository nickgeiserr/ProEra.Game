// Decompiled with JetBrains decompiler
// Type: BackgroundMusicManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
  public static BackgroundMusicManager instance;
  private List<int> trackOrder;
  private AudioClip currentClip;
  private Text guiTitleText;
  private AudioClip[] loadedClips;
  private int trackIndex;
  private AudioSource source;
  private Coroutine delayCoroutine;
  private float startFadeTime;
  private float localTimeScale;
  private float timeBetweenTracks = 0.5f;
  private int numberOfTracks;

  private void Awake()
  {
    if ((Object) BackgroundMusicManager.instance == (Object) null)
    {
      BackgroundMusicManager.instance = this;
      this.source = this.gameObject.GetComponent<AudioSource>();
      Object.DontDestroyOnLoad((Object) this.gameObject);
      this.trackOrder = new List<int>();
    }
    else
      Object.Destroy((Object) this.gameObject);
  }

  private void Start()
  {
    this.trackIndex = -1;
    this.localTimeScale = (double) Time.timeScale == 0.0 ? 1f : Time.timeScale;
    this.StartWithRandom();
  }

  public bool IsPlaying() => this.source.isPlaying;

  public void SetVolume(float v)
  {
    if (!((Object) this.source != (Object) null))
      return;
    this.source.volume = v;
  }

  private void SetTrackOrder() => this.trackOrder = Enumerable.Range(0, this.numberOfTracks).ToList<int>();

  public void ShuffleTrackOrder()
  {
    this.trackOrder.Clear();
    int num1 = 0;
    while (this.trackOrder.Count < this.numberOfTracks)
    {
      int num2 = Random.Range(0, this.numberOfTracks);
      if (!this.trackOrder.Contains(num2))
      {
        this.trackOrder.Add(num2);
        ++num1;
      }
    }
  }

  private IEnumerator WaitForDelay(float delay)
  {
    yield return (object) new WaitForSeconds(delay);
    this.PlayNextTrack();
  }

  private void PlayNextTrack()
  {
    ++this.trackIndex;
    if (this.trackIndex == this.numberOfTracks)
      this.trackIndex = 0;
    int index = this.trackOrder[this.trackIndex];
    if (this.loadedClips != null && (Object) this.loadedClips[index] != (Object) null)
    {
      this.source.clip = this.loadedClips[index];
      this.source.Play();
      this.startFadeTime = (Time.time + this.source.clip.length) * this.localTimeScale;
    }
    float delay = (this.source.clip.length + this.timeBetweenTracks) * this.localTimeScale;
    if (this.delayCoroutine != null)
      this.StopCoroutine(this.delayCoroutine);
    this.delayCoroutine = this.StartCoroutine(this.WaitForDelay(delay));
  }

  public void StartWithFirst()
  {
    this.trackIndex = -1;
    this.PlayNextTrack();
  }

  public void StartWithRandom()
  {
    this.ShuffleTrackOrder();
    this.trackIndex = -1;
    this.PlayNextTrack();
  }

  public void PlayNext() => this.PlayNextTrack();

  public void PlayPrev()
  {
    this.trackIndex -= 2;
    if (this.trackIndex < -1)
      this.trackIndex = this.numberOfTracks - 2;
    this.PlayNextTrack();
  }

  private void Pause()
  {
    if ((Object) this.source == (Object) null)
      return;
    if (this.source.isPlaying)
      this.source.Pause();
    else
      this.source.UnPause();
  }

  public void Stop()
  {
    if ((Object) this.source != (Object) null)
      this.source.Stop();
    if (this.delayCoroutine == null)
      return;
    this.StopCoroutine(this.delayCoroutine);
  }
}
