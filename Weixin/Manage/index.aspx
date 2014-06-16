<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Weixin.Web.Manage.index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Weixin_Demo</title>
    <script src="Style/js/menu.js" type="text/javascript"></script>
    <style>
        .accordion{ border:none; background:#D5E9F2}
        .layout-split-south {border:none;}
        .layout-split-west {border:none;}
        .panel-body-noheader{ border:none;}
        
        
        .ul{ margin:0px; padding:0px; list-style:none; width:100%; text-align:center; }
        .ul li{ line-height:25px; cursor:pointer; margin-top:1px;}
        .ul li:hover{ background:#195D9A; color:#fff;}
        .ul li.current{ background:#195D9A; color:#fff;}
    </style>
</head>
<body class="easyui-layout">
    <!--顶部-->
	<div data-options="region:'north'" style="height:75px; background:#00A2CA">
	    <div>&nbsp;&nbsp;<img height="70px" src="logo.png" /></div>
    </div>
    <!--底部-->
	<div data-options="region:'south',split:true" style="height:30px;  background:#00A2CA;"><div style=" text-align:center; line-height:30px; color:#fff;">湛微科技 @ 1988-2014</div></div>
	<!--左侧菜单-->
	<div data-options="region:'west',split:true" title="系统菜单" style="width:180px;">
	    <div class="easyui-accordion" data-options=" fit:true" >
	        <%=StrMenu %>
	    </div>
	</div>
	<!--主内容-->
	<div data-options="region:'center',title:''">
	    <div id="nav_tabs" class="easyui-tabs" fit="true" border="false">
            <div title="欢迎使用" style="padding: 20px; overflow: hidden;">
                <div style=" font-size:36px; font-weight:bold; text-align:center; margin-top:200px; color:#0A64A4;">欢迎登录微信公众开发平台</div>
            </div>
        </div>
	</div>
	<!--右键菜单-->
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="tabupdate"> 刷新</div>
        <div class="menu-sep"></div>
        <div id="close">关闭</div>
        <div id="closeall">全部关闭</div>
        <div id="closeother">除此之外全部关闭</div>
        <div class="menu-sep"></div>
        <div id="closeright">当前页右侧全部关闭</div>
        <div id="closeleft">当前页左侧全部关闭</div>
        <div class="menu-sep"> </div>
        <div id="exit">退出</div>
    </div>
</body>
</html>