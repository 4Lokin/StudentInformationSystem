<%@ Page Title="Home Page" Language="VB"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Default.aspx.vb"
    Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />

  <main class="container mt-5 text-light bg-dark p-4 rounded">
    <!-- Greeting section -->
    <section class="row mb-5">
      <h2>Welcome to the Student Information System</h2>
      <div class="mt-3">
        <asp:TextBox ID="t_entername" runat="server"
                     CssClass="form-control d-inline w-auto"
                     placeholder="Enter your name" />
        <asp:Button ID="mybutton" runat="server" Text="Click me"
                    CssClass="btn btn-outline-light ms-2"
                    OnClick="mybutton_Click" />
      </div>
      <asp:Panel ID="msg_box" runat="server"
                 CssClass="alert alert-success mt-3 fade show"
                 Visible="False">
        <asp:Label ID="lbl_message" runat="server" />
      </asp:Panel>
    </section>

    <hr class="border-secondary" />

    <!-- Calculator section -->
    <section class="row">
      <h3>Simple Calculator</h3>

      <div class="mb-3">
        <label for="txtNumber1">Number 1</label>
        <asp:TextBox ID="txtNumber1" runat="server"
                     CssClass="form-control bg-secondary text-light border-0"
                     placeholder="e.g. 12.34" />
      </div>

      <div class="mb-3">
        <label for="ddlOperator">Operator</label>
        <asp:DropDownList ID="ddlOperator" runat="server"
                          CssClass="form-control bg-secondary text-light border-0">
          <asp:ListItem Text="+" Value="Add" />
          <asp:ListItem Text="−" Value="Subtract" />
          <asp:ListItem Text="×" Value="Multiply" />
          <asp:ListItem Text="÷" Value="Divide" />
        </asp:DropDownList>
      </div>

      <div class="mb-3">
        <label for="txtNumber2">Number 2</label>
        <asp:TextBox ID="txtNumber2" runat="server"
                     CssClass="form-control bg-secondary text-light border-0"
                     placeholder="e.g. 56.78" />
      </div>

      <asp:Button ID="btnCalculate" runat="server" Text="Calculate"
                  CssClass="btn btn-outline-light chrome-btn"
                  OnClick="btnCalculate_Click" />

      <asp:Panel ID="ResultPanel" runat="server"
                 CssClass="alert mt-4 fade show"
                 Visible="False">
        <asp:Label ID="lblResult" runat="server" />
      </asp:Panel>
    </section>
  </main>

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