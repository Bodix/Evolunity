using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	/// <summary>
	/// Keeps the GameObject active as long as there is at least one component that has requested it.
	/// </summary>
	public class MultiSourceActivationTracker : MonoBehaviour
	{
		private readonly HashSet<Component> _requesters = new HashSet<Component>();

		public void AddRequest(Component requester)
		{
			_requesters.Add(requester);
			UpdateState();
		}

		public void RemoveRequest(Component requester)
		{
			_requesters.Remove(requester);
			UpdateState();
		}

		private void UpdateState()
		{
			gameObject.SetActive(_requesters.Count > 0);
		}
	}
}