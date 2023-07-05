// Decompiled with JetBrains decompiler
// Type: TB12.ThrowGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using System.Collections.Generic;
using TB12.GameplayData;
using UnityEngine;

namespace TB12
{
  public class ThrowGameScene : MonoBehaviour
  {
    private UniformStore _uniformStore;
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private QbStore _qbStore;
    [SerializeField]
    private OpponentQB _qbPrefab;
    [SerializeField]
    private Transform[] _qbPositions;
    private ManagedList<OpponentQB> _qbs;

    public bool Done
    {
      get
      {
        if (this._qbs == null)
          return true;
        foreach (OpponentQB qb in this._qbs)
        {
          if (!qb.Done)
            return false;
        }
        return true;
      }
    }

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      this._qbs = new ManagedList<OpponentQB>((IObjectPool<OpponentQB>) new MonoBehaviorObjectPool<OpponentQB>(this._qbPrefab, this.transform));
    }

    public void LoadOpponents(string[] qbIds, ThrowLevel level)
    {
      List<OpponentData> opponentDatas = this._gameplayStore.OpponentDatas;
      opponentDatas.Clear();
      int length = qbIds.Length;
      float num = length < 2 ? 0.8f : 2f;
      List<QbData> qbDataList = new List<QbData>();
      List<string> names = new List<string>(length);
      List<int> numbers = new List<int>(length);
      List<UniformConfig> uniformConfigList = new List<UniformConfig>();
      for (int index = 0; index < length; ++index)
      {
        QbData data = this._qbStore.GetData(qbIds[index]);
        qbDataList.Add(data);
        names.Add(data.lastName.ToUpper());
        numbers.Add(data.number);
        UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(data.team, ETeamUniformFlags.Home);
        uniformConfigList.Add(uniformConfig);
      }
      UniformCapture.GenerateUniforms(1, numbers, names, ETeamUniformFlags.Home);
      this._qbs.SetCount(length);
      for (int index = 0; index < length; ++index)
      {
        QbData qbData1 = qbDataList[index];
        Transform qbPosition = index < this._qbPositions.Length ? this._qbPositions[index] : (Transform) null;
        OpponentQB qb = this._qbs[index];
        OpponentData opponentData = index == 0 ? this._gameplayStore.OpponentData : new OpponentData();
        ThrowLevel level1 = level;
        OpponentData data = opponentData;
        Transform qbPos = qbPosition;
        QbData qbData2 = qbData1;
        int playerIndex = index;
        double maxDelay = (double) num;
        qb.SetupQb(level1, data, qbPos, qbData2, playerIndex, (float) maxDelay);
        opponentDatas.Add(opponentData);
      }
    }

    public void CleanupScene()
    {
      foreach (OpponentQB qb in this._qbs)
        qb.CleanupScene();
    }
  }
}
