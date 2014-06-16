//文件名：master.js
//描述：用户管理
//时间：2013-09-29
//创建人：杨良斌

$(function() {
    GetList();
});
var url = '/Manage/Ajax/Sys.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetAdminList', 'Name': $('#txtName').val(), 'Status': $('#selStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '用户列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'UserName', title: '用户名', width: 100, align: 'center' },
                        { field: 'AdminName', title: '姓名', width: 100, align: 'center' },
                        { field: 'Status', title: '状态', width: 60, align: 'center', formatter: GetStatus },
                        { field: 'CreateTime', title: '创建时间', width: 120, align: 'center', formatter: FormatTime },
                        { field: 'UpdateTime', title: '修改时间', width: 120, align: 'center', formatter: FormatTime }
                        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: true,
        pageSize: 20,
        nowrap: false
    });
    //设置分页
    SetPager(tab);
}
//添加
function Add() {
    OpenWin('添加用户', 430, 330, '/Manage/Sys/AdminAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改用户', 430, 330, '/Manage/Sys/AdminAdd.aspx?ID=' + id);
    }
    else {
        AlertInfo('操作提示', '请选择一条要修改的记录！');
    }
}
//删除
function Delete() {
    var code = '';
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        $.messager.confirm('操作提示', '是否确认删除操作？', function(r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteMaster&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function(data) {
                        if (data.res == 0) {
                            AlertInfo('操作提示', '删除成功！');
                            GetList();
                        }
                        else {
                            AlertInfo('操作提示', '该数据已在系统中使用，不能删除！');
                        }
                    }
                });
            }
        });
    }
    else {
        AlertInfo('操作提示', '请选择要删除的记录');
    }
}