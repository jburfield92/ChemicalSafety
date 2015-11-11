using UnityEngine;

public class Quit : MonoBehaviour
{
	public void QuitGame () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void QuitModule () 
	{
        Application.LoadLevel("MainMenu");
    }

    public void RestartModule()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
}
