<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form3.aspx.vb" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        @page {
    size: 21cm 29.7cm;
    margin: 0.5mm 0.5mm 0.5mm 0.5mm; /* change the margins as you want them to be. */
}

        table {
          border-collapse: collapse;
             }

   </style>
</head>
<body>
     <div id="mainDiv" dir="rtl" >
        <table cellpadding="0" cellspacing="0"  style="width:17cm;border:solid;font-size:large"  align=center  >
            <tr>
                <td style="width:100%;" colspan="3">
                    <table style="width:19.0cm">
                        <tr>
                            <td rowspan="2" style="text-align:center;width:30%;">
                                المملكة العربية السعودية<br />
                                وزارة التربية والتعليم<br />
                                إدارة التربية والتعليم بمنطقة الرياض<br />
                                مدرسة : خيبر الإبتدائية<br />

                            </td>
                            <td rowspan="2" style="text-align:center;padding-left:50px;width:40%;padding-right:10px">
                                بسم الله الرحمن الرحيم<br />
                                <img src="img/moelogo.jpg" style="height:100px;width:100%">
                            </td>
                            <td style="width:30%;align-content:stretch">
                                الرقم:
                                <asp:Label ID="Label1" runat="server" Text="1234"></asp:Label>
                                <br />
                                التاريخ:
                                <asp:Label ID="Label2" runat="server" Text="1437/01/01 هـ"></asp:Label>
                                <br />
                                المرفقات:<br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center">
                  &nbsp
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center">
                   =================================================================
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center">
                  &nbsp
                </td>
            </tr>
            <tr>
              <td style="padding-right:25px"">
                  نموذج رقم 3
              </td>
              <td>
                  &nbsp
               </td>
              <td>
                  &nbsp
               </td>
              </tr>
            <tr>
               <td>
                  &nbsp
               </td>
              <td style="text-align:center;padding-right:180px;font-weight:bolder">
                 << مساءلة غياب>>
              </td>
               <td>
                  &nbsp
               </td>
                </tr>
            <tr>
                <td>
                  &nbsp
               </td>
                <td>
                  &nbsp
               </td>
              <td style="padding-right:90px;">
                 <table>
                        <tr>
                           <td style="width:40%;border: 1px solid black;">
                              رقم الملف :
                               </td>
                                <td style="width:60%;border: 1px solid black;">
                                  <asp:Label ID="Label3" runat="server" Text="1254888"></asp:Label>
                                </td>
                         </tr>
                     </table>
              </td>
          </tr>
           
            <tr>
                <td colspan="3">
                   
                    <table>
                        <tr>
                            <td style="height:0.1cm">

                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp
                            </td>
                            <td colspan="4" style="border: 1px solid black;width:2.2cm;">
                                المدرسة/ القسم

                            </td>
                            <td colspan="9" style="border: 1px solid black;width:4.8cm">
                                <asp:Label ID="Label4" runat="server" Text="مدرسة خيبر الإبتدائية"></asp:Label>

                            </td>
                            <td>
                                &nbsp
                            </td>
                            <td colspan="5" style="border: 1px solid black;width:3.5cm">
                                رقم الحاسب الآلي:
                            </td>
                            <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id10" runat="server" Text="1"></asp:Label>
                            </td>
                            <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id9" runat="server" Text="1"></asp:Label>
                            </td>
                            <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id8" runat="server" Text="1"></asp:Label>
                            </td>
                            <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id7" runat="server" Text="1"></asp:Label>
                            </td>
                            <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id6" runat="server" Text="1"></asp:Label>
                            </td>
                           <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id5" runat="server" Text="1"></asp:Label>
                            </td>
                            <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id4" runat="server" Text="1"></asp:Label>
                            </td>
                           <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id3" runat="server" Text="1"></asp:Label>
                            </td>
                           <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id2" runat="server" Text="1"></asp:Label>
                            </td>
                           <td style="border: 1px solid black;width:0.6cm">
                                <asp:Label ID="id1" runat="server" Text=" 1"></asp:Label>
                            </td>

                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table>
                         <tr>
                            <td style="height:0.1cm">

                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="width:5.0cm;border: 2px solid black;font-weight:bold;text-align:center">
                                الإســــــــــــم
                            </td>
                            <td style="width:2.0cm;border: 2px solid black;font-weight:bold;text-align:center">
                                التخصص
                            </td>
                            <td style="width:3.3cm;border: 2px solid black;font-weight:bold;text-align:center">
                                المستوى/المرتبة
                            </td>
                            <td style="width:2.0cm;border: 2px solid black;font-weight:bold;text-align:center">
                                رقم الوظيفة
                            </td>
                            <td style="width:2.5cm;border: 2px solid black;font-weight:bold;text-align:center">
                                العمل الحالي
                            </td>
                            <td style="width:2.5cm;border: 2px solid black;font-weight:bold;text-align:center">
                                عدد ايام الغياب
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="width:5.0cm;border: 2px solid black;text-align:center">
                                
                            </td>
                            <td style="width:2.0cm;border: 2px solid black;text-align:center">
                                
                            </td>
                            <td style="width:2.5cm;border: 2px solid black;text-align:center">
                                
                            </td>
                            <td style="width:2.0cm;border: 2px solid black;text-align:center">
                                
                            </td>
                            <td style="width:2.5cm;border: 2px solid black;text-align:center">
                                
                            </td>
                            <td style="width:2.5cm;border: 2px solid black;text-align:center">
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="height:0.1cm">

                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="width:17cm;border: 1px solid black;text-align:center" colspan="6">

                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="3">
                    <p >
                       &nbsp; &nbsp; &nbsp; (1) طلب الإفادة
                        <br />
                        <br />
                       &nbsp; &nbsp; &nbsp; المكرم المعلم/<asp:Label ID="empName1" runat="server" Text=""></asp:Label>&nbsp; &nbsp; &nbsp; وفقه الله
                        <br />
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; السلام عليكم ورحمة الله وبركاته, وبعد:
                        <br />
                        &nbsp; &nbsp; &nbsp;  من خلال متابعة سجل الدوام تبين غيابكم خلال الفترة الموضحة بعاليه , آمل الإفادة عن أسباب ذلك , وعليكم تقديم
                        <br />
                        &nbsp; &nbsp; &nbsp;  .ما يؤيد عذركم خلال إسبوع من تاريخه ... علماً بأنه في حالة عدم الإلتزام سيتم إتخاذ اللازم حسب التعليمات
                        <br />
                         <br />
                        &nbsp; &nbsp; &nbsp; إسم مدير المدرسة:<asp:Label ID="managername" runat="server" Text="........................"></asp:Label> 
                        &nbsp; &nbsp; &nbsp; التوقيع........................
                        &nbsp; &nbsp; &nbsp; التاريخ &nbsp; &nbsp; &nbsp;/&nbsp; &nbsp; &nbsp;/&nbsp; &nbsp; &nbsp; 143هــ
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="3" style ="text-align:center ">
                _____________________________________________________________________________
                </td>
            </tr>
            
            <tr>
                <td colspan="3">
                    <p >
                       &nbsp; &nbsp; &nbsp; (2) الإفادة
                        <br />
                        <br />
                       &nbsp; &nbsp; &nbsp; المكرم مدير/<asp:Label ID="schoolname" runat="server" Text=""></asp:Label>&nbsp; &nbsp; &nbsp;  المحتــــرم
                        <br />
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; السلام عليكم ورحمة الله وبركاته
                        <br />
                        &nbsp; &nbsp; &nbsp;  أفيدكم أن غيابي للأسباب التالية ............................................................................
                        <br />
                        &nbsp; &nbsp; &nbsp; ...................................................................................................................
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ...................................................وسأقوم بتقديم ما يثبت ذلك خلال اسبوع من تاريخه.
                        <br />
                         <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; الإسم :<asp:Label ID="Label6" runat="server" Text="........................"></asp:Label> 
                        &nbsp; &nbsp; &nbsp; التوقيع........................
                        &nbsp; &nbsp; &nbsp; التاريخ &nbsp; &nbsp; &nbsp;/&nbsp; &nbsp; &nbsp;/&nbsp; &nbsp; &nbsp; 143هــ
                    </p>
                </td>
            </tr>
             <tr>
                <td colspan="3" style ="text-align:center ">
                _____________________________________________________________________________
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <p >
                       &nbsp; &nbsp; &nbsp; (3) مدير  :
                        <br />
                        <br />
                        &nbsp; &nbsp; &nbsp; <input type="checkbox"  id="CheckBox1"  /> تحتسب له إجازة مرضية بعد التأكد من نظامية التقرير.
                        <br />
                        &nbsp; &nbsp; &nbsp; <input type="checkbox"  id="CheckBox2"  /> يحتسب غيابه من رصيده للإجازات الاضطرارية لقبول عذره إذا كان رصيده يسمح وإلا يحسم عليه.
                        <br />
                        &nbsp; &nbsp; &nbsp; <input type="checkbox"  id="CheckBox3"  /> يعتمد الحسم لعدم قبول عذره.
                        <br />
                         <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; الإسم :<asp:Label ID="Label7" runat="server" Text="........................"></asp:Label> 
                        &nbsp; &nbsp; &nbsp; التوقيع........................
                        &nbsp; &nbsp; &nbsp; التاريخ &nbsp; &nbsp; &nbsp;/&nbsp; &nbsp; &nbsp;/&nbsp; &nbsp; &nbsp; 143هــ
                    </p>
                </td>
            </tr>
        </table>
         
   </div>
</body>
</html>
