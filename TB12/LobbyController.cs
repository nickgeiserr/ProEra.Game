// Decompiled with JetBrains decompiler
// Type: TB12.LobbyController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using TB12.AppStates;
using TMPro;
using UnityEngine;

namespace TB12
{
  public class LobbyController : MonoBehaviour
  {
    [SerializeField]
    private LobbyState _lobbyState;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private float _countDelay = 0.7f;
    [SerializeField]
    private int _counts = 5;

    private void OnEnable()
    {
      this._lobbyState.StartCountdown += new Action(this.StartCountdownHandler);
      this._text.enabled = false;
    }

    private void OnDisable() => this._lobbyState.StartCountdown -= new Action(this.StartCountdownHandler);

    private void StartCountdownHandler() => this.StartCoroutine(this.CountdownRoutine());

    private IEnumerator CountdownRoutine()
    {
      this._text.enabled = true;
      for (int i = this._counts; i >= 0; --i)
      {
        this._text.text = string.Format("Starts in: {0}", (object) (i + 1));
        yield return (object) new WaitForSeconds(this._countDelay);
      }
    }
  }
}
