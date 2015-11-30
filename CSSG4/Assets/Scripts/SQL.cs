using PixelCrushers.DialogueSystem.UnityGUI;
using System;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;
/*using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
*/
public class SQL : MonoBehaviour 
{
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

    public Text TCompleteText;
    public Text TTestScoreText;
    public Text HSCompleteText;
    public Text HSTestScoreText;
    public Text SDSCompleteText;
    public Text SDSTestScoreText;
    public Text FCompleteText;
    public Text FTestScoreText;

    public Button ContinueButton;
    public Button GetCertificateButton;

    /// <summary> Update is called once per frame
    /// </summary>
    void Update () 
	{
		
	}

	/// <summary> Use this for initialization
	/// </summary>
	void Start () 
	{
        // will be replaced by a JavaScript SendMessage function to populate the UserName
        UserName = "cssg";

        // displays the current user
		usernameText.text = UserName;

        player = GameObject.FindGameObjectWithTag("Player");

        RefreshSettings();

        if (Application.loadedLevelName == "MainMenu") 
		{
            DataTable userProgress = GetProgress();

            TCompleteText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["TutorialExamDate"].ToString())) ? "No" : "Yes";
	        HSCompleteText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["HazardExamDate"].ToString())) ? "No" : "Yes";
	        SDSCompleteText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["SDSExamDate"].ToString())) ? "No" : "Yes";
	        FCompleteText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["FinalExamDate"].ToString())) ? "No" : "Yes";

            if (TCompleteText.text == "No" && TCompleteText.text == "No" && TCompleteText.text == "No" && TCompleteText.text == "No")
            {
                ContinueButton.interactable = false;
            }
            else
            {
                ContinueButton.interactable = true;
            }

	        TTestScoreText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["TutorialExamScore"].ToString())) ? "N/A" : userProgress.Rows[0]["TutorialExamScore"].ToString();
	        HSTestScoreText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["HazardExamScore"].ToString())) ? "N/A" : userProgress.Rows[0]["HazardExamScore"].ToString();
            SDSTestScoreText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["SDSExamScore"].ToString())) ? "N/A" : userProgress.Rows[0]["SDSExamScore"].ToString();
            FTestScoreText.text = (string.IsNullOrEmpty(userProgress.Rows[0]["FinalExamScore"].ToString())) ? "N/A" : userProgress.Rows[0]["FinalExamScore"].ToString();

            if (FTestScoreText.text != "N/A")
            {
                int score = Convert.ToInt32(FTestScoreText.text);

                if (score >= 80)
                {
                    GetCertificateButton.interactable = true;
                }
                else
                {
                    GetCertificateButton.interactable = false;
                }
            }
		}
	}

    void ChangeGUIRootFontSize()
    {
        if (Application.loadedLevelName != "MainMenu")
        {
            FindObjectOfType<GUIRoot>().guiSkin.GetStyle("Text").fontSize = (int)(24 * AuxiliaryMethods.fontSizeMultiplier);
        }

        if (fontSizeSlider.value == 3f)
        {
            FindObjectOfType<GUIRoot>().GetComponent<GUIControl>().scaledRect.y = ScaledValue.FromNormalizedValue(0.05f);
        }
        else if (fontSizeSlider.value == 2f)
        {
            FindObjectOfType<GUIRoot>().GetComponent<GUIControl>().scaledRect.y = ScaledValue.FromNormalizedValue(0.15f);
        }
        else
        {
            FindObjectOfType<GUIRoot>().GetComponent<GUIControl>().scaledRect.y = ScaledValue.FromNormalizedValue(0.2f);
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
                    SET TutorialExamScore = CASE WHEN @score > TutorialExamScore 
                                                THEN @score 
                                                ELSE TutorialExamScore END, TutorialExamDate = @date
                    FROM SaveInfo SI
                    INNER JOIN Users U
                    ON U.SaveInfoId = SI.SaveInfoId
                    WHERE U.UserName = @userName";
            }
            else if (module == "Hazard")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                    SET HazardExamScore = CASE WHEN @score > HazardExamScore 
                                                THEN @score 
                                                ELSE HazardExamScore END, HazardExamDate = @date
                    FROM SaveInfo SI
                    INNER JOIN Users U
                    ON U.SaveInfoId = SI.SaveInfoId
                    WHERE U.UserName = @userName";
            }
            else if (module == "SDS")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                    SET SDSExamScore = CASE WHEN @score > SDSExamScore 
                                                THEN @score 
                                                ELSE SDSExamScore END, SDSExamDate = @date
                    FROM SaveInfo SI
                    INNER JOIN Users U
                    ON U.SaveInfoId = SI.SaveInfoId
                    WHERE U.UserName = @userName";
            }
            else if (module == "FinalExam")
            {
                command.CommandText =
                    @"UPDATE SaveInfo 
                    SET FinalExamScore = CASE WHEN @score > FinalExamScore 
                                                THEN @score 
                                                ELSE FinalExamScore END, FinalExamDate = @date
                    FROM SaveInfo SI
                    INNER JOIN Users U
                    ON U.SaveInfoId = SI.SaveInfoId
                    WHERE U.UserName = @userName";
            }

            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@userName", UserName);

            command.ExecuteNonQuery ();
		}
	}

    public static void EraseProgress()
    {
        using (SqlConnection connection = new SqlConnection(GetNewConnection()))
        using (SqlCommand command = new SqlCommand(null, connection))
        {
            connection.Open();

            command.CommandText =
                 @"UPDATE SaveInfo 
                    SET 
                        TutorialExamDate = NULL,
                        HazardExamDate = NULL,
                        SDSExamDate = NULL,
                        FinalExamDate = NULL
                    FROM SaveInfo SI
                    INNER JOIN Users U
                    ON U.SaveInfoId = SI.SaveInfoId
                    WHERE U.UserName = @userName";

            command.Parameters.AddWithValue("@userName", UserName);

            command.ExecuteNonQuery();
        }
    }

    /// <summary> Gets the user progress
	/// </summary>
	public static DataTable GetProgress()
    {
        DataTable userProgress = new DataTable();

        using (SqlConnection connection = new SqlConnection(GetNewConnection()))
        using (SqlCommand command = new SqlCommand(null, connection))
        {
            connection.Open();

            command.CommandText =
                @"SELECT *
                FROM SaveInfo SI
                INNER JOIN Users U
                ON U.SaveInfoId = SI.SaveInfoId
                WHERE U.UserName = @userName";

            command.Parameters.AddWithValue("@userName", UserName);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                userProgress.Load(reader);
            }
        }

        return userProgress;
    }

    /// <summary> Saves the settings
    /// </summary>
    public void Save()
	{
		SaveSettings (brightnessSlider.value, sensitivitySlider.value, (int)fontSizeSlider.value, gameVolumeSlider.value);

        RefreshSettings();
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
       brightnessSliderValue.text = value.ToString ("F2");
	}

	public void ChangeSensValue(float value)
	{
		sensitivitySliderValue.text = value.ToString ("F2");
	}

	public void ChangeFontSizeValue(float value)
	{
		fontSizeSliderValue.text = value.ToString ();
	}

	public void ChangeGameVolumeValue(float value)
	{
		gameVolumeSliderValue.text = value.ToString ();
	}
    /*
    // untested
    public void GetCertificate()
    {
        PdfDocument pdf = PdfReader.Open("BlankCertificate.pdf");
        XGraphics gfx = XGraphics.FromPdfPage(pdf.Pages[0]);
        XFont mainFont = new XFont("Arial", 24, XFontStyle.Bold);

        XTextFormatter xtf = new XTextFormatter(gfx);
        xtf.Alignment = XParagraphAlignment.Center;
        xtf.DrawString(UserName, mainFont, XBrushes.Black, new XRect(0, pdf.Pages[0].Height / 2 - 30, pdf.Pages[0].Width, 50), XStringFormats.TopLeft);

        // need to get score
        xtf.DrawString("XX", mainFont, XBrushes.Black, new XRect(0, pdf.Pages[0].Height / 2 + 85, pdf.Pages[0].Width, 50), XStringFormats.TopLeft);

        XFont font = new XFont("Arial", 18, XFontStyle.Bold);
        xtf.DrawString("Isabel Perry", font, XBrushes.Black, new XRect(100, pdf.Pages[0].Height / 2 + 140, pdf.Pages[0].Width / 3, 50), XStringFormats.TopLeft);

        // need to get score
        xtf.DrawString(DateTime.Now.ToString("MM/dd/yyyy"), font, XBrushes.Black, new XRect(pdf.Pages[0].Height / 2 - 50, pdf.Pages[0].Height / 2 + 140, 50, 50), XStringFormats.TopLeft);

        // need to get certificate ID
        xtf.DrawString("123456789", font, XBrushes.Black, new XRect(pdf.Pages[0].Height / 2 + 25, pdf.Pages[0].Height / 2 + 140, pdf.Pages[0].Width / 3, 50), XStringFormats.TopLeft);

        // need to get score
        xtf.DrawString("3251 Progress Dr., Suite 105, Orlando, FL 32826", font, XBrushes.Black, new XRect(0, pdf.Pages[0].Height / 2 + 185, pdf.Pages[0].Width, 50), XStringFormats.TopLeft);

        pdf.Save(UserName + "Certfiicate.pdf");
    }*/

    public void RefreshSettings()
    {
        texts = Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[];

        // gets the user's current settings
        DataTable initialSettings = GetSettings();
        sensitivitySlider.value = float.Parse(initialSettings.Rows[0][0].ToString());
        brightnessSlider.value = float.Parse(initialSettings.Rows[1][0].ToString());
        fontSizeSlider.value = float.Parse(initialSettings.Rows[2][0].ToString());
        gameVolumeSlider.value = float.Parse(initialSettings.Rows[3][0].ToString());

        if (Application.loadedLevelName != "MainMenu")
        {
            // sets up the initial sensitivity of mouselook
            player.GetComponent<MouseLook>().sensitivityX = 5 * sensitivitySlider.value;
            Camera.main.GetComponent<MouseLook>().sensitivityY = 7.5f * sensitivitySlider.value;

            // sets up the initial game brightness
            foreach (Light lt in FindObjectsOfType<Light>())
            {
                lt.intensity = 1.5f * brightnessSlider.value;
            }
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
        AudioListener.volume = gameVolumeSlider.value * 0.25f;
    }
}