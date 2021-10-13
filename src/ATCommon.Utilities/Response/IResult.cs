using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ATCommon.Utilities.Response
{
    public interface IResult<T>
    {
        [DataMember]
        public string Version { get; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Data { get; set; }
    }
}
