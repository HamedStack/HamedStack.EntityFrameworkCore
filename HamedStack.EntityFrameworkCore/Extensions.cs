using Microsoft.EntityFrameworkCore;

namespace HamedStack.EntityFrameworkCore;

/// <summary>
/// Provides a collection of extension methods to enhance the functionality of existing .NET types.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Determines whether the specified exception is a <see cref="DbUpdateConcurrencyException"/>.
    /// </summary>
    /// <param name="ex">The exception to check.</param>
    /// <returns><c>true</c> if the exception is a <see cref="DbUpdateConcurrencyException"/>; otherwise, <c>false</c>.</returns>
    public static bool IsDbUpdateConcurrencyException(this Exception ex)
    {
        return ex is DbUpdateConcurrencyException;
    }
}