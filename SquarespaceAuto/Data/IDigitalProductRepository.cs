using System;
using System.Collections.Generic;
using System.Text;

namespace SquarespaceAuto.Data
{
    public interface IDigitalProductRepository
    {
        List<DigitalProduct> Products { get; set; }
    }
}
