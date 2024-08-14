
GetPIDetails();
GetPODetails();
function DashBoardPanel(valElement, noElement, colorElement, valueDEc, valName, genVal, menuId, menuEle, href) {
    var wd = (valueDEc * 100) / genVal + "%";
    var href = "Purchase?MenuId=" + menuId;
    $("#" + noElement).text(valueDEc);
    $("#" + valElement).html(valName);
    $("#" + colorElement).width(wd);
    $("#" + menuEle).attr("href", href);
}
function GetPIDetails() {
    $.ajax(
    {
        type: "GET", 
        traditional: true,
        contentType: 'application/json; charset=utf-8',
        url: '/DashboardCommon/GetDashboardDetails',
        data: { id: 2 },
        success: function (data) {
            DashBoardPanel("GenPI", "GenPIQ", "qtyclass", data.records[0].Gen, "Generated", data.records[0].Gen, data.records[0].MenuId, "PIGMenu", "Purchase?MenuId=");
            DashBoardPanel("PenPI", "PenPIQ", "PIPenqtyclass", data.records[0].Pen, "Pending", data.records[0].Gen, data.records[0].MenuId, "PIPMenu", "Purchase?MenuId=");
            DashBoardPanel("AuthPI", "AuthPIQ", "PIAuthqtyclass", data.records[0].Auth, "Authorized", data.records[0].Gen, data.records[0].MenuId, "PIAMenu", "Purchase?MenuId=");
            DashBoardPanel("CanPI", "CanPIQ", "PICanqtyclass", data.records[0].Can, "Cancel", data.records[0].Gen, data.records[0].MenuId, "PICMenu", "Purchase?MenuId=");
            $("#PenPIQN").text(data.records[0].Pen);
        }
    });
}
function GetPODetails() {
    $.ajax(
       {
           type: "GET", 
           traditional: true,
           contentType: 'application/json; charset=utf-8',
           url: '/DashboardCommon/GetDashboardDetails',
           data: { id: 3 },
           success: function (data) {
               DashBoardPanel("GenPO", "GenPOQ", "qtyclass", data.records[0].Gen, "Generated", data.records[0].Gen, data.records[0].MenuId, "POGMenu", "Purchase?MenuId=");
               DashBoardPanel("PenPO", "PenPOQ", "POPenqtyclass", data.records[0].Pen, "Pending", data.records[0].Gen, data.records[0].MenuId, "POPMenu", "Purchase?MenuId=");
               DashBoardPanel("AuthPO", "AuthPOQ", "POAuthqtyclass", data.records[0].Auth, "Authorized", data.records[0].Gen, data.records[0].MenuId, "POAMenu", "Purchase?MenuId=");
               DashBoardPanel("CanPO", "CanPOQ", "POCanqtyclass", data.records[0].Can, "Cancel", data.records[0].Gen, data.records[0].MenuId, "POCMenu", "Purchase?MenuId=");
               $("#PenPOQN").text(data.records[0].Pen);
           }
       });
}


