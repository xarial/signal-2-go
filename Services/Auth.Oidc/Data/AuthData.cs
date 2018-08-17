using System.Runtime.Serialization;

namespace Xarial.AppLaunchKit.Services.Auth.Oidc.Data
{
    [DataContract]
    public class AuthData
    {
        [DataMember]
        public bool StayLoggedIn { get; set; }
    }
}
