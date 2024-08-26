using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public GameObject doofus; // Assign this in the Unity Inspector
    public Vector3 startPosition = new Vector3(0, 4, 0); // Adjust as needed

    public void StartGame()
    {
        if (doofus != null)
        {
            doofus.transform.position = startPosition;
        }
        SceneManager.LoadScene("Game");
    }
}
