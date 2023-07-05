// Decompiled with JetBrains decompiler
// Type: AvlNodeEnumerator`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;

public sealed class AvlNodeEnumerator<TKey, TValue> : 
  IEnumerator<AvlNode<TKey, TValue>>,
  IEnumerator,
  IDisposable
{
  private AvlNode<TKey, TValue> _root;
  private AvlNodeEnumerator<TKey, TValue>.Action _action;
  private AvlNode<TKey, TValue> _current;
  private AvlNode<TKey, TValue> _right;

  public AvlNodeEnumerator(AvlNode<TKey, TValue> root)
  {
    this._right = this._root = root;
    this._action = this._root == null ? AvlNodeEnumerator<TKey, TValue>.Action.End : AvlNodeEnumerator<TKey, TValue>.Action.Right;
  }

  public bool MoveNext()
  {
    switch (this._action)
    {
      case AvlNodeEnumerator<TKey, TValue>.Action.Parent:
        while (this._current.Parent != null)
        {
          AvlNode<TKey, TValue> current = this._current;
          this._current = this._current.Parent;
          if (this._current.Left == current)
          {
            this._right = this._current.Right;
            this._action = this._right != null ? AvlNodeEnumerator<TKey, TValue>.Action.Right : AvlNodeEnumerator<TKey, TValue>.Action.Parent;
            return true;
          }
        }
        this._action = AvlNodeEnumerator<TKey, TValue>.Action.End;
        return false;
      case AvlNodeEnumerator<TKey, TValue>.Action.Right:
        this._current = this._right;
        while (this._current.Left != null)
          this._current = this._current.Left;
        this._right = this._current.Right;
        this._action = this._right != null ? AvlNodeEnumerator<TKey, TValue>.Action.Right : AvlNodeEnumerator<TKey, TValue>.Action.Parent;
        return true;
      default:
        return false;
    }
  }

  public void Reset()
  {
    this._right = this._root;
    this._action = this._root == null ? AvlNodeEnumerator<TKey, TValue>.Action.End : AvlNodeEnumerator<TKey, TValue>.Action.Right;
  }

  public AvlNode<TKey, TValue> Current => this._current;

  object IEnumerator.Current => (object) this.Current;

  public void Dispose()
  {
  }

  private enum Action
  {
    Parent,
    Right,
    End,
  }
}
