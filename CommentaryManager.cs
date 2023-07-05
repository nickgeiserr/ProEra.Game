// Decompiled with JetBrains decompiler
// Type: CommentaryManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentaryManager : MonoBehaviour
{
  public AnnouncerAudio announcer;
  public AnalystAudio analyst;
  public ReporterAudio reporter;
  public static float defaultPauseTime = 0.4f;
  [SerializeField]
  private AudioSource audioSource;
  private Queue<AudioSegment> announcerQueue;
  private bool isPlaying;
  private float timeOfClipsWaitingToPlay;
  [HideInInspector]
  public PlayerAI offPlayerReference;
  [HideInInspector]
  public PlayerAI defPlayerReference;
  public PlayDirection playDirection;
  public bool defenseBlitzed;
  public bool insideRun;
  public bool outsideRun;
  public bool sacked;
  public bool passCaughtInTraffic;
  public bool passCaughtWideOpen;
  public bool setRunType;
  public float tackledAtPos;
  private WaitForSecondsRealtime _waitForAudioSegment;

  private void Awake() => this.announcerQueue = new Queue<AudioSegment>();

  public void Init()
  {
    this.isPlaying = false;
    this.timeOfClipsWaitingToPlay = 0.0f;
  }

  private IEnumerator LoadAllAudioPaths()
  {
    WaitForSeconds loadDelay = new WaitForSeconds(1f);
    yield return (object) loadDelay;
    this.announcer.LoadAudioPaths();
    this.announcer.LoadTeamAudio();
    this.announcer.PreloadPopularPlayerNames();
    yield return (object) loadDelay;
    this.analyst.LoadAudioPaths();
    this.analyst.LoadTeamAudio();
    this.PlayPregameIntro();
    yield return (object) loadDelay;
    this.reporter.LoadAudioPaths();
    this.reporter.LoadTeamAudio();
  }

  public void ForceLoadAudioPaths()
  {
    this.announcer.LoadAudioPaths();
    this.announcer.LoadTeamAudio();
    this.analyst.LoadAudioPaths();
    this.analyst.LoadTeamAudio();
    this.reporter.LoadAudioPaths();
    this.reporter.LoadTeamAudio();
  }

  public void LoadAllTeamAudio()
  {
    this.analyst.LoadTeamAudio();
    this.announcer.LoadTeamAudio();
    this.reporter.LoadTeamAudio();
  }

  public AudioSource GetAudioSource() => this.audioSource;

  public void ClearQueue()
  {
    this.timeOfClipsWaitingToPlay = 0.0f;
    this.announcerQueue.Clear();
  }

  public void ClearQueueAfterThis() => this.AddSegmentToQueue(new AudioSegment(0.0f, true));

  public void StopCurrentClip()
  {
    this.ClearQueue();
    this.audioSource.Stop();
    this.StopAllCoroutines();
    this.isPlaying = false;
    this.timeOfClipsWaitingToPlay = 0.0f;
  }

  public bool IsPlaying() => this.isPlaying;

  public void AddSegmentToQueue(AudioSegment audioSegment)
  {
    if ((Object) audioSegment.GetAudioClip() != (Object) null)
      this.timeOfClipsWaitingToPlay += audioSegment.GetAudioClip().length;
    this.timeOfClipsWaitingToPlay += audioSegment.GetDelayAfter();
    this.announcerQueue.Enqueue(audioSegment);
    this.CheckForImmediatePlay();
  }

  private void CheckForNextSegment()
  {
    if (this.announcerQueue.Count == 0)
    {
      this.StopAllCoroutines();
      this.isPlaying = false;
      this.timeOfClipsWaitingToPlay = 0.0f;
    }
    else
      this.PlayNextAudioSegment();
  }

  private void CheckForImmediatePlay()
  {
    if (this.isPlaying)
      return;
    this.PlayNextAudioSegment();
  }

  private void PlayNextAudioSegment()
  {
    AudioSegment audioSegment = this.announcerQueue.Dequeue();
    if (audioSegment.ClearQueueAfterPlay())
    {
      this.ClearQueue();
      this.isPlaying = false;
    }
    else
    {
      AudioClip audioClip = audioSegment.GetAudioClip();
      float num = 0.0f;
      if ((Object) audioClip != (Object) null)
      {
        this.audioSource.clip = audioClip;
        this.audioSource.PlayScheduled(AudioSettings.dspTime + 0.079999998211860657);
        num = audioClip.length;
      }
      float delay = num + audioSegment.GetDelayAfter();
      this.isPlaying = true;
      this.StartCoroutine(this.WaitForAudioSegment(delay));
    }
  }

  private IEnumerator WaitForAudioSegment(float delay)
  {
    this._waitForAudioSegment = new WaitForSecondsRealtime(delay);
    yield return (object) this._waitForAudioSegment;
    this.CheckForNextSegment();
  }

  public void AddPause() => this.AddPause(CommentaryManager.defaultPauseTime);

  public void AddPause(float pauseTime) => this.AddSegmentToQueue(new AudioSegment((AudioClip) null, pauseTime));

  public void ResetCommentVariables()
  {
    this.announcer.ResetCommentVariables();
    this.analyst.ResetCommentVariables();
    this.playDirection = PlayDirection.None;
    this.defenseBlitzed = false;
    this.insideRun = false;
    this.outsideRun = false;
    this.sacked = false;
    this.passCaughtInTraffic = false;
    this.passCaughtWideOpen = false;
    this.setRunType = false;
  }

  public void DetermineReceiverOpenness()
  {
    Vector3 position = MatchManager.instance.playersManager.ballHolderScript.trans.position;
    List<PlayerAI> playerAiList = !global::Game.IsPlayerOneOnDefense ? MatchManager.instance.playersManager.curCompScriptRef : MatchManager.instance.playersManager.curUserScriptRef;
    float num1 = 3f;
    float num2 = 5f;
    int num3 = 0;
    int num4 = 0;
    for (int index = 0; index < playerAiList.Count; ++index)
    {
      double num5 = (double) Vector3.Distance(position, playerAiList[index].trans.position);
      if (num5 < (double) num1)
        ++num3;
      if (num5 < (double) num2)
        ++num4;
    }
    if (num4 == 0)
      this.passCaughtWideOpen = true;
    if (num3 <= 2)
      return;
    this.passCaughtInTraffic = true;
  }

  public int DetermineGainOrLoss()
  {
    if ((bool) ProEra.Game.MatchState.Turnover)
      return 0;
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    if ((Object) ballHolderScript == (Object) null)
      return 0;
    return (double) Field.FindDifference(ballHolderScript.trans.position.z, ProEra.Game.MatchState.BallOn.Value) / 24.0 * 50.0 > 0.0 ? 1 : -1;
  }

  public float GetYardsGained() => (bool) ProEra.Game.MatchState.Turnover || (Object) MatchManager.instance.playersManager.ballHolderScript == (Object) null || global::Game.PET_IsIncomplete ? 0.0f : (float) (((double) ProEra.Game.MatchState.BallOn.Value - (double) MatchManager.instance.savedLineOfScrim) / 24.0 * 50.0);

  public bool GotFirstDownOnPlay() => Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value);

  public bool IsRunnerABack()
  {
    if (global::Game.BallHolderIsNull)
      return false;
    Position playerPosition = MatchManager.instance.playersManager.ballHolderScript.playerPosition;
    return playerPosition == Position.RB || playerPosition == Position.FB;
  }

  public bool IsHandoffToRightSide() => (double) MatchManager.instance.playersManager.ballHolderScript.trans.position.x > (double) MatchManager.instance.ballHashPosition;

  public bool IsHandoffToLeftSide() => (double) MatchManager.instance.playersManager.ballHolderScript.trans.position.x < (double) MatchManager.instance.ballHashPosition;

  public bool IsHandoffToStrongSide()
  {
    FormationPositions formation = MatchManager.instance.playManager.savedOffPlay.GetFormation();
    return this.IsHandoffToLeftSide() && formation.IsTELeft() && !formation.IsTERight() || this.IsHandoffToRightSide() && formation.IsTERight() && !formation.IsTELeft();
  }

  public bool IsHandoffToWeakSide()
  {
    FormationPositions formation = MatchManager.instance.playManager.savedOffPlay.GetFormation();
    return this.IsHandoffToLeftSide() && !formation.IsTELeft() && formation.IsTERight() || this.IsHandoffToRightSide() && !formation.IsTERight() && formation.IsTELeft();
  }

  public bool IsHandoffInside() => (double) Mathf.Abs(MatchManager.instance.playManager.handOffTarget.trans.position.x - MatchManager.instance.ballHashPosition) < 2.0;

  public void SetRunType()
  {
    this.setRunType = true;
    if (this.IsHandoffInside())
      this.insideRun = true;
    else
      this.outsideRun = true;
  }

  public void SetPassType(PlayerAI intendedReceiver)
  {
    if ((double) MatchManager.instance.playersManager.passDestination.z >= 0.0)
      return;
    if (intendedReceiver.playerPosition == Position.SLT || intendedReceiver.playerPosition == Position.WR)
      this.playDirection = PlayDirection.WRScreen;
    else
      this.playDirection = PlayDirection.InsideScreen;
  }

  public void PlayPregameIntro()
  {
    Random.Range(0, 100);
    this.announcer.PlayPregameGeneric();
    this.announcer.PlayAnnouncerIntroduction();
    int num = Mathf.Abs(PersistentData.GetUserTeamRating()[0] - PersistentData.GetCompTeamRating()[0]);
    if (global::Game.IsSeasonMode && PersistentData.seasonWeek == 19)
      this.analyst.PlayPregameChampionship();
    else if (global::Game.IsSeasonMode && PersistentData.seasonWeek > 16)
      this.analyst.PlayPregamePlayoffs();
    else if (PersistentData.weather == 1 && Random.Range(0, 100) < 75)
      this.analyst.PlayPregameRain();
    else if (PersistentData.weather == 2 && Random.Range(0, 100) < 75)
      this.analyst.PlayPregameSnow();
    else if (num < 3 && PersistentData.GetUserTeamRating()[0] > 85 && Random.Range(0, 100) < 75)
      this.analyst.PlayPregameCloseMatchup();
    else if (num > 12 && Random.Range(0, 100) < 75)
      this.analyst.PlayPregameLopsidedMatchup();
    else
      this.analyst.PlayPregameGeneric();
    this.AddPause();
    this.announcer.PlayPregameResponseToAnalyst();
    this.AddPause();
    Debug.LogWarning((object) "CD: No Axis weather. Commented out the weather remark as if was a hassle to null wrap");
    if (global::Game.IsSeasonMode && PersistentData.seasonWeek == 19)
      this.announcer.PlayPregameFinalRemarksChampionship();
    else if (global::Game.IsSeasonMode && PersistentData.seasonWeek > 16)
      this.announcer.PlayPregameFinalRemarksPlayoffs();
    else if (num < 3 && PersistentData.GetUserTeamRating()[0] > 85 && Random.Range(0, 100) < 50)
      this.announcer.PlayPregameFinalRemarksCloseMatchup();
    else
      this.announcer.PlayPregameFinalRemarksGeneric();
    this.ClearQueueAfterThis();
  }

  public void PlayHalftimeReportSequence()
  {
    if (this.reporter.playedHalftimeReport)
      return;
    this.announcer.PlayCalldownToReporter();
    this.AddPause();
    this.reporter.PlayHalftimeReport();
    this.AddPause();
    this.announcer.PlayResponseToReporter();
    this.ClearQueueAfterThis();
  }

  public void PlayWeatherReportSequence()
  {
    Debug.LogWarning((object) "CD: Forcing weather report off");
    if ((Object) PersistentData.stadiumSet == (Object) null)
      this.reporter.playedWeatherReport = true;
    else if (!PersistentData.stadiumSet.allowsPrecipitation || !PersistentData.stadiumSet.affectedByWind)
      this.reporter.playedWeatherReport = true;
    if (PersistentData.weather != 1 && PersistentData.weather != 2 && PersistentData.windType != 2)
      this.reporter.playedWeatherReport = true;
    if (this.reporter.playedWeatherReport)
      return;
    this.announcer.PlayCalldownToReporter();
    this.AddPause();
    this.reporter.PlayWeatherReport();
    this.AddPause();
    this.announcer.PlayResponseToReporter();
    this.ClearQueueAfterThis();
  }

  public void PlayPlayerOfGame(bool onUserTeam, int indexOnTeam)
  {
    this.ClearQueue();
    this.announcer.PlayPlayerOfGameIntro();
    this.AddPause();
    this.analyst.PlayPlayerOfGameSelection(onUserTeam, indexOnTeam);
    this.AddPause();
    this.analyst.PlayPlayerOfGameOutro();
    this.ClearQueueAfterThis();
  }
}
