using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class revolverProjectile : MonoBehaviour
{
    [SerializeField] private pickupScript pickupScriptObject;
    GameObject hitObject;
    string hitObjectReturn;
    public Canvas gunCanvas;
    public GameObject gun;
    int bullets;
    bool reloading = false;
    public float timeLeft = 5;
    public Text ammoText;
    public Text reloadText;
    void Start()
    {
        gun.SetActive(false);
        gunCanvas.enabled = false;
        bullets = 6;
    }

    void Update()
    {

        Vector3 inputPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector3)Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (pickupScriptObject.gunGot == true)
        {
            gunCanvas.enabled = true;
            gun.SetActive(true);

            ammoText.text = "Ammo: "+bullets;

            if (bullets <= 0)
            {
                reloading = true;
            }

            if (Input.GetMouseButtonDown(0) && reloading == false)
            {
                bullets--;
                Debug.Log("shot");

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        hitObjectReturn = hit.collider.gameObject.name;
                        hitObject = GameObject.Find(hitObjectReturn);

                        if (hit.collider.gameObject.tag == "enemy")
                        {
                            Debug.Log("enemy hit");
                            hit.collider.gameObject.gameObject.SetActive(false);
                        }

                    }
                }
            }
            if(Input.GetMouseButtonDown(0) && reloading)
            {
                //do nothing
            }

            if(Input.GetKey(KeyCode.R))
            {
                reloading = true;
            }

            if (reloading)
            {
                timeLeft = timeLeft - Time.deltaTime;
                Debug.Log(timeLeft);
                reloadText.enabled = true;
            }

            if (timeLeft < 0)
            {
                reloading = false;
                bullets = 6;

            }

            if (reloading == false)
            { 
                timeLeft = 5;
                reloadText.enabled = false;
            }

        }



    }
}
