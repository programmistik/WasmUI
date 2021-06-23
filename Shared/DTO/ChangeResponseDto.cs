using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmUI.Shared.DTO
{
    public class ChangeResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
