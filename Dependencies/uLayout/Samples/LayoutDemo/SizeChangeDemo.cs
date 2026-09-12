/*
    Copyright (c) 2026 Alex Howe

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
*/
using TMPro;
using UnityEngine;

namespace Poke.UI
{
    public class SizeChangeDemo : MonoBehaviour
    {
        [SerializeField] private bool m_logLayoutRefeshes;
        [SerializeField] private TMP_Text m_text;
        [SerializeField] private TMP_Text m_text2;
        [SerializeField] private float m_minSize = 12;
        [SerializeField] private float m_maxSize = 48;
        
        private void Update() {
            #if UNITY_EDITOR
            if(m_logLayoutRefeshes) Debug.Log($"<color=white>{Layout.RefreshedThisFrame.Count} layouts refreshed</color>");
            Layout.RefreshedThisFrame.Clear();
            #endif
            
            float t = Mathf.Sin(Time.unscaledTime) * 0.5f + 0.5f;
            float t2 = Mathf.Cos(Time.unscaledTime) * 0.5f + 0.5f;
            m_text.fontSize = Mathf.Lerp(m_minSize, m_maxSize, t);
            m_text2.fontSize = Mathf.Lerp(m_minSize, m_maxSize, t2);
        }
    }
}
