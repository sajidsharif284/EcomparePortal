/// <reference path="jquery-1.12.3.js" />

(function ($) {
    var list = [];

    /* function to be executed when product is selected for comparision*/

    $(document).on('click', '.addToCompare', function () {
        debugger;
        $(".comparePanle").show();
        $(this).toggleClass("rotateBtn");
        var a = $(this).attr('data-id');
       $(this).parents(".selectProduct").toggleClass("selected");
        //var productID = $(this).parents('.selectProduct').attr('data-title');
        var productID = $(this).attr('data-id');
        var inArray = $.inArray(productID, list);
        if (inArray < 0) {
            if (list.length > 1) {
                $("#WarningModal").show();
                $("#warningModalClose").click(function () {
                    $("#WarningModal").hide();
                });
                $(this).toggleClass("rotateBtn");
                $(this).parents(".selectProduct").toggleClass("selected");
                return;
            }

            if (list.length < 2) {
                list.push(productID);

                //var displayTitle = $(this).parents('.selectProduct').attr('data-id');
                var displayTitle = $(this).attr('data-title');
               // var image = $(this).siblings(".productImg").attr('src');
                var image = $(this).attr('data-img');
                $(".comparePan").append('<div id="' + productID + '" class="relPos titleMargin w3-margin-bottom   w3-col l3 m4 s4"><div class="w3-white titleMargin"><a class="selectedItemCloseBtn w3-closebtn cursor">&times</a><img src="' + image + '" alt="image" style="height:70px;width:161px"/><p style="font-size:15px" id="' + productID + '" class="titleMargin1">' + displayTitle + '</p></div></div>');
            }
        } else {
            debugger;
            list.splice($.inArray(productID, list), 1);
            var prod = productID.replace(" ", "");
            $('#' + prod).remove();
            hideComparePanel();

        }
        if (list.length > 1) {

            $(".cmprBtn").addClass("active");
            $(".cmprBtn").removeAttr('disabled');
        } else {
            $(".cmprBtn").removeClass("active");
            $(".cmprBtn").attr('disabled', '');
        }

    });
    /*function to be executed when compare button is clicked*/
    $(document).on('click', '.cmprBtn', function () {
        debugger;
        if ($(".cmprBtn").hasClass("active")) {
            /* this is to print the  features list statically*/
            $(".contentPop").append('<div class="w3-col s3 m3 l3 compareItemParent relPos">' + '<ul class="product">' + '<li class=" relPos compHeader"><p class="w3-display-middle">Features</p></li>' + '<li>Title</li>' + '<li>Size</li>' + '<li>Weight</li>' + '<li class="cpu">Processor</li>' + '<li>Battery</li></ul>' + '</div>');

            for (var i = 0; i < list.length; i++) {
                /* this is to add the items to popup which are selected for comparision */
                product = $('.selectProduct[data-id="' + list[i] + '"]');
               // var image = $('[data-title=' + list[i] + ']').find(".productImg").attr('src');
                var image = $('[data-id=' + list[i] + ']').attr('data-img');
                var title = $('[data-id=' + list[i] + ']').attr('data-company');
               
                /*appending to div*/
                $(".contentPop").append('<div class="w3-col s3 m3 l3 compareItemParent relPos">' + '<ul class="product">' + '<li class="compHeader"><img src="' + image + '" class="compareThumb"></li>' + '<li>' + title + '</li>' + '<li>' + $(product).data('title') + '</li>' + '<li>' + $(product).data('id') + '<li class="cpu">' + $(product).data('company') + '</li>' + '<li>' + $(product).data('battery') + '</ul>' + '</div>');
            }
        }
        $(".modPos").show();
    });

    /* function to close the comparision popup */
    $(document).on('click', '.closeBtn', function () {
        debugger;
        $(".contentPop").empty();
        $(".comparePan").empty();
        $(".comparePanle").hide();
        $(".modPos").hide();
        $(".selectProduct").removeClass("selected");
        $(".cmprBtn").attr('disabled', '');
        list.length = 0;
        $(".rotateBtn").toggleClass("rotateBtn");
    });

    /*function to remove item from preview panel*/
    $(document).on('click', '.selectedItemCloseBtn', function () {
        debugger;
        var test = $(this).siblings("p").attr('id');
        $('[data-title=' + test + ']').find(".addToCompare").click();
        hideComparePanel();
    });

    function hideComparePanel() {
        debugger;
        if (list.length) {
            $(".comparePan").empty();
            $(".comparePanle").hide();
        }
    }
})(jQuery);
