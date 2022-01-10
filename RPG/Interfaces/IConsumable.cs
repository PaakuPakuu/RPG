namespace RPG
{
    public interface IConsumable
    {
        int Count { get; set; }

        void Add(int count)
        {
            Count += count;
        }

        bool Use()
        {
            if (Count == 0)
            {
                return false;
            }

            Count--;
            return true;
        }
    }
}
