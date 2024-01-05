using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Nancy.Json;
using GTAutomation.Entities;

namespace GTAutomation.Framework.Utilities
{
    public class JsonReader
    {
        public static string Get(string key)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration[key];
        }

        public static object JsonReadData(Type type, string filePath)
        {
            var readData = File.ReadAllText(filePath);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (type == typeof(Credential))
            {
                return serializer.Deserialize<Credential>(readData);
            }

            return null;
        }
    }
}
