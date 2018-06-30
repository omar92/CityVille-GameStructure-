using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

namespace CityVilleClone
{
    public class FactoryBlock : BuildingBlock
    {
        [Header("Factory Attributes")]
        public SResourceAmount[] InResources = new SResourceAmount[0];
        public SResourceAmount[] OutResources = new SResourceAmount[0];
        public float ManufacturingTime;
        public UnityEvent OnManufacturingStartE;
        public UnityEvent OnManufacturingFailE;
        public UnityEvents.F OnManufacturingProgressE;
        public UnityEvent OnManufacturingDoneE;


        private Coroutine ManufactoringCORef;

        public override void OnConstructionDone()
        {
            StartManufactoring();

        }

        private void StartManufactoring()
        {
            ManufactoringCORef = StartCoroutine(ManufactoringCO(ManufacturingTime));
        }

        private IEnumerator ManufactoringCO(float ManufacturingTime)
        {
            float remainingTime;
            while (true)
            {
                remainingTime = ManufacturingTime;
                if (ResourcesSufficient())
                {
                    OnManufacturingStartE.Invoke();
                    for (int i = 0; i < InResources.Length; i++)
                    {
                        InResources[i].Withdraw();
                    }

                    while (remainingTime >= 0)
                    {
                        OnManufacturingProgress(remainingTime);
                        yield return new WaitForSeconds(1f);
                        remainingTime -= 1;
                    }

                    OnManufacturingDone();
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    OnManufacturingFailE.Invoke();
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        private bool ResourcesSufficient()
        {
            for (int i = 0; i < InResources.Length; i++)
            {
                if (!InResources[i].IsResourceAvaliable())
                {
                    Debug.LogError("Resource " + InResources[i].resource.name + " is not sufficient");
                    return false;
                }
            }
            return true;
        }

        private void OnManufacturingDone()
        {
            for (int i = 0; i < OutResources.Length; i++)
            {
                OutResources[i].Store();
            }
            OnManufacturingDoneE.Invoke();
        }

        private void OnManufacturingProgress(float manufacturingTime)
        {
            OnManufacturingProgressE.Invoke(manufacturingTime);
        }

        internal override void OnDisable()
        {
            base.OnDisable();
            StopCoroutine(ManufactoringCORef);
        }

    }
}