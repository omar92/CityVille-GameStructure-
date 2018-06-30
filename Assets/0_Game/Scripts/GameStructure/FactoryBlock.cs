using UnityEngine;
using System.Collections;
using System;

namespace CityVilleClone
{
    public class FactoryBlock : BuildingBlock
    {

        public SResourceAmount[] InResources = new SResourceAmount[0];
        public SResourceAmount[] OutResources = new SResourceAmount[0];
        public float ManufacturingTime;

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
            while (true)
            {
                if (ResourcesSufficient())
                {
                    for (int i = 0; i < InResources.Length; i++)
                    {
                        InResources[i].Withdraw();
                    }

                    while (ManufacturingTime >= 0)
                    {
                        OnManufacturingProgress(ManufacturingTime);
                        yield return new WaitForSeconds(1f);
                        ManufacturingTime -= 1;
                    }

                    OnManufacturingDone();
                }
                else
                {
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
        }

        private void OnManufacturingProgress(float manufacturingTime)
        {

        }

        internal override void OnDisable()
        {
            base.OnDisable();
            StopCoroutine(ManufactoringCORef);
        }

    }
}