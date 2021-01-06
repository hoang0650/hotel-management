$(document).ready(function () {
    _getAll();

    $('#close').click(function () {
        $('#Title_Brand').val("");
        $('#Content_Brand').val("");
        $('#Logo_Brand').val("");
        $('#Link_Brand').val("");
        $("#Logo_Brand img").attr('src', '/Areas/CPanelAdmin/Content/img/add-image.jpg');
    });
  

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
        url: "/Brand/List",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td class=""> <img id="img-preview" width="50" src="..' + item.Logo_Brand + '" /> </td>';
                html += '<td class="">' + item.Title_Brand + '</td>';
                html += '<td><a href="#">' + item.Link_Brand + '</a></td>';
                if (item.IsActive == true) {
                    html += '<td class="hidden-480"><span class="label label-sm label-primary arrowed arrowed-right">Active</span></td>';
                }
                else {
                    html += '<td class="hidden-480"><span class="label label-sm label label-warning arrowed arrowed-right">Not Active</span></td>';
                }

                html += '<td class="hidden-480">';
                html += '<div class="pull-left action-buttons">';
                html += '<a href="#"  onclick="return _getById(' + item.ID_Brand.substring(2, 8) + ')" class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i>Sửa</a><span class="vbar"></span>';
                html += '<a href="#" onclick="return _delete(' + item.ID_Brand.substring(2, 8) + ')"  class="red"><i class="ace-icon fa fa-trash-o bigger-130"></i> Xóa</a>';
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
        url: '/Brand/Get/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Title_Brand').val(result.Title_Brand);
            $('#Content_Brand').val(result.Content_Brand);
            $('#Link_Brand').val(result.Link_Brand);
            $('#IsActive').val(result.IsActive);
            $('#avatar').attr('src', result.Logo_Brand);
            $('#ID_Brand').val(result.ID_Brand);
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
        Title_Brand: $('#Title_Brand').val(),
        Content_Brand: $('#Content_Brand').val(),
        IsActive: $('#IsActive').val(),
        Link_Brand: $('#Link_Brand').val(),
        Logo_Brand: $("#Logo_Brand img").attr('src'),
        IsActive: $('#IsActive').val(),
    }

    $.ajax({
        url: '/Brand/Create',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            _getAll();
            $('#myModal').modal('hide');
            $('#Title_Brand').val("");
            $('#Content_Brand').val("");
            $('#Description_LineCar').val("");
            $('#Link_Brand').val("");
            $("#Logo_Brand img").attr('src', '/Areas/CPanelAdmin/Content/img/add-image.jpg'),
            $.gritter.add({
                title: 'SUCCESS!',
                text: result,
                sticky: false,
                time: '1000',
                class_name: 'gritter-success'
            });
        },
        error: function (errormessage) {
            alert("loi");
        }
    });
}
function _edit() {
    var obj = {
        id: $('#ID_Brand').val(),
        Title_Brand: $('#Title_Brand').val(),
        Content_Brand: $('#Content_Brand').val(),
        Link_Brand: $('#Link_Brand').val(),
        Logo_Brand: $("#Logo_Brand img").attr('src'),
        IsActive: $('#IsActive').val(),
    }
    $.ajax({
        url: '/Brand/Edit',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            _getAll();
            $('#Title_Brand').val("");
            $('#Content_Brand').val("");
            $('#Description_LineCar').val("");
            $('#Link_Brand').val("");
            $("#Logo_Brand img").attr('src', '/Areas/CPanelAdmin/Content/img/add-image.jpg'),
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
            url: '/Brand/Delete/' + id,
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

$('#btnths').click(function () {
    var images = $("#avatar img").attr('src');
    alert(images);
});

jQuery(function ($) {


    //editables on first profile page
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editableform.loading = "<div class='editableform-loading' ><i class='ace-icon fa fa-spinner fa-spin fa-2x light-blue'></i></div>";
    $.fn.editableform.buttons = '<button type="submit" class="btn btn-info editable-submit"><i class="ace-icon fa fa-check"></i></button>' +
        '<button type="button" class="btn editable-cancel"><i class="ace-icon fa fa-times"></i></button>';

    //editables 


    // *** editable avatar *** //
    try { //ie8 throws some harmless exceptions, so let's catch'em

        //first let's add a fake appendChild method for Image element for browsers that have a problem with this
        //because editable plugin calls appendChild, and it causes errors on IE at unpredicted points
        try {
            document.createElement('IMG').appendChild(document.createElement('B'));
        } catch (e) {
            Image.prototype.appendChild = function () {
            };
        }

        var lastGritter;

        $('#avatar').editable({
            type: 'image',
            name: 'avatar',
            value: null,
            
            image: {
                //specify ace file input plugin's options here
                btn_choose: 'Thay đổi ảnh đại diện',
                droppable: true,
                maxSize: 110000000000,//~100Kb

                //and a few extra ones here
                name: 'avatar',//put the field name here as well, will be used inside the custom plugin
                on_error: function (error_type) {//on_error function will be called when the selected file has a problem
                    if (last_gritter) $.gritter.remove(last_gritter);
                    if (error_type == 1) {//file format error
                        last_gritter = $.gritter.add({
                            title: 'File is not an image!',
                            text: 'Please choose a jpg|gif|png image!',
                            class_name: 'gritter-error gritter-center'
                        });
                    } else if (error_type == 2) {//file size rror
                        last_gritter = $.gritter.add({
                            title: 'File too big!',
                            text: 'Image size should not exceed 100Kb!',
                            class_name: 'gritter-error gritter-center'
                        });
                    }
                    else {//other error
                    }
                },
                on_success: function () {
                    $.gritter.removeAll();
                    
                }
            },
            url: function (params) {
                // ***UPDATE AVATAR HERE*** //
                //for a working upload example you can replace the contents of this function with 
                //examples/profile-avatar-update.js

                var deferred = new $.Deferred

                var value = $('#avatar').next().find('input[type=hidden]:eq(0)').val();
                if (!value || value.length == 0) {
                    deferred.resolve();
                    return deferred.promise();
                }

                //dummy upload
                setTimeout(function () {
                    if ("FileReader" in window) {
                        //for browsers that have a thumbnail of selected image
                        var thumb = $('#avatar').next().find('img').data('thumb');
                        if (thumb) $('#avatar').get(0).src = thumb;
                    }
                    deferred.resolve();

                }, parseInt(Math.random() * 800 + 800))

                return deferred.promise();

                // ***END OF UPDATE AVATAR HERE*** //
            },

            success: function (response, newValue) {
                $.gritter.add({
                    title: 'Images Upload!',
                    text: 'Uploading to server can be easily implemented. A working example is included with the template.',
                    class_name: 'gritter-success gritter-right',
                    time: 1000
                });
                
            }
        })

    } catch (e) {
    }
});