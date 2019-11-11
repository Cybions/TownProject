using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float MoveSpeed = 1;
    public bool CanMove = true;
    //---Rotation
    public float sensitivityX = 5F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            CheckInputRotation();
            CheckMovementInputs();
            CheckActionInput();
        }
    }

    private void CheckMovementInputs()
    {
        Vector3 MovementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(MovementVector * MoveSpeed);
    }

    private void CheckInputRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
    }

    private void CheckActionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && CameraManager.Instance.ClosestFR != null)
        {
            CameraManager.Instance.ClosestFR.FarmResource();
        }
        if (isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            if(GetComponent<Rigidbody>().velocity.y < 0)
            {
                GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
            }
        }
 
    }
    private void Jump()
    {
        //print("Jumping");
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 50, 0),ForceMode.Impulse);
    }
    private bool isGrounded()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.down), 1.0f);
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.tag != "Player")
            {
                return true;
            }
        }
        return false;
    }
}
