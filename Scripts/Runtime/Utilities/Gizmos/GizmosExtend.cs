// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
#if UNITY_EDITOR
using Handles = UnityEditor.Handles;
using HandleUtility = UnityEditor.HandleUtility;
#endif

namespace Evolutex.Evolunity.Utilities.Gizmos
{
	/// <summary>
	/// Gizmo Extension - Static class that extends Unity's gizmo functionality.
	///
	/// Reference:
	/// https://forum.unity.com/threads/how-to-see-spherecast.759233/#post-5057492
	///
	/// Used in Raycast Gizmos Visualizer:
	/// https://assetstore.unity.com/packages/tools/physics/raycast-gizmos-visualizer-94754
	///
	/// Author: Canis (canistk@gmail.com)
	/// https://assetstore.unity.com/publishers/6205
	/// https://forum.unity.com/members/canis.445122/
	/// https://www.clonefactor.com/
	///
	/// See also:
	/// https://gist.github.com/tsubaki/d4c6424b3792dbb92878
	/// https://github.com/code-beans/GizmoExtensions/blob/master/src/GizmosExtensions.cs
	/// https://github.com/ophilbinbriscoe/Unity-Extensions/blob/master/Assets/Runtime/GizmosExtensions.cs
	/// </summary>
	public static class GizmosExtend
	{
		#region GizmoDrawFunctions
		/// <summary>- Draws a point.</summary>
		/// <param name='position'>- The point to draw.</param>
		///  <param name='color'>- The color of the drawn point.</param>
		/// <param name='scale'>- The size of the drawn point.</param>
		public static void DrawPoint(Vector3 position, Color color = default(Color), float scale = 1.0f)
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawRay(position + (Vector3.up * (scale * 0.5f)), -Vector3.up * scale);
				UnityEngine.Gizmos.DrawRay(position + (Vector3.right * (scale * 0.5f)), -Vector3.right * scale);
				UnityEngine.Gizmos.DrawRay(position + (Vector3.forward * (scale * 0.5f)), -Vector3.forward * scale);
			}
		}

		public static void DrawRay(Vector3 position, Vector3 direction, Color color = default(Color))
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawRay(position, direction);
			}
		}

		/// <summary>Override DrawLine</summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="color"></param>
		public static void DrawLine(Vector3 from, Vector3 to, Color color = default(Color))
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawLine(from, to);
			}
		}

		/// <summary>- Draws an axis-aligned bounding box.</summary>
		/// <param name='bounds'>- The bounds to draw.</param>
		/// <param name='color'>- The color of the bounds.</param>
		public static void DrawBounds(Bounds bounds, Color color = default(Color))
		{
			Vector3
				ruf = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z),
				rub = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z),
				luf = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z),
				lub = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z),
				rdf = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z),
				rdb = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z),
				lfd = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z),
				lbd = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);

			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawLine(ruf, luf);
				UnityEngine.Gizmos.DrawLine(ruf, rub);
				UnityEngine.Gizmos.DrawLine(luf, lub);
				UnityEngine.Gizmos.DrawLine(rub, lub);

				UnityEngine.Gizmos.DrawLine(ruf, rdf);
				UnityEngine.Gizmos.DrawLine(rub, rdb);
				UnityEngine.Gizmos.DrawLine(luf, lfd);
				UnityEngine.Gizmos.DrawLine(lub, lbd);

				UnityEngine.Gizmos.DrawLine(rdf, lfd);
				UnityEngine.Gizmos.DrawLine(rdf, rdb);
				UnityEngine.Gizmos.DrawLine(lfd, lbd);
				UnityEngine.Gizmos.DrawLine(lbd, rdb);
			}
		}

		/// <summary>- Draws a circle.</summary>
		/// <param name='position'>- Where the center of the circle will be positioned.</param>
		/// <param name='up'>- The direction perpendicular to the surface of the circle.</param>
		/// <param name='color'>- The color of the circle.</param>
		/// <param name='radius'>- The radius of the circle.</param>
		public static void DrawCircle(Vector3 position, Vector3 up = default(Vector3), Color color = default(Color), float radius = 1.0f)
		{
			up = ((up == default(Vector3)) ? Vector3.up : up).normalized * radius;
			Vector3
				forward = Vector3.Slerp(up, -up, 0.5f),
				right = Vector3.Cross(up, forward).normalized * radius;

			Matrix4x4 matrix = new Matrix4x4()
			{
				m00 = right.x,
				m10 = right.y,
				m20 = right.z,

				m01 = up.x,
				m11 = up.y,
				m21 = up.z,

				m02 = forward.x,
				m12 = forward.y,
				m22 = forward.z
			};

			Vector3
				lastPoint = position + matrix.MultiplyPoint3x4(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0))),
				nextPoint = Vector3.zero;

			using (new GizmosColorScope(color))
			{
				for (int i = 0; i <= 90; i++)
				{
					nextPoint = position + matrix.MultiplyPoint3x4(
						new Vector3(
							Mathf.Cos((i * 4) * Mathf.Deg2Rad),
							0f,
							Mathf.Sin((i * 4) * Mathf.Deg2Rad)
							)
						);
					UnityEngine.Gizmos.DrawLine(lastPoint, nextPoint);
					lastPoint = nextPoint;
				}
			}
		}

		/// <summary>- Draws a cylinder.</summary>
		/// <param name='start'>- The position of one end of the cylinder.</param>
		/// <param name='end'>- The position of the other end of the cylinder.</param>
		/// <param name='color'>- The color of the cylinder.</param>
		/// <param name='radius'>- The radius of the cylinder.</param>
		public static void DrawCylinder(Vector3 start, Vector3 end, Color color = default(Color), float radius = 1.0f)
		{
			Vector3
				up = (end - start).normalized * radius,
				forward = Vector3.Slerp(up, -up, 0.5f),
				right = Vector3.Cross(up, forward).normalized * radius;

			//Radial circles
			DrawCircle(start, up, color, radius);
			DrawCircle(end, -up, color, radius);
			DrawCircle((start + end) * 0.5f, up, color, radius);

			using (new GizmosColorScope(color))
			{
				//Side lines
				UnityEngine.Gizmos.DrawLine(start + right, end + right);
				UnityEngine.Gizmos.DrawLine(start - right, end - right);

				UnityEngine.Gizmos.DrawLine(start + forward, end + forward);
				UnityEngine.Gizmos.DrawLine(start - forward, end - forward);

				//Start endcap
				UnityEngine.Gizmos.DrawLine(start - right, start + right);
				UnityEngine.Gizmos.DrawLine(start - forward, start + forward);

				//End endcap
				UnityEngine.Gizmos.DrawLine(end - right, end + right);
				UnityEngine.Gizmos.DrawLine(end - forward, end + forward);
			}
		}

		/// <summary>- Draws a cone.</summary>
		/// <param name='position'>- The position for the tip of the cone.</param>
		/// <param name='direction'>- The direction for the cone to get wider in.</param>
		/// <param name='color'>- The color of the cone.</param>
		/// <param name='angle'>- The angle of the cone.</param>
		public static void DrawCone(Vector3 position, Vector3 direction, Color color = default(Color), float angle = 45)
		{
			float length = direction.magnitude;
			angle = Mathf.Clamp(angle, 0f, 90f);

			Vector3
				forward = direction,
				up = Vector3.Slerp(forward, -forward, 0.5f),
				right = Vector3.Cross(forward, up).normalized * length,
				slerpedVector = Vector3.Slerp(forward, up, angle / 90.0f);

			Plane farPlane = new Plane(-direction, position + forward);
			Ray distRay = new Ray(position, slerpedVector);

			float dist;
			farPlane.Raycast(distRay, out dist);

			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawRay(position, slerpedVector.normalized * dist);
				UnityEngine.Gizmos.DrawRay(position, Vector3.Slerp(forward, -up, angle / 90.0f).normalized * dist);
				UnityEngine.Gizmos.DrawRay(position, Vector3.Slerp(forward, right, angle / 90.0f).normalized * dist);
				UnityEngine.Gizmos.DrawRay(position, Vector3.Slerp(forward, -right, angle / 90.0f).normalized * dist);
			}
			DrawCircle(position + forward, direction, color, (forward - (slerpedVector.normalized * dist)).magnitude);
			DrawCircle(position + (forward * 0.5f), direction, color, ((forward * 0.5f) - (slerpedVector.normalized * (dist * 0.5f))).magnitude);
		}

		/// <summary>- Draws an arrow.</summary>
		/// <param name='position'>- The start position of the arrow.</param>
		/// <param name='direction'>- The direction the arrow will point in.</param>
		/// <param name='color'>- The color of the arrow.</param>
		/// <param name="angle">- The angle of arrow head.0 ~ 90f</param>
		/// <param name="headLength">- The angle length of arrow head. 0 ~ 1 in percent</param>
		public static void DrawArrow(Vector3 position, Vector3 direction, Color color = default(Color), float angle = 15f, float headLength = 0.3f)
		{
			if (direction == Vector3.zero)
				return; // can't draw a thing
			if (angle < 0f)
				angle = Mathf.Abs(angle);
			if (angle > 0f)
			{
				float length = direction.magnitude;
				float arrowLength = length * Mathf.Clamp01(headLength);
				Vector3 headDir = direction.normalized * -arrowLength;
				DrawCone(position + direction, headDir, color, angle);
			}
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawRay(position, direction);
			}
		}

		/// <summary>- Draws a capsule.</summary>
		/// <param name='point1'>- The position of one end of the capsule.</param>
		/// <param name='point2'>- The position of the other end of the capsule.</param>
		/// <param name='color'>- The color of the capsule.</param>
		/// <param name='radius'>- The radius of the capsule.</param>
		public static void DrawCapsule(Vector3 point1, Vector3 point2, float radius = 1f, Color color = default(Color))
		{
			if (point1 == point2)
			{
				using (new GizmosColorScope(color))
				{
					UnityEngine.Gizmos.DrawWireSphere(point1, radius);
				}
			}
			else
			{
				float
					height = (point1 - point2).magnitude,
					sideLength = Mathf.Max(0, (height * 0.5f));

				Vector3
					up = (point2 - point1).normalized * radius,
					forward = Vector3.Slerp(up, -up, 0.5f),
					right = Vector3.Cross(up, forward).normalized * radius,
					middle = (point2 + point1) * 0.5f;

				point1 = middle + ((point1 - middle).normalized * sideLength);
				point2 = middle + ((point2 - middle).normalized * sideLength);

				//Radial circles
				DrawCircle(point1, up, color, radius);
				DrawCircle(point2, -up, color, radius);

				using (new GizmosColorScope(color))
				{
					//Side lines
					UnityEngine.Gizmos.DrawLine(point1 + right, point2 + right);
					UnityEngine.Gizmos.DrawLine(point1 - right, point2 - right);

					UnityEngine.Gizmos.DrawLine(point1 + forward, point2 + forward);
					UnityEngine.Gizmos.DrawLine(point1 - forward, point2 - forward);

					for (int i = 1; i < 26; i++)
					{
						//Start endcap
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + point1, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + point1);
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + point1, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + point1);
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + point1, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + point1);
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + point1, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + point1);

						//End endcap
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + point2, Vector3.Slerp(right, up, (i - 1) / 25.0f) + point2);
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + point2, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + point2);
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + point2, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + point2);
						UnityEngine.Gizmos.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + point2, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + point2);
					}
				}
			}
		}

		/// <summary>Draw Camera based on give reference.</summary>
		/// <param name="camera"></param>
		/// <param name="color"></param>
		public static void DrawFrustum(Camera camera, Color color = default(Color))
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.matrix = Matrix4x4.TRS(camera.transform.position, camera.transform.rotation, Vector3.one);
				UnityEngine.Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);
				UnityEngine.Gizmos.matrix = Matrix4x4.identity;
			}
		}

		/// <summary>Draw Plane, based on giving points start to end (forward)</summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="upward"></param>
		/// <param name="height"></param>
		/// <param name="color"></param>
		/// <remarks>pivot point is start point</remarks>
		public static void DrawPlane(Vector3 start, Vector3 end, Vector3 upward, float height = 1f, Color color = default(Color))
		{
			float width = Vector3.Distance(start, end);
			if (Mathf.Approximately(width, 0f))
				return;

			using (new GizmosColorScope(color))
			{
				Quaternion rotation =
					Quaternion.LookRotation(end - start, upward) *
					Quaternion.Euler(0f, -90f, 0f);
				UnityEngine.Gizmos.matrix = Matrix4x4.TRS(start, rotation, Vector3.one);
				UnityEngine.Gizmos.DrawCube(
					new Vector3(width * 0.5f, height * 0.5f, 0f),
					new Vector3(width, height, float.Epsilon));
				UnityEngine.Gizmos.matrix = Matrix4x4.identity;
			}
		}

		/// <summary>Draw Plane, based on transform's forward</summary>
		/// <param name="self"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="color"></param>
		public static void DrawPlane(Transform self, float width, float height = 1f, Color color = default(Color))
		{
			DrawPlane(self.position, self.position + (self.forward * width), self.up, height, color);
		}

		public static void DrawSphere(Transform self, Color color = default(Color))
		{
			DrawSphere(self.position, self.localScale.x, color);
		}

		/// <summary>Draw Sphere</summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		/// <param name="color"></param>
		public static void DrawSphere(Vector3 position, float radius, Color color = default(Color))
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawSphere(position, radius);
			}
		}

		public static void DrawDirection(Transform self, Color color = default(Color))
		{
			DrawDirection(self.position, Vector3.forward, self.localScale.x, color);
		}

		/// <summary>Draw Direction</summary>
		/// <param name="position"></param>
		/// <param name="direction"></param>
		/// <param name="distance"></param>
		/// <param name="color"></param>
		public static void DrawDirection(Vector3 position, Vector3 direction, float distance = 1f, Color color = default(Color))
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawLine(position, position + (direction * distance));
			}
		}
		#endregion

		#region Handles
