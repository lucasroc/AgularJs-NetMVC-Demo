var app = angular.module("veiculosApp", []);
app.controller("veiculosController", ["$scope", "$http", veiculosController]);

function veiculosController($scope, $http) {

    $scope.loading = true;

    $http.get("/api/Veiculo/").then(function (response) {
        $scope.veiculos = response.data;
        $scope.loading = false;
    }, function (response) {
        alert("Ocorreu um erro ao obter os veículos" + response.data.Message);
    });

    //inclui um veículo
    $scope.add = function () {
        $http.post("/api/Veiculo/", this.novoVeiculo).then(function (response) {
            alert("Incluído com sucesso");
            $scope.veiculos.push(response.config.data);
        }, function (response) {
            alert("Ocorreu um erro ao incluir" + response.config.data.Message);
        });
    }

    //atualiza dados do veículo
    $scope.update = function () {
        var veiculoUpdate = this.veiculo;
        $http.put("/api/Veiculo/", veiculoUpdate).then(function (response) {
            alert("Atualizado com sucesso");
        }, function (response) {
            alert("Ocorreu um erro durante a atualização" + response.data.Message);
        });
    }

    //exclui um veículo
    $scope.delete = function () {
        var renavan = this.veiculo.Renavan;
        $http["delete"]("/api/Veiculo/" + renavan).then(function (response) {
            alert("Excluído com sucesso");
            $each($scope.veiculos, function (i) {
                if ($scope.veiculos[i].Renavan === renavan) {
                    $scope.veiculos.splice(i, 1);
                    return;
                }
            });
        }, function (response) {
            alert("Ocorreu um erro durante a exclusão" + response.data.Message);
        });
    }
}
