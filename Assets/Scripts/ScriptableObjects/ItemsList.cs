using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "ItemsList", menuName = "Scriptable Objects/ItemsList")]
public class ItemsList : RuntimeSet<GameObject>
{

    private void OnEnable()
    {
        Items.Clear();
    }
}
