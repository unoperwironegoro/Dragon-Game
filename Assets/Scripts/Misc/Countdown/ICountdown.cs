public interface ICountdown {
    /// <summary>
    /// Called from Countdown with decrementing counts, with the final call being Countdown(0, beginning).
    /// </summary>
    /// <param name="count"> The current (descending) count value. </param>
    /// <param name="beginning"> The initial count value. </param>
    void Countdown(int count, int beginning);
}