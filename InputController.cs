// Decompiled with JetBrains decompiler
// Type: InputController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class InputController : MonoBehaviour
{
  public static InputController instance;

  private void Awake()
  {
    if ((Object) InputController.instance == (Object) null)
    {
      InputController.instance = this;
    }
    else
    {
      if (!((Object) InputController.instance != (Object) this))
        return;
      Object.Destroy((Object) this.gameObject);
    }
  }

  public static bool Exists() => (Object) InputController.instance != (Object) null;

  public Vector2 InputPosition() => (Vector2) Input.mousePosition;

  public bool GetInputDown(int index) => Input.GetMouseButtonDown(index);

  public bool GetInputUp(int index) => Input.GetMouseButtonUp(index);
}
