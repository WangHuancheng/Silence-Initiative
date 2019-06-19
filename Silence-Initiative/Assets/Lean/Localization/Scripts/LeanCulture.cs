namespace Lean.Localization
{
	/// <summary>This class stores an association between a language name, and a language alias. An alias is an alternative name for a language based on region or system settings. For example, English might use one of these aliases: en, en-GB, en-US</summary>
	[System.Serializable]
	public class LeanCulture
	{
		/// <summary>This is the name of the language you set in the main LeanLocalization component (e.g. English, Japanese).</summary>
		public string Language;

		/// <summary>This is an alias for this language (e.g. en, en-GB, en-US).
		/// NOTE: You can have multiple cultures for the same Langiage, allowing you to set many different Aliases.</summary>
		public string Alias;
	}
}