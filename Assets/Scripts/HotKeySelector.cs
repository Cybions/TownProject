using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeySelector : MonoBehaviour
{
    [SerializeField]
    private List<HotBarKey> hotBarKeys;
    [SerializeField]
    private Transform hotSelector;

    public GameItem SelectedGI = null;
    private int CurrentPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        hotSelector.transform.position = hotBarKeys[0].transform.position;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && GameManager.Instance.CanScrollHotBar) // forward
        {
            ChangeSelection(GetNextValue(CurrentPosition+1));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && GameManager.Instance.CanScrollHotBar) // backwards
        {
            ChangeSelection(GetNextValue(CurrentPosition-1));
        }
        CheckNumInput();
    }

    private void CheckNumInput()
    {
        if (!GameManager.Instance.CanScrollHotBar) { return; }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSelection(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSelection(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSelection(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSelection(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSelection(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSelection(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSelection(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeSelection(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeSelection(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeSelection(9);
        }
    }

    private void ChangeSelection(int Destination)
    {
        CurrentPosition = Destination;
        SelectedGI = hotBarKeys[Destination].gameItem;
        hotSelector.transform.position = hotBarKeys[Destination].transform.position;
    }

    private int GetNextValue(int inputInt)
    {
        if (inputInt > 9)
        {
            inputInt = 0;
        }
        if (inputInt < 0)
        {
            inputInt = 9;
        }
        
        return inputInt;
    }
}
