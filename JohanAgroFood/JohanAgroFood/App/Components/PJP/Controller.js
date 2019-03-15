app.controller("PjpCtrl", function ($scope, PJPService) {
    GetAllBuyer();
    $scope.divRetailerInformationbySrWise = false;
    //To Get All PJP Records  
    function GetAllPjp() {
        var Data = PJPService.getAllPJP();
        Data.then(function (pjp) {
            $scope.pjps = pjp.data;
        }, function () {
            alert('Error');
        });
    }

    //

    $scope.edit = function (pjp) {
        $scope.PJPID = pjp.PJPID;
        $scope.PJPCODE = pjp.PJPCODE;
        $scope.PJPBUYERID = pjp.PJPBUYERID;
        $scope.PJPSRID = pjp.PJPSRID;
        $scope.RETAILERID = pjp.RETAILERID;
        $scope.RETAILERNAME = pjp.RETAILERNAME;
        $scope.RETAILERADDRESS = pjp.RETAILERADDRESS;
        $scope.RETAILERDISTRICTID = pjp.RETAILERDISTRICTID;
        $scope.RETAILERTHANAID = pjp.RETAILERTHANAID;
        $scope.SA = pjp.SA;
        $scope.SU = pjp.SU;
        $scope.MO = pjp.MO;
        $scope.TU = pjp.TU;
        $scope.WE = pjp.WE;
        $scope.TH = pjp.TH;
        $scope.FR = pjp.FR;
        $scope.CREATEBY = pjp.CREATEBY;
        $scope.CREATEDATE = pjp.CREATEDATE;
        $scope.MODIFIEDYBY = pjp.MODIFIEDYBY;
        $scope.MODIFIEDDATE = pjp.MODIFIEDDATE;
        $scope.Operation = "Update";     
    }
    $scope.add = function () {
        $scope.PJPID ='';
        $scope.PJPCODE = '';
        $scope.PJPBUYERID ='';
        $scope.PJPSRID = '';
        $scope.RETAILERID ='';
        $scope.RETAILERNAME = '';
        $scope.RETAILERADDRESS = '';
        $scope.RETAILERDISTRICTID = '';
        $scope.RETAILERTHANAID = '';
        $scope.SA = '';
        $scope.SU = '';
        $scope.MO = '';
        $scope.TU = '';
        $scope.WE = '';
        $scope.TH ='';
        $scope.FR = '';
        $scope.CREATEBY = '';
        $scope.CREATEDATE = '';
        $scope.MODIFIEDYBY = '';
        $scope.MODIFIEDDATE = '';
        $scope.Operation = "Add";      
    }
    
    //$scope.Operation = "Add";
    $scope.Save = function () {
        //CE-150409-ANIS
        $scope.items = [];
        //$Scope.pjpdates = [];
        //$scope.SASTATUS = $scope.RetailerInformation[i].SASTATUS;
        $scope.SASTATUS = false;
        $scope.SUSTATUS = false;
        $scope.MOSTATUS = false;
        $scope.TUSTATUS = false;
        $scope.WESTATUS = false;
        $scope.THSTATUS = false;
        $scope.FRSTATUS = false;
        for (var i = 0; i < $scope.RetailerInformation.length; i++) {
            $scope.items.push({
                //PJPCODE: $scope.PJPCODE,
                PJPBUYERID: $scope.BuyerId,
                PJPSRID: $scope.SrId,
                RETAILERID: $scope.RetailerInformation[i].RETAILERID,
                RETAILERNAME: $scope.RetailerInformation[i].RETAILERNAME,
                RETAILERADDRESS: $scope.RetailerInformation[i].RETAILERADDRESS,
                RETAILERDISTRICTID: $scope.RetailerInformation[i].RETAILERDISTRICTID,
                RETAILERTHANAID: $scope.RetailerInformation[i].RETAILERTHANAID,
                SA: $scope.RetailerInformation[i].SASTATUS,
                SU: $scope.RetailerInformation[i].SUSTATUS,
                MO: $scope.RetailerInformation[i].MOSTATUS,
                TU: $scope.RetailerInformation[i].TUSTATUS,
                WE: $scope.RetailerInformation[i].WESTATUS,
                TH: $scope.RetailerInformation[i].THSTATUS,
                FR: $scope.RetailerInformation[i].FRSTATUS,
                CREATEBY: $scope.CREATEBY,
                CREATEDATE: $scope.CREATEDATE,
                MODIFIEDYBY: $scope.MODIFIEDYBY,
                MODIFIEDDATE: $scope.MODIFIEDDATE,
            });
        }
        var jpjs = $scope.items;
        
        var Operation = $scope.Operation;
        
        if (Operation == "Update") {
            Pjp.PJPID = $scope.PJPID;
            var getMSG = PJPService.Update(Pjp);
            getMSG.then(function (messagefromController) {
                alert(messagefromController.data);
                $scope.add();
            }, function () {
                alert('আপডেট হয়নি। ');
            });
        }
        else {
            var getMSG = PJPService.Add(jpjs);
            getMSG.then(function (messagefromController) {
                alert(messagefromController.data);
            }, function () {
                alert('Insert Pjp Error');
            });
        }     
    }
    $scope.delete = function (pjp) {
        var getMSG = PJPService.Delete(pjp.PJPID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }
    //To Get All Distributor/Buyer   
    function GetAllBuyer() {
        var Data = PJPService.getBuyer();
        Data.then(function (buyer) {
            $scope.Buyers = buyer.data;
        }, function () {
            alert('Error Buyer');
        });
    }
    //CE-150408-ANIS
    $scope.GetSrByBuyerId = function () {
        var buyerId = $scope.BuyerId;
        var Data = PJPService.getSrByBuyerId(buyerId);
        Data.then(function (sr) {
            $scope.SRs = sr.data;
        }, function () {
            alert("ডাটা পাওয়া যায়নি।");
        });
    }
    //CE-150408-ANIS

    //CE-150409-ANIS
    $scope.GetRetailerInformationBySrId = function () {
        var ID = $scope.SrId;
        var Data = PJPService.getRetailerInformationBySrId(ID);
        Data.then(function (retailerinformation) {
            $scope.RetailerInformation = retailerinformation.data;
            $scope.divRetailerInformationbySrWise = true;
        }, function () {
            alert("ডাটা পাওয়া যায়নি।");
        });
    }

    //CE-150409-ANIS

});