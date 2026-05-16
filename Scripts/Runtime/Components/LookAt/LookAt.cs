// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Look At")]
	public class LookAt : AbstractLookAt
	{
		public Transform target;

		public override Transform Target => target;
	}
}