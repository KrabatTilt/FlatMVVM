using System.Runtime.CompilerServices;

namespace TT.FlatMVVM.Core;

/// <summary>
/// Converts untyped command parameters into typed values accepted by delegate commands.
/// </summary>
internal static class CommandParameterConverter
{
    /// <summary>
    /// Tries to convert an untyped command parameter to the requested parameter type.
    /// </summary>
    /// <typeparam name="T">The expected parameter type.</typeparam>
    /// <param name="parameter">The untyped command parameter.</param>
    /// <param name="value">The converted value when conversion succeeds; otherwise, the default value.</param>
    /// <returns><c>true</c> when the parameter can be used as <typeparamref name="T"/>; otherwise, <c>false</c>.</returns>
    public static bool TryConvert<T>(object? parameter, out T? value)
    {
        switch (parameter)
        {
            case null:
                value = default;
                return IsNullableParameter<T>();
            case T typedValue:
                value = typedValue;
                return true;
            default:
                value = default;
                return false;
        }
    }

    /// <summary>
    /// Converts an untyped command parameter or throws when the value is not valid for the requested type.
    /// </summary>
    /// <typeparam name="T">The expected parameter type.</typeparam>
    /// <param name="parameter">The untyped command parameter.</param>
    /// <param name="parameterName">The parameter name used when creating an exception.</param>
    /// <returns>The converted parameter value.</returns>
    /// <exception cref="ArgumentException">Thrown when the parameter cannot be used as <typeparamref name="T"/>.</exception>
    public static T? ConvertOrThrow<T>(object? parameter, string parameterName)
    {
        return TryConvert(parameter, out T? value) ? value : throw CreateArgumentException<T>(parameter, parameterName);
    }

    /// <summary>
    /// Tries to convert an untyped command parameter to a two-item tuple with independently validated items.
    /// </summary>
    /// <typeparam name="T1">The expected first item type.</typeparam>
    /// <typeparam name="T2">The expected second item type.</typeparam>
    /// <param name="parameter">The untyped command parameter.</param>
    /// <param name="first">The converted first item when conversion succeeds; otherwise, the default value.</param>
    /// <param name="second">The converted second item when conversion succeeds; otherwise, the default value.</param>
    /// <returns><c>true</c> when the parameter is a valid two-item tuple; otherwise, <c>false</c>.</returns>
    public static bool TryConvertTuple<T1, T2>(object? parameter, out T1? first, out T2? second)
    {
        first = default;
        second = default;

        if (parameter is not ITuple tuple || tuple.Length != 2)
            return false;

        if (!TryConvert(tuple[0], out first))
            return false;

        return TryConvert(tuple[1], out second);
    }

    /// <summary>
    /// Converts an untyped command parameter to a two-item tuple or throws when it is not valid.
    /// </summary>
    /// <typeparam name="T1">The expected first item type.</typeparam>
    /// <typeparam name="T2">The expected second item type.</typeparam>
    /// <param name="parameter">The untyped command parameter.</param>
    /// <param name="parameterName">The parameter name used when creating an exception.</param>
    /// <returns>The converted tuple items.</returns>
    /// <exception cref="ArgumentException">Thrown when the parameter is not a valid two-item tuple.</exception>
    public static (T1? First, T2? Second) ConvertTupleOrThrow<T1, T2>(object? parameter, string parameterName)
    {
        return TryConvertTuple(parameter, out T1? first, out T2? second) ? (first, second) : throw CreateTupleArgumentException<T1, T2>(parameter, parameterName);
    }

    /// <summary>
    /// Determines whether the requested parameter type can accept a null command parameter.
    /// </summary>
    /// <typeparam name="T">The expected parameter type.</typeparam>
    /// <returns><c>true</c> for reference types and nullable value types; otherwise, <c>false</c>.</returns>
    private static bool IsNullableParameter<T>()
    {
        Type parameterType = typeof(T);
        return !parameterType.IsValueType || Nullable.GetUnderlyingType(parameterType) != null;
    }

    /// <summary>
    /// Creates an exception describing a parameter that cannot be used as the requested type.
    /// </summary>
    /// <typeparam name="T">The expected parameter type.</typeparam>
    /// <param name="parameter">The invalid command parameter.</param>
    /// <param name="parameterName">The parameter name assigned to the exception.</param>
    /// <returns>An exception with the expected and actual parameter types.</returns>
    private static ArgumentException CreateArgumentException<T>(object? parameter, string parameterName)
    {
        string actualType = parameter?.GetType().FullName ?? "null";
        return new ArgumentException(
            $"Command parameter must be assignable to '{typeof(T).FullName}' and cannot be null for non-nullable value types. Actual parameter type was '{actualType}'.",
            parameterName);
    }

    /// <summary>
    /// Creates an exception describing a parameter that is not a valid two-item tuple.
    /// </summary>
    /// <typeparam name="T1">The expected first item type.</typeparam>
    /// <typeparam name="T2">The expected second item type.</typeparam>
    /// <param name="parameter">The invalid command parameter.</param>
    /// <param name="parameterName">The parameter name assigned to the exception.</param>
    /// <returns>An exception with the expected tuple item types and actual parameter type.</returns>
    private static ArgumentException CreateTupleArgumentException<T1, T2>(object? parameter, string parameterName)
    {
        string actualType = parameter?.GetType().FullName ?? "null";
        return new ArgumentException(
            $"Command parameter must be tuple with items assignable to '{typeof(T1).FullName}' and '{typeof(T2).FullName}'. Null is allowed only for reference types and nullable value types. Actual parameter type was '{actualType}'.",
            parameterName);
    }
}