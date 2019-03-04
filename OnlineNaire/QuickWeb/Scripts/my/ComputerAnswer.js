//来源判断
function IsMobile() {
    var system = {};
    var p = navigator.platform;
    system.win = p.indexOf("Win") == 0;
    system.mac = p.indexOf("Mac") == 0;
    system.x11 = (p == "X11") || (p.indexOf("Linux") == 0);
    if ((system.win || system.mac || system.xll)) {//如果是电脑
        return false;
    } else {  //如果是手机
        return true;
    }
}


//1 是否显示题号
var isShowQuestionIndex = $("#form1").attr("data-isShowOptionIndex");
if (isShowQuestionIndex == 1) {//显示题目序号
    $(".questionnaire_question_item_name_index").show();

} else {
    $(".questionnaire_question_item_name_index").hide();
}
//1.1日期输入处理
$("input[type=text][data-type=8]").attr("onfocus", "WdatePicker({dateFmt: 'yyyy-MM-dd'})");
$("input[type=text][data-type=9]").attr("onfocus", "WdatePicker({dateFmt: 'yyyy-MM-dd HH:mm'})");



//2 选择题去除最后一个选项的底边
var $options = $(".questionnaire_question_item_option");
for (var i = 0; i < $options.length; i++) {
    $options.eq(i).children(".questionnaire_question_item_option_option").last().css("border-bottom", "initial");
}
//3 设置单选题、多选题 选项文字的宽度
optionLabelWidth();
$(window).resize(function () {
    optionLabelWidth();
});
function optionLabelWidth() {
    var $optinLabels = $(".questionnaire_question_item_option_option_label");
    for (var i = 0; i < $optinLabels.length; i++) {
        var $cur = $optinLabels.eq(i);
        var $parent = $cur.parent();
        var width = $parent.css("width").replace("px", "") - 40;
		//$cur.css("width", width + "px");
		$cur.css({ "cssText": 'min-width:' + width+'px !important' });
    }
}

//4 二级下拉菜单 第一级菜单选择
$(".firstLevelSelect").change(function () {
    var data = $(this).val();
    var secondLevelSelectData = $("#secondLevelSelectData_" + data).val().split(",");
    var $secondSelect = $(this).next();
    $secondSelect.html("");
    var s = "<option value='0'>--请选择--</option>";
    for (var i = 0; i < secondLevelSelectData.length; i++) {
        s += "<option value='" + secondLevelSelectData[i] + "'>" + secondLevelSelectData[i] + "</option>";
    }
    $secondSelect.html(s);
});

//5 排序题处理
$(".paixuti :checkbox").click(function () {
    var questionid = $(this).attr("questionid");
    var $allCheckboxs = $("#questionItem_" + questionid).find(":checkbox");
    var maxIndex = 0;
    if ($(this).is(':checked')) {//选中
        for (var i = 0; i < $allCheckboxs.length; i++) {

            var nowV = $allCheckboxs.eq(i).val();
            if (nowV > maxIndex) {
                maxIndex = nowV;
            }
        }
        maxIndex++;
        $(this).val(maxIndex).prev().text(maxIndex);
        //  $(this).next().css("width",($(this).next().css("width").replace("px","")-10)+ "px");
    } else {//取消选中
        var curValue = $(this).val();
        $(this).val(0).prev().text("");
        for (var i = 0; i < $allCheckboxs.length; i++) {

            var nowV = $allCheckboxs.eq(i).val();
            if (nowV != 0 && nowV > curValue) {

                $allCheckboxs.eq(i).val(nowV - 1).prev().text(nowV - 1);
            }
        }
    }

});

//6初始化时，设置逻辑关联的问题先隐藏
$("div[hassettinglogicdiv=0],div[hassettinglogicdiv=1]").hide();

//7 显示隐藏逻辑关联题
$(":radio,:checkbox").click(function () {
    questionShowHide();
});
$(".questionnaire_question_item_xialacaidan select").change(function () {
    questionShowHide();
});

