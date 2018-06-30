using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityVilleClone
{
    /// <summary>
    /// The Base Class
    /// </summary>
    public abstract class Block : MonoBehaviour
    {
        [Header("Block Attributes")]
        public Vector2 size;
        [SerializeField]
        public Vector2 Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        internal abstract void OnEnable();
        internal abstract void OnDisable();

    }
}