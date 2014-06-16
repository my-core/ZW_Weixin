<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WechatAdd.aspx.cs" Inherits="Weixin.Web.Manage.Merchant.WechatAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="infotable" >
            <tr>
                <td class="tr w100">类型：</td>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblType">
                        <asp:ListItem Value="0">订阅号</asp:ListItem>
                        <asp:ListItem Value="1">服务号</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tr w100">名称：</td>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr w100">微信号：</td>
                <td><asp:TextBox ID="txtWechatNo" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入微信号"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr w100">密码：</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入密码"></asp:TextBox>
                    <span style="color:Red;">为空则不修改</span>
                </td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </form>
</body>
</html>
