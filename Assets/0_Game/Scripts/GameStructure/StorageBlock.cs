using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CityVilleClone
{
    public class StorageBlock : BuildingBlock
    {
        [Header("Storage Attributes")]
        public Resource StorageType;
        public int storageCapacity;
        [SerializeField]
        private int m_storedAmount;
        public int StoredAmount
        {
            get { return m_storedAmount; }
        }
  
        public int EmptySpace { get { return storageCapacity - m_storedAmount; } }

        public bool IsFull { get { return storageCapacity == StoredAmount; } }
        public bool IsEmpty { get { return StoredAmount == 0; } }



        internal override void OnDisable()
        {
            StorageType.UnRegister(this);
        }

        public override void OnConstructionDone()
        {
            StorageType.Register(this);
        }


        /// <summary>
        /// add resources to this storage
        /// the function return false if not all requested resources stored
        /// </summary>
        /// <param name="amount"> resources amount must be >0</param>
        /// <param name="StoredAmount">return the amount of resources successfully stored</param>
        /// <returns></returns>
        internal bool Store(int requestedAmount, out int StoredAmount)
        {
            if (requestedAmount <= 0 || IsFull) { StoredAmount = 0; return true; }
            if (requestedAmount > EmptySpace)
            {
                StoredAmount = EmptySpace;
                m_storedAmount = storageCapacity;
                return false;
            }

            m_storedAmount += requestedAmount;
            StoredAmount = requestedAmount;
            return true;
        }

        /// <summary>
        /// widraw resources from this storage
        /// the function return false if withdrawn resources less than requested 
        /// </summary>
        /// <param name="amount"> resources amount must be >0</param>
        /// <param name="WithdrawAmount">resources successfully withdrawn</param>
        /// <returns></returns>
        internal bool Withdraw(int requestedAmount, out int WithdrawAmount)
        {
            if (requestedAmount <= 0 || IsEmpty) { WithdrawAmount = 0; return true; }
            if (requestedAmount > m_storedAmount)
            {
                WithdrawAmount = m_storedAmount;
                m_storedAmount = 0;
                return false;
            }

            m_storedAmount -= requestedAmount;
            WithdrawAmount = requestedAmount;
            return true;
        }

    }
}