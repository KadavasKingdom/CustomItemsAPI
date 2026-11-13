using System.Reflection;

/// <summary>
/// Provides a version from the assembly.
/// </summary>
internal static class Versioning
{
	/// <summary>
	/// Gets the version of this assembly.
	/// </summary>
	public static readonly Version Version = new(typeof(Versioning).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? "0.0.0");
}
