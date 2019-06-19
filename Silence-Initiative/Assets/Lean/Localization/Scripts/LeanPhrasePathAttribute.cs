using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization
{
	[CustomPropertyDrawer(typeof(LeanPhrasePathAttribute))]
	public class LeanPhrasePathDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var left  = position; left.xMax -= 40;
			var right = position; right.xMin = left.xMax + 2;
			var color = GUI.color;

			if (LeanLocalization.PhraseExists(property.stringValue) == false)
			{
				GUI.color = Color.red;
			}

			EditorGUI.PropertyField(left, property);

			GUI.color = color;

			if (GUI.Button(right, "List") == true)
			{
				var menu = new GenericMenu();

				foreach (var phraseName in LeanLocalization.CurrentPhrases.Keys)
				{
					menu.AddItem(new GUIContent(phraseName), property.stringValue == phraseName, () => { property.stringValue = phraseName; property.serializedObject.ApplyModifiedProperties(); });
				}

				if (menu.GetItemCount() > 0)
				{
					menu.DropDown(right);
				}
				else
				{
					Debug.LogWarning("Your scene doesn't contain any phrases, so the phrase name list couldn't be created.");
				}
			}
		}
	}
}
#endif

namespace Lean.Localization
{
	/// <summary>This attribute can be added to strings, allowing you to select a phrase from all current phrases in the scene.</summary>
	public class LeanPhrasePathAttribute : PropertyAttribute
	{
	}
}