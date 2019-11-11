using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameItem", menuName = "Runatheryl/GameItem", order = 1)]
public class GameItem : ScriptableObject
{
    public string GI_name;
    public Sprite GI_sprite;
    public bool isUsable;
    public bool isStackable;
    public GameObject ItemGO;
    public int Quantity;
    public UnityEvent OnUse;
}
