//使用Window.Open打开一个新窗体URL（不带滚动条）
function myOpenURL(url1, height1, width1) {
    var top1 = (window.screen.availHeight - height1)/2;
    var left1 = (window.screen.availWidth - width1)/2;
    window.open(url1, "", "location=no,Status=no,scrollbars=no,resizable=no,left=" + left1 + ",top=" + top1 + ",width=" + width1 + ",height=" + height1);
}

function myOpenURLForPrint(url1) {
    var top1 = 100;
    var left1 = 150;
    var height1 = 500;
    var width1 = 800;
    window.open(url1, "", "location=no,Status=no,menubar=yes,scrollbars=yes,resizable=yes,left=" + left1 + ",top=" + top1 + ",width=" + width1 + ",height=" + height1);
}

//使用Window.Open打开一个新窗体URL（带滚动条）
function myOpenURLWithScroll(url1, height1, width1) {
    var top1 = (window.screen.availHeight - height1) / 2;
    var left1 = (window.screen.availWidth - width1) / 2;
    window.open(url1, "", "location=no,Status=no,scrollbars=yes,resizable=yes,left=" + left1 + ",top=" + top1 + ",width=" + width1 + ",height=" + height1);
}

//以屏幕最大尺寸打开窗口（带滚动条）
function myBigOpenURL(url1) {
    var top1 = window.screen.availHeight-40;
    var left1 = window.screen.availWidth -40;
    myOpenURLWithScroll(url1, top1, left1);
}

//使用模态窗口打开一个新窗体URL
function myShowModalDialog(url1, height1, width1) {
    var top1 = (window.screen.availHeight - height1) / 2;
    var left1 = (window.screen.availWidth - width1) / 2;
    var features = "dialogLeft:" + left1 + "px;dialogTop:" + top1 + "px;dialogHeight:" + height1 + "px;dialogWidth:" + width1 + "px;help:no;status:no;";
    return window.showModalDialog(url1, "", features);
}

function addFav() {   // 加入收藏夹
    if (document.all) {
        try
        {
            window.external.addFavorite(window.location.href, document.title);
        }
        catch(e)
        {
            try
            {
               window.external.addToFavoritesBar(window.location.href, document.title);  
            }
            catch(e)
            {
                alert("加入收藏失败，请您手工加入。");
            }
        }
    } else if (window.sidebar) {
        window.sidebar.addPanel(document.title, window.location.href, "");
    }
}

function setHomepage() {   // 设置首页
    if (document.all) {
        document.body.style.behavior = 'url(#default#homepage)';
        document.body.setHomePage(window.location.href);
    } else if (window.sidebar) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true");
            }
        }
        var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
        prefs.setCharPref('browser.startup.homepage', window.location.href);
    }
}

//得到当前URL中某参数的值
function getMyPara(pname)
{       
    var para = new Array();
    var reg =new RegExp("("+pname+"=\\S*&)|("+pname+"=\\S*)","ig");
    var    str = "";       
    if(arguments.length>1&arguments[1]!="")
    {
        str = arguments[1];
    }
    else
    {
        str = document.location.href;
    }
    var result = str.match(reg);       
    if(result==null||result.length==0) return para;
    for(var i=0; i<result.length; i++)
    {
        reg = new RegExp("[^("+pname+"=)](\\S*)([^&])","ig");
        para.push(result[i].match(reg));           
    }
    return para;
}       

//全部选中（不选）某组CheckBoxList
function selectAllDocument(selID,value1)
{
    var cons1=document.getElementsByName(selID);
    for(var i=0;i<cons1.length;i++)
    {
        cons1[i].checked=value1;
    }
}

//执行全选的成批操作（例如成批删除等）
function executeSelectAllDocument(selID,caption)
{
    var sel=false;
    var cons1=document.getElementsByName(selID);
    for(var i=0;i<cons1.length;i++)
    {
        if(cons1[i].checked)
        {
            sel=true;
            break;
        }
    }
    
    if(sel)
    {
        return confirm("警告：确定要【"+caption+"】选中的数据吗？");
    }
    else
    {
        alert("错误：没有选中要操作的数据！");
        return false;
    }
}

//Copy内容到粘贴板（支持IE和火狐）
function copyToClipboard(txt) 
{
    if (window.clipboardData)
    {
        window.clipboardData.clearData();
        window.clipboardData.setData("Text", txt);
    }
    else if (navigator.userAgent.indexOf("Opera") != -1)
    {
        window.location = txt;
    }
    else if (window.netscape)
    {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        } catch (e) {
            alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
        }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip)
            return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans)
            return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txt;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip)
            return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
    }
}    

//////////////////////////////////////////////////////////////////
//压回车键后，将光标定位到某控件
function keyEnterFocusOn(e,conClientID)
{
    var keycode=getKeycode(e);
    if(keycode==13)
    {              
        var con1=document.getElementById(conClientID);
        if(con1!=undefined)
        {
           if(con1.type=="button" || con1.type=="submit")
           {
               con1.focus();
               con1.click();
           }
           else
           {
               con1.focus();
               e.keyCode=0;
           }
        }
    }
}

function getKeycode(e)
{
    var isie = (document.all) ? true : false;    
	var key;
	if (isie)
	    key = window.event.keyCode;    //IE
	else
	{
		key = e.which;                  //FF
	}
	return key;
}


//////////////////////////////////////////////////////////////////////////
//Trim Function
String.prototype.trim = function()
{
    var reExtraSpace = /^\s*(.*?)\s+$/;
    return this.replace(reExtraSpace,"$1");
}


//////////////////////////////////////////////////////////////////////////
var ajaxError = "错误:浏览器不支持Ajax，请使用IE和火狐等浏览器！";
function initXMLHttpClient()
{
    var xmlhttp;
    try
    {
        xmlhttp=new XMLHttpRequest();
    }
    catch(e)
    {
        var XMLHTTP_IDS=new Array( 'MSXML2.XMLHTTP.5.0',
                                   'MSXML2.XMLHTTP.4.0',
                                   'MSXML2.XMLHTTP.3.0',
                                   'MSXML2.XMLHTTP',
                                   'Microsoft.XMLHTTP');
        var success=false;
        for(var i=0;i<XMLHTTP_IDS.length && !success;i++)
        {
            try
            {
                xmlhttp=new ActiveXObject(XMLHTTP_IDS[i]);
                success=true;
            }
            catch(e){ }
        }
        
        if(!success)
        {
            throw new Error(ajaxError);
        }
    }
    return xmlhttp;
}