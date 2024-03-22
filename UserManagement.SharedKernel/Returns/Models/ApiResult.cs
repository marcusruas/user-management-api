using UserManagement.SharedKernel.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.SharedKernel.Returns
{
    public class ApiResult<T>
    {
        public ApiResult() { }

        public ApiResult(T data, IEnumerable<Message> messages)
        {
            Data = data;
            Messages = messages;
        }

        public T Data { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
