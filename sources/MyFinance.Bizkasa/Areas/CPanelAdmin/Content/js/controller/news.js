$(document).ready(function () {
    _getAll();

   
});


function _getAll() {
    $.ajax({
        url: "/News/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class=""> <img id="img-preview" style="width:100%; height:70px" src="..' + item.ImgUrl_News + '" /> </td>';
                html += '<td class="">' + item.Title_News + '</td>';
                html += '<td><a target="_blank" href="' + item.Link_News + '">' + item.Link_News + '</a></td>';
                html += '<td><a href="#">' + item.Views + '</a></td>';
                if (item.IsActive_News == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }

                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#" onclick="return _getById(' + item.ID_News.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_News.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
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
        url: '/News/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location.href = "/add-news.html";
            setCookie("ID_News", result.ID_News, 1);
            setCookie("Title_News", result.Title_News, 1);
            setCookie("Content_News", result.Content_News, 1);
            setCookie("ID_Menu", result.ID_Menu, 1);
            setCookie("Keyword_News", result.Keyword_News, 1);
            setCookie("Description_News", result.Description_News, 1);
            setCookie("IsHighlight_Page", result.IsHighlight_Page, 1);
            setCookie("ImgUrl_News", result.ImgUrl_News, 1);
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function _delete(id) {

    var cf = confirm('Bạn có chắc chắn muốn xóa mã PA' + id + '');
    if (cf) {
        $.ajax({
            url: '/News/Delete/' + id,
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
    delCookie("ID_News");
    delCookie("Title_News");
    delCookie("IsHighlight_Page");
    delCookie("Description_News");
    delCookie("ID_Menu");
    delCookie("Keyword_News");
    delCookie("ImgUrl_News");


}
function _savePage() {
    var obj = {
        Title_News: $('#Title_News').val(),
        ID_Catalogies: $("#ID_Menu option:selected").val(),
        IsHighlight_News: $("#IsActive").val(),
        Content_News: $("#editor1").html(),
        Keyword_News: $('#Keyword_News').val(),
        Description_News: $('#Description_News').val(),
        ImgUrl_News: $('#ImgUrl_News').val()
    }

    $.ajax({
        url: '/News/SaveNews',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.gritter.add({
                title: 'SUCCESS!',
                text: result,
                sticky: false,
                time: '500',
                class_name: 'gritter-success'
            });

            setTimeout(function () { location.href = "/news.html" }, 1000);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}
function _editPage() {
    var obj = {
        id: $('#ID_News').val(),
        Title_News: $('#Title_News').val(),
        ID_Catalogies: $("#ID_Menu option:selected").val(),
        IsHighlight_News: $("#IsActive").val(),
        Content_News: $("#editor1").html(),
        Keyword_News: $('#Keyword_News').val(),
        Description_News: $('#Description_News').val(),
        ImgUrl_News: $('#ImgUrl_News').val()
    }

    $.ajax({
        url: '/News/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            _delAllCookie();
            $.gritter.add({
                title: 'SUCCESS!',
                text: result,
                sticky: false,
                time: '1000',
                class_name: 'gritter-success'
            });
            setTimeout(function () { location.href = "/News.html" }, 1000);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
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
