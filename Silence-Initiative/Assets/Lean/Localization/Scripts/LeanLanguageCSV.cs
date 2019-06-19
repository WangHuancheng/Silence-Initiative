using UnityEngine;
using Lean.Common;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization.Examples
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(LeanLanguageCSV), true)]
	public class LeanLanguageCSV_Inspector : LeanInspector<LeanLanguageCSV>
	{
		protected override void DrawInspector()
		{
			Draw("Source", "The text asset that contains all the translations.");
			Draw("Language", "The language of the translations in the source file.");
			Draw("Separator", "The string separating the phrase name from the translation.");
			Draw("NewLine", "The string denoting a new line within a translation.");
			Draw("Comment", "The (optional) string separating the translation from the comment (empty = no comments).");

			EditorGUILayout.Separator();

			EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.LabelField("CollectItem" + Target.Separator + "アイテム" + Target.NewLine + "集めました" + Target.Comment + "Comment here");
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.Separator();

			EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Load Now") == true)
				{
					Each(t => t.LoadFromSource());
				}
				if (GUILayout.Button("Export") == true)
				{
					Each(t => t.ExportTextAsset());
				}
			EditorGUILayout.EndHorizontal();
		}
	}
}
#endif

namespace Lean.Localization
{
	/// <summary>This component will load localizations from a CSV file. By default they should be in the format:
	/// Phrase Name Here = Translation Here // Optional Comment Here
	/// </summary>
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanLanguageCSV")]
	[AddComponentMenu(LeanLocalization.ComponentPathPrefix + "Language CSV")]
	public class LeanLanguageCSV : MonoBehaviour
	{
		/// <summary>The text asset that contains all the translations.</summary>
		public TextAsset Source;

		/// <summary>The language of the translations in the source file.</summary>
		[LeanLanguageName]
		public string Language;

		/// <summary>The string separating the phrase name from the translation.</summary>
		public string Separator = " = ";

		/// <summary>The string denoting a new line within a translation.</summary>
		public string NewLine = "\\n";

		/// <summary>The (optional) string separating the translation from the comment (empty = no comments).</summary>
		public string Comment = " // ";

		/// <summary>The characters used to separate each translation.</summary>
		private static readonly char[] newlineCharacters = new char[] { '\r', '\n' };

		protected virtual void Start()
		{
			LoadFromSource();
		}

		[ContextMenu("Load From Source")]
		public void LoadFromSource()
		{
			if (Source != null && string.IsNullOrEmpty(Language) == false)
			{
				// Split file into lines, and loop through them all
				var lines = Source.text.Split(newlineCharacters, System.StringSplitOptions.RemoveEmptyEntries);

				for (var i = 0; i < lines.Length; i++)
				{
					var line        = lines[i];
					var equalsIndex = line.IndexOf(Separator);

					// Only consider lines with the Separator character
					if (equalsIndex != -1)
					{
						var title        = line.Substring(0, equalsIndex).Trim();
						var text = line.Substring(equalsIndex + Separator.Length).Trim();

						// Does this entry have a comment?
						if (string.IsNullOrEmpty(Comment) == false)
						{
							var commentIndex = text.LastIndexOf(Comment);

							if (commentIndex != -1)
							{
								text = text.Substring(0, commentIndex).Trim();
							}
						}

						// Replace newline markers with actual newlines
						if (string.IsNullOrEmpty(NewLine) == false)
						{
							text = text.Replace(NewLine, System.Environment.NewLine);
						}

						// Find or add the translation for this phrase
						LeanLocalization.AddTranslationToFirst(title, Language, text);
					}
				}

				// Update translations?
				if (LeanLocalization.CurrentLanguage == Language)
				{
					LeanLocalization.UpdateTranslations();
				}
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Export Text Asset")]
		public void ExportTextAsset()
		{
			if (string.IsNullOrEmpty(Language) == false)
			{
				// Find where we want to save the file
				var path = EditorUtility.SaveFilePanelInProject("Export Text Asset for " + Language, Language, "txt", "");

				// Make sure we didn't cancel the panel
				if (string.IsNullOrEmpty(path) == false)
				{
					var data = "";
					var gaps = false;

					// Add all phrase names and existing translations to lines
					foreach (var pair in LeanLocalization.CurrentPhrases)
					{
						var translation = default(LeanTranslation);

						if (pair.Value.TryFindTranslation(Language, ref translation) == true)
						{
							if (gaps == true)
							{
								data += System.Environment.NewLine;
							}

							data += pair.Key + Separator;
							gaps  = true;

							if (translation != null)
							{
								var text = translation.Text;

								// Replace all new line permutations with the new line token
								text = text.Replace("\r\n", NewLine);
								text = text.Replace("\n\r", NewLine);
								text = text.Replace("\n", NewLine);
								text = text.Replace("\r", NewLine);

								data += text;
							}
						}
					}

					// Write text to file
					using (var file = System.IO.File.OpenWrite(path))
					{
						var encoding = new System.Text.UTF8Encoding();
						var bytes    = encoding.GetBytes(data);

						file.Write(bytes, 0, bytes.Length);
					}

					// Import asset into project
					AssetDatabase.ImportAsset(path);

					// Replace Soure with new Text Asset?
					var textAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

					if (textAsset != null)
					{
						Source = textAsset;

						EditorGUIUtility.PingObject(textAsset);

						EditorUtility.SetDirty(this);
					}
				}
			}
		}
#endif
	}
}