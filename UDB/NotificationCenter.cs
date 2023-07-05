﻿// Decompiled with JetBrains decompiler
// Type: UDB.NotificationCenter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  public static class NotificationCenter
  {
    public static void AddListener(string notificationID, Callback handler)
    {
      NotificationInternal.OnListenerAdding(notificationID, (Delegate) handler);
      NotificationInternal.eventTable[notificationID] = Delegate.Combine(NotificationInternal.eventTable[notificationID], (Delegate) handler);
    }

    public static void RemoveListener(string notificationID, Callback handler)
    {
      NotificationInternal.OnListenerRemoving(notificationID, (Delegate) handler);
      NotificationInternal.eventTable[notificationID] = Delegate.Remove(NotificationInternal.eventTable[notificationID], (Delegate) handler);
      NotificationInternal.OnListenerRemoved(notificationID);
    }

    public static void Broadcast(string notificationID) => NotificationCenter.Broadcast(notificationID, NotificationInternal.DEFAULT_MODE);

    public static void Broadcast(string notificationID, NotificationMode mode)
    {
      NotificationInternal.OnBroadcasting(notificationID, mode);
      Delegate @delegate;
      if (!NotificationInternal.eventTable.TryGetValue(notificationID, out @delegate))
        return;
      if (!(@delegate is Callback callback))
        throw NotificationInternal.CreateBroadcastSignatureException(notificationID);
      callback();
    }
  }
}
