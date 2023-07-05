// Decompiled with JetBrains decompiler
// Type: SGD_Uniforms
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class SGD_Uniforms
{
  [Key(0)]
  public string fileName;
  [Key(1)]
  public double timeStamp;
  [Key(2)]
  public string gameVersion;
  [Key(3)]
  public string userName;
  [Key(4)]
  public bool markAsDeleted;
  [Key(5)]
  public List<UniformConfig> uniforms;

  public void SetFileName(string f) => this.fileName = f;

  public string GetFileName() => this.fileName;

  public void UpdateTimeStamp() => this.timeStamp = SaveSyncUtils.Current();

  public DateTime GetTimeStamp() => SaveSyncUtils.ConvertToDate(this.timeStamp);

  public void UpdateGameVersion() => this.gameVersion = SaveIO.gameVersion;

  public string GetGameVersion() => this.gameVersion;

  public void UpdateUserName(string u) => this.userName = u;

  public string GetUserName() => this.userName;

  public SGD_Uniforms(string f)
  {
    this.SetFileName(f);
    this.uniforms = new List<UniformConfig>();
  }

  public void AddUniform(UniformConfig u) => this.uniforms.Add(u);

  public List<UniformConfig> GetUniformList() => this.uniforms;

  public int GetNumberOfUniforms() => this.uniforms.Count;

  public int FindUniformConfigByName(string uniformSetName)
  {
    for (int index = 0; index < this.uniforms.Count; ++index)
    {
      if (this.uniforms[index].UniformSetName == uniformSetName)
        return index;
    }
    return -1;
  }

  public void DeleteUniform(string uniformSetName)
  {
    for (int index = 0; index < this.uniforms.Count; ++index)
    {
      if (this.uniforms[index].UniformSetName == uniformSetName)
      {
        this.uniforms.RemoveAt(index);
        break;
      }
    }
  }

  public void OverrideUniformAt(int i, UniformConfig u)
  {
    if (i > this.uniforms.Count - 1)
      Debug.Log((object) ("Attempting to replace a uniform at position: " + i.ToString() + " which is outside the bounds of the uniform list."));
    else
      this.uniforms[i] = u;
  }
}
