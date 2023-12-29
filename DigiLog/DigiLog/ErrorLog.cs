namespace DigiLog
{
    public class ErrorLog
    {
        public ErrorLog() { }

        public class AppException : Exception
        {
            public string ErrorCode { get; }

            public AppException(string errorCode, string message)
                : base(message)
            {
                ErrorCode = errorCode;
            }
        }

        public class VisitorNotFoundException : Exception
        {
            public VisitorNotFoundException(int visitorId)
                : base($"Visitor with ID {visitorId} not found.")
            {
            }
        }

        public class TagNotFoundException : Exception
        {
            public TagNotFoundException(string tagNumber)
                : base($"Tag with number {tagNumber} not found or already in use.")
            {
            }
        }



    }
}
