namespace Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> ErrorMessages { get; set; }
        public string FriendlyErrorMassage { get; set; }

        public CustomValidationException(List<string> errorMessages, string friendlyErrorMassage)
            : base(friendlyErrorMassage)
        {
            ErrorMessages = errorMessages;
            FriendlyErrorMassage = friendlyErrorMassage;
        }
    }
}
