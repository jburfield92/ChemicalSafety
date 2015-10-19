using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem.UnityGUI;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

public class SQL : MonoBehaviour 
{
    /* 
    Test User Info:
	    Username: cssg
	    First Name: cssg
	    Last Name: admin
	    Password: 123
	    Email: cssg@admin.com
	    SaveInfoId: 9
    */

	/// <summary> The user that is currently playing
	/// </summary>
	public static string UserName;

	public Slider brightnessSlider;
	public Slider sensitivitySlider;
	public Slider fontSizeSlider;
	public Slider gameVolumeSlider;

	public Text brightnessSliderValue;
	public Text sensitivitySliderValue;
	public Text fontSizeSliderValue;
	public Text gameVolumeSliderValue;

	public Text usernameText;

	private static GameObject player;

	private Text[] texts;
	private GUIText[] guiTexts;

	/// <summary> Update is called once per frame
	/// </summary>
	void Update () 
	{
		
	}

	/// <summary> Use this for initialization
	/// </summary>
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
        texts = Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[];

        // will be replaced by a JavaScript SendMessage function to populate the UserName
        UserName = "cssg";

        // displays the current user
		usernameText.text = UserName;

        // gets the user's current settings
		DataTable initialSettings = GetSettings ();
		sensitivitySlider.value = float.Parse (initialSettings.Rows [0] [0].ToString ());
		brightnessSlider.value = float.Parse (initialSettings.Rows [1] [0].ToString ());
		fontSizeSlider.value = float.Parse (initialSettings.Rows [2] [0].ToString ());
		gameVolumeSlider.value = float.Parse (initialSettings.Rows [3] [0].ToString ());

        // sets up the initial sensitivity of mouselook
		player.GetComponent<MouseLook> ().sensitivityX = 5 * sensitivitySlider.value;
		player.GetComponent<MouseLook> ().sensitivityY = 10 * sensitivitySlider.value;
	
        // sets up the initial game brightness
        foreach (Light lt in FindObjectsOfType<Light>())
        {
            lt.intensity = 1.5f * brightnessSlider.value;
        }

        // makes the multiplier a more modest value
        switch ((int)fontSizeSlider.value)
        {
            case 1:
                AuxiliaryMethods.fontSizeMultiplier = 1;
                break;
            case 2:
                AuxiliaryMethods.fontSizeMultiplier = 1.25f;
                break;
            case 3:
                AuxiliaryMethods.fontSizeMultiplier = 1.5f;
                break;
        }

        // changes the font size of all Text objects according to their tag
        foreach (Text t in texts)
        {
            if (t.tag == "LargeText")
            {
                t.fontSize = (int)(24 * AuxiliaryMethods.fontSizeMultiplier);
            }
            else if (t.tag == "MediumText")
            {
                t.fontSize = (int)(18 * AuxiliaryMethods.fontSizeMultiplier);
            }
            else if (t.tag == "SmallText")
            {
                t.fontSize = (int)(14 * AuxiliaryMethods.fontSizeMultiplier);
            }
        }

        Invoke("ChangeGUIRootFontSize", 0.1f);

