// Decompiled with JetBrains decompiler
// Type: ControllerSupport.User
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace ControllerSupport
{
  public class User : MonoBehaviour
  {
    public GameObject _gameObject;
    public int realUserIndex;
    public int userIndex;

    public UserActions Actions { get; set; }

    private void Awake() => Object.DontDestroyOnLoad((Object) this._gameObject);

    private void OnDisable()
    {
      if (this.Actions == null)
        return;
      this.Actions.Destroy();
    }

    public void AssignController(int index) => this.realUserIndex = index;
  }
}
