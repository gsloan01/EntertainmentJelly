using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Handgun : MonoBehaviour
{
    //Animator for arm & gun
    Animator animator;

    //Audio
    AudioSource audio;

    //Player Camera
    public Camera playerCamera;

    //Ammo Info
    public int clipSize = 8;
    public int ammo = 8;
    private int currentClip = 8;

    //Prefabs
    public GameObject hitFX;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
    }

    private void CheckFire()
    {
        if (Input.GetButtonDown("Fire1") && (currentClip > 0))
        {
            currentClip -= 1;

            animator.Play("Fire", 0, 0);

            //Play Sound

            //Hitscan
            FireShot();
        }
    }

    private void FireShot()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Instantiate(hitFX, hit.point, Quaternion.identity);
        }
    }
}
