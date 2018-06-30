using UnityEngine;
using UnityEditor;
using CityVilleClone;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Resource", menuName = "GameElements/Resources", order = 0)]
public class Resource : ScriptableObject
{

    private List<StorageBlock> storages;

    [SerializeField]
    private int resourceAmount;
   
    public int ResourceAmount { get { return resourceAmount; } }

    [SerializeField]
    private int resourceCapacity;

    public int GetResourceCapacity()
    { return resourceAmount; }
    public List<StorageBlock> Storages
    {
        get
        {
            if (storages == null) storages = new List<StorageBlock>();
            return storages;
        }

    }

    internal void Add(StorageBlock storageBlock)
    {
        if (!Storages.Contains(storageBlock))
        {
            Storages.Add(storageBlock);
            resourceCapacity += storageBlock.storageCapacity;
           // resourceAmount += storageBlock.StoredAmount;
        }
    }
    internal void Remove(StorageBlock storageBlock)
    {
        if (Storages.Contains(storageBlock))
        {
            Storages.Remove(storageBlock);
            resourceCapacity -= storageBlock.storageCapacity;

            if (resourceAmount < resourceCapacity)
            {
                resourceAmount = resourceCapacity;
            }
        }
    }

    public bool Withdraw(int amount)
    {
        int collectedResources = 0;
        if (resourceAmount > amount)
        {
            for (int i = 0; i < storages.Count && amount > 0; i++)
            {
                storages[i].Withdraw(amount, out collectedResources);
                amount -= collectedResources;
                resourceAmount -= collectedResources;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Store(int amount, out int Remaining)
    {
        Remaining = amount;
        int stored = 0;
        for (int i = 0; i < storages.Count && amount > 0 && resourceAmount < resourceCapacity; i++)
        {
            storages[i].Store(Remaining, out stored);
            Remaining -= stored;
            resourceAmount += stored;
        }
        return Remaining == 0;
    }

    public bool IsResourceAvaliable(int amount)
    {
        return amount <= ResourceAmount;
    }
}