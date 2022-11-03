namespace Services;

public interface IRequestValidator<T>
{
    public Task<Dictionary<string, IEnumerable<string>>> Validate(T request);
}