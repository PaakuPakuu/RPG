namespace RPG
{
    public interface ITriggerable : ILocatable2D
    {
        bool Activable { get; }

        void OnTrigger();
    }
}