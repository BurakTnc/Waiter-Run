using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class GlassScript : MonoBehaviour
{
    public Material redMat, greenMat;
    public GameObject smallMoneyParticle, bigMoneyParticle, redSplashFX, greenSplashFX, destroyParticle;
    public AudioClip crackSound, fillSound, moneySound, glassSound;
    public GameObject coin;
    public GameObject target;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        target = GameObject.Find("GamePanel");
    }
    public void SpawnCoins(int x, int y)
    {
        int f = Random.Range(x, y);

        for (int i = 0; i < f; i++)
        {
            float randomX = Random.Range(-0.5f, 0.6f);
            float randomY = Random.Range(-0.5f, 0.05f);
            Instantiate(coin, Camera.main.WorldToScreenPoint(transform.position + new Vector3(randomX, randomY, 0)), target.transform.rotation, target.transform);
        }
    }
    void EarnFromGlass(GameObject other)
    {
        Instantiate(smallMoneyParticle, other.transform.position + Vector3.up * 0.3f, Quaternion.identity);
    }
    void EarnFromRedGlass(GameObject other)
    {
        Instantiate(bigMoneyParticle, other.transform.position + Vector3.up * 0.3f, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Table"))
        {

            AudioSource.PlayClipAtPoint(glassSound, transform.position);
            AudioSource.PlayClipAtPoint(moneySound, transform.position);
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            other.gameObject.tag = "Untagged";
            if (transform.tag == "GlassOnList")
            {
                SpawnCoins(1, 4);
                EarnFromGlass(other.gameObject);

            }
            else if (transform.tag == "RedGlass")
            {
                SpawnCoins(4, 9);
                EarnFromRedGlass(other.gameObject);
            }
            else if (transform.tag == "GreenGlass")
            {
                SpawnCoins(9, 16);
                EarnFromRedGlass(other.gameObject);
            }
            MovementScript.instance.glasses.Remove(transform.gameObject);
            transform.position = other.transform.position;

        }
        if (other.gameObject.CompareTag("Refill"))
        {
            if (transform.tag == "RedGlass")
            {
                MMVibrationManager.Haptic(HapticTypes.Warning);
                AudioSource.PlayClipAtPoint(fillSound, transform.position);
                transform.tag = "GreenGlass";
                transform.GetComponent<MeshRenderer>().material = greenMat;
                Instantiate(greenSplashFX, transform.position, Quaternion.identity);
            }
            if (transform.tag == "GlassOnList")
            {
                MMVibrationManager.Haptic(HapticTypes.Warning);
                AudioSource.PlayClipAtPoint(fillSound, transform.position);
                transform.tag = "RedGlass";
                transform.GetComponent<MeshRenderer>().material = redMat;
                Instantiate(redSplashFX, transform.position, Quaternion.identity);
            }
            


        }
        if (other.gameObject.tag == "Trap")
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            MovementScript.instance.glasses.Remove(transform.gameObject);
            if (transform.tag == "RedGlass")
            {
                Instantiate(redSplashFX, transform.position, Quaternion.identity);
            }
            if (transform.tag == "GreenGlass")
            {
                Instantiate(greenSplashFX, transform.position, Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(crackSound, transform.position);
            
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }
}
