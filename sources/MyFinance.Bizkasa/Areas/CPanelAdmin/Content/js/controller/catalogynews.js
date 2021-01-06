$(document).ready(function () {
    _getAll();

    $('#close').click(function () {
        $('#Title_CatalogiesNews').val("");
        $('#Keyword_CatalogiesNews').val("");
        $('#Description_CatalogiesNews').val("");

    });
  
    $('#click-active').change(function () {
        var inp = $('#IsActive_CatalogiesNews').get(0);
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
        url: "/CatalogyNews/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class="">' + item.Title_CatalogiesNews + '</td>';
                html += '<td><a href="#">' + item.Link_CatalogiesNews + '</a></td>';
                html += '<td class="">' + item.Title_Menu + '</td>';
                if (item.IsActive_CatalogiesNews == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }
                html += '<td>' + item.Title_Lang + " (" + item.ID_Lang + ") " + '</td>';
                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#"  onclick="return _getById(' + item.ID_Catalogies.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_Catalogies.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
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
        url: '/CatalogyNews/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Title_CatalogiesNews').val(result.Title_CatalogiesNews);
            $('#Keyword_CatalogiesNews').val(result.Keyword_CatalogiesNews);
            $('#Description_CatalogiesNews').val(result.Description_CatalogiesNews);
            $('#IsActive_CatalogiesNews').val(result.IsActive_CatalogiesNews);
            $('#ID_Catalogies').val(result.ID_Catalogies);
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
        Title_CatalogiesNews: $('#Title_CatalogiesNews').val(),
        Keyword_CatalogiesNews: $('#Keyword_CatalogiesNews').val(),
        Description_CatalogiesNews: $('#Description_CatalogiesNews').val(),
        IsActive_CatalogiesNews: $('#IsActive_CatalogiesNews').val(),

    }

    $.ajax({
        url: '/CatalogyNews/Create',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            _getAll();
            $('#myModal').modal('hide');
            $('#Title_CatalogiesNews').val("");
            $('#Keyword_CatalogiesNews').val("");
            $('#Description_CatalogiesNews').val("");
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
        id: $('#ID_Catalogies').val(),
        Title_CatalogiesNews: $('#Title_CatalogiesNews').val(),
        Keyword_CatalogiesNews: $('#Keyword_CatalogiesNews').val(),
        Description_CatalogiesNews: $('#Description_CatalogiesNews').val(),
        IsActive_CatalogiesNews: $('#IsActive_CatalogiesNews').val(),
      
    }
    $.ajax({
        url: '/CatalogyNews/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            _getAll();
            $('#Title_CatalogiesNews').val("");
            $('#Keyword_CatalogiesNews').val("");
            $('#Description_CatalogiesNews').val("");
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
            url: '/CatalogyNews/Delete/' + id,
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