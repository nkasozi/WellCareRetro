using System;

namespace WellCare.Models
{
    public class Status
    {
        public const string FAILURE_STATUS_CODE = "100";
        public const string SUCCESS_STATUS_CODE = "0";

        public static Status SUCCESS = new Status
        {
            StatusCode = SUCCESS_STATUS_CODE,
            StatusDesc = "SUCCESS"
        };

        public static Status FAILURE = new Status
        {
            StatusCode = FAILURE_STATUS_CODE,
            StatusDesc = "OPERATION FAILED"
        };


        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
    }
}
