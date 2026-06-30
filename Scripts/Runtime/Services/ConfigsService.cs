using System.Collections.Generic;
using Bodix.Evolunity.Collections;
using UnityEngine;

namespace Bodix.Evolunity.Services
{
	public class ConfigService
	{
		private readonly Dictionary<string, DataAsset> _configs;

		public ConfigService(Dictionary<string, DataAsset> configs)
		{
			_configs = configs;
		}

		public T GetConfig<T>(string id) where T : DataAsset
		{
			if (string.IsNullOrEmpty(id))
			{
				Debug.LogError($"[{nameof(ConfigService)}] Requested config with an empty ID.");
				return null;
			}

			if (_configs.TryGetValue(id, out DataAsset asset))
			{
				if (asset is T typedAsset)
					return typedAsset;

				Debug.LogError($"[{nameof(ConfigService)}] Config '{id}' was found, but it is not of type {typeof(T).Name}.");
				return null;
			}

			Debug.LogError($"[{nameof(ConfigService)}] Config with ID '{id}' not found in the registry.");
			return null;
		}
	}
}