using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic; 

public class DoofusMovement : MonoBehaviour
{
    public float speed = 10f; 
    private PulpitManager pulpitManager; 
    private ScoreManager scoreManager; 
    private HashSet<Transform> visitedPulpits = new HashSet<Transform>(); 
    private Transform lastPulpit; 

    private void Start()
    {
        StartCoroutine(FetchData());
        pulpitManager = FindObjectOfType<PulpitManager>(); 
        scoreManager = FindObjectOfType<ScoreManager>(); 
    }

    private IEnumerator FetchData()
    {
        string url = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching data: " + request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                GameData gameData = JsonUtility.FromJson<GameData>(json);
  
                speed = gameData.player_data.speed;
                Debug.Log("Speed set to: " + speed);
            }
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = speed;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pulpit"))
        {
            Transform currentPulpit = other.transform;

            if (!visitedPulpits.Contains(currentPulpit))
            {
                visitedPulpits.Add(currentPulpit);
                scoreManager?.IncreaseScore(); 
                Debug.Log("Score: " + scoreManager?.score);
            }
        }
    }
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}
