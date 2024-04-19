using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfo.Data.DataModels
{
    public record KeyValueType(string Key, object Value, string Type = null);
}
