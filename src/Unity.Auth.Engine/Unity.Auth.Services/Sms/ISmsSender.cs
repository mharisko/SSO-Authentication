using System.Threading.Tasks;

namespace Unity.Auth.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}