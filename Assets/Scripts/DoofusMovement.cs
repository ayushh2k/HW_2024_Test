using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DoofusMovement : MonoBehaviour
{
    public float speed = 30f; // Default speed, will be overridden by JSON data
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FetchData());
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
                // Parse JSON data
                string json = request.downloadHandler.text;
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                // Apply fetched data
                speed = gameData.player_data.speed;
                Debug.Log("Speed set to: " + speed);
            }
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
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
