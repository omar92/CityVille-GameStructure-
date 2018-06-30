using UnityEngine;
using System.Collections;


namespace CityVilleClone {
    public class InitialiseResources : MonoBehaviour
    {
        public Resource[] initResoources;

        private void Awake()
        {
            for (int i = 0; i < initResoources.Length; i++)
            {
                initResoources[i].Init();
            }
        }
    }
}