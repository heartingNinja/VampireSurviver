using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    bool isQutting = false;

    [SerializeField] bool isNuke = false;
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

        if((Random.value < chance) && !isNuke) // is nuke my add
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
            Transform t = Instantiate(toDrop).transform;
            t.position = transform.position;
        }

        if (isNuke) // all my add
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
            Transform t = Instantiate(toDrop).transform;
            t.position = transform.position + new Vector3(0,4f,0);
        }

    }
}
