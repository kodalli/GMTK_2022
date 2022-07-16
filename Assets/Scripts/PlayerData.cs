using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class PlayerData : MonoBehaviour
{
    public int DiceCount { get; set; }
    public int NumberOfDiceRoller { get; set; }
    public List<IWeapon> weapons = new List<IWeapon>();
    public IWeapon activeWeapon;
}
