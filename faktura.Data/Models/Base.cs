using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktura.Data.Models
{
    public class Base<TKey>
    {
        public TKey Id { get; set; }
    }
}
