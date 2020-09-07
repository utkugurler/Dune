using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] renderers;
    public GameObject scoreObjects;
    // Start is called before the first frame update
    private Transform renderersLastVector;
    private Transform scoresLastVector;
    void Start()
    {
        renderersLastVector = GameObject.Find("Start").transform;
        scoresLastVector = GameObject.Find("StartScoreTrig").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlatform()
	{
        int random = Random.Range(0, renderers.Length - 1);
        GameObject platform = Instantiate(renderers[random], new Vector3(renderersLastVector.transform.position.x + 20, renderersLastVector.transform.position.y,
            renderersLastVector.transform.position.z), Quaternion.identity);
        renderersLastVector = platform.transform;
        random = Random.Range(0, renderers.Length - 1);
        GameObject platform2 = Instantiate(renderers[random], new Vector3(renderersLastVector.transform.position.x + 20, renderersLastVector.transform.position.y,
        renderersLastVector.transform.position.z), Quaternion.identity);
        renderersLastVector = platform2.transform;
    }

    public void SpawnScore()
	{
        GameObject platform = Instantiate(scoreObjects, new Vector3(scoresLastVector.transform.position.x + 20, scoresLastVector.transform.position.y,
            scoresLastVector.transform.position.z), Quaternion.identity);
        scoresLastVector = platform.transform;
        GameObject platform2 = Instantiate(scoreObjects, new Vector3(scoresLastVector.transform.position.x + 20, scoresLastVector.transform.position.y,
        scoresLastVector.transform.position.z), Quaternion.identity);
        scoresLastVector = platform2.transform;
    }
}
