$(function () {
   

    //$("tr:odd").live("addClass","odd");
    //$("tr:even").live("addClass","even");


    $(".selectAll").live("click", function () {
        $(".listcheck").attr("checked", "checked");
    });
    $(".unSelect").live("click", function () {
        $(".listcheck").attr("checked", false);
    });
    $(".delSelect").live("click", function () {
        var strId = "";
        var DelMoreUrl = $(this).attr("url");
        $("input[name='listcheck']:checkbox").each(function () {
            if ($(this).attr("checked")) {
                strId += $(this).val() + "$"
            }
        })
        if (strId != "" && strId != undefined) {
            $.post(DelMoreUrl, { ListId: strId }, function (d) {
                if (d == "True") {
                    alert("操作成功！");
                    window.location.reload();
                }
                else { alert("操作失败！"); }
            });
        }
        else { alert("请先去选中！"); }
    });
    $(".TransferSelect").live("click", function () {
        var strId = "";
        var toUrl = $(this).attr("url");
        var TabTitle = $(this).attr("title");
        $("input[name='listcheck']:checkbox").each(function () {
            if ($(this).attr("checked")) {
                strId += $(this).val() + "$"
            }
        })
        if (strId != "" && strId != undefined) {
            var $dialog = $("#adddialog");
            $dialog.html("");
            $dialog.dialog("option", "title", TabTitle);
            $dialog.dialog("open");
            $.get(toUrl, { ListId: strId }, function (d) { $dialog.html(d); });
        }
        else { alert("请先去选中！"); }
    });


    $(".delone").live("click", function () {
        var a = confirm("您确定要这么操作吗？");
        if (a == true) {
            var _this = $(this);
            var Id = _this.attr("ref");
            var DelUrl = _this.attr("url");
            $.post(DelUrl, { Id: Id }, function (d) {
                if (d == "True") {
                    alert("操作成功！");
                    _this.parent().parent().remove();
                }
                else { alert("操作失败！"); }
            });
        } else { return false;}
    });
 
    $(".adddialog,.editbtn,.belongbtn,.detailbtn").live("click", function () {
        
        var PageIndex = $(this).attr("page");
        var toUrl = $(this).attr("url");
        var Id = $(this).attr("ref");
        
        var TabTitle = $(this).attr("title");
        var $dialog = $("#adddialog");

        $dialog.html("");
        $dialog.dialog("option", "title", TabTitle);
        $dialog.dialog("open");
        $.get(toUrl, { Id: Id, PageIndex: PageIndex }, function (d) { $dialog.html(d); });
    });
    $(".checked").live("click", function () {
        var _this = $(this);
        var Id = _this.attr("ref");
        var CheckUrl = _this.attr("url");
        var ReturnUrl = _this.attr("ReturnUrl");
        $.post(CheckUrl, { Id: Id }, function (d) {
            if (d == "True") {
                alert("提交成功！");
                if (ReturnUrl == null || ReturnUrl == "" || ReturnUrl == undefined) { window.location.reload(); }
                else { window.location.href = ReturnUrl; }
            }
            else { alert("网络问题！"); }
        });
    });
    $(".submitbtn").live("click", function () {
        $(".form").submit();
    });


});

function get_cookie(Name) {
    var search = Name + "="//查询检索的值
    var returnvalue = "";//返回值
    if (document.cookie.length > 0) {
        sd = document.cookie.indexOf(search);
        if (sd != -1) {
            sd += search.length;
            end = document.cookie.indexOf(";", sd);
            if (end == -1)
                end = document.cookie.length;
            //unescape() 函数可对通过 escape() 编码的字符串进行解码。
            returnvalue = unescape(document.cookie.substring(sd, end))
        }
    }
    return returnvalue;
}