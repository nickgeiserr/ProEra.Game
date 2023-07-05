// Decompiled with JetBrains decompiler
// Type: UDB.NotificationCenter`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  public static class NotificationCenter<T, U>
  {
    public static void AddListener(string notificationID, Callback<T, U> handler)
    {
      NotificationInternal.OnListenerAdding(notificationID, (Delegate) handler);
      NotificationInternal.eventTable[notificationID] = Delegate.Combine(NotificationInternal.eventTable[notificationID], (Delegate) handler);
    }

    public static void RemoveListener(string eventType, Callback<T, U> handler)
    {
      NotificationInternal.OnListenerRemoving(eventType, (Delegate) handler);
      NotificationInternal.eventTable[eventType] = Delegate.Remove(NotificationInternal.eventTable[eventType], (Delegate) handler);
      NotificationInternal.OnListenerRemoved(eventType);
    }

    public static void Broadcast(string notificationID, T arg1, U arg2) => NotificationCenter<T, U>.Broadcast(notificationID, arg1, arg2, NotificationInternal.DEFAULT_MODE);

    public static void Broadcast(string notificationID, T arg1, U arg2, NotificationMode mode)
    {
      NotificationInternal.OnBroadcasting(notificationID, mode);
      Delegate @delegate;
      if (!NotificationInternal.eventTable.TryGetValue(notificationID, out @delegate))
        return;
      if (!(@delegate is Callback<T, U> callback))
        throw NotificationInternal.CreateBroadcastSignatureException(notificationID);
      callback(arg1, arg2);
    }
  }
}
