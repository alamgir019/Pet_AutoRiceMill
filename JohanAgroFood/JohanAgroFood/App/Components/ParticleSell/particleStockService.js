app.service("particleStockSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getParticleStock: getParticleStock,
        getStock:getStock,
        save: save,
        edit: edit,
        deleteParticleStock: deleteParticleStock
    };
    return dataSvc;

    function deleteParticleStock(particle) {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/DeleteParticleStock', { particleStock: particle });
        } catch (e) {
            throw e;
        }
    }

    function edit(particle)
    {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/EditParticleStock', { particleStock: particle });
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

    function getParticleStock() {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/GetParticleStock', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objParticleStock) {
        try {
            return baseDataSvc.executeQuery('/ParticleSell/SaveParticleStock', objParticleStock);
        } catch (e) {
            throw e;
        }
    }
}]);