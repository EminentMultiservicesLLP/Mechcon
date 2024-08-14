
loadDocument();
var dataUploadDocument = { location: "local" };
var colUploadGrid = [
    { title: "", dataIndx: "UploadId", dataType: "integer", hidden: true },
    { title: "", dataIndx: "CatergoryId", dataType: "integer", hidden: true },
    { title: "", dataIndx: "CityId", dataType: "integer", hidden: true },
    { title: "", dataIndx: "OutletId", dataType: "integer", hidden: true },
    {
        title: "Outlet", dataIndx: "OutletName", width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
    {
       title: "Document Type", dataIndx: "CategoryName", width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
    { title: "", dataIndx: "SubCategoryId", dataType: "integer", hidden: true },
    {
        title: "Docoment Sub Type", dataIndx: "SubCategoryName", width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }

    },
    {
        title: "", editable: false, minWidth: '13%', sortable: false,
        render: function (ui) {
                        
            var renderButton = '<button type="button" class="btn btn-primary" title="Click Here to View Document" onclick="ViewDoc(' + ui.rowIndx +'); "  title="Click Here to Delete Bill"></i>View Document</button>';
            return renderButton;
        }


    },
    { title: "", dataIndx: "VendorId", dataType: "integer", hidden: true },
    {
        title: "Vendor", dataIndx: "VendorName", width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }

    },
    {
        title: "Date of Bill", dataIndx: "strBilldate",  width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
    {
        title: "Reference Bill No", dataIndx: "ReferenceNo", width: '10%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
    {
        title: "Date Of Upload", dataIndx: "strUploadDate",width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
    {
        title: "Remark", dataIndx: "Comment", dataType: String, width: '15%', required: true, editable: true,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] },
        editable: function (ui) {

            if (ui.rowData.Status > 0) {

                ShowAlert("warning", "Cannot make changes document Approved or Deleted!");
                return false;
            }
            else {
                return true
            }
        }
    },
    {
        title: "Date Of Approval", dataIndx: "strApprovaldate", width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
    { title: "", dataIndx: "Status", dataType: "integer", hidden: true },
    {
        title: "Status", dataIndx: "StatusName", dataType: String, width: '15%', editable: false,
        filter: { type: 'textbox', condition: 'contain', listeners: ['keyup'] }
    },
   
    {
        title: "", editable: false, minWidth: '14%', sortable: false,
        render: function (ui) {
            if (ui.rowData.IsApproved == true && ui.rowData.Status==0) {
                var renderButton = '<button type="button" class="btn btn-primary" onclick="SaveDoc(' + ui.rowIndx + '); "  title="Click Here to Delete Document"></i>Save To Approve</button>';
                return renderButton;
            }
           
        }


    },
    
    {
        title: "", editable: false, minWidth: '10%', sortable: false,
        render: function (ui) {
            
            //  let BillId = ui.rowData.BillId;
            if (ui.rowData.IsDelete == true && ui.rowData.Status<2) {
                var renderButton = '<button type="button" class="btn btn-primary" onclick="DeleteDoc(' + ui.rowIndx + ');" title="Click Here to Delete Document"></i>Delete</button>';
                return renderButton;
            }
            else {

            }
        }


    },
    {
        title: "", editable: false, minWidth: '10%', sortable: false,
        render: function (ui) {

            //  let BillId = ui.rowData.BillId;
            if (ui.rowData.IsRevoke==true && ui.rowData.Status==2) {
                var renderButton = '<button type="button" class="btn btn-primary" onclick="RevokeDoc(' + ui.rowIndx + ');"  title="Click Here to Revoke Document"></i>Revoke</button>';
                return renderButton;
                //  let BillId = ui.rowData.BillId;
            }
            else {
                
            }
        }


    },

    


];
var setUploadGrid = {
    width: 'auto',
    maxWidth: '100%',
    autofill: true,
    numberCell: { show: true },
    hoverMode: 'row',
    showTop: false,
    showTitle: false,
    showBottom: false,
    sortable: true,
    scrollModel: { autoFit: false },
    draggable: false,
    hwrap: false,
    wrap: false,
    editable: true,
    columnBorders: true,
    pageModel: { type: "local", rPP: 20 },
    filterModel: { on: true, mode: "AND", header: true },
    selectionModel: { type: 'row', subtype: 'incr', cbHeader: true, cbAll: true },
    colModel: colUploadGrid,
    dataModel: dataUploadDocument,
    }
    //pageModel: { type: "local", rPP: 100 },
    //rowClick: function (evt, ui) {

    //    if (ui.rowData) {

    //        var data = ui.rowData;
    //        $("#TotalAmount").val(data.TotalAmount);
    //        $("#DueAmount").val(data.DueAmount);
    //        $("#BillId").val(data.BillId);
    //        $("#ReceiveAmount").prop('disabled', false);
    //        $("#ddlPaymentMode").prop('disabled', false);
    //        $("#RDate").prop('disabled', false);
    //        $("#TDS").prop('disabled', false);
    //        $("#Remark").prop('disabled', false);


    //    }
    //}

var $SearchGrid = $("#Documentgrid").pqGrid(setUploadGrid);
function loadDocument() {
    
    var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
    $.ajax({

        headers: {
            "__RequestVerificationToken": antiForgeryToken
        },
        type: 'GET',
        url: "/AddDocument/GetAllDocument",
        //  dataType: "json",

        beforeSend: function () {
            

            //$SearchGrid.pqGrid("showLoading");
        },
        complete: function () {

            $SearchGrid.pqGrid("hideLoading");
        },

        success: function (data) {
            

            $("#Documentgrid").pqGrid("hideLoading");
            $("#Documentgrid").pqGrid("option", "dataModel.data", data.data);
            $("#Documentgrid").pqGrid("refreshDataAndView");
            PqGridRefreshClick($SearchGrid);

        }

    });
}

function ViewDoc(rowIndex)
{
    
    let DocumentGridData = $("#Documentgrid").pqGrid("option", "dataModel.data");
     

    let CategoryId = DocumentGridData[rowIndex].CatergoryId;
    let SubCategoryId = DocumentGridData[rowIndex].SubCategoryId;
    let VendorId = DocumentGridData[rowIndex].VendorId;
    let ReferenceNo = DocumentGridData[rowIndex].ReferenceNo;
    let BillDate = DocumentGridData[rowIndex].strBilldate;
    let UploadDate = DocumentGridData[rowIndex].strUploadDate;
    let CityId = DocumentGridData[rowIndex].CityId;
    let OutletId = DocumentGridData[rowIndex].OutletId;
    $("#AttchmentsPoPupModal").modal("show");

    var token = $("input[name=__RequestVerificationToken]").val();
    $.ajax({
        type: "GET",
        traditional: true,
        data: { CategoryId: CategoryId, SubCategoryId: SubCategoryId, VendorId: VendorId, CityId: CityId, OutletId: OutletId, ReferenceNo: ReferenceNo, BillDate: BillDate, UploadDate: UploadDate },
        url: '/AddDocument/GetAttachments',
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", token);
        },
        success: function (data) {
            
            $("#DocumentAttachments").empty();
            var x= data.data;
            for (var i = 0; i < x.length; i++) {
                
               
                    DisplayOtherFiles("DocumentAttachments", x[i].File,i,x[i].UploadId);
               
            }
        },
        error: function (a, exception, b) {
        }
    });
}
function SaveDoc(rowIndex) {
    
    let DocumentGridData = $("#Documentgrid").pqGrid("option", "dataModel.data");


    let CategoryId = DocumentGridData[rowIndex].CatergoryId;
    let SubCategoryId = DocumentGridData[rowIndex].SubCategoryId;
    let VendorId = DocumentGridData[rowIndex].VendorId;
    let ReferenceNo = DocumentGridData[rowIndex].ReferenceNo;
    let BillDate = DocumentGridData[rowIndex].strBilldate;
    let UploadDate = DocumentGridData[rowIndex].strUploadDate;
    let CityId = DocumentGridData[rowIndex].CityId;
    let OutletId = DocumentGridData[rowIndex].OutletId;
    let Comment = DocumentGridData[rowIndex].Comment;
    if (Comment == null) {
        
        ShowAlert("warning", "Please Enter Remark");
        return;

    }
    var token = $("input[name=__RequestVerificationToken]").val();
    $.ajax({
        type: "GET",
        traditional: true,
        data: { CategoryId: CategoryId, SubCategoryId: SubCategoryId, VendorId: VendorId, CityId: CityId, OutletId: OutletId, ReferenceNo: ReferenceNo, BillDate: BillDate, UploadDate: UploadDate, Comment: Comment },
        url: '/AddDocument/SaveStatus',
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", token);
        },
        success: function (data) {
            loadDocument();
            ShowAlert("success", "Save Successfully");
        },
        error: function (a, exception, b) {
        }
    });
}
function DeleteDoc(rowIndex) {
    
    let DocumentGridData = $("#Documentgrid").pqGrid("option", "dataModel.data");


    let CategoryId = DocumentGridData[rowIndex].CatergoryId;
    let SubCategoryId = DocumentGridData[rowIndex].SubCategoryId;
    let VendorId = DocumentGridData[rowIndex].VendorId;
    let ReferenceNo = DocumentGridData[rowIndex].ReferenceNo;
    let BillDate = DocumentGridData[rowIndex].strBilldate;
    let UploadDate = DocumentGridData[rowIndex].strUploadDate;
    let CityId = DocumentGridData[rowIndex].CityId;
    let OutletId = DocumentGridData[rowIndex].OutletId;
   // let Comment = DocumentGridData[rowIndex].Comment;

    var token = $("input[name=__RequestVerificationToken]").val();
    $.ajax({
        type: "GET",
        traditional: true,
        data: { CategoryId: CategoryId, SubCategoryId: SubCategoryId, VendorId: VendorId, CityId: CityId, OutletId: OutletId, ReferenceNo: ReferenceNo, BillDate: BillDate, UploadDate: UploadDate},
        url: '/AddDocument/DeleteDocument',
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", token);
        },
        success: function (data) {
            loadDocument();
            ShowAlert("success", "Document Delete Successfully");
        },
        error: function (a, exception, b) {
        }
    });
}

