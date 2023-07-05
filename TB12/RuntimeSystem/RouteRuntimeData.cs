// Decompiled with JetBrains decompiler
// Type: TB12.RuntimeSystem.RouteRuntimeData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.RuntimeSystem
{
  [Serializable]
  public class RouteRuntimeData : Data
  {
    public Keyframe[] xAxisKeyframes;
    public Keyframe[] zAxisKeyframes;
    public EMovementType movementType;
    public float startTime;
    public float endTime;
    public ERouteStage stage;
    [SerializeField]
    private float _timeLenght;
    public int anchorId;
    public System.Type anchorType;

    public float TimeLength => this._timeLenght;

    public int KeyCount => this.xAxisKeyframes.Length;

    public void Init(RouteData storedRouteData, int parentId, System.Type parentType)
    {
      this.id = storedRouteData.id;
      List<Vector2> coordinates = storedRouteData.coordinates;
      List<float> timeOffsets = storedRouteData.timeOffsets;
      this.movementType = storedRouteData.movementType;
      int count = coordinates.Count;
      this.stage = storedRouteData.routeStage;
      this.startTime = timeOffsets[0];
      this.endTime = timeOffsets[count - 1];
      this._timeLenght = Mathf.Abs(this.endTime - this.startTime);
      this._timeLenght += storedRouteData.routeStage == ERouteStage.PostSnap ? this.startTime : this.endTime;
      this.anchorId = parentId;
      this.anchorType = parentType;
      this.xAxisKeyframes = new Keyframe[count];
      this.zAxisKeyframes = new Keyframe[count];
      for (int index = 0; index < count; ++index)
      {
        Vector2 vector2 = coordinates[index];
        float time = timeOffsets[index];
        this.xAxisKeyframes[index] = new Keyframe(time, vector2.x);
        this.zAxisKeyframes[index] = new Keyframe(time, vector2.y);
      }
    }

    public void Deinit()
    {
      this.xAxisKeyframes = (Keyframe[]) null;
      this.zAxisKeyframes = (Keyframe[]) null;
      this.startTime = 0.0f;
      this.endTime = 0.0f;
      this.anchorId = -1;
      this.anchorType = (System.Type) null;
    }
  }
}
