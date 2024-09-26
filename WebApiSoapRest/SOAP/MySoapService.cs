namespace WebApplication3
{
    public class MySoapService : IMySoapService
    {
        public string GetMessage(string name)
        {
            return $"Hello, {name}!";
        }
    }
}