using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatCodeManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField cheatCodeField;

    private bool isCMDOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        cheatCodeField.gameObject.SetActive(isCMDOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Quote)) { ChangeFieldState(true); }
        if (isCMDOpen && (Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.KeypadEnter))) { ExecuteCMD(cheatCodeField.text.ToCharArray()); }
    }

    private void ChangeFieldState(bool newState)
    {
        if(newState == isCMDOpen) { newState = !newState; }
        cheatCodeField.gameObject.SetActive(newState);
        isCMDOpen = newState;
        print(isCMDOpen);
        if (isCMDOpen)
        {
            cheatCodeField.ActivateInputField();
            GameManager.Instance.UnlockCursor();
        }
        else
        {
            GameManager.Instance.LockCursor();
        }
    }

    private void ExecuteCMD(char[] NewCommand)
    {
        if(NewCommand[0] != '/') { WriteError(); return; }
        string sentence = "";
        bool isSlash = true;
        foreach(char letter in NewCommand)
        {
            if (isSlash) { isSlash = false; } else
            {
                sentence += letter;
            }
        }
        print(3);
        string[] ListOfWords = sentence.Split(' ');
        //print(ListOfWords.Length);
        switch (ListOfWords[0].ToLower())
        {
            case "teleport":
                if (ListOfWords.Length < 1) { WriteError(); }
                GameObject[] TeleportPoints = GameObject.FindGameObjectsWithTag("TeleportPoint");
                bool isDestinationFound = false;
                foreach(GameObject TeleportPoint in TeleportPoints)
                {
                    if(TeleportPoint.name.ToLower() == ListOfWords[1].ToLower())
                    {
                        isDestinationFound = true;
                        WriteResult("Player teleported to " + TeleportPoint.name);
                        GameManager.Instance.player.transform.position = TeleportPoint.transform.position;
                    }
                }
                if (!isDestinationFound) { WriteError(); }
                break;
            case "givegold":
                if (ListOfWords.Length < 1) { WriteError(); }
                int value;
                if(int.TryParse(ListOfWords[1], out value))
                {
                    GameManager.Instance.ChangeAmountOfGold(value);
                }
                else
                {
                    WriteError();
                }
                
                break;
            default:
                WriteError();
                break;
        }
        //cheatCodeField.gameObject.SetActive(false);
    }

    private void WriteError()
    {
        cheatCodeField.text = "Invalid Command";
        print("error");
    }
    private void WriteResult(string textToDisplay = "Command accepted")
    {
        cheatCodeField.text = textToDisplay;
        print(textToDisplay);
    }
}
