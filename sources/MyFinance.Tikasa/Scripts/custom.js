/**
 * Created by nguyenphung on 9/24/16.
 */

jQuery(document).ready(function($) {

    //change view type
    $('.sort_grid a').on('click',function(){
        $('.booking-list').hide();
        $('.booking-grid').show();
        $('.sort_list a').removeClass('active');
        $(this).addClass('active');
    });
    $('.sort_list a').on('click',function(){
        $('.booking-grid').hide();
        $('.booking-list').show();
        $('.sort_grid a').removeClass('active');
        $(this).addClass('active');
    });

    //gallery imgs
    $('.st-gp-item').each(function(index , value){
        var gallery = $(this);
        var galleryImages = $(this).data('links').split(',');
        var items = [];
        for(var i=0;i<galleryImages.length; i++){
            items.push({
                src:galleryImages[i],
                title:''
            });
        }
        gallery.magnificPopup({
            mainClass: 'mfp-fade',
            items:items,
            gallery:{
                enabled:true,
                tPrev: $(this).data('prev-text'),
                tNext: $(this).data('next-text')
            },
            type: 'image'
        });
    });
/*detail page*/

    $('.booking-item-review-expand').click(function(event) {
        var parent = $(this).parent('.booking-item-review-content');
        if (parent.hasClass('expanded')) {
            parent.removeClass('expanded');
        } else {
            parent.addClass('expanded');
        }
    });
    $('.stats-list-select > li > .booking-item-rating-stars > li').each(function() {
        var list = $(this).parent(),
            listItems = list.children(),
            itemIndex = $(this).index(),
            parentItem = list.parent();

        $(this).hover(function() {
            for (var i = 0; i < listItems.length; i++) {
                if (i <= itemIndex) {
                    $(listItems[i]).addClass('hovered');
                } else {
                    break;
                }
            };
            $(this).click(function() {
                for (var i = 0; i < listItems.length; i++) {
                    if (i <= itemIndex) {
                        $(listItems[i]).addClass('selected');
                    } else {
                        $(listItems[i]).removeClass('selected');
                    }
                };

                parentItem.children('.st_review_stats').val(itemIndex + 1);
            });
        }, function() {
            listItems.removeClass('hovered');
        });
    });

//tab in detail page
    $(document).on('click','.ui-state-default .ui-tabs-anchor',function(e){
        var id = $(this).attr('href');
        $('.ui-tabs-panel').hide();
        $(id).show();
        $('.nav-detail-page .ui-state-default').removeClass('ui-state-active');
        $(this).parent('.ui-state-default').addClass('ui-state-active');

        e.preventDefault();
    })
});


