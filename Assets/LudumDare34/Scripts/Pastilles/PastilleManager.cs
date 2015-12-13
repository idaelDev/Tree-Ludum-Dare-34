using UnityEngine;
using System.Collections;

public class PastilleManager : Singleton<PastilleManager> {

    public Vector2 worldMinBound;
    public Vector2 worldMaxBound;

    public int nbPastilles = 100;

    public GameObject[] pastilles;
    private int[] pastillesCounter;

	// Use this for initialization
	void Start () {
        pastillesCounter = new int[pastilles.Length];
        SpawnPatilles();
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
            }
        }
    }
	
    private void PastilleGrabed(PastilleType type)
    {

    }


}
