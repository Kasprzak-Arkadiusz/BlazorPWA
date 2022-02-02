namespace Application.Common.Responses
{
    public class CreateResponse
    {
        internal CreateResponse(int id, string message)
        {
            Id = id;
            ErrorMessage = message;
        }

        public int Id { get; set; }
        public string ErrorMessage { get; set; }

        public static CreateResponse Success(int id)
        {
            return new CreateResponse(id, string.Empty);
        }

        public static CreateResponse Error(string message)
        {
            return new CreateResponse(0, message);
        }

        // For deserialization only
        public CreateResponse()
        { }
    }
}