#if UNITY_EDITOR
		private static bool IsHandleHackAvailable =>
			UnityEditor.SceneView.currentDrawingSceneView != null ||
			(Application.isPlaying && Camera.main != null);
#else
		private const bool IsHandleHackAvailable = false;
#endif

		public static float GetHandleSize(Vector3 center)
		{
#if UNITY_EDITOR
			if (IsHandleHackAvailable)
				return HandleUtility.GetHandleSize(center);
			else
#endif
				return 1f;
		}

		/// <summary>Draw label on current SceneView</summary>
		/// <param name="position"></param>
		/// <param name="text"></param>
		/// <param name="style"></param>
		/// <param name="color"></param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void DrawLabel(Vector3 position, string text, GUIStyle style = default(GUIStyle), Color color = default(Color), float offsetX = 0f, float offsetY = 0f)
		{
#if UNITY_EDITOR
			if (IsHandleHackAvailable)
			{
				Transform cam = UnityEditor.SceneView.currentDrawingSceneView != null ?
					UnityEditor.SceneView.currentDrawingSceneView.camera.transform : // Scene View
					Camera.main.transform; // Only Game View
				if (Vector3.Dot(cam.forward, position - cam.position) > 0)
				{
					Vector3 pos = position;
					if (offsetX != 0f || offsetY != 0f)
					{
						Vector3 camRightVector = cam.right * offsetX; // base on view
						pos += camRightVector + new Vector3(0f, offsetY, 0f); // base on target
					}

					if (style == default(GUIStyle))
					{
						if (color == default(Color))
							Handles.Label(pos, text, GUI.skin.textArea);
						else
						{
							style = new GUIStyle(GUI.skin.textArea);
							Color old = style.normal.textColor;
							style.normal.textColor = color;
							Handles.Label(pos, text, style);
							style.normal.textColor = old;
						}
					}
					else
					{
						if (color == default(Color))
							Handles.Label(pos, text, style);
						else
						{
							Color old = style.normal.textColor;
							style.normal.textColor = color;
							Handles.Label(pos, text, style);
							style.normal.textColor = old;
						}
					}
				}
			}
#endif
		}

		/// <summary>Draw a circular sector (pie piece) in 3D space.</summary>
		/// <param name="center">The center of the circle</param>
		/// <param name="normal">The normal of the circle</param>
		/// <param name="from">The direction of the point on the circumference, relative to the center, where the sector begins</param>
		/// <param name="angle">The angle of the sector, in degrees.</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="constantScreenSize">Have constant screen-sized</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void DrawArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color color, bool constantScreenSize = true)
		{
#if UNITY_EDITOR
			if (IsHandleHackAvailable)
			{
				if (constantScreenSize)
					radius *= HandleUtility.GetHandleSize(center);
				using (new HandleColorScope(color))
				{
					Handles.DrawSolidArc(center, normal, from, angle, radius);
				}
			}
#endif
		}

		/// <summary></summary>
		/// <param name="center">position</param>
		/// <param name="from">direction</param>
		/// <param name="to">direction</param>
		/// <param name="axis">A vector around, which the other vectors are rotated.</param>
		/// <param name="radius"></param>
		/// <param name="constantScreenSize"></param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void DrawAngleBetween(Vector3 center, Vector3 from, Vector3 to, Vector3 axis, float radius, Color color, bool constantScreenSize = true, bool label = false)
		{
#if UNITY_EDITOR
			if (IsHandleHackAvailable)
			{
				float angle = Vector3.SignedAngle(from, to, axis);
				DrawArc(center, axis, from, angle, radius, color, constantScreenSize);
				if (label)
				{
					float factor = constantScreenSize ? HandleUtility.GetHandleSize(center) : 1f;
					Vector3 labelPos = center + (Vector3.Lerp(from, to, 0.5f) * factor);
					DrawLabel(labelPos, $"{angle:F2}");
				}
			}
#endif
		}

		private struct HandleColorScope : System.IDisposable
		{
			Color oldColor;
			public HandleColorScope(Color color)
			{
#if UNITY_EDITOR
				oldColor = Handles.color;
				Handles.color = color == default(Color) ? oldColor : color;
#else
				oldColor = Color.white;
#endif
			}

			public void Dispose()
			{
#if UNITY_EDITOR
				Handles.color = oldColor;
#endif
			}
		}
		#endregion // Handles
		
		#region Cube
		public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color = default(Color))
		{
			DrawBox(new Box(origin, halfExtents, orientation), color);
		}

		public static void DrawBox(Box box, Color color = default(Color))
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawLine(box.frontTopLeft, box.frontTopRight);
				UnityEngine.Gizmos.DrawLine(box.frontTopRight, box.frontBottomRight);
				UnityEngine.Gizmos.DrawLine(box.frontBottomRight, box.frontBottomLeft);
				UnityEngine.Gizmos.DrawLine(box.frontBottomLeft, box.frontTopLeft);

				UnityEngine.Gizmos.DrawLine(box.backTopLeft, box.backTopRight);
				UnityEngine.Gizmos.DrawLine(box.backTopRight, box.backBottomRight);
				UnityEngine.Gizmos.DrawLine(box.backBottomRight, box.backBottomLeft);
				UnityEngine.Gizmos.DrawLine(box.backBottomLeft, box.backTopLeft);

				UnityEngine.Gizmos.DrawLine(box.frontTopLeft, box.backTopLeft);
				UnityEngine.Gizmos.DrawLine(box.frontTopRight, box.backTopRight);
				UnityEngine.Gizmos.DrawLine(box.frontBottomRight, box.backBottomRight);
				UnityEngine.Gizmos.DrawLine(box.frontBottomLeft, box.backBottomLeft);
			}
		}

		public struct Box
		{
			public Vector3 localFrontTopLeft { get; private set; }
			public Vector3 localFrontTopRight { get; private set; }
			public Vector3 localFrontBottomLeft { get; private set; }
			public Vector3 localFrontBottomRight { get; private set; }
			public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
			public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
			public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
			public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

			public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
			public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
			public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
			public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
			public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
			public Vector3 backTopRight { get { return localBackTopRight + origin; } }
			public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
			public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

			public Vector3 origin { get; private set; }

			public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
			{
				Rotate(orientation);
			}

			public Box(Vector3 origin, Vector3 halfExtents) : this()
			{
				this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
				this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
				this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
				this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

				this.origin = origin;
			}

			public void Rotate(Quaternion orientation)
			{
				localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
				localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
				localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
				localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
			}
		}

		//Draws just the box at where it is currently hitting.
		public static void DrawBoxCastOnHit(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float hitInfoDistance, Color color = default(Color))
		{
			origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
			DrawBox(origin, halfExtents, orientation, color);
		}

		//Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
		public static void DrawBoxCastBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance, Color color = default(Color))
		{
			direction.Normalize();
			Box bottomBox = new Box(origin, halfExtents, orientation);
			Box topBox = new Box(origin + (direction * distance), halfExtents, orientation);

			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft);
				UnityEngine.Gizmos.DrawLine(bottomBox.backBottomRight, topBox.backBottomRight);
				UnityEngine.Gizmos.DrawLine(bottomBox.backTopLeft, topBox.backTopLeft);
				UnityEngine.Gizmos.DrawLine(bottomBox.backTopRight, topBox.backTopRight);
				UnityEngine.Gizmos.DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft);
				UnityEngine.Gizmos.DrawLine(bottomBox.frontTopRight, topBox.frontTopRight);
				UnityEngine.Gizmos.DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft);
				UnityEngine.Gizmos.DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight);
			}

			DrawBox(bottomBox, color);
			DrawBox(topBox, color);
		}

		//This should work for all cast types
		private static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
		{
			return origin + (direction.normalized * hitInfoDistance);
		}

		private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
		{
			Vector3 direction = point - pivot;
			return pivot + rotation * direction;
		}

		[System.Obsolete("Use DrawBox", true)]
		private static void DrawLocalCube(ref Color color, ref Vector3 lbb, ref Vector3 rbb, ref Vector3 lbf, ref Vector3 rbf, ref Vector3 lub, ref Vector3 rub, ref Vector3 luf, ref Vector3 ruf)
		{
			using (new GizmosColorScope(color))
			{
				UnityEngine.Gizmos.DrawLine(lbb, rbb);
				UnityEngine.Gizmos.DrawLine(rbb, lbf);
				UnityEngine.Gizmos.DrawLine(lbf, rbf);
				UnityEngine.Gizmos.DrawLine(rbf, lbb);

				UnityEngine.Gizmos.DrawLine(lub, rub);
				UnityEngine.Gizmos.DrawLine(rub, luf);
				UnityEngine.Gizmos.DrawLine(luf, ruf);
				UnityEngine.Gizmos.DrawLine(ruf, lub);

				UnityEngine.Gizmos.DrawLine(lbb, lub);
				UnityEngine.Gizmos.DrawLine(rbb, rub);
				UnityEngine.Gizmos.DrawLine(lbf, luf);
				UnityEngine.Gizmos.DrawLine(rbf, ruf);
			}
		}

		[System.Obsolete("Use DrawBox", true)]
		/// <summary>- Draws a local cube.</summary>
		/// <param name='transform'>- The transform the cube will be local to.</param>
		/// <param name='size'>- The local size of the cube.</param>
		/// <param name='center'>- The local position of the cube.</param>
		/// <param name='color'>- The color of the cube.</param>
		public static void DrawLocalCube(Transform transform, Vector3 size, Color color = default(Color), Vector3 center = default(Vector3))
		{
			Box box = new Box(transform.position, size * 0.5f, transform.rotation);
			DrawBox(box, color);
		}

		[System.Obsolete("Use DrawBox", true)]
		/// <summary>- Draws a local cube.</summary>
		/// <param name='space'>- The space the cube will be local to.</param>
		/// <param name='size'>- The local size of the cube.</param>
		/// <param name='center'>- The local position of the cube.</param>
		/// <param name='color'>- The color of the cube.</param>
		public static void DrawLocalCube(Matrix4x4 space, Vector3 size = default(Vector3), Color color = default(Color), Vector3 center = default(Vector3))
		{
			// Box box = new Box(space.GetPosition(), size * 0.5f, space.GetRotation());
			size = (size == default(Vector3)) ? Vector3.one : size;
			Vector3
				lbb = space.MultiplyPoint3x4(center + ((-size) * 0.5f)),
				rbb = space.MultiplyPoint3x4(center + (new Vector3(size.x, -size.y, -size.z) * 0.5f)),
				lbf = space.MultiplyPoint3x4(center + (new Vector3(size.x, -size.y, size.z) * 0.5f)),
				rbf = space.MultiplyPoint3x4(center + (new Vector3(-size.x, -size.y, size.z) * 0.5f)),
				lub = space.MultiplyPoint3x4(center + (new Vector3(-size.x, size.y, -size.z) * 0.5f)),
				rub = space.MultiplyPoint3x4(center + (new Vector3(size.x, size.y, -size.z) * 0.5f)),
				luf = space.MultiplyPoint3x4(center + ((size) * 0.5f)),
				ruf = space.MultiplyPoint3x4(center + (new Vector3(-size.x, size.y, size.z) * 0.5f));

			DrawLocalCube(ref color, ref lbb, ref rbb, ref lbf, ref rbf, ref lub, ref rub, ref luf, ref ruf);
		}

		[System.Obsolete("Use DrawBox", true)]
		/// <summary>- Draws a local cube.</summary>
		/// <param name="position">- The position of the cube.</param>
		/// <param name="rotation">- The rotation of the cube.</param>
		/// <param name='size'>- The local size of the cube.</param>
		/// <param name='color'>- The color of the cube.</param>
		public static void DrawLocalCube(Vector3 position, Quaternion rotation, Vector3 size = default(Vector3), Color color = default(Color))
		{
			DrawLocalCube(Matrix4x4.TRS(position, rotation, Vector3.one), size, color, Vector3.zero);
		}
		#endregion // Cube
	}
}