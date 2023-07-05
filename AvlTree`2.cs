// Decompiled with JetBrains decompiler
// Type: AvlTree`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;

public class AvlTree<TKey, TValue> : IEnumerable<AvlNode<TKey, TValue>>, IEnumerable
{
  private IComparer<TKey> _comparer;
  private AvlNode<TKey, TValue> _root;

  public AvlTree(IComparer<TKey> comparer) => this._comparer = comparer;

  public AvlTree()
    : this((IComparer<TKey>) Comparer<TKey>.Default)
  {
  }

  public AvlNode<TKey, TValue> Root => this._root;

  public IEnumerator<AvlNode<TKey, TValue>> GetEnumerator() => (IEnumerator<AvlNode<TKey, TValue>>) new AvlNodeEnumerator<TKey, TValue>(this._root);

  public bool Search(TKey key, out TValue value)
  {
    AvlNode<TKey, TValue> avlNode = this._root;
    while (avlNode != null)
    {
      if (this._comparer.Compare(key, avlNode.Key) < 0)
        avlNode = avlNode.Left;
      else if (this._comparer.Compare(key, avlNode.Key) > 0)
      {
        avlNode = avlNode.Right;
      }
      else
      {
        value = avlNode.Value;
        return true;
      }
    }
    value = default (TValue);
    return false;
  }

  public bool Insert(TKey key, TValue value)
  {
    AvlNode<TKey, TValue> node = this._root;
    while (node != null)
    {
      int num = this._comparer.Compare(key, node.Key);
      if (num < 0)
      {
        AvlNode<TKey, TValue> left = node.Left;
        if (left == null)
        {
          node.Left = new AvlNode<TKey, TValue>()
          {
            Key = key,
            Value = value,
            Parent = node
          };
          this.InsertBalance(node, 1);
          return true;
        }
        node = left;
      }
      else if (num > 0)
      {
        AvlNode<TKey, TValue> right = node.Right;
        if (right == null)
        {
          node.Right = new AvlNode<TKey, TValue>()
          {
            Key = key,
            Value = value,
            Parent = node
          };
          this.InsertBalance(node, -1);
          return true;
        }
        node = right;
      }
      else
      {
        node.Value = value;
        return false;
      }
    }
    this._root = new AvlNode<TKey, TValue>()
    {
      Key = key,
      Value = value
    };
    return true;
  }

  private void InsertBalance(AvlNode<TKey, TValue> node, int balance)
  {
    AvlNode<TKey, TValue> parent;
    for (; node != null; node = parent)
    {
      balance = (node.Balance += balance);
      switch (balance)
      {
        case -2:
          if (node.Right.Balance == -1)
          {
            this.RotateLeft(node);
            return;
          }
          this.RotateRightLeft(node);
          return;
        case 0:
          return;
        case 2:
          if (node.Left.Balance == 1)
          {
            this.RotateRight(node);
            return;
          }
          this.RotateLeftRight(node);
          return;
        default:
          parent = node.Parent;
          if (parent != null)
          {
            balance = parent.Left == node ? 1 : -1;
            continue;
          }
          continue;
      }
    }
  }

  private AvlNode<TKey, TValue> RotateLeft(AvlNode<TKey, TValue> node)
  {
    AvlNode<TKey, TValue> right = node.Right;
    AvlNode<TKey, TValue> left = right.Left;
    AvlNode<TKey, TValue> parent = node.Parent;
    right.Parent = parent;
    right.Left = node;
    node.Right = left;
    node.Parent = right;
    if (left != null)
      left.Parent = node;
    if (node == this._root)
      this._root = right;
    else if (parent.Right == node)
      parent.Right = right;
    else
      parent.Left = right;
    ++right.Balance;
    node.Balance = -right.Balance;
    return right;
  }

  private AvlNode<TKey, TValue> RotateRight(AvlNode<TKey, TValue> node)
  {
    AvlNode<TKey, TValue> left = node.Left;
    AvlNode<TKey, TValue> right = left.Right;
    AvlNode<TKey, TValue> parent = node.Parent;
    left.Parent = parent;
    left.Right = node;
    node.Left = right;
    node.Parent = left;
    if (right != null)
      right.Parent = node;
    if (node == this._root)
      this._root = left;
    else if (parent.Left == node)
      parent.Left = left;
    else
      parent.Right = left;
    --left.Balance;
    node.Balance = -left.Balance;
    return left;
  }

  private AvlNode<TKey, TValue> RotateLeftRight(AvlNode<TKey, TValue> node)
  {
    AvlNode<TKey, TValue> left1 = node.Left;
    AvlNode<TKey, TValue> right1 = left1.Right;
    AvlNode<TKey, TValue> parent = node.Parent;
    AvlNode<TKey, TValue> right2 = right1.Right;
    AvlNode<TKey, TValue> left2 = right1.Left;
    right1.Parent = parent;
    node.Left = right2;
    left1.Right = left2;
    right1.Left = left1;
    right1.Right = node;
    left1.Parent = right1;
    node.Parent = right1;
    if (right2 != null)
      right2.Parent = node;
    if (left2 != null)
      left2.Parent = left1;
    if (node == this._root)
      this._root = right1;
    else if (parent.Left == node)
      parent.Left = right1;
    else
      parent.Right = right1;
    if (right1.Balance == -1)
    {
      node.Balance = 0;
      left1.Balance = 1;
    }
    else if (right1.Balance == 0)
    {
      node.Balance = 0;
      left1.Balance = 0;
    }
    else
    {
      node.Balance = -1;
      left1.Balance = 0;
    }
    right1.Balance = 0;
    return right1;
  }

