using System;
using UnityEngine;

namespace CityVilleClone
{
    public class BuildingBlock : Block
    {

        public float ConstructionTime;
        public SResourceAmount[] ConstructionCost = new SResourceAmount[0];

        private Coroutine ConstructionCoRef;

        internal override void OnEnable()
        {
            bool isResourcesSufficient = true;
            for (int i = 0; i < ConstructionCost.Length; i++)
            {
                if (!ConstructionCost[i].IsResourceAvaliable())
                {
                    isResourcesSufficient = false;
                    Debug.LogError("Resource " + ConstructionCost[i].resource.name + " is not sufficient");
                    break;
                }
            }

            if (isResourcesSufficient)
            {
                for (int i = 0; i < ConstructionCost.Length; i++)
                {
                    ConstructionCost[i].Withdraw();
                }

                ConstructionCoRef = StartCoroutine(ConstructionCo(ConstructionTime));
            }
            else
            {
                Destroy(this);
            }
        }

        private System.Collections.IEnumerator ConstructionCo(float constructionTime)
        {
            while (constructionTime >= 0)
            {
                OnConstructionProgress(constructionTime);
                yield return new WaitForSeconds(1f);
                constructionTime -= 1;
            }
            OnConstructionDone();
        }

        public virtual void OnConstructionProgress(float remainingTIme)
        {

        }
        public virtual void OnConstructionDone()
        {

        }

        internal override void OnDisable()
        {
            StopCoroutine(ConstructionCoRef);
        }
    }
}