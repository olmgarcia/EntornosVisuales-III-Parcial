Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Public Class Usuario
    Dim conexion As New conexion()

    Private Sub Usuario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.conectar()
        mostrarDatos()
    End Sub

    Private Function validarCorreo(ByVal isCorreo As String) As Boolean
        Return Regex.IsMatch(isCorreo, "^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")
    End Function

    Private Sub mostrarDatos()
        Dim cmd As New SqlCommand("Select * from usuario", conexion.conexion)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            conexion.conexion.Open()
            cmd.CommandType = CommandType.Text
            da.Fill(dt)
            If dt.Rows.Count <> 0 Then
                DTGUsuario.DataSource = dt
                conexion.conexion.Close()
            Else
                DTGUsuario.DataSource = Nothing
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function validarTextBox() As Integer
        If (TxtUsuario.Text <> String.Empty And TxtNombre.Text <> String.Empty And TxtApellido.Text <> String.Empty And TxtCorreo.Text <> String.Empty And TxtCodigo.Text <> String.Empty And TxtContraseña.Text <> String.Empty And IsNumeric(TxtCodigo.Text)) Then
            Return 1
        Else
            Return 0
        End If

    End Function

    Private Sub limpiar()
        TxtCodigo.Clear()
        TxtNombre.Clear()
        TxtApellido.Clear()
        TxtUsuario.Clear()
        TxtContraseña.Clear()
        TxtCorreo.Clear()
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        limpiar()
    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Dim opcion As Integer

        opcion = MessageBox.Show("¿Desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If opcion = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        If validarCorreo(LCase(TxtCorreo.Text)) = False Then
            MessageBox.Show("Correo invalido, *username@midominio.com*", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtCorreo.Focus()
            TxtCorreo.SelectAll()
        Else
            insertarUsuaurio()
            MessageBox.Show("Correo valido", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            mostrarDatos()
        End If
    End Sub

    Private Sub insertarUsuaurio()
        Dim idUsuario As Integer
        Dim nombre, apellido, userName, psw, correo, rol, estado As String
        idUsuario = TxtCodigo.Text
        nombre = TxtNombre.Text
        apellido = TxtApellido.Text
        userName = TxtUsuario.Text
        psw = TxtContraseña.Text
        correo = TxtCorreo.Text
        estado = "activo"
        rol = CmbRol.Text
        Try
            If conexion.insertarUsuario(idUsuario, nombre, apellido, userName, psw, rol, estado, correo) Then
                MessageBox.Show("Guardado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al guardar", "Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub eliminarUsuario()
        Dim idUsuario As Integer
        Dim rol As String
        idUsuario = TxtCodigo.Text
        rol = CmbRol.Text
        Try
            If (conexion.eliminarUsuario(idUsuario, rol)) Then
                MsgBox("Dado de baja")
                conexion.conexion.Close()
            Else
                MsgBox("Error al dar de baja usuario")
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        eliminarUsuario()
    End Sub

    Private Sub modificarUsuario()
        Dim idUsuario As Integer
        Dim nombre, apellido, userName, psw, correo, rol As String
        idUsuario = TxtCodigo.Text
        nombre = TxtNombre.Text
        apellido = TxtApellido.Text
        userName = TxtUsuario.Text
        psw = TxtContraseña.Text
        correo = TxtCorreo.Text
        rol = CmbRol.Text
        Try
            If conexion.modificarUsuario(idUsuario, nombre, apellido, userName, psw, rol, correo) Then
                MessageBox.Show("Cliente Actualizado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Error al actualizar cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        If validarCorreo(LCase(TxtCorreo.Text)) = False And validarTextBox() = 0 Then
            MessageBox.Show("Error al modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf validarCorreo(LCase(TxtCorreo.Text)) = True And validarTextBox() = 1 Then
            modificarUsuario()
            mostrarDatos()
        End If
    End Sub

    Private Sub buscarUsuario()

        Dim userName As String
        userName = TxtBuscar.Text
        Try
            If (conexion.buscarUsuario(userName)) Then
                MsgBox("Encontrado")
                mostrarBusqueda()
                'conexion.conexion.Close()
            Else
                MsgBox("Error al buscar")
                'conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub mostrarBusqueda()
        Dim cmd As New SqlCommand("buscarUsuario", conexion.conexion)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            conexion.conexion.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@userName", TxtBuscar.Text)
            da.Fill(dt)
            If dt.Rows.Count <> 0 Then
                DTGUsuario.DataSource = dt
                conexion.conexion.Close()
            Else
                DTGUsuario.DataSource = Nothing
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        buscarUsuario()
    End Sub

End Class