$(document).ready(function () {
    _getAll();

    $('#close').click(function () {
        $('#Title_CarMaker').val("");
        $('#Keyword_CarMaker').val("");
        $('#Description_CarMaker').val("");

    });
    $('#click-active').change(function () {
        var inp = $('#IsActive_CarMaker').get(0);
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
    $('#click-hightlight').change(function () {
        var inp = $('#IsHighight_CarMaker').get(0);
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
        url: "/CarMaker/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class="">' + item.Title_CarMaker + '</td>';
                html += '<td><a href="#">' + item.Link_CarMaker + '</a></td>';
                if (item.IsHighight_CarMaker == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-success arrowed arrowed-right">Hiện trang chủ</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Ẩn</span></td>';
                }
                if (item.IsActive_CarMaker == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }
                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#"  onclick="return _getById(' + item.ID_CarMaker.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_CarMaker.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
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
        url: '/CarMaker/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Title_CarMaker').val(result.Title_CarMaker);
            $('#Keyword_CarMaker').val(result.Keyword_CarMaker);
            $('#Description_CarMaker').val(result.Description_CarMaker);
            $('#IsActive_CarMaker').val(result.IsActive_CarMaker);
            $('#IsHighight_CarMaker').val(result.IsHighight_CarMaker);
            $('#ID_CarMaker').val(result.ID_CarMaker);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function _add() {
    var obj = {
        Title_CarMaker: $('#Title_CarMaker').val(),
        Keyword_CarMaker: $('#Keyword_CarMaker').val(),
        Description_CarMaker: $('#Description_CarMaker').val(),
        IsActive_CarMaker: $('#IsActive_CarMaker').val(),
        IsHighight_CarMaker: $('#IsHighight_CarMaker').val()
    }

    $.ajax({
        url: '/CarMaker/Create',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            _getAll();
            $('#myModal').modal('hide');
            $('#Title_CarMaker').val("");
            $('#Keyword_CarMaker').val("");
            $('#Description_CarMaker').val("");
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
function _edit() {
    var obj = {
        id: $('#ID_CarMaker').val(),
        Title_CarMaker: $('#Title_CarMaker').val(),
        Keyword_CarMaker: $('#Keyword_CarMaker').val(),
        Description_CarMaker: $('#Description_CarMaker').val(),
        IsActive_CarMaker: $('#IsActive_CarMaker').val(),
        IsHighight_CarMaker: $('#IsHighight_CarMaker').val()
    }
    $.ajax({
        url: '/CarMaker/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            _getAll();
            $('#Title_CarMaker').val("");
            $('#Keyword_CarMaker').val("");
            $('#Description_CarMaker').val("");
            $('#myModal').modal('hide');
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
function _delete(id) {

    var cf = confirm('Bạn có chắc chắn muốn xóa mã HX' + id + '');
    if (cf) {
        $.ajax({
            url: '/CarMaker/Delete/' + id,
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