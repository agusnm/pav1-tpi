﻿Public Class FormUsuarios


    Dim cadena_conexion As String = "Provider=SQLNCLI11;Data Source=(local)\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=BD_CLOTTA"
    Dim accion As tipo_grabacion = tipo_grabacion.insertar
    Dim seleccion As String




    'ENUMERACION DE TIPOS DE GRABACION
    Enum tipo_grabacion
        insertar
        modificar
    End Enum



    'ENUMERACION DE TIPOS DE RESPUESTAS DE VALIDACION
    Enum respuesta_validacion
        _ok
        _error
    End Enum
    Private Sub FormUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargar_grilla_usuarios()
    End Sub
    'SUBRUTINA PARA CARGAR GRILLAS
    Private Sub cargar_grilla_usuarios()

        Dim tabla As New DataTable
        Dim sql_cargar_grilla As String = ""

        sql_cargar_grilla &= "SELECT usuarios.* FROM usuarios"


        tabla = ejecuto_sql(sql_cargar_grilla)

        Dim c As Integer
        Me.grilla_usuarios.Rows.Clear()
        For c = 0 To tabla.Rows.Count - 1

            Me.grilla_usuarios.Rows.Add()
            Me.grilla_usuarios.Rows(c).Cells(0).Value = tabla.Rows(c)("nombre")
            Me.grilla_usuarios.Rows(c).Cells(1).Value = tabla.Rows(c)("apellido")
            Me.grilla_usuarios.Rows(c).Cells(2).Value = tabla.Rows(c)("fecha_alta")


        Next

        Me.txt_nombre.Focus()

    End Sub



    'FUNCION PARA EJECUTAR CONSULTAS SQL
    Private Function ejecuto_sql(ByVal sql As String)

        Dim conexion As New Data.OleDb.OleDbConnection
        Dim cmd As New Data.OleDb.OleDbCommand
        Dim tabla As New DataTable

        conexion.ConnectionString = cadena_conexion
        conexion.Open()
        cmd.Connection = conexion
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sql
        tabla.Load(cmd.ExecuteReader())
        conexion.Close()
        Return tabla
    End Function



    'SUBRUTINA PARA EJECUTAR INSERCIONES Y ELIMINACIONES EN LA BD
    Private Sub grabar_borrar(ByVal sql As String)
        Dim conexion As New Data.OleDb.OleDbConnection
        Dim cmd As New Data.OleDb.OleDbCommand
        Dim tabla As New DataTable

        conexion.ConnectionString = cadena_conexion
        conexion.Open()
        cmd.Connection = conexion
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        conexion.Close()
    End Sub
    'SUBRUTINA PARA BLANQUEAR LOS CAMPOS
    Private Sub borrar_datos()

        For Each obj As Windows.Forms.Control In Me.Controls

            If obj.GetType().Name = "TextBox" Then

                obj.Text = ""


            End If

            If obj.GetType().Name = "MaskedTextBox" Then

                obj.Text = ""

            End If



        Next
    End Sub
    'BOTON PARA BLANQUEAR NUEVO usuario
    Private Sub cmd_nuevo_Click(sender As Object, e As EventArgs) Handles cmd_nuevo.Click
        'If MessageBox.Show("¿Está seguro que desea eliminar los datos ingresados?", "Importante", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
        Me.borrar_datos()
        Me.accion = tipo_grabacion.insertar
        Me.cmd_grabar.Enabled = True
        Me.txt_apellido.Enabled = True
        Me.txt_nombre.Enabled = True
        Me.txt_contraseña1.Enabled = True
        Me.txt_contraseña2.Enabled = True
        Me.txt_fecha_alta.Enabled = True
        Me.txt_nombre.Focus()
        Me.cargar_grilla_usuarios()
        ' End If

    End Sub
    'FUNCION PARA VALIDAR DATOS A GUARDAR
    Private Function validar_datos() As respuesta_validacion

        For Each obj As Windows.Forms.Control In Me.Controls

            If obj.GetType().Name = "TextBox" Or obj.GetType().Name = "MaskedTextBox" Then

                If obj.Text = "" Then

                    MsgBox("El campo " + obj.Name + "esta vacio.", MsgBoxStyle.OkOnly, "Error")
                    obj.Focus()
                    Return respuesta_validacion._error

                End If

            End If



        Next

        Return respuesta_validacion._ok

    End Function



    'FUNCION PARA VALIDAR UN usuario (PARA QUE NO EXISTA)
    Private Function validar_persona() As respuesta_validacion

        Dim tabla As New DataTable
        Dim sql As String = ""

        sql &= "SELECT nombre FROM usuarios WHERE nombre = " & Me.txt_nombre.Text

        tabla = ejecuto_sql(sql)

        If tabla.Rows.Count = 1 Then
            Return respuesta_validacion._error
        End If

        Return respuesta_validacion._ok

    End Function
    'SUBRUTINA PARA GUARDAR INFORMACION A LA BD

    Private Sub cmd_grabar_Click(sender As Object, e As EventArgs) Handles cmd_grabar.Click
        If validar_datos() = respuesta_validacion._ok Then

            If accion = tipo_grabacion.insertar Then

                If validar_persona() = respuesta_validacion._ok Then

                    insertar()
                    'Me.borrar_datos()

                End If
            Else
                modificar()

            End If



        End If

    End Sub
    'SUBRUTINA PARA INSERTAR DATOS
    Private Sub insertar()

        Dim sql As String = ""

        sql &= "INSERT INTO usuarios("
        sql &= "apellido,"
        sql &= "nombre,"
        sql &= "contraseña,"
        sql &= "fecha_alta) "
        sql &= " VALUES("
        sql &= " '" & Me.txt_nombre.Text & "'"
        sql &= ",  '" & Me.txt_apellido.Text & "'"
        sql &= ", '" & Me.txt_contraseña1.Text & "'"
        sql &= "," & Me.txt_fecha_alta.Text & "')"
        If Me.txt_contraseña1.Text = Me.txt_contraseña2.Text Then
            MsgBox("La carga del usuario fue exitosa", MessageBoxButtons.OK, "Carga Usuario")
        Else
            MsgBox("Error al cargar la contraseña nuevamente, vuelva a ingresarla", MessageBoxButtons.OK, "Carga Usuario")
            Me.txt_contraseña1.Focus()
            Me.txt_contraseña1.Text = ""
            Me.txt_contraseña2.Text = ""
        End If
        Me.grabar_borrar(sql)
        Me.cargar_grilla_usuarios()
        Me.cmd_grabar.Enabled = False
        Me.txt_apellido.Enabled = False
        Me.txt_nombre.Enabled = False
        Me.txt_contraseña1.Enabled = False
        Me.txt_contraseña2.Enabled = False
        Me.txt_fecha_alta.Enabled = False





    End Sub
    'SUBRUTINA PARA INTERACCION DE GRILLA

    Private Sub grilla_usuarios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grilla_usuarios.CellContentClick
        Dim sql As String = ""
        Dim tabla As New DataTable

        sql &= " SELECT * FROM usuarios "
        sql &= " WHERE nombre = " & Me.grilla_usuarios.CurrentRow.Cells(0).Value

        tabla = Me.ejecuto_sql(sql)


        Me.txt_nombre.Text = tabla.Rows(0)("nombre")
        Me.txt_apellido.Text = tabla.Rows(0)("apellido")
        Me.txt_fecha_alta.Text = tabla.Rows(0)("fecha_alta")

        Me.accion = tipo_grabacion.modificar
        Me.txt_apellido.Enabled = True
        Me.txt_nombre.Enabled = True
        Me.txt_fecha_alta.Enabled = False
        Me.txt_contraseña1.Enabled = False
        Me.txt_contraseña2.Enabled = True
        Me.cmd_grabar.Enabled = True
    End Sub
    'SUBRUTINA PARA MODIFICAR usuarios
    Private Sub modificar()

        Dim sql As String = ""

        sql &= "UPDATE usuarios SET "
        sql &= "apellido = '" & Me.txt_apellido.Text & "'"
        sql &= ", nombre = '" & Me.txt_nombre.Text & "'"
        sql &= ", contraseña = '" & Me.txt_contraseña1.Text & "'"
        sql &= ", fecha_alta = " & Me.txt_fecha_alta.Text
        sql &= " WHERE nombre = " & Me.txt_nombre.Text


        grabar_borrar(sql)

        MsgBox("El usuario fue modificado", MessageBoxButtons.OK, "Exito")

        cargar_grilla_usuarios()

    End Sub
    'SUBRUTINA PARA BORRAR usuarios

    Private Sub cmd_eliminar_Click(sender As Object, e As EventArgs) Handles cmd_eliminar.Click
        Dim sql As String = ""

        sql &= "DELETE usuarios WHERE nombre = " & Me.grilla_usuarios.CurrentRow.Cells(0).Value

        If MessageBox.Show("¿Está seguro que quiere eliminar el registro?", "Importante", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            Me.grabar_borrar(sql)
            MsgBox("Se borraron los datos exitosamente", MessageBoxButtons.OK, "Eliminación Usuario")
            cargar_grilla_usuarios()
        End If

        If Me.grilla_usuarios.CurrentCell.Selected = False Then
            MessageBox.Show("Falta seleccionar dato en grilla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    'SUBRUTINA PARA PREGUNTAR CUANDO SE CIERRA EL FORMULARIO
    Private Sub FormUsuarios_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("Está seguro que quiere salir del formulario", "Importante", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub
    'SUBRUTINA PARA BUSCAR Y ENCONTRAR UN usuario X su nombre

    Private Sub cmd_buscar_Click(sender As Object, e As EventArgs) Handles cmd_buscar.Click
        Dim sql As String = ""
        Dim tabla As New DataTable
        sql &= "SELECT * FROM usuarios c JOIN nombre td ON nombre = td.nombre "
        sql &= " WHERE td.nombre = '" & Me.txt_buscar_usuario.Text & "'"

        tabla = Me.ejecuto_sql(sql)

        Dim c As Integer
        Me.grilla_usuarios.Rows.Clear()
        For c = 0 To tabla.Rows.Count - 1

            Me.grilla_usuarios.Rows.Add()
            Me.grilla_usuarios.Rows(c).Cells(0).Value = tabla.Rows(c)("nombre")
            Me.grilla_usuarios.Rows(c).Cells(1).Value = tabla.Rows(c)("apellido")
            Me.grilla_usuarios.Rows(c).Cells(2).Value = tabla.Rows(c)("fecha_alta")


        Next

        If tabla.Rows.Count = 0 Then
            MsgBox("No se encontró ningun resultado", MsgBoxStyle.OkOnly, "Error")
            cargar_grilla_usuarios()
        End If
    End Sub
End Class