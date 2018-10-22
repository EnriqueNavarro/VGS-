using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public static bool isPaused = false;
	public GameObject pauseMenu;
	public AudioMixer master;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}
	
	public void LoadMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void SetVolume(float v)
	{
		master.SetFloat("Volume", v);
	}
}
