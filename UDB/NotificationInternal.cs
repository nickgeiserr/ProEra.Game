// Decompiled with JetBrains decompiler
// Type: UDB.NotificationInternal
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  internal static class NotificationInternal
  {
    public static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();
    public static readonly NotificationMode DEFAULT_MODE = NotificationMode.DONT_REQUIRE_LISTENER;

    public static void OnListenerAdding(string notificationID, Delegate listenerBeingAdded)
    {
      if (!NotificationInternal.eventTable.ContainsKey(notificationID))
        NotificationInternal.eventTable.Add(notificationID, (Delegate) null);
      Delegate @delegate = NotificationInternal.eventTable[notificationID];
      if ((object) @delegate != null && @delegate.GetType() != listenerBeingAdded.GetType())
        throw new NotificationInternal.ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", (object) notificationID, (object) @delegate.GetType().Name, (object) listenerBeingAdded.GetType().Name));
    }

    public static void OnListenerRemoving(string notificationID, Delegate listenerBeingRemoved)
    {
      if (NotificationInternal.eventTable.ContainsKey(notificationID))
      {
        Delegate @delegate = NotificationInternal.eventTable[notificationID];
        if ((object) @delegate == null)
          throw new NotificationInternal.ListenerException(string.Format("Attempting to remove listener with for event type {0} but current listener is null.", (object) notificationID));
        if (@delegate.GetType() != listenerBeingRemoved.GetType())
          throw new NotificationInternal.ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", (object) notificationID, (object) @delegate.GetType().Name, (object) listenerBeingRemoved.GetType().Name));
      }
      else
        Debug.LogException((Exception) new NotificationInternal.ListenerException("Attempting to remove listener for type " + notificationID + " but Notification doesn't know about this event ID."));
    }

    public static void OnListenerRemoved(string notificationID)
    {
      if ((object) NotificationInternal.eventTable[notificationID] != null)
        return;
      NotificationInternal.eventTable.Remove(notificationID);
    }

    public static void OnBroadcasting(string notificationID, NotificationMode mode)
    {
      if (mode == NotificationMode.REQUIRE_LISTENER && !NotificationInternal.eventTable.ContainsKey(notificationID))
        throw new NotificationInternal.BroadcastException(string.Format("Broadcasting message {0} but no listener found.", (object) notificationID));
    }

    public static NotificationInternal.BroadcastException CreateBroadcastSignatureException(
      string notificationID)
    {
      return new NotificationInternal.BroadcastException(string.Format("Broadcasting message {0} but listeners have a different signature than the broadcaster.", (object) notificationID));
    }

    public class BroadcastException : Exception
    {
      public BroadcastException(string message)
        : base(message)
      {
        Debug.Log((object) message);
      }
    }

    public class ListenerException : Exception
    {
      public ListenerException(string message)
        : base(message)
      {
        Debug.Log((object) message);
      }
    }
  }
}
