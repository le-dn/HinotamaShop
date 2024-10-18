namespace API.Extensions;

public static class GenericMappingExtensions
{
    // Generic extension method for mapping from the current object (TIn) to TOut
    public static TOut MapTo<TOut>(this object input)
    {
        var output = Activator.CreateInstance<TOut>(); // Create an instance using reflection
        var inputProperties = input.GetType().GetProperties();
        var outputProperties = typeof(TOut).GetProperties();

        foreach (var inputProperty in inputProperties)
        {
            var outputProperty = outputProperties.FirstOrDefault(p =>
                p.Name == inputProperty.Name && p.PropertyType == inputProperty.PropertyType
            );
            if (outputProperty != null && outputProperty.CanWrite)
            {
                outputProperty.SetValue(output, inputProperty.GetValue(input));
            }
        }

        return output;
    }
}
