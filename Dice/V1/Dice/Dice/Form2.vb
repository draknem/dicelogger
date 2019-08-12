Public Class Form2
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = "5"
        TextBox2.Text = "3"
        Me.Top = Me.Top + 60
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CInt(TextBox1.Text) > 1 Then TextBox1.Text = CStr(CInt(TextBox1.Text) - 1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If CInt(TextBox1.Text) < 5 Then TextBox1.Text = CStr(CInt(TextBox1.Text) + 1)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If CInt(TextBox2.Text) > 1 Then TextBox2.Text = CStr(CInt(TextBox2.Text) - 1)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox2.Text = CStr(CInt(TextBox2.Text) + 1)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Form1.Setup(CInt(TextBox1.Text), CInt(TextBox2.Text))
        Form1.Button1.Select()
    End Sub
End Class