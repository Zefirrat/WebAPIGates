using WebApi.Gates.Interfaces.Default;

namespace WebApi.Gates.Interfaces
{
    public interface IGateAuth<TPostParameters, TAuthParameters> : IGate<TPostParameters>
        where TPostParameters : PostParameters
    {
        string Token { get; }
        void Authenticate(TAuthParameters authParams);
    }
}