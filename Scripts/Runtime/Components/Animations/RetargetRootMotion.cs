// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Animations
{
    public enum RootMotionTarget
    {
        /// <summary>
        /// Use it for debug purposes only. In release just delete the <see cref="RetargetRootMotion" /> component.
        /// </summary>
        BuiltIn,
        CharacterController,
        Rigidbody,
        Transform,
        CustomReceiver
    }

    public interface IRootMotionReceiver
    {
        void ApplyRootMotion(Vector3 deltaPosition, Quaternion deltaRotation);
    }

    [AddComponentMenu("Evolunity/Animations/Retarget Root Motion")]
    [RequireComponent(typeof(Animator))]
    public class RetargetRootMotion : MonoBehaviour
    {
        // TODO: Make API of this class public.

        [SerializeField, ValidateInput(nameof(IsBuiltInTarget), "The same as without this component")]
        private RootMotionTarget _targetType = RootMotionTarget.CharacterController;

        [SerializeField, ShowIf(nameof(IsCharacterControllerTarget))]
        private CharacterController _targetCharacterController;
        [SerializeField, ShowIf(nameof(IsRigidbodyTarget))]
        private Rigidbody _targetRigidbody;
        [SerializeField, ShowIf(nameof(IsTransformTarget))]
        private Transform _targetTransform;
        [SerializeField, InterfaceType(typeof(IRootMotionReceiver)), ShowIf(nameof(IsCustomReceiverTarget))]
        private Object _targetReceiver;

        private bool IsBuiltInTarget(RootMotionTarget target) => target != RootMotionTarget.BuiltIn;
        private bool IsCharacterControllerTarget => _targetType == RootMotionTarget.CharacterController;
        private bool IsRigidbodyTarget => _targetType == RootMotionTarget.Rigidbody;
        private bool IsTransformTarget => _targetType == RootMotionTarget.Transform;
        private bool IsCustomReceiverTarget => _targetType == RootMotionTarget.CustomReceiver;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // ReSharper disable once Unity.RedundantEventFunction
        // Used to make enabling checkbox in inspector
        private void Start()
        {
        }

        private void OnAnimatorMove()
        {
            if (!_animator.applyRootMotion || !enabled)
                return;

            switch (_targetType)
            {
                case RootMotionTarget.CharacterController:
                    _targetCharacterController.Move(_animator.deltaPosition);
                    _targetCharacterController.transform.rotation *= _animator.deltaRotation;
                    break;
                case RootMotionTarget.Rigidbody:
                    _targetRigidbody.MovePosition(_targetRigidbody.position + _animator.deltaPosition);
                    _targetRigidbody.MoveRotation(_targetRigidbody.rotation * _animator.deltaRotation);
                    break;
                case RootMotionTarget.Transform:
                    _targetTransform.position += _animator.deltaPosition;
                    _targetTransform.rotation *= _animator.deltaRotation;
                    break;
                case RootMotionTarget.CustomReceiver:
                    IRootMotionReceiver receiver = (IRootMotionReceiver)_targetReceiver;
                    receiver.ApplyRootMotion(_animator.deltaPosition, _animator.deltaRotation);
                    break;
                case RootMotionTarget.BuiltIn:
                default:
                    _animator.ApplyBuiltinRootMotion();
                    break;
            }
        }
    }
}