app.service("particleSellService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getParticleSellInfo: getParticleSellInfo,
        getStock:getStock,
        save: save,
        edit: edit,
        deleteSell: deleteSell
    };
    return dataSvc;

    function getParticleSellInfo() {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/GetParticleInfo', {});
        } catch (e) {
            throw e;
        }
    }

    function getStock() {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/GetStock', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objParticleInfo) {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/SaveParticleInfo', objParticleInfo);
        } catch (e) {
            throw e;
        }
    }

    function edit(objParticleInfo) {
        try {
            return baseDataSvc.save('/ParticleSell/EditParticleInfo', objParticleInfo);
        } catch (e) {
            throw e;
        }
    }
    function deleteSell(objParticleInfo) {
        try {
            return baseDataSvc.remove('/ParticleSell/DeleteParticleInfo', objParticleInfo, "ID");
        } catch (e) {
            throw e;
        }
    }
}]);