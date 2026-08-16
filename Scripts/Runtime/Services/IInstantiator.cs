// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Services
{
	/// <summary>
	/// Abstracts the instantiation process to allow external frameworks 
	/// to inject dependencies into newly created objects.
	/// </summary>
	public interface IInstantiator
	{
		T Instantiate<T>(T prefab, Transform parent) where T : Component;
	}
}