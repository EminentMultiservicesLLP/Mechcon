/*************** Item Grid which willbe available for ALl item list **********/
colItemList = [
    {
        dataIndx: "state", Width: 25, align: "center", type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false,
        editor: false, dataType: 'bool',
        title: "<input type='checkbox' />",
        cb: { select: true, all: false, header: true }
    },
    { dataIndx: "ID", hidden: true },
    { dataIndx: "ExpiryDate", hidden: true },
    { title: "Code", dataIndx: "Code", width: 100, dataType: "string", filter: { type: 'textbox', condition: 'begin', listeners: ['keyup'] }, hidden: true },
    { title: "Item Name", dataIndx: "Name", width: 200, dataType: "string", filter: { type: 'textbox', condition: 'begin', listeners: ['keyup'] } },
    { title: "HSN/SAC Code", dataIndx: "HSNCode", width: 150, editable: false },
    { title: "Unit Name", dataIndx: "UnitName", width: 90 },
    { title: "PackSize", dataIndx: "PackSize", width: 80 },
    { title: "Mrp", dataIndx: "MRP", width: 60, hidden: true },
    { title: "Current Stock", dataIndx: "CurrentQty", width: 90, dataType: "double", editable: false, hidden: true },
];

colGRNItemList = [
    {
        dataIndx: "state", Width: 25, align: "center", type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false,
        editor: false, dataType: 'bool',
        cb: { select: true, all: false, header: true }
    },
    { title: "", dataIndx: "ID", editable: false, hidden: true },
    { title: "", dataIndx: "MarkupPercentage", editable: false, hidden: true },
    { title: "", dataIndx: "PackSizeID", editable: false, hidden: true },
    { title: "Code", dataIndx: "Code", width: 100, hidden: true },
    { title: "Item Name", dataIndx: "Name", width: 200, dataType: "string", editable: false, filter: { type: 'textbox', condition: 'begin', listeners: ['keyup'] } },
    { title: "HSN/SAC Code", dataIndx: "HSNCode", width: 150, editable: false },
    { title: "Unit", dataIndx: "UnitName", width: 100, editable: false },
    { title: "MRP", dataIndx: "MRP", width: 100, editable: false },
    { title: "Rate", dataIndx: "StandardRate", width: 100, editable: false, hidden: true }
];

colMIItemList = [
    { dataIndx: "state", maxWidth: 25, minWidth: 25, align: "center", type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false, editor: false, dataType: 'bool', title: "<input type='checkbox' />", cb: { select: true, all: false, header: true } },
    { title: "Itemmm Name & Description", dataIndx: "Name", width: '95%', dataType: "string", editable: false, filter: { type: 'textbox', condition: 'begin', listeners: ['keyup'] } },
    { title: "Description", dataIndx: "DescriptiveName", width: '50%', dataType: "string", filter: { type: 'textbox', condition: 'begin', listeners: ['keyup'] }, hidden: true },
    { dataIndx: "ID", hidden: true },
    { dataIndx: "ExpiryDate", hidden: true },
    { title: "Code", dataIndx: "Code", width: 100, dataType: "string", filter: { type: 'textbox', condition: 'begin', listeners: ['keyup'] }, hidden: true },
    { title: "HSN/SAC Code", dataIndx: "HSNCode", minWidth: 125, editable: false, hidden: true },
    { title: "Unit Name", dataIndx: "UnitName", minWidth: 90, hidden: true },
    { title: "PackSize", dataIndx: "PackSize", minWidth: 80, hidden: true },
    { title: "Purchase Rate", dataIndx: "PurchaseRate", minWidth: 100, hidden: true },
    { title: "Current Stock", dataIndx: "CurrentQty", minWidth: 90, dataType: "double", editable: false, hidden: true },
];

dataItemList = {
    location: 'local',
    sorting: 'local',
    paging: 'local',
    dataType: 'JSON'
};


