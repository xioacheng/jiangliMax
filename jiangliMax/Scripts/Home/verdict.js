function keyChange() {
    $("#doublePayNum").val($("#paySureNum").val() * 2);
}
function verdictCheak() {
    var jie = $("#paySureNum").val();
    var reg = /^\d+(\.\d+)?$/;
    if (reg.test(jie) == false) {
        $("#jine").css('display', 'block');
        return false;
    }
    else {
        return true;
    }
}

$(document).ready(function () {
    $('#rootwizard').bootstrapWizard({
        onNext: function () {
            var temp = $("input[name='pctype']:checked").val();
            //互不赔偿
            if (temp == 2) {
                $("#PC1").css('display', 'none');
                $("#PC2").css('display', 'block');
                $("#paySure").css('display', 'none');
                $("#nopayDegree").css('display', 'none');
                $("#paySureNum").val("0");
            }
                //必须赔偿
            else {
                $("#PC2").css('display', 'none');
                $("#PC1").css('display', 'block');
                $("#paySure").css('display', 'block');
                $("#nopayDegree").css('display', 'block');
            }
        },
        onTabClick: function (tab, navigation, index) {
            return false;
        }
    });
});
function outnoPay() {
    var num = $("#ex1").val();
    $("#noPay").val(num);
}
function outdoublePay() {
    var num = $("#ex2").val();
    $("#doublePay").val(num);
}
function Other1Pay() {
    var num = $("#ex3").val();
    $("#OtherNum1").val(num);
}
function Other2Pay() {
    var num = $("#ex4").val();
    $("#OtherNum2").val(num);
}
function Other3Pay() {
    var num = $("#ex5").val();
    $("#OtherNum3").val(num);
}
function nopayChange() {
    var num = $("#noPay").val();
    num = num.trim();
    if (num == '') {
        return false;
    }
    if (isNotANumber(num)) {
        var number = parseFloat(num);
        if (number >= -10.0 && number <= 10.0) {
            $("#noPay").val(number);
        }
        else {
            alert("满意度范围为-10到10.");
            $("#noPay").val(0);
        }
    }
    else {
        alert("满意度范围为-10到10.");
        $("#noPay").val(0);
    }
}
function doublepayChange() {
    var num = $("#doublePay").val();
    num = num.trim();
    if (num == '') {
        return false;
    }
    if (isNotANumber(num)) {
        var number = parseFloat(num);
        if (number >= -10.0 && number <= 10.0) {
            $("#doublePay").val(number);
        }
        else {
            alert("满意度范围为-10到10.");
            $("#doublePay").val(0);
        }
    }
    else {
        alert("满意度范围为-10到10.");
        $("#doublePay").val(0);
    }
}
function Other1Change() {
    var num = $("#OtherNum1").val();
    num = num.trim();
    if (num == '') {
        return false;
    }
    if (isNotANumber(num)) {
        var number = parseFloat(num);
        if (number >= -10.0 && number <= 10.0) {
            $("#OtherNum1").val(number);
        }
        else {
            alert("满意度范围为-10到10.");
            $("#OtherNum1").val(0);
        }
    }
    else {
        alert("满意度范围为-10到10.");
        $("#OtherNum1").val(0);
    }
}
function Other2Change() {
    var num = $("#OtherNum2").val();
    num = num.trim();
    if (num == '') {
        return false;
    }
    if (isNotANumber(num)) {
        var number = parseFloat(num);
        if (number >= -10.0 && number <= 10.0) {
            $("#OtherNum2").val(number);
        }
        else {
            alert("满意度范围为-10到10.");
            $("#OtherNum2").val(0);
        }
    }
    else {
        alert("满意度范围为-10到10.");
        $("#OtherNum2").val(0);
    }
}
function Other3Change() {
    var num = $("#OtherNum3").val();
    num = num.trim();
    if (num == '') {
        return false;
    }
    if (isNotANumber(num)) {
        var number = parseFloat(num);
        if (number >= -10.0 && number <= 10.0) {
            $("#OtherNum3").val(number);
        }
        else {
            alert("满意度范围为-10到10.");
            $("#OtherNum3").val(0);
        }
    }
    else {
        alert("满意度范围为-10到10.");
        $("#OtherNum3").val(0);
    }
}
function isNotANumber(inputData) {
    if (parseFloat(inputData).toString() === "NaN") {
        return false;
    } else {
        return true;
    }
}
$(document).ready(function () {
    $('#ex1').slider({});
    $('#ex2').slider({});
    $('#ex3').slider({});
    $('#ex4').slider({});
    $('#ex5').slider({});
    $("#noPay").val(0);
    $("#doublePay").val(0);
    $("#OtherNum1").val(0);
    $("#OtherNum2").val(0);
    $("#OtherNum3").val(0);
});