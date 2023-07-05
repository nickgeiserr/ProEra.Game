// Decompiled with JetBrains decompiler
// Type: UnityTypeSupport
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

internal static class UnityTypeSupport
{
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
  private static void Init() => JsonConvert.DefaultSettings += new Func<JsonSerializerSettings>(UnityTypeSupport.GetJsonSerializerSettings);

  private static JsonSerializerSettings GetJsonSerializerSettings()
  {
    JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
    serializerSettings.Converters.Add((JsonConverter) new UnityTypeSupport.UnityTypeConverter());
    serializerSettings.Converters.Add((JsonConverter) new UnityTypeSupport.ScriptableObjectCreator());
    return serializerSettings;
  }

  private class UnityTypeConverter : JsonConverter
  {
    private static readonly HashSet<System.Type> UnityEngineTypes = new HashSet<System.Type>((IEnumerable<System.Type>) typeof (UnityEngine.Object).Assembly.GetTypes());

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteRawValue(JsonUtility.ToJson(value));

    public override object ReadJson(
      JsonReader reader,
      System.Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      if (!typeof (ScriptableObject).IsAssignableFrom(objectType))
        return JsonUtility.FromJson(JObject.Load(reader).ToString(), objectType);
      JsonUtility.FromJsonOverwrite(JObject.Load(reader).ToString(), existingValue);
      return existingValue;
    }

    public override bool CanConvert(System.Type objectType) => UnityTypeSupport.UnityTypeConverter.IsUnityEngineType(objectType);

    private static bool IsUnityEngineType(System.Type objectType) => UnityTypeSupport.UnityTypeConverter.UnityEngineTypes.Contains(objectType);
  }

  private class ScriptableObjectCreator : CustomCreationConverter<ScriptableObject>
  {
    public override ScriptableObject Create(System.Type objectType) => ScriptableObject.CreateInstance(objectType);
  }
}
