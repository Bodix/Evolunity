// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class AnimatorExtensions
    {
        public static bool IsInState(this Animator animator, string stateName) => 
            IsInState(animator, 0, stateName);

        public static bool IsInState(this Animator animator, int stateHash) => 
            IsInState(animator, 0, stateHash);
        
        public static bool IsInState(this Animator animator, int layerIndex, string stateName) => 
            animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);

        public static bool IsInState(this Animator animator, int layerIndex, int stateHash) => 
            animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateHash;
    }
}