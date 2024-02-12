using UserManagement.SharedKernel.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.SharedKernel.Returns
{
    public class ApiResult<T>
    {
        public ApiResult() { }

        public ApiResult(bool success, T data, IEnumerable<Message> messages)
        {
            Success = success;
            Data = data;
            Messages = messages;
        }

        public ApiResult(T dados, IEnumerable<Message> messages)
        {
            Success = true;
            Data = dados;
            Messages = messages;
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
