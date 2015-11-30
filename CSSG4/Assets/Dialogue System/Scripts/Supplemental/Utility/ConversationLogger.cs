using UnityEngine;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem {

	/// <summary>
	/// When you attach this script to an actor, conversations involving that actor will be
	/// logged to the console.
	/// </summary>
	[AddComponentMenu("Dialogue System/Miscellaneous/Conversation Logger")]
	public class ConversationLogger : MonoBehaviour 
	{
		public Text savedDialogue;
		public Text timer;

		public void OnConversationLine(Subtitle subtitle) 
		{
			if (subtitle == null | subtitle.formattedText == null | string.IsNullOrEmpty(subtitle.formattedText.text)) return;
			savedDialogue.text += (timer.text + " - " + subtitle.formattedText.text + "\n\n");
		}
	}
}
