using Microsoft.AspNetCore.Http;

namespace Services;

public class Helpers
{
    public static async Task<byte[]?> GetFileBytes(IFormFile file, CancellationToken cancellationToken = default)
    {
        using var imageStream = new MemoryStream();
        await file.CopyToAsync(imageStream, cancellationToken);
        return imageStream.Length > 0 ? imageStream.ToArray() : null;
    }
}