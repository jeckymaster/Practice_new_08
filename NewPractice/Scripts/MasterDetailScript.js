var respData;

$(document).ready(function ()
{
    $("#BtnDelete").hide();
})

function GetItems()
{
    debugger
    var obj = {};
    
    //debugger
    $.ajax({
        //url: '../MasterDetail/getItems',
        url: '../MasterDetail/LikeFuncFromItemMaster',

        type: "POST",
        data: obj,
        success: function (result)
        {
            debugger
            //var ResponseData = JSON.parse(result);
            ResponseData = JSON.parse(result);


        }
    })

}

function doFunction()
{
    debugger
    var obj = { };
    var Irate = $("#ddlItem").val();
    var i = Gridview1.SelectedRow.Cells[1].Text;

    obj.Irate = Irate;

    //debugger
    $.ajax({
        //url: '../MasterDetail/getItems',
        url: '../MasterDetail/GetRate',
        
        type: "POST",
        data: obj,
        success: function (result)
        {
            debugger
            
            //respData = JSON.parse(result);
            //$('#txtRate').val(respData["ItemRate"]);
            $('#txtRate').val(result);

        }
    })
}

function GetTotal()
{
    debugger
    var obj = {};
    var ItemQty = $("#txtItemQty").val();
    var ItemRate = $("#txtRate").val();

    obj.Qty = ItemQty;
    obj.Rate = ItemRate;

    //debugger
    $.ajax({
        
        url: '../MasterDetail/GetTotal',
        type: "POST",
        data: obj,
        success: function (result)
        {
            debugger
            //var ResponseData = JSON.parse(result);
            //ResponseData = JSON.parse(result);
            ResponseData = result;
            $("#txtAmount").val(ResponseData);

        }
    })
}

function AddNewRow()
{
    debugger
    $("#BtnDelete").show();
    var tblBody = $("#tblItemDetails").find("tbody");
    var tblLastRow = tblBody.find("tr:last");
    var tblClone = tblLastRow.clone().find("input[type=text]").val('').end();
    tblLastRow.after(tblClone);
    $("#BtnDelete").hide();
}

function DeleteRow(e)
{
    $("#BtnDelete").closest("tr").remove();
}