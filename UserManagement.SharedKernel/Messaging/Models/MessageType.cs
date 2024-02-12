using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.SharedKernel.Messaging
{
    public enum MessageType
    {
        Informational,
        Alert,
        ValidationFailure,
        Error
    }
}
