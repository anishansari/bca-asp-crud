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