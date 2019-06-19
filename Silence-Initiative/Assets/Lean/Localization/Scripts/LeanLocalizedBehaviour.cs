using UnityEngine;
using UnityEngine.Serialization;
using Lean.Common;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(LeanLocalizedBehaviour), true)]
	public class LeanLocalizedBehaviour_Inspector : LeanInspector<LeanLocalizedBehaviour>
	{
	}
}
#endif

namespace Lean.Localization
{
	/// <summary>This component simplifies the updating process, extend it if you want to cause a specific object to get localized</summary>
	public abstract class LeanLocalizedBehaviour : MonoBehaviour
	{
		[Tooltip("The name of the phrase we want to use for this localized component")]
		[LeanPhrasePath]
		[SerializeField]
		[FormerlySerializedAs("phraseName")]
		private string phrasePath;

		/// <summary>This allows you to get/set the phrasePath value, and automatically upcate the localization.</summary>
		public string PhrasePath
		{
			set
			{
				// phraseName changed?
				if (value != phrasePath)
				{
					// Update localization with new phrase
					phrasePath = value;

					UpdateLocalization();
				}
			}

			get
			{
				return phrasePath;
			}
		}

		// This gets called every time the translation needs updating
		// NOTE: translation may be null if it can't be found
		public abstract void UpdateTranslation(LeanTranslation translation);

		/// <summary>If you call this then this component will update using the translation for the specified phrase.</summary>
		public void UpdateLocalization()
		{
			UpdateTranslation(LeanLocalization.GetTranslation(phrasePath));
		}

		protected virtual void OnEnable()
		{
			LeanLocalization.OnLocalizationChanged += UpdateLocalization;

			UpdateLocalization();
		}

		protected virtual void OnDisable()
		{
			LeanLocalization.OnLocalizationChanged -= UpdateLocalization;
		}

#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			if (isActiveAndEnabled == true)
			{
				UpdateLocalization();
			}
		}
#endif
	}
}