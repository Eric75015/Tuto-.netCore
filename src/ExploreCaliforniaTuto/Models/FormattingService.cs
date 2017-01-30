using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCaliforniaTuto.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime datetime)
        {
            return datetime.ToString("d");
        }
    }
}
