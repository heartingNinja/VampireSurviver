using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject dropItemPickUp;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    bool isQutting = false;
    private void OnApplicationQuit()
    {
        isQutting = true;
    }

    public void CheckDrop()
    {
        if(isQutting)
        {
            return;
        }

        if(Random.value < chance)
        {
            Transform t = Instantiate(dropItemPickUp).transform;
            t.position = transform.position;
        }
        
    }
}
