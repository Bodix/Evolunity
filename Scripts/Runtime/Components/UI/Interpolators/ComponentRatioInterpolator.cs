using NaughtyAttributes;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	public abstract class ComponentRatioInterpolator<TComponent, TData> : AspectRatioInterpolator
		where TComponent : Component
	{
		[SerializeField]
		protected TComponent Target;
		[SerializeField]
		protected TData WidestData;
		[SerializeField]
		protected TData NarrowestData;

		protected override void Awake()
		{
			if (Target == null)
				Target = GetComponent<TComponent>();

			base.Awake();
		}

		protected override void InterpolateAndApply(float t)
		{
			if (Target == null)
			{
				Debug.LogWarning("Target component is missing. Cannot apply interpolation.");

				return;
			}

			TData interpolatedData = InterpolateData(WidestData, NarrowestData, t);

			ApplyDataToTarget(interpolatedData);
		}

		/// <summary>
		/// Interpolates between the widest and narrowest data states.
		/// </summary>
		protected abstract TData InterpolateData(TData widest, TData narrowest, float t);

		/// <summary>
		/// Extracts the current state directly from the target component.
		/// </summary>
		protected abstract TData ExtractDataFromTarget();

		/// <summary>
		/// Applies the given state to the target component.
		/// </summary>
		protected abstract void ApplyDataToTarget(TData data);

		[Button("Save Current To Widest")]
		private void SaveCurrentToWidest()
		{
			if (Target == null) return;

			RecordUndo(this, "Save Widest Data");
			WidestData = ExtractDataFromTarget();
		}

		[Button("Save Current To Narrowest")]
		private void SaveCurrentToNarrowest()
		{
			if (Target == null) return;

			RecordUndo(this, "Save Narrowest Data");
			NarrowestData = ExtractDataFromTarget();
		}

		[Button("Apply Widest To Current")]
		private void ApplyWidestToCurrent()
		{
			if (Target == null) return;

			RecordUndo(Target, "Apply Widest Data");
			ApplyDataToTarget(WidestData);
		}

		[Button("Apply Narrowest To Current")]
		private void ApplyNarrowestToCurrent()
		{
			if (Target == null) return;

			RecordUndo(Target, "Apply Narrowest Data");
			ApplyDataToTarget(NarrowestData);
		}

		private void RecordUndo(Object target, string actionName)
		{
#if UNITY_EDITOR
			UnityEditor.Undo.RecordObject(target, actionName);
			UnityEditor.EditorUtility.SetDirty(target);
#endif
		}
	}
}