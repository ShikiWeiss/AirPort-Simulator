using Airport.Utilities.Api;
using AirPort.Services.ServerServices.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace AirPort.Services.ServerServices.Implementations
{
    public class JsonSerializationService : ISerializer
    {
        private readonly IErrorLogger errorLogger;

        public JsonSerializationService(IErrorLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }
        public string Serialize<T>(T objectGraph)
        {
            try
            {

                return JsonConvert.SerializeObject(objectGraph, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"There was an error while serialzie {typeof(T)} object.\nTake a look on this message error:\n {ex.Message}");
                errorLogger.Log($"There was an error while serialzie {typeof(T)} object.\nTake a look on this message error:\n {ex.Message}");
                return null;
            }
        }

        public T Deserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                errorLogger.Log($"There was an error while deserialzie {typeof(T)} object.\nTake a look on this message error:\n {ex.Message}");
                return default(T);
            }
        }
    }
}
