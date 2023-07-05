// Decompiled with JetBrains decompiler
// Type: SaveKeycloakUserData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[MessagePackObject(false)]
[Serializable]
public class SaveKeycloakUserData : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "SaveKeycloakData";
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public string Username;
  [Key(2)]
  public string Password;
  [Key(3)]
  public string AuthToken;
  [Key(4)]
  public string RefreshToken;
  [IgnoreMember]
  public string PasswordConfirmation;

  public void Clear()
  {
    this.AuthToken = string.Empty;
    this.RefreshToken = string.Empty;
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value)
  {
    this.isDirty = value;
    if (!this.isDirty)
      return;
    AppEvents.SaveKeycloak.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<SaveKeycloakUserData> deserializedData = SaveIO.LoadAsync<SaveKeycloakUserData>(SaveIO.GetPath(SaveKeycloakUserData.FileName));
    SaveKeycloakUserData keycloakUserData = await deserializedData;
    if (deserializedData.Result != null)
      PersistentSingleton<SaveManager>.Instance.keycloakUserData = deserializedData.Result;
    else
      Debug.Log((object) "COULD NOT FIND KEYCLOAK USER DATA");
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<SaveKeycloakUserData>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<SaveKeycloakUserData>) null;
    }
  }

  public async Task Save()
  {
    SaveKeycloakUserData objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<SaveKeycloakUserData>(objectTarget, SaveIO.GetPath(SaveKeycloakUserData.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
