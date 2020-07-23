Imports System.Text.RegularExpressions
Public Class Usuario
    Dim conexion As New conexion()

    Private Sub Usuario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.conectar()
    End Sub

    Private Function validarCorreo(ByVal isCorreo As String) As Boolean
        Return Regex.IsMatch(isCorreo, "^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")
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
                'conexion.conexion.Close()
            Else
                MsgBox("Error al dar de baja usuario")
                'conexion.conexion.Close()
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
        rol = CmbRol.Text
        correo = TxtCorreo.Text

        Try
            If conexion.modificarUsuario(idUsuario, nombre, apellido, userName, psw, rol, correo) Then
                MessageBox.Show("Modificado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al modificar", "Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        modificarUsuario()
    End Sub

    Private Sub buscarUsuario()
        Dim idUsuario As Integer
        Dim userName As String
        idUsuario = TxtCodigo.Text
        userName = TxtUsuario.Text

        Try
            If conexion.buscarUsuario(idUsuario, userName) Then
                MessageBox.Show("Se encontro", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                conexion.conexion.Close()
            Else
                MessageBox.Show("Error al Buscar", "Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
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