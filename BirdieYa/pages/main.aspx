<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="BirdieYa.pages.control" %>

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
        <section id="container" class="img_bg">
        <div class="main-wrap">
            <div class="right-top">
                <img src="../images/bir-icon.png">
            </div>
            <img src="../images/birdieya-logo-orange.png" class="logo-img">
            <h1>Mobile Control<br>Management System</h1>

            <div class="main-box clearfix">
                <div class="fist-box">
                    <a href="./play_menu.aspx">
                        <div class="left-box menu-btn-box">
                            <img src="../images/main-play.png">
                            <p>경기관제</p>
                        </div>
                    </a>
                    <!-- 미완성메뉴입니다.-->
                    <a href="javascript:alert('준비중입니다.')">
                        <div class="right-box menu-btn-box">
                            <img src="../images/main-setting.png">
                            <p>SETTING</p>
                        </div>
                    </a>
                    <!-- 미완성메뉴입니다.-->
                </div>
                <div class="second-box">
                    <!--회원정보노출-->
                    <div class="left-box">
                        <p><strong><asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label></strong>님<br>안녕하세요!</p>
                        <asp:Button ID="btnLogout" runat="server" Text="LOGOUT" CssClass="main-logout" OnClick="btnLogout_Click" />
                    </div>
                    <!--회원정보노출-->
                    <div class="right-box menu-btn-box">
                        <a href="facility_menu.aspx">
                            <div class="up-box">
                                <img src="../images/main-fac.png">
                                <p>시설관제</p>
                            </div>
                        </a>
                        <a href="./course_menu.aspx">
                            <div class="down-box menu-btn-box">
                                <img src="../images/main-course.png">
                                <p>코스관제</p>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <!--푸터-->
            <div class="foot-copy">
                <p>Copyright (C) 2020 Birdieya all rights reserved.</p>
            </div>
            <!--푸터-->
        </div>
    </section>
    </form>
</body>
</html>
