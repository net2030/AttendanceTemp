<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form2.aspx.vb" Inherits="Default5" %>

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
                  &nbsp
                </td>
            </tr>
            <tr>
              <td style="padding-right:25px"">
                  نموذج رقم 2
              </td>
              <td>
                  &nbsp
               </td>
              <td style="padding-right:200px">
                  <asp:Label ID="Label3" runat="server" Text="الموضوع: قرار حسم غياب معلم عن ساعات تأخر وخروج مبكر" Font-Names="Andalus" Font-Bold="true"></asp:Label>
               </td>
                
              
          </tr>
            <tr>
                <td colspan ="3">
                      <br />
                    <br />
                      <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                  &nbsp
               </td>
                <td>
                  &nbsp
               </td>
              <td style="padding-right:415px;">
                 <table>
                        <tr>
                           <td style="width:40%;border: 1px solid black;">
                              رقم الملف :
                               </td>
                                <td style="width:60%;border: 1px solid black;">
                                  <asp:Label ID="Label5" runat="server" Text="1254888"></asp:Label>
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
                        
                        
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="3">
                    <p  >
                        <br />
                        &nbsp; &nbsp; &nbsp; إن مدير مدرسة/ خيبر الإبتدائية
                        <br />
                        &nbsp; &nbsp; &nbsp;  بناء على صلاحياته, وبناءاً على المادة(21) من نظام الخدمة المدنية, وبناءاً على موافقة معالي الوزير على
                        <br />
                        &nbsp; &nbsp; &nbsp;  .إعطاء بعض صلاحياته لمديري المدارس بالقرار رقم 1139/1 وتاريخ 17/3/1421 هــ , ولبلوغ ساعات التأخر
                        <br />
                         &nbsp; &nbsp; &nbsp; عن الدوام والخروج المبكر من الدوام(      ) ساعة خلال الفترة من     /   /     هــ 
                        <br />
                        &nbsp; &nbsp; &nbsp; الى      /    /       هــ وحيث ان عذره غير مقبول وبمقتضى النظام.
                        <br />
                         <br />
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; 
                         &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; 
                         &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;

                        <asp:Label ID="Label8" runat="server" Text="يقرر ما يلي:" Font-Bold="true" Font-Names="aldhabi" Font-Size="Larger"></asp:Label>
                         <br />
                         <br />
                        &nbsp; &nbsp; &nbsp; [1]  حسم مدة الغياب الموضحة بعاليه وعددها () يوما من راتبه.
                        <br />
                        &nbsp; &nbsp; &nbsp; [2]  على إدارة شؤون الموظفين [تنفيذ الأنظمه] تنفيذ إجراء الحسم واستبعادها من خدماته.
                        <br />
                        &nbsp; &nbsp; &nbsp; وأصل القرار لملفه بالإدارة مع الأساس لملفه(   )
                         <br />
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; 
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; 
                         &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; والله الموفق ,,,,,
                         

                         <br />
                         <br />
                         <br />
                         <br />
                         <br />
                        
                    </p>
                </td>

            </tr>
            
            
            <tr>
                <td colspan="3">
                    <table>
                        <tr>
                            <td rowspan="5">
                                  &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; الختم
                            </td>
                        </tr>
                        <tr >

                            <td style="padding-right:300px;text-align:center" >
                                مدير المدرسة
                            </td>
                        </tr>
                         <tr >

                            <td style="padding-right:300px;text-align:center" >
                                الإسم : <asp:Label ID="Label6" runat="server" Text="فواز بن عياش العنزي"></asp:Label>
                            </td>
                        </tr>
                         <tr >

                            <td style="padding-right:300px;text-align:center" >
                                التوقيع : ...........................
                            </td>
                        </tr>
                        <tr >

                            <td style="padding-right:300px;text-align:center" >
                                التاريخ :     /    /      هــ
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="3">
                    <br />
                     <br />
                     <br />
                     <br />
                     <br />
                     <br />
                </td>
            </tr>
        </table>
         
   </div>
</body>
</html>
