namespace API.Domain
{
    public enum STATUS { success, failure }

    public class SystemMessageBase
    {
        public static string[] StatusText = ["Sucesso", "Fracasso"];
        public string? Status { get; protected set; }
        public SystemError? Error { get; protected set; }

        public SystemMessageBase() { }

        public SystemMessageBase(SystemError error)
        {
            this.Status = StatusText[(int) STATUS.failure];
            this.Error = error;
        }
    }

    public class SystemMessage<T> : SystemMessageBase
    {
        public T? Response { get; private set; }

        public SystemMessage(T response)
        {
            this.Status = StatusText[(int) STATUS.success];
            this.Response = response;
        }
    }

    public class SystemError
    {
        public string Message { get; set; }

        public SystemError(string message)
        {
            this.Message = message;
        }
    }
}