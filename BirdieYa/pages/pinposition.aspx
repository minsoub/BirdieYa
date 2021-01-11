<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="pinposition.aspx.cs" Inherits="BirdieYa.pages.pinposition" %>

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
    <script type="text/javascript" language="javascript">

        function GetPosition(e)
        {
            var x = e.pageX;
            var y = e.pageY;
                        
            document.getElementById("<%=hdnPosX.ClientID %>").value = x - 10;
            document.getElementById("<%=hdnPosY.ClientID %>").value = y - 10;
            
            document.getElementById("<%=Form.ClientID %>").submit();
        }

        function ChangeCourse(selCourse) {

            console.log(selCourse);
            console.log(document.getElementById("hole").value);
            SetCourse(selCourse);
            document.getElementById("<%=iCourse.ClientID %>").value = selCourse;
            document.getElementById("<%=iHole.ClientID %>").value = document.getElementById("hole").value;

            <%= Page.GetPostBackEventReference(btnImage) %>
        }

        function SetCourse(selCourse)
        {

            $("#aCourseA").attr('class', 'tab-01');
            $("#aCourseB").attr('class', 'tab-01');
            $("#aCourseC").attr('class', 'tab-01');

            $("#aCourse" + selCourse).attr('class', 'tab-01 active');
        }

        function HoleChange(data)
        {
            console.log(data);
            document.getElementById("<%=iHole.ClientID %>").value = data;
            ChangeCourse($("#<%=iCourse.ClientID %>").val());
        }

        function PinSave()
        {
            if (confirm("Pin위치를 저장하시겠습니까?"))
            {
                <%= Page.GetPostBackEventReference(btnSave) %>
            }
        }

        $(window).load(function () {
            console.log("window load => " + $("#<%=iCourse.ClientID %>").val());
            if ($("#<%=hdnWidth.ClientID %>").val() == "0") {
                $("#<%=hdnWidth.ClientID %>").val($(window).width());
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
        <asp:TextBox ID="iCourse" runat="server" Value="A" Style="display:none;"  AutoPostBack="true"></asp:TextBox>
        <asp:TextBox ID="iHole" runat="server" Value="1" Style="display:none;" AutoPostBack="true" />

        <asp:Button ID="btnImage" runat="server" onclick="SetBtnImage" Visible="false" />       
        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Visible="false" />
    <!--헤더-->
    <nav id="header">
        <a href="" class="left-btn"><img src="../images/arrow-left-btn.png" height="100%"></a>
        <div class="logo">
            <h1>핀위치</h1>
        </div>
        <a href="pin_search.aspx" class="right-btn"><img src="../images/cal-sea.png" height="100%"></a>
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
       
        <div class="content">
    <!--홀선택-->
            <select id="hole" name="hole" class="select-box" onchange="javascript:HoleChange(this.value);">
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
    <!--홀선택-->
            <div id="hole" style="position:absolute; top:<%=hdnPosY.Value %>px; left:<%=hdnPosX.Value %>px;"><img src="../Images/pin.png" width="20px" height="20px" alt="홀 위치" /></div>
            <asp:Image ID="imgHole" runat="server" onmousedown="javascript:GetPosition(event);" />

            <!--img src="../images/pin-bg-img.png" class="" width="100%"  -->

            <a href="javascript:PinSave();" class="one-btn">핀위치 저장</a>
        </div>





    </section>

        <!--하단메뉴-->
        <!-- #include file="bottom.html" -->
        <!--하단메뉴-->

    </form>
</body>
</html>
