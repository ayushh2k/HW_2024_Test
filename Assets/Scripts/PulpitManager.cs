using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab; 
    public Transform doofusTransform; 
    public int maxPulpits = 2; 

    private List<GameObject> activePulpits = new List<GameObject>();
    private float minDestroyTime = 4f; 
    private float maxDestroyTime = 5f;
    private float spawnTime = 2.5f;

    private Coroutine spawnCoroutine; 
    private bool isSpawningStopped = false; 

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
                string json = request.downloadHandler.text;
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                minDestroyTime = gameData.pulpit_data.min_pulpit_destroy_time;
                maxDestroyTime = gameData.pulpit_data.max_pulpit_destroy_time;
                spawnTime = gameData.pulpit_data.pulpit_spawn_time;
                Debug.Log("Pulpit Data: minDestroyTime = " + minDestroyTime + ", maxDestroyTime = " + maxDestroyTime + ", spawnTime = " + spawnTime);

                spawnCoroutine = StartCoroutine(SpawnPulpits());
            }
        }
    }

    private IEnumerator SpawnPulpits()
    {
        while (!isSpawningStopped)
        {
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
            spawnPosition = new Vector3(doofusTransform.position.x, 0, doofusTransform.position.z);
        }
        else
        {
            GameObject lastPulpit = activePulpits[activePulpits.Count - 1];
            Vector3 lastPosition = lastPulpit.transform.position;

            Vector3[] directions = {
                new Vector3(9, 0, 0),  // Right
                new Vector3(-9, 0, 0), // Left
                new Vector3(0, 0, 9),  // Up
                new Vector3(0, 0, -9)  // Down
            };
            Vector3 selectedDirection = directions[Random.Range(0, directions.Length)];

            spawnPosition = lastPosition + selectedDirection;
            spawnPosition.y = 0; 
        }

        GameObject newPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);
        activePulpits.Add(newPulpit);

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

    public void StopSpawning()
    {
        isSpawningStopped = true;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        foreach (GameObject pulpit in activePulpits)
        {
            Destroy(pulpit);
        }
        activePulpits.Clear();
    }

    public List<GameObject> GetActivePulpits()
    {
        return activePulpits;
    }
}
