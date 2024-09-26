using CoreWCF;

namespace WebApplication3
{
    [ServiceContract]
    public interface IMySoapService
    {
        [OperationContract]
        string GetMessage(string name);
    }
}
