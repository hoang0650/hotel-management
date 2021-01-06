$(document).ready(function () {
    _getAll();

    $('#click-active').change(function () {
        var inp = $('#IsActive').get(0);
        if (inp.hasAttribute('disabled')) {
            inp.setAttribute('readonly', 'true');
            inp.removeAttribute('disabled');
            inp.value = "false";

        }
        else {
            inp.setAttribute('disabled', 'disabled');
            inp.removeAttribute('readonly');
            inp.value = "true";
        }

    });

});


function _getAll() {
    $.ajax({
        url: "/Product/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class=""> <img id="img-preview" src="..' + item.ImgUrl + '" style="width:100%; height:70px" /> </td>';
                html += '<td class="">' + item.Name_Car + '</td>';
                html += '<td><a href="#">' + item.Link_Car + '</a></td>';
                html += '<td><a href="#">' + item.Price_Car + '</a></td>';
                if (item.IsHightlight_Car == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-success arrowed arrowed-right">Hiện trang chủ</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Ẩn</span></td>';
                }
                if (item.IsActive_Car == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }

                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#"  onclick="return _getById(' + item.ID_Car.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_Car.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
                html += '</div></td></tr>';
            });
            $('#list tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    return false;
}

function _getById(id) {
    $.ajax({
        url: '/Product/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            alert(JSON.stringify(result))
            window.location.href = "/add-product.html";
            setCookie("ID_Car", result.ID_Car, 1);
            setCookie("Title_Car", result.Title_Car, 1);
            setCookie("Content_Car", result.Content_Car, 1);
            setCookie("ID_CarKind", result.ID_CarKind, 1);
            setCookie("ID_CarMaker", result.ID_CarMaker, 1);
            setCookie("ID_LineCar", result.ID_LineCar, 1);
            setCookie("Keyword_Car", result.Keyword_Car, 1);
            setCookie("Description_Car", result.Description_Car, 1);
            setCookie("IsHightlight_Car", result.IsHightlight_Car, 1);
            setCookie("ImgUrl", result.ImgUrl, 1);
            setCookie("Price_Car", result.Price_Car, 1);
            setCookie("Life_Car", result.Life_Car, 1);
            setCookie("Color_Car", result.Color_Carl, 1);
            etCookie("Quantity_Car", result.Quantity_Car, 1);
            setCookie("Note_Car", result.Note_Car, 1);
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function _delete(id) {

    var cf = confirm('Bạn có chắc chắn muốn xóa mã XE' + id + '');
    if (cf) {
        $.ajax({
            url: '/Product/Delete/' + id,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (result) {
                _getAll();
                $.gritter.add({
                    title: 'SUCCESS!',
                    text: result,
                    sticky: false,
                    time: '1000',
                    class_name: 'gritter-success'
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function _delAllCookie() {
    delCookie("ID_Car");
    delCookie("Title_Car");
    delCookie("Content_Car");
    delCookie("IsHightlight_Car");
    delCookie("Description_Car");
    delCookie("Keyword_Car");
    delCookie("ImgUrl");
    delCookie("Price_Car");
    delCookie("Life_Car");
    delCookie("Color_Car");
    delCookie("Note_Car");
    delCookie("ID_CarKind");
    delCookie("ID_CarMaker");
    delCookie("ID_LineCar");
    delCookie("Quantity_Car");


}
function _save() {
    var obj = {
        Title_Car: $('#Title_Car').val(),
        ID_CarKind: $("#ID_CarKind option:selected").val(),
        ID_CarMaker: $("#ID_CarMaker option:selected").val(),
        ID_LineCar: $("#ID_LineCar option:selected").val(),
        IsHighlight_Car: $("#IsActive").val(),
        Content_Car: $("#editor1").html(),
        Keyword_Car: $('#Keyword_Car').val(),
        Description_Car: $('#Description_Car').val(),
        ImgUrl: $('#ImgUrl').val(),
        Price_Carr: $('#Price_Car').val(),
        Life_Car: $('#Life_Car').val(),
        Color_Car: $('#Color_Car').val(),
        Note_Car: $('#Note_Car').val(),
        Quantity_Car: $('#Quantity_Car').val(),
       
    }
  
    $.ajax({
        url: '/Product/Save',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.gritter.add({
                title: 'SUCCESS!',
                text: response.Msg,
                sticky: false,
                time: '500',
                class_name: 'gritter-success'
            });

            setTimeout(function () { location.href = "/product.html" }, 1000);

        },
        error: function (response) {
            $.gritter.add({
                title: 'Error!',
                text: response.Msg,
                sticky: false,
                time: '1000',
                class_name: 'gritter-error'
            });
        }
    });


}
function _edit() {
    var obj = {
        id: $('#ID_Car').val(),
        Title_Car: $('#Title_Car').val(),
        ID_CarKind: $("#ID_CarKind option:selected").val(),
        ID_CarMaker: $("#ID_CarMaker option:selected").val(),
        ID_LineCar: $("#ID_LineCard option:selected").val(),
        IsHighlight_Car: $("#IsActive").val(),
        Content_Car: $("#editor1").html(),
        Keyword_Car: $('#Keyword_Car').val(),
        Description_Car: $('#Description_Car').val(),
        ImgUrl: $('#ImgUrl').val(),
        Price_Carr: $('#Price_Car').val(),
        Life_Car: $('#Life_Car').val(),
        Color_Car: $('#Color_Car').val(),
        Note_Car: $('#Note_Car').val(),
        Quantity_Car: $('#Quantity_Car').val(),
    }

    $.ajax({
        url: '/Product/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            $.gritter.add({
                title: 'SUCCESS!',
                text: result,
                sticky: false,
                time: '1000',
                class_name: 'gritter-success'
            });
            _delAllCookie();
            setTimeout(function () { location.href = "/product.html" }, 1000);
        },
        error: function (response) {
            $.gritter.add({
                title: 'Error!',
                text: response.Msg,
                sticky: false,
                time: '1000',
                class_name: 'gritter-error'
            });
        }
    });
}



//Hàm Cookie
function setCookie(name, value, days) {
    var date, expires;
    if (days) {
        date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(name) {
    var i, c, ca, nameEQ = name + "=";
    ca = document.cookie.split(';');
    for (i = 0; i < ca.length; i++) {
        c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length);
        }
        if (c.indexOf(nameEQ) == 0) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return '';
}

function checkCookie() {
    var username = getCookie("username");
    if (username != "") {
        alert("Welcome again " + username);
    } else {
        username = prompt("Please enter your name:", "");
        if (username != "" && username != null) {
            setCookie("username", username, 365);
        }
    }
}

function delCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}
// End Hàm cookie
