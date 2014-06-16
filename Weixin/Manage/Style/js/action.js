﻿//文件名：action.js
//描述：权限管理
//时间：2013-09-29
//创建人：杨良斌

$(function() {
    GetList();
});
var url = '/Manage/Ajax/Sys.ashx';
//获取资源列表
function GetList() {
    var queryParams = { 'action': 'GetActionList' };
    var tab = $('#ListTable');
    tab.treegrid({
        title: '栏目列表',
        url: dealAjaxUrl(url),
        idField: 'ID',
        treeField: 'Name',
        animate: true,
        fitColumns: false,
        showFooter: true,
        frozenColumns:[[
            { field: 'ID', width: 30, align: '', checkbox: true },
            { field: 'Name', title: '名称', width: 150, align: '' },
            { field: 'Code', title: '编码', width: 120, align: 'center' },
        ]],
        columns: [[
            { field: 'Type', title: '类别', width: 80, align: 'center', formatter: GetType },
            { field: 'Link', title: '链接', width: 150, align: 'center' },
            { field: 'Action', title: '事件', width: 150, align: 'center' },
            { field: 'Icon', title: '图标', width: 100, align: 'center' },
            { field: 'ParentCode', title: '上一级编码', width: 120, align: 'center' },
            { field: 'Sort', title: '排序号', width: 100, align: 'center' },
            { field: 'Disabled', title: '禁用', width: 100, align: 'center', formatter: GetStatus }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect: true,
        onContextMenu: OnContextMenu
    });
}
//资源类别
function GetType(v) {
    if (v == 1)
        return '分栏';
    else if (v == 2)
        return '菜单';
    else if (v == 3)
        return '功能';
}
//添加
function Add() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        AddItem(false);
    }
    else
        AddItem(true);
}
//新增项（isRoot为true则添加根项，false则添加子项）
function AddItem(isRoot) {
    var rows = GetSelectValue('ListTable');
    if (isRoot) {
        OpenWin('添加资源', 450, 430, '/Manage/Sys/ActionAdd.aspx');
    }
    else {
        if (rows.length != 1) {
            ShowMsg('操作提示', '请选择一条记录！');
            return;
        }
        OpenWin('修改资源', 450, 430, '/Manage/Sys/ActionAdd.aspx?ParentCode=' + rows[0].Code);
    }
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改资源', 450, 430, '/Manage/Sys/ActionAdd.aspx?ID=' + id);
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
                    data: { 'action': 'DeleteAction', 'ID': id },
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
//右键菜单
function OnContextMenu(e, row) {
    e.preventDefault();
    $(this).treegrid('unselectAll');
    $(this).treegrid('select', row.ID);
    $('#mm').menu('show', {
        left: e.pageX,
        top: e.pageY,
        onClick: function(item) {//点击事件
            clickMenu(item.id);
        }
    });
}
//菜单相关操作
function clickMenu(action) {
    //执行对应事件
    switch (action) {
        case "refresh":
            GetList();
            break;
        case "add-root":
            AddItem(true);
            break;
        case "add-node":
            AddItem(false);
            break;
        case "edit":
            Edit();
            break;
        case "delete":
            Delete();
            break;
        case "expand-all":
            ExpandAll();
            break;
        case "collapse-all":
            CollapseAll();
            break;
    }
}

//关闭所有节点
function CollapseAll() {
    $('#ListTable').treegrid('collapseAll');
}
//展开所有节点
function ExpandAll() {
    $('#ListTable').treegrid('expandAll');
}

