using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public void SetNormalDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "Normal");
        LoadGameScene();
    }

    public void SetHardDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "Hard");
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Game"); // Replace with the actual name of your game scene
    }
}
