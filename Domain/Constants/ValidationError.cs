using FluentResults;

namespace vsa_journey.Domain.Constants;

public class ValidationError : Error
{
    public string FieldName { get; }

    public ValidationError(string fieldName, string message)
        : base(message)
    {
        FieldName = fieldName;
        Metadata.Add("FieldName", fieldName);
    }
}
