using UnityEngine;
using System.Collections;

public class PastilleManager : Singleton<PastilleManager> {

    public Vector2 worldMinBound;
    public Vector2 worldMaxBound;

    public int nbPastilles = 100;
    public int minZoneValue = 3;
    public int maxZoneValue = 8;


    public GameObject[] pastilles;
    
    private int[] pastillesCounter;
    private int zoneValue = 0;
    private int currentZonePastilleNb = 0;
    private int pastilleLeft = 0;

	// Use this for initialization
	void Start () {
        pastillesCounter = new int[pastilles.Length];
        SpawnPatilles();
        SetNewZone();
	}

    void SpawnPatilles()
    {
        GameObject buf;
        Vector3 spawnPosition  = Vector3.zero;
        int nbEach = nbPastilles / pastilles.Length;
        for (int i = 0; i < pastilles.Length; i++)
        {
            for (int j = 0; j < nbEach; j++)
            {
                spawnPosition.x = Random.Range(worldMinBound.x, worldMaxBound.x);
                spawnPosition.y = Random.Range(worldMinBound.y, worldMaxBound.y);

                buf = Instantiate(pastilles[i], spawnPosition, Quaternion.identity) as GameObject;
                buf.GetComponent<Pastille>().PastilleGrabEvent += PastilleGrabed;

                pastilleLeft++;
            }
        }
    }
	
    private void PastilleGrabed(PastilleType type)
    {
        for (int i = 0; i < pastilles.Length; i++)
        {
            if(type == pastilles[i].GetComponent<Pastille>().type)
            {
                pastillesCounter[i]++;
            }
        }
        currentZonePastilleNb++;
        pastilleLeft--;
        if(currentZonePastilleNb >= zoneValue)
        {
            SetNewZone();
        }
    }

    private void SetNewZone()
    {
        zoneValue = Mathf.Min(Random.Range(minZoneValue, maxZoneValue), pastilleLeft);
        currentZonePastilleNb = 0;
        Debug.Log("New Zone : " + zoneValue);
    }
}
