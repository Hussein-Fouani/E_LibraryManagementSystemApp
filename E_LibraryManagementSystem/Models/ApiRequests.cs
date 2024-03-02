using E_Library_Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_Library_Utilities.ApiTypes;

namespace E_LibraryManagementSystem.Models
{
    public class ApiRequests
    {
        public ApiType ApiTypes { get; set; } = ApiType.GET;
        public string URI { get; set; }
        public object Data { get; set; }
    }
}
