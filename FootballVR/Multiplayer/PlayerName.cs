// Decompiled with JetBrains decompiler
// Type: FootballVR.Multiplayer.PlayerName
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.Multiplayer
{
  public class PlayerName : MonoBehaviour
  {
    [SerializeField]
    private Transform _nameTx;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private Image _speakerIcon;
    [SerializeField]
    private Image _muteIcon;
    [SerializeField]
    private Image _hostIcon;
    [SerializeField]
    private float _scaleMultiplier = 0.12f;
    [SerializeField]
    private float _minScale = 1f;
    [SerializeField]
    private float _height = 0.2f;
    [SerializeField]
    private float _offset = 0.3f;
    private Transform _camTx;
    private Transform _headTx;
    private bool _initialized;

    public bool SpeakerEnabled
    {
      set => this._speakerIcon.enabled = value;
      get => this._speakerIcon.enabled;
    }

    private void Awake()
    {
      this.SpeakerEnabled = false;
      this._muteIcon.gameObject.SetActive(false);
      if (this._initialized)
        return;
      this.enabled = false;
    }

    public void Initialize(Transform headTx)
    {
      if ((Object) this._nameTx == (Object) null)
      {
        Debug.LogError((object) "Player Name transform missing.");
      }
      else
      {
        if (this._initialized)
          return;
        this._camTx = PersistentSingleton<PlayerCamera>.Instance.transform;
        this._headTx = headTx;
        this._initialized = true;
        this.enabled = true;
      }
    }

    private void Update()
    {
      Vector3 position1 = this._camTx.position;
      Vector3 position2 = this._headTx.position;
      float num = Mathf.Clamp((position2 - position1).magnitude * this._scaleMultiplier, this._minScale, float.MaxValue);
      position2.y += (this._height * num + this._offset) * this._headTx.lossyScale.x;
      this._nameTx.position = position2;
      this._nameTx.LookAt(2f * position2 - position1, Vector3.up);
      this._nameTx.localScale = num * Vector3.one;
    }

    public void SetName(string name)
    {
      if (!((Object) this._nameText != (Object) null))
        return;
      this._nameText.text = name;
    }

    public void SetColor(Color color) => this._nameText.color = color;

    public void SetState(bool state) => this.gameObject.SetActive(state);

    public void SetVisible(bool state) => this._nameText.enabled = state;

    public void SetMuted(bool muted)
    {
      this._speakerIcon.gameObject.SetActive(!muted);
      this._muteIcon.gameObject.SetActive(muted);
    }

    public void CheckHostIcon(bool turnOn) => this._hostIcon.enabled = turnOn;
  }
}
