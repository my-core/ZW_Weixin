<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminList.aspx.cs" Inherits="Weixin.Web.Manage.Admin.AdminList" %>

<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理-<%=AdminTitle %></title>
    <script src="/Manage/Style/js/admin.js" type="text/javascript"></script>
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
            <table class="searchTable">
                <tr>
                    <td>用户名/姓名：<input id="txtName" type="text" class="txt" /></td>
                    <td>
                        状态：
                        <select id="selStatus" class="txt w70">
                            <option value="">全部</option>
                            <option value="1">正常</option>
                            <option value="0">禁用</option>
                        </select>
                    </td>
                    <td>
                        创建时间：
                        <input id="txtMinTime" type="text" class="txt Wdate w100" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />~
                        <input id="txtMaxTime" type="text" class="txt Wdate w100" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                    </td>
                    <td><input type="button" class="searchBtn" value="查询" onclick="GetList()" /></td>
                </tr>
            </table>
        </div>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
