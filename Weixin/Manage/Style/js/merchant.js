//文件名：Merchant.js
//描述：商户管理
//时间：2014-06-09
//创建人：杨良斌

$(function() {
    GetList();
});
var url = '/Manage/Ajax/Merchant.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetMerchantList', 'Name': $('#txtName').val(), 'Status': $('#Status').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '商户列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center',checkbox: true },
                        { field: 'Name', title: '商户名称', width: 100, align: 'center' },
                        { field: 'MobileNo', title: '联系手机', width: 100, align: 'center' },
                        { field: 'QQ', title: '常用QQ', width: 60, align: 'center' },
                        { field: 'Email', title: '常用Email', width: 120, align: 'center' },
                        { field: 'Address', title: '商户地址', width: 120, align: 'center'},
                        { field: 'Status', title: '状态', width: 60, align: 'center', formatter: GetStatus },
                        { field: 'CreateAdmin', title: '创建人', width: 80, align: 'center', formatter: FormatTime },
                        { field: 'CreateTime', title: '创建时间', width: 120, align: 'center', formatter: FormatTime },
                        { field: 'UpdateAdmin', title: '修改人', width: 80, align: 'center', formatter: FormatTime },
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
    OpenWin('添加商户', 480, 430, '/Manage/Merchant/MerchantAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改商户', 480, 430, '/Manage/Merchant/MerchantAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteMerchant&ID=' + id,
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