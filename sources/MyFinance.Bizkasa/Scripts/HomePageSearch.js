function isEmpty(obj) {
    if (typeof obj == 'undefined' || obj === null || obj === '') return true;
    if (typeof obj == 'number' && isNaN(obj)) return true;
    if (obj instanceof Date && isNaN(Number(obj))) return true;
    return false;
}
$(document).ready(function ()
{
    $('input[value="oneway"],input[value="qt_oneway"]').attr("checked", "checked");
    $("#div-ReturnDate").hide();
    $("#qt_ReturnDate").hide();
});
$(function ()
{
    $('input[value="oneway"]').click(function () {
        $("#div-ReturnDate").hide();
    });
    $('input[value="qt_oneway"]').click(function ()
    {
        $("#qt_ReturnDate").hide();
    });
    $('input[value="roundtrip"]').click(function () {
        $("#div-ReturnDate").show();
    });
    $('input[value="qt_roundtrip"]').click(function () {
        $("#qt_ReturnDate").show();
    });
    $("#div-loading").hide();

    $("#search-flights").click(function () {
        $("#div-loading").show();
    });
    $(".sl-city").find('input[type=text]').click(function (e) {
        $('.sl-city').removeClass('active');
        var parent = $(this).closest('.sl-city');
        parent.addClass('active');
        parent.find('.txtFlightCity').focus();
        e.stopPropagation();
    });
    $('body').click(function (e) {
        $('.sl-city').removeClass('active');
        $(".startdate").datepicker('hide');
        $(".enddate").datepicker('hide');

   
    });
    $(".startplace,.endplace").focus(function () {
        $(this).addClass('focus-input');
        dateType = $(this).attr('datetype');
        var deOffset = $('.startplace').offset();
        var arrOffset = $('.endplace').offset();
        var interCityInput = '';
        $(".txtFlightCity").keyup(function () {
            interCityInput = $(this).val();
        });
        var depCity = $('.startplace').val();
        var arrCity = $('.endplace').val();
        if (isEmpty(interCityInput)) {
            $(".submit").click(function () {
                var father = $(this).closest('.sl-city');
                var val = father.find(".txtFlightCity").val().trim();
                if (val == '') {
                    father.find('.error').text('Xin hãy nhập tên thành phố hoặc sân bay để tiếp tục.');
                    father.find('.txtFlightCity').attr('value', val);
                    return false;
                }
                else {
                    father.find('.error').text(' ');
                    father.find('.city').attr('value', val);
                    father.removeClass('active');
                }

            });
        }
        // get data from dialog when click
        $('.list-city a').click(function () {
            var father = $(this).closest('.sl-city');
            father.find('.city').attr('value', $(this).text());
            father.removeClass('active');
            return false;
        });
     
    });


    $('.departure-date').datepicker(
        {
            onSelect: function (date)
            {
                if ($('input[value="roundtrip"]').attr('checked') == 'checked') {
                    var rtdatetext = $('.return-date').val().split('/');
                    var depdatetext = date.split('/')
                    var rtdate = new Date(rtdatetext[2], (rtdatetext[1] - 1), rtdatetext[0]);
                    var depdate = new Date(depdatetext[2], (depdatetext[1] - 1), depdatetext[0]);
                    var oneday = 24 * 60 * 60 * 1000;
                    var count = Math.round((rtdate - depdate) / (oneday));
                    if (count <= 0) {
                        var newdate = new Date(depdate.setDate(depdate.getDate() + 3));
                        var strnewdate = newdate.getDate() + "/" + (newdate.getMonth() + 1) + "/" + newdate.getFullYear();
                        $('.return-date').val(strnewdate);
                     
                    }

                }
            },
            dateFormat:"dd/mm/yy"
        })
    $('.return-date,.qt_return-date').datepicker({
            showOn: 'both', buttonImage: '/Images/ThemeForest/icon/blank.png', buttonText: '', buttonImageOnly: true, changeYear: false,dateFormat:"dd/mm/yy"
        }
        )
   

    $('.qt_deparutre-date').datepicker({
        onSelect: function (date) {
            if ($('input[value="qt_roundtrip"]').attr('checked') == 'checked') {
                var rtdatetext = $('.qt_return-date').val().split('/');
                var depdatetext = date.split('/')
                var rtdate = new Date(rtdatetext[2], (rtdatetext[1] - 1), rtdatetext[0]);
                var depdate = new Date(depdatetext[2], (depdatetext[1] - 1), depdatetext[0]);
                var oneday = 24 * 60 * 60 * 1000;
                var count = Math.round((rtdate - depdate) / (oneday));
                if (count <= 0) {
                    var newdate = new Date(depdate.setDate(depdate.getDate() + 3));
                    var strnewdate = newdate.getDate() + "/" + (newdate.getMonth() + 1) + "/" + newdate.getFullYear();
                    $('.qt_return-date').val(strnewdate);
                }

            }
        },
        dateFormat: "dd/mm/yy"

    });

    
})
