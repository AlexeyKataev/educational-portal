using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Models.Account
{
    public class UserTicket
    {
        public int Id { get; set; } = 0;
        
        public string OS { get; set; } = "";

		public string Location { get; set; } = "";

        public string Token { get; set; } = "0";

		public DateTime ActivationDate { get; set; } = DateTime.UtcNow;

		public DateTime LastActive { get; set; } = new DateTime(0001, 01, 01, 01, 01, 01);

        public bool IsValid { get; set; } = false;

		public int UserId { get; set; } = 0;
    }
}
