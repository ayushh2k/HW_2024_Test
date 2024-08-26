using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab; // Assign this in the Unity Inspector
    public Transform doofusTransform; // Reference to Doofus for adjacent spawn
    public int maxPulpits = 2; // Maximum number of Pulpits allowed at once

    private List<GameObject> activePulpits = new List<GameObject>();
    private float minDestroyTime = 4f; // Default values, will be overridden by JSON
    private float maxDestroyTime = 5f;
    private float spawnTime = 2.5f;

    private void Start()
    {
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
                minDestroyTime = gameData.pulpit_data.min_pulpit_destroy_time;
                maxDestroyTime = gameData.pulpit_data.max_pulpit_destroy_time;
                spawnTime = gameData.pulpit_data.pulpit_spawn_time;
                Debug.Log("Pulpit Data: minDestroyTime = " + minDestroyTime + ", maxDestroyTime = " + maxDestroyTime + ", spawnTime = " + spawnTime);

                // Start spawning Pulpits
                StartCoroutine(SpawnPulpits());
            }
        }
    }

    private IEnumerator SpawnPulpits()
    {
        while (true)
        {
            // Spawn a new Pulpit if there are less than maxPulpits active
            if (activePulpits.Count < maxPulpits)
            {
                SpawnPulpit();
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnPulpit()
    {
        Vector3 spawnPosition;

        if (activePulpits.Count == 0)
        {
            // If there are no active Pulpits, spawn the first one at Doofus's position with y = 0
            spawnPosition = new Vector3(doofusTransform.position.x, 0, doofusTransform.position.z);
        }
        else
        {
            // Otherwise, spawn adjacent to the last active Pulpit
            GameObject lastPulpit = activePulpits[activePulpits.Count - 1];
            Vector3 lastPosition = lastPulpit.transform.position;

            // Randomly select an adjacent direction (up, down, left, right)
            Vector3[] directions = {
                new Vector3(9, 0, 0),  // Right
                new Vector3(-9, 0, 0), // Left
                new Vector3(0, 0, 9),  // Up
                new Vector3(0, 0, -9)  // Down
            };
            Vector3 selectedDirection = directions[Random.Range(0, directions.Length)];

            // Calculate the new spawn position with y = 0
            spawnPosition = lastPosition + selectedDirection;
            spawnPosition.y = 0; // Ensure y = 0
        }

        // Instantiate the Pulpit
        GameObject newPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);
        activePulpits.Add(newPulpit);

        // Start the despawn timer
        float destroyTime = Random.Range(minDestroyTime, maxDestroyTime);
        StartCoroutine(DespawnPulpit(newPulpit, destroyTime));
    }

    private IEnumerator DespawnPulpit(GameObject pulpit, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (pulpit != null)
        {
            activePulpits.Remove(pulpit);
            Destroy(pulpit);
        }
    }
}
