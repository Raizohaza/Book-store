using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1
{
    using System;
    using System.Collections.Generic;

    public partial class Product_Images
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public virtual Product Product { get; set; }
    }
}
