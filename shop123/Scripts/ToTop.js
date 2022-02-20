$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.back-to-top').fadeIn('slow');
    } else {
        $('.back-to-top').fadeOut('slow');
    }
});

$(function () {
    $('.back-to-top').click(function () {
        // 瞬間滾到頂部
        //$('html,body').scrollTop(0)

        // 平滑滾到頂部
        // 總距離
        var $page = $('html,body')
        var distance = $('html').scrollTop() + $('body').scrollTop()
        // 總時間
        var time = 150
        // 間隔時間
        var intervalTime = 1
        var itemDistance = distance / (time / intervalTime)
        // 使用循環定時器不斷滾動
        var intervalId = setInterval(function () {
            distance -= itemDistance
            // 到達頂部, 停止定時器
            if (distance <= 0) {
                distance = 0 //修正
                clearInterval(intervalId)
            }
            $page.scrollTop(distance)
        }, intervalTime)

        return false;
    });
})
