<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="pin_search.aspx.cs" Inherits="BirdieYa.pages.pin_search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=device-width" />
    <title>버디야통합관제</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css">
    <link rel="stylesheet" type="text/css" href="../css/common.css">
    <link rel="stylesheet" type="text/css" href="../css/font.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.datepicker-ko.js" type="text/javascript"></script>
    <link href="../js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function ChangeCourse(selCourse) {

            console.log("course=>"+selCourse);
            console.log("hole=>"+document.getElementById("hole").value);
            SetCourse(selCourse);
            document.getElementById("<%=iCourse.ClientID %>").value = selCourse;
            document.getElementById("<%=iHole.ClientID %>").value = document.getElementById("hole").value;

            <%= Page.GetPostBackEventReference(btnSearch) %>
        }

        function HoleChange(data)
        {
            console.log(data);
            document.getElementById("<%=iHole.ClientID %>").value = data;
            ChangeCourse($("#<%=iCourse.ClientID %>").val());
        }

        function SetCourse(selCourse) {

            $("#aCourseA").attr('class', 'tab-01');
            $("#aCourseB").attr('class', 'tab-01');
            $("#aCourseC").attr('class', 'tab-01');

            $("#aCourse" + selCourse).attr('class', 'tab-01 active');
        }

        $(function(){
            $("#<%=txtDate1.ClientID %>").datepicker();
            $("#<%=txtDate2.ClientID %>").datepicker();
        });

        $(window).load(function () {
            console.log("load.....");
            console.log("hdnWidth => " + $("#<%=hdnWidth.ClientID %>").val());
            if ($("#<%=hdnWidth.ClientID %>").val() == "0")
            {
                $("#<%=hdnWidth.ClientID %>").val($(window).width());   
                <%= Page.GetPostBackEventReference(btnSearch) %>
            }

            if ($("#<%=iCourse.ClientID %>").val() == "-1")
            {
                $("#<%=iCourse.ClientID %>").val("A");
                $("#<%=iHole.ClientID %>").val("1");
            } else {
                console.log("hole => " + $("#<%=iHole.ClientID %>").val());
                SetCourse($("#<%=iCourse.ClientID %>").val());
                $("#hole").val($("#<%=iHole.ClientID %>").val()).prop("selected", true);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnPosX" runat="server" Value="0" />
        <asp:HiddenField ID="hdnPosY" runat="server" Value="110" />
        <asp:HiddenField ID="hdnWidth" runat="server" Value="0" />
        <asp:TextBox ID="iCourse" runat="server" Value="A" Style="display:none;" ></asp:TextBox>
        <asp:TextBox ID="iHole" runat="server" Value="1" Style="display:none;"></asp:TextBox>

        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Visible="false" />       
   <!--헤더-->
    <nav id="header">
        <a href="course_menu.aspx" class="left-btn"><img src="../images/arrow-left-btn.png" height="100%"></a>
        <div class="logo">
            <h1>핀위치 검색</h1>
        </div>
    </nav>
    <!--헤더-->
    
    <section id="container" class="black_bg">
    <!--코스선택-->
        <div class="tab-btn">
            <a href="javascript:ChangeCourse('A');" id="aCourseA"  class="tab-01 active">A코스</a>
            <a href="javascript:ChangeCourse('B');" id="aCourseB"  class="tab-01">B코스</a>
            <a href="javascript:ChangeCourse('C');" id="aCourseC"  class="tab-01">C코스</a>
        </div>
    <!--코스선택-->
    
    <!--홀선택-->
        <div class="content">
            <select name="hole" id="hole" class="select-box" onchange="javascript:HoleChange(this.value);">
                <option value="1">1 hole</option>
                <option value="2">2 hole</option>
                <option value="3">3 hole</option>
                <option value="4">4 hole</option>
                <option value="5">5 hole</option>
                <option value="6">6 hole</option>
                <option value="7">7 hole</option>
                <option value="8">8 hole</option>
                <option value="9">9 hole</option>
            </select>
            <div class="date-sear">
                <asp:TextBox ID="txtDate1" runat="server" MaxLength="10" style="margin-left:4px; height:30px; font-size:18px; color:Black" Width="30%"></asp:TextBox> ~
                <asp:TextBox ID="txtDate2" runat="server" MaxLength="10" style="margin-left:4px; height:30px; font-size:18px; color:Black" Width="30%"></asp:TextBox>
                <asp:Button ID="btnSearch2" runat="server" text="검색" style="height:35px; font-size:18px;" onclick="btnSearch_Click" />
            </div>


            <asp:Literal ID="ltrPosition" runat="server"></asp:Literal>
                        
            <asp:Repeater ID="rptList" runat="server" onitemdatabound="rptList_ItemDataBound">
               <ItemTemplate>
                  <asp:Literal ID="ltrPosition" runat="server"></asp:Literal>
               </ItemTemplate>
            </asp:Repeater>
                        
            <asp:Image ID="imgHole" runat="server" />

        </div>
    <!--홀선택-->
    </section>
        <!--하단메뉴-->
        <!-- #include file="bottom.html" -->
        <!--하단메뉴-->
    </form>

    <script>
        $('#menu1').attr('class', 'menu-gnb');
        $('#menu2').attr('class', 'menu-gnb');
        $('#menu3').attr('class', 'menu-gnb');
        $('#menu4').attr('class', 'menu-gnb on');
    </script>
</body>
</html>
