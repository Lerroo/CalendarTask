using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarProj.DAO.Models.Enums
{
    public enum TypeMetting : int
    {
        [StringValue("Long")]
        Long,
        [StringValue("Short")]
        Short,
    }
}

