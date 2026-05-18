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
		protected void SaveCurrentToWidest()
		{
			RecordUndo(this, "Save Widest Data");

			WidestData = ExtractDataFromTarget();
		}

		[Button("Save Current To Narrowest")]
		protected void SaveCurrentToNarrowest()
		{
			RecordUndo(this, "Save Narrowest Data");

			NarrowestData = ExtractDataFromTarget();
		}

		[Button("Apply Widest To Current")]
		protected void ApplyWidestToCurrent()
		{
			RecordUndo(Target, "Apply Widest Data");

			ApplyDataToTarget(WidestData);
		}

		[Button("Apply Narrowest To Current")]
		protected void ApplyNarrowestToCurrent()
		{
			RecordUndo(Target, "Apply Narrowest Data");

			ApplyDataToTarget(NarrowestData);
		}

		[Button("Swap Widest And Narrowest")]
		protected void SwapWidestAndNarrowest()
		{
			RecordUndo(Target, "Swap Widest And Narrowest");

			(WidestData, NarrowestData) = (NarrowestData, WidestData);
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