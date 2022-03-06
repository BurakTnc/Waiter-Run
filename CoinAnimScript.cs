using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinAnimScript : MonoBehaviour
{
    public Transform target;
    private void Awake()
    {
        
    }
    void Start()
    {
        float f = Random.Range(0.7f,1.3f);
        float i = Random.Range(0.2f, 0.6f);
        StartCoroutine(waitForAnim(i, f));
        
    }
    IEnumerator waitForAnim(float i,float f)
    {
        yield return new WaitForSeconds(i);
        StartAnim(f);
    }
    void StartAnim( float f)
    {
        transform.DOMove(target.position, f).OnComplete(() => UpdateScore());
        transform.DOScale(Vector3.one, f);
    }
    void UpdateScore()
    {
        GameManager.currentMoney += 7;
        Destroy(gameObject);
    }
    void Update()
    {
        target = GameObject.Find("dolaricon").transform;
    }
}
