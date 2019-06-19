using UnityEngine;
using System.Collections.Generic;
using Lean.Common;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization
{
	[CustomEditor(typeof(LeanPhrase))]
	public class LeanPhrase_Inspector : LeanInspector<LeanPhrase>
	{
		private static List<string> languageNames = new List<string>();
		private static List<LeanTranslation> translations = new List<LeanTranslation>();

		protected override void DrawInspector()
		{
			translations.Clear();
			translations.AddRange(Target.Translations);

			languageNames.Clear();
			languageNames.AddRange(LeanLocalization.CurrentLanguages.Keys);

			foreach (var languageName in languageNames)
			{
				var translation = default(LeanTranslation);

				if (Target.TryFindTranslation(languageName, ref translation) == true)
				{
					DrawTranslation(translation, false);

					translations.Remove(translation);
				}
				else
				{
					EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(languageName, EditorStyles.boldLabel);
						if (GUILayout.Button("Create", EditorStyles.miniButton, GUILayout.Width(45.0f)) == true)
						{
							Undo.RecordObject(Target, "Create Translation");

							Target.AddTranslation(languageName);

							Dirty();
						}
					EditorGUILayout.EndHorizontal();
				}

				EditorGUILayout.Separator();
			}

			if (translations.Count > 0)
			{
				foreach (var translation in translations)
				{
					DrawTranslation(translation, true);
				}
			}
		}

		private void DrawTranslation(LeanTranslation translation, bool unexpected)
		{
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(translation.Language, EditorStyles.boldLabel);
				if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(55.0f)) == true)
				{
					Undo.RecordObject(Target, "Remove Translation");

					Target.RemoveTranslation(translation.Language);

					Dirty();
				}
			EditorGUILayout.EndHorizontal();

			if (unexpected == true)
			{
				EditorGUILayout.HelpBox("Your LeanLocalization component doesn't define the " + translation.Language + " language.", MessageType.Warning);
			}

			Undo.RecordObject(Target, "Modified Translation");

			EditorGUI.BeginChangeCheck();
			
			translation.Object = EditorGUILayout.ObjectField(translation.Object, typeof(Object), true);
			translation.Text   = EditorGUILayout.TextArea(translation.Text ?? "", GUILayout.MinHeight(40.0f));

			if (EditorGUI.EndChangeCheck() == true)
			{
				Dirty(); LeanLocalization.UpdateTranslations();
			}

			EditorGUILayout.Separator();
		}
	}
}
#endif

namespace Lean.Localization
{
	/// <summary>This contains data about each phrase, which is then translated into different languages.</summary>
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanPhrase")]
	[AddComponentMenu(LeanLocalization.ComponentPathPrefix + "Phrase")]
	public class LeanPhrase : MonoBehaviour
	{
		/// <summary>This list stores all translations of this phrase in each language.</summary>
		[SerializeField]
		private List<LeanTranslation> translations;

		public List<LeanTranslation> Translations
		{
			get
			{
				if (translations == null)
				{
					translations = new List<LeanTranslation>();
				}

				return translations;
			}
		}

		/// <summary>This will return the translation of this phrase for the specified language.</summary>
		public bool TryFindTranslation(string languageName, ref LeanTranslation translation)
		{
			if (translations != null)
			{
				for (var i = translations.Count - 1; i >= 0; i--)
				{
					translation = translations[i];

					if (translation.Language == languageName)
					{
						return true;
					}
				}
			}

			return false;
		}

		public void RemoveTranslation(string languageName)
		{
			if (translations != null)
			{
				for (var i = translations.Count - 1; i >= 0; i--)
				{
					if (translations[i].Language == languageName)
					{
						translations.RemoveAt(i);

						return;
					}
				}
			}
		}

		/// <summary>Add a new translation to this phrase for the specified language, or return the current one.</summary>
		public LeanTranslation AddTranslation(string languageName, string text = null, Object obj = null)
		{
			var translation = default(LeanTranslation);

			if (TryFindTranslation(languageName, ref translation) == false)
			{
				translation = new LeanTranslation();

				translation.Language = languageName;

				if (translations == null)
				{
					translations = new List<LeanTranslation>();
				}

				translations.Add(translation);
			}

			translation.Text   = text;
			translation.Object = obj;

			return translation;
		}

		public void Register(string path, string defaultLanguage)
		{
			var translation = default(LeanTranslation);

			// Try and register using the current language
			if (TryFindTranslation(LeanLocalization.CurrentLanguage, ref translation) == true)
			{
				Register(path, translation);
			}
			else
			{
				// Try and register using the default language
				if (TryFindTranslation(defaultLanguage, ref translation) == true)
				{
					Register(path, translation);
				}
			}
		}

		protected virtual void OnEnable()
		{
			LeanLocalization.DelayUpdateTranslations();
		}

		protected virtual void OnDisable()
		{
			LeanLocalization.DelayUpdateTranslations();
		}

		private void Register(string path, LeanTranslation translation)
		{
			if (LeanLocalization.CurrentTranslations.Remove(path) == true)
			{
				//Debug.LogWarning("You have multiple registered translations with the exact same path: " + path);
			}

			LeanLocalization.CurrentTranslations.Add(path, translation);
		}
	}
}