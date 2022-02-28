/*左邊開合*/
$(document).ready(function () {
    $("#collapse").on("click", function () {
        $(".sidebar").toggleClass("active");
        $(".fa-align-left").toggleClass("fa-angle-double-left");

    })
})
/*顯示隱藏區塊*/
$(document).ready(function () {
    $(".editSelfFiles").click(function () {
        $(".selfFiles").show(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").hide(1000);
        $(".favItems").hide(1000);
        $(".upload").hide(1000);
        $('.showSku').hide(1000);

    })
})
$(document).ready(function () {
    $(".editOrder").click(function () {
        $("#tabs1").show(1000);
        $("#tabs2").hide(1000);
        $(".selfFiles").hide(1000);
        $(".favItems").hide(1000);
        $(".upload").hide(1000);
        $('.showSku').hide(1000);
    })
})
$(document).ready(function () {
    $(".editFavItems").click(function () {
        $(".favItems").show(1000);
        $(".selfFiles").hide(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").hide(1000);
        $(".upload").hide(1000);
        $('.showSku').hide(1000);
    })
})
$(document).ready(function () {
    $(".buyer-editOrder").click(function () {
        $(".favItems").hide(1000);
        $(".selfFiles").hide(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").show(1000);
        $(".upload").hide(1000);
        $('.showSku').hide(1000);
    })
})
$(document).ready(function () {
    $(".addProduct").click(function () {
        $(".upload").show(1000);
        $(".selfFiles").hide(1000);
        $(".favItems").hide(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").hide(1000);
        $('.showSku').hide(1000);
    })
})

$(document).ready(function () {
    $(".myProduct").click(function () {
        $(".upload").hide(1000);
        $(".selfFiles").hide(1000);
        $(".favItems").hide(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").hide(1000);
        $('.showSku').show(1000);
    })
})

// 買家 賣家click後顯示內容
$(document).ready(function () {
    $(".buyer-features").click(function () {
        $(".editSelfFiles").show(1000);
        $(".editFavItems").show(1000);
        $(".buyer-editOrder").show(1000);
        $(".editOrder").hide(1000);
        $(".addProduct").hide(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").hide(1000);
        $('.myProduct').hide(1000);
    })
})

$(document).ready(function () {
    $(".seller-features").click(function () {
        $(".editFavItems").hide(1000);
        $(".editSelfFiles").hide(1000);
        $(".editOrder").show(1000);
        $(".addProduct").show(1000);
        $("#tabs1").hide(1000);
        $("#tabs2").hide(1000);
        $(".buyer-editOrder").hide(1000);
        $('.myProduct').show(1000);
    })
})