<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="play_menu.aspx.cs" Inherits="BirdieYa.pages.play_menu" %>

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
    <section id="container" class="img_bg">


        <div class="content-ful">
            <div class="right-top">
                <img src="../images/gnb-1.png">
                <p>HOME</p>
            </div>

            <div class="menu-wrap">
                <h1>경기관제</h1>
                <div class="menu-list">
                    <ul>
                        <a href="cartinfo.aspx">
                            <li>카트관제</li>
                        </a>
                        <a href="teamlist.aspx">
                            <li>예약리스트</li>
                        </a>
                    </ul>
                </div>
            </div>


        </div>

    </section>
    </form>
</body>
</html>
