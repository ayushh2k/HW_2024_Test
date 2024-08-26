using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Transform doofusTransform;
    public float fallThreshold = -10f; 
    public Transform initialPulpitTransform;

    public GameObject gameOverUI; 
    public GameObject gameOverText;
    public GameObject restartButton; 

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && doofusTransform.position.y < fallThreshold)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! Doofus fell off the platforms.");

        gameOverUI.SetActive(true);
        gameOverText.SetActive(true);
        restartButton.SetActive(true);

        FindObjectOfType<PulpitManager>().StopSpawning();
    }

    private void RestartGame()
    {
        doofusTransform.position = new Vector3(initialPulpitTransform.position.x, initialPulpitTransform.position.y + 4, initialPulpitTransform.position.z);

        if (doofusTransform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }

        SceneManager.LoadScene("Game");
    }
}
