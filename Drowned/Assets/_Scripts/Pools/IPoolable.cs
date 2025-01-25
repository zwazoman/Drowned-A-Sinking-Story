public interface IPoolable
{
    /// <summary>
    /// Destroy Equivalent
    /// </summary>
    public void ReturnToPool();

    /// <summary>
    /// Start Equivalent
    /// </summary>
    public void OnPulledFromPool();
}