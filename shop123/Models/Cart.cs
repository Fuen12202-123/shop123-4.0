using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class Cart: IEnumerable<ordersDetail>
    {
        public Cart()
        {
            this.cartItems = new List<ordersDetail>();
        }

        private List<ordersDetail> cartItems;
        public int count
        {
            get 
            { 
                return this.cartItems.Count;
            }
        }

        IEnumerator<ordersDetail> IEnumerable<ordersDetail>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}