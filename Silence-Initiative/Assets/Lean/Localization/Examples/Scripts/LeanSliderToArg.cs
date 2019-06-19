using UnityEngine;
using UnityEngine.UI;
using Lean.Common;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization.Examples
{
	[CustomEditor(typeof(LeanSliderToArg), true)]
	public class LeanSliderToArg_Inspector : LeanInspector<LeanSliderToArg>
	{
	}
}
#endif

namespace Lean.Localization.Examples
{
	/// <summary>This component binds a UI Slider to the specified LeanLocalizedText.Args element.</summary>
	[RequireComponent(typeof(Slider))]
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanSliderToArg")]
	[AddComponentMenu(LeanLocalization.ComponentPathPrefix + "Slider To Arg")]
	public class LeanSliderToArg : MonoBehaviour
	{
		public LeanLocalizedText Target;

		public int Index;

		[System.NonSerialized]
		private Slider cachedSlider;

		public void UpdateArg()
		{
			if (Target != null)
			{
				if (cachedSlider == null)
				{
					cachedSlider = GetComponent<Slider>();
				}

				Target.SetArg(cachedSlider.value.ToString(), Index);
			}
		}

		protected virtual void OnEnable()
		{
			UpdateArg();

			cachedSlider.onValueChanged.AddListener(ValueChanged);
		}

		protected virtual void OnDisable()
		{
			cachedSlider.onValueChanged.RemoveListener(ValueChanged);
		}

		private void ValueChanged(float v)
		{
			UpdateArg();
		}
	}
}