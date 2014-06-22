//文件名：wechat.js
//描述：微信号管理
//时间：2014-06-09
//创建人：杨良斌

$(function() {
    GetList();
});
var url = '/Manage/Ajax/Merchant.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetWechatList', 'WechatNo': $('#txtWechatNo').val(), 'MerchantName': $('#txtMerchantName').val(), 'Type': $('#selType').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '微信号列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center',checkbox: true },
                        { field: 'Type', title: '类别', width: 100, align: 'center', formatter: GetWechatType },
                        { field: 'WechatNo', title: '微信号', width: 100, align: 'center' },
                        { field: 'Token', title: '令牌', width: 60, align: 'center' },
                        { field: 'ApiUrl', title: '接口地址', width: 120, align: 'center' },
                        { field: 'ValidDate', title: '校验时间', width: 120, align: 'center'},
                        { field: 'TextLimitCount', title: '文本限制数/月', width: 110, align: 'center' },
                        { field: 'ImageTextLimitCount', title: '图文限制数/月', width: 110, align: 'center' },
                        { field: 'RequestLimitCount', title: '请求限制数/月', width: 110, align: 'center' },
                        { field: 'RequestCount', title: '请求总数/月', width: 110, align: 'center' },
                        { field: 'AppID', title: '操作', width: 110, align: 'center',formatter:GetAction },
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
//微信号类别
function GetWechatType(v) {
    if (v == 1) {
        return '订阅号'
    }
    return '服务号';
}
//操作
function GetAction(v, row) {
    return '<a href="/manage/weindex.aspx?id=' + row.ID + '" target="parent">进入管理</a>';
}
//添加
function Add() {
    OpenWin('添加微信号', 480, 430, '/Manage/Merchant/WechatAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改微信号', 480, 430, '/Manage/Merchant/WechatAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteWechat&ID=' + id,
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