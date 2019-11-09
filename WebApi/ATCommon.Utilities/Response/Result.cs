using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ATCommon.Utilities.Response
{
    public class Result<T> : IResult<T>
    {
        public Result(int statusCode, T data, string errorMessage = null)
        {
            StatusCode = statusCode;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public Result()
        {

        }
        public string Version { get { return "1.0"; } }

        public int StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }
}
