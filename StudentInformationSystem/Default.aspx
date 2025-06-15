<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Default.aspx.vb" Inherits="StudentInformationSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <main class="container mt-5 text-light bg-dark p-4 rounded">
        <section class="row">
            <h2 class="mb-4">Welcome to the Student Information System</h2>

            <p>
                Enter your name:
                <asp:TextBox ID="t_entername" runat="server" CssClass="form-control d-inline w-auto" />
                <asp:Button runat="server" Text="Click me" OnClick="mybutton_Click" ID="mybutton" CssClass="btn btn-outline-light ms-2" />
            </p>

            <asp:Panel ID="msg_box" runat="server" CssClass="alert alert-success mt-3 fade show" Visible="False">
                <asp:Label ID="lbl_message" runat="server" />
            </asp:Panel>
        </section>

        <hr class="my-5 border-secondary" />

        <section class="row">
            <h3 class="mb-3">Simple Calculator</h3>

            <div class="form-group mb-3">
                <label for="txtNumber1">Number 1</label>
                <asp:TextBox ID="txtNumber1" runat="server" CssClass="form-control bg-secondary text-light border-0" />
            </div>

            <div class="form-group mb-3">
                <label for="ddlOperator">Operator</label>
                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control bg-secondary text-light border-0">
                    <asp:ListItem Text="+" Value="Add" />
                    <asp:ListItem Text="-" Value="Subtract" />
                    <asp:ListItem Text="×" Value="Multiply" />
                    <asp:ListItem Text="÷" Value="Divide" />
                </asp:DropDownList>
            </div>

            <div class="form-group mb-3">
                <label for="txtNumber2">Number 2</label>
                <asp:TextBox ID="txtNumber2" runat="server" CssClass="form-control bg-secondary text-light border-0" />
            </div>

            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="btn btn-outline-light chrome-btn" OnClick="btnCalculate_Click" />

            <asp:Panel ID="ResultPanel" runat="server" CssClass="alert mt-4 fade show" Visible="False">
                <asp:Label ID="lblResult" runat="server" />
            </asp:Panel>
        </section>
    </main>
    <asp:TextBox ID="TextBox1" runat="server" />
    <asp:Label ID="Label1" runat="server" />
    <asp:Panel ID="Panel1" runat="server" />
    <asp:TextBox ID="TextBox2" runat="server" />
    <asp:TextBox ID="TextBox3" runat="server" />
    <asp:DropDownList ID="DropDownList1" runat="server" />
       <asp:Label ID="Label2" runat="server" />
    <asp:Panel ID="Panel2" runat="server" />
    <style>
        .chrome-btn {
            background: linear-gradient(to bottom, #f0f0f0, #ccc);
            border: 1px solid #999;
            color: #000;
            font-weight: bold;
            box-shadow: inset 0 1px 0 #fff, 0 1px 2px rgba(0,0,0,.2);
        }

        .chrome-btn:hover {
            background: linear-gradient(to bottom, #e0e0e0, #bbb);
            border-color: #666;
            color: #000;
        }
    </style>

    <script type="text/javascript">
        function fadeInMsgBox() {
            var msgBox = document.getElementById('<%= msg_box.ClientID %>');
            if (msgBox) {
                msgBox.classList.remove("fade");
                void msgBox.offsetWidth;
                msgBox.classList.add("fade", "show");
            }
        }
    </script>
</asp:Content>