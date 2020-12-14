/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using UnityEngine;

namespace Evolunity.Components
{
    [AddComponentMenu("Toolkit/Comment")]
    public class Comment : MonoBehaviour
    {
        public string Message;
        public CommentType Type = CommentType.Info;
        
        private void Awake()
        {
            enabled = false;
        }

        public enum CommentType
        {
            Info,
            Warning
        }
    }
}