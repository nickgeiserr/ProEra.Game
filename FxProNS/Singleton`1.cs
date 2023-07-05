// Decompiled with JetBrains decompiler
// Type: FxProNS.Singleton`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace FxProNS
{
  public abstract class Singleton<T> where T : class, new()
  {
    private static T instance;

    private static bool Compare<U>(U x, U y) where U : class => (object) x == (object) y;

    public static T Instance
    {
      get
      {
        if (Singleton<T>.Compare<T>(default (T), Singleton<T>.instance))
          Singleton<T>.instance = new T();
        return Singleton<T>.instance;
      }
    }
  }
}
