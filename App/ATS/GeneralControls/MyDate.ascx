<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyDate.ascx.vb" Inherits="GeneralControls_MyDate" %>


    <link href="../jquery.calendars.package-2.0.0/flora.calendars.picker.css" rel="stylesheet" />
    <link href="../jquery.calendars.package-2.0.0/humanity.calendars.picker.css" rel="stylesheet" />
    <script src="../jquery.calendars.package-2.0.0/jquery.plugin.js"></script>
    <script src="../jquery.calendars.package-2.0.0/jquery.calendars.js"></script>
    <script src="../jquery.calendars.package-2.0.0/jquery.calendars.plus.js"></script>
    <script src="../jquery.calendars.package-2.0.0/jquery.calendars.picker.js"></script>
    <script src="../jquery.calendars.package-2.0.0/jquery.calendars.ummalqura.js"></script>

<script>

    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            $(function () {


                var dattype = "<%=DateType.ToString()%>"

                var calendar = $.calendars.instance(dattype);


                var txt = document.getElementById("<%=TextBox1.ClientID.ToString()%>").id;

                //  $('#' + txt).calendarsPicker({ calendar: calendar, showTrigger: '#MyDate_imgCalendar' });

        $('#' + txt).calendarsPicker({
            calendar: calendar,
            renderer: $.extend({}, $.calendarsPicker.defaultRenderer,
                {
                    picker: $.calendarsPicker.defaultRenderer.picker.
                       replace(/\{link:clear\}/, '{button:clear}').
                       replace(/\{link:close\}/, '{button:close}')
                }),
            showTrigger: '#MyDate_imgCalendar',
            onSelect: function (dates) {
                GetPeriod();
            }
        });

            });

        }

    });

    $(function () {


        var dattype = "<%=DateType.ToString()%>"

        var calendar = $.calendars.instance(dattype);


        var txt = document.getElementById("<%=TextBox1.ClientID.ToString()%>").id;

        //  $('#' + txt).calendarsPicker({ calendar: calendar, showTrigger: '#MyDate_imgCalendar' });

        $('#' + txt).calendarsPicker({
            calendar: calendar,
            renderer: $.extend({}, $.calendarsPicker.defaultRenderer,
                {
                    picker: $.calendarsPicker.defaultRenderer.picker.
                       replace(/\{link:clear\}/, '{button:clear}').
                       replace(/\{link:close\}/, '{button:close}')
                }),
            showTrigger: '#MyDate_imgCalendar',
            onSelect: function (dates) {
                GetPeriod();
            }
        });

    });

   
</script>

<table style="line-height:0px;" >
           <tr >
                <td style="display:none;line-height:0px;">
                   <img id="MyDate_imgCalendar"  src="../Images/calendar.gif" alt="Popup" class="trigger" style="height:25px;border-width:0px;" >
                </td>

                <td  >
                   <asp:TextBox ID="TextBox1" runat="server" style="width:90px;"  ></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>

                </td>
            </tr>

        </table>

