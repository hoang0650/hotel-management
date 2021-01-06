$(document).ready(function () {
    _getAll();

    $('#close').click(function () {
        $('#Title_Info').val("");
        $('#Address_Info').val("");
        $('#Email_Info').val("");
        $('#Phone_Info').val("");
        $('#Fax_Info').val("");
        $('#LinkWebsite_Info').val("");
    });

    $('#click-active').change(function () {
        var inp = $('#IsActive_Info').get(0);
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
        url: "/Info/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class="">' + item.Title_info + '</td>';
                html += '<td><a href="#">' + item.Address_Info + '</a></td>';
                html += '<td>' + item.Phone_Info + '</td>';
                if (item.IsActive_Info == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }

               
                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#"  onclick="return _getById(' + item.ID_Info.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_Info.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
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
        url: '/Info/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Title_info').val(result.Title_info);
            $('#Address_Info').val(result.Address_Info);
            $('#Email_Info').val(result.Email_Info);
            $('#Phone_Info').val(result.Phone_Info);
            $('#Fax_Info').val(result.Fax_Info);
            $('#LinkWebsite_Info').val(result.LinkWebsite_Info);
            $('#isActive_Info').val(result.IsActive_Info);
            $('#ID_Info').val(result.ID_Info);
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
        Title_info: $('#Title_info').val(),
        Address_Info: $('#Address_Info').val(),
        Email_Info: $('#Email_Info').val(),
        Phone_Info: $('#Phone_Info').val(),
        Fax_Info: $('#Fax_Info').val(),
        LinkWebsite_Info: $('#LinkWebsite_Info').val(),
        IsActive_Info: $('#IsActive_Info').val(),
    }
    $.ajax({
        url: '/Info/Create',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            _getAll();
            $('#myModal').modal('hide');
            $('#Title_Info').val("");
            $('#Address_Info').val("");
            $('#Email_Info').val("");
            $('#Phone_Info').val("");
            $('#Fax_Info').val("");
            $('#LinkWebsite_Info').val("");
            $.gritter.add({
                title: 'SUCCESS!',
                text: result,
                sticky: false,
                time: '1000',
                class_name: 'gritter-success'
            });
        },
        error: function (errormessage) {
            $.gritter.add({
                title: 'ERROR!',
                text: 'Thêm thông tin không thành công.',
                sticky: false,
                time: '1000',
                class_name: 'gritter-error'

            });
        }
    });
}
function _edit() {
    var obj = {
        id: $('#ID_Info').val(),
        Title_info: $('#Title_info').val(),
        Address_Info: $('#Address_Info').val(),
        Email_Info: $('#Email_Info').val(),
        Phone_Info: $('#Phone_Info').val(),
        Fax_Info: $('#Fax_Info').val(),
        LinkWebsite_Info: $('#LinkWebsite_Info').val(),
        IsActive_Info: $('#IsActive_Info').val(),
    }
    $.ajax({
        url: '/Info/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            _getAll();
            $('#Title_Info').val("");
            $('#Address_Info').val("");
            $('#Email_Info').val("");
            $('#Phone_Info').val("");
            $('#Fax_Info').val("");
            $('#LinkWebsite_Info').val("");
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
            $.gritter.add({
                title: 'ERROR!',
                text: 'Sửa thông tin không thành công.',
                sticky: false,
                time: '1000',
                class_name: 'gritter-error'

            });
        }
    });
}
function _delete(id) {

    var cf = confirm('Bạn có chắc chắn muốn xóa mã IF' + id + '');
    if (cf) {
        $.ajax({
            url: '/Info/Delete/' + id,
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
            error: function (result) {
                $.gritter.add({
                    title: 'ERROR!',
                    text: 'Xóa thông tin không thành công.',
                    sticky: false,
                    time: '1000',
                    class_name: 'gritter-error'

                });
            }
        });
    }
}