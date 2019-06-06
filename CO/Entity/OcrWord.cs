using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CO.Entity
{
    class OcrWord
    {
       [JsonProperty("words")]
        public string Words { set; get; }
    }
}
