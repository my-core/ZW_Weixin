<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantAdd.aspx.cs" Inherits="Weixin.Web.Manage.Merchant.MerchantAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="infotable" >
            <tr>
                <td style="width:100px;" class="tr">商户名称：</td>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入商户名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px;" class="tr">用户名：</td>
                <td><asp:TextBox ID="txtAccount" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入用户名"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">密码：</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入密码"></asp:TextBox>
                    <span style="color:Red;">为空则不修改</span>
                </td>
            </tr>
            <tr>
                <td class="tr">联系手机：</td>
                <td><asp:TextBox ID="txtMobileNo" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入手机" Validtype="Mobile"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">常用QQ：</td>
                <td><asp:TextBox ID="txtQQ" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入QQ" Validtype="QQ"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">常用Email：</td>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入邮箱" Validtype="Email"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">地址：</td>
                <td><asp:TextBox ID="txtAddress" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入地址"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">状态：</td>
                <td><asp:CheckBox ID="cbStatus" runat="server" Text="禁用" /></td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </form>
</body>
</html>
