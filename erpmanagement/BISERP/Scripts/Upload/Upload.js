$(document).ready(function () {
    SubCategoryList = [];
    LoadSubCategory();
    var CountryId;
    var fileTypes = ['pdf', 'docx', 'jpg', 'jpeg', 'png', 'txt'];  //acceptable file types
    $('#id_outlet').on('change', function () {
       //   
        let NoOfDiv = document.getElementsByClassName("uploadDoc");
       
        for (let y = 0; y <= NoOfDiv.length - 1; y++) {
            let test = NoOfDiv[y].id.split("_");
            var t = test[test.length - 1];
            $('#id_vendor' + t).empty();
         

        }
      
         CountryId=($(this).find(':selected').data('countryid'));
        LoadVendor(CountryId);
        //var a = $("#id_outlet").val($('option:selected', this).attr("countryid"));
        //alert(a);
        
    });
  //  var outletid1 = $("#id_outlet").data();
    CityList = [];
    VendorList = [];
    UploadFile = [];
   // LoadSubCategory();
   // LoadCityList();
    
    AddDiv(0);
    //function LoadCityList() {
        

    //    var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
    //    $.ajax({
    //        //headers: {
    //        //    "__RequestVerificationToken": antiForgeryToken
    //        //},
    //        type: "Get", //HTTP POST Method
    //        url: "/Masters/GetCity", // Controller/View
    //        success: function (response) {
    //            if (response.success === true) {

    //                LoadCities1(response);
    //                AddDiv(0);
    //                // loadSearchGrid();
    //            }
    //            else {

    //                //  loadSearchGrid();
    //            }
    //        }

    //    });
    //};
    function LoadSubCategory() {
       //  
        
        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        $.ajax({
            //headers: {
            //    "__requestverificationtoken": antiforgerytoken
            //},
            type: "Get", //HTTP POST Method
            url: "/Masters/GetSubCategory", // Controller/View
            success: function (data) {
             //    
                if (data.success === true) {
                    
                    PushSubCategory(data);


                    // loadSearchGrid();
                }
                else {

                    //  loadSearchGrid();
                }
            }

        });

    };
    function LoadVendor(CountryId) {
        // 
        
        //var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();//{ "__RequestVerificationToken": antiForgeryToken },
        //ajaxCall("/Masters/GetCity", { "__RequestVerificationToken": antiForgeryToken }, "GET", LoadCities1, function (response) { ajaxCallFailed(response, response.message); }, true, []);
        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        $.ajax({
            //headers: {
            //    "__RequestVerificationToken": antiForgeryToken
            //},
            type: "Get", //HTTP POST Method
            url: "/Masters/Vendor", // Controller/View
            data: { CountryId: CountryId },
            success: function (response) {
               //  
                if (response.success === true) {
                   
                   
                    PushVendor(response);


                    // loadSearchGrid();
                }
                else {

                    //  loadSearchGrid();
                }
            }

        });

    };
    function PushVendor(response) {
        VendorList = [];

        $.each(response.data, function (key, value) {
            
           
            VendorList.push({
                "VendorId": value.VendorId, "VendorName": value.VendorName
            });
        });
       // $('#id_vendor' + index).empty();
        let NoOfDiv = document.getElementsByClassName("uploadDoc");

        for (let y = 0; y <= NoOfDiv.length - 1; y++) {
            let test = NoOfDiv[y].id.split("_");
            var t = test[test.length - 1];
            Vendor(t);


        }
       
    }
    function PushSubCategory(response) {
        $.each(response.data, function (key, value) {
            
            SubCategoryList.push({
                "SubCategoryId": value.SubCategoryId, "SubCategoryName": value.SubCategoryName
            });
        });
        SubCategory(0);
    }
    //function LoadCities1(response) {
    //    $.each(response.data, function (key, value) {
            
    //        CityList.push({
    //            "CityID": value.CityID, "CityName": value.CityName
    //        });
    //    });
    //}
    
    
    $(document).on('change', '.up', function () {
        var id = $(this).attr('id'); /* gets the filepath and filename from the input */
        var profilePicValue = $(this).val();
        var fileNameStart = profilePicValue.lastIndexOf('\\'); /* finds the end of the filepath */
        profilePicValue = profilePicValue.substr(fileNameStart + 1).substring(0, 20); /* isolates the filename */
        //var profilePicLabelText = $(".upl"); /* finds the label text */
        if (profilePicValue != '') {
            //console.log($(this).closest('.fileUpload').find('.upl').length);
            $(this).closest('.fileUpload').find('.upl').html(profilePicValue); /* changes the label text */
        }
    });
    var div_increaseid = 1;
    var check = 1;
    $(".btn-new").on('click', function () {
        
        if (check <= 4) {
            AddDiv(div_increaseid);
            check++;
            div_increaseid++;

        }
        else {
            ShowAlert("error", "You cant Add more than 5!");
        }
    });
    function AddDiv(div_increaseid) {
        
        //$("#uploader").append('<div class="uploadDoc col-sm-12 col-md-12 fieldsetCustom" id="Div_' + div_increaseid + '" style="border:1px solid black"><div class="col-sm-12 col-md-12"><div class="col-sm-3 col-md-3"><label for="FileInput' + div_increaseid + '">Upload File</lable><input type="file" multiple accept=".pdf,.jpg,.jpeg,.png,.bmp" name="fileInput0" class="uploadup" index=' + div_increaseid + ' id="FileInput' + div_increaseid + '"onchange="" /></div><div class="col-md-2 col-sm-2"><label for="Title' + div_increaseid + '">Reference Bill No.</label><input type="text" class="" id="Title' + div_increaseid + '" name=""></div><div class="col-md-1 col-sm-1"> <label for="id_city' + div_increaseid + '">City</label> <select id="id_city' + div_increaseid + '" index=' + div_increaseid + ' class="dynamicDropdown" ><option value="0" >--Select City--</option> </select></div><div class="col-sm-1 col-md-1"><label for="id_vendor' + div_increaseid + '">Vendor</label><select id="id_vendor' + div_increaseid + '"><option >--Select--</option> </select></div><div class="col-md-2 col-sm-2"> <label for="id_Subtype' + div_increaseid + '">Document Type</label> <select id="id_docsubType' + div_increaseid + '" index=' + div_increaseid + '  ><option value="0">Select Document Type</option></select></div><div class="col-md-2 col-sm-2"><label for="BillDate' + div_increaseid + '">Bill Date</label><input type="date" id="BillDate' + div_increaseid + '" /></diV><div class="col-sm-1 col-md-1" style="padding-top:25px"><span><a class="btn-check"><i class="fa fa-times"></i></span></div></div><div class="col-sm-12 col-md-12 imgDiv" id="nameappend'+div_increaseid+'"></div></div>');
        $("#uploader").append('  <div class="uploadDoc col-sm-12 col-md-12 fieldsetCustom" id="Div_' + div_increaseid + '" style="border:1px solid black"> 	<div class="row"><div class="col-sm-3 col-md-3"><label class="required" for="FileInput' + div_increaseid + '">Upload File</label><input class="uploadup form-control SaleBreakup required" type="file" multiple accept=".pdf,.jpg,.jpeg,.png,.bmp" name="fileInput0" index=' + div_increaseid + ' id="FileInput' + div_increaseid + '"onchange="" /></div><div class="col-sm-9 col-md-9" imgDiv" id="nameappend' + div_increaseid + '"></div></div><hr><div class="row"><div class="col-md-2 col-sm-2 required"><label class="required" for="Title' + div_increaseid + '">Reference Bill No.</label><input type="text" class="form-control SaleBreakup required" id="Title' + div_increaseid + '" name=""></div><div class="col-sm-2 col-md-2"><label class="required" for="id_vendor' + div_increaseid + '">Vendor</label><select class="form-control SaleBreakup required" id="id_vendor' + div_increaseid + '"><option >--Select Vendor--</option> </select></div><div class="col-md-2 col-sm-2"> <label class="required" for="id_Subtype' + div_increaseid + '">Document Type</label> <select class="form-control SaleBreakup required" id="id_docsubType' + div_increaseid + '" index=' + div_increaseid + '  ><option value="0">Select Document Type</option></select></div><div class="col-md-2 col-sm-2"><label class="required" for="BillDate' + div_increaseid + '">Bill Date</label><input class="form-control SaleBreakup required" type="date" id="BillDate' + div_increaseid + '" /></diV><div class="col-sm-1 col-md-1" style="padding-top:25px"><span><a class="btn-check"><i class="fa fa-times"></i></span></div></div></div></div>');
        //BindDrpDwn(div_increaseid);
     Vendor(div_increaseid);
        SubCategory(div_increaseid);
    };
    //$(document).on("change", '.dynamicDropdown', function (e) {
        
    //   // VendorList = [];
    //    index = this.getAttribute("index");
        
    //    LoadVendor(this.value);


    //    //$.each(VendorList, function (data, value) {
    //    //    
    //    //    $('#id_vendor'+index+'').append('<option value="' + value.VendorId + '">' + value.VendorName + '</option>');
    //    //});
    //    //VendorList = [];
    //});
    function BindVendor(index) {
        //var x = document.getElementById('id_vendor'+index);
        //$('id_vendor' + index).val("");
      
        //VendorList = [];
    }
    function SubCategory(index) {
       //  
        $.each(SubCategoryList, function (data, value) {
            
            $('#id_docsubType' + index + '').append('<option value="' + value.SubCategoryId + '">' + value.SubCategoryName + '</option>');
        });
       // VendorList = [];
    }
    function Vendor(index) {
      //  $('#id_vendor' + index + '').append('<option value="0">Select Vendor</option>');
        $.each(VendorList, function (data, value) {

            $('#id_vendor' + index + '').append('<option value="' + value.VendorId + '">' + value.VendorName + '</option>');
        });
    }
    //function BindDrpDwn(div_increaseid) {
        

    //    $.each(VendorList, function (index, value) {

    //        $('#id_city' + div_increaseid + '').append('<option value="' + value.VendorId + '">' + value.VendorName + '</option>');
    //    });

    //}

    $(document).on("click", "a.btn-check", function () {
       
        if ($(".uploadDoc").length > 0) {
            
            $(this).closest(".uploadDoc").remove();
            check--;
        } else {
            ShowAlert("error","You have to upload at least one document.");
        }
    });
    $(".btn-next").on('click', function () {
        let NoOfDiv = document.getElementsByClassName("uploadDoc");
        var totalfilesize = 0;
        
        if ($("#id_outlet").val() == 0) {
            ShowAlert("error", "Please Select Outlet!");
            return;
        }

        for (let y = 0; y <= NoOfDiv.length - 1; y++) {
            let test = NoOfDiv[y].id.split("_");
            var DivNO = test[test.length - 1];
            
            var list = document.getElementById('FileInput' + DivNO + '');
            if ($('#FileInput' + DivNO + '').val() == '') {
                ShowAlert("warning", "Attachment is missing ! Please Select Attachment !");
                return;
            }
            if ($('#Title' + DivNO+'').val() == 0) {
                ShowAlert("warning", "Please Enter Reference No!");
                return;

            }
            if ($('#id_city' + DivNO + '').val() == 0) {
                ShowAlert("warning", "Please Select City!");
                return;

            }
            //if ($('#id_vendor' + DivNO + '').val() == 0) {
            //    ShowAlert("warning", "Please Select Vendor!");
            //    return;

            //}
            if ($('#id_docsubType' + DivNO + '').val() == 0) {
                
                ShowAlert("warning", "Please Select Document Type!");
                return;

            }
            if ($('#BillDate' + DivNO + '').val() == '') {
                ShowAlert("warning", "Please Select Bill Date!");
                return;

            }
           
            var files = $("#FileInput" + y).get(0).files;
            for (f = 0; f < files.length; f++) {
                 
                totalfilesize = totalfilesize + files.item(f).size;
            }
           
        }
        if (totalfilesize => 1048576) {
             
            ShowAlert("warning", "You cannot upload more than 10mb!");
            return;
        }






        
        var fdata = new FormData();
        // add the uploaded file to the form data collection  
        var outletid = $("#id_outlet").val();
        fdata.append("outletid", outletid);
        for (let i = 0; i <= NoOfDiv.length - 1; i++) {
            let test = NoOfDiv[i].id.split("_");
            var DivNO = test[test.length - 1];
            let allfiles = new Array();
            var title = $("#Title" + DivNO + "").val();
           // var CountryId = 
            var vendorid = $("#id_vendor" + DivNO).val();
            var billdate = $("#BillDate" + DivNO).val();
            var docSubTypeId = $("#id_docsubType" + DivNO).val();
            fdata.append("cityid", CountryId);
            fdata.append("vendorid", vendorid);
            var files = $("#FileInput" + DivNO).get(0).files;
            for (f = 0; f < files.length; f++) {
                allfiles.push(files[f]);
            }

            for (let z = 0; z < allfiles.length; z++) {
                let dd = {
                    Title: title,
                    CityId: CountryId,
                    VendorId: vendorid,
                    BillDate: billdate,
                    DocSubTypeId: docSubTypeId,

                }
                fdata.append(JSON.stringify(dd), allfiles[z]);
            }

         
        }


        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        $.ajax({
            //headers: {
            //    "__RequestVerificationToken": antiForgeryToken
            //},
            type: "POST",
            url: "/AddDocument/FileUpload_Post", // Controller/View
            data: fdata,
            processData: false,
            contentType: false,
            beforeSend: function () {
                
              
            },
            success: function (msg) {
                if (msg.success === true) {
                    
                   // ClearFormData(div_increaseid);
                    ShowAlert("success", msg.message);
                    ClearFormData();
                    // loadSearchGrid();
                }
                else {
                    ShowAlert("error", msg.message);
                    //  loadSearchGrid();
                }
            }

        });

                 //$.ajax({
        //    url: '/adddocument/uploadfile',
        //    type: 'post',
        //    data: data,
        //    contenttype: false,
        //    processdata: false,
        //    success: function (d) {

        //    }

        //});


    });
    function ClearFormData() {
        
        let NoOfDiv = document.getElementsByClassName("uploadDoc");
        $('Div.imgclick').remove();
        
        for (let y = 0; y <= NoOfDiv.length - 1; y++) {
            let test = NoOfDiv[y].id.split("_");
            var t = test[test.length - 1];
           
            
            $('#id_city' + t).val(0);
            $('#id_outlet').val(0);
            $('#id_outlet' + t + '').val(0);
            $('#FileInput'+t).val('');
            $('#id_vendor' +t).empty();
            $('#id_vendor' + t+'').val('');
            $('#Title' + t + '').val('');
            $('#id_docsubType' + t + '').val(0);
            $('#BillDate' + t + '').val('');

        }
    }


});

