using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	public void LoadMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadTest()
	{
		SceneManager.LoadScene("Level01(Intento de Union)");
	}

	public void LoadRunes()
	{
		SceneManager.LoadScene("Runes");
	}
    public void LoadCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void ShadowDancer()
    {
        CharacterSelection.ShadowDancer();
    }
    public void CrystalSowrd()
    {
        CharacterSelection.CrystalSword();
    }
}
