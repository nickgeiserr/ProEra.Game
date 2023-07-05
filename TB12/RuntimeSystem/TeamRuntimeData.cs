// Decompiled with JetBrains decompiler
// Type: TB12.RuntimeSystem.TeamRuntimeData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL;
using ProjectConstants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.RuntimeSystem
{
  [Serializable]
  public class TeamRuntimeData : Data
  {
    [SerializeField]
    private ETeamLetters _designatedLetter;
    [SerializeField]
    private List<int> _players = new List<int>();
    [SerializeField]
    private bool _startInHuddle;
    [SerializeField]
    private List<Vector2> _huddlePositions = new List<Vector2>();
    [SerializeField]
    private List<float> _huddleOrientations = new List<float>();
    [SerializeField]
    private PlayRuntimeData playRuntimeData;

    public void Init(DataSensitiveStructs_v5.TeamData storedTeamData, ETeamLetters team, PlayRuntimeData playRuntimeData)
    {
      this.playRuntimeData = playRuntimeData;
      this._designatedLetter = team;
      this._players = new List<int>((IEnumerable<int>) storedTeamData.playerIds);
      this._startInHuddle = PlayRuntimeData.HuddleEnabled;
      this.HandleHuddlePositionsGeneration(this.playRuntimeData.GetBallDataScenePos());
    }

    public bool StartInHuddle => this._startInHuddle;

    public ETeamLetters TeamID => this._designatedLetter;

    public void Deinit()
    {
      this._players.Clear();
      this._players = (List<int>) null;
      this._huddlePositions.Clear();
      this._huddlePositions = (List<Vector2>) null;
      this._huddleOrientations.Clear();
      this._huddleOrientations = (List<float>) null;
      this.playRuntimeData = (PlayRuntimeData) null;
    }

    public bool ContainsPlayerWithID(int id) => this._players.Contains(id);

    public Vector2 GetPlayerHuddlePosition(int id) => this._huddlePositions[this._players.IndexOf(id)];

    public float GetPlayerHuddleOrientation(int id) => this._huddleOrientations[this._players.IndexOf(id)];

    private void HandleHuddlePositionsGeneration(Vector3 ballScenePos)
    {
      if (!this._startInHuddle)
        return;
      this._huddlePositions = TeamRuntimeData.GenerateHuddlePositions(ballScenePos, this._players.Count, this._designatedLetter);
      this._huddleOrientations = TeamRuntimeData.GenerateHuddleOrientations(this._players.Count);
    }

    private static List<Vector2> GenerateHuddlePositions(
      Vector3 ballScenePos,
      int playerCount,
      ETeamLetters designatedLetter)
    {
      List<Vector2> huddlePositions = new List<Vector2>();
      Vector2 dataCoordinates = MathUtils.TransformScenePositionToDataCoordinates(ballScenePos);
      float distanceFromLine = PlayEditorConfiguration.Whiteboard.HuddleGeneration.DistanceFromLine;
      Vector2 vector2_1 = new Vector2(designatedLetter == ETeamLetters.TeamA ? distanceFromLine : -distanceFromLine, 0.0f);
      Vector2 vector2_2 = dataCoordinates + vector2_1;
      float num = (float) playerCount / 6f;
      for (int index = 0; index < playerCount; ++index)
      {
        float f = (float) ((double) index * 3.1415927410125732 * 2.0) / (float) playerCount;
        Vector2 vector2_3 = vector2_2 + new Vector2(Mathf.Cos(f), Mathf.Sin(f)) * num;
        huddlePositions.Add(vector2_3);
      }
      return huddlePositions;
    }

    private static List<float> GenerateHuddleOrientations(int playerCount)
    {
      List<float> huddleOrientations = new List<float>();
      float num1 = -90f;
      float num2 = 360f / (float) playerCount;
      for (int index = 0; index < playerCount; ++index)
      {
        huddleOrientations.Add(num1);
        num1 -= num2;
      }
      return huddleOrientations;
    }
  }
}
