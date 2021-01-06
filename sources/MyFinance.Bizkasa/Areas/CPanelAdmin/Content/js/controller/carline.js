$(document).ready(function () {
    _getAll();

    $('#close').click(function () {
        $('#Title_LineCar').val("");
        $('#Keyword_LineCar').val("");
        $('#Description_LineCar').val("");

    });
  

    $('#click-active').change(function () {
        var inp = $('#IsActive_LineCar').get(0);
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
        url: "/CarLine/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class="">' + item.Title_LineCar + '</td>';
                html += '<td><a href="#">' + item.Link_LineCar + '</a></td>';
                if (item.IsActive_LineCar == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }

                html += '<td>' + item.Title_Lang + " (" + item.ID_Lang + ") " + '</td>';
                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#"  onclick="return _getById(' + item.ID_LineCar.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_LineCar.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
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
        url: '/CarLine/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Title_LineCar').val(result.Title_LineCar);
            $('#Keyword_LineCar').val(result.Keyword_LineCar);
            $('#Description_LineCar').val(result.Description_LineCar);
            $('#IsActive_LineCar').val(result.IsActive_LineCar);
            $('#ID_LineCar').val(result.ID_LineCar);
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
        Title_LineCar: $('#Title_LineCar').val(),
        Keyword_LineCar: $('#Keyword_LineCar').val(),
        Description_LineCar: $('#Description_LineCar').val(),
        IsActive_LineCar: $('#IsActive_LineCar').val(),
    }

    $.ajax({
        url: '/CarLine/Create',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            _getAll();
            $('#myModal').modal('hide');
            $('#Title_LineCar').val("");
            $('#Keyword_LineCar').val("");
            $('#Description_LineCar').val("");
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
        id: $('#ID_LineCar').val(),
        Title_LineCar: $('#Title_LineCar').val(),
        Keyword_LineCar: $('#Keyword_LineCar').val(),
        Description_LineCar: $('#Description_LineCar').val(),
        IsActive_LineCar: $('#IsActive_LineCar').val(),
    }
    $.ajax({
        url: '/CarLine/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            _getAll();
            $('#Title_LineCar').val("");
            $('#Keyword_LineCar').val("");
            $('#Description_LineCar').val("");
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

    var cf = confirm('Bạn có chắc chắn muốn xóa mã DX' + id + '');
    if (cf) {
        $.ajax({
            url: '/CarLine/Delete/' + id,
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