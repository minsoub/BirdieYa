<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="facility_menu.aspx.cs" Inherits="BirdieYa.pages.facility_menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=device-width" />
    <title>버디야통합관제</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css">
    <link rel="stylesheet" type="text/css" href="../css/common.css">
    <link rel="stylesheet" type="text/css" href="../css/font.css">
</head>
<body>
    <form id="form1" runat="server">
<!--메뉴는 메인에서 서브메뉴가 있는 페이지만 이동됩니다.-->
    <section id="container" class="img_bg">


        <div class="content-ful">
            <div class="right-top" onclick="location.href='main.aspx'" style="cursor:pointer">
                <img src="../images/gnb-1.png">
                <p>HOME</p>
            </div>

            <div class="menu-wrap">
                <h1>시설관제</h1>
                <div class="menu-list">
                    <ul>
                        <a href="javascript:alert('준비중');">
                            <li>수위제어</li>
                        </a>
                        <a href="javascript:alert('준비중');">
                            <li>분수제어</li>
                        </a>
                        <a href="javascript:alert('준비중');">
                            <li>기상장비제어</li>
                        </a>
                        <a href="javascript:alert('준비중');">
                            <li>펌프제어</li>
                        </a>
                    </ul>
                </div>
            </div>



        </div>





    </section>
    </form>
</body>
</html>
