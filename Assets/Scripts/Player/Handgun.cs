using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Handgun : MonoBehaviour
{
    //Animator for arm & gun
    Animator animator;

    //Particles & light
    public ParticleSystem flashParticles;
    public ParticleSystem sparkParticles;
    public Light muzzleLight;

    //Audio
    AudioSource audio;
    public AudioClip shotClip;
    public AudioClip reloadClip;

    //Player Camera
    public Camera playerCamera;
    public float bobbingSpeed = 14f;

    //UI elements
    public Text totalAmmoText;
    public Text currentClipText;

    //Ammo Info
    public int clipSize = 8;
    public int ammo = 8;
    private int currentClip = 8;
    public float fireRate = .3f;
    private float fireTime;

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
        CheckReload();

        UpdateUIText();
    }

    private void UpdateUIText()
    {
        currentClipText.text = currentClip + "";
        totalAmmoText.text = ammo + "";
    }

    private void CheckFire()
    {
        fireTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && (currentClip > 0) && fireTime >= fireRate)
        {
            fireTime = 0;
            currentClip -= 1;

            animator.Play("Fire", 0, 0);

            //Play flash
            StartCoroutine(PlayFlash());

            //Emit Particles
            flashParticles.Emit(1);
            sparkParticles.Emit(1);

            //Play Sound
            audio.clip = shotClip;
            audio.Play();

            //Hitscan
            FireShot();
        }
    }

    private IEnumerator PlayFlash()
    {
        muzzleLight.enabled = true;
        yield return new WaitForSeconds(.02f);
        muzzleLight.enabled = false;
    }

    private void FireShot()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Create hitFX
            Instantiate(hitFX, hit.point, Quaternion.identity);

            //Remove health from object hit
            hit.collider.GetComponent<Health>()?.SubtractHealth(1);

            //Try to grab EnemyManager
            EnemyMovement enemyMovement = hit.collider.gameObject.GetComponentInParents<EnemyMovement>();
            if (enemyMovement)
            {
                enemyMovement.currentTarget = FPSPlayer.Instance;
            }
        }
    }

    private void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && (currentClip < clipSize) && (ammo > 0))
        {
            //Play animation, animation will call ReloadAmmo()
            animator.Play("Reload Ammo Left", 0, 0);

            //Play Sound
            audio.clip = reloadClip;
            audio.Play();
        }
    }

    public void ReloadAmmo()
    {
        int ammoTaken = Mathf.Min(ammo, (clipSize - currentClip));
        ammo -= ammoTaken;
        currentClip += ammoTaken;
    }
}