function RevokeDoc(rowIndex) {
    
    let DocumentGridData = $("#Documentgrid").pqGrid("option", "dataModel.data");


    let CategoryId = DocumentGridData[rowIndex].CatergoryId;
    let SubCategoryId = DocumentGridData[rowIndex].SubCategoryId;
    let VendorId = DocumentGridData[rowIndex].VendorId;
    let ReferenceNo = DocumentGridData[rowIndex].ReferenceNo;
    let BillDate = DocumentGridData[rowIndex].strBilldate;
    let UploadDate = DocumentGridData[rowIndex].strUploadDate;
    let CityId = DocumentGridData[rowIndex].CityId;
    let OutletId = DocumentGridData[rowIndex].OutletId;
    // let Comment = DocumentGridData[rowIndex].Comment;

    var token = $("input[name=__RequestVerificationToken]").val();
    $.ajax({
        type: "GET",
        traditional: true,
        data: { CategoryId: CategoryId, SubCategoryId: SubCategoryId, VendorId: VendorId, CityId: CityId, OutletId: OutletId, ReferenceNo: ReferenceNo, BillDate: BillDate, UploadDate: UploadDate },
        url: '/AddDocument/RevokeDocument',
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", token);
        },
        success: function (data) {
            loadDocument();
            ShowAlert("success", "Document Revoke Successfully");
        },
        error: function (a, exception, b) {
        }
    });
}
function isFileImage(Str) {
    var test = (/\.(gif|jpg|jpeg|tiff|png)$/i).test(Str)
    return test;
}