$(document).on("click", '.imgclick', function (e) {
    
  // jQuery.noConflict();
    document.getElementById('viewer').contentDocument.location.reload(true);
   // $("#AttchmentsPoPupModal").modal("toggle");
  //  $("#AttchmentsPoPupModal").modal("show");
    let name=e.currentTarget.getAttribute("name")
        let NoOfDiv = document.getElementsByClassName("uploadDoc");
        for (let i = 0; i <= NoOfDiv.length - 1; i++) {
            let test = NoOfDiv[i].id.split("_");
            var DivNO = test[test.length - 1];
            var files = $("#FileInput" + DivNO).get(0).files;
            for (f = 0; f < files.length; f++) {
                if (files.item(f).name == name) {
                    pdffile = $("#FileInput" + DivNO).get(0).files[f];
                    pdffile_url = URL.createObjectURL(pdffile);
                    window.open(pdffile_url, '_blank');
                   // $('#viewer').attr('src', pdffile_url);
                }
            }
        }
       
    
});
$(document).on("change", '.uploadup', function (e) {
    
   // 
    let y = this.getAttribute("index");
    var n = document.getElementById("nameappend" + y);
    if (n.childNodes.length != 0) {
        while(n.childNodes.length>0) {
            n.removeChild(n.childNodes[n.childNodes.length - 1]);
            n.childNodes.length--;
        }
       
    }
    else {
        //n.removeChild(n.childNodes[0]);
    }
    var inp = $("#FileInput" + y).get(0).files;
    
    for (var i = 0; i < inp.length; ++i)
    {
         
        var name = inp.item(i).name;
        var size = inp.item(i).size;
        var x = size / 1048576;
        var totalsize = x.toFixed(2);
        var extension = name.split('.').pop().toLowerCase();  //file extension from input file
            isSuccess = fileTypes.indexOf(extension) > -1;  //is extension in acceptable types

       

        if (extension == 'pdf') {
            $("#nameappend" + y).append('<div class="col-sm-2 col-md-2 imgclick" id="d' + i + '" name="' + name + '"  style="font-size:12px" ><a><img width="25px" src="https://image.flaticon.com/icons/svg/179/179483.svg">' + name + '('+totalsize+'mb)</img ></a ></div > ');
        }
        else if (extension == 'png') {
            $("#nameappend" + y).append('<div class="col-sm-2 col-md-2 imgclick" name="' + name + '" style="font-size:12px"><img width="25px" src="https://image.flaticon.com/icons/svg/136/136523.svg">' + name + '(' + totalsize +'mb)</img></div>');
        }
        else if (extension == 'jpg' || extension == 'jpeg') {
            $("#nameappend" + y).append('<div class="col-sm-2 col-md-2 imgclick" name="' + name + '" style="font-size:12px"><img  width="25px" src="https://image.flaticon.com/icons/svg/136/136524.svg">' + name + '(' + totalsize +'mb)</img></div>');
        }
        else {
            //console.log('here=>'+$(input).closest('.uploadDoc').length);
            $(input).closest('.uploadDoc').find(".docErr").slideUp('slow');
            }

        
        
       
       
        //alert("file name: " + name);
    }
});
var fileTypes = ['pdf', 'docx', 'rtf', 'jpg', 'jpeg', 'png', 'txt'];  //acceptable file types
//function readURL(input) {
//    
        
    
//}

