using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Models
{
    public class UsersTags
    {
        public int Tag_ID { get; set; }

        public Tag Tag { get; set; }

        public Guid Question_ID { get; set; }

        public Question Question { get; set; }
    }
}
