using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Payments
{
    public class PaymobCallbackDto
    {
        public bool success { get; set; }
        public int amount_cents { get; set; }
        public PaymobOrderInfo order { get; set; }
        public PaymobDataInfo data { get; set; }
        public string hmac { get; set; }
    }
}
