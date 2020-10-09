using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Model;

namespace POS.Middleware
{
    class ProductRequest
    {
        public static bool Validate(Product product)
        {
            if (product.productName.Length <= 0) return false;
            if (product.productMinimunQuantity <= 0) return false;
            if (product.productPrice <= 0) return false;
            if (product.productQuantity <= 0) return false;
            if (product.productUnit.Length <= 0) return false;
            if (product.productUnitValue <= 0) return false;

            return true;
        }
    }
}
