public delegate void Response();

public interface IResponder {
    /// <summary>
    /// A method that, once called, should return whether it will eventually call the given method parameter.
    /// </summary>
    /// <param name="responseCall">The parameter to be eventually called.</param>
    /// <returns>Whether <paramref name="responseCall"/> will eventually be called.</returns>
    bool Request(Response responseCall);
}