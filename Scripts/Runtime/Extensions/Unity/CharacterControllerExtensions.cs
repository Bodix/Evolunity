// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class CharacterControllerExtensions
    {
        /// <summary>
        /// https://forum.unity.com/threads/does-transform-position-work-on-a-charactercontroller.36149/#post-4132021
        /// </summary>
        public static void SetPositionAndRotation(this CharacterController characterController,
            Vector3 position, Quaternion rotation)
        {
            characterController.enabled = false;
            characterController.transform.SetPositionAndRotation(position, rotation);
            characterController.enabled = true;
        }
    }
}