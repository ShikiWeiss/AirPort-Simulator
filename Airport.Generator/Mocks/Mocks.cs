using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirPort.Server.BL
{
    public class Mocks
    {
        public List<string> Companies { get; set; }

        public Mocks()
        {
            SetCompanies();
        }

        private void SetCompanies()
        {
            Companies = new List<string>
            {
                "El Al",
                "Delta",
                "United",
                "American",
                "Lufthansa",
                "KLM",
                "Ryanair",
                "Emirates",
                "British Airways"
            };
        }
    }
}
