using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public GameObject doofus;
    public Vector3 startPosition = new Vector3(0, 4, 0);

    public void StartGame()
    {
        if (doofus != null)
        {
            doofus.transform.position = startPosition;
        }
        SceneManager.LoadScene("Game");
    }
}