//逻辑条件显示或者隐藏
function questionShowHide() {
    var $allXialacaidan = $(".questionnaire_question_item_xialacaidan select");
    var allXialacaidanSelectedValue = "xl,";
    for (var i = 0; i < $allXialacaidan.length; i++) {
        allXialacaidanSelectedValue += $allXialacaidan.eq(i).val() + ",";
    }
    $logicSettingDiv = $("div[hassettinglogicdiv=0],div[hassettinglogicdiv=1]");
    for (var i = 0; i < $logicSettingDiv.length; i++) {
        var $cur = $logicSettingDiv.eq(i);
        var arrTiaoJian = $cur.attr("logic").split(",");
        if ($cur.attr("hassettinglogicdiv") == 0) {//并且关系
            var right = true;
            for (var j = 0; j < arrTiaoJian.length; j++) {
                var optionId = arrTiaoJian[j];

                if ($("input[value=" + optionId + "]").is(':checked') == false && allXialacaidanSelectedValue.indexOf("," + optionId + ",") <= 0) {
                    right = false;
                    break;
                }
            }
            if (right) {
                $cur.show();
            } else {
                $cur.hide();
                $cur.find("input:radio").attr('checked', false);
                $cur.find("input:checkbox").attr('checked', false);
                $cur.find("input[type=text],input[type=number],textarea").val("");


            }
        }

        if ($cur.attr("hassettinglogicdiv") == 1) {//或者关系
            var right = false;
            for (var j = 0; j < arrTiaoJian.length; j++) {
                var optionId = arrTiaoJian[j];
                if ($("input[value=" + optionId + "]").is(':checked') || allXialacaidanSelectedValue.indexOf("," + optionId + ",") > 0) {
                    right = true;
                    break;
                }
            }
            if (right) {
                $cur.show();
            } else {
                $cur.hide();
                $cur.find("input:radio").attr('checked', false);
                $cur.find("input:checkbox").attr('checked', false);
                $cur.find("input[type=text],input[type=number],textarea").val("");
            }
        }

    }
}

$("input,textarea").click(function () {
    var qid = $(this).attr("data-qid");
    $("#questionItem_" + qid).removeClass("errorDiv");
    $("#questionItem_" + qid).find("div[data-qid=" + qid + "]").text("");
});
$("select").change(function () {
    var qid = $(this).attr("data-qid");
    $("#questionItem_" + qid).removeClass("errorDiv");
    $("#questionItem_" + qid).find("div[data-qid=" + qid + "]").text("");
});

//8.获取验证码
$(".validateGetSpan").click(function () {
    $(this).parent().hide();
    $(this).parent().next().show();
    changeValidateCode();
});
//9 更新验证码
$("#yzmimg").click(function () {
    changeValidateCode();
});
//10 获取验证码
function changeValidateCode() {
    $("#yzmimg").attr("src", "/Wenjuan/GetValidateCode" + "?id=" + new Date().getTime());
}

