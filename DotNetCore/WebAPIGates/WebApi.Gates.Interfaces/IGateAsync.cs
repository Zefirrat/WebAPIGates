using System.Threading.Tasks;
using WebApi.Gates.Interfaces.Default;

namespace WebApi.Gates.Interfaces
{
    public interface IGateAsync<TPostParameters> where TPostParameters : PostParameters
    {
        Task<string> GetAsync(string uri);
        Task<string> PostAsync(TPostParameters postParameters);
    }
}