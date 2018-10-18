using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;

	private string menu = "MainMenu";
	private SaveData SaveData;
	private string saveName = "save.json";

	void Awake()
	{
		
		if (instance == null)
		{
			instance = this;
		}else if (instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
		
		LoadGameData();
	}
	
	public void EndGame()
	{
		SaveProgress();
		SceneManager.LoadScene("ScoreScreen");
	}
	
	private void SaveProgress()
	{
		//Create a new Save data
		SaveData sD = new SaveData();

		//Assign current status to that saved data
		//Declare what will be saved of the player
        
		//Parse it to JSON
		string dataJson = JsonUtility.ToJson(sD);
		string saveState = Path.Combine(Application.persistentDataPath, saveName);

		//Write file
		File.WriteAllText(saveState, dataJson);
	}

	//Load current data from saved data
	private void LoadGameData()
	{
		string filePath = Path.Combine(Application.persistentDataPath, saveName);
        
		if (File.Exists(filePath))
		{
			string dataJson = File.ReadAllText(filePath);
            
			SaveData loadData = JsonUtility.FromJson<SaveData>(dataJson);

			//Load stored data into current execution
		}
		else
		{
			Debug.LogError("No Save File");   
		}
	}


}
