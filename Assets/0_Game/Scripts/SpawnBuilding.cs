using CityVilleClone;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnBuilding : MonoBehaviour
{


    public BuildingBlock building;
    public int maxX;
    public bool isNegativeX;

    static int lastIndex = 1;
    static int lastIndexN = 1;

    private Button button;
    // Use this for initialization
    void Awake()
    {
        button = GetComponent<Button>();

    }

    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            Vector3 pos;
            if (isNegativeX)
            {
                pos = new Vector3(lastIndexN % maxX,0, (int)lastIndexN / maxX);
                pos.x *= -1;
                pos.z *= -1;
                lastIndexN++;
            }
            else
            {
                pos = new Vector3(lastIndex % maxX,0, (int)lastIndex / maxX);
                lastIndex++;
            }


            var newBuilding = GameObject.Instantiate<GameObject>(building.gameObject);
            newBuilding.transform.position = pos;
        });
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = building.IsResourcesSufficient();
    }
}
