# ASP.NET Web Forms Project with SQLite CRUD

This project demonstrates how to use ASP.NET Web Forms with:

- 💾 SQLite database for full CRUD (Create, Read, Update, Delete)
- Watch this database installation and setup video here - https://youtu.be/qQqguGHsj2E
---

## 📁 Prerequisites

- Visual Studio 2022
- System.Data.SQLite NuGet package
- SQLite database file (e.g., `test.db`)
- DB Browser for SQLite (optional, for DB inspection)

---

## 📦 Project Structure

- `Default.aspx` - UI markup
- `Default.aspx.vb` - Code-behind logic
- `test.db` - SQLite database (placed in `App_Data`)
- `Web.config` - Configuration + connection string

---


## 💾 SQLite CRUD Example

### Web.config
```xml
<connectionStrings>
  <add name="SQLiteConn"
       connectionString="Data Source=|DataDirectory|\test.db;Version=3;"
       providerName="System.Data.SQLite" />
</connectionStrings>
```

Example
```
<connectionStrings>
	<add name="SQLiteConn"
		 connectionString="Data Source=C:\Users\Anish\Desktop\test.db;Version=3;"
		 providerName="System.Data.SQLite" />
</connectionStrings>
```
Database path - C:\Users\Anish\Desktop\test.db

### Default.aspx
```aspx
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox><br />

<asp:TextBox ID="txtAddress" runat="server" Placeholder="Address"></asp:TextBox><br />
<asp:Button ID="btnAdd" runat="server" Text="Add User" OnClick="btnAdd_Click" /><br /><br />

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
    OnRowEditing="GridView1_RowEditing"
    OnRowCancelingEdit="GridView1_RowCancelingEdit"
    OnRowUpdating="GridView1_RowUpdating"
    OnRowDeleting="GridView1_RowDeleting">
    
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Address" HeaderText="Address" />

        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>

</asp:Content>
```

### Default.aspx.vb (CRUD Logic)
```vb
Imports System.Data.SQLite
Imports System.Data
Imports System.Configuration
Public Class _Default
    Inherits System.Web.UI.Page
    Private connString As String = ConfigurationManager.ConnectionStrings("SQLiteConn").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUsers()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Using conn As New SQLiteConnection(connString)
            conn.Open()
            Dim cmd As New SQLiteCommand("INSERT INTO users (name, address) VALUES (@Name, @Address)", conn)
            cmd.Parameters.AddWithValue("@Name", txtName.Text)
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            cmd.ExecuteNonQuery()
        End Using
        LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Using conn As New SQLiteConnection(connString)
            conn.Open()
            Dim cmd As New SQLiteCommand("SELECT * FROM users", conn)
            Dim da As New SQLiteDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using
    End Sub


    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        LoadUsers()
    End Sub


    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
        LoadUsers()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)
        Dim name As String = CType(GridView1.Rows(e.RowIndex).Cells(1).Controls(0), TextBox).Text
        Dim email As String = CType(GridView1.Rows(e.RowIndex).Cells(2).Controls(0), TextBox).Text

        Using conn As New SQLiteConnection(connString)
            conn.Open()
            Dim cmd As New SQLiteCommand("UPDATE users SET Name = @Name, address = @Address WHERE Id = @Id", conn)
            cmd.Parameters.AddWithValue("@Name", name)
            cmd.Parameters.AddWithValue("@Address", email)
            cmd.Parameters.AddWithValue("@Id", id)
            cmd.ExecuteNonQuery()
        End Using

        GridView1.EditIndex = -1
        LoadUsers()
    End Sub


    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)

        Using conn As New SQLiteConnection(connString)
            conn.Open()
            Dim cmd As New SQLiteCommand("DELETE FROM Users WHERE Id = @Id", conn)
            cmd.Parameters.AddWithValue("@Id", id)
            cmd.ExecuteNonQuery()
        End Using

        LoadUsers()
    End Sub



End Class
```

---

## ✅ Table Schema (Run in DB Browser)
```sql
CREATE TABLE users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Address TEXT NOT NULL
);
```

---

## 🙌 How to Run

1. Unzip the project and open `.sln` in Visual Studio 2022
2. Ensure `System.Data.SQLite` is installed via NuGet
3. Build and run with `F5`
4. Make sure `test.db` is in `App_Data` folder

You're ready to go!

