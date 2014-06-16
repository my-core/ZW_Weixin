//文件名：menu.js
//描述：用于点击菜单时，在主框架中显示对应内容（页面）、选项卡上下文菜单功能
//时间：2014-05-30

var index = 0;
function add(url, title, obj) {
    setCurrentMenu(obj);
    if (isTabExist(title))
        return;
    //调用EasyUItabs方法添加选项卡
    $('#nav_tabs').tabs('add', {
        title: title,
        closable: true,
        content: "<iframe scrolling=\"auto\" frameborder=\"0\" src=\"" + url + "\" style=\"width:100%; height:100%;\"></iframe>"
    });
    initTabEvent();
}
//选项卡是否存在，存在则选中并返回true,不存在则
function isTabExist(title) {
    var flag = $('#nav_tabs').tabs('exists', title);
    if (flag) {
        refreshTabs(title);
        return true;
    }
    else
        return false;
}
//刷新选项卡
function refreshTabs(title) {
    $('#nav_tabs').tabs('select', title);
    var currTab = $('#nav_tabs').tabs('getTab', title);
    var iframe = $(currTab.panel('options').content);
    var url = iframe.attr('src');
    if (url) {
        $('#nav_tabs').tabs('update', { tab: currTab, options: { content: "<iframe scrolling=\"auto\" frameborder=\"0\" src=\"" + url + "\" style=\"width:100%; height:100%;\"></iframe>"} });
    }
}
function setCurrentMenu(obj) {
    $('li').removeClass('current');
    $(obj).addClass('current');
}
//初始化tab双击、右键事件
function initTabEvent() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function() {

        var title = $(this).children(".tabs-closable").text();
        $('#nav_tabs').tabs('close', title);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function(e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY, 
            onClick: function(item) {//点击事件
                clickMenu(item.id);
            }
        });
        var title = $(this).children(".tabs-closable").text();
        $('#mm').data("currTab", title);
        $('#nav_tabs').tabs('select', title);
        return false;
    });
}
//菜单相关操作
function clickMenu(action) {
    var allTab = $('#nav_tabs').tabs('tabs'); //选项卡集合
    var currTab = $('#nav_tabs').tabs('getSelected'); //当前激活选项卡
    var currTitle = $('#mm').data("currTab"); //当前激活选项卡标题
    var tabsTitle = []; //选项标题集合
    //获取标题集合
    $.each(allTab, function(index, item) {
        if($(item).panel('options').title!='欢迎使用')
            tabsTitle.push($(item).panel('options').title);
    });
    //执行对应事件
    switch (action) {
        case "refresh":
            refreshTabs(currTitle);
            break;
        case "close":
            $('#nav_tabs').tabs('close', currTitle);
            break;
        case "closeall":
            $.each(tabsTitle, function(index, item) {
                $('#nav_tabs').tabs('close', item);
            });
            break;
        case "closeother":
            $.each(tabsTitle, function(index, item) {
                if (item != currTitle) {
                    $('#nav_tabs').tabs('close', item);
                }
            });
            break;
        case "closeright":
            var tabIndex = $('#nav_tabs').tabs('getTabIndex', currTab);
            if (tabIndex == allTab.length - 1) {
                return false;
            }
            $.each(tabsTitle, function(index, item) {
                if (index > tabIndex) {
                    $('#nav_tabs').tabs('close', item);
                }
            });
            break;
        case "closeleft":
            var tabIndex = $('#nav_tabs').tabs('getTabIndex', currTab);
            if (tabIndex == 1) {
                return false;
            }
            $.each(tabsTitle, function(index, item) {
                if (index < tabIndex) {
                    $('#nav_tabs').tabs('close', item);
                }
            });
            break;
        case "exit":
            $('#mm').menu('hide');
            break;
    }
}