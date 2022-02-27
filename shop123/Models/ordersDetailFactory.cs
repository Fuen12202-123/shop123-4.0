using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class ordersDetailFactory
    {
        shop123Entities2 db = new shop123Entities2();

        public List<ordersDetail> queryAll()
        {
            var ordersDetail = db.ordersDetail.ToList();
            return ordersDetail;
        }
         public List<ordersDetail> queryBymemberId(string memberId)
        {
            var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();
            return ordersDetail;
        }      

        public List<ordersDetail> queryBycartitem(int skuid,string memberId)
        {
            var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否" && m.skuId == skuid).ToList();

            return (ordersDetail);
        }
       
            public void updatesku(int odid, string color, string size)
        {
            var od = db.ordersDetail.Where(o => o.id == odid).ToList();
            var sku = db.sku.Where(s => s.skuColor == color && s.skuSize == size).Select(s => s.id).FirstOrDefault(); ;
            foreach (var item in od)
            {
                item.skuId = sku;
                item.orderDetailcolor = color;
                item.orderDetailsize = size;
            }
            db.SaveChanges();
        }
       
        public void DeleteCar(int Id)
        {
            // 依Id找出要刪除購物車狀態的產品
            var orderDetail = db.ordersDetail.Where
                (m => m.id == Id).FirstOrDefault();
            //刪除購物車狀態的產品
            if (orderDetail != null)
            {
                db.ordersDetail.Remove(orderDetail);
            }
            db.SaveChanges();
        }

        public void skuchecked(int skuid, string memberId)
        {
            var ordersDetail = queryBycartitem(skuid, memberId);
            foreach (var item in ordersDetail)
            {
                if (item.@checked == true)
                {
                    item.@checked = false;
                }
                else
                {
                    item.@checked = true;
                }
            }
            db.SaveChanges();
        }
          public void minus(int skuid, string memberId)
        {

            var ordersDetail = queryBycartitem(skuid, memberId);

            foreach (var item in ordersDetail)
            {
                item.orderDetailnum -= 1;
            }
            db.SaveChanges();
        }
          public void plus(int skuid, string memberId)
        {

            var ordersDetail = queryBycartitem(skuid, memberId);
            foreach (var item in ordersDetail)
            {
                item.orderDetailnum += 1;
            }
            db.SaveChanges();
        }

       public List<ShoppingCartViewModel> queryByjoinspushopcart(string memberId)
        {
            var groupby = db.spu
               .GroupBy(m => m.memberId)
               .Select(c => new
               {
                   memberId = c.Key,

               });

            var OD = db.ordersDetail.Join(groupby,
               o => o.sellerId,
               d => d.memberId,
               (o, d) => new
               {
                   sellerId = d.memberId,
                   memberId = o.memberId,
                   orderDetailIsApproved = o.orderDetailIsApproved,

               })
               .Where(cs => cs.memberId == memberId && cs.orderDetailIsApproved == "否");

            List<ShoppingCartViewModel> vm = new List<ShoppingCartViewModel>();
            foreach (var item in OD)
            {
                vm.Add(new ShoppingCartViewModel()
                {
                    sellerId = item.sellerId,
                    Detail = db.ordersDetail.Where(m => m.sellerId == item.sellerId && m.memberId == memberId && m.orderDetailIsApproved == "否").Select(s => new ShoppingcartsViewModel()
                    {
                        id = s.id,
                        skuId = s.skuId,
                        @checked = s.@checked,
                        spuId = s.spuId,
                        orderDetailcolor = s.orderDetailcolor,
                        orderDetailsize = s.orderDetailsize,
                        orderDetailnum = s.orderDetailnum,
                        orderDetailspuname = s.orderDetailspuname,
                        orderDetailprice = s.orderDetailprice,
                        spuImg1 = s.spuImg1,

                    })
                });
            }

            return vm;
        }

        public List<ShoppingCartViewModel> queryByjoinspucheckout(string memberId)
        {
            var groupby = db.spu
               .GroupBy(m => m.memberId)
               .Select(c => new
               {
                   memberId = c.Key,

               });

            var OD = db.ordersDetail.Join(groupby,
               o => o.sellerId,
               d => d.memberId,
               (o, d) => new
               {
                   sellerId = d.memberId,
                   memberId = o.memberId,
                   orderDetailIsApproved = o.orderDetailIsApproved,
                   @checked = o.@checked
               })
               .Where(cs => cs.memberId == memberId && cs.orderDetailIsApproved == "否" && cs.@checked == true);

            List<ShoppingCartViewModel> vm = new List<ShoppingCartViewModel>();
            foreach (var item in OD)
            {
                vm.Add(new ShoppingCartViewModel()
                {
                    sellerId = item.sellerId,

                    Detail = db.ordersDetail.Where(m => m.sellerId == item.sellerId && m.memberId == memberId && m.orderDetailIsApproved == "否" && m.@checked == true).Select(s => new ShoppingcartsViewModel()
                    {
                        skuId = s.skuId,
                        orderDetailcolor = s.orderDetailcolor,
                        orderDetailsize = s.orderDetailsize,
                        orderDetailnum = s.orderDetailnum,
                        orderDetailspuname = s.orderDetailspuname,
                        orderDetailprice = s.orderDetailprice,
                        spuImg1 = s.spuImg1,
                        spuId = s.spuId,

                    })
                }); ;
            }
            return vm;
        }

        public void checkout(string[] receiverName, string[] receiverPhone, string[] receiverAddress, string[] sellerIds, string[] ordermessage, string[] paytype,string memberId)
        {
            int index = 0;
            string guid = "";

            foreach (var sId in sellerIds)
            {
                guid = Guid.NewGuid().ToString();

                orders order = new orders();
                order.orderguid = guid;
                order.memberId = memberId;
                order.receiverName = (string)receiverName.GetValue(index);
                order.receiverAddress = (string)receiverAddress.GetValue(index);
                order.receiverPhone = (string)receiverPhone.GetValue(index);
                order.ordermessage = (string)ordermessage.GetValue(index);
                order.orderCreateTime = DateTime.Now;
                if ((string)paytype.GetValue(index) == "銀行轉帳")
                {
                    order.orderState = "未付款";
                }
                else if ((string)paytype.GetValue(index) == "貨到付款")
                {
                    order.orderState = "待出貨";
                }
                order.sellerId = sId;
                db.orders.Add(order);

                var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否" && m.@checked == true && m.sellerId == sId).ToList();


                foreach (var item in ordersDetail)
                {
                    item.orderguid = guid;
                    item.orderDetailIsApproved = "是";
                }
                index++;
            }
            db.SaveChanges();
        }


        public void AddCar(int skuid,int quantity,string memberId)
        {
            var currentCar = db.ordersDetail
                .Where(m => m.skuId == skuid && m.orderDetailIsApproved == "否" && m.memberId == memberId)
                .FirstOrDefault();


            //若currentCar等於null，表示會員選購的產品不是購物車狀態
            if (currentCar == null)
            {
                //找出目前選購的產品並指定給sku
                var spusku = db.sku.Join(db.spu,
                  k => k.spuId,
                  u => u.id,
                  (k, u) => new
                  {
                      spuid = u.id,
                      skuid = k.id,
                      spuname = u.spuName,
                      spuimg = u.spuImg1,
                      color = k.skuColor,
                      size = k.skuSize,
                      price = u.spuPrice,
                      sellerId = u.memberId,
                  }).Where(cs => cs.skuid == skuid).FirstOrDefault();

                //將產品放入訂單明細，因為產品的fIsApproved為"否"，表示為購物車狀態
                ordersDetail orderDetail = new ordersDetail();
                orderDetail.memberId = memberId;
                orderDetail.skuId = skuid;
                orderDetail.spuId = spusku.spuid;
                orderDetail.spuImg1 = spusku.spuimg;
                orderDetail.orderDetailspuname = spusku.spuname;
                orderDetail.orderDetailcolor = spusku.color;
                orderDetail.orderDetailsize = spusku.size;
                orderDetail.orderDetailprice = spusku.price;
                orderDetail.orderDetailnum = quantity;
                orderDetail.orderDetailIsApproved = "否";
                orderDetail.@checked = false;
                orderDetail.sellerId = spusku.sellerId;
                db.ordersDetail.Add(orderDetail);
            }
            else
            {
                //若產品為購物車狀態，即將該產品數量加1
                currentCar.orderDetailnum += quantity;
            }
            db.SaveChanges();
        }

        public void Buyagain(int id,string memberId)
        {
            var skuid = db.orders.Join(db.ordersDetail,
                o => o.orderguid,
                d => d.orderguid,
                (o, d) => new
                {
                    orderId = o.id,
                    skuid = d.skuId,
                }).Where(od => od.orderId == id).Select(od => od.skuid).ToList();
            foreach (var item in skuid)
            {
                var currentCar = db.ordersDetail
                    .Where(m => m.skuId == item && m.orderDetailIsApproved == "否" && m.memberId == memberId)
                    .FirstOrDefault();


                //若currentCar等於null，表示會員選購的產品不是購物車狀態
                if (currentCar == null)
                {
                    //找出目前選購的產品並指定給sku
                    var spusku = db.sku.Join(db.spu,
                      k => k.spuId,
                      u => u.id,
                      (k, u) => new
                      {
                          spuid = u.id,
                          skuid = k.id,
                          spuname = u.spuName,
                          spuimg = u.spuImg1,
                          color = k.skuColor,
                          size = k.skuSize,
                          price = u.spuPrice,
                          sellerId = u.memberId,
                      }).Where(cs => cs.skuid == item).FirstOrDefault();

                    //將產品放入訂單明細，因為產品的fIsApproved為"否"，表示為購物車狀態
                    ordersDetail orderDetail = new ordersDetail();
                    orderDetail.memberId = memberId;
                    orderDetail.skuId = item;
                    orderDetail.spuId = spusku.spuid;
                    orderDetail.spuImg1 = spusku.spuimg;
                    orderDetail.orderDetailspuname = spusku.spuname;
                    orderDetail.orderDetailcolor = spusku.color;
                    orderDetail.orderDetailsize = spusku.size;
                    orderDetail.orderDetailprice = spusku.price;
                    orderDetail.orderDetailIsApproved = "否";
                    orderDetail.@checked = false;
                    orderDetail.sellerId = spusku.sellerId;
                    orderDetail.orderDetailnum = 1;
                    db.ordersDetail.Add(orderDetail);
                }
                else
                {
                    //若產品為購物車狀態，即將該產品數量加1
                    currentCar.orderDetailnum += 1;
                }
            }
            db.SaveChanges();
        }
    }
}