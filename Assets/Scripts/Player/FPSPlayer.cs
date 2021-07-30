using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSPlayer : MonoBehaviour
{

    public float speed = 4;
    public float mouseSensitivity = 80.0f;

    public GameObject headTransform;

    CharacterController charController;

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
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        MoveCamera();
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

        //Rotate up and down
        headTransform.transform.Rotate(new Vector3(LookY, 0, 0) * Time.deltaTime);


    }
}
