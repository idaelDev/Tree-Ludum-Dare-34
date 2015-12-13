using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PastilleManager : Singleton<PastilleManager> {

    public Vector2 worldMinBound;
    public Vector2 worldMaxBound;

    public int nbPastilles = 100;
    public int minZoneValue = 3;
    public int maxZoneValue = 8;

    public float minDistance = 1f;

    public GameObject[] pastilles;
    
    private int[] pastillesCounter;
    private int zoneValue = 0;
    private int currentZonePastilleNb = 0;
    private int pastilleLeft = 0;

    private List<Vector3> positions;

	// Use this for initialization
	void Start () {
        pastillesCounter = new int[pastilles.Length];
        positions = new List<Vector3>();
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
                do
                {
                    spawnPosition.x = Random.Range(worldMinBound.x, worldMaxBound.x);
                    spawnPosition.y = Random.Range(worldMinBound.y, worldMaxBound.y);
                }
                while (isTooClose(spawnPosition));



                buf = Instantiate(pastilles[i], spawnPosition, Quaternion.identity) as GameObject;
                buf.GetComponent<Pastille>().PastilleGrabEvent += PastilleGrabed;

                positions.Add(buf.transform.position);

                pastilleLeft++;
            }
        }
    }

    private bool isTooClose(Vector3 pos)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            if(Vector3.Distance(pos, positions[i]) < minDistance)
            {
                return true;
            }

        }
        return false;
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
