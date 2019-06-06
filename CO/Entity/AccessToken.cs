using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CO.Entity
{
    class AccessToken
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { set; get; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { set; get; }
        [JsonProperty("session_key")]
        public string SessionKey { set; get; }
        [JsonProperty("access_token")]
        public string Token { set; get; }
        [JsonProperty("scope")]
        public string Scope { set; get; }
        [JsonProperty("session_secret")]
        public string SessionSecret { set; get; }
    }
}
