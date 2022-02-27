using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class ordersFactory
    {
        shop123Entities2 db = new shop123Entities2();
        List<OrderViewModel> vm = new List<OrderViewModel>();

       

        public List<OrderViewModel> orderall(string state, string memberId)
        {

            var GroupBy = db.ordersDetail
              .GroupBy(m => m.orderguid)
              .Select(c => new
              {
                  orderguid = c.Key,
                  count = c.Count()
              });
            var OD = db.orders.Join(GroupBy,
               o => o.orderguid,
               d => d.orderguid,
               (o, d) => new
               {
                   orderguid = d.orderguid,
                   memberId = o.memberId,
                   CreateTime = o.orderCreateTime,
                   count = d.count,
                   state = o.orderState,
                   sellerId = o.sellerId,
                   id = o.id,
                   receiverName = o.receiverName,
                   receiverAddress = o.receiverAddress,
                   receiverPhone = o.receiverPhone,
                   ordermessage = o.ordermessage

               })
               .OrderByDescending(od => od.CreateTime).Where(cs => cs.memberId == memberId);
            foreach (var item in OD)
            {
                vm.Add(new OrderViewModel()
                {
                    orderguid = item.orderguid,
                    orderCreateTime = item.CreateTime,
                    memberId = item.memberId,
                    orderState = item.state,
                    sellerId = item.sellerId,
                    id = item.id,
                    receiverName = item.receiverName,
                    receiverAddress = item.receiverAddress,
                    receiverPhone = item.receiverPhone,
                    ordermessage = item.ordermessage,

                    Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
                    {
                        id = o.id,
                        skuId = o.skuId,
                        orderDetailcolor = o.orderDetailcolor,
                        orderDetailsize = o.orderDetailsize,
                        orderDetailnum = o.orderDetailnum,
                        orderDetailspuname = o.orderDetailspuname,
                        orderDetailprice = o.orderDetailprice,
                        spuImg1 = o.spuImg1,
                        spuId = o.spuId
                    })
                });
            }
            return vm;
        }
        public List<OrderViewModel> orderstate(string state, string memberId)
        {
            var GroupBy = db.ordersDetail
                 .GroupBy(m => m.orderguid)
                 .Select(c => new
                 {
                     orderguid = c.Key,
                     count = c.Count()
                 });
            var OD = db.orders.Join(GroupBy,
             o => o.orderguid,
             d => d.orderguid,
             (o, d) => new
             {
                 orderguid = d.orderguid,
                 memberId = o.memberId,
                 CreateTime = o.orderCreateTime,
                 count = d.count,
                 state = o.orderState,
                 sellerId = o.sellerId,
                 id = o.id,
                 receiverName = o.receiverName,
                 receiverAddress = o.receiverAddress,
                 receiverPhone = o.receiverPhone,
                 ordermessage = o.ordermessage,
             })
             .OrderByDescending(od => od.CreateTime).Where(cs => cs.memberId == memberId && cs.state == state);
            foreach (var item in OD)
            {
                vm.Add(new OrderViewModel()
                {
                    orderguid = item.orderguid,
                    orderCreateTime = item.CreateTime,
                    memberId = item.memberId,
                    orderState = item.state,
                    sellerId = item.sellerId,
                    id = item.id,
                    receiverName = item.receiverName,
                    receiverAddress = item.receiverAddress,
                    receiverPhone = item.receiverPhone,
                    ordermessage = item.ordermessage,

                    Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
                    {
                        id = o.id,
                        skuId = o.skuId,
                        orderDetailcolor = o.orderDetailcolor,
                        orderDetailsize = o.orderDetailsize,
                        orderDetailnum = o.orderDetailnum,
                        orderDetailspuname = o.orderDetailspuname,
                        orderDetailprice = o.orderDetailprice,
                        spuImg1 = o.spuImg1,
                        spuId = o.spuId
                    })
                });
            }
            return vm;
        }


    }
}