// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if NEWTONSOFT_JSON

using System;
using Bodix.Evolunity.Collections;
using Bodix.Evolunity.Services;
using Newtonsoft.Json;

namespace Evolunity.Newtonsoft
{
	public class DataAssetConverter<T> : JsonConverter<T> where T : DataAsset
	{
		private readonly ConfigService _configService;

		public DataAssetConverter(ConfigService configService)
		{
			_configService = configService;
		}

		public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteValue(value.Id);
		}

		public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue,
			JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			string id = (string)reader.Value;

			return !string.IsNullOrEmpty(id)
				? _configService.GetConfig<T>(id)
				: null;
		}
	}
}

#endif