$(document).ready(function () {
    _getAll();
   
   
});

function _getAll() {
    $.ajax({
        url: "/Page/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class="">' + item.Title_Page + '</td>';
                html += '<td><a target="_blank" href="' + item.Link_Page + '">' + item.Link_Page + '</a></td>';
                if (item.IsHighlight_Page == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-success arrowed arrowed-right">Hiện trang chủ</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Ẩn</span></td>';
                }
                if (item.IsActive_Page == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }

                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#" onclick="return _getById(' + item.ID_Page.substring(2, 8) + ')"  class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_Page.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
                html += '</div></td></tr>';
            });
            $('#list tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    return false;
}
function _getById(id) {
    $.ajax({
        url: '/Page/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location.href = "/add-page.html";
            setCookie("ID_Page", result.ID_Page, 1);
            setCookie("Title_Page", result.Title_Page, 1);
            setCookie("Content_Page", result.Content_Page, 1);
            setCookie("ID_Menu", result.ID_Menu, 1);
            setCookie("Keyword_Page", result.Keyword_Page, 1);
            setCookie("Description_Page", result.Description_Page, 1);
            setCookie("IsHighlight_Page", result.IsHighlight_Page, 1);
            setCookie("IsActive_Page", result.IsActive_Page, 1);
            
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
            url: '/Page/Delete/' + id,
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
    delCookie("ID_Page");
    delCookie("Title_Page");
    delCookie("Content_Page");

}
function _savePage() {
    var obj = {

        Title_Page: $('#Title_Page').val(),
        ID_Menu: $("#ID_Menu option:selected").val(),
        IsHighlight_Page: $("#IsActive").val(),
        Content_Page: $("#editor1").html(),
        Keyword_Page: $('#Keyword_Page').val(),
        Description_Page: $('#Description_Page').val()
    }
    $.ajax({
        url: '/Page/SavePage',
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

            setTimeout(function () { location.href = "/page.html" }, 1000);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}


function _editPage() {
    var obj = {
        id: $('#ID_Page').val(),
        Title_Page: $('#Title_Page').val(),
        ID_Menu: $("#ID_Menu option:selected").val(),
        IsHighlight_Page: $("#IsActive").val(),
        Content_Page: $("#editor1").html(),
        Keyword_Page: $('#Keyword_Page').val(),
        Description_Page: $('#Description_Page').val()
    }
    
    $.ajax({
        url: '/Page/Edit',
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
            setTimeout(function () { location.href = "/page.html" }, 1000);
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
