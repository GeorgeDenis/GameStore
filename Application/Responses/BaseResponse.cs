﻿namespace Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse() => Success = true;

        public BaseResponse(string message, bool success)
        {
            this.Success = success;
            this.Message = message;         
        }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public List<string>? ValidationsErrors { get; set; }
    }
}
