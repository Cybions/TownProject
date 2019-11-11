using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    private int CameraFrameDelay = 30;
    private List<Vector3> CameraSpotsPosition;
    [SerializeField]
    private Transform CameraSpotPosition;
    public bool CanMoveCamera = true;
    //---Rotation
    public float sensitivityY = 5F;
    public float minimumY = -60F;
    public float maximumY = 30F;
    float rotationY = 0F;
    // What Am I Looking At
    public List<FarmableResource> OldFarmableResources;
    private float MaxDistanceOfDetection = 15;
    public FarmableResource ClosestFR = null;
    // Start is called before the first frame update
    void Start()
    {
        SetupCameraMovement();
        OldFarmableResources = new List<FarmableResource>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMoveCamera)
        {
            CameraZoom();
            CheckInputRotation();
            GetLookAtPosition();
            //CheckOcclusion();
            //MoveCameraToSpot();
            //CameraSpotsPosition.Add(CameraSpotPosition.position);
        }
    }

    public Vector3 GetLookAtPosition()
    {
        RaycastHit[] hits;
        // Does the ray intersect any objects excluding the player layer
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity);
        Vector3 groundReturn = Vector3.zero;
        List<Material> newMatFaded = new List<Material>();
        List<FarmableResource> newFarmables = new List<FarmableResource>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Ground")
            {
                groundReturn = hit.point;
            }
            if(hit.transform.tag == "Farmable")
            {
                //print(Vector3.Distance(hit.transform.position, Camera.main.transform.position));
                if(Vector3.Distance(hit.transform.position,Camera.main.transform.position) <= MaxDistanceOfDetection)
                {
                    newFarmables.Add(hit.transform.GetComponent<FarmableResource>());
                }
            }
        }
        if(groundReturn == Vector3.zero)
        {
            Debug.LogError("No Ground Found - GameManager must replace player");
        }
        CheckAllFarmables(newFarmables);
        return groundReturn;
    }

    private void CheckAllFarmables(List<FarmableResource> newFarmables)
    {
        foreach(FarmableResource fr in OldFarmableResources)
        {
            if(fr == null) { OldFarmableResources.Remove(fr); return; }
            if (!newFarmables.Contains(fr))
            {
                fr.HideIcon();
            }
        }
        ClosestFR = null;
        foreach (FarmableResource fr in newFarmables)
        {
            if(ClosestFR == null || Vector3.Distance(fr.transform.position, Camera.main.transform.position) < Vector3.Distance(ClosestFR.transform.position, Camera.main.transform.position))
            {
                ClosestFR = fr;
            }
        }
        if(ClosestFR != null)
        {
            ClosestFR.DisplayIcon();
        }
        OldFarmableResources = newFarmables;
    }

    private void SetupCameraMovement()
    {
        CanMoveCamera = false;
        CameraSpotsPosition = new List<Vector3>();
        int i = 0;
        while(i != CameraFrameDelay)
        {
            CameraSpotsPosition.Add(CameraSpotPosition.position);
            i++;
        }
        transform.position = CameraSpotPosition.position;
        CanMoveCamera = true;
    }
    private void MoveCameraToSpot()
    {
        transform.position = CameraSpotsPosition[0];
        CameraSpotsPosition.RemoveAt(0);
    }

    private void CheckInputRotation()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
    }

    private void CameraZoom()
    {
        float positiveRotY = ((maximumY * 2) + (rotationY))-60 ;
        float result = (positiveRotY * (-0.1f)) + 5; // -0.05 = (-3 / 60) // by Cedric JEHAN <3

        Vector3 newPosition = new Vector3(transform.localPosition.x, result, transform.localPosition.z);
        transform.localPosition = newPosition;
    }

    private void CheckOcclusion()
    {
        RaycastHit hit;
        Physics.Linecast(transform.position, PlayerController.Instance.transform.position, out hit);
        if (hit.collider.tag != "Player")
        {
            if (transform.localPosition.z < -4)
            {
                transform.localPosition += new Vector3(0, .1f, .1f);
            }
        }
        else
        {
            if(transform.localPosition.z > -8)
            {
                transform.localPosition -= new Vector3(0,.1f,.1f);
            }
        }
            
    }
}
