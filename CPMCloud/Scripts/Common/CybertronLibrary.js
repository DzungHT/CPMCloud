var getFormAsJson = function(formId){
    var data = {};
    $('#' + formId + ' input[type=text]').each(function (x, y) {
        data[y.name] = y.value;
    });
    $('#' + formId + ' input[type=hidden]').each(function (x, y) {
        data[y.name] = y.value;
    });
    $('#' + formId + ' input[type=password]').each(function (x, y) {
        data[y.name] = y.value;
    });
    $('#' + formId + ' input[type=radio]').each(function (x, y) {
        if (y.checked) {
            data[y.name] = y.value;
        }
    });
    $('#' + formId + ' textarea').each(function (x, y) {
        data[y.name] = y.value;
    });
    $('#' + formId + ' select').each(function (x, y) {
        data[y.name] = y.value;
    });
    $('#' + formId + ' input[name=__RequestVerificationToken]').each(function (x, y) {
        data[y.name] = y.value;
    });
    return data;
}

/**
 * Hien thi anh loading.
 */
function tctInitProgress() {
    $("#processingContainer").show();
}

/**
 * An anh loading.
 */
function tctResetProgress() {
    $("#processingContainer").hide();
}

function tctConvertStringToDate(value) {
    var dateArray = value.split('/');
    var day = dateArray[0];
    var month = dateArray[1] - 1; // Javascript consider months in the range 0 - 11
    var year = dateArray[2];
    var sourceDate = new Date(year, month, day);
    return sourceDate;
}

function tctDateToString(sourceDate) {
    var year = sourceDate.getFullYear();
    var month = sourceDate.getMonth() + 1;
    var day = sourceDate.getDate();
    var s = "";
    s += (day < 10) ? "0" + day : day;
    s += "/";
    s += (month < 10) ? "0" + month : month;
    s += "/" + year;
    return s;
}

/**
 * Xu ly di chuyen sang trang moi
 */
function tctRedirect(url, timeout) {
    try {
        tctInitProgress();
        if (undefined == timeout) {
            timeout = 3000;
        }
        setTimeout(function() {
            window.location = (url);
        }, timeout);
    } catch (ex) {
        alert(ex.message);
    }
}

/**
 * An hien noi dung cua fieldset.
 * @param toogleDiv The DIV chua nut an hien
 * @author 
 */
function tctToggleFieldset(toogleDiv) {
    var contentDiv = $(toogleDiv).parent().next();
    if (toogleDiv.is(":hidden")) {
        //toogleDiv.style.backgroundPosition = "0% -45px";
        toogleDiv.show();
    } else {
        //toogleDiv.style.backgroundPosition = "0% -30px";
        toogleDiv.hide();
    }
}

function isNullStr(str) {
    var strVal = String(str);
    strVal = strVal.trim();
    if (!strVal || undefined == str) {
        return true;
    }
    else {
        return false;
    }
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function tctShowMessage(type, message) {
    var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 50, "firstpos2": 20 };
    var opts = {
        title: "Over Here",
        text: "Check me out. I'm in a different stack.",
        addclass: "stack-bottomright",
        stack: stack_bottomright
    };
    switch (type) {
        case 'error':
            opts.title = "Cảnh báo";
            opts.text = message +" !";
            opts.type = "error";
            break;
        case 'info':
            opts.title = "Thông tin";
            opts.text = message + " !";
            opts.type = "info";
            break;
        case 'success':
            opts.title = "Thông báo";
            opts.text = message + " !";
            opts.type = "success";
            break;
    }
    new PNotify(opts);
}
function tctConfirm(callBack,message) {
    $.confirm({
        title: 'Thông báo!',
        content: message,
        icon: 'fa fa-warning',
        type: 'green',
        buttons: {
            confirm: {
                text: 'Đồng ý', // With spaces and symbols
                btnClass: 'btn-success',
                action: function () {
                    if (callBack) {
                        callBack();
                    }
                }
            },
            cancel: {
                text: 'Hủy bỏ', // With spaces and symbols
                btnClass: 'btn-default',
                action: function () { }
            }
        }
    });
}

function tctConfirmLogin() {
    $.confirm({
        title: 'Thông báo!',
        content: 'Vui lòng đăng nhập lại!',
        icon: 'fa fa-warning',
        type: 'orange',
        buttons: {
            confirm: {
                text: 'Đồng ý', // With spaces and symbols
                btnClass: 'btn-success',
                action: function () {
                    var path =  window.location.pathname;
                    var url = '/Accounts/Login?ReturnUrl=' + path;
                    tctRedirect(url, 1000);
                }
            }
        }
    });
}

/**
 * Update Ajax.
 * @param areaId ID vung DIV cap nhat
 * @param actionUrl Xau URL
 * @param formData Tham so gui len server
 * @return deferred call ajax
 */
function tctDeferredAjax(areaId, actionUrl, formData) {
    var dfd = $.Deferred();
    try {
        tctInitProgress();
        dfd = jQuery.ajax({
            type: "POST",
            url: actionUrl,
            data: formData,
            cache: false,
            success: function (html) {
                tctResetProgress();
                jQuery("#" + areaId).html(html);
                //auto call validate jquery unfocus input form
                $("form").validationEngine({ promptPosition: 'inline' });
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    } catch (ex) {
        alert(ex.message);
    }
    return dfd;
}

/**
 * Update Ajax.
 * @param actionUrl Xau URL
 * @param formData Tham so gui len server
 * @return deferred call ajax
 */
function tctDeferredAjaxGet(actionUrl, formData) {
    var dfd = $.Deferred();
    try {
        tctInitProgress();
        dfd = jQuery.ajax({
            type: "POST",
            url: actionUrl,
            data: formData,
            cache: false,
            success: function (html) {
                tctResetProgress();
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    } catch (ex) {
        alert(ex.message);
    }
    return dfd;
}

/**
 * Reset form bang Javascript.
 * Khong reset truong hidden, vi truong hidden thuong la truong de cau hinh.
 * @param formId ID form
 * @return void
 */
function tctResetForm(formId) {
    var formObject = document.getElementById(formId);
    var formElements = formObject.elements;
    for (var i = 0; i < formElements.length; i++) {
        var e = formElements[i];
        if (e.type !== undefined) {
            var fieldType = e.type.toLowerCase();
            switch (fieldType) {
                case "text":
                case "password":
                case "textarea":
                    //case "hidden":
                case "file":
                    e.value = "";
                    break;
                case "radio":
                case "checkbox":
                    if (e.checked) {
                        e.checked = false;
                    }
                    break;
                case "select-one":
                case "select-multi":
                    e.selectedIndex = 0;
                    break;
                default:
                    break;
            }
        }
    }
}