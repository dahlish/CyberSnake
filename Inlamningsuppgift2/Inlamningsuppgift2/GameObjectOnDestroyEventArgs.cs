namespace Inlamningsuppgift2
{
    /// <summary>
    /// Arguments for a GameObjects OnDestroy eventhandler.
    /// </summary>
    public class GameObjectOnDestroyEventArgs
    {
        public GameObject destroyedObject;
        public GameObject destroyedByObject;
        public float timeElapsed;
    }
}