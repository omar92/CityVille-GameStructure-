
namespace CityVilleClone
{
    [System.Serializable]
    public struct SResourceAmount
    {
        public Resource resource;
        public int amount;

        public bool IsResourceAvaliable()
        {
            return resource.IsResourceAvaliable(amount);
        }
        public bool Store(int amount, out int remaining)
        {
            return resource.Store(amount, out remaining);
        }
        public bool Store(out int remaining)
        {
            return resource.Store(amount, out remaining);
        }
        public bool Store()
        {
            int temp;
            return resource.Store(amount, out temp);
        }
        public bool Withdraw()
        {
            return resource.Withdraw(amount);
        }
    }
}