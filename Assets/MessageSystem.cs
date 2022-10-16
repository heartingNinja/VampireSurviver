using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject damageMessage;

    List<TextMeshPro> messagePool;

    int objectCount = 10;
    int count;

    private void Start()
    {
        messagePool = new List<TextMeshPro>();
        for(int i = 0; i < objectCount; i++)
        {
            Populate();
        }
    }
    public void Populate()
    {
        GameObject go = Instantiate(damageMessage, transform);
        messagePool.Add(go.GetComponent<TextMeshPro>());
        go.SetActive(false);
    }

    
    public void PostMessage(string text, Vector3 worldPosition)
    {
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = worldPosition;
        messagePool[count].text = text;
        count += 1;

        if(count >= objectCount)
        {
            count = 0;
        }
       // go.transform.position = worldPosition;
       // go.GetComponent<TextMeshPro>().text = text;
    }
}
