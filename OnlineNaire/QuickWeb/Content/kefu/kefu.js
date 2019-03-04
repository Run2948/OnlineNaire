/*
 * @Author: ecitlm 
 * @Date: 2017-08-04 23:18:26 
 * @Last Modified by:   ecitlm 
 * @Last Modified time: 2017-08-04 23:18:26 
 */

!(function() {
    var serviceOnline = (function() {
        var sideContent = document.querySelector(".side_content");
        var show_btn = document.querySelector(".show_btn");
        var close_btn = document.querySelector(".close_btn");
        var timer = null;

        //����QQ�����ƶ�
        var startMove = function(argument) {
            var scrollsidebar = document.getElementById("scrollsidebar");
            clearInterval(timer);
            timer = setInterval(function() {
                var speed = (argument - scrollsidebar.offsetTop) / 4;
                speed = speed > 0 ? Math.ceil(speed) : Math.floor(speed);
                if (argument == scrollsidebar.offsetTop) {
                    clearInterval(timer);
                } else {
                    scrollsidebar.style.top = scrollsidebar.offsetTop + speed + "px";
                }
            }, 20);
        };

        //����ƶ�
        var scrollMove = function() {
            window.onscroll = window.onload = function() {
                var scrollsidebar = document.getElementById("scrollsidebar");
                var scrolltop =
                    document.body.scrollTop || document.documentElement.scrollTop;
                startMove(
                    parseInt(
                        (document.documentElement.clientHeight -
                            scrollsidebar.offsetHeight) /2 +scrolltop
                    )
                );
            };
        };

        //����QQ��ʾ
        var slideShow = function() {
            if (!show_btn) return false;
            show_btn.addEventListener(
                "click",
                function() {
                    show_btn.style.width = 0;
                    sideContent.style.width = "154px";
                },
                false
            );
        };

        //����QQ����
        var slideClose = function() {
            if (!close_btn) return false;
            close_btn.addEventListener(
                "click",
                function() {
                    console.log(this);
                    sideContent.style.width = 0;
                    show_btn.style.width = "25px";
                },
                false
            );
        };

        //���س����ķ���
        return {
            init: function() {
                scrollMove();
                slideClose();
                slideShow();
            }
        };
    })();

    //��ʼ��
    serviceOnline.init();
})();