// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Utilities.Screenshot
{
    public class ScreenshotSettings
    {
        public string Name { get; set; } = "Screenshot";
        public Camera Camera { get; set; }
        public Vector2Int? Resolution { get; set; }
        public CameraClearFlags? ClearFlags { get; set; }
        public Color? BackgroundColor { get; set; }
        public bool? IsOrthographic { get; set; }
        public float? OrthographicSize { get; set; }
        public float? FieldOfView { get; set; }
        public bool IsIgnoreUI { get; set; }
    }
}