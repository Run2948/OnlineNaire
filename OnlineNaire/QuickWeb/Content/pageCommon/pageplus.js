//加载分页条
function LoadPageBar(callURL, Pagination, currentPage) {
    $.get(callURL, $("#formSearch").serialize(), function (html) {
        $("#loanTable").html(html).attr("serach", "isSearch");
        var rows = $("#loanTable #hidden_rows").val();
        var pageSize = $("#loanTable #hidden_pageSize").val();
        var pageCount = $("#loanTable #hidden_pageCount").val();
        var pageIndex = $("#loanTable #hidden_pageIndex").val();
        $("#pageSetting").html("每页" + pageSize + "条记录，共有" + pageCount + "页，共有" + rows + "条记录");
        //分页，PageCount是总条目数，这是必选参数，其它参数都是可选
        $("#" + Pagination).pagination(rows, {
            callback: PageCallback,
            prev_text: '上一页',       //上一页按钮里text
            next_text: '下一页',       //下一页按钮里text
            items_per_page: pageSize,  //显示条数
            num_display_entries: 5,    //连续分页主体部分分页条目数
            current_page: currentPage,   //当前页索引
            num_edge_entries: 5,      //两侧首尾分页条目数
            callURL: callURL
        });
    });

}
//翻页调用
function PageCallback(index, callURL) {
    WriteGoBar(callURL);
    if ($("#loanTable").attr("serach") == "isSearch") {
        $("#loanTable").removeAttr("serach");
        return;//没有点击分页条
    }
    index++;
    $("#pageIndex").val(index);
    InitTable(callURL);
}
//请求数据
function InitTable(callURL) {
    $.get(callURL, $("#formSearch").serialize(), function (html, status) {
        $("#loanTable").html(html);

    });
}
//追加跳转功能
function WriteGoBar(callURL) {
    $("#Pagination div").children("#gopage,#goskip").remove();
    $("#Pagination div").append("<input type='text' id='gopage'/><a id='goskip'>跳转</a>");
    $("#goskip").click(function () {
        var page = $("#gopage").val();
        if (!/^[1-9]([0-9])*$/.test(page)) {
            alert("请输入数字或者正数!");
            $("#gopage").val("");
            return false;
        }
        if (page == "") {
            alert("请输入数字!");
            $("#gopage").val("");
            return false;
        }
        $("#pageIndex").val(page);
        $("#loanTable").attr("serach", "isSearch");
        LoadPageBar(callURL, "Pagination", page - 1);

    });
}
