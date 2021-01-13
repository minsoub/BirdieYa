<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="course_menu.aspx.cs" Inherits="BirdieYa.pages.course_menu" %>

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

<!--메뉴는 메인에서 서브메뉴가 있는 페이지만 이동됩니다.-->
    <section id="container" class="img_bg">

        <div class="content-ful">
            <div class="right-top" onclick="location.href='main.aspx'" style="cursor:pointer">
                <img src="../images/gnb-1.png">
                <p>HOME</p>
            </div>

            <div class="menu-wrap">
                <h1>코스관제</h1>
                <div class="menu-list">
                    <ul>
                        <a href="pinposition.aspx">
                            <li>핀위치</li>
                        </a>
                        <a href="pin_search.aspx">
                            <li>핀위치 검색</li>
                        </a>
                    </ul>
                </div>
            </div>
          
        </div>
    </section>
</body>

</html>

