using System.Runtime.Serialization;

namespace Xarial.Signal2Go.Services.Auth.Oidc.Data
{
    [DataContract]
    public class AuthData
    {
        [DataMember]
        public bool StayLoggedIn { get; set; }
    }
}
