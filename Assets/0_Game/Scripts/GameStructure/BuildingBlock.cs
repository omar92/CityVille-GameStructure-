using System;
using UnityEngine;
using UnityEngine.Events;

namespace CityVilleClone
{
    public class BuildingBlock : Block
    {
        [Header("Building Attributes")]
        public float ConstructionTime;
        public SResourceAmount[] ConstructionCost = new SResourceAmount[0];
        public UnityEvent OnConstructionStartE;
        public UnityEvent OnConstructionFailE;
        public UnityEvents.F OnConstructioProgressE;
        public UnityEvent OnConstructionDoneE;


        private Coroutine ConstructionCoRef;

        internal override void OnEnable()
        {


            if (IsResourcesSufficient())
            {
                OnConstructionStartE.Invoke();
                for (int i = 0; i < ConstructionCost.Length; i++)
                {
                    ConstructionCost[i].Withdraw();
                }

                ConstructionCoRef = StartCoroutine(ConstructionCo(ConstructionTime));
            }
            else
            {
                OnConstructionFailE.Invoke();
                Destroy(gameObject);
            }
        }

        public bool IsResourcesSufficient()
        {
            for (int i = 0; i < ConstructionCost.Length; i++)
            {
                if (!ConstructionCost[i].IsResourceAvaliable())
                {
                  //  Debug.Log("Resource " + ConstructionCost[i].resource.name + " is not sufficient");
                    return false;
                }
            }
            return true;
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
            OnConstructioProgressE.Invoke(remainingTIme);
        }
        public virtual void OnConstructionDone()
        {
            OnConstructionDoneE.Invoke();
        }

        internal override void OnDisable()
        {
            StopCoroutine(ConstructionCoRef);
        }
    }
}