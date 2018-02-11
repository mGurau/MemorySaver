using System;
using System.Collections.Generic;
using System.Text;

namespace MemorySaver.Domain.ServiceContracts.DTOs.Response
{
    public class GenericResponseDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }
    }
}