  private AvlNode<TKey, TValue> RotateRightLeft(AvlNode<TKey, TValue> node)
  {
    AvlNode<TKey, TValue> right1 = node.Right;
    AvlNode<TKey, TValue> left1 = right1.Left;
    AvlNode<TKey, TValue> parent = node.Parent;
    AvlNode<TKey, TValue> left2 = left1.Left;
    AvlNode<TKey, TValue> right2 = left1.Right;
    left1.Parent = parent;
    node.Right = left2;
    right1.Left = right2;
    left1.Right = right1;
    left1.Left = node;
    right1.Parent = left1;
    node.Parent = left1;
    if (left2 != null)
      left2.Parent = node;
    if (right2 != null)
      right2.Parent = right1;
    if (node == this._root)
      this._root = left1;
    else if (parent.Right == node)
      parent.Right = left1;
    else
      parent.Left = left1;
    if (left1.Balance == 1)
    {
      node.Balance = 0;
      right1.Balance = -1;
    }
    else if (left1.Balance == 0)
    {
      node.Balance = 0;
      right1.Balance = 0;
    }
    else
    {
      node.Balance = 1;
      right1.Balance = 0;
    }
    left1.Balance = 0;
    return left1;
  }

  public bool Delete(TKey key)
  {
    AvlNode<TKey, TValue> avlNode = this._root;
    while (avlNode != null)
    {
      if (this._comparer.Compare(key, avlNode.Key) < 0)
        avlNode = avlNode.Left;
      else if (this._comparer.Compare(key, avlNode.Key) > 0)
      {
        avlNode = avlNode.Right;
      }
      else
      {
        AvlNode<TKey, TValue> left = avlNode.Left;
        AvlNode<TKey, TValue> right1 = avlNode.Right;
        if (left == null)
        {
          if (right1 == null)
          {
            if (avlNode == this._root)
            {
              this._root = (AvlNode<TKey, TValue>) null;
            }
            else
            {
              AvlNode<TKey, TValue> parent = avlNode.Parent;
              if (parent.Left == avlNode)
              {
                parent.Left = (AvlNode<TKey, TValue>) null;
                this.DeleteBalance(parent, -1);
              }
              else
              {
                parent.Right = (AvlNode<TKey, TValue>) null;
                this.DeleteBalance(parent, 1);
              }
            }
          }
          else
          {
            AvlTree<TKey, TValue>.Replace(avlNode, right1);
            this.DeleteBalance(avlNode, 0);
          }
        }
        else if (right1 == null)
        {
          AvlTree<TKey, TValue>.Replace(avlNode, left);
          this.DeleteBalance(avlNode, 0);
        }
        else
        {
          AvlNode<TKey, TValue> node = right1;
          if (node.Left == null)
          {
            AvlNode<TKey, TValue> parent = avlNode.Parent;
            node.Parent = parent;
            node.Left = left;
            node.Balance = avlNode.Balance;
            left.Parent = node;
            if (avlNode == this._root)
              this._root = node;
            else if (parent.Left == avlNode)
              parent.Left = node;
            else
              parent.Right = node;
            this.DeleteBalance(node, 1);
          }
          else
          {
            while (node.Left != null)
              node = node.Left;
            AvlNode<TKey, TValue> parent1 = avlNode.Parent;
            AvlNode<TKey, TValue> parent2 = node.Parent;
            AvlNode<TKey, TValue> right2 = node.Right;
            if (parent2.Left == node)
              parent2.Left = right2;
            else
              parent2.Right = right2;
            if (right2 != null)
              right2.Parent = parent2;
            node.Parent = parent1;
            node.Left = left;
            node.Balance = avlNode.Balance;
            node.Right = right1;
            right1.Parent = node;
            left.Parent = node;
            if (avlNode == this._root)
              this._root = node;
            else if (parent1.Left == avlNode)
              parent1.Left = node;
            else
              parent1.Right = node;
            this.DeleteBalance(parent2, -1);
          }
        }
        return true;
      }
    }
    return false;
  }

  private void DeleteBalance(AvlNode<TKey, TValue> node, int balance)
  {
    AvlNode<TKey, TValue> parent;
    for (; node != null; node = parent)
    {
      balance = (node.Balance += balance);
      switch (balance)
      {
        case -2:
          if (node.Right.Balance <= 0)
          {
            node = this.RotateLeft(node);
            if (node.Balance == 1)
              return;
            goto case 0;
          }
          else
          {
            node = this.RotateRightLeft(node);
            goto case 0;
          }
        case 0:
          parent = node.Parent;
          if (parent != null)
          {
            balance = parent.Left == node ? -1 : 1;
            continue;
          }
          continue;
        case 2:
          if (node.Left.Balance >= 0)
          {
            node = this.RotateRight(node);
            if (node.Balance == -1)
              return;
            goto case 0;
          }
          else
          {
            node = this.RotateLeftRight(node);
            goto case 0;
          }
        default:
          return;
      }
    }
  }

  private static void Replace(AvlNode<TKey, TValue> target, AvlNode<TKey, TValue> source)
  {
    AvlNode<TKey, TValue> left = source.Left;
    AvlNode<TKey, TValue> right = source.Right;
    target.Balance = source.Balance;
    target.Key = source.Key;
    target.Value = source.Value;
    target.Left = left;
    target.Right = right;
    if (left != null)
      left.Parent = target;
    if (right == null)
      return;
    right.Parent = target;
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
