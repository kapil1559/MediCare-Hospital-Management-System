using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.Domain.Responses
{
    public class ModuleResponse
    {
        public int ModuleID { get; set; }

        public string ModuleCode { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Route { get; set; }

        public bool IsActive { get; set; }
    }
}