        // sets up the initial game volume
        AudioListener.volume = gameVolumeSlider.value;                                                                  
	}

    void ChangeGUIRootFontSize()
    {
        FindObjectOfType<GUIRoot>().guiSkin.GetStyle("Text").fontSize = (int)(24 * AuxiliaryMethods.fontSizeMultiplier);

        if (AuxiliaryMethods.fontSizeMultiplier >= 2f)
        {
            FindObjectOfType<GUIRoot>().GetComponent<GUIControl>().scaledRect.y = ScaledValue.FromNormalizedValue(-0.15f);
        }
        else
        {
            FindObjectOfType<GUIRoot>().GetComponent<GUIControl>().scaledRect.y = ScaledValue.FromNormalizedValue(0.15f);
        }

        FindObjectOfType<GUIRoot>().ManualRefresh();
    }

	/// <summary> Gets the connection string
	/// </summary>
	/// <returns></returns>
	public static string GetNewConnection()
	{
		return "Data Source=184.168.194.55; Initial Catalog=CSSG_DATABASE; Persist Security Info=True; User ID=CSSG_ADMIN;Password=CSSG_ADMIN123";
	}

	/// <summary> Saves the user progress
	/// </summary>
	/// <param name="module">Name of the module</param>
    /// <param name="score">The score of the test</param>
	public static void SaveProgress(string module, int score)
	{
		using (SqlConnection connection = new SqlConnection(GetNewConnection()))
		using (SqlCommand command = new SqlCommand(null, connection)) 
		{
			connection.Open ();

            if (module == "Tutorial")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                        INNER JOIN Users
                        ON Users.SaveInfoId = SafeInfo.SaveInfoId
                    SET TutorialExamScore = @score, TutorialExamDate = @date
                    WHERE Users.UserName = @userName";
            }
            else if (module == "Hazard")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                        INNER JOIN Users
                        ON Users.SaveInfoId = SafeInfo.SaveInfoId
                    SET HazardExamScore = @score, HazardExamDate = @date
                    WHERE Users.UserName = @userName";
            }
            else if (module == "SDS")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                        INNER JOIN Users
                        ON Users.SaveInfoId = SafeInfo.SaveInfoId
                    SET SDSExamScore = @score, SDSExamDate = @date
                    WHERE Users.UserName = @userName";
            }
            else if (module == "Safety")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                        INNER JOIN Users
                        ON Users.SaveInfoId = SafeInfo.SaveInfoId
                    SET SafetyExamScore = @score, SafetyExamDate = @date
                    WHERE Users.UserName = @userName";
            }
            else if (module == "FinalExam")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                        INNER JOIN Users
                        ON Users.SaveInfoId = SafeInfo.SaveInfoId
                    SET FinalExamScore = @score, FinalExamDate = @date
                    WHERE Users.UserName = @userName";
            }

            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@userName", UserName);

            command.ExecuteNonQuery ();
		}
	}

	/// <summary> Saves the settings
	/// </summary>
	public void Save()
	{
		SQL.SaveSettings (brightnessSlider.value, sensitivitySlider.value, (int)fontSizeSlider.value, gameVolumeSlider.value);

        texts = Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[];
        guiTexts = Resources.FindObjectsOfTypeAll(typeof(GUIText)) as GUIText[];

        // sets up the initial sensitivity of mouselook
        player.GetComponent<MouseLook>().sensitivityX = 5 * sensitivitySlider.value;
        player.GetComponent<MouseLook>().sensitivityY = 10 * sensitivitySlider.value;

        // sets up the initial game brightness
        foreach (Light lt in FindObjectsOfType<Light>())
        {
            lt.intensity = 2f * brightnessSlider.value;
        }

        // makes the multiplier a more modest value
        switch ((int)fontSizeSlider.value)
        {
            case 1:
                AuxiliaryMethods.fontSizeMultiplier = 1;
                break;
            case 2:
                AuxiliaryMethods.fontSizeMultiplier = 1.25f;
                break;
            case 3:
                AuxiliaryMethods.fontSizeMultiplier = 1.5f;
                break;
        }

        // changes the font size of all Text objects according to their tag
        foreach (Text t in texts)
        {
            if (t == null)
            {
                continue;
            }

            if (t.tag == "LargeText")
            {
                t.fontSize = (int)(24 * AuxiliaryMethods.fontSizeMultiplier);
            }
            else if (t.tag == "MediumText")
            {
                t.fontSize = (int)(18 * AuxiliaryMethods.fontSizeMultiplier);
            }
            else if (t.tag == "SmallText")
            {
                t.fontSize = (int)(14 * AuxiliaryMethods.fontSizeMultiplier);
            }
        }

        Invoke("ChangeGUIRootFontSize", 0.01f);

        // sets up the initial game volume
        AudioListener.volume = gameVolumeSlider.value;
    }

    /// <summary> Saves the user settings
    /// </summary>
    /// <param name="brightness"></param>
    /// <param name="sensitivity"></param>
    /// <param name="fontSize"></param>
    /// <param name="gameVolume"></param>
    public static void SaveSettings(float brightness, float sensitivity, int fontSize, float gameVolume)
    {
        using (SqlConnection connection = new SqlConnection(GetNewConnection()))
        using (SqlCommand command = new SqlCommand(null, connection))
        {
            connection.Open();

            command.CommandText =
				@"UPDATE UserSettings
				SET UserSettings.SettingValue = @brightness
				FROM UserSettings
				INNER JOIN Users
				ON Users.UserId = UserSettings.UserId
				INNER JOIN Settings
				ON Settings.SettingId = UserSettings.SettingId
				WHERE Settings.SettingName = 'Brightness' AND Users.UserName = @userName;

				UPDATE UserSettings
				SET UserSettings.SettingValue = @sensitivity
				FROM UserSettings
				INNER JOIN Users
				ON Users.UserId = UserSettings.UserId
				INNER JOIN Settings
				ON Settings.SettingId = UserSettings.SettingId
				WHERE Settings.SettingName = 'Sensitivity' AND Users.UserName = @userName;

				UPDATE UserSettings
				SET UserSettings.SettingValue = @fontSize
				FROM UserSettings
				INNER JOIN Users
				ON Users.UserId = UserSettings.UserId
				INNER JOIN Settings
				ON Settings.SettingId = UserSettings.SettingId
				WHERE Settings.SettingName = 'TextSize' AND Users.UserName = @userName;

				UPDATE UserSettings
				SET UserSettings.SettingValue = @gameVolume
				FROM UserSettings
				INNER JOIN Users
				ON Users.UserId = UserSettings.UserId
				INNER JOIN Settings
				ON Settings.SettingId = UserSettings.SettingId
				WHERE Settings.SettingName = 'GameVolume' AND Users.UserName = @userName;";

            command.Parameters.AddWithValue("@brightness", brightness);
            command.Parameters.AddWithValue("@sensitivity", sensitivity);
            command.Parameters.AddWithValue("@fontSize", fontSize);
            command.Parameters.AddWithValue("@gameVolume", gameVolume);
            command.Parameters.AddWithValue("@userName", UserName);

            command.ExecuteNonQuery();
        }
    }

    /// <summary> Gets the user settings
    /// </summary>
    /// <returns></returns>
    public static DataTable GetSettings()
    {
        DataTable settings = new DataTable();

        using (SqlConnection connection = new SqlConnection(GetNewConnection()))
        using (SqlCommand command = new SqlCommand(null, connection))
        {
            connection.Open();

            command.CommandText =
                @"SELECT CAST (SettingValue AS float)
                FROM UserSettings
                INNER JOIN Users
                ON Users.UserId = UserSettings.UserId
                WHERE Users.UserName = @userName";

            command.Parameters.AddWithValue("@userName", UserName);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                settings.Load(reader);
            }
        }

        return settings;
    }

    /// <summary> Method that is called from web portal javascript to set the username for the game.
    /// </summary>
    /// <param name="username"></param>
    public static void SetUserName(string username)
    {
        UserName = username;
    }

	public void ChangeBrightnessValue(float value)
	{
		brightnessSliderValue.text = value.ToString ();
	}

	public void ChangeSensValue(float value)
	{
		sensitivitySliderValue.text = value.ToString ();
	}

	public void ChangeFontSizeValue(float value)
	{
		fontSizeSliderValue.text = value.ToString ();
	}

	public void ChangeGameVolumeValue(float value)
	{
		gameVolumeSliderValue.text = value.ToString ();
	}
}