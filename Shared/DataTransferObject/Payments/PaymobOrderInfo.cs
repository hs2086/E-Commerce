using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Payments
{
    public class PaymobOrderInfo
    {
        public string id { get; set; }  // Paymob order id
        public string merchant_order_id { get; set; } // your system order id
    }
}