function DisplayOtherFiles(objectName, filePath, liID,UploadId) {
    
    var FileDetail = filePath.split('\\');
    FileDetail.push(filePath);
    var FileDetail = filePath.split('\\');
    FileDetail.push(filePath);
    var id = liID != undefined || liID != null ? liID : parseInt(FileDetail[FileDetail.length - 3]);
    //
    var src;
    var y = FileDetail[FileDetail.length - 2]
    var extension = y.split('.').pop().toLowerCase()
    isSuccess = fileTypes.indexOf(extension) > -1;
    if (extension == 'pdf') {
        src = "https://image.flaticon.com/icons/svg/179/179483.svg";
    }
    else {
        src = "https://image.flaticon.com/icons/svg/136/136524.svg";
    }
    var newImageLI = '<li style="margin-left: 40px;"  id=' + id + '>';
         newImageLI = newImageLI +
        '<a class="title" style="font-style:normal;font-size:15px" id="' +
        parseInt(parseInt(FileDetail[FileDetail.length - 3])) +
        '" href="' +
        '/AddDocument/DownloadDocument?UploadId=' + UploadId + "&FileName=" +FileDetail[FileDetail.length - 2]+
        '" target="_blank">'+
        FileDetail[FileDetail.length - 2] +'<img width="60px" style="margin-left:0" src="'+src+'" /></a>';

    $("#" + objectName).append(newImageLI);
}

var fileTypes = ['pdf', 'docx', 'rtf', 'jpg', 'jpeg', 'png', 'txt'];