//11.答题进度
//答题进度
$("input,select").change(function () {
    answerProcess(this);
});
//文本变化事件
$('textarea').bind('input propertychange', 'textarea', function () {
    answerProcess(this);
});
//文本变化事件
$('input').bind('input propertychange', 'input', function () {
    answerProcess(this);
});
function answerProcess(objs) {
    debugger;
    var $clickThis = $(objs);//当前选项
    var currentQuestionId = $clickThis.attr("data-qid");//当前问题ID
    //所有可见的问题
    var $allQuestion = $(".questionnaire_question_item");

    var totalCount = 0;
    var errorCount = 0;
    for (var i = 0; i < $allQuestion.length; i++) {
        var $cur = $allQuestion.eq(i);

        if ($cur.hasClass("isMustInputDiv") == false) {//非必填题 不做验证
            continue;
        }
        if ($cur.css("display") == "none") {//已经隐藏的逻辑题不做处理
            continue;
        }
        var questionItemId = $cur.attr("id").replace("questionItem_", "");
        totalCount++;
        if ($cur.hasClass("danxuanti")) {
            var radio = $cur.find(":radio:checked");
            if (radio.length == 0) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_danxuan_errorInfo").text("请选择");
                }

            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_danxuan_errorInfo").text("");

            }
        }
        if ($cur.hasClass("duoxuanti")) {
            var checkbox = $cur.find(":checkbox:checked");
            var max = $cur.attr("max");
            var min = $cur.attr("min");
            if (min != 0 && checkbox.length < min) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("最少选择" + min + "个");
                }

            }
            else if (max != 0 && checkbox.length > max) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("最多选择" + max + "个");
                }


            }
            else if (checkbox.length == 0) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("请选择");
                }
            }
            else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("");
            }

        }

        if ($cur.hasClass("paixuti")) {

            if ($cur.find(":checkbox:checked").length != $cur.find(":checkbox").length) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_paixu_errorInfo").text("还有没有排序的选项");
                }
            } else {
                if (currentQuestionId == questionItemId) {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_paixu_errorInfo").text("");
                }
            }
        }


        if ($cur.hasClass("tiankongti")) {

            var $tk = $cur.find("input,textarea");
            var value = $tk.val();
            var validatedPass = false;;
            var errInfo = "";
            if (value == "" || value == null) {
                validatedPass = false;
                errInfo = "请填空";
            } else {
                var tianKongType = $tk.attr("data-type");
                validatedPass = true;
                errInfo = "";
                if (tianKongType == 3) {//数字
                    var min = $tk.attr("data-min");
                    min = parseInt(min);
                    var max = $tk.attr("data-max");
                    max = parseInt(max);
                    value = parseInt(value);
                    var diget = $tk.attr("data-diget");
                    if (min != 0 || max != 0) {
                        var r = /^\d+$/;　　//整数
                        if (r.test(value)) {
                            if (value > max || value < min) {
                                validatedPass = false;
                                errInfo = "最小值" + min + ",最大值" + max;
                            } else {
                                validatedPass = true;
                                errInfo = "";
                            }

                        } else {
                            validatedPass = false;
                            errInfo = "请输入整数";
                        }
                    } else {
                        validatedPass = true;
                        errInfo = "";
                    }


                }
                if (tianKongType == 4) {
                    var r = /(^1[3|4|5|6|7|8]\d{9}$)|(^09\d{8}$)/;//手机号
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的手机号码";
                    }
                }
                if (tianKongType == 5) {
                    var r = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;//邮箱
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的邮箱格式";
                    }
                }
                if (tianKongType == 6) {
                    var r = /^\d{15}(\d{2}[A-Za-z0-9])?$/;//
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的身份证号";
                    }
                }
                if (tianKongType == 7) {
                    var r = /^(http:\/\/){0,1}[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;//
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的网址";
                    }
                }

            }

            if (validatedPass) {
                $cur.find(".questionnaire_question_item_tiankong_errorInfo").text("");
                $cur.removeClass("errorDiv");
            } else {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_tiankong_errorInfo").text(errInfo);
                }
            }

        }


        if ($cur.hasClass("juzhenti")) {
            var juzhenType = $cur.attr("juzhenType");
            var col = $cur.attr("col");
            var row = $cur.attr("row");
            if (juzhenType == 1) {//单选矩阵
                var radio = $cur.find(":radio:checked").length;
                if (radio < col) {
                    errorCount++;
                    if (currentQuestionId == questionItemId) {
                        $cur.addClass("errorDiv");
                        $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("请完成所有的选择项");

                    }

                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");

                }
            }
            if (juzhenType == 2) {//多选矩阵
                var $trs = $cur.find("tr");
                var rowSelect = 0;
                for (var j = 0; j < $trs.length; j++) {
                    var $tr = $trs.eq(j);
                    if ($tr.find(":checkbox:checked").length > 0) {//如果每一行都有选中项
                        rowSelect++;
                    }
                }
                if (rowSelect < col) {
                    errorCount++;
                    if (currentQuestionId == questionItemId) {
                        $cur.addClass("errorDiv");
                        $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("请完成所有的选择项");

                    }

                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");

                }
            }
            if (juzhenType == 3) {//填空矩阵 
                var all = col * row;//总共的填空项
                var $inputs = $cur.find("input");
                var rowSelect = 0;
                for (var j = 0; j < $inputs.length; j++) {
                    var v = $inputs.eq(j).val();
                    if (v != "" && v != null) {
                        rowSelect++;
                    }
                }
                if (rowSelect < all) {
                    errorCount++;
                    if (currentQuestionId == questionItemId) {
                        $cur.addClass("errorDiv");
                        $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("请完成所有的输入项");

                    }

                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");

                }
            }
            if (juzhenType == 4) {//数字矩阵
                var all = col * row;//总共的填空项
                var min = $cur.attr("data-min");
                var max = $cur.attr("data-max");
                min = parseInt(min);
                max = parseInt(max);

                var $inputs = $cur.find("input");
                var rowSelect = 0;
                for (var j = 0; j < $inputs.length; j++) {
                    var v = $inputs.eq(j).val();
                    var numberValue = parseInt(v);//当前数字
                    if (v != "" && v != null) {
                        if ((min == 0 && max == 0) || ((min != 0 || max != 0) && numberValue >= min && numberValue <= max)) {
                            rowSelect++;
                        }

                    }
                }
                if (rowSelect < all) {
                    errorCount++;
                    if (currentQuestionId == questionItemId) {
                        $cur.addClass("errorDiv");
                        var errorInfo = "请完成所有的输入项";
                        if (min != 0 || max != 0) {
                            errorInfo = "请完成所有的输入项,最小值" + min + "最大值" + max;
                        }
                        $cur.find(".questionnaire_question_item_juzhen_errorInfo").text(errorInfo);
                    }

                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");
                }
            }

        }

        if ($cur.hasClass("xialacaidanti")) {
            var v = $cur.find("select option:selected").val();
            if (v == 0) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_xialacaidan_errorInfo").text("请选择");
                }



            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_xialacaidan_errorInfo").text("");

            }
        }

        if ($cur.hasClass("erjixialacaidanti")) {
            var v = $cur.find("select:first option:selected").val();
            var v1 = $cur.find("select:last option:selected").val();
            if (v == 0 || v1 == 0) {
                errorCount++;
                if (currentQuestionId == questionItemId) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_erjixialacaidan_errorInfo").text("请选择");
                }

            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_erjixialacaidan_errorInfo").text("");

            }
        }
    }

    var bfb = Math.ceil((totalCount - errorCount) / totalCount * 100);
    if (bfb > 100) {
        bfb = 100;
    }
    $("#progressbarDiv").css("width", bfb + "%").html(bfb + "%");
}
//提交问卷
$(".btnConfirm").click(function () {

    $questions = $(".questionnaire_question_item");
    var $firstNotValidateQuestion = null;
    for (var i = 0; i < $questions.length; i++) {
        var $cur = $questions.eq(i);
        if ($cur.hasClass("isMustInputDiv") == false) {//非必填题 不做验证
            continue;
        }
        if ($cur.css("display") == "none") {//已经隐藏的逻辑题不做处理
            continue;
        }
        if ($cur.hasClass("danxuanti")) {
            var radio = $cur.find(":radio:checked");
            if (radio.length == 0) {
                $cur.addClass("errorDiv");
                $cur.find(".questionnaire_question_item_danxuan_errorInfo").text("请选择");

                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_danxuan_errorInfo").text("");

            }
        }
        //find 可以获取所有子元素 children只能获取单一层级的子元素
        if ($cur.hasClass("duoxuanti")) {

            var checkbox = $cur.find(":checkbox:checked");
            var max = $cur.attr("max");
            var min = $cur.attr("min");
            if (min != 0 && checkbox.length < min) {
                $cur.addClass("errorDiv");
                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
                $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("最少选择" + min + "个");
            }
            else if (max != 0 && checkbox.length > max) {
                $cur.addClass("errorDiv");
                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
                $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("最多选择" + max + "个");

            }
            else if (checkbox.length == 0) {
                $cur.addClass("errorDiv");
                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
                $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("请选择");

            }
            else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_duoxuan_errorInfo").text("");
            }

        }

        if ($cur.hasClass("paixuti")) {

            if ($cur.find(":checkbox:checked").length != $cur.find(":checkbox").length) {
                $cur.addClass("errorDiv");
                $cur.find(".questionnaire_question_item_paixu_errorInfo").text("还有没有排序的选项");

                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_paixu_errorInfo").text("");

            }
        }


        if ($cur.hasClass("tiankongti")) {

            var $tk = $cur.find("input,textarea");
            var value = $tk.val();
            var validatedPass = false;;
            var errInfo = "";
            if (value == "" || value == null) {
                validatedPass = false;
                errInfo = "请填空";
            } else {
                var tianKongType = $tk.attr("data-type");
                validatedPass = true;
                errInfo = "";
                if (tianKongType == 3) {//数字
                    var min = $tk.attr("data-min");
                    min = parseInt(min);
                    var max = $tk.attr("data-max");
                    max = parseInt(max);
                    value = parseInt(value);
                    var diget = $tk.attr("data-diget");
                    if (min != 0 || max != 0) {
                        var r = /^\d+$/;　　//整数
                        if (r.test(value)) {
                            if (value > max || value < min) {
                                validatedPass = false;
                                errInfo = "最小值" + min + ",最大值" + max;
                            } else {
                                validatedPass = true;
                                errInfo = "";
                            }

                        } else {
                            validatedPass = false;
                            errInfo = "请输入整数";
                        }
                    } else {
                        validatedPass = true;
                        errInfo = "";
                    }


                }
                if (tianKongType == 4) {
                    var r = /(^1[3|4|5|6|7|8]\d{9}$)|(^09\d{8}$)/;//手机号
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的手机号码";
                    }
                }
                if (tianKongType == 5) {
                    var r = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;//邮箱
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的邮箱格式";
                    }
                }
                if (tianKongType == 6) {
                    var r = /^\d{15}(\d{2}[A-Za-z0-9])?$/;//
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的身份证号";
                    }
                }
                if (tianKongType == 7) {
                    var r = /^(http:\/\/){0,1}[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;//
                    if (r.test(value)) {
                        validatedPass = true;
                        errInfo = "";
                    } else {
                        validatedPass = false;
                        errInfo = "请输入正确的网址";
                    }
                }

            }

            if (validatedPass) {
                $cur.find(".questionnaire_question_item_tiankong_errorInfo").text("");
                $cur.removeClass("errorDiv");
            } else {
                $cur.addClass("errorDiv");
                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
                $cur.find(".questionnaire_question_item_tiankong_errorInfo").text(errInfo);
            }

        }


        if ($cur.hasClass("juzhenti")) {
            var juzhenType = $cur.attr("juzhenType");
            var col = $cur.attr("col");
            var row = $cur.attr("row");
            if (juzhenType == 1) {//单选矩阵
                var radio = $cur.find(":radio:checked").length;
                if (radio < col) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("请完成所有的选择项");

                    if ($firstNotValidateQuestion == null) {
                        $firstNotValidateQuestion = $cur;
                    }
                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");

                }
            }
            if (juzhenType == 2) {//多选矩阵
                var $trs = $cur.find("tr");
                var rowSelect = 0;
                for (var j = 0; j < $trs.length; j++) {
                    var $tr = $trs.eq(j);
                    if ($tr.find(":checkbox:checked").length > 0) {//如果每一行都有选中项
                        rowSelect++;
                    }
                }
                if (rowSelect < col) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("请完成所有的选择项");

                    if ($firstNotValidateQuestion == null) {
                        $firstNotValidateQuestion = $cur;
                    }
                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");

                }
            }
            if (juzhenType == 3 ) {//填空矩阵 
                var all = col * row;//总共的填空项
                var $inputs = $cur.find("input");
                var rowSelect = 0;
                for (var j = 0; j < $inputs.length; j++) {
                    var v = $inputs.eq(j).val();
                    if (v != "" && v != null) {
                        rowSelect++;
                    }
                }
                if (rowSelect < all) {
                    $cur.addClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("请完成所有的输入项");

                    if ($firstNotValidateQuestion == null) {
                        $firstNotValidateQuestion = $cur;
                    }
                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text("");

                }
            }
            if (juzhenType == 4) {//数字矩阵
                var all = col * row;//总共的填空项
                var min = $cur.attr("data-min");
                var max = $cur.attr("data-max");
                min = parseInt(min);
                max = parseInt(max);
                var $inputs = $cur.find("input");
                var rowSelect = 0;
                for (var j = 0; j < $inputs.length; j++) {
                    var v = $inputs.eq(j).val();
                    var numberValue = parseInt(v);//当前数字
                    if (v != "" && v != null) {
                        if ((min == 0 && max == 0) || ((min != 0 || max != 0) && numberValue >= min && numberValue <= max)) {
                            rowSelect++;
                        }

                    }
                }
                if (rowSelect < all) {
                    $cur.addClass("errorDiv");
                    var errorInfo = "请完成所有的输入项";
                    if (min != 0 || max != 0) {
                        errorInfo = "请完成所有的输入项,最小值" + min + "最大值" + max;
                    }
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text(errorInfo);
                    if ($firstNotValidateQuestion == null) {
                        $firstNotValidateQuestion = $cur;
                    }
                }
                else {
                    $cur.removeClass("errorDiv");
                    $cur.find(".questionnaire_question_item_juzhen_errorInfo").text(errorInfo);
                }
            }


        }

        if ($cur.hasClass("xialacaidanti")) {
            var v = $cur.find("select option:selected").val();
            if (v == 0) {
                $cur.addClass("errorDiv");
                $cur.find(".questionnaire_question_item_xialacaidan_errorInfo").text("请选择");

                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_xialacaidan_errorInfo").text("");

            }
        }

        if ($cur.hasClass("erjixialacaidanti")) {
            var v = $cur.find("select:first option:selected").val();
            var v1 = $cur.find("select:last option:selected").val();
            if (v == 0 || v1 == 0) {
                $cur.addClass("errorDiv");
                $cur.find(".questionnaire_question_item_erjixialacaidan_errorInfo").text("请选择");

                if ($firstNotValidateQuestion == null) {
                    $firstNotValidateQuestion = $cur;
                }
            } else {
                $cur.removeClass("errorDiv");
                $cur.find(".questionnaire_question_item_erjixialacaidan_errorInfo").text("");

            }
        }
    }

    if ($firstNotValidateQuestion != null) {
        $firstNotValidateQuestion.get(0).scrollIntoView();
    } else {
        //提交
        $(".btnConfirm").attr("disabled", "disabled").text("正在提交");
        $.post("/Wenjuan/Submit", $("#form1").serialize(), function (data) {
            if (data == "ok") {
                $(".btnConfirm").text("提交成功正在跳转");
                var code = $("input[name=questionnaireCode]").val();
                window.location.href = "/Wenjuan/GoingResult/" + code;
            }
            else {
                $(".btnConfirm").removeAttr("disabled").text("提交");
                $("#validateCode").val("");
                changeValidateCode();
                alert(data);
            }

        });
    }
});
