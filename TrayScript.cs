using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class TrayScript : MonoBehaviour
{
    public AudioClip collectSound;

    void Start()
    {

    }


    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Glass"))
        {
            if (MovementScript.instance.glasses.Count < 13)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);
                other.gameObject.tag = "GlassOnList";
                other.transform.DOMoveY((1f + MovementScript.instance.glasses.Count / 4), .3f).SetRelative(true);
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                if (!MovementScript.instance.animator.GetBool("onLeft"))
                {
                    if (MovementScript.instance.glasses.Count > 0)
                    {
                        MovementScript.instance.glasses.Insert(0, other.gameObject);
                    }
                    else
                    {
                        MovementScript.instance.glasses.Add(other.gameObject);
                    }

                    StartCoroutine(ScaleUpRight());
                }
                else
                {
                    if (MovementScript.instance.glasses.Count > 0)
                    {
                        MovementScript.instance.glasses.Insert(MovementScript.instance.glasses.Count - 1, other.gameObject);
                    }
                    else
                    {
                        MovementScript.instance.glasses.Add(other.gameObject);
                    }

                   StartCoroutine(ScaleUpLeft());
                }
            }
            

        }
    }
    public IEnumerator ScaleUpRight()
    {

        if (MovementScript.instance.glasses.Count > 1)
        {
            for (int i = 0; i < MovementScript.instance.glasses.Count; i++)
            {
                Vector3 scale = new Vector3(300, 300, 300);
                scale *= 1.3f;
                MovementScript.instance.glasses[i].transform.DOScale(scale, 0.07f).OnComplete(() =>
                 MovementScript.instance.glasses[i].transform.DOScale(new Vector3(300, 300, 300), 0.07f));
                yield return new WaitForSeconds(0.07f);
            }
        }

    }
    public IEnumerator ScaleUpLeft()
    {

        if (MovementScript.instance.glasses.Count > 1)
        {
            for (int i = MovementScript.instance.glasses.Count - 1; i > 0; i--)
            {
                Vector3 scale = new Vector3(300, 300, 300);
                scale *= 1.3f;
                MovementScript.instance.glasses[i].transform.DOScale(scale, 0.07f).OnComplete(() =>
                 MovementScript.instance.glasses[i].transform.DOScale(new Vector3(300, 300, 300), 0.07f));
                yield return new WaitForSeconds(0.07f);
            }
        }


    }
}
