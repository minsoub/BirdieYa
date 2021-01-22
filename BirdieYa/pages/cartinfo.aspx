<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="cartinfo.aspx.cs" Inherits="BirdieYa.pages.cartinfo" %>

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
        function ChangeCourse(selCourse) {
            $("#<%=hdnSelCourse.ClientID %>").val(selCourse);

            SetCourse(selCourse);

            console.log("ChangeCourse call=> " + selCourse);

            <%=Page.GetPostBackEventReference(btnSelCourse)%>;   
        }

        function SetCourse(selCourse)
        {
            $("#aCourse1").attr('class', 'tab-01');
            $("#aCourse2").attr('class', 'tab-01');
            $("#aCourse3").attr('class', 'tab-01');

            $("#aCourse" + selCourse).attr('class', 'tab-01 active');
        }

        function SetSelDetail(vCartNo, vCartType, vTeamNo)
        {
            //string sCartNo, string sCartType, string sTeamNo
            jQuery.ajax({
                url: 'cartinfo.aspx/GetCartDetail',
                type: "POST",
                dataType: "json",
                data: "{'sCartNo': '" + vCartNo + "', 'sCartType':'" + vCartType + "', 'sTeamNo':'" + vTeamNo + "'}",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(JSON.stringify(data));
                    var jdata = data;
                    console.log("Json.d=> " + jdata.d);
                    //lblCartNo, lblCartType, lblStartTime, lblMember
                    var arr = jdata.d;  // jdata.d.split(",");
                    console.log("arr[0] => " + arr.cart_no);
                    console.log("arr[1] => " + arr.member);
                    SetCartDetail(arr);
                }
            });

            //var data = PageMethods.GetCartDetail(vCartNo, vCartType, vTeamNo, SetCartDetail);

            //console.log(data);
        }

        function SetCartDetail(result)
        {
            console.log(result.cart_no);
            $("#<%=lblCartNo.ClientID %>").text(result.cart_no);
            $("#<%=lblCartType.ClientID %>").text(result.cart_type);
            $("#<%=lblCaddie.ClientID %>").text(result.caddie_name);
            $("#<%=lblStartTime.ClientID %>").text(result.start_time);
            $("#<%=lblMember.ClientID %>").text(result.member);
        }

        $(window).load(function () {
            console.log("window load => " + $("#<%=hdnSelCourse.ClientID %>").val());
            if ($("#<%=hdnSelCourse.ClientID %>").val() == "-1") {
                ChangeCourse("1");
            } else {
                console.log("course => " + $("#<%=hdnSelCourse.ClientID %>").val());
                SetCourse($("#<%=hdnSelCourse.ClientID %>").val());
            }            
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnSelCourse" runat="server" Value="-1" />
        <asp:Button ID="btnSelCourse" runat="server" OnClick="btnSelCourse_Click" Visible="false"/>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <!--헤더-->
        <nav id="header">
            <a href="javascript:history.back();" class="left-btn"><img src="../images/arrow-left-btn.png" height="100%"></a>
            <div class="logo">
                <h1>카트관제</h1>
            </div>
            <a href="./teamlist.aspx" class="right-btn"><img src="../images/list-btn.png" height="100%"></a>
        </nav>
        <!--헤더-->

        <section id="container" class="black_bg">
            <!--코스선택-->
            <div class="tab-btn">
                <a href="javascript:ChangeCourse('1');" id="aCourse1" class="tab-01 active">JimEngh A</a>
                <a href="javascript:ChangeCourse('2');" id="aCourse2" class="tab-01">JimEngh B</a>
                <a href="javascript:ChangeCourse('3');" id="aCourse3" class="tab-01">JimEngh C</a>
            </div>
            <!--코스선택-->
            <div class="content">

<!-- 정보컬러설명-->
                <div class="car-info-wrap">
                    <div class="car-info">
                        <div class="car-round-1 round"></div>
                        <p>일반</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-2 round"></div>
                        <p>교육</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-3 round"></div>
                        <p>주의</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-4 round"></div>
                        <p>처음</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-5 round"></div>
                        <p>끝</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-6 round"></div>
                        <p>9H</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-7 round"></div>
                        <p>VIP</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-8 round"></div>
                        <p>단체</p>
                    </div>
                    <div class="car-info">
                        <div class="car-round-9 round"></div>
                        <p>마샬</p>
                    </div>
                </div>
                <!-- 정보컬러설명-->

                <div class="cart-hole-wrap">
                    <div class="hole-wrap">
                        <div class="hole"><asp:Literal ID="ltrHole1" runat="server"></asp:Literal></div>
                        <div class="hole"><asp:Literal ID="ltrHole2" runat="server"></asp:Literal></div>
                        <div class="hole"><asp:Literal ID="ltrHole3" runat="server"></asp:Literal></div>
                    </div>
                    <div class="hole-wrap">
                        <div class="hole"><asp:Literal ID="ltrHole4" runat="server"></asp:Literal></div>
                        <div class="hole"><asp:Literal ID="ltrHole5" runat="server"></asp:Literal></div>
                        <div class="hole"><asp:Literal ID="ltrHole6" runat="server"></asp:Literal></div>
                    </div>
                    <div class="hole-wrap">
                        <div class="hole"><asp:Literal ID="ltrHole7" runat="server"></asp:Literal></div>
                        <div class="hole"><asp:Literal ID="ltrHole8" runat="server"></asp:Literal></div>
                        <div class="hole"><asp:Literal ID="ltrHole9" runat="server"></asp:Literal></div>
                    </div>
                </div>

                
            </div>
            <!--카트 클릭시 상세내용-->
            <div class="info-box dis-n">
                <div class="info-b-tit">
                    CartNo.<asp:Label ID="lblCartNo" runat="server"></asp:Label><button class="close"><img src="../images/x-icon.png"></button></div>
                <table>
                    <colgroup>
                        <col width="20%">
                        <col width="16%">
                        <col width="16%">
                        <col width="16%">
                        <col width="16%">
                        <col width="16%">
                    </colgroup>
                    <tr>
                        <td>카트구분</td>
                        <td><asp:Label ID="lblCartType" runat="server"></asp:Label></td>
                        <td>캐디</td>
                        <td><asp:Label ID="lblCaddie" runat="server"></asp:Label></td>
                        <td>예약</td>
                        <td><asp:Label ID="lblStartTime" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>회원</td>
                        <td colspan="5"><asp:Label ID="lblMember" runat="server"></asp:Label></td>
                    </tr>
                </table>        
            </div>
        </section>

        <!--하단메뉴-->
        <!-- #include file="bottom.html" -->
        <!--하단메뉴-->

        <!--scripts loaded here -->

        <script>
            $('.info-box').hide();
            $('.gnb-1').on('click', function () {
                location.href = "./main.aspx";
            })
            $('.close').on('click', function () {
                $('.info-box').css('display', 'none');
            })
            $('.detail-car').on('click', function () {
                $('.detail-car').toggleClass("on-btn");
                $('.info-box').toggle();
            })

        </script>
    </form>
</body>
</html>