function ShowItemListPoup(gridName, modelName, setObject, getUrl, searchStoreId) {
    var ItemListGrid = $("#" + gridName);
    var ModelWindow = $("#" + modelName);
    if (ItemListGrid != undefined) {

        ItemListGrid.pqGrid(setObject);
        ItemListGrid.pqGrid("option", "dataModel.data", []);

        ModelWindow.dialog({
            height: 500,
            width: 700,
            modal: true,
            open: function (evt, ui) {

                $.ajax({
                    type: "GET",
                    url: getUrl,
                    data: { StoreId: searchStoreId },
                    datatype: "Json",
                    beforeSend: function () {
                        ItemListGrid.pqGrid("showLoading");
                    },
                    complete: function () {
                        ItemListGrid.pqGrid("hideLoading");
                    },
                    success: function (data) {
                        if (data.success) {
                            ItemListGrid.pqGrid("hideLoading");
                            ItemListGrid.pqGrid("option", "dataModel.data", data.items);
                            ItemListGrid.pqGrid("refreshDataAndView");
                            return;
                        } else {
                            ClearParamGrid('Itemgrid');
                            ShowAlert("error", data.Messsage);
                            return;
                        }
                    },
                    error: function (request, status, error) {
                        ItemListGrid.pqGrid("hideLoading");
                        ItemListGrid.pqGrid("refreshDataAndView");
                        ShowAlert("error", "Server error! Please Contact Administrator");
                        return;
                    }
                });
            },
            close: function (event, ui) {
            },
            show: {
                effect: "blind",
                duration: 500
            }
        });
    }
}

function ShowItemListPoupByItemtype(gridName, modelName, setObject, getUrl, searchStoreId, searchItemType) {
    var ItemListGrid = $("#" + gridName);
    var ModelWindow = $("#" + modelName);
    if (ItemListGrid != undefined) {

        ItemListGrid.pqGrid(setObject);
        ItemListGrid.pqGrid("option", "dataModel.data", []);

        ModelWindow.dialog({
            height: 500,
            width: 700,
            modal: true,
            open: function (evt, ui) {

                $.ajax({
                    type: "GET",
                    url: getUrl,
                    data: { StoreId: searchStoreId, ItemTypeId: searchItemType },
                    datatype: "Json",
                    beforeSend: function () {
                        ItemListGrid.pqGrid("showLoading");
                    },
                    complete: function () {
                        ItemListGrid.pqGrid("hideLoading");
                    },
                    success: function (data) {
                        if (data.success) {
                            ItemListGrid.pqGrid("hideLoading");
                            ItemListGrid.pqGrid("option", "dataModel.data", data.items);
                            ItemListGrid.pqGrid("refreshDataAndView");
                            return;
                        } else {
                            ClearParamGrid('Itemgrid');
                            ShowAlert("error", data.Messsage);
                            return;
                        }
                    },
                    error: function (request, status, error) {
                        ItemListGrid.pqGrid("hideLoading");
                        ItemListGrid.pqGrid("refreshDataAndView");
                        ShowAlert("error", "Server error! Please Contact Administrator");
                        return;
                    }
                });
            },
            close: function (event, ui) {
            },
            show: {
                effect: "blind",
                duration: 500
            }
        });
    }
}
function ShowTaxPoup() {
    var $Taxgrid;
    var dataTaxM = { location: "local", sorting: 'local' }
    var colTaxM = [
        {
            dataIndx: "state", Width: 25, align: "center", type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false,
            editor: false, dataType: 'bool',
            title: "<input type='checkbox' />",
            cb: { select: true, all: true, header: true }
        },
        { title: "", dataIndx: "Taxid", dataType: "integer", hidden: true },
        { title: "Code", dataIndx: "Tax_Code", width: 50, editable: false },
        { title: "Name", dataIndx: "Tax_name", width: 160, editable: false },
        { title: "Tax Type", dataIndx: "Tax_Type", width: 50, hidden: true },
        { title: "Percentage", dataIndx: "Tax_percentage", width: 120, editable: false },
        { title: "Formula", dataIndx: "Formula", width: 350, editable: false },
        { title: "Tax", dataIndx: "Tax_EncExc", width: 100, editable: false },
        { title: "Taxes", dataIndx: "Taxes", hidden: true }
    ];
    var taxM = {
        width: 400,
        height: 290,
        sortable: false,
        numberCell: { show: false },
        hoverMode: 'cell',
        showTop: false,
        resizable: true,
        scrollModel: { autoFit: true },
        draggable: false,
        wrap: false,
        editable: true,
        columnBorders: true,
        selectionModel: { type: 'row' },
        colModel: colTaxM,
        dataModel: dataTaxM,
        pageModel: { type: "local", rPP: 20 }
    };
    $Taxgrid = $("#Taxgrid").pqGrid(taxM);
    $.ajax({
        type: "GET",
        url: "/Master/TaxMaster",
        datatype: "Json",
        success: function (data) {
            $Taxgrid.pqGrid("option", "dataModel.data", data);
            $Taxgrid.pqGrid("refreshDataAndView");
        }
    });
}

/*********************************************************************/