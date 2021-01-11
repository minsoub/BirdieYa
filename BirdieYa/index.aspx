<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BirdieYa.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=device-width" />
    <title>버디야통합관제</title>
    <link rel="stylesheet" type="text/css" href="./css/style.css">
    <link rel="stylesheet" type="text/css" href="./css/common.css">
    <link rel="stylesheet" type="text/css" href="./css/font.css">
</head>
<body>
    <form id="form1" runat="server">
        <section id="container" class="img_bg">
            <div class="login-wrap">
                <img src="./images/birdieya-logo.png" class="logo-img">

                <div class="login-box">
                    <h4>골프 관제 시스템을 이용하시려면<br>로그인을 해주세요</h4>
                    <div class="input-log">
                        <asp:TextBox ID="txtUserID" runat="server" placeholder="아이디입력"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="비밀번호 입력" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="one-btn" OnClick="btnLogin_Click" />
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
