(function($) {
    'use strict';

    var searchEl = $('#search'),
        posterEl = $('.poster'),
        opts = {
            lines: 11, // The number of lines to draw
            length: 24, // The length of each line
            width: 4, // The line thickness
            radius: 30, // The radius of the inner circle
            corners: 1, // Corner roundness (0..1)
            rotate: 0, // The rotation offset
            direction: 1, // 1: clockwise, -1: counterclockwise
            color: '#000', // #rgb or #rrggbb or array of colors
            speed: 1, // Rounds per second
            trail: 60, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        },
        spinner = new Spinner(opts);
    
    $(document).on('keydown', function(e) {
        if (searchEl.is(":focus")) {
            if (e.which == 13) {
                submitSearch();
            }
            return;
        }
        
        searchEl.focus();
    });
    
    function submitSearch() {
        posterEl.hide();
        startSpin();

        setTimeout(function () {
            stopSpin();
            posterEl.show();
        }, 4000);
    }
    
    function startSpin() {
        var target = document.getElementById('body');
        spinner.spin(target);
    }

    function stopSpin() {
        spinner.stop();
    }
    
    function animateIn() {
        posterEl.addClass('animate-in')
                .removeClass('animate-out-right animate-out-left')
                .one('webkitAnimationEnd oanimationend msAnimationEnd animationend', animationComplete);
    }
    
    function simulateLeft(e) {
        if (e) {
            e.preventDefault();
        }
        posterEl.removeClass('reflect')
               .addClass('animate-out-right')
               .one('webkitAnimationEnd oanimationend msAnimationEnd animationend', animateIn);
    }
    
    function simulateRight(e) {
        if (e) {
            e.preventDefault();
        }
        posterEl.removeClass('reflect')
                .addClass('animate-out-left')
                .one('webkitAnimationEnd oanimationend msAnimationEnd animationend', animateIn);
    }
    
    function animationComplete() {
        posterEl.removeClass('animate-in')
                 .addClass('reflect');
    }

    $('.fa-chevron-left').on('click', simulateLeft); 
    $('.fa-chevron-right').on('click', simulateRight);
    
    var controller = new Leap.Controller({ enableGestures: true });

    controller.on('connect', function () {
        console.log("Successfully connected.");
    });

    controller.on('deviceConnected', function () {
        console.log("A Leap device has been connected.");
    });

    controller.on('deviceDisconnected', function () {
        console.log("A Leap device has been disconnected.");
    });

    controller.on('frame', function (frame) {
        for (var i = 0; i < frame.gestures.length; i++) {
            var gesture = frame.gestures[i];
            var type = gesture.type;
            var state = gesture.state;
            if (state === 'stop') {
                switch (type) {
                    case "circle":
                        onCircle(gesture);
                        break;
                    case "swipe":
                        onSwipe(gesture);
                        break;
                    case "screenTap":
                        onScreenTap(gesture);
                        break;
                    case "keyTap":
                        onKeyTap(gesture);
                        break;
                }
            }
        }
    });
    
    function onCircle(gesture) {
        console.log('circle performed');
    }
    function onSwipe(gesture) {
        console.log('swipe performed');
        if (gesture.direction[0] > 0) {
            simulateLeft();
        } else {
            simulateRight();
        }
    }
    function onScreenTap(gesture) {
        console.log('screenTap performed');
    }
    function onKeyTap(gesture) {
        console.log('keyTap performed');
    }
    
    controller.connect();

})(jQuery);