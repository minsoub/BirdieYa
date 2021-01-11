<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="teamlist.aspx.cs" Inherits="BirdieYa.pages.teamlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=device-width" />
    <title>버디야통합관제</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css">
    <link rel="stylesheet" type="text/css" href="../css/common.css">
    <link rel="stylesheet" type="text/css" href="../css/font.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--헤더-->
        <nav id="header">
            <a href="javascript:history.back();" class="left-btn"><img src="../images/arrow-left-btn.png" height="100%"></a>
            <div class="logo">
                <h1>팀 조회</h1>
            </div>
        </nav>
        <!--헤더-->
        <section id="container" class="black_bg">
            <div class="content">
                <div class="list-wrap">
                <!--총 예약수-->
                    <h4>Total.<asp:Label ID="lblTotal" runat="server"></asp:Label></h4>
                <!--예약내역-->
                    <table>
                        <colgroup>
                            <col width="10%">
                            <col width="20%">
                            <col width="20%">
                            <col width="10%">
                            <col width="20%">
                            <col width="20%">
                        </colgroup>
                        <tr>
                            <td>No</td>
                            <td>예약자</td>
                            <td>출발시간</td>
                            <td>코스</td>
                            <td>캐디(카트번호)</td>
                            <td>내장객</td>
                        </tr>
<asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
    <ItemTemplate>
                        <asp:Literal ID="ltrCaddie" runat="server"></asp:Literal>
    </ItemTemplate>
</asp:Repeater>
                        <%--<tr>
                            <td rowspan="2">2</td>
                            <td rowspan="2">홍길동</td>
                            <td rowspan="2">06:15</td>
                            <td rowspan="2">B</td>
                            <td>122</td>
                            <td>홍길동</td>
                        </tr>
                        <tr>
                            <td>122</td>
                            <td>홍길동</td>
                        </tr>                    
                        <tr>
                            <td rowspan="4">1</td>
                            <td rowspan="4">홍길동</td>
                            <td rowspan="4">06:15</td>
                            <td rowspan="4">B</td>
                            <td>22</td>
                            <td>홍길동</td>
                        </tr>
                        <tr>
                            <td>22</td>
                            <td>홍길동</td>
                        </tr>                     
                        <tr>
                            <td>63</td>
                            <td>홍길동</td>
                        </tr>                     
                        <tr>
                            <td>63</td>
                            <td>홍길동</td>
                        </tr>           --%>          
                    </table>
                <!--예약내역-->
                </div>
            </div>
        </section>

        <!--하단메뉴-->
        <!-- #include file="bottom.html" -->
        <!--하단메뉴-->
        <script>
            $('.gnb-1').on('click', function () {
                location.href = "./main.aspx";
            })
            $('.gnb-2').on('click', function () {
                location.href = "./cartinfo.aspx";
            })
        </script>
    </form>
</body>
</html>
