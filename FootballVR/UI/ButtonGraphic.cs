// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.ButtonGraphic
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using UnityEngine.Serialization;

namespace FootballVR.UI
{
  public class ButtonGraphic : MonoBehaviour
  {
    [FormerlySerializedAs("_textTx")]
    [SerializeField]
    protected Transform _containerTx;
    [SerializeField]
    private bool _moveToParent;

    public bool movesToParent => this._moveToParent;

    public Transform containerTx => this._containerTx;

    protected virtual void Awake()
    {
      if (!this._moveToParent || !((Object) this._containerTx != (Object) null))
        return;
      this._containerTx.SetParent(this.transform.parent);
      this._containerTx.SetAsLastSibling();
    }

    private void OnDestroy()
    {
      if (!((Object) this._containerTx != (Object) null))
        return;
      Objects.SafeDestroy((Object) this._containerTx.gameObject);
    }

    private void OnEnable()
    {
      if (!this._moveToParent)
        return;
      this._containerTx.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
      if (!this._moveToParent)
        return;
      this._containerTx.gameObject.SetActive(false);
    }
  }
}
