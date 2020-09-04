using WebApi.Gates.Interfaces.Default;

namespace WebApi.Gates.Interfaces
{
    public interface IGate<TPostParameters> where TPostParameters : PostParameters
    {
        string MainUri { get; }
        string Get(string uri);
        string Post(TPostParameters postParameters);
    }
}