using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player;
    public int Gold = 0;
    public bool CanScrollHotBar = true;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
            isPaused = !isPaused;
        }
    }

    public void UnlockCursor()
    {
        PlayerController.Instance.CanMove = false;
        CameraManager.Instance.CanMoveCamera = false;
        CanScrollHotBar = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        PlayerController.Instance.CanMove = true;
        CameraManager.Instance.CanMoveCamera = true;
        CanScrollHotBar = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChangeAmountOfGold(int newValue)
    {
        Gold += newValue;
    }
}
