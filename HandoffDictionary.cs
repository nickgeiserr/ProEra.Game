// Decompiled with JetBrains decompiler
// Type: HandoffDictionary
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class HandoffDictionary : ScriptableObject
{
  [SerializeField]
  private List<HandoffDictionary.HandoffTypeControllerPair> _typeControllerPairs;
  private Dictionary<int, InteractionController> _typeControllerDictionary;

  public InteractionController GetController(int handoffType)
  {
    if (this._typeControllerDictionary == null || this._typeControllerDictionary.Count == 0)
    {
      this._typeControllerDictionary = new Dictionary<int, InteractionController>();
      for (int index = 0; index < this._typeControllerPairs.Count; ++index)
        this._typeControllerDictionary.Add((int) this._typeControllerPairs[index].type, this._typeControllerPairs[index].controller);
    }
    return this._typeControllerDictionary[handoffType];
  }

  [Serializable]
  public class HandoffTypeControllerPair
  {
    public HandoffType type;
    public InteractionController controller;
  }
}
