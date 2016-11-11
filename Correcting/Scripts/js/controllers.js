angular.module('starter.controllers', [])
.controller('InstitutionsCtrl', ['$scope', '$state', 'myInsService', function ($scope, $state, myInsService) {
    //加载提示
    $scope.loading = true;
    var promise = myInsService.getEmployeeInstitutions(); // 同步调用，获得承诺接口
    promise.then(function (data) {  // 调用承诺API获取数据 .resolve
        $scope.EmployeeInstitution = data;
        $scope.loading = false;
    });

    $scope.warningdialog = { show: false, content: '' };
    //警告框确定
    $scope.warningdialogconfirm = function () {
        $scope.warningdialog.show = false;
        $scope.warningdialog.content = '';
    }
    $scope.redirectdetail = function (item) {
        var promiseCheckCanEdit = myInsService.CheckCanEdit(item.Id);
        promiseCheckCanEdit.then(function (data) {
            if (data.Error) {
                $scope.warningdialog.show = true;
                $scope.warningdialog.content = data.Message;
            } else {
                $state.go("detail", { id: item.Id, reload: true });
            }
        });
    }
}])
.controller('DetailCtrl', ['$scope', '$state', '$stateParams', 'myInsService', 'cacheService', '$timeout', function ($scope, $state, $stateParams, myInsService, cacheService, $timeout) {
    //加载提示
    $scope.loading = false;
    $scope.tips = false;
    //新增的医院
    $scope.CreateInstitution = {};
    if ($stateParams.reload) {
        var promise = myInsService.getInstitution($stateParams.id);
        promise.then(function (data) {
            $scope.Institution = data;
            cacheService.setRootIns(data);
        });
    } else {
        $scope.Institution = cacheService.getRootIns();
        $scope.CreateInstitution = cacheService.getCreateIns();
    }

    //医院等级选项
    $scope.levels = [
        { name: '三级特等' },
        { name: '三级甲等' },
        { name: '三级乙等' },
        { name: '三级丙等' },
        { name: '二级甲等' },
        { name: '二级乙等' },
        { name: '二级丙等' },
        { name: '一级甲等' },
        { name: '一级乙等' },
        { name: '一级丙等' },
    ];
    //医院性质选项
    $scope.natures = [
        { name: '公立' },
        { name: '民营' }
    ];
    //医院属性选项
    $scope.attributes = [
        { name: '普通医院' },
        { name: '厂矿医院' },
        { name: '军警医院' }
    ];
    //机构类型选项
    $scope.insTypes = [
        { name: '综合医院' },
        { name: '专科医院' },
        { name: '基层医疗机构' }
    ];
    $scope.selectLocation = function () {
        $state.go('selectLocation', { actiontype: $stateParams.actiontype });
    }
    //新增分院的保存
    $scope.createsave = function () {
        var error = false;
        if ($scope.CreateInstitution.Name == null || $scope.CreateInstitution.Name.trim().length == 0) {
            $scope.warningdialog.content = "单位名称不能为空";
            error = true;
        }
        if ($scope.CreateInstitution.LocationCode == null || $scope.CreateInstitution.LocationCode.trim().length == 0) {
            $scope.warningdialog.content = "省份区县不能为空";
            error = true;
        }
        $scope.warningdialog.show = error;
        if (!error) {
            //选中添加至分院List              
            var doit = function () {
                $scope.tips = true;
                $scope.Institution.Childrens.push({
                    Id: $scope.CreateInstitution.Id,
                    Name: $scope.CreateInstitution.Name,
                    Checked: true
                });
                $timeout(function () {
                    $scope.tips = false;
                    $state.go('detail', { reload: false });
                }, 1000)
            }
            if ($scope.Institution.Childrens.length == 0) {
                if ($scope.Institution.Name != $scope.CreateInstitution.Name) {
                    doit();
                }

            } else {
                for (var i = 0; i < $scope.Institution.Childrens.length; i++) {
                    if ($scope.Institution.Childrens[i].Name == $scope.CreateInstitution.Name) {
                        break;
                    }
                    if ($scope.Institution.Parent != null) {
                        if ($scope.Institution.Parent.Id == $scope.CreateInstitution.Id) {
                            break;
                        }
                    }
                    if (i == $scope.Institution.Childrens.length - 1) {
                        doit();
                    }
                }
            }
        }
    }
    //提交保存到数据库
    $scope.save = function () {
        var error = false;
        if ($scope.Institution.Name == null || $scope.Institution.Name.trim().length == 0) {
            $scope.warningdialog.content = "单位名称不能为空";
            error = true;
        }
        if ($scope.Institution.LocationCode == null || $scope.Institution.LocationCode.trim().length == 0) {
            $scope.warningdialog.content = "省份区县不能为空";
            error = true;
        }
        $scope.warningdialog.show = error;
        if (!error) {
            $scope.loading = true;
            var promise = myInsService.postInstitution($scope.Institution);
            promise.then(function (data) {
                if (!data.Error) {
                    $scope.loading = false;
                    $scope.tips = true;
                    $timeout(function () {
                        $state.go('institutions');
                    }, 2000)
                } else {
                    $scope.warningdialog.show = true;
                    $scope.warningdialog.content = data.Message;
                }

            });
        }
    }
    //移除分院
    $scope.removechildren = function (item) {
        $scope.dialog.show = true;
        $scope.dialog.item = item;
        $scope.dialog.title = "确认是否移除分院";
        $scope.dialog.content = item.Name;
    }
    //确认框
    $scope.dialog = {
        show: false,
        title: '',
        content: '',
        item: {}
    };
    $scope.dialogcannel = function () {
        $scope.dialog.show = false;
        $scope.dialog.item.Checked = true;
    }
    //移除分院确定
    $scope.dialogconfirm = function () {
        $scope.dialog.show = false;
        for (var i = 0; i < $scope.Institution.Childrens.length; i++) {
            if ($scope.Institution.Childrens[i] == $scope.dialog.item) {
                $scope.Institution.Childrens.splice(i, 1);
                break;
            }
        }
    }
    //警告框
    $scope.warningdialog = {
        show: false,
        content: ''
    };
    //警告框确定
    $scope.warningdialogconfirm = function () {
        $scope.warningdialog.show = false;
        $scope.warningdialog.content = '';
    }
}])
.controller('SelectInsCtrl', ['$scope', '$state', '$timeout', 'cacheService', 'myInsService', function ($scope, $state, $timeout, cacheService, myInsService) {
    //选中的root机构     
    $scope.rootIns = cacheService.getRootIns();
    $scope.cancel = function () {
        $state.go('detail', { reload: false });
    }
    //加载提示
    $scope.loading = false;
    //完成提示
    $scope.tips = false;
    $scope.searchkey = '';
    $scope.opensearch = function () {
        $scope.shouldBeOpen = true;
    };
    $scope.clearkey = function () {
        $scope.searchkey = ''
    }
    $scope.cancelsearch = function () {
        $scope.searchkey = ''
        $scope.shouldBeOpen = false;
        $scope.searchresult = {};
    }
    //搜索的结果列表
    $scope.searchresult = {};
    $scope.search = function () {
        if ($scope.searchkey != null) {
            $scope.loading = true;
            var promise = myInsService.getSearchInstitutions($scope.searchkey, '');
            promise.then(function (parents) {
                $scope.searchresult = parents;
                $scope.loading = false;
            });
        }
    }
    //下拉翻页
    $scope.turned = function () {
        if ($scope.searchresult.Page != $scope.searchresult.PageCount && $scope.searchresult.InstitutionModels.length != 0) {
            $scope.searchresult.Page++;
            $scope.loading = true;
            var promise = myInsService.getSearchInstitutions($scope.searchkey, $scope.searchresult.Page);
            promise.then(function (parents) {
                for (var i = 0; i < parents.InstitutionModels.length; i++) {
                    if (i == parents.InstitutionModels.length - 1) {
                        $scope.loading = false;
                    }
                    $scope.searchresult.InstitutionModels.push(parents.InstitutionModels[i]);
                }

            });
        }
    }
    //选中总院
    $scope.selected = function (model) {
        for (var i = 0; i < $scope.rootIns.Childrens.length; i++) {
            if ($scope.rootIns.Childrens[i].Name == model.Name) {
                break;
            }
            if ($scope.rootIns.Parent != null) {
                if ($scope.rootIns.Parent.Id == model.Id) {
                    break;
                }
            }
            if (i == $scope.rootIns.Childrens.length - 1) {
                $scope.tips = true;
                $scope.rootIns.Parent = {
                    Id: model.Id,
                    Name: model.Name
                }
                $timeout(function () {
                    $scope.searchresult = {};
                    $scope.tips = false;
                    $state.go('detail', { reload: false });
                }, 1000)
            }
        }
    }
    //删除总院
    $scope.deleteparent = function () {
        $scope.tips = true;
        $scope.rootIns.Parent = null;
        $timeout(function () { $scope.tips = false; }, 1000)
    }
    //选中添加至分院List
    $scope.pushtochildren = function (model) {
        var doit = function () {
            $scope.tips = true;
            $scope.rootIns.Childrens.push({
                Id: model.Id,
                Name: model.Name,
                Checked: true
            });
            $timeout(function () {
                $scope.searchresult = {};
                $scope.tips = false;
                $state.go('detail', { reload: false });
            }, 1000)
        }
        if ($scope.rootIns.Childrens.length == 0 && $scope.rootIns.Id != model.Id && $scope.rootIns.Name != model.Name) {
            doit();
        } else {
            for (var i = 0; i < $scope.rootIns.Childrens.length; i++) {
                if ($scope.rootIns.Childrens[i].Name == model.Name) {
                    break;
                }
                if ($scope.rootIns.Parent != null) {
                    if ($scope.rootIns.Parent.Id == model.Id) {
                        break;
                    }
                }
                if (i == $scope.rootIns.Childrens.length - 1) {
                    doit();
                }
            }
        }
    }
    //新增分院
    $scope.createchildren = function () {
        var createIns = {
            Parent: {
                Id: $scope.rootIns.Id,
                Name: $scope.rootIns.Name
            }
        }
        cacheService.setCreateIns(createIns);
        $state.go('create', { reload: false });
    }
}])
.controller('SelectLocationCtrl', ['$scope', '$state', 'cacheService', 'locationService', '$stateParams', function ($scope, $state, cacheService, locationService, $stateParams) {
    console.log($stateParams.actiontype);
    var rootIns = cacheService.getRootIns();
    $scope.CreateInstitution = cacheService.getCreateIns();
    //加载提示
    $scope.loading = true;
    //选择范围
    $scope.SelectedLocations = [];
    //顶部标签
    $scope.Tabs = [];
    var promise = locationService.getLocations();
    promise.then(function (locations) {  // 调用承诺API获取数据 .resolve
        $scope.Locations = locations;
        $scope.SelectedLocations = locations;
        $scope.loading = false;
    });
    $scope.cancel = function () {
        if ($stateParams.actiontype == 'root') {
            $state.go('detail', { reload: false });
        } else {
            $state.go('create', { reload: false });
        }

    }
    //切换标签
    $scope.tab = function (item) {
        if (item.ParentId == null) {
            $scope.SelectedLocations = $scope.Locations;
            $scope.Tabs = [];
        } else {
            var index = $scope.Tabs.indexOf(item);
            $scope.Tabs.splice(index, 1);
            $scope.SelectedLocations = $scope.Tabs[0].SubModels
        }
    }
    //选择地理
    $scope.select = function (item) {
        if (item.SubModels == null) {
            if ($stateParams.actiontype == 'root') {
                rootIns.LocationCode = item.Id;
                rootIns.LocationName = item.FullName;
                $state.go('detail', { reload: false });
            } else {
                $scope.CreateInstitution.LocationCode = item.Id;
                $scope.CreateInstitution.LocationName = item.FullName;
                $state.go('create', { reload: false });
            }

        } else {
            $scope.SelectedLocations = item.SubModels;
            $scope.Tabs.push(item);
        }
    }
}])
.controller('ScoreBoardCtrl', ['$scope', '$state', 'myInsService', function ($scope, $state, myInsService) {
    //加载提示
    $scope.loading = true;
    var promise = myInsService.getScoreBoards(); // 同步调用，获得承诺接口
    promise.then(function (data) {  // 调用承诺API获取数据 .resolve
        $scope.ScoreBoards = data;
        $scope.loading = false;
    });
}]);
