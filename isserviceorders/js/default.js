//This is a custom js file required for enabling certain fields in Default page

$(document).ready(function () {

    serviceType();

    $("#ddlService").change(function () {
        serviceType();
    });

    $("#ddladdSubService").change(function () {
        AddSubType();
    });

    $("#ddlVoiceMailBox").change(function () {
        dropDownEnableFields("#ddlVoiceMailBox", "#div_UserEmailAlias");
    });
    $("#ddllongDistanceDial").change(function () {
        dropDownEnableFields("#ddllongDistanceDial", "#div_AuthCode");
    });
    $("#ddlCallPickUpGroup").change(function () {
        dropDownEnableFields("#ddlCallPickUpGroup", "#div_CallGroupPhoneNumber");
    });
    $("#ddlExistingJack").change(function () {
        dropDownEnableFields("#ddlExistingJack", "#div_ExistingJack");
    });

    $("#ddlMovingType").change(function () {
        if ($("#ddlMovingType").find("option:selected").val() == "Phone Line") {
            $("#div_PhoneType").show();
        }
        else {
            $("#div_PhoneType").hide();
        }
    });
});

function serviceType() {

    if ($("#ddlService").find("option:selected").val() === "Change") {
        ChangeService();
        $("#ddlExistingJack").attr("disabled", false);

    } else if ($("#ddlService").find("option:selected").val() === "Move") {
        MoveService();
        $("#ddlExistingJack").attr("disabled", false);

    } else if ($("#ddlService").find("option:selected").val() === "Disconnect") {
        DisconnectService();
        $("#ddlExistingJack").attr("disabled", false);

    }
    else if ($("#ddlService").find("option:selected").val() === "Activation") {
        ActivationService();

        $("#ddlExistingJack").attr("disabled", true);
    }
    else if ($("#ddlService").find("option:selected").val() === "Labor") {
        LaborService();
        $("#ddlExistingJack").attr("disabled", false);

    }
    else {
        AddService();
        $("#ddlExistingJack").attr("disabled", false);

    }

}
function AddService() {
    $(".ChangeService").hide();
    $(".MoveService").hide();
    $(".DisconnectService").hide();
    $(".LaborService").hide();
    $(".ActivationService").hide();
    $(".AddService").show();
    AddSubType();
}

function ChangeService() {
    $(".AddService").hide();
    $(".MoveService").hide();
    $(".LaborService").hide();
    $(".ActivationService").hide();
    $(".ChangeService").show();
    $(".DisconnectService").show();
}

function MoveService() {

    $(".AddService").hide();
    $(".ChangeService").hide();
    $(".DisconnectService").hide();
    $(".LaborService").hide();
    $(".ActivationService").hide();
    $(".MoveService").show();

    if ($("#ddlMovingType").find("option:selected").val() == "Phone Line") {
        $("#div_PhoneType").show();
    }
    else {
        $("#div_PhoneType").hide();
    }
}

function DisconnectService() {
    $(".AddService").hide();
    $(".ChangeService").hide();
    $(".MoveService").hide();
    $(".LaborService").hide();
    $(".ActivationService").hide();
    $(".DisconnectService").show();
}

function ActivationService() {
    $(".AddService").hide();
    $(".DisconnectService").hide();
    $(".ChangeService").hide();
    $(".MoveService").hide();
    $(".LaborService").hide();
    $(".ActivationService").show();

}

function LaborService() {
    $(".AddService").hide();
    $(".DisconnectService").hide();
    $(".ChangeService").hide();
    $(".MoveService").hide();
    $(".ActivationService").hide();
    $(".LaborService").show();
}

function dropDownEnableFields(ddlId, elementId) {

    if ($(ddlId).find("option:selected").val() === "True") {
        $(elementId).show();
    }
    else {
        $(elementId).hide();
    }
}

function enableVoiceSubType() {

    $(".DataService").hide();
    $(".VoiceService").show();
    $(".AnalogService").show();
    // $("ddlVoiceMailBox, ddllongDistanceDial, ddlCallPickUpGroup")[0].selectedIndex = 1;
    dropDownEnableFields("#ddlVoiceMailBox", "#div_UserEmailAlias");
    dropDownEnableFields("#ddllongDistanceDial", "#div_AuthCode");
    dropDownEnableFields("#ddlCallPickUpGroup", "#div_CallGroupPhoneNumber");
}
function enableDataSubType() {

    $(".AnalogService").hide();
    $(".VoiceService").hide();
    $(".DataService").show();
    dropDownEnableFields("#ddlExistingJack", "#div_ExistingJack");
}
function enableAnalogSubType() {

    $(".DataService").hide();
    $(".VoiceService").hide();
    $(".AnalogService").show();
    dropDownEnableFields("#ddllongDistanceDial", "#div_AuthCode");

}

function AddSubType() {


    if ($("#ddladdSubService").find("option:selected").val() === "Voice") {
        enableVoiceSubType();
    }
    else if ($("#ddladdSubService").find("option:selected").val() === "Analog") {
        enableAnalogSubType();
    } else {
        enableDataSubType();
    }
}

//$(function () {
//    //debugger;
//    $.getJSON("https://cf3.umkc.edu/intapps/services/photo-service/photo.cfc?emplid=16208639&method=getProfileImgJsonP&callback=?",
//        function (data) {
//            var imag = "<img "
//                + "src='" + "data:image/jpg;base64,"
//                + data.image + "'/>";
//            $("#divImg").innerHTML = imag;
//            // data.image
//        });
//});




