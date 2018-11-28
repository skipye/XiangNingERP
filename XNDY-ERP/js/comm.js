$(function () {
    //先定义dialog
    $("#adddialog").dialog({
        autoOpen: false,
        width: 750,
        height: 620,
        show: 'scale',
        hide: "explode"
    });
    $("#dialog_img").dialog({
        autoOpen: false,
        width: 450,
        height: 300,
        show: 'scale',
        hide: "explode"
    });


    //$("tr:odd").live("addClass","odd");
    //$("tr:even").live("addClass","even");

    $(":text[ref='datepicker']").datepicker({
        changeYear: true,
        changeMonth: true,
        showButtonPanel: true
    });

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
                    alert("删除成功！");
                    window.location.reload();
                }
                else { alert("删除失败！"); }
            });
        }
        else { alert("请先去选中！"); }
    });
    $(".delone").live("click",function () {
        var _this = $(this);
        var Id = _this.attr("ref");
        var DelUrl = _this.attr("url");
        $.post(DelUrl, { Id: Id }, function (d) {
            if (d == "True") {
                alert("删除成功！");
                _this.parent().parent().remove();
            }
            else { alert("删除失败！"); }
        });
    });
  
    $(".adddialog,.editbtn,.belongbtn").live("click", function () {
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
        $.post(CheckUrl, { Id: Id }, function (d) {
            if (d == "True") {
                alert("审核成功！");
                window.location.reload();
            }
            else { alert("网络问题！"); }
        });
    });
    $(".submitbtn").live("click", function () {
        $(".form").submit();
    });


});