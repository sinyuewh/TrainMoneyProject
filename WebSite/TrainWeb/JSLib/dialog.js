function Dialog(id) { return document.getElementById(id) }
// 计算当前窗口的宽度 //
function pageWidth() {
    return window.innerWidth != null ? window.innerWidth : document.documentElement && document.documentElement.clientWidth ? document.documentElement.clientWidth : document.body != null ? document.body.clientWidth : null;
}

// 计算当前窗口的高度 //
function pageHeight() {
    return window.innerHeight != null ? window.innerHeight : document.documentElement && document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body != null ? document.body.clientHeight : null;
}

// 计算当前窗口的上边滚动条//
function topPosition() {
    return typeof window.pageYOffset != 'undefined' ? window.pageYOffset : document.documentElement && document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop ? document.body.scrollTop : 0;
}

// 计算当前窗口的左边滚动条//
function leftPosition() {
    return typeof window.pageXOffset != 'undefined' ? window.pageXOffset : document.documentElement && document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft ? document.body.scrollLeft : 0;
}

//计算body或documentElement滚动条总宽度
function scrollWidth() {
    return document.body.scrollWidth ? document.body.scrollWidth : document.documentElement.scrollWidth ? document.documentElement.scrollWidth : pageWidth();
}

//计算body或documentElement滚动条总高度
function scrollHeight() {
    return document.body.scrollHeight ? document.body.scrollHeight : document.documentElement.scrollHeight ? document.documentElement.scrollHeight : pageHeight();
}


function AlertMsg(url, title, w, h, mask, top, left) {
    var msgw, msgh, msgbg, msgcolor, bordercolor, titlecolor, titlebg, content, ismask, msgtop, msgleft;
    //弹出窗口设置
    msgw = w ? w : 600; 	//窗口宽度
    msgh = h ? h : 400; 	//窗口高度
    ismask = mask == null ? true : mask; 	//是否创建遮罩层
    msgtop = top ? top : (pageHeight() - msgh) / 2;
    msgleft = left ? left : (pageWidth() - msgw) / 2;
    msgbg = "#FFF"; 		//内容背景
    msgcolor = "#000"; 	//内容颜色
    bordercolor = "#5A6D58"; 	//边框颜色 
    titlecolor = "#254015"; //标题颜色
    titlebg = "#369 url(/images/bg.gif)"; 	//标题背景
    //内容页设置
    content = "<iframe id='ifrm' width=" + msgw + " height=" + (msgh - 35) + " frameborder='0' src='" + url + "'></iframe>";
    //遮罩层设置
    if (ismask) {
        maskcontent = "<iframe id='ifrmmask' width='100%' height='100%' frameborder='0' src=''></iframe>";
        //创建遮罩背景 
        var maskObj = document.createElement("div");
        maskObj.setAttribute('id', 'maskdiv');
        maskObj.style.position = "absolute";
        maskObj.style.top = "0";
        maskObj.style.left = "0";
        maskObj.style.background = "#fff";
        maskObj.style.filter = "Alpha(opacity=80);";
        maskObj.style.opacity = "0.3";
        maskObj.style.width = '100%';
        maskObj.style.height = '100%';
        maskObj.style.zIndex = "10000";
        maskObj.innerHTML = maskcontent;
        document.body.appendChild(maskObj);
    }
    //创建弹出窗口
    var msgObj = document.createElement("div");
    msgObj.setAttribute("id", "msgdiv");
    msgObj.style.position = "absolute";
    //msgObj.style.top = (topPosition() + (pageHeight() - msgh) / 2) + "px";
    msgObj.style.top = (topPosition() + msgtop) + "px";

    //msgObj.style.left = (leftPosition() + (pageWidth() - msgw) / 2) + "px";
    msgObj.style.left = (leftPosition() + msgleft) + "px";
    
    msgObj.style.width = msgw + "px";
    msgObj.style.height = msgh + "px";
    msgObj.style.fontSize = "12px";
    msgObj.style.background = msgbg;
    msgObj.style.border = "1px solid " + bordercolor;
    msgObj.style.zIndex = "10001";
    //创建标题
    var thObj = document.createElement("div");
    thObj.setAttribute("id", "msgth");
    thObj.className = "DragAble";
    thObj.title = "按住鼠标左键可以拖动窗口！";
    thObj.style.cursor = "move";
    thObj.style.padding = "4px 6px";
    thObj.style.color = titlecolor;
    thObj.style.fontWeight = 'bold';
    thObj.style.background = titlebg;
    var titleStr = "<b title='关闭' style='cursor:pointer;float:right;font-weight:bold;font-size:14px;font-family: @宋体;' onclick='CloseMsg()'> × </b><b tipWidth='" + msgw + "' tipHeight='" + msgh + "' title='最大化' style='cursor:pointer;float:right;font-weight:bold;font-size:14px;font-family: @宋体;' onclick='ChangeMsgSize(this)'> □ </b>" + "<span>" + title + "</span>";
    thObj.innerHTML = titleStr;
    //创建内容
    var bodyObj = document.createElement("div");
    bodyObj.setAttribute("id", "msgbody");
    bodyObj.style.padding = "0px";
    bodyObj.style.lineHeight = "1.5em";
    bodyObj.innerHTML = content;
    //生成窗口
    document.body.appendChild(msgObj);
    Dialog("msgdiv").appendChild(thObj);
    Dialog("msgdiv").appendChild(bodyObj);
}
function CloseMsg() {
    //移除对象
    if (Dialog("ifrm")) {
        Dialog("ifrm").src = "";
    }
    if (Dialog("maskdiv")) {
        document.body.removeChild(Dialog("maskdiv"));
    }
    Dialog("msgdiv").removeChild(Dialog("msgth"));
    Dialog("msgdiv").removeChild(Dialog("msgbody"));
    document.body.removeChild(Dialog("msgdiv"));
}

