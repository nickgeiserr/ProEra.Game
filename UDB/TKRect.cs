// Decompiled with JetBrains decompiler
// Type: UDB.TKRect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public struct TKRect
  {
    public float x;
    public float y;
    public float width;
    public float height;

    public float xMin => this.x;

    public float xMax => this.x + this.width;

    public float yMin => this.y;

    public float yMax => this.y + this.height;

    public Vector2 center => new Vector2(this.x + this.width / 2f, this.y + this.height / 2f);

    public TKRect(float x, float y, float width, float height)
    {
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
      this.UpdateRectWithRuntimeScaleModifier();
    }

    public TKRect(float width, float height, Vector2 center)
    {
      this.width = width;
      this.height = height;
      this.x = center.x - width / 2f;
      this.y = center.y - height / 2f;
      this.UpdateRectWithRuntimeScaleModifier();
    }

    public override string ToString() => string.Format("TKRect: x: {0}, xMax: {1}, y: {2}, yMax: {3}, width: {4}, height: {5}, center: {6}", (object) this.x, (object) this.xMax, (object) this.y, (object) this.yMax, (object) this.width, (object) this.height, (object) this.center);

    private void UpdateRectWithRuntimeScaleModifier()
    {
      Vector2 runtimeScaleModifier = SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier;
      this.x *= runtimeScaleModifier.x;
      this.y *= runtimeScaleModifier.y;
      this.width *= runtimeScaleModifier.x;
      this.height *= runtimeScaleModifier.y;
    }

    public TKRect CopyWithExpansion(float allSidesExpansion) => this.CopyWithExpansion(allSidesExpansion, allSidesExpansion);

    public TKRect CopyWithExpansion(float xExpansion, float yExpansion)
    {
      xExpansion *= SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier.x;
      yExpansion *= SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier.y;
      return new TKRect()
      {
        x = this.x - xExpansion,
        y = this.y - yExpansion,
        width = this.width + xExpansion * 2f,
        height = this.height + yExpansion * 2f
      };
    }

    public bool Contains(Vector2 point) => (double) this.x <= (double) point.x && (double) this.y <= (double) point.y && (double) this.xMax >= (double) point.x && (double) this.yMax >= (double) point.y;
  }
}
