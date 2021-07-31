using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPSPlayer : MonoBehaviour
{

    public float speed = 4;
    public float mouseSensitivity = 80.0f;

    public float bobbingSpeed = 2f;
    private float normalizedBobbingSpeed
    {
        get { return (Mathf.PI) / bobbingSpeed; }
    }
    private float firstStep
    {
        get { return normalizedBobbingSpeed / 4.0f; }
    }
    public float bobbingPeak = .2f;
    private float bobbingTime = 0;
    private bool takenStep = false;

    private float xRotation;

    public GameObject headTransform;
    public GameObject playerCamera;
    private Vector3 startPosition;

    CharacterController charController;
    AudioSource audio;
    public List<AudioClip> steps = new List<AudioClip>();

    public float Move { 
        get { return Input.GetAxis("Vertical"); }
    }

    public float Strafe
    {
        get { return Input.GetAxis("Horizontal"); }
    }

    public float LookX
    {
        get { return Input.GetAxis("Mouse X") * mouseSensitivity; }
    }

    public float LookY
    {
        get { return Input.GetAxis("Mouse Y") * mouseSensitivity * -1; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        audio = GetComponent<AudioSource>();
        charController = GetComponent<CharacterController>();

        startPosition = playerCamera.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        MoveCamera();
    }

    private void Update()
    {
        ViewBobbing();
        //PlaySounds();
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
            if (bobbingTime > normalizedBobbingSpeed)
            {
                bobbingTime -= normalizedBobbingSpeed;
                takenStep = false;
            }
            //Debug.Log(bobbingTime + ": " + Mathf.Sin(bobbingTime));

            float sinv = Mathf.Sin(bobbingTime);

            Vector3 bobOffset = new Vector3(0, sinv * bobbingPeak, 0);
            playerCamera.transform.localPosition = bobOffset + startPosition;

            Debug.Log(bobbingTime + ": " + firstStep);
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

            Vector3 slowRest = Vector3.Lerp(playerCamera.transform.localPosition, startPosition, Time.deltaTime);

            playerCamera.transform.localPosition = slowRest;
        }
    }


    private void MovePlayer()
    {
        Vector3 input = new Vector3(Strafe, 0, Move);
        Vector3 movement = transform.TransformDirection(input);

        movement = movement * speed * Time.deltaTime;
        charController.Move(movement);
    }

    private void MoveCamera()
    {
        //Debug.Log("MouseX: " + LookX + "|MouseY: "+ LookY);
        //Rotate left<->right
        transform.Rotate(new Vector3(0, LookX, 0) * Time.deltaTime);

        //Clamp up and down
        xRotation += (LookY * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        headTransform.transform.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
    }
}
