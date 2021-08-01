using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPSPlayer : MonoBehaviour
{

    public float speed = 4;
    public float mouseSensitivity = 80.0f;

    public float bobbingSpeed = 2f;
    [Range(0, 1)]
    public float crouchBobRatio = .5f;

    private float normalizedBobbingSpeed
    {
        get { return (Mathf.PI) / (bobbingSpeed / crouchBobMultiplier); }
    }

    private float crouchBobMultiplier
    {
        get { return (isCrouching) ? crouchBobRatio : 1f; }
    }

    private float firstStep
    {
        get { return normalizedBobbingSpeed / 4.0f; }
    }

    private float movementSpeed
    {
        get { return speed * movementMultiplier; }
    }

    private float movementMultiplier
    {
        get { return (isCrouching) ? crouchSpeedRatio : 1f; }
    }

    public float bobbingPeak = .2f;
    public float swivelPeak = .4f;
    private float bobbingTime = 0;
    private float swivelTime = 0;
    private bool takenStep = false;

    private bool isCrouching = false;
    public float crouchingTime = .3f;
    public float crouchSpeedRatio = .5f;

    private float xRotation;

    public GameObject headTransform;
    public GameObject swivelObject;
    public GameObject playerCamera;
    public Image damageImage;
    private Vector3 camPosition;
    private Vector3 headPosition;

    private Health playerHealth;
    private CharacterController charController;
    private AudioSource audio;
    public List<AudioClip> steps = new List<AudioClip>();

    private static FPSPlayer instance;
    public static FPSPlayer Instance
    {
        get { return instance; }
    }

    public float Move { 
        get { return Input.GetAxisRaw("Vertical"); }
    }

    public float Strafe
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }

    public float LookX
    {
        get { return (Input.GetAxis("Mouse X") + staggerView.x) * mouseSensitivity; }
    }

    public float LookY
    {
        get { return (Input.GetAxis("Mouse Y") + staggerView.y) * mouseSensitivity * -1; }
    }

    private Vector2 staggerView = new Vector2();

    private void Awake()
    {
        
        headPosition = headTransform.transform.localPosition;
        camPosition = playerCamera.transform.localPosition;

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerHealth = GetComponent<Health>();
        audio = GetComponent<AudioSource>();
        charController = GetComponent<CharacterController>();
        //headTransform.transform.rotation.SetLookRotation(Vector3.forward, Vector3.up);
        //transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        //Debug.Log(Input.GetAxis("Mouse X") + " | " + Input.GetAxis("Mouse Y"));
        ReduceStagger();
        UpdateUI();
        MoveCamera();
        MovePlayer();
        ViewBobbing();
        CrouchPlayer();
        //PlaySounds();
    }

    private void UpdateUI()
    {
        //HealthUI
        float currentHealthRatio = (playerHealth.GetHealth() / playerHealth.maxHealth) * 1f;
        damageImage.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), currentHealthRatio);
    }

    private void ReduceStagger()
    {
        staggerView = Vector2.Lerp(staggerView, Vector2.zero, Time.deltaTime * 14);
        
    }

    public void Stagger()
    {
        float randAngle = Random.Range(0, 360);
        Vector2 direction = new Vector2(Mathf.Cos(randAngle * Mathf.Deg2Rad), Mathf.Sin(randAngle * Mathf.Deg2Rad));

        staggerView = direction * 8.0f;
    }

    private void PlaySounds()
    {
        if (Mathf.Abs(Move) > 0.1f || Mathf.Abs(Strafe) > 0.1f)
        {
            if (!audio.isPlaying) audio.Play();
        } else
        {
            if (audio.isPlaying) audio.Stop();
        }
    }

    private void ViewBobbing()
    {
        if (Mathf.Abs(Move) > 0.1f || Mathf.Abs(Strafe) > 0.1f)
        {
            bobbingTime += Time.deltaTime * normalizedBobbingSpeed;
            swivelTime += Time.deltaTime * normalizedBobbingSpeed;
            if (bobbingTime > normalizedBobbingSpeed)
            {
                bobbingTime -= normalizedBobbingSpeed;
                takenStep = false;
            }
            
            //Debug.Log(bobbingTime + ": " + Mathf.Sin(bobbingTime));

            float sinv = Mathf.Sin(bobbingTime);
            float sin2 = Mathf.Sin(swivelTime / 2);

            Vector3 bobOffset = new Vector3(0, sinv * bobbingPeak, 0);
            //Debug.Log(bobOffset);
            playerCamera.transform.localPosition = bobOffset + camPosition;

            Quaternion quat = Quaternion.Euler(0, 0, sin2 * swivelPeak);
            swivelObject.transform.localRotation = quat;

            //Debug.Log(bobbingTime + ": " + firstStep);
            if (!takenStep)
            {
                if (bobbingTime > firstStep)
                {
                    audio.clip = steps[Random.Range(0, steps.Count)];
                    audio.Play();
                    takenStep = true;
                }
            }
        } else
        {
            takenStep = false;
            bobbingTime = 0;

            Vector3 slowRest = Vector3.Lerp(playerCamera.transform.localPosition, camPosition, Time.deltaTime * 3);
            Quaternion slowRot = Quaternion.Lerp(swivelObject.transform.localRotation, Quaternion.identity, Time.deltaTime * 3);

            playerCamera.transform.localPosition = slowRest;
            swivelObject.transform.localRotation = slowRot;
        }
    }

    private void CrouchPlayer()
    {
        isCrouching = (Input.GetKey(KeyCode.LeftShift));

        Vector3 slowRest;
        if (isCrouching)
        {
            slowRest = Vector3.Lerp(headTransform.transform.localPosition, (new Vector3(0, -.5f, 0) + headPosition), Time.deltaTime / crouchingTime);
            headTransform.transform.localPosition = slowRest;
        } else
        {
            slowRest = Vector3.Lerp(headTransform.transform.localPosition, headPosition, Time.deltaTime / crouchingTime);
            headTransform.transform.localPosition = slowRest;
        }
        //Debug.Log("SlowRest: " + slowRest);
    }

    private void MovePlayer()
    {
        Vector3 input = new Vector3(Strafe, 0, Move);
        
        Vector3 movement = transform.TransformDirection(input).normalized;

        movement = movement * movementSpeed * Time.deltaTime;
        charController.Move(movement);
    }

    private void MoveCamera()
    {
        //Debug.Log("MouseX: " + LookX + "|MouseY: "+ LookY);
        //Rotate left<->right
        transform.Rotate(new Vector3(0, Mathf.Clamp(LookX * Time.deltaTime, -5, 5), 0));

        //Clamp up and down
        xRotation += Mathf.Clamp(LookY * Time.deltaTime, -5, 5);
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        headTransform.transform.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
    }
}
