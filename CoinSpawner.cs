using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;
    public GameObject target;
    public Vector3 vector;
    void Start()
    {

    }

    public void SpawnCoins()
    {
        int x, y;
        for (int i = 0; i < 15; i++)
        {
            x = Random.Range(-45, 46);
            y = Random.Range(-60, 41);
            //Instantiate(coin, Camera.main.WorldToScreenPoint(new Vector3(x, y, PlayerScript.rb.transform.position.z+200)), target.transform.rotation, target.transform);


        }
    }
}
