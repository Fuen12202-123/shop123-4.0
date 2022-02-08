const order = document.querySelector(".order-manage");
const product = document.querySelector(".product-manage");
const orderList = document.querySelector(".show-list");
const productList = document.querySelector(".show-product");
const table_all = document.querySelector(".allList");  // 全部按鈕
const all = document.querySelector(".all");
const notpay = document.querySelector(".notpay");  // 未付款按鈕
const nopaylist = document.querySelector(".no-pay");

order.addEventListener("click", function () {
    // console.log("click this");
    orderList.style.display = "flex";
    productList.style.display = "none";
});

product.addEventListener("click", function () {
    orderList.style.display = "none";
    productList.style.display = "flex";
});

all.addEventListener("click", function () {
    table_all.style.display = "table";
    nopaylist.style.display = "none";
});

notpay.addEventListener("click", function () {
    nopaylist.style.display = "table";
    table_all.style.display = "none";
    console.log("123");
});