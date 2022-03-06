using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    private Transform bonusTarget;
    public static bool onBonus;
    public Vector3 offset;
    public static int xOffset = 4;
    public float Speed = 0.5f;



    void Start()
    {
        onBonus = false;
    }
    void LateUpdate()
    {
        if (onBonus)
        {

            Vector3 targetPos = new Vector3(target.transform.position.x + xOffset, target.position.y + 1, target.transform.position.z+2 );
            Speed = 1f;
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPos, Speed);

        }
    }

    void Update()
    {
        if (!onBonus)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x + offset.x, target.position.y + offset.y, target.transform.position.z + offset.z);
            Speed = 1f;
            gameObject.transform.position = targetPos;

        }


    }
}
