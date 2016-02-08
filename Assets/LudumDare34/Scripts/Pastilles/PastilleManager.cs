using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PastilleManager : Singleton<PastilleManager> {

    public Vector2 worldMinBound;
    public Vector2 worldMaxBound;

	public GameObject cercle;
	public Sprite spriteCercleBleu;
	public Sprite spriteCercleVert;


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
        StartCoroutine( SpawnPatilles());
        SetNewZone();
	}

    IEnumerator SpawnPatilles()
    {
		int cpt = 0;
        GameObject buf;
        Vector3 spawnPosition  = Vector3.zero;
        int nbEach = nbPastilles / pastilles.Length;
        for (int i = 0; i < pastilles.Length; i++)
        {
			for (int j = 0; j < nbEach; j++)
			{
				int countTime = 0;
				yield return new WaitForFixedUpdate();
				do
				{
					spawnPosition.x = Random.Range(worldMinBound.x, worldMaxBound.x);
					spawnPosition.y = Random.Range(worldMinBound.y, worldMaxBound.y);
					countTime++;
					/*if(countTime < 1000)
					{
						
						break;
					}*/
				}
				while (isTooClose(spawnPosition));

				buf = Instantiate(pastilles[i], spawnPosition, Quaternion.identity) as GameObject;
				buf.GetComponent<Pastille>().PastilleGrabEvent += PastilleGrabed;
				positions.Add(buf.transform.position);

				buf.transform.SetParent(transform);
				if (cpt == 1) {
					GameObject newCercle = Instantiate(cercle, spawnPosition, Quaternion.identity) as GameObject;
					cercle.GetComponent<SpriteRenderer>().sprite = spriteCercleBleu;
					newCercle.transform.position += new Vector3(0f, 0.03f, 0f);
					newCercle.transform.parent = buf.transform;
					buf.GetComponent<Pastille>().bType = BonusType.SLOW;
                } else if(cpt == 2) {
					GameObject newCercle = Instantiate(cercle, spawnPosition, Quaternion.identity) as GameObject;
					cercle.GetComponent<SpriteRenderer>().sprite = spriteCercleVert;
                    newCercle.transform.position += new Vector3(0f, 0.03f, 0f);
					newCercle.transform.parent = buf.transform;
					buf.GetComponent<Pastille>().bType = BonusType.FAST;
				} else {
					buf.GetComponent<Pastille>().bType = BonusType.NORMAL;
				}
				cpt = (cpt + 1) % 3;

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
            if(type == pastilles[i].GetComponent<Pastille>().pType)
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
		if(Application.isEditor)
			Debug.Log("New Zone : " + zoneValue);
    }
}
