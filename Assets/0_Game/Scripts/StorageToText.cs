using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityVilleClone;
using UnityEngine.UI;

public class StorageToText : MonoBehaviour
{
    /// <summary>
    /// dirty code just to show values
    /// </summary>

    StorageBlock storage;
    Text text;
    // Use this for initialization
    void Awake()
    {
        storage = GetComponent<StorageBlock>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = storage.StoredAmount+"";
    }
}
