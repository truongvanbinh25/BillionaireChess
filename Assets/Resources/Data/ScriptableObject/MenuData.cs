using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuData", menuName = "Data")]
public class MenuData:ScriptableObject
{
    public List<PlayerSettingCanvas> listDataPlayer = new List<PlayerSettingCanvas>();
}
