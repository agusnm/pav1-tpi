﻿Public Class FormClientes

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

    'LOADER DEL FORM
    Private Sub FormClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargar_grilla_cliente()
        Soporte.cargar_combo(cmb_tipo_documento_cliente_carga, Soporte.leerBD("SELECT * FROM tipo_documento"), "id_tipo_documento", "nombre_tipo_documento")
        Soporte.cargar_combo(cmb_tipo_documento_cliente_busqueda, Soporte.leerBD("SELECT * FROM tipo_documento"), "id_tipo_documento", "nombre_tipo_documento")
    End Sub

    'SUBRUTINA PARA CARGAR GRILLAS
    Private Sub cargar_grilla_cliente()
        Dim tabla As New DataTable
        Dim sql_cargar_grilla As String = ""
        sql_cargar_grilla &= "SELECT * FROM clientes c "
        sql_cargar_grilla &= "JOIN tipo_documento td ON c.tipo_documento = td.id_tipo_documento"

        tabla = Soporte.leerBD(sql_cargar_grilla)

        Dim c As Integer
        Me.grid_clientes.Rows.Clear()
        For c = 0 To tabla.Rows.Count - 1

            Me.grid_clientes.Rows.Add()
            Me.grid_clientes.Rows(c).Cells(0).Value = tabla.Rows(c)("apellido_cliente")
            Me.grid_clientes.Rows(c).Cells(1).Value = tabla.Rows(c)("nombre_cliente")
            Me.grid_clientes.Rows(c).Cells(2).Value = tabla.Rows(c)("nombre_tipo_documento")
            Me.grid_clientes.Rows(c).Cells(3).Value = tabla.Rows(c)("numero_documento")
            Me.grid_clientes.Rows(c).Cells(4).Value = tabla.Rows(c)("e_mail_cliente")
            Me.grid_clientes.Rows(c).Cells(5).Value = tabla.Rows(c)("telefono_cliente")
        Next
        Me.txt_apellido_cliente_carga.Focus()
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

            If obj.GetType().Name = "ComboBox" Then
                Dim local As ComboBox = obj
                local.SelectedValue = -1
            End If
            Me.ocultar_lblERROR()
        Next
    End Sub

    'BOTON PARA BLANQUEAR NUEVO CLIENTE
    Private Sub btn_nuevo_cliente_carga_Click(sender As Object, e As EventArgs) Handles btn_nuevo_cliente_carga.Click
        'If MessageBox.Show("¿Está seguro que desea eliminar los datos ingresados?", "Importante", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
        Me.borrar_datos()
        Me.accion = tipo_grabacion.insertar
        Me.btn_guardar_cliente_carga.Enabled = True
        Me.txt_apellido_cliente_carga.Enabled = True
        Me.txt_nombre_cliente_carga.Enabled = True
        Me.txt_numero_documento_carga.Enabled = True
        Me.cmb_tipo_documento_cliente_carga.Enabled = True
        Me.txt_email_cliente_cliente_carga.Enabled = True
        Me.txt_telefono_cliente_carga.Enabled = True
        Me.txt_apellido_cliente_carga.Focus()
        Me.cargar_grilla_cliente()
        ' End If
    End Sub


    'FUNCION PARA VALIDAR DATOS INGRESADOS
    Private Function validar_datos() As respuesta_validacion
        Me.ocultar_lblERROR()
        Dim rdo = respuesta_validacion._ok
        If txt_apellido_cliente_carga.Text = "" Then
            lbl_apellidoERROR.Visible = True
            txt_apellido_cliente_carga.Focus()
            rdo = respuesta_validacion._error
            MsgBox("El apellido no fue ingresado", MsgBoxStyle.OkOnly, "Error")
        End If
        If txt_nombre_cliente_carga.Text = "" Then
            lbl_nombreERROR.Visible = True
            txt_nombre_cliente_carga.Focus()
            rdo = respuesta_validacion._error
            MsgBox("El nombre no fue ingresado", MsgBoxStyle.OkOnly, "Error")
        End If

        If cmb_tipo_documento_cliente_carga.Text = "" Then
            lbl_tipodocERROR.Visible = True
            cmb_tipo_documento_cliente_carga.Focus()
            rdo = respuesta_validacion._error
            MsgBox("El tipo de documento no fue ingresado", MsgBoxStyle.OkOnly, "Error")
        End If
        
        If txt_numero_documento_carga.Text = "" Then
            lbl_documentoERROR.Visible = True
            txt_numero_documento_carga.Focus()
            rdo = respuesta_validacion._error
            MsgBox("El numero de documento no fue ingresado", MsgBoxStyle.OkOnly, "Error")
        End If

        Return rdo

    End Function


    'SUBRUTINA PARA OCULTAR LOS LABELS X
    Private Sub ocultar_lblERROR()
        lbl_apellidoERROR.Visible = False
        lbl_documentoERROR.Visible = False
        lbl_nombreERROR.Visible = False
        lbl_tipodocERROR.Visible = False

    End Sub

    'FUNCION PARA VALIDAR UNA PERSONA (PARA QUE NO EXISTA)
    Private Function validar_persona() As respuesta_validacion
        Dim tabla As New DataTable
        Dim sql As String = ""
        sql &= "SELECT numero_documento, tipo_documento FROM clientes WHERE numero_documento = " & Me.txt_numero_documento_carga.Text
        sql &= "AND tipo_documento =" & Me.cmb_tipo_documento_cliente_carga.SelectedValue

        tabla = Soporte.leerBD(sql)

        If tabla.Rows.Count = 1 Then
            MsgBox("La combinacion de tipo y numero de docuento ya existe", MsgBoxStyle.OkOnly, "Error")
            Return respuesta_validacion._error
        End If

        Return respuesta_validacion._ok
    End Function

    'SUBRUTINA PARA GUARDAR INFORMACION A LA BD
    Private Sub btn_guardar_cliente_carga_Click(sender As Object, e As EventArgs) Handles btn_guardar_cliente_carga.Click
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
        sql &= "INSERT INTO clientes("
        sql &= "numero_documento,"
        sql &= "tipo_documento,"
        sql &= "nombre_cliente,"
        sql &= "apellido_cliente,"
        sql &= "telefono_cliente,"
        sql &= "e_mail_cliente)"
        sql &= " VALUES("
        sql &= " ' " & Me.txt_numero_documento_carga.Text & "'"
        sql &= "," & Me.cmb_tipo_documento_cliente_carga.SelectedValue
        sql &= ", '" & Me.txt_nombre_cliente_carga.Text & "'"
        sql &= ", '" & Me.txt_apellido_cliente_carga.Text & "'"
        sql &= "," & Me.txt_telefono_cliente_carga.Text
        sql &= ", '" & Me.txt_email_cliente_cliente_carga.Text & "')"

        Soporte.escribirBD(sql)
        Me.cargar_grilla_cliente()
        Me.btn_guardar_cliente_carga.Enabled = False
        Me.txt_apellido_cliente_carga.Enabled = False
        Me.txt_nombre_cliente_carga.Enabled = False
        Me.txt_numero_documento_carga.Enabled = False
        Me.cmb_tipo_documento_cliente_carga.Enabled = False
        Me.txt_email_cliente_cliente_carga.Enabled = False
        Me.txt_telefono_cliente_carga.Enabled = False

        MsgBox("La carga del cliente fue exitosa", MessageBoxButtons.OK, "Carga Cliente")
    End Sub

    'SUBRUTINA PARA INTERACCION DE GRILLA
    Private Sub grid_clientes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grid_clientes.CellContentClick
        Dim sql As String = ""
        Dim tabla As New DataTable
        sql &= " SELECT * FROM clientes "
        sql &= " WHERE numero_documento = '" & Me.grid_clientes.CurrentRow.Cells(3).Value & "'"

        tabla = Soporte.leerBD(sql)

        Me.txt_apellido_cliente_carga.Text = tabla.Rows(0)("apellido_cliente")
        Me.txt_nombre_cliente_carga.Text = tabla.Rows(0)("nombre_cliente")
        Me.cmb_tipo_documento_cliente_carga.SelectedValue = tabla.Rows(0)("tipo_documento")
        Me.txt_numero_documento_carga.Text = tabla.Rows(0)("numero_documento")
        Me.txt_email_cliente_cliente_carga.Text = tabla.Rows(0)("e_mail_cliente")
        Me.txt_telefono_cliente_carga.Text = tabla.Rows(0)("telefono_cliente")

        Me.accion = tipo_grabacion.modificar
        Me.txt_apellido_cliente_carga.Enabled = True
        Me.txt_nombre_cliente_carga.Enabled = True
        Me.txt_numero_documento_carga.Enabled = False
        Me.cmb_tipo_documento_cliente_carga.Enabled = False
        Me.txt_email_cliente_cliente_carga.Enabled = True
        Me.txt_telefono_cliente_carga.Enabled = True
        Me.btn_guardar_cliente_carga.Enabled = True
        Me.btn_eliminar_cliente_carga.Enabled = True
    End Sub

    'SUBRUTINA PARA MODIFICAR CLIENTES
    Private Sub modificar()
        Dim sql As String = ""
        sql &= "UPDATE clientes SET "
        sql &= "apellido_cliente = '" & Me.txt_apellido_cliente_carga.Text & "'"
        sql &= ", nombre_cliente = '" & Me.txt_nombre_cliente_carga.Text & "'"
        sql &= ", e_mail_cliente = '" & Me.txt_email_cliente_cliente_carga.Text & "'"
        sql &= ", telefono_cliente = " & Me.txt_telefono_cliente_carga.Text
        sql &= " WHERE numero_documento = '" & Me.txt_numero_documento_carga.Text & "'"

        Soporte.escribirBD(sql)
        MsgBox("El cliente fue modificado", MessageBoxButtons.OK, "Exito")
        cargar_grilla_cliente()
        txt_apellido_cliente_carga.Enabled = False
        txt_email_cliente_cliente_carga.Enabled = False
        txt_nombre_cliente_carga.Enabled = False
        txt_telefono_cliente_carga.Enabled = False
        btn_eliminar_cliente_carga.Enabled = False
        btn_guardar_cliente_carga.Enabled = False
    End Sub

    'SUBRUTINA PARA BORRAR CLIENTES
    Private Sub btn_eliminar_cliente_carga_Click(sender As Object, e As EventArgs) Handles btn_eliminar_cliente_carga.Click
        Dim sql As String = ""
        sql &= "DELETE clientes WHERE numero_documento = '" & Me.grid_clientes.CurrentRow.Cells(3).Value & "'"

        If MessageBox.Show("¿Está seguro que quiere eliminar el registro?", "Importante", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            Soporte.escribirBD(sql)
            MsgBox("Se borraron los datos exitosamente", MessageBoxButtons.OK, "Eliminación Cliente")
            cargar_grilla_cliente()
        End If

        Me.btn_guardar_cliente_carga.Enabled = False
        Me.btn_eliminar_cliente_carga.Enabled = False
        Me.txt_apellido_cliente_carga.Enabled = False
        Me.txt_email_cliente_cliente_carga.Enabled = False
        Me.txt_nombre_cliente_carga.Enabled = False
        Me.txt_telefono_cliente_carga.Enabled = False


    End Sub

    'SUBRUTINA PARA PREGUNTAR CUANDO SE CIERRA EL FORMULARIO
    Private Sub formClientes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("¿Está seguro que quiere salir del formulario?", "Importante", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    'SUBRUTINA PARA BUSCAR Y ENCONTRAR UN CLIENTE X NUMERO Y TIPO DE DOCUMENTO
    Private Sub btn_buscar_cliente_Click(sender As Object, e As EventArgs) Handles btn_buscar_cliente.Click
        Dim sql As String = ""
        Dim tabla As New DataTable
        sql &= "SELECT * FROM clientes c JOIN tipo_documento td ON c.tipo_documento = td.id_tipo_documento "
        sql &= " WHERE td.nombre_tipo_documento = '" & Me.cmb_tipo_documento_cliente_busqueda.Text & "'"
        sql &= " AND c.numero_documento = " & Me.txt_numero_documento_cliente_busqueda.Text

        If txt_numero_documento_cliente_busqueda.Text = "" Then
            MsgBox("No existe valor de busqueda", MsgBoxStyle.OkOnly, "Error")
            txt_numero_documento_cliente_busqueda.Focus()

        Else
            tabla = Soporte.leerBD(sql)
            Dim c As Integer
            Me.grid_clientes.Rows.Clear()
            For c = 0 To tabla.Rows.Count - 1
                Me.grid_clientes.Rows.Add()
                Me.grid_clientes.Rows(c).Cells(0).Value = tabla.Rows(c)("apellido_cliente")
                Me.grid_clientes.Rows(c).Cells(1).Value = tabla.Rows(c)("nombre_cliente")
                Me.grid_clientes.Rows(c).Cells(2).Value = tabla.Rows(c)("nombre_tipo_documento")
                Me.grid_clientes.Rows(c).Cells(3).Value = tabla.Rows(c)("numero_documento")
                Me.grid_clientes.Rows(c).Cells(4).Value = tabla.Rows(c)("e_mail_cliente")
                Me.grid_clientes.Rows(c).Cells(5).Value = tabla.Rows(c)("telefono_cliente")
            Next
            If tabla.Rows.Count = 0 Then
                MsgBox("No se encontró ningun resultado", MsgBoxStyle.OkOnly, "Error")
                cargar_grilla_cliente()
            End If
        End If

    End Sub

    'FUNCION PARA VALIDAR DATOS A GUARDAR
    'Private Function validar_datos() As respuesta_validacion
    '    For Each obj As Windows.Forms.Control In Me.Controls
    '        If obj.GetType().Name = "TextBox" Or obj.GetType().Name = "MaskedTextBox" Then
    '            If obj.Text = "" Then
    '                MsgBox("El campo " + obj.Name + "esta vacio.", MsgBoxStyle.OkOnly, "Error")
    '                obj.Focus()
    '                Return respuesta_validacion._error
    '            End If
    '        End If

    '        If obj.GetType().Name = "ComboBox" Then
    '            Dim local As ComboBox = obj
    '            If local.SelectedValue = -1 Then
    '                MsgBox("El campo " + obj.Name + "esta vacio.", MsgBoxStyle.OkOnly, "Error")
    '                obj.Focus()
    '                Return respuesta_validacion._error
    '            End If
    '        End If
    '    Next
    '    Return respuesta_validacion._ok
    'End Function

End Class