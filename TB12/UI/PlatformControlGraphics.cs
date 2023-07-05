// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlatformControlGraphics
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12.UI
{
  [CreateAssetMenu(menuName = "TB12/Screen/ControllerGraphics", fileName = "UIControllerGraphicsBank")]
  public class PlatformControlGraphics : ScriptableObject
  {
    [SerializeField]
    private Sprite[] controllerGraphics;

    public Sprite GetGraphic(int index) => index >= this.controllerGraphics.Length || index < 0 ? (Sprite) null : this.controllerGraphics[index];
  }
}
