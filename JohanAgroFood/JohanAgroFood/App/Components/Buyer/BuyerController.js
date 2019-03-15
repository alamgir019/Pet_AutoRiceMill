app.controller("BuyerCtrl",["$scope", "buyerService","$filter", function ($scope, buyerService,$filter) {

    GetAll();
    //GetAllRetailerType();
    //GetAllDistrict();
    //GetAllThana();
    //GetAllZone();
    //GetAllLocation();
    //GetAllRoute();

    // GetAllDistrict();
    //To Get All Records  
    function GetAll() {
        var fromDate = $scope.fromDate;
        var toDate = $scope.toDate;
        var Data = buyerService.getSalesReportSrWise();
        Data.then(function (sireport) {
            $scope.retailersforgridview = sireport.data;
        }, function () {
            alert('Error');
        });
    }


    //var myDateTemplate = '<input ui-date="{ dateFormat: \'dd mm yyyy\' }" ui-date-format="dd mm yyyy" />';
 
    $scope.gridOptions = {
        paginationPageSizes: [15, 20, 25],
        paginationPageSize: 25,
        data: 'retailersforgridview',
        columnDefs: [
          { field: 'SR', displayName: 'Sr Name', width: 100 },
          { field: 'INVOICENO', displayName: 'Invoice No' },
          { field: 'RETAILERNAME', displayName: 'Retailer Name',width:140 },
          { field: 'PRODUCTNAME', displayName: 'Product Name' },
          { field: 'COLOR', displayName: 'Color',width:90},
          { field: 'QTY', displayName: 'Quantity', width: 100 },
          { field: 'RATE', displayName: 'Rate', width: 100 },
          { field: 'PRICE', displayName: 'Price', width: 100 },
          { field: 'INVOICEDATE', displayName: 'Invoice Date', width: 140 }  //cellFilter: 'date:\'MM/dd/yyyy HH:MM:SS Z\''
        ],
        filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
        enableFiltering: true
    };

  
  

    //$scope.divEmpModification = false;
    // GetAll();
    //GetAllRetailerType();

    //$scope.ShowAll = function () {
    //    GetAll();
    //}

    $scope.SearchById = function () {
        var retailerId = $scope.retailerId;

        var Data = retailerService.getRetailerById(retailerId);
        Data.then(function (retailer) {
            $scope.retailers = retailer.data;
        }, function () {
            alert('ডাটা পাওয়া যায়নি।');
        });
    }
    
    $scope.GetThanaByDistrictId = function () {
        var districtId = $scope.DISTRICTID;
        var Data = retailerService.getThanaByDistrictId(districtId);
        Data.then(function (thana) {
            $scope.Thanas = thana.data;
        }, function () {
            alert("ডাটা পাওয়া যায়নি।");
        });
    }
    //CE-150325-ANIS
    $scope.GetLocationByDistrictId = function ()
    {
        var districtId = $scope.DISTRICTID;
        var Data = retailerService.getLocationByDistrictId(districtId);
        Data.then(function (location) {
            $scope.Locations = location.data;
        }, function () {
            alert("Fetch location Data Error");
        });
    }
    $scope.GetRouteByDistrictId = function () {
        var districtId = $scope.DISTRICTID;
        var Data = retailerService.getRouteByDistrictId(districtId);
        Data.then(function (route) {
            $scope.Routes = route.data;
        }, function () {
            alert("Fetch route Data Error");
        });
    }
    //CE-150325-ANIS

    //$scope.GetLocationByDistrictId = function () {
    //    var districtId = $scope.DISTRICTID;
    //    var Data = retailerService.getLocationByDistrictId(districtId);
    //    Data.then(function (thana) {
    //        $scope.Thanas = thana.data;
    //    }, function () {
    //        alert("ডাটা পাওয়া যায়নি।");
    //    });
    //}

    $scope.edit = function (retailer) {
        $scope.RETAILERID = retailer.RETAILERID;
        $scope.RETAILERCODE = retailer.RETAILERCODE;
        $scope.RETAILERNAME = retailer.RETAILERNAME;
        $scope.RETAILERADDRESS = retailer.RETAILERADDRESS;
        $scope.RETAILERTYPEID = retailer.RETAILERTYPEID;
        $scope.DISTRICTID = retailer.RETAILERDISTRICTID;
        $scope.THANAID = retailer.RETAILERTHANAID;    
        $scope.ZONEID = retailer.RETAILERZONEID;
        $scope.LOCATIONID = retailer.RETAILERLOCATIONID;
        $scope.ROUTEID = retailer.RETAILERROUTEID;
        $scope.RETAILERPERSON = retailer.RETAILERPERSON;
        $scope.RETAILERDESIGNATION = retailer.RETAILERDESIGNATION;
        $scope.RETAILERPHONE = retailer.RETAILERPHONE;
        $scope.RETAILERMOBILE1 = retailer.RETAILERMOBILE1;
        $scope.RETAILERMOBILE2 = retailer.RETAILERMOBILE2;
        $scope.RETAILEREMAIL = retailer.RETAILEREMAIL;
        $scope.RETAILERFAX = retailer.RETAILERFAX;
        $scope.CREATEBY = retailer.CREATEBY;
        $scope.CREATEDATE = retailer.CREATEDATE;
        $scope.MODIFIEDYBY = retailer.MODIFIEDYBY;
        $scope.MODIFIEDDATE = retailer.MODIFIEDDATE;
        $scope.RETAILERSTATUS = retailer.RETAILERSTATUS;

        if (retailer.RETAILERSTATUS == "Y") {
            $scope.RETAILERSTATUS = true;
        }
        else
            $scope.RETAILERSTATUS = false;

        $scope.Operation = "Update";
    }

    $scope.add = function () {
        $scope.RETAILERID = "";
        $scope.RETAILERCODE = "";
        $scope.RETAILERNAME = "";
        $scope.RETAILERADDRESS = "";
        $scope.DISTRICTID = $scope.Districts[-1];
        $scope.THANAID = $scope.Thanas[-1],
        $scope.ZONEID = $scope.Zones[-1],
        $scope.LOCATIONID = $scope.Locations[-1],
        $scope.ROUTEID = $scope.Routes[-1],
        $scope.RETAILERTYPEID = $scope.retailerTypes[-1];

        $scope.RETAILERPERSON = "";
        $scope.RETAILERDESIGNATION = "";
        $scope.RETAILERPHONE = "";
        $scope.RETAILERMOBILE1 = "";
        $scope.RETAILERMOBILE2 = "";
        $scope.RETAILEREMAIL = "";
        $scope.RETAILERFAX = "";
        $scope.CREATEBY = "";
        $scope.CREATEDATE = "";
        $scope.MODIFIEDYBY = "";
        $scope.MODIFIEDDATE = "";
      //  $scope.RETAILERSTATUS = "Y";
        $scope.RETAILERSTATUS = true;
        $scope.Operation = "Add";
        $scope.myForm.RETAILERNAME.$error.required = false;
        $scope.myForm.RETAILERPHONE.$error.required = false;
        $scope.myForm.RETAILERMOBILE1.$error.required = false;
        $scope.divEmpModification = true;
    }

    $scope.Save = function () {
        var Retailer = {
            RETAILERID: $scope.RETAILERID,
            RETAILERCODE: $scope.RETAILERCODE,
            RETAILERNAME: $scope.RETAILERNAME,
            RETAILERADDRESS: $scope.RETAILERADDRESS,
            RETAILERDISTRICTID: $scope.DISTRICTID,
            RETAILERTHANAID: $scope.THANAID,
            RETAILERZONEID: $scope.ZONEID,
            RETAILERLOCATIONID: $scope.LOCATIONID,
            RETAILERROUTEID: $scope.ROUTEID,
            RETAILERTYPEID: $scope.RETAILERTYPEID,
            RETAILERPERSON: $scope.RETAILERPERSON,
            RETAILERDESIGNATION: $scope.RETAILERDESIGNATION,
            RETAILERPHONE: $scope.RETAILERPHONE,
            RETAILERMOBILE1: $scope.RETAILERMOBILE1,
            RETAILERMOBILE2: $scope.RETAILERMOBILE2,
            RETAILEREMAIL: $scope.RETAILEREMAIL,
            RETAILERFAX: $scope.RETAILERFAX,
            CREATEBY: $scope.CREATEBY,
            CREATEDATE: $scope.CREATEDATE,
            MODIFIEDYBY: $scope.MODIFIEDYBY,
            MODIFIEDDATE: $scope.MODIFIEDDATE,
            RETAILERSTATUS: $scope.RETAILERSTATUS
        };
        var Operation = $scope.Operation;
        if (Operation == "Update") { 
            Retailer.RETAILERID = $scope.RETAILERID;
            var getMSG = retailerService.update(Retailer);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.add();
                //$scope.divEmpModification = false;
            }, function () {
                alert('আপডেট হয়নি। ');
            });
        }
        else {
            var getMSG = retailerService.Add(Retailer);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.add();
            }, function () {
                alert('Insert Error');
            });
        }
    }

    $scope.delete = function (retailer) {
        var getMSG = retailerService.Delete(retailer.RETAILERID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

    //CE-150423-ANIS
    $scope.Print = function () {
        debugger;
        //$scope.FromDate = form_date.toDateString("dd/mm/yyyy");
        //var fromdate = $scope.FromDate;
        //$scope.ToDate = new Date();
        //$scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'shortDate'];
        var fromdate = $("#txtFromDate").val();
        var todate = $("#txtToDate").val();
        //var todate = $scope.ToDate.value;
        var getMSG = buyerService.ShowReport(fromdate, todate);
        getMSG.then(function (messagefromController) {
            alert(messagefromController.data);
        }, function () {
            alert('Show Error');
        });
    }
    //CE-Edit-150423-ANIS

    //To Get All District  
    function GetAllDistrict() {
        var Data = retailerService.getDistrict();
        Data.then(function (district) {
            $scope.Districts = district.data;
        }, function () {
            alert('Error');
        });
    }

  //To Get All Thana  
    function GetAllThana() {
        var Data = retailerService.getThana();
        Data.then(function (thana) {
            $scope.Thanas = thana.data;
        }, function () {
            alert('Error');
        });
    }

 //To Get All Zone  
    function GetAllZone() {
        var Data = retailerService.getZone();
        Data.then(function (zone) {
            $scope.Zones = zone.data;
        }, function () {
            alert('Error Zone');
        });
    }

    //To Get All Location  
    function GetAllLocation() {
        var Data = retailerService.getLocation();
        Data.then(function (location) {
            $scope.Locations = location.data;
        }, function () {
            alert('Error Location');
        });
    }

    //To Get All Route  
    function GetAllRoute() {
        var Data = retailerService.getRoute();
        Data.then(function (route) {
            $scope.Routes = route.data;
        }, function () {
            alert('Error Route');
        });
    }

    //$scope.retailerTypes = [{ RETAILERTYPEID: "1", RETAILERTYPENAME: "ABC" },
    //                        { RETAILERTYPEID: "2", RETAILERTYPENAME: "EFG" },
    //                        { RETAILERTYPEID: "3", RETAILERTYPENAME: "HIJ" }];


    function GetAllRetailerType() {
        var Data = retailerService.getRetailerType();
        Data.then(function (retailertype) {
            $scope.retailerTypes = retailertype.data;
        }, function () {
            alert('Retailer Type Load Error');
        });
    }
   // $scope.Districts = [{ RETAILERDISTRICTID: 1, RETAILERDISTRICTNAME: "Dhaka" }, { RETAILERDISTRICTID: 2, RETAILERDISTRICTNAME: "Barisal" }];

    //$scope.Thanas = [{ RETAILERTHANAID: 1, RETAILERTHANANAME: "Gulshan" },{ RETAILERTHANAID: 2, RETAILERTHANANAME: "Badda" }];

    //$scope.Zones = [{ RETAILERZONEID: 1, RETAILERZONENAME: "Gulshan" },{ RETAILERZONEID: 2, RETAILERZONENAME: "Badda" }];


   /* $scope.Locations = [{ RETAILERLOCATIONID: 1, RETAILERLOCATIONNAME: "Gulshan" },{ RETAILERLOCATIONID: 2, RETAILERLOCATIONNAME: "Badda" }];
    $scope.Routes = [{ RETAILERROUTEID: 1, RETAILERROUTENAME: "Gulshan" },
                      { RETAILERROUTEID: 2, RETAILERROUTENAME: "Badda" }];*/

    $scope.divEmpModification = true;
}]);
