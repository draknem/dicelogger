Imports System.Threading
Imports System.Math

Public Class Form1
    'Список кубиков
    Dim dice As New List(Of PictureBox)
    'Настройка формы при запуске
    Private Sub Form1_Load() Handles MyBase.Load
        Dim i As Integer
        'Список кубиков
        With dice
            .Add(PictureBox1)
            .Add(PictureBox2)
            .Add(PictureBox3)
            .Add(PictureBox4)
            .Add(PictureBox5)
        End With
        'Выравнивание но центру и расстановка элементов формы
        Label1.Top = 30
        Label1.Left = CInt((Me.Width - Label1.Width) / 2)
        Label2.Hide()

        dice(0).Show()
        dice(0).Left = CInt((Me.Width - PictureBox1.Width) / 2)
        dice(0).Top = Label1.Height + Label1.Top * 2
        Button1.Left = PictureBox1.Left
        Button1.Top = PictureBox1.Top + PictureBox1.Height + 10
        Button4.Left = Button1.Left
        Button4.Top = Button1.Top
        For i = 1 To 4
            dice(i).Hide()
            dice(i).Top = dice(0).Top
        Next

        Form2.Show()
    End Sub
    'Настройка формы перед игрой
    Public Sub Setup(ByVal n As Integer, ByVal k As Integer)
        Label2.Text = "Количество бросков: " & CStr(k)
        Label2.Show()
        Label4.Show()
        Dice_Relocate(n)
        Form2.Hide()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Button1.Show()
        Button4.Hide()
    End Sub
    'Настройка расположения и кол-ва кубиков
    Private Sub Dice_Relocate(ByVal n As Integer)
        Dim i As Integer
        For i = 0 To 4 'Спрятать все картинки
            dice(i).Hide()
        Next
        For i = 0 To n - 1 'Расставит нужные картинки
            dice(i).Left = CInt((Me.Width - PictureBox1.Width) / (n + 1) * (i + 1))
            dice(i).Show()
        Next
    End Sub
    'Бросок
    Private Sub Dice_Roll(ByRef score() As Integer)
        Dim i As Integer
        Randomize()
        ReDim score(dice.Count - 1)
        For i = 0 To dice.Count - 1
            score(i) = CInt(Rnd() * 5) + 1
            dice(i).Image = My.Resources.ResourceManager.GetObject(CStr(score(i)))
            Me.Refresh()
        Next
    End Sub
    'Анимация броска
    Private Sub Dice_Animate(ByRef score() As Integer)
        Dim i As Integer
        For i = 1 To 10
            Dice_Roll(score)
            Thread.Sleep(Exp(i / 2))
        Next
    End Sub
    'Вывод массива в листбокс
    Private Overloads Sub vivodL(ByVal z() As Integer, ByRef l As ListBox)
        Dim i, s As Integer
        For i = 0 To UBound(z)
            s = s + z(i)
        Next
        l.Items.Add(CStr(s))
    End Sub
    'Вывод строки в листбокс
    Private Overloads Sub vivodL(ByVal z As String, ByRef l As ListBox)
        l.Items.Add(z)
    End Sub
    'Подсчет итогового счета
    Private Function sumL(ByVal l As ListBox) As Integer
        Dim i, s As Integer
        For i = 1 To l.Items.Count - 1
            s = s + CStr(l.Items.Item(i))
        Next
        Return s
    End Function
    'начать игру
    Private Sub Button1_Click() Handles Button1.Click
        Button4.Show()
        Button1.Hide()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        vivodL("Игрок 1", ListBox1)
        vivodL("Игрок 2", ListBox2)
        Button4_Click()
    End Sub
    'выход
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form2.Close()
        End
    End Sub
    'настройки
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form2.Show()
    End Sub
    'бросок
    Private Sub Button4_Click() Handles Button4.Click
        Dim score(0) As Integer
        'Dice_Roll(score)
        Dice_Animate(score)

        If ListBox1.Items.Count = ListBox2.Items.Count Then
            vivodL(score, ListBox1)
            Label4.Text = "Ход 2 игрока"
        Else
            If CInt(Label2.Text.Substring(20, Label2.Text.Length - 20)) - ListBox2.Items.Count = 0 Then
                vivodL(score, ListBox2)
                Results(ListBox1, ListBox2)
                vivodL("Итого: " & sumL(ListBox1), ListBox1)
                vivodL("Итого: " & sumL(ListBox2), ListBox2)
                Label4.Text = "Игра окончена"
                Button1.Show()
                Button4.Hide()
                Button1.Select()
            Else
                vivodL(score, ListBox2)
                Label4.Text = "Ход 1 игрока"
            End If
        End If
    End Sub
    'Подсчет результатов и оповещение
    Private Sub Results(ByVal l1 As ListBox, ByVal l2 As ListBox)
        Dim i, w, l As Integer
        w = sumL(ListBox1)
        l = sumL(ListBox2)
        If w > i Then i = 1 Else i = 2
        MsgBox(("Выиграл игрок № " & CStr(i) & " со счетом " & CStr(w) & ":" & CStr(l)), _
               vbInformation, "Результаты игры")

    End Sub

    Private Sub b1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Button1.KeyPress
        If e.KeyChar = Chr(81) Or e.KeyChar = Chr(113) Then
            Button1_Click()
        End If
    End Sub

    Private Sub b4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Button4.KeyPress
        If e.KeyChar = Chr(81) Or e.KeyChar = Chr(113) Then
            If ListBox1.Items.Count = ListBox2.Items.Count Then
                Button4_Click()
            Else
                MsgBox(("Сейчас очередь 2 игрока"), vbExclamation, "Нарушение правил")
            End If
        End If
        If e.KeyChar = Chr(80) Or e.KeyChar = Chr(112) Then
            If ListBox1.Items.Count > ListBox2.Items.Count Then
                Button4_Click()
            Else
                MsgBox(("Сейчас очередь 1 игрока"), vbExclamation, "Нарушение правил")
            End If
        End If
    End Sub
End Class
