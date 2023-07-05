// Decompiled with JetBrains decompiler
// Type: SerializableStack`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class SerializableStack<T>
{
  public List<T> data;

  public SerializableStack() => this.data = new List<T>();

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = this.data.Count<T>() - 1; index >= 0; --index)
      stringBuilder.Append(this.data.ElementAt<T>(index)?.ToString() + ", ");
    return stringBuilder.ToString();
  }

  public void Push(T d) => this.data.Add(d);

  public T Pop()
  {
    if (this.data.Count == 0)
      return default (T);
    T obj = this.data.ElementAt<T>(this.data.Count<T>() - 1);
    this.data.RemoveAt(this.data.Count<T>() - 1);
    return obj;
  }

  public T Peek() => this.data.Count<T>() == 0 ? default (T) : this.data.ElementAt<T>(this.data.Count<T>() - 1);

  public int Count() => this.data.Count<T>();

  public void Clear() => this.data.Clear();
}
