using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArendaClassLibrary
{
    internal class ProductClass
    {
        //ЗАНЕСТИ В ТАБЛИЦУ ПРОДУКТА КОЭФФ ИЗНОСА
        public decimal PriceFromWear(int qtyDays, decimal price, float coeff)
        {
            var res = qtyDays * price - (Convert.ToDecimal(coeff) * qtyDays);
            return res;
        }

    }
}
