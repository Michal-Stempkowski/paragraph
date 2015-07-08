namespace DataLayer.Schema.Validation
{
    public interface IValidationAttribute
    {
        bool IsValid(object validatedProperty);
    }
}
