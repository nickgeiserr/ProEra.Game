// Decompiled with JetBrains decompiler
// Type: UDB.SerializedCachedMonoBehaviour
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using UnityEngine;

namespace UDB
{
  public abstract class SerializedCachedMonoBehaviour : SerializedMonoBehaviour
  {
    [HideInInspector]
    private Transform _transform;
    [HideInInspector]
    private RectTransform _rectTransform;
    [HideInInspector]
    private Camera _camera;
    [HideInInspector]
    private Collider _collider;
    [HideInInspector]
    private Collider2D _collider2D;
    [HideInInspector]
    private Rigidbody _rigidbody;
    [HideInInspector]
    private Rigidbody2D _rigidbody2D;
    [HideInInspector]
    private Renderer _renderer;

    public new Transform transform
    {
      get
      {
        if (this._transform == null)
          this._transform = this.GetComponent<Transform>();
        return this._transform;
      }
    }

    public RectTransform rectTransform
    {
      get
      {
        if ((Object) this._rectTransform == (Object) null)
          this._rectTransform = this.GetComponent<RectTransform>();
        return this._rectTransform;
      }
    }

    public Camera camera
    {
      get
      {
        if (this._camera == null)
          this._camera = this.GetComponent<Camera>();
        return this._camera;
      }
    }

    public Collider collider
    {
      get
      {
        if (this._collider == null)
          this._collider = this.GetComponent<Collider>();
        return this._collider;
      }
    }

    public Collider2D collider2D
    {
      get
      {
        if (this._collider2D == null)
          this._collider2D = this.GetComponent<Collider2D>();
        return this._collider2D;
      }
    }

    public Rigidbody rigidbody
    {
      get
      {
        if (this._rigidbody == null)
          this._rigidbody = this.GetComponent<Rigidbody>();
        return this._rigidbody;
      }
    }

    public Rigidbody2D rigidbody2D
    {
      get
      {
        if (this._rigidbody2D == null)
          this._rigidbody2D = this.GetComponent<Rigidbody2D>();
        return this._rigidbody2D;
      }
    }

    public Renderer renderer
    {
      get
      {
        if (this._renderer == null)
          this._renderer = this.GetComponent<Renderer>();
        return this._renderer;
      }
    }
  }
}
