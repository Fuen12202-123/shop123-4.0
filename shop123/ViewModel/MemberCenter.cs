using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class MemberCenter
    {
        public int id { get; set; }
        public string memberName { get; set; }
        public string memberPassword { get; set; }
        public string memberEmail { get; set; }
        public string memberPhone { get; set; }
        public string memberImg { get; set; }
        public IEnumerable<orderSeller> orderSeller { get; set; }
    }

    public class orderSeller
    {
        public int id { get; set; }
        public string receiverName { get; set; }
        public string receiverPhone { get; set; }
        public DateTime orderCreate { get; set; }
        public string orderState { get; set; }
        public string memberId { get; set; }
        public int totalPrice { get; set; }
    }
}