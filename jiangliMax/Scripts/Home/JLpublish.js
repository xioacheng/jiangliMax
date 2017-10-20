
//查找应诉人弹出框
function yingsuren() {
    $("#yingsurenModal").modal();
}
function onkey() {
    
}
//获取应诉人数据
yingsurendata = {
    payinput:function(){
        alert("123");
    },
    check: function () {
        var title = $("#casetitle").val().replace(/(^\s*)|(\s*$)/g, "");
        var yingsuren = $("#yingsurenname").val().replace(/(^\s*)|(\s*$)/g, "");
        var paynum = $("#paynum").val().replace(/(^\s*)|(\s*$)/g, "");
        var content = $("#content7").val().replace(/(^\s*)|(\s*$)/g, "");
        if (title == "" || paynum == "") {
            if (title == "") {
                $("#titleerror").css("display","block");
            }
            else {
                $("#titleerror").css("display", "none");
            }
            if (paynum == "") {
                $("#payerror").css("display", "block");
            }
            else {
                $("#payerror").css("display", "none");
            }
            //if (content=="") {
            //    $("#contenterror").css("display", "block");
            //}
            //else {
            //    $("#contenterror").css("display", "none");
            //}
            return false;
        }
        else {
            return true;
        }
    },
    //站内查找用户信息
    getName: function () {
        //获取文本框的输入内容
        var searchname = $("#searchname").val();
        var searchphone = $("#searchphone").val();
        var searchemail = $("#searchemail").val();
        //使用ajax进行和后台交互
        $.ajax({
            url: '/search/userlist',
            data: {
                searchname: searchname,
                searchphone: searchphone,
                searchemail: searchemail,
            },
            type: "post",
            success: function (data) {
                $("#YSRsearchtable tbody").html("");
                //将获取到的数据追加到表格中
                $.each(data, function (index, item) {
                    var userinfo = item.userid + "&" + item.username;
                    $("#YSRsearchtable tbody").append("<tr><td>" + item.username +
                        '</td><td>' + item.userphone + '</td><td>' + item.useremail +
                        "</td><td><input type='radio'name='RSRradio' value=" + userinfo + " ></td></tr>");
                })
            }
        });
    },
    //确认应诉人信息
    YSRConfirm: function () {
        //获取文本框的值
        var searchname = $("#searchname").val();
        var searchphone = $("#searchphone").val();
        var searchemail = $("#searchemail").val();
        var searchuser = $("input[name='RSRradio']:checked").val();//站内检索到的用户

        var inputname = $("#inputname").val();//用户输入的用户名
        var inputphone = $("#inputphone").val();//用户输入的手机号
        var inputemail = $("#inputemail").val();//用户输入的邮箱
        var inputaddress = $("#inputaddress").val();//用户输入的地址
        var inputexplain = $("#inputexplain").val();//用户输入的其他说明
        $("#yingsurenphone").val(inputphone);
        $("#ringsurenemail").val(inputemail);
        $("#yingsurenaddress").val(inputaddress);
        $("#yingsurenexplain").val(inputexplain);
        //来自站内检索的信息
        if (searchuser != null && searchuser!="") {
            var arr = searchuser.split('&');
            $("#yingsurenid").val(arr[0]);
            $("#yingsurenname").val(arr[1]);
        }
        //来自手动输入的信息
        else if (inputname != null && inputname != "") {
            $("#yingsurenid").val("");
            $("#yingsurenname").val(inputname);
            
        }
    },
}