using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Checkout
{
    public class AddCheckoutDto
    {
        public string PaymentMethod { get; set; }   
        public string DeliveryAddress { get; set; }
    } 
}