function ChangeMsgSize(obj) {
    if (obj.title == "最大化") {
        //最大化
        obj.innerHTML = "回";
        obj.title = "正常化";
        Dialog("msgdiv").style.top = "0";
        Dialog("msgdiv").style.left = "0";
        Dialog("msgdiv").style.width = (leftPosition() + pageWidth() - 2) + "px";
        Dialog("msgdiv").style.height = (topPosition() + pageHeight() - 2) + "px";
        Dialog("ifrm").setAttribute('width', (leftPosition() + pageWidth() - 2));
        Dialog("ifrm").setAttribute('height', (topPosition() + pageHeight() - 2 - 35));

    } else if (obj.title == "正常化") {
        obj.innerHTML = "□";
        obj.title = "最大化";
        Dialog("msgdiv").style.top = (topPosition() + (pageHeight() - obj.getAttribute("tipHeight")) / 2) + "px";
        Dialog("msgdiv").style.left = (leftPosition() + (pageWidth() - obj.getAttribute("tipWidth")) / 2) + "px";
        Dialog("msgdiv").style.width = obj.getAttribute("tipWidth") + "px";
        Dialog("msgdiv").style.height = obj.getAttribute("tipHeight") + "px";
        Dialog("ifrm").setAttribute('width', obj.getAttribute("tipWidth"));
        Dialog("ifrm").setAttribute('height', (obj.getAttribute("tipHeight") - 35));

    }
}

//拖动窗口
var ie = document.all;
var nn6 = document.getElementById && !document.all;
var isdrag = false;
var y, x;
var oDragObj;

function moveMouse(e) {
    if (isdrag) {
        oDragObj.style.top = (nn6 ? nTY + e.clientY - y : nTY + event.clientY - y) + "px";
        oDragObj.style.left = (nn6 ? nTX + e.clientX - x : nTX + event.clientX - x) + "px";
        return false;
    }
}

function initDrag(e) {
    var oDragHandle = nn6 ? e.target : event.srcElement;
    var topElement = "HTML";
    while (oDragHandle == "undefined" && oDragHandle.tagName != topElement && oDragHandle.className != "DragAble") {
        oDragHandle = nn6 ? oDragHandle.parentNode : oDragHandle.parentElement;
    }
    if (oDragHandle.className == "DragAble") {
        isdrag = true;
        oDragObj = oDragHandle.parentNode;
        nTY = parseInt(oDragObj.style.top);
        y = nn6 ? e.clientY : event.clientY;
        nTX = parseInt(oDragObj.style.left);
        x = nn6 ? e.clientX : event.clientX;
        document.onmousemove = moveMouse;
        return false;
    }
}

//禁止窗口移动
document.onmousedown = initDrag;
document.onmouseup = new Function("isdrag=false");

/*
function killerrors() { 
return true; 
} 
window.onerror = killerrors; 
*/