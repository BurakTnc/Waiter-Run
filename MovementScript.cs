using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;
using TMPro;




public class MovementScript : MonoBehaviour
{
    public static bool onTrap = false;
    public static Rigidbody rb;
    public float speed;
    public static bool onBonus = false;
    public List<GameObject> glasses = new List<GameObject>();
    public Transform prevObjectLeft, prevObjectRight;
    public Animator animator;
    public static bool onLeft = false;
    public GameObject fakeTray;
    public GameObject leftTray, rightTray;
    public static MovementScript instance;
    public GameObject redDropEffect, greenDropEffect, blueDropEffect;
    public List<GameObject> customers = new List<GameObject>();
    private int lastCount;
    public new Camera camera;
    public AudioClip whoopSound;
    public GameObject fakeWall;
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        instance = this;
        fakeWall = GameObject.Find("FakeWall");

    }
    void Start()
    {
        onBonus = false;
        onTrap = false;

    }

    private void Update()
    {
        if (GameManager.isGameOver == false && !onBonus)
        {

            Movement();

        }
    }
    void LateUpdate()
    {

        if (GameManager.isGameOver == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeHand();
                if (animator.GetBool("onLeft"))
                {
                    StartCoroutine(StartFollowLeft());

                }
                else
                {
                    StartCoroutine(StartFollowRight());
                }

            }
            else if (animator.GetBool("onLeft"))
            {
                GlassFollowLeft();
            }
            else
            {
                GlassFollowRight();
            }
        }


    }
    void FakeTrayOff()
    {
        fakeTray.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        if (animator.GetBool("onLeft"))
        {
            leftTray.SetActive(true);
        }
        else
        {
            rightTray.SetActive(true);
        }
    }
    void ChangeHand()
    {
        AudioSource.PlayClipAtPoint(whoopSound, transform.position);
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        onLeft = !onLeft;
        fakeTray.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        if (animator.GetBool("onLeft"))
        {
            leftTray.SetActive(false);
            fakeTray.transform.DOLocalMoveX(1.5f, 0.3f).SetRelative(true).OnComplete(() => FakeTrayOff());
            animator.SetBool("onLeft", false);
        }
        else
        {
            rightTray.SetActive(false);
            fakeTray.transform.DOLocalMoveX(-1.5f, 0.3f).SetRelative(true).OnComplete(() => FakeTrayOff());
            animator.SetBool("onLeft", true);
        }
    }
    void Movement()
    {
        if (!onTrap)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }

    IEnumerator StartFollowLeft()
    {
        if (glasses.Count > 0)
        {
            for (int i = 0; i < glasses.Count; i++)
            {
                if (i == 0)
                {

                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, prevObjectLeft.transform.position.x, 0.001f), Mathf.Lerp(glasses[i].transform.position.y, prevObjectLeft.transform.position.y + 0.4f, 0.001f), prevObjectLeft.transform.position.z);
                    if (glasses[i].transform.tag == "RedGlass")
                    {
                        Instantiate(redDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "GreenGlass")
                    {
                        Instantiate(greenDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "glassOnList")
                    {
                        Instantiate(blueDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }

                    yield return new WaitForSeconds(.2f);
                }

                else
                {
                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, glasses[i - 1].transform.position.x, 0.001f), Mathf.Lerp(glasses[i].transform.position.y, glasses[i - 1].transform.position.y + 0.25f, 0.001f), glasses[i - 1].transform.position.z);
                    if (glasses[i].transform.tag == "GreenGlass")
                    {
                        Instantiate(redDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "GreenGlass")
                    {
                        Instantiate(greenDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "glassOnList")
                    {
                        Instantiate(blueDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                }
            }
        }

    }

    IEnumerator StartFollowRight()
    {
        if (glasses.Count > 0)
        {
            for (int i = glasses.Count - 1; i > -1; i--)
            {
                if (i == glasses.Count - 1)
                {

                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, prevObjectRight.transform.position.x, 0.001f), Mathf.Lerp(glasses[i].transform.position.y, prevObjectRight.transform.position.y + 0.4f, 0.001f), prevObjectRight.transform.position.z);
                    if (glasses[i].transform.tag == "RedGlass")
                    {
                        Instantiate(redDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "GreenGlass")
                    {
                        Instantiate(greenDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "glassOnList")
                    {
                        Instantiate(blueDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    yield return new WaitForSecondsRealtime(.2f);
                }
                else
                {
                    yield return new WaitForSeconds(0.02f);
                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, glasses[i + 1].transform.position.x, 0.001f), Mathf.Lerp(glasses[i].transform.position.y, glasses[i + 1].transform.position.y + .25f, 0.001f), glasses[i + 1].transform.position.z);
                    if (glasses[i].transform.tag == "FullGlass")
                    {
                        Instantiate(redDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "GreenGlass")
                    {
                        Instantiate(greenDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                    if (glasses[i].transform.tag == "glassOnList")
                    {
                        Instantiate(blueDropEffect, glasses[i].transform.position, Quaternion.identity);
                    }
                }
            }
        }


    }

    void GlassFollowLeft()
    {
        if (glasses.Count > 0)
        {
            for (int i = 0; i < glasses.Count; i++)
            {
                if (i == 0)
                {
                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, prevObjectLeft.transform.position.x, 0.5f), Mathf.Lerp(glasses[i].transform.position.y, prevObjectLeft.transform.position.y + 0.4f, 0.3f), prevObjectLeft.transform.position.z);

                }
                else
                {
                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, glasses[i - 1].transform.position.x, 0.1f), Mathf.Lerp(glasses[i].transform.position.y, glasses[i - 1].transform.position.y + 0.25f, 0.2f), glasses[i - 1].transform.position.z);
                }
            }
        }

    }
    void GlassFollowRight()
    {
        if (glasses.Count > 0)
        {
            for (int i = glasses.Count - 1; i > -1; i--)
            {
                if (i == glasses.Count - 1)
                {
                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, prevObjectRight.transform.position.x, 0.5f), Mathf.Lerp(glasses[i].transform.position.y, prevObjectRight.transform.position.y + 0.4f, 0.3f), prevObjectRight.transform.position.z);

                }
                else
                {
                    glasses[i].transform.position = new Vector3(Mathf.Lerp(glasses[i].transform.position.x, glasses[i + 1].transform.position.x, 0.1f), Mathf.Lerp(glasses[i].transform.position.y, glasses[i + 1].transform.position.y + .25f, 0.2f), glasses[i + 1].transform.position.z);
                }
            }
        }

    }

    IEnumerator ServeTheGlasses()
    {
        for (int i = 0; i < lastCount; i++)
        {
            if (i == 4)
            {
                transform.DORotate(new Vector3(0, 90, 0), .8f);
                fakeWall.SetActive(false); // For Camera Angle
            }
            else if (i == 7)
            {
                transform.DORotate(new Vector3(0, 90, 0), .8f).SetRelative(true);
            }
            transform.DOMoveX(customers[i].transform.position.x, 1.2f).OnComplete(() => MoveNext(customers[i + 1]));
            transform.DOMoveZ(customers[i].transform.position.z, 1.2f);
            yield return new WaitForSeconds(1f);
            if (i == lastCount - 1)
            {
                GameManager.instance.Win();
            }

            
        }
    }
    void MoveNext(GameObject other)
    {
        if (glasses.Count > 0)
        {
            transform.DOMoveX(other.transform.position.x, 1.2f);
            transform.DOMoveZ(other.transform.position.z, 1.2f);

        }

    }
    void RotateCamera()
    {
        camera.transform.DORotate(new Vector3(13, -60, 0), 5f);
        CamFollow.xOffset += 2;
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Finish":
                CamFollow.onBonus = true;
                if (!animator.GetBool("onLeft"))
                {
                    ChangeHand();
                }
                camera.transform.DORotate(new Vector3(13, -55, 0), 1f).OnComplete(() => RotateCamera());
                lastCount = glasses.Count;
                StartCoroutine(ServeTheGlasses());
                onTrap = true;
                break;
        }

    }

}
