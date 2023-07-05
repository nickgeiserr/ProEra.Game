// Decompiled with JetBrains decompiler
// Type: Stat
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[Serializable]
public abstract class Stat
{
  [SerializeField]
  private string statAccessName = "some_stat_0";
  [SerializeField]
  private string statDisplayName = nameof (Stat);
  [SerializeField]
  private Stat.EStatType valueType;
  [SerializeField]
  private StatValue statValue = (StatValue) "{}";

  public Stat()
  {
  }

  public Stat(string name, string value, int type)
  {
    this.StatDisplayName = name;
    this.StatValue = (StatValue) value;
    this.valueType = (Stat.EStatType) type;
  }

  public Stat(Stat statToCopy)
  {
    this.statAccessName = statToCopy.StatAccessName;
    this.statDisplayName = statToCopy.StatDisplayName;
    this.statValue = statToCopy.StatValue;
    this.valueType = statToCopy.ValueType;
  }

  public string StatAccessName => this.statAccessName;

  public string StatDisplayName
  {
    get => this.statDisplayName;
    set => this.statDisplayName = value;
  }

  public StatValue StatValue
  {
    get => this.statValue;
    set => this.statValue.Value = (string) value;
  }

  public Stat.EStatType ValueType => this.valueType;

  public virtual T GetValue<T>() => JsonUtility.FromJson<T>((string) this.StatValue);

  public virtual void SetValue<T>(T newValue) => this.StatValue = (StatValue) newValue.ToString();

  public enum EStatType
  {
    OBJECT,
    BOOL,
    INT,
    FLOAT,
    STRING,
  }
}
