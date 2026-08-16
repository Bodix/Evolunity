// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if NEWTONSOFT_JSON

using System.IO;
using Bodix.Evolunity.Collections;
using Bodix.Evolunity.Services;
using Bodix.Evolunity.Utilities;
using Newtonsoft.Json;

namespace Evolunity.Newtonsoft
{
	public class JsonDataSerializer : IDataSerializer
	{
		private readonly JsonSerializerSettings _settings;

		public JsonDataSerializer(ConfigService configService)
		{
			_settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				Converters = { new DataAssetConverter<DataAsset>(configService) }
			};
		}

		public string Extension => ".json";

		public void Serialize<T>(T data, string filePath)
		{
			string json = JsonConvert.SerializeObject(data, _settings);

			File.WriteAllText(filePath, json);
		}

		public T Deserialize<T>(string filePath)
		{
			string json = File.ReadAllText(filePath);

			return JsonConvert.DeserializeObject<T>(json, _settings);
		}
	}
}

#endif