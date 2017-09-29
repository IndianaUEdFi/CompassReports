using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompassReports.Resources.Models
{
    public class FilterModel<T>
    {
        public string Display { get; set; }

        public T Value { get; set; }
    }
}
