
var GrdOrderDetails = "GrdOrderDetails";
var GrdOrderHistory = "GrdOrderHistory";
var ResponseData;
$(document).ready(function ()
{
    AddRow();
    //OrderDetail();
    //BindOrderDetails();
    
    OrderHistory();
    BindOrderDetailsHistory();
    //SaveFreezeDepartments();
})

function SaveFreezeDepartments()
{
    debugger
    var obj = {};

    var FinalFreezeDepartments = w2ui['GrdOrderDetails'].getChanges();

    obj.M = JSON.stringify(FinalFreezeDepartments);

    //debugger
    $.ajax({
        url: '../Item/LikeFuncFromItemMaster',

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

function OrderDetail()
{
    //debugger
    if (w2ui.hasOwnProperty(GrdOrderDetails) == true)
    {
        w2ui[GrdOrderDetails].destroy();
    }

   
    $('#DivListOrderDetails').w2grid({
        name: GrdOrderDetails,
        recid: 'OrderDetailsNo',
        disableCVS: true, // very imp for Horizontal scroo problem. it skips header columns.

        show:
        {
            lineNumbers: true,
            footer: true,
            toolbar: true,
            toolbarSearch: false,
            toolbarInput: false,

        },
        toolbar:
        {
            right: '<div style="text-align:right;">List of order Details</div>'
        },

        columns:
            [
                { field: 'recid', text: 'OrderDetailsNo', size: '120px', sortable: true, resizable: true },
                { field: 'OrderNo', text: 'Order No', size: '120px', sortable: true, resizable: true },

                {
                    field: 'ItemName', text: 'Item', size: '120px', sortable: true, resizable: true, //editable: { type: 'select', items: Item },  //editable: { type: 'combo' },
                    editable: { type: 'combo', items: ResponseData, filter: true },

                    //caption: '<div><input id="txtitemname"  type="text" onChange="SaveFreezeDepartments(record)"></div>',
                    //render: function (record, index, column_index)
                    //{
                    //    debugger
                    //    var html = '';
                    //    for (var s in Item)
                    //    {
                    //        if (Item[s].id == this.SaveFreezeDepartments(event)) html = Item[s].text;
                    //    }
                    //    return html;

                        //var ReturnString = '<id="Renderchkisfrozen" type="combo" onClick = "SaveFreezeDepartments(record)">';

                        //return ReturnString;

                        //new w2field('combo', {
                        //    el: query('input[type=combo]')[0],
                        //    items: ReturnString,
                        //    match: 'contains',
                        //    markSearch: true
                        //})
                     
                },

                { field: 'ItemQty', text: 'Qty', size: '120px', sortable: true, resizable: true, editable: { type: 'text' },  },
                { field: 'ItemRate', text: 'Rate', size: '120px', sortable: true, resizable: true, editable: { type: 'text' },  },
                { field: 'TotalAmt', text: 'TotalAmt', size: '120px', sortable: true, resizable: true },

            ],
       
    });

    
    
}

function BindOrderDetails()
{
    //debugger
    $.ajax({
        type: "GET",
        url: "../item/ListData1",
        success: function (result)
        {
            var resp = JSON.parse(result);

            if (resp.Status == 'ERROR')
            {
                ShowMessage('Error while get rough stock valuation data', SwalTypes.error, resp.Data, 0);
            }
            else
            {
                debugger
                var data = JSON.parse(resp.Data);
                for (var i = 0; i < data.length; i++)
                {
                    data[i].SrNo = (i + 1);
                }

                w2ui[GrdOrderDetails].records = data;
                w2ui[GrdOrderDetails].reload();
                w2ui[GrdOrderDetails].unlock();

            }
        }
    })    

}


function BindDivListDataHistory1()
{
    debugger
    w2utils.settings.dataType = 'HTTPJSON';
    w2utils.settings.dateFormat = "yyyy-MM-dd";


    $('#DivListOrderDetailsHISTORY').w2grid({
        name: 'ListDataHistory',
        recid: 'OrderNo',
        recordHeight: 27,
        limit: 200,
        url: '../Item/GetOrderHistory',

        toolbar:
        {
            right: '<div style="text-align:left;"><span class="FAont Font20 Color_DarkGray">History of Orders</span></div>'
        },
        show: {
            footer: true,
            toolbar: true,
            // toolbarSearch: true,
            toolbarInput: false,
            lineNumbers: true,

        },

        columns:
            [
                { field: 'recid', caption: '', hidden: true },

                {
                    field: 'OrderDate', caption: 'Date', size: '120px', sortable: true, frozen: false, style: 'font-weight:bold;'
                },

                {
                    field: 'PartyCode', caption: 'party', size: '125px', sortable: true, style: 'font-weight:bold;',
                    //render: function (record, index, column_index)
                    //{
                    //    var html = w2utils.formatDate(record.FrozenDate, 'dd-MM-yyyy');
                    //    return html;
                    //}
                },

                {
                    field: 'EntryDate', caption: 'Entry', size: '120px', sortable: true, style: 'font-weight:bold;'
                },

            ],
    });
}



function AddRow()
{
    //if (w2ui.hasOwnProperty(GrdOrderDetails) == true)
    //{
    //    w2ui[GrdOrderDetails].destroy();
    //}

    
    let GrdOrderDetails = new w2grid({
        name: 'GrdOrderDetails',
        box: '#DivListOrderDetails',
        disableCVS: true,
        show: {
            lineNumbers: true,
            footer: true
        },
        columns: [
            { field: 'recid', text: 'OrderDetailsNo', size: '120px', sortable: true, resizable: true },

           
            { field: 'ItemCode', text: 'ItemCode', size: '120px', sortable: true, resizable: true, hidden: true },
            {
                field: 'ItemName', text: 'Item', size: '120px', sortable: true, resizable: true, //editable: { type: 'select', items: Item },  //editable: { type: 'combo' },
                editable: { type: 'combo', items: ResponseData, filter: true },

            },

            { field: 'ItemQty', text: 'Qty', size: '120px', sortable: true, resizable: true, editable: { type: 'text' }, },
            { field: 'ItemRate', text: 'Rate', size: '120px', sortable: true, resizable: true, editable: { type: 'text' }, },
            { field: 'TotalAmt', text: 'TotalAmt', size: '120px', sortable: true, resizable: true },

        ],
       
    })

    window.addARecord = function ()
    {
        debugger
        var item = $("#ddlItem").val();
        var IName = $("#ddlItem :selected").text();
        var Ival = $("#ddlItem :selected").val();
        let len = GrdOrderDetails.records.length
        GrdOrderDetails.add({ recid: len + 1, ItemCode: Ival, ItemName: IName, ItemQty: $("#txtItemQty").val(), ItemRate: $("#txtItemRate").val(), TotalAmt: $("#txtItemAmt").val() })
        
        $("#txtItemQty").val('');
        $("#txtItemRate").val('');
        $("#txtItemAmt").val('');
        $("#ddlItem").focus();
        $("#ddlItem").prop("selectedIndex", 0);

    }     
    window.removeRecords = function ()
    {
        GrdOrderDetails.clear()
        GrdOrderDetails.refresh()
    }    
}


function fncGetRate()
{
    debugger
    var obj = {};
    var Irate = $("#ddlItem").val();
    
    obj.Irate = Irate;

    //debugger
    $.ajax({
        url: '../Item/GetRate',
        type: "POST",
        data: obj,
        success: function (result)
        {
            debugger

            //respData = JSON.parse(result);            
            $('#txtItemRate').val(result);

        }
    })
}


function GetTotal()
{
    debugger
    var obj = {};
    var ItemQty = $("#txtItemQty").val();
    var ItemRate = $("#txtItemRate").val();

    obj.Qty = ItemQty;
    obj.Rate = ItemRate;
    obj.ItemAmt = obj.Qty * obj.Rate;

    $("#txtItemAmt").val(obj.ItemAmt);
   
}

function SaveData()
{
    debugger
    var obj = $('#FromItemOrder').serializeArray().reduce(function (obj, item)
    {
        obj[item.name] = item.value;

        return obj;
    }, {});

    debugger
    obj.ListOrderDetails = JSON.stringify(w2ui[GrdOrderDetails].records);
    obj.pid = $('#ddlParty').val();

    $.ajax({
        url: '../Item/SaveData',
        type: 'POST',
        data: obj,
        success: function (result)
        {
            var resp = JSON.parse(result);

            if (resp.Status == 'ERROR')
            {
                alert("Error saving record");
            }
            else
            {
                $('#NewFormContainer').html(resp.Data);

                //GenerateLastBatchNo();

               // w2ui['GrdOrderDetails'].reload();
                                
            }
        }
    })
    return false;
   // $('#FromItemOrder').reload();

}


function OrderHistory()
{
    //debugger
    if (w2ui.hasOwnProperty(GrdOrderHistory) == true)
    {
        w2ui[GrdOrderHistory].destroy();
    }


    $('#DivListOrderDetailsHISTORY').w2grid({
        name: GrdOrderHistory,
        recid: 'OrderNo',
        disableCVS: true, // very imp for Horizontal scroo problem. it skips header columns.

        show:
        {
            lineNumbers: true,
            footer: true,
            toolbar: true,
            toolbarSearch: false,
            toolbarInput: false,

        },
        toolbar:
        {
            right: '<div style="text-align:right;">List of order history</div>'

        },
        items: [
            { type: 'break' },
            { type: 'button', id: 'mybutton', caption: 'My other button', img: 'icon-folder' }
        ],

        columns:
            [
                { field: 'recid', text: 'OrderNo', size: '120px', sortable: true, resizable: true, hidden: true },
                {
                    field: 'OrderDate', text: 'Order Date', size: '120px', sortable: true, resizable: true,
                    render: function (record, index, column_index)
                    {
                        var html = w2utils.formatDate(record.OrderDate, 'dd-MM-yyyy');
                        return html;
                    }
                },
                { field: 'PartyCode', text: 'Party Code', size: '120px', sortable: true, resizable: true },
                { field: 'PartyName', text: 'Party Name', size: '120px', sortable: true, resizable: true },
                {
                    field: 'EntryDate', text: 'Entry', size: '120px', sortable: true, resizable: true,
                    render: function (record, index, column_index)
                    {
                        var html = w2utils.formatDate(record.EntryDate, 'dd-MM-yyyy');
                        return html;
                    }
                },
                {
                    field: '', caption: 'Action', size: '50px;',
                    render: function (record, index, column_index)
                    {
                        var BtnEdit = "BtnEdit" + record.recid;

                        if (record.recid != 0)
                        {
                            var HtmlString = '<button id="' + BtnEdit + '" type="button" class="BtnNormal_MattRed Font Font13" style="padding:2px 5px; margin-left:5px;" ' +
                                'onclick="EditFromGird(' + record.recid + ');" >' +
                                'Edit</button > ';
                        }
                        return HtmlString;
                    }

                },
            ],

    });
}

function EditFromGird(recid)
{

}

function BindOrderDetailsHistory()
{
    //debugger
    $.ajax({
        type: "GET",
        url: "../item/ListDataHistory",
        success: function (result)
        {
            var resp = JSON.parse(result);

            if (resp.Status == 'ERROR')
            {
                ShowMessage('Error while get rough stock valuation data', SwalTypes.error, resp.Data, 0);
            }
            else
            {
                debugger
                var data = JSON.parse(resp.Data);
                for (var i = 0; i < data.length; i++)
                {
                    data[i].SrNo = (i + 1);
                }

                w2ui[GrdOrderHistory].records = data;
                w2ui[GrdOrderHistory].reload();
                w2ui[GrdOrderHistory].unlock();

            }
        }
    })

}


//function EditBatch(Id)
//{
//    $('#DivCommonLoading').show();

//    $.ajax({
//        url: '../Batches/EditBatch',
//        data: { id: Id },
//        type: 'POST',
//        success: function (result)
//        {
//            $('#DivCommonLoading').hide();

//            var resp = JSON.parse(result);

//            if (resp.Status == 'ERROR')
//            {
//                ShowMessage('Failed to load record', SwalTypes.error, resp.Data, 0);
//            }
//            else
//            {
//                $('#DivForm').html(resp.Data);
//                IssueableStockForBatch();

//                ScrollToTop();
//            }
//        }
//    }).always(function ()
//    {
//        $('#DivCommonLoading').hide();
//    });
//}

//function DeleteBatch()
//{
//    swal({
//        title: 'Delete',
//        text: 'Are you sure you want to delete this record?',
//        type: SwalTypes.question,
//        showCancelButton: true,
//        confirmButtonColor: '#4a97d0',
//        cancelButtonColor: '#7d2828',
//        confirmButtonText: 'Yes, delete it!',
//        cancelButtonText: 'No'
//    }).then(result =>
//    {
//        if (result.value != undefined || result.value != null)
//        {
//            if (result.value == true)
//            {
//                var Code = $('#HfCode').val();

//                $.ajax({
//                    url: '../Batches/DeleteBatch',
//                    data: { id: Code },
//                    type: 'POST',
//                    success: function (result)
//                    {
//                        var resp = JSON.parse(result);

//                        if (resp.Status == 'ERROR')
//                        {
//                            ShowMessage('Error deleting record', SwalTypes.error, resp.Data, 0);
//                        }
//                        else
//                        {
//                            $('#DivForm').html(resp.Data);

//                            w2ui['ListData'].reload();

//                            ShowMessage('Batches', SwalTypes.success, 'Record deleted successfully.', 0);
//                        }
//                    }
//                });
//            }
//        }
//    });
//}