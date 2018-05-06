<%@ Page Language="C#" AutoEventWireup="true" EnableTheming="false" StylesheetTheme=""%>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title><%=System.Configuration.ConfigurationManager.AppSettings["HTGL_WebTitle"]%></title>

    <script language="javascript" type="text/javascript">
        if (top.location != self.location) {
            top.location = self.location;
        }

        function InitSync() {
            if ("object" == typeof (top.deeptree) && "unknown" == typeof (top.deeptree.Sync)) {
                top.deeptree.Sync();
            }
        }

        moveTo(0, 0);
        resizeTo(screen.availWidth, screen.availHeight);

        function CloseWindow() {
            window.event.returnValue = "提示：您确定要退出后台办公系统吗？";
        }
    </script>

</head>
<frameset cols="*,990,*" frameborder="0" scrolling="no" border="0" framespacing="0"
    onbeforeunload="javaScript:CloseWindow();">

    <frame src="FrameBorder.html" scrolling="no" noresize frameborder="no" 
    border="0" framespacing="0"></frame> 

    <frameset rows="128,*,30" cols="*" frameborder="no" border="0" framespacing="0">
        <frame src="FrameTop.aspx" name="topFrame" scrolling="no"
         noresize="noresize" id="topFrame" title="topFrame" target="RightFrm" />
         
         <frameset cols="19,210,*,20" frameborder="0" border="0" id="Frmid" name="Frm" 
            bordercolor="#0086ef" scrolling="no"> 
            
            <frame name="Left1" id="Left1"  src="FrameLeft1.html" scrolling="no"  
            noresize="noresize"> 
         
            <frame name="LeftFrm" id="LeftFrm" src="FrameMenu.aspx" 
            scrolling="no"  noresize="noresize" target="RightFrm"> 
         
            <frameset cols="11,*" frameborder="0" border="0" id="MiddleFrm" name="MiddleFrm"> 
         
            <frame name="ButtonFrm" id="ButtonFrm" scrolling="no" 
            src="FrameMiddle.html" noresize> 
         
            <frame name="view" id="view" src="MainPage.aspx" > 
        </frameset> 
        
                
		<frame name="Left2" id="Left2"  src="FrameLeft2.html" scrolling="no"  noresize="noresize"> 
</frameset>

        <frame src="FrameBottom.html" name="footpage" id="footpage" title="footpage" />
    </frameset>

    <frame src="FrameBorder.html" scrolling="no" noresize frameborder="no" 
    border="0" framespacing="0"></frame> 
 
</frameset>
<noframes>
</noframes>
</html>
