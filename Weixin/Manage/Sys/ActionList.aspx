<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionList.aspx.cs" Inherits="Weixin.Web.Manage.Admin.ActionList" %>

<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-<%=AdminTitle %></title>
    <script src="../Style/js/action.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <!--列表-->
    <table id="ListTable" data-options="toolbar:'#tb',iconCls:'icon-tip'">
    </table>
    <!--工具栏-->
    <div id="tb">
        <div>
            <uc1:ToolBar ID="ToolBar1" runat="server" />
        </div>
    </div>
    <!--右键菜单-->
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="refresh"> 刷新</div>
        <div class="menu-sep"></div>
        <div id="add-root">添加(根)</div>
        <div id="add-node">添加(子)</div>
        <div id="edit">编辑</div>
        <div id="delete">删除</div>
        <div class="menu-sep"></div>
        <div id="expand-all">展开全部</div>
        <div id="collapse-all">收起全部</div>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